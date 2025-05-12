using System;
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

        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail, txtNumarCamera;
        private Label lblNume, lblPrenume, lblTelefon, lblEmail, lblMesajEroare, lblNumarCamera, lblMesajCamera;
        private CheckBox cbOcupata, cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon;
        private RadioButton rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe;

        public Form1()
        {
            InitializeComponent();
            ConfigureazaFormular();
            AdaugaControale();
            IncarcaCamere();
        }

        private void ConfigureazaFormular()
        {
            this.Text = "Hotel Management System";
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 249);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Font = new Font("Segoe UI", 9);
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
            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            this.Controls.Add(panelPrincipal);

            var panelAntet = new Panel
            {
                BackColor = Color.FromArgb(44, 62, 80),
                Dock = DockStyle.Fill
            };

            var lblTitlu = new Label
            {
                Text = "HOTEL MANAGEMENT",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelAntet.Controls.Add(lblTitlu);
            panelPrincipal.Controls.Add(panelAntet);

            var zonaMijloc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10, 10, 10, 0)
            };
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));

            panelPrincipal.Controls.Add(zonaMijloc);

            var panelFormular = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var panelFormularContent = new TableLayoutPanel
            {

                Text = "GESTIONARE CLIENTI",
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 20,
                AutoSize = true
            };

            var separatorClient = new Label
            {
                Text = "GESTIONARE CLIENTI",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 5)
            };

            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            for (int i = 0; i < 20; i++)
                panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 4 || i == 14 ? 45 : 35));

            lblNume = new Label { Text = "Nume:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            txtNume = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 6) };
            lblPrenume = new Label { Text = "Prenume:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            txtPrenume = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 6) };
            lblTelefon = new Label { Text = "Telefon:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            txtTelefon = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 6) };
            lblEmail = new Label { Text = "Email:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            txtEmail = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 6) };

            var btnAdaugaClient = new Button
            {
                Text = "Adaugă Client",
                Width = 180,
                Height = 25,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 6, FontStyle.Bold),
                Anchor = AnchorStyles.Left,
                Cursor = Cursors.Hand
            };
            btnAdaugaClient.FlatAppearance.BorderSize = 0;
            btnAdaugaClient.Click += BtnAdaugaClient_Click;

            lblMesajEroare = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 6)
            };

            var separatorCamera = new Label
            {
                Text = "GESTIONARE CAMERE",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 5)
            };

            lblNumarCamera = new Label { Text = "Număr cameră:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            txtNumarCamera = new TextBox { Width = 100, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 6) };

            var lblTipCamera = new Label { Text = "Tip cameră:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            var panelTipCamera = new FlowLayoutPanel { AutoSize = true, Anchor = AnchorStyles.Left, FlowDirection = FlowDirection.LeftToRight };
            rbSingle = new RadioButton { Text = "Single", Checked = true, AutoSize = true, Font = new Font("Segoe UI", 6) };
            rbDouble = new RadioButton { Text = "Double", AutoSize = true, Font = new Font("Segoe UI", 6) };
            rbQuad = new RadioButton { Text = "Quad", AutoSize = true, Font = new Font("Segoe UI", 6) };
            rbSuite = new RadioButton { Text = "Suite", AutoSize = true, Font = new Font("Segoe UI", 6) };
            rbDeluxe = new RadioButton { Text = "Deluxe", AutoSize = true, Font = new Font("Segoe UI", 6) };
            panelTipCamera.Controls.AddRange(new Control[] { rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe });

            var lblOptiuniCamera = new Label { Text = "Opțiuni:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            var panelOptiuniCamera = new FlowLayoutPanel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            cbWiFi = new CheckBox { Text = "WiFi", AutoSize = true, Font = new Font("Segoe UI", 6) };
            cbTV = new CheckBox { Text = "TV", AutoSize = true, Font = new Font("Segoe UI", 6) };
            cbAC = new CheckBox { Text = "Aer cond.", AutoSize = true, Font = new Font("Segoe UI", 6) };
            cbFrigider = new CheckBox { Text = "Frigider", AutoSize = true, Font = new Font("Segoe UI", 6) };
            cbBalcon = new CheckBox { Text = "Balcon", AutoSize = true, Font = new Font("Segoe UI", 6) };

            panelOptiuniCamera.Controls.AddRange(new Control[] { cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon });

            var lblOcupareCamera = new Label { Text = "Ocupată:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 6, FontStyle.Bold) };
            var panelOcupare = new FlowLayoutPanel { AutoSize = true, Anchor = AnchorStyles.Left };
            cbOcupata = new CheckBox { AutoSize = true, Font = new Font("Segoe UI", 6) };
            var lblDa = new Label { Text = "Da", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 6, 0, 0), Font = new Font("Segoe UI", 6) };
            panelOcupare.Controls.Add(cbOcupata);
            panelOcupare.Controls.Add(lblDa);

            var panelButoaneCamera = new FlowLayoutPanel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            var btnAdaugaCamera = new Button
            {
                Text = "Adaugă cameră",
                Width = 120,
                Height = 20,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 6, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdaugaCamera.FlatAppearance.BorderSize = 0;
            var btnRefresh = new Button
            {
                Text = "Refresh",
                Width = 80,
                Height = 20,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 6, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            panelButoaneCamera.Controls.AddRange(new Control[] { btnAdaugaCamera, btnRefresh });


            var panelCautare = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Dock = DockStyle.Top,
                WrapContents = false,
                Padding = new Padding(0, 10, 0, 10)
            };

            Label lblCriteriuCautare = new Label
            {
                Text = "Căutare după ",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            ComboBox cmbCriteriuCautare = new ComboBox
            {
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbCriteriuCautare.Items.AddRange(new string[] { "Nume și Prenume", "Număr Telefon", "Adresă Email" });
            cmbCriteriuCautare.SelectedIndex = 0;

            TextBox txtCauta = new TextBox
            {
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9)
            };

            Button btnCauta = new Button
            {
                Text = "Caută",
                Width = 80,
                Height = 28,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCauta.FlatAppearance.BorderSize = 0;

            Label lblRezultatCautare = new Label
            {
                AutoSize = true,
                ForeColor = Color.DarkBlue,
                Font = new Font("Segoe UI", 9)
            };

            panelCautare.Controls.Add(lblCriteriuCautare);
            panelCautare.Controls.Add(cmbCriteriuCautare);
            panelCautare.Controls.Add(txtCauta);
            panelCautare.Controls.Add(btnCauta);
            panelFormularContent.Controls.Add(panelCautare, 0, 14);
            panelFormularContent.SetColumnSpan(panelCautare, 2);
            panelFormularContent.Controls.Add(lblRezultatCautare, 0, 15);
            panelFormularContent.SetColumnSpan(lblRezultatCautare, 2);



            lblMesajCamera = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 6)
            };



            Label lblCauta = new Label
            {
                Text = "Introduceți numele și prenumele:",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Font = new Font("Segoe UI", 6, FontStyle.Bold)
            };


            cmbCriteriuCautare.SelectedIndexChanged += (s, e) =>
            {
                switch (cmbCriteriuCautare.SelectedItem.ToString())
                {
                    case "Nume și Prenume":
                        lblCauta.Text = "Introduceți numele și prenumele:";
                        break;
                    case "Număr Telefon":
                        lblCauta.Text = "Introduceți numărul de telefon:";                  
                        break;
                    case "Adresă Email":
                        lblCauta.Text = "Introduceți adresa de email:";
                        break;
                }
            };

            btnCauta.Click += (s, e) =>
            {
                string criteriu = cmbCriteriuCautare.SelectedItem.ToString();
                string valoare = txtCauta.Text.Trim();
                var adminClienti = new AdministrareClienti();
                Client clientGasit = null;

                if (string.IsNullOrWhiteSpace(valoare))
                {
                    lblRezultatCautare.ForeColor = Color.Red;
                    lblRezultatCautare.Text = "Introduceți o valoare pentru căutare!";
                    return;
                }

                switch (criteriu)
                {
                    case "Nume și Prenume":
                        var split = valoare.Split(' ');
                        if (split.Length != 2)
                        {
                            lblRezultatCautare.Text = "Introduceți Nume și Prenume separate prin spațiu!";
                            lblRezultatCautare.ForeColor = Color.Red;
                            return;
                        }
                        clientGasit = adminClienti.CautaClient(split[0], split[1]);
                        break;

                    case "Număr Telefon":
                        clientGasit = adminClienti.CautaClientDupaTelefon(valoare);
                        break;

                    case "Adresă Email":
                        clientGasit = adminClienti.CautaClientDupaEmail(valoare);
                        break;
                }

                if (clientGasit != null)
                {
                    lblRezultatCautare.ForeColor = Color.Green;
                    lblRezultatCautare.Text = $"Client găsit: {clientGasit.Nume} {clientGasit.Prenume} " +
                                              $"Telefon: {clientGasit.Telefon} Email: {clientGasit.Email}";
                }
                else
                {
                    lblRezultatCautare.ForeColor = Color.Red;
                    lblRezultatCautare.Text = "Niciun client găsit.";
                }
            };

            btnAdaugaCamera.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtNumarCamera.Text))
                {
                    lblMesajCamera.Text = "Introduceți numărul camerei!";
                    return;
                }

                TipCamera tip = rbSingle.Checked ? TipCamera.Single :
                               rbDouble.Checked ? TipCamera.Double :
                               rbQuad.Checked ? TipCamera.Quad :
                               rbSuite.Checked ? TipCamera.Suite :
                               TipCamera.Deluxe;

                OptiuniCamera optiuni = OptiuniCamera.Niciuna;
                if (cbWiFi.Checked) optiuni |= OptiuniCamera.WiFi;
                if (cbTV.Checked) optiuni |= OptiuniCamera.TV;
                if (cbAC.Checked) optiuni |= OptiuniCamera.AerConditionat;
                if (cbFrigider.Checked) optiuni |= OptiuniCamera.Frigider;
                if (cbBalcon.Checked) optiuni |= OptiuniCamera.Balcon;

                try
                {
                    string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
                    string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                    string caleCompletaFisier = Path.Combine(locatieFisierSolutie, numeFisier);

                    var cameraNoua = new Camera(
                        int.Parse(txtNumarCamera.Text),
                        tip,
                        optiuni,
                        cbOcupata.Checked
                    );

                    AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);
                    adminCamere.AdaugaCamera(cameraNoua);

                    lblMesajCamera.ForeColor = Color.Green;
                    lblMesajCamera.Text = "Camera adăugată cu succes!";
                    txtNumarCamera.Text = "";
                    cbOcupata.Checked = false;
                    cbWiFi.Checked = cbTV.Checked = cbAC.Checked = cbFrigider.Checked = cbBalcon.Checked = false;
                }
                catch (Exception ex)
                {
                    lblMesajCamera.Text = $"Eroare: {ex.Message}";
                }
            };

            btnRefresh.Click += (s, e) => IncarcaCamere();

            panelFormularContent.Controls.Add(separatorClient, 0, 0);
            panelFormularContent.SetColumnSpan(separatorClient, 2);
            panelFormularContent.Controls.Add(lblNume, 0, 1);
            panelFormularContent.Controls.Add(txtNume, 1, 1);
            panelFormularContent.Controls.Add(lblPrenume, 0, 2);
            panelFormularContent.Controls.Add(txtPrenume, 1, 2);
            panelFormularContent.Controls.Add(lblTelefon, 0, 3);
            panelFormularContent.Controls.Add(txtTelefon, 1, 3);
            panelFormularContent.Controls.Add(lblEmail, 0, 4);
            panelFormularContent.Controls.Add(txtEmail, 1, 4);
            panelFormularContent.Controls.Add(btnAdaugaClient, 1, 5);
            panelFormularContent.Controls.Add(lblMesajEroare, 1, 6);
            panelFormularContent.Controls.Add(separatorCamera, 0, 7);
            panelFormularContent.SetColumnSpan(separatorCamera, 2);
            panelFormularContent.Controls.Add(lblNumarCamera, 0, 8);
            panelFormularContent.Controls.Add(txtNumarCamera, 1, 8);
            panelFormularContent.Controls.Add(lblTipCamera, 0, 9);
            panelFormularContent.Controls.Add(panelTipCamera, 1, 9);
            panelFormularContent.Controls.Add(lblOptiuniCamera, 0, 10);
            panelFormularContent.Controls.Add(panelOptiuniCamera, 1, 10);
            panelFormularContent.Controls.Add(lblOcupareCamera, 0, 11);
            panelFormularContent.Controls.Add(panelOcupare, 1, 11);
            panelFormularContent.Controls.Add(panelButoaneCamera, 1, 12);
            panelFormularContent.Controls.Add(lblMesajCamera, 0, 13);
            panelFormularContent.SetColumnSpan(lblMesajCamera, 2);
            panelFormularContent.Controls.Add(lblRezultatCautare, 0, 16);
            panelFormularContent.SetColumnSpan(lblRezultatCautare, 2);
            panelFormular.Controls.Add(panelFormularContent);
            zonaMijloc.Controls.Add(panelFormular, 0, 0);

            var panelTabel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 15, 15, 15),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            dgvCamere = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                ColumnHeadersHeight = 30,
                RowTemplate = { Height = 28 },
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                GridColor = Color.FromArgb(230, 230, 230)
            };

            dgvCamere.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCamere.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvCamere.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCamere.EnableHeadersVisualStyles = false;
            dgvCamere.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvCamere.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvCamere.DefaultCellStyle.SelectionForeColor = Color.Black;

            panelTabel.Controls.Add(dgvCamere);
            zonaMijloc.Controls.Add(panelTabel, 1, 0);

            var panelStatistici = new Panel
            {
                BackColor = Color.FromArgb(44, 62, 80),
                Dock = DockStyle.Fill,
                Height = 50
            };

            lblStatistici = new Label
            {
                Text = "Statistici camere...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
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
                dgvCamere.Columns["Tip"].Width = 80;
                dgvCamere.Columns["EsteOcupata"].Width = 80;

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
                dgvCamere.Columns["EsteOcupata"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                dgvCamere.RowsDefaultCellStyle.BackColor = Color.White;

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