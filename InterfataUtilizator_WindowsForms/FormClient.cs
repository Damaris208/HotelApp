using LibrarieModele;
using NivelModele;
using NivelStocareDate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InterfataUtilizator_WindowsForms
{
    public partial class FormClient : Form
    {
        private const int LUNGIME_MINIMA_NUME = 2;
        private const int LUNGIME_MINIMA_TELEFON = 10;

        private DataGridView dgvCamere;
        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail;
        private Label lblNume, lblPrenume, lblTelefon, lblEmail, lblMesajEroare;
        private Panel pnlFormular;
        private string caleFisierCamere;
        private string caleFisierClienti;
        private Button btnInapoi;
        private Label lblAntet;
        private Panel pnlAntet;

        public FormClient()
        {
            InitializeComponent();
            ConfigureazaFormular();

            string numeFisierClienti = ConfigurationManager.AppSettings["NumeFisierClienti"];
            caleFisierClienti = Path.Combine(Application.StartupPath, numeFisierClienti);

            this.FormClosing += FormClient_FormClosing;

            AdaugaControale();
        }

        private void InitializeComponent()
        {
            this.lblAntet = new System.Windows.Forms.Label();
            this.btnInapoi = new System.Windows.Forms.Button();
            this.pnlAntet = new System.Windows.Forms.Panel();
            this.pnlAntet.SuspendLayout();
            this.SuspendLayout();

            this.lblAntet.Font = new System.Drawing.Font("Modern No. 20", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntet.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lblAntet.Location = new System.Drawing.Point(206, 18);
            this.lblAntet.Name = "lblAntet";
            this.lblAntet.Size = new System.Drawing.Size(480, 54);
            this.lblAntet.TabIndex = 0;
            this.lblAntet.Text = "Rezervare Camere";

            this.btnInapoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInapoi.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.btnInapoi.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnInapoi.Location = new System.Drawing.Point(20, 20);
            this.btnInapoi.Name = "btnInapoi";
            this.btnInapoi.Size = new System.Drawing.Size(120, 30);
            this.btnInapoi.TabIndex = 1;
            this.btnInapoi.Text = "Inapoi la meniu";
            this.btnInapoi.UseVisualStyleBackColor = false;
            this.btnInapoi.Click += new System.EventHandler(this.btnInapoi_Click);

            this.pnlAntet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.pnlAntet.Controls.Add(this.lblAntet);
            this.pnlAntet.Location = new System.Drawing.Point(-37, 2);
            this.pnlAntet.Name = "pnlAntet";
            this.pnlAntet.Size = new System.Drawing.Size(1371, 64);
            this.pnlAntet.TabIndex = 2;

            this.ClientSize = new System.Drawing.Size(990, 656);
            this.Controls.Add(this.btnInapoi);
            this.Controls.Add(this.pnlAntet);
            this.Name = "FormClient";
            this.pnlAntet.ResumeLayout(false);
            this.pnlAntet.PerformLayout();
            this.ResumeLayout(false);
        }

        private void ConfigureazaFormular()
        {
            this.Text = "Gestionare Clienți";
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 249);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Font = new Font("Segoe UI", 9);
        }

        private Button CreeazaButon(string text, Color backColor, EventHandler handler, int width = 120, int height = 30)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = height,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Left
            };
            btn.FlatAppearance.BorderSize = 0;
            if (handler != null)
                btn.Click += handler;
            return btn;
        }

        private void AdaugaControale()
        {
            var panelPrincipal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.White,
                Padding = new Padding(0, 0, 0, 5)
            };

            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            this.Controls.Add(panelPrincipal);

            pnlAntet = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(52, 152, 219)
            };

            lblAntet = new Label
            {
                Text = "Gestionare Clienți",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            btnInapoi = CreeazaButon("Înapoi", Color.FromArgb(41, 128, 185), (s, e) => this.Close());
            btnInapoi.Location = new Point(pnlAntet.Width - btnInapoi.Width - 20, 20);

            pnlAntet.Controls.Add(lblAntet);
            pnlAntet.Controls.Add(btnInapoi);
            panelPrincipal.Controls.Add(pnlAntet, 0, 0);

            var zonaMijloc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            pnlFormular = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var panelFormularContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 2,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            lblNume = new Label { Text = "Nume:", AutoSize = true };
            txtNume = new TextBox { Width = 200 };

            lblPrenume = new Label { Text = "Prenume:", AutoSize = true };
            txtPrenume = new TextBox { Width = 200 };

            lblTelefon = new Label { Text = "Telefon:", AutoSize = true };
            txtTelefon = new TextBox { Width = 200 };

            lblEmail = new Label { Text = "Email:", AutoSize = true };
            txtEmail = new TextBox { Width = 200 };

            var btnSalveaza = CreeazaButon("Salvează", Color.FromArgb(52, 152, 219), BtnSalveaza_Click, 150, 30);

            lblMesajEroare = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 8)
            };

            panelFormularContent.Controls.Add(lblNume, 0, 0);
            panelFormularContent.Controls.Add(txtNume, 1, 0);
            panelFormularContent.Controls.Add(lblPrenume, 0, 1);
            panelFormularContent.Controls.Add(txtPrenume, 1, 1);
            panelFormularContent.Controls.Add(lblTelefon, 0, 2);
            panelFormularContent.Controls.Add(txtTelefon, 1, 2);
            panelFormularContent.Controls.Add(lblEmail, 0, 3);
            panelFormularContent.Controls.Add(txtEmail, 1, 3);
            panelFormularContent.Controls.Add(btnSalveaza, 1, 4);
            panelFormularContent.Controls.Add(lblMesajEroare, 0, 5);
            panelFormularContent.SetColumnSpan(lblMesajEroare, 2);

            pnlFormular.Controls.Add(panelFormularContent);
            zonaMijloc.Controls.Add(pnlFormular, 0, 0);

            var dgvCamere = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackColor = Color.White,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(64, 0, 64),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(220, 220, 220), // Gri deschis
                    SelectionForeColor = Color.Black // Text negru pentru contrast mai bun
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240)
                }
            };

            dgvCamere.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Numar", HeaderText = "Număr", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "Tip", HeaderText = "Tip", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Optiuni", HeaderText = "Facilități", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 }
            });

            zonaMijloc.Controls.Add(dgvCamere, 1, 0);
            panelPrincipal.Controls.Add(zonaMijloc, 0, 1);

            try
            {
                string numeFisierCamere = ConfigurationManager.AppSettings["NumeFisierCamere"];
                if (string.IsNullOrEmpty(numeFisierCamere))
                {
                    throw new ConfigurationErrorsException("Numele fișierului camerelor nu este setat în configurație!");
                }

                caleFisierCamere = Path.Combine(Application.StartupPath, numeFisierCamere);
                if (!File.Exists(caleFisierCamere))
                {
                    throw new FileNotFoundException($"Fișierul camerelor nu există la calea: {caleFisierCamere}");
                }

                var adminCamere = new AdministrareCamere(caleFisierCamere);
                var camere = adminCamere.AfisareCamere();

                dgvCamere.Rows.Clear();
                foreach (var camera in camere)
                {
                    if (camera != null)
                    {
                        dgvCamere.Rows.Add(
                            camera.Numar,
                            camera.Tip.ToString(),
                            camera.Optiuni.ToString(),
                            camera.EsteOcupata ? "Ocupată" : "Liberă"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea camerelor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalveaza_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNume.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenume.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefon.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vă rugăm să completați toate câmpurile!",
                        "Informații incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var adminClienti = new AdministrareClienti(caleFisierClienti);
                var client = new Client(txtNume.Text, txtPrenume.Text, txtTelefon.Text, txtEmail.Text);
                adminClienti.AdaugaClient(client);

                MessageBox.Show("Client adăugat cu succes!",
                    "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var clienti = adminClienti.AfisareClienti();
                ((DataGridView)Controls.Find("dgvClienti", true)[0]).DataSource = null;
                ((DataGridView)Controls.Find("dgvClienti", true)[0]).DataSource = clienti;

                txtNume.Text = "";
                txtPrenume.Text = "";
                txtTelefon.Text = "";
                txtEmail.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea clientului: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInapoi_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formMenu = new Form1();
            formMenu.FormClosed += (s, args) => this.Close();
            formMenu.Show();
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}