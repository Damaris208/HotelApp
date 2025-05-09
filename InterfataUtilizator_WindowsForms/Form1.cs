﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using NivelModele;
using NivelStocareDate;
using System.Drawing;
using System.Linq;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1 : Form
    {
        private const int LUNGIME_MINIMA_NUME = 2;
        private const int LUNGIME_MINIMA_TELEFON = 10;

        private DataGridView dgvCamere;
        private Label lblStatistici;

        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail;
        private Label lblNume, lblPrenume, lblTelefon, lblEmail, lblMesajEroare;

        public Form1()
        {
            InitializeComponent();
            ConfigureazaFormular();
            AdaugaControale();
            IncarcaCamere();
        }

        private void ConfigureazaFormular()
        {
            this.Text = "Detalii Camere Hotel + Clienti";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void AdaugaControale()
        {
            var panelPrincipal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.White
            };

            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Antet
            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Conținut (form + tabel)
            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Statistici

            this.Controls.Add(panelPrincipal);

            // Antet
            var panelAntet = new Panel
            {
                BackColor = Color.SteelBlue,
                Dock = DockStyle.Fill
            };

            var lblTitlu = new Label
            {
                Text = "LISTA CAMERE HOTEL",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            panelAntet.Controls.Add(lblTitlu);
            panelPrincipal.Controls.Add(panelAntet);

            // Zona mijloc: 2 coloane - formular + tabel
            var zonaMijloc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            panelPrincipal.Controls.Add(zonaMijloc);

            // Formular în partea stângă
            var panelFormular = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 10, 20, 20),
                ColumnCount = 2,
                RowCount = 9,
                AutoSize = true
            };

            panelFormular.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panelFormular.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            for (int i = 0; i < 9; i++)
                panelFormular.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 4 ? 45 : 35));

            lblNume = new Label { Text = "Nume:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtNume = new TextBox { Width = 250 };
            lblPrenume = new Label { Text = "Prenume:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtPrenume = new TextBox { Width = 250 };
            lblTelefon = new Label { Text = "Telefon:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtTelefon = new TextBox { Width = 250 };
            lblEmail = new Label { Text = "Email:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtEmail = new TextBox { Width = 250 };

            var btnAdauga = new Button
            {
                Text = "Adaugă Client",
                Width = 180,
                Height = 35,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            btnAdauga.Click += BtnAdaugaClient_Click;

            lblMesajEroare = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };

            Label lblCauta = new Label { Text = "Caută client (Nume Prenume):", Anchor = AnchorStyles.Left, AutoSize = true };
            TextBox txtCauta = new TextBox { Width = 250 };
            Button btnCauta = new Button
            {
                Text = "Caută",
                Width = 100,
                Height = 30,
                BackColor = Color.MediumSeaGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            Label lblRezultatCautare = new Label
            {
                AutoSize = true,
                ForeColor = Color.DarkBlue,
                Anchor = AnchorStyles.Left
            };

            btnCauta.Click += (s, e) =>
            {
                string[] numePrenume = txtCauta.Text.Trim().Split(' ');
                if (numePrenume.Length != 2)
                {
                    lblRezultatCautare.Text = "Introduceți nume și prenume separate prin spațiu.";
                    lblRezultatCautare.ForeColor = Color.Red;
                    return;
                }

                var adminClienti = new AdministrareClienti();
                var clientGasit = adminClienti.CautaClient(numePrenume[0], numePrenume[1]);

                if (clientGasit != null)
                {
                    lblRezultatCautare.Text = $"Client găsit: {clientGasit.Nume} {clientGasit.Prenume}, {clientGasit.Telefon}, {clientGasit.Email}";
                    lblRezultatCautare.ForeColor = Color.Green;
                }
                else
                {
                    lblRezultatCautare.Text = "Clientul nu a fost găsit.";
                    lblRezultatCautare.ForeColor = Color.Red;
                }
            };

            // Adaugare controale în formular
            panelFormular.Controls.Add(lblNume, 0, 0);
            panelFormular.Controls.Add(txtNume, 1, 0);
            panelFormular.Controls.Add(lblPrenume, 0, 1);
            panelFormular.Controls.Add(txtPrenume, 1, 1);
            panelFormular.Controls.Add(lblTelefon, 0, 2);
            panelFormular.Controls.Add(txtTelefon, 1, 2);
            panelFormular.Controls.Add(lblEmail, 0, 3);
            panelFormular.Controls.Add(txtEmail, 1, 3);
            panelFormular.Controls.Add(btnAdauga, 1, 4);
            panelFormular.Controls.Add(lblMesajEroare, 1, 5);
            panelFormular.Controls.Add(lblCauta, 0, 6);
            panelFormular.Controls.Add(txtCauta, 1, 6);
            panelFormular.Controls.Add(btnCauta, 1, 7);
            panelFormular.Controls.Add(lblRezultatCautare, 1, 8);

            zonaMijloc.Controls.Add(panelFormular, 0, 0);

            // Tabel camere în partea dreaptă
            var panelTabel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            dgvCamere = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10),
                ColumnHeadersHeight = 30,
                RowTemplate = { Height = 26 }
            };

            dgvCamere.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            panelTabel.Controls.Add(dgvCamere);
            zonaMijloc.Controls.Add(panelTabel, 1, 0);

            // Statistici
            var panelStatistici = new Panel
            {
                BackColor = Color.LightSteelBlue,
                Dock = DockStyle.Fill,
                Height = 40
            };

            lblStatistici = new Label
            {
                Text = "Statistici camere...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelStatistici.Controls.Add(lblStatistici);
            panelPrincipal.Controls.Add(panelStatistici);
        }


        private void BtnAdaugaClient_Click(object sender, EventArgs e)
        {
            ResetEtichete();

            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string email = txtEmail.Text.Trim();

            bool valid = true;

            if (nume.Length < LUNGIME_MINIMA_NUME)
            {
                lblNume.ForeColor = Color.Red;
                valid = false;
            }

            if (prenume.Length < LUNGIME_MINIMA_NUME)
            {
                lblPrenume.ForeColor = Color.Red;
                valid = false;
            }

            if (telefon.Length < LUNGIME_MINIMA_TELEFON || !telefon.All(char.IsDigit))
            {
                lblTelefon.ForeColor = Color.Red;
                valid = false;
            }

            if (!email.Contains("@") || email.Length < 5)
            {
                lblEmail.ForeColor = Color.Red;
                valid = false;
            }

            if (!valid)
            {
                lblMesajEroare.Text = "Datele introduse nu sunt valide!";
                return;
            }

            var clientNou = new Client(nume, prenume, telefon, email);
            var adminClienti = new AdministrareClienti();
            adminClienti.AdaugaClient(clientNou);

            lblMesajEroare.ForeColor = Color.Green;
            lblMesajEroare.Text = "Client adăugat cu succes!";
            ClearForm();
        }

        private void ResetEtichete()
        {
            lblNume.ForeColor = lblPrenume.ForeColor = lblTelefon.ForeColor = lblEmail.ForeColor = Color.Black;
            lblMesajEroare.Text = "";
        }

        private void ClearForm()
        {
            txtNume.Text = txtPrenume.Text = txtTelefon.Text = txtEmail.Text = "";
        }

        private void IncarcaCamere()
        {
            try
            {
                string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
                string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string caleCompletaFisier = Path.Combine(locatieFisierSolutie, numeFisier);

                AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);
                List<Camera> camere = adminCamere.AfisareCamere();

                dgvCamere.Columns.Clear();
                dgvCamere.Columns.Add("Numar", "Număr");
                dgvCamere.Columns.Add("Optiuni", "Opțiuni");
                dgvCamere.Columns.Add("Tip", "Tip");
                dgvCamere.Columns.Add("EsteOcupata", "Ocupată");

                dgvCamere.Columns["Numar"].Width = 80;
                dgvCamere.Columns["Optiuni"].Width = 250;
                dgvCamere.Columns["Tip"].Width = 150;
                dgvCamere.Columns["EsteOcupata"].Width = 100;

                foreach (var camera in camere)
                {
                    dgvCamere.Rows.Add(
                        camera.Numar,
                        camera.Optiuni.ToString(),
                        camera.Tip.ToString(),
                        camera.EsteOcupata ? "DA" : "NU"
                    );
                }

                dgvCamere.Columns["EsteOcupata"].DefaultCellStyle.ForeColor = Color.Red;
                dgvCamere.Columns["EsteOcupata"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                int total = camere.Count;
                int ocupate = camere.Count(c => c.EsteOcupata);
                int libere = total - ocupate;

                lblStatistici.Text = $"Total camere: {total} | Ocupate: {ocupate} | Libere: {libere}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea camerelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
