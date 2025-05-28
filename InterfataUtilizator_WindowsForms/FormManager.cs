using LibrarieModele;
using NivelModele;
using NivelStocareDate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InterfataUtilizator_WindowsForms
{
    public partial class FormManager : Form
    {
        private const int LUNGIME_MINIMA_NUME = 2;
        private const int LUNGIME_MINIMA_TELEFON = 10;

        private DataGridView dgvCamere;
        private DataGridView dgvClienti;
        private Label lblStatistici;
        private TextBox txtCauta;
        private Label lblRezultatCautare;
        private FlowLayoutPanel panelButoaneCamera;

        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail, txtNumarCamera;
        private Label lblNume, lblPrenume, lblTelefon, lblEmail, lblMesajEroare, lblNumarCamera, lblMesajCamera;
        private CheckBox cbOcupata, cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon;
        private RadioButton rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe;

        private void btnInapoi_Click(object sender, EventArgs e)
        {
            // Ascundem formularul curent
            this.Hide();

            // Deschidem formularul meniului principal
            var formMenu = new Form1();
            formMenu.FormClosed += (s, args) => this.Close();
            formMenu.Show();
        }

        private Label lblAntet;
        private Button btnInapoi;
        private Panel pnlAntet;
        private Camera cameraSelectata = null;
        private Client clientSelectat = null;
        private Button btnClienti, btnCamere;
        private Panel pnlFormular;
        private bool showClientForm = true;
        private string caleFisierClienti = null;
        private string numeVechiClient = null;
        private string prenumeVechiClient = null;

        public FormManager()
        {
            InitializeComponent();
            ConfigureazaFormular();

            this.FormClosing += FormManager_FormClosing;

            string numeFisierClienti = ConfigurationManager.AppSettings["NumeFisierClienti"];
            string numeFisierCamere = ConfigurationManager.AppSettings["NumeFisierCamere"];

            caleFisierClienti = Path.Combine(Application.StartupPath, numeFisierClienti);
            string caleFisierCamere = Path.Combine(Application.StartupPath, numeFisierCamere);

            if (!File.Exists(caleFisierClienti))
                File.WriteAllText(caleFisierClienti, "");
            if (!File.Exists(caleFisierCamere))
                File.WriteAllText(caleFisierCamere, "");

            AdaugaControale();
            showClientForm = true;

            // Setăm starea inițială a butoanelor
            if (btnClienti != null)
                btnClienti.BackColor = Color.FromArgb(128, 0, 128); // Mai închis pentru butonul activ
            if (btnCamere != null)
                btnCamere.BackColor = Color.FromArgb(64, 0, 64);

            // Setăm starea inițială a grid-urilor
            if (dgvClienti != null)
            {
                dgvClienti.Visible = true;
                dgvClienti.BringToFront();
                ConfigureazaDataGridViewClienti();
                IncarcaClienti();
            }
            if (dgvCamere != null)
            {
                dgvCamere.Visible = false;
                ConfigureazaDataGridViewCamere();
            }

            IncarcaFormClienti();
            pnlFormular.Visible = true;

            // Actualizăm statisticile
            if (lblStatistici != null)
            {
                try
                {
                    var adminClienti = new AdministrareClienti(caleFisierClienti);
                    var clienti = adminClienti.AfisareClienti();
                    lblStatistici.Text = $"Total clienți: {clienti.Count}";
                }
                catch (Exception ex)
                {
                    lblStatistici.Text = $"Eroare la încărcarea statisticilor: {ex.Message}";
                }
            }
        }

        private void FormManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void InitializeComponent()
        {
            this.lblAntet = new System.Windows.Forms.Label();
            this.btnInapoi = new System.Windows.Forms.Button();
            this.pnlAntet = new System.Windows.Forms.Panel();
            this.pnlAntet.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAntet
            // 
            this.lblAntet.AutoSize = true;
            this.lblAntet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblAntet.Font = new System.Drawing.Font("Modern No. 20", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntet.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lblAntet.Location = new System.Drawing.Point(206, 18);
            this.lblAntet.Name = "lblAntet";
            this.lblAntet.Size = new System.Drawing.Size(480, 54);
            this.lblAntet.TabIndex = 0;
            this.lblAntet.Text = "Bun venit, Manager!";
            // 
            // btnInapoi
            // 
            this.btnInapoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInapoi.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInapoi.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnInapoi.Location = new System.Drawing.Point(20, 20);
            this.btnInapoi.Name = "btnInapoi";
            this.btnInapoi.Size = new System.Drawing.Size(120, 30);
            this.btnInapoi.TabIndex = 1;
            this.btnInapoi.Text = "Inapoi la meniu";
            this.btnInapoi.UseVisualStyleBackColor = false;
            this.btnInapoi.Click += new System.EventHandler(this.btnInapoi_Click);
            // 
            // pnlAntet
            // 
            this.pnlAntet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.pnlAntet.Controls.Add(this.lblAntet);
            this.pnlAntet.Location = new System.Drawing.Point(-37, 2);
            this.pnlAntet.Name = "pnlAntet";
            this.pnlAntet.Size = new System.Drawing.Size(1371, 64);
            this.pnlAntet.TabIndex = 2;
            // 
            // FormManager
            // 
            this.ClientSize = new System.Drawing.Size(990, 656);
            this.Controls.Add(this.btnInapoi);
            this.Controls.Add(this.pnlAntet);
            this.Name = "FormManager";
            this.pnlAntet.ResumeLayout(false);
            this.pnlAntet.PerformLayout();
            this.ResumeLayout(false);
        }

        private void ConfigureazaFormular()
        {
            this.Text = "Hotel Management";
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

        private Label CreeazaLabel(string text, bool isBold = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, isBold ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        private TextBox CreeazaTextBox(int width = 200)
        {
            return new TextBox
            {
                Width = width,
                Font = new Font("Segoe UI", 9)
            };
        }

        private void ConfigureazaDataGridView(DataGridView dgv, bool isClientGrid)
        {
            dgv.Columns.Clear();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 0, 64);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            if (isClientGrid)
            {
                dgv.Columns.Add("Nume", "Nume");
                dgv.Columns.Add("Prenume", "Prenume");
                dgv.Columns.Add("Telefon", "Telefon");
                dgv.Columns.Add("Email", "Email");
            }
            else
            {
                dgv.Columns.Add("Numar", "Număr");
                dgv.Columns.Add("Tip", "Tip");
                dgv.Columns.Add("Optiuni", "Facilități");
                dgv.Columns.Add("Status", "Status");
            }
        }

        private void AdaugaControale()
        {
            var panelPrincipal = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                BackColor = Color.White,
                Padding = new Padding(0, 0, 0, 5)
            };

            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            panelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
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
            panelPrincipal.Controls.Add(panelAntet, 0, 0);

            var panelControlBar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 240, 240),
                ColumnCount = 2,
                RowCount = 1
            };
            panelControlBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panelControlBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            var panelToggleButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(240, 240, 240),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            btnClienti = new Button
            {
                Text = "Gestionare Clienți",
                Width = 150,
                Height = 30,
                BackColor = Color.FromArgb(64, 0, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Tag = "clienti",
                Margin = new Padding(5)
            };
            btnClienti.Click += ToggleFormView;

            btnCamere = new Button
            {
                Text = "Gestionare Camere",
                Width = 150,
                Height = 30,
                BackColor = Color.FromArgb(64, 0, 64),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Tag = "camere",
                Margin = new Padding(5)
            };
            btnCamere.Click += ToggleFormView;

            panelToggleButtons.Controls.Add(btnClienti);
            panelToggleButtons.Controls.Add(btnCamere);

            // Panel Căutare
            var panelCautare = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(240, 240, 240),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5)
            };

            var lblCautare = new Label
            {
                Text = "Căutare:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 0, 64),
                Margin = new Padding(5, 5, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtCauta = new TextBox
            {
                Width = 200,
                Height = 28,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(5)
            };

            var lblAjutorCautare = new Label
            {
                Text = "Introduceți nume, prenume, telefon sau email...",
                AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Margin = new Padding(5, 0, 0, 0),
                Visible = true
            };

            txtCauta.Enter += (s, e) => lblAjutorCautare.Visible = false;
            txtCauta.Leave += (s, e) => lblAjutorCautare.Visible = string.IsNullOrEmpty(txtCauta.Text);

            var btnCauta = CreeazaButon("Caută", Color.FromArgb(46, 204, 113), (s, e) => ExecutaCautare(), 80, 28);
            btnCauta.Margin = new Padding(5);

            var btnResetare = CreeazaButon("Resetare", Color.FromArgb(231, 76, 60), (s, e) =>
            {
                txtCauta.Text = "";
                if (showClientForm)
                    IncarcaClienti();
                else
                    IncarcaCamere();
                txtCauta.Focus();
            }, 80, 28);
            btnResetare.Margin = new Padding(5);

            txtCauta.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    ExecutaCautare();
                }
            };

            panelCautare.Controls.Add(lblCautare);
            panelCautare.Controls.Add(txtCauta);
            panelCautare.Controls.Add(lblAjutorCautare);
            panelCautare.Controls.Add(btnCauta);
            panelCautare.Controls.Add(btnResetare);

            panelControlBar.Controls.Add(panelToggleButtons, 0, 0);
            panelControlBar.Controls.Add(panelCautare, 1, 0);
            panelPrincipal.Controls.Add(panelControlBar, 0, 1);

            var zonaMijloc = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10, 10, 10, 0)
            };
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            zonaMijloc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            pnlFormular = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Visible = true
            };

            var panelTabel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            dgvClienti = new DataGridView
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
                GridColor = Color.FromArgb(230, 230, 230),
                Visible = showClientForm
            };
            ConfigureazaDataGridViewClienti();

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
                GridColor = Color.FromArgb(230, 230, 230),
                Visible = !showClientForm
            };
            ConfigureazaDataGridViewCamere();

            dgvClienti.SelectionChanged += DgvClienti_SelectionChanged;
            dgvCamere.SelectionChanged += DgvCamere_SelectionChanged;

            panelTabel.Controls.Add(dgvClienti);
            panelTabel.Controls.Add(dgvCamere);

            zonaMijloc.Controls.Add(pnlFormular, 0, 0);
            zonaMijloc.Controls.Add(panelTabel, 1, 0);
            panelPrincipal.Controls.Add(zonaMijloc, 0, 2);

            var panelStatistici = new Panel
            {
                BackColor = Color.FromArgb(64, 0, 64),
                Dock = DockStyle.Fill
            };

            lblStatistici = new Label
            {
                Text = "Statistici...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelStatistici.Controls.Add(lblStatistici);
            panelPrincipal.Controls.Add(panelStatistici, 0, 3);

            if (showClientForm)
                IncarcaFormClienti();
            else
                IncarcaFormCamere();
        }

        private void ConfigureazaDataGridViewClienti()
        {
            ConfigureazaDataGridView(dgvClienti, true);
        }

        private void ConfigureazaDataGridViewCamere()
        {
            ConfigureazaDataGridView(dgvCamere, false);
        }

        private void ExecutaCautare()
        {
            string valoare = txtCauta.Text.Trim();

            if (string.IsNullOrWhiteSpace(valoare))
            {
                if (showClientForm)
                    IncarcaClienti();
                else
                    IncarcaCamere();
                return;
            }

            try
            {
                if (showClientForm)
                    CautaClient(valoare);
                else
                    CautaCamera(valoare);

                SelecteazaPrimulRand();
            }
            catch (Exception ex)
            {
                AfiseazaEroare("căutare", ex);
            }
        }

        private void SelecteazaPrimulRand()
        {
            if (showClientForm && dgvClienti.Rows.Count > 0)
            {
                dgvClienti.Rows[0].Selected = true;
                dgvClienti.FirstDisplayedScrollingRowIndex = 0;
            }
            else if (!showClientForm && dgvCamere.Rows.Count > 0)
            {
                dgvCamere.Rows[0].Selected = true;
                dgvCamere.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void AfiseazaEroare(string operatie, Exception ex)
        {
            MessageBox.Show($"Eroare la {operatie}: {ex.Message}", "Eroare",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CautaClient(string valoare)
        {
            var adminClienti = new AdministrareClienti(caleFisierClienti);
            var clientiGasiti = new HashSet<Client>();

            if (valoare.Contains(' '))
            {
                var split = valoare.Split(new[] { ' ' }, 2);
                var client = adminClienti.CautaClient(split[0], split[1]);
                if (client != null)
                    clientiGasiti.Add(client);
            }

            var toateClientii = adminClienti.AfisareClienti();
            foreach (var client in toateClientii)
            {
                if (client == null) continue;

                if (client.Nume?.ToLower().Contains(valoare.ToLower()) == true ||
                    client.Prenume?.ToLower().Contains(valoare.ToLower()) == true)
                {
                    clientiGasiti.Add(client);
                    continue;
                }

                if (client.Telefon?.Contains(valoare) == true ||
                    client.Email?.ToLower().Contains(valoare.ToLower()) == true)
                {
                    clientiGasiti.Add(client);
                }
            }

            ActualizeazaGridClienti(clientiGasiti);
        }

        private void ActualizeazaGridClienti(IEnumerable<Client> clienti)
        {
            dgvClienti.Rows.Clear();
            if (clienti.Any())
            {
                foreach (var client in clienti)
                {
                    dgvClienti.Rows.Add(
                        client.Nume,
                        client.Prenume,
                        client.Telefon,
                        client.Email
                    );
                }
                lblStatistici.Text = $"Rezultate căutare: {clienti.Count()} client(i) găsiți";
            }
            else
            {
                lblStatistici.Text = "Nu s-au găsit clienți pentru căutarea efectuată";
            }
        }

        private void CautaCamera(string valoare)
        {
            string caleFisier = ConfigurationManager.AppSettings["NumeFisierCamere"];
            var adminCamere = new AdministrareCamere(caleFisier);
            var camereGasite = new HashSet<Camera>();

            if (int.TryParse(valoare, out int numarCamera))
            {
                var camera = adminCamere.CautaCameraDupaNumar(numarCamera);
                if (camera != null)
                    camereGasite.Add(camera);
            }

            if (Enum.TryParse(valoare, true, out TipCamera tip))
            {
                var camere = adminCamere.AfisareCamere();
                foreach (var camera in camere.Where(c => c?.Tip == tip))
                {
                    camereGasite.Add(camera);
                }
            }

            ActualizeazaGridCamere(camereGasite);
        }

        private void ActualizeazaGridCamere(IEnumerable<Camera> camere)
        {
            dgvCamere.Rows.Clear();
            if (camere.Any())
            {
                foreach (var camera in camere)
                {
                    dgvCamere.Rows.Add(
                        camera.Numar,
                        camera.Tip.ToString(),
                        camera.Optiuni.ToString(),
                        camera.EsteOcupata ? "Ocupată" : "Liberă"
                    );
                }
                int ocupate = camere.Count(c => c.EsteOcupata);
                int libere = camere.Count() - ocupate;
                lblStatistici.Text = $"Rezultate căutare: {camere.Count()} cameră(e) găsite | Ocupate: {ocupate} | Libere: {libere}";
            }
            else
            {
                lblStatistici.Text = "Nu s-au găsit camere pentru căutarea efectuată";
            }
        }

        private void ToggleFormView(object sender, EventArgs e)
        {
            try
            {
                if (sender == null || !(sender is Button button) || button.Tag == null)
                    return;

                bool wasClientForm = showClientForm;
                showClientForm = (string)button.Tag == "clienti";

                if (wasClientForm == showClientForm)
                    return;

                txtCauta.Text = "";

                pnlFormular.Controls.Clear();
                pnlFormular.Visible = false;

                // Setăm culoarea butoanelor
                if (btnClienti != null)
                    btnClienti.BackColor = showClientForm ? Color.FromArgb(128, 0, 128) : Color.FromArgb(64, 0, 64);
                if (btnCamere != null)
                    btnCamere.BackColor = !showClientForm ? Color.FromArgb(128, 0, 128) : Color.FromArgb(64, 0, 64);

                // Setăm vizibilitatea grid-urilor
                if (dgvClienti != null)
                {
                    dgvClienti.Visible = showClientForm;
                    dgvClienti.BringToFront();
                    if (dgvClienti.SelectedRows.Count > 0)
                        dgvClienti.SelectedRows[0].Selected = false;
                }

                if (dgvCamere != null)
                {
                    dgvCamere.Visible = !showClientForm;
                    dgvCamere.BringToFront();
                    if (dgvCamere.SelectedRows.Count > 0)
                        dgvCamere.SelectedRows[0].Selected = false;
                }

                if (wasClientForm)
                {
                    if (clientSelectat != null)
                    {
                        ClearForm();
                        clientSelectat = null;
                    }
                }
                else
                {
                    if (cameraSelectata != null)
                    {
                        ResetFormularCamera();
                    }
                }

                try
                {
                    if (showClientForm)
                    {
                        IncarcaFormClienti();
                        IncarcaClienti();
                    }
                    else
                    {
                        IncarcaFormCamere();
                        IncarcaCamere();
                    }

                    pnlFormular.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la încărcarea formularului: {ex.Message}", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la schimbarea formularului: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncarcaFormClienti()
        {
            try
            {
                if (pnlFormular == null)
                {
                    MessageBox.Show("Eroare: Panoul de formular nu este inițializat", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pnlFormular.Controls.Clear();

                var panelFormularContent = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 7,
                    AutoSize = true,
                    Padding = new Padding(15)
                };

                try
                {
                    panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                    panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
                    for (int i = 0; i < 7; i++)
                        panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 5 ? 45 : 35));

                    lblNume = CreeazaLabel("Nume:", true);
                    txtNume = CreeazaTextBox(200);

                    lblPrenume = CreeazaLabel("Prenume:", true);
                    txtPrenume = CreeazaTextBox(200);

                    lblTelefon = CreeazaLabel("Telefon:", true);
                    txtTelefon = CreeazaTextBox(200);

                    lblEmail = CreeazaLabel("Email:", true);
                    txtEmail = CreeazaTextBox(200);

                    var panelButoaneClient = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Margin = new Padding(0, 8, 0, 0)
                    };

                    var btnAdaugaClient = CreeazaButon("Salvează", Color.FromArgb(52, 152, 219), BtnAdaugaClient_Click, 90, 25);
                    var btnAnuleazaClient = CreeazaButon("Anulează", Color.FromArgb(231, 76, 60), BtnAnuleazaClient_Click, 80, 25);
                    var btnStergeClient = CreeazaButon("Șterge", Color.FromArgb(192, 57, 43), BtnStergeClient_Click, 80, 25);

                    btnAdaugaClient.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnAnuleazaClient.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnStergeClient.Font = new Font("Segoe UI", 8, FontStyle.Bold);

                    btnStergeClient.Enabled = false;

                    btnAdaugaClient.Margin = new Padding(0, 0, 8, 0);
                    btnAnuleazaClient.Margin = new Padding(0, 0, 8, 0);
                    btnStergeClient.Margin = new Padding(0, 0, 0, 0);

                    panelButoaneClient.Controls.Add(btnAdaugaClient);
                    panelButoaneClient.Controls.Add(btnAnuleazaClient);
                    panelButoaneClient.Controls.Add(btnStergeClient);

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
                    panelFormularContent.Controls.Add(panelButoaneClient, 1, 4);
                    panelFormularContent.Controls.Add(lblMesajEroare, 0, 5);
                    panelFormularContent.SetColumnSpan(lblMesajEroare, 2);

                    pnlFormular.Controls.Add(panelFormularContent);
                    ClearForm();
                    clientSelectat = null;
                    if (lblMesajEroare != null)
                        lblMesajEroare.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la inițializarea controalelor: {ex.Message}\n\nDetalii: {ex.StackTrace}", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea formularului clienților: {ex.Message}\n\nDetalii: {ex.StackTrace}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void IncarcaFormCamere()
        {
            try
            {
                if (pnlFormular == null)
                    return;

                pnlFormular.Controls.Clear();

                var panelFormularContent = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 7,
                    AutoSize = true,
                    Padding = new Padding(15)
                };

                try
                {
                    panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                    panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
                    panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                    lblNumarCamera = CreeazaLabel("Număr cameră:", true);
                    txtNumarCamera = CreeazaTextBox(200);

                    var lblTipCamera = CreeazaLabel("Tip cameră:", true);
                    var panelTipCamera = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = true,
                        Margin = new Padding(0, 5, 0, 0)
                    };

                    rbSingle = new RadioButton { Text = "Single", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    rbDouble = new RadioButton { Text = "Double", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    rbQuad = new RadioButton { Text = "Quad", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    rbSuite = new RadioButton { Text = "Suite", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    rbDeluxe = new RadioButton { Text = "Deluxe", AutoSize = true, Font = new Font("Segoe UI", 8) };

                    panelTipCamera.Controls.AddRange(new Control[] { rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe });

                    var lblOptiuniCamera = CreeazaLabel("Opțiuni:", true);
                    var panelOptiuniCamera = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = true,
                        Margin = new Padding(0, 5, 0, 0)
                    };

                    cbWiFi = new CheckBox { Text = "WiFi", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    cbTV = new CheckBox { Text = "TV", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    cbAC = new CheckBox { Text = "Aer cond.", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    cbFrigider = new CheckBox { Text = "Frigider", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(0, 0, 20, 0) };
                    cbBalcon = new CheckBox { Text = "Balcon", AutoSize = true, Font = new Font("Segoe UI", 8) };

                    panelOptiuniCamera.Controls.AddRange(new Control[] { cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon });

                    var lblOcupareCamera = CreeazaLabel("Ocupată:", true);
                    var panelOcupare = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(0, 5, 0, 0)
                    };

                    cbOcupata = new CheckBox { AutoSize = true, Font = new Font("Segoe UI", 8) };
                    var lblDa = new Label
                    {
                        Text = "Da",
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(5, 3, 0, 0),
                        Font = new Font("Segoe UI", 8)
                    };

                    panelOcupare.Controls.Add(cbOcupata);
                    panelOcupare.Controls.Add(lblDa);

                    panelButoaneCamera = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Margin = new Padding(0, 8, 0, 0)
                    };

                    var btnAdaugaCamera = CreeazaButon("Salvează", Color.FromArgb(52, 152, 219), (s, e) =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(txtNumarCamera.Text))
                            {
                                lblMesajCamera.Text = "Introduceți numărul camerei!";
                                return;
                            }

                            if (!int.TryParse(txtNumarCamera.Text, out int numarNou))
                            {
                                lblMesajCamera.Text = "Numărul camerei trebuie să fie un întreg!";
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

                            string caleFisier = ConfigurationManager.AppSettings["NumeFisierCamere"];
                            if (string.IsNullOrEmpty(caleFisier))
                            {
                                lblMesajCamera.Text = "Eroare: Calea fișierului camerelor nu este setată!";
                                return;
                            }

                            var adminCamere = new AdministrareCamere(caleFisier);
                            var cameraNoua = new Camera(numarNou, tip, optiuni, cbOcupata.Checked);

                            if (cameraSelectata == null)
                            {
                                adminCamere.AdaugaCamera(cameraNoua);
                                lblMesajCamera.ForeColor = Color.Green;
                                lblMesajCamera.Text = "Camera adăugată cu succes!";
                            }
                            else
                            {
                                cameraSelectata.Numar = numarNou;
                                cameraSelectata.Tip = tip;
                                cameraSelectata.Optiuni = optiuni;
                                cameraSelectata.EsteOcupata = cbOcupata.Checked;
                                adminCamere.ActualizeazaCamera(cameraSelectata);
                                lblMesajCamera.ForeColor = Color.Green;
                                lblMesajCamera.Text = "Camera actualizată cu succes!";
                            }

                            ResetFormularCamera();
                            IncarcaCamere();
                        }
                        catch (Exception ex)
                        {
                            lblMesajCamera.Text = $"Eroare: {ex.Message}";
                        }
                    }, 90, 25);

                    var btnAnuleaza = CreeazaButon("Anulează", Color.FromArgb(231, 76, 60), (s, e) =>
                    {
                        ResetFormularCamera();
                    }, 80, 25);

                    var btnStergeCamera = CreeazaButon("Șterge", Color.FromArgb(192, 57, 43), BtnStergeCamera_Click, 80, 25);

                    btnAdaugaCamera.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnAnuleaza.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnStergeCamera.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnStergeCamera.Enabled = false;

                    panelButoaneCamera.Controls.Add(btnAdaugaCamera);
                    panelButoaneCamera.Controls.Add(btnAnuleaza);
                    panelButoaneCamera.Controls.Add(btnStergeCamera);

                    btnAdaugaCamera.Margin = new Padding(0, 0, 8, 0);
                    btnAnuleaza.Margin = new Padding(0, 0, 8, 0);
                    btnStergeCamera.Margin = new Padding(0, 0, 0, 0);

                    lblMesajCamera = new Label
                    {
                        ForeColor = Color.Red,
                        AutoSize = true,
                        Anchor = AnchorStyles.Left,
                        Font = new Font("Segoe UI", 8),
                        Margin = new Padding(0, 5, 0, 0)
                    };

                    panelFormularContent.Controls.Add(lblNumarCamera, 0, 0);
                    panelFormularContent.Controls.Add(txtNumarCamera, 1, 0);
                    panelFormularContent.Controls.Add(lblTipCamera, 0, 1);
                    panelFormularContent.Controls.Add(panelTipCamera, 1, 1);
                    panelFormularContent.Controls.Add(lblOptiuniCamera, 0, 2);
                    panelFormularContent.Controls.Add(panelOptiuniCamera, 1, 2);
                    panelFormularContent.Controls.Add(lblOcupareCamera, 0, 3);
                    panelFormularContent.Controls.Add(panelOcupare, 1, 3);
                    panelFormularContent.Controls.Add(panelButoaneCamera, 1, 4);
                    panelFormularContent.Controls.Add(lblMesajCamera, 0, 5);
                    panelFormularContent.SetColumnSpan(lblMesajCamera, 2);
                    pnlFormular.Controls.Add(panelFormularContent);

                    ResetFormularCamera();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la inițializarea controalelor: {ex.Message}\n\nDetalii: {ex.StackTrace}", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea formularului camerelor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ResetFormularCamera()
        {
            try
            {
                cameraSelectata = null;
                DezactiveazaButonStergereCamera();

                if (txtNumarCamera != null)
                    txtNumarCamera.Text = "";

                if (cbOcupata != null)
                    cbOcupata.Checked = false;

                if (cbWiFi != null)
                    cbWiFi.Checked = false;

                if (cbTV != null)
                    cbTV.Checked = false;

                if (cbAC != null)
                    cbAC.Checked = false;

                if (cbFrigider != null)
                    cbFrigider.Checked = false;

                if (cbBalcon != null)
                    cbBalcon.Checked = false;

                if (rbSingle != null)
                    rbSingle.Checked = true;

                if (rbDouble != null)
                    rbDouble.Checked = false;

                if (rbQuad != null)
                    rbQuad.Checked = false;

                if (rbSuite != null)
                    rbSuite.Checked = false;

                if (rbDeluxe != null)
                    rbDeluxe.Checked = false;

                if (lblMesajCamera != null)
                    lblMesajCamera.Text = "";
            }
            catch (Exception)
            {
                cameraSelectata = null;
                DezactiveazaButonStergereCamera();
                if (lblMesajCamera != null)
                    lblMesajCamera.Text = "Eroare la resetarea formularului";
            }
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
                lblMesajEroare.ForeColor = Color.Red;
                lblMesajEroare.Text = "Datele introduse nu sunt valide!";
                return;
            }

            try
            {
                string caleFisierClienti = ConfigurationManager.AppSettings["NumeFisierClienti"];
                if (string.IsNullOrEmpty(caleFisierClienti))
                    throw new ConfigurationErrorsException("Cheia 'NumeFisierClienti' lipsește din fișierul App.config.");

                var adminClienti = new AdministrareClienti(caleFisierClienti);

                if (clientSelectat == null)
                {
                    var clientNou = new Client(nume, prenume, telefon, email);
                    adminClienti.AdaugaClient(clientNou);

                    lblMesajEroare.ForeColor = Color.Green;
                    lblMesajEroare.Text = "Client adăugat cu succes!";
                }
                else
                {
                    clientSelectat.Nume = nume;
                    clientSelectat.Prenume = prenume;
                    clientSelectat.Telefon = telefon;
                    clientSelectat.Email = email;

                    adminClienti.ActualizeazaClient(clientSelectat, numeVechiClient, prenumeVechiClient);

                    lblMesajEroare.ForeColor = Color.Green;
                    lblMesajEroare.Text = "Client actualizat cu succes!";
                    clientSelectat = null;
                    numeVechiClient = null;
                    prenumeVechiClient = null;
                }

                ClearForm();
                IncarcaClienti();
            }
            catch (ConfigurationErrorsException ex)
            {
                lblMesajEroare.ForeColor = Color.Red;
                lblMesajEroare.Text = $"Eroare de configurare: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblMesajEroare.ForeColor = Color.Red;
                lblMesajEroare.Text = $"Eroare: {ex.Message}";
            }
        }


        private void BtnAnuleazaClient_Click(object sender, EventArgs e)
        {
            clientSelectat = null;
            ClearForm();
            lblMesajEroare.Text = "";
        }

        private void BtnStergeClient_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientSelectat == null)
                {
                    MessageBox.Show("Selectați mai întâi un client pentru ștergere!", "Atenție",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    $"Sigur doriți să ștergeți clientul {clientSelectat.Nume} {clientSelectat.Prenume}?",
                    "Confirmare ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(caleFisierClienti))
                    {
                        throw new ConfigurationErrorsException("Calea fișierului clienților nu este setată!");
                    }

                    var adminClienti = new AdministrareClienti(caleFisierClienti);
                    adminClienti.StergeClient(clientSelectat.Nume, clientSelectat.Prenume);
                    lblMesajEroare.ForeColor = Color.Green;
                    lblMesajEroare.Text = "Clientul a fost șters cu succes!";
                    clientSelectat = null;
                    ClearForm();
                    IncarcaClienti();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergerea clientului: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMesajEroare.ForeColor = Color.Red;
                lblMesajEroare.Text = "Eroare la ștergerea clientului!";
            }
        }

        private void DgvClienti_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!showClientForm || dgvClienti.SelectedRows.Count == 0)
                {
                    DezactiveazaButonStergereClient();
                    return;
                }

                DataGridViewRow selectedRow = dgvClienti.SelectedRows[0];
                if (selectedRow.Cells["Nume"].Value == null || selectedRow.Cells["Prenume"].Value == null)
                {
                    lblMesajEroare.ForeColor = Color.Red;
                    lblMesajEroare.Text = "Datele clientului sunt incomplete!";
                    DezactiveazaButonStergereClient();
                    return;
                }

                numeVechiClient = selectedRow.Cells["Nume"].Value.ToString();
                prenumeVechiClient = selectedRow.Cells["Prenume"].Value.ToString();

                if (string.IsNullOrWhiteSpace(caleFisierClienti))
                {
                    lblMesajEroare.ForeColor = Color.Red;
                    lblMesajEroare.Text = "Calea către fișierul clienților nu este setată!";
                    DezactiveazaButonStergereClient();
                    return;
                }

                if (!File.Exists(caleFisierClienti))
                {
                    lblMesajEroare.ForeColor = Color.Red;
                    lblMesajEroare.Text = $"Fișierul clienților nu există la calea: {caleFisierClienti}";
                    DezactiveazaButonStergereClient();
                    return;
                }

                var adminClienti = new AdministrareClienti(caleFisierClienti);
                clientSelectat = adminClienti.CautaClient(numeVechiClient, prenumeVechiClient);

                if (clientSelectat != null)
                {
                    AfiseazaClientSelectat();
                    ActiveazaButonStergereClient();
                }
                else
                {
                    lblMesajEroare.ForeColor = Color.Red;
                    lblMesajEroare.Text = $"Clientul {numeVechiClient} {prenumeVechiClient} nu a fost găsit în baza de date!";
                    ClearForm();
                    DezactiveazaButonStergereClient();
                }
            }
            catch (Exception ex)
            {
                lblMesajEroare.ForeColor = Color.Red;
                lblMesajEroare.Text = $"Eroare la selectarea clientului: {ex.Message}";
                ClearForm();
                DezactiveazaButonStergereClient();
            }
        }

        private void AfiseazaClientSelectat()
        {
            try
            {
                if (clientSelectat == null)
                {
                    ClearForm();
                    return;
                }

                txtNume.Text = clientSelectat.Nume ?? "";
                txtPrenume.Text = clientSelectat.Prenume ?? "";
                txtTelefon.Text = clientSelectat.Telefon ?? "";
                txtEmail.Text = clientSelectat.Email ?? "";

                lblMesajEroare.ForeColor = Color.FromArgb(64, 0, 64);
                lblMesajEroare.Text = "Client selectat. Puteți modifica și salva.";
            }
            catch (Exception)
            {
                ClearForm();
            }
        }

        private void DgvCamere_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!showClientForm && dgvCamere.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvCamere.SelectedRows[0];
                    if (selectedRow.Cells["Numar"].Value == null)
                    {
                        lblMesajCamera.ForeColor = Color.Red;
                        lblMesajCamera.Text = "Datele camerei sunt incomplete!";
                        DezactiveazaButonStergereCamera();
                        return;
                    }

                    int numarCamera;
                    if (!int.TryParse(selectedRow.Cells["Numar"].Value.ToString(), out numarCamera))
                    {
                        lblMesajCamera.ForeColor = Color.Red;
                        lblMesajCamera.Text = "Numărul camerei este invalid!";
                        DezactiveazaButonStergereCamera();
                        return;
                    }

                    string numeFisier = ConfigurationManager.AppSettings["NumeFisierCamere"];
                    if (string.IsNullOrWhiteSpace(numeFisier))
                    {
                        lblMesajCamera.ForeColor = Color.Red;
                        lblMesajCamera.Text = "Numele fișierului camerelor nu este setat în configurație!";
                        DezactiveazaButonStergereCamera();
                        return;
                    }

                    string caleCompletaFisier = Path.Combine(Application.StartupPath, numeFisier);
                    if (!File.Exists(caleCompletaFisier))
                    {
                        lblMesajCamera.ForeColor = Color.Red;
                        lblMesajCamera.Text = $"Fișierul camerelor nu există la calea: {caleCompletaFisier}";
                        DezactiveazaButonStergereCamera();
                        return;
                    }

                    AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);
                    cameraSelectata = adminCamere.CautaCameraDupaNumar(numarCamera);

                    if (cameraSelectata != null)
                    {
                        AfiseazaCameraSelectata();
                        ActiveazaButonStergereCamera();
                    }
                    else
                    {
                        lblMesajCamera.ForeColor = Color.Red;
                        lblMesajCamera.Text = $"Camera cu numărul {numarCamera} nu a fost găsită în baza de date!";
                        DezactiveazaButonStergereCamera();
                        ResetFormularCamera();
                    }
                }
                else
                {
                    DezactiveazaButonStergereCamera();
                }
            }
            catch (Exception ex)
            {
                lblMesajCamera.ForeColor = Color.Red;
                lblMesajCamera.Text = $"Eroare la selectarea camerei: {ex.Message}";
                DezactiveazaButonStergereCamera();
                ResetFormularCamera();
            }
        }

        private void AfiseazaCameraSelectata()
        {
            try
            {
                if (cameraSelectata == null)
                {
                    ResetFormularCamera();
                    return;
                }

                txtNumarCamera.Text = cameraSelectata.Numar.ToString();
                cbOcupata.Checked = cameraSelectata.EsteOcupata;

                rbSingle.Checked = cameraSelectata.Tip == TipCamera.Single;
                rbDouble.Checked = cameraSelectata.Tip == TipCamera.Double;
                rbQuad.Checked = cameraSelectata.Tip == TipCamera.Quad;
                rbSuite.Checked = cameraSelectata.Tip == TipCamera.Suite;
                rbDeluxe.Checked = cameraSelectata.Tip == TipCamera.Deluxe;

                cbWiFi.Checked = cameraSelectata.Optiuni.HasFlag(OptiuniCamera.WiFi);
                cbTV.Checked = cameraSelectata.Optiuni.HasFlag(OptiuniCamera.TV);
                cbAC.Checked = cameraSelectata.Optiuni.HasFlag(OptiuniCamera.AerConditionat);
                cbFrigider.Checked = cameraSelectata.Optiuni.HasFlag(OptiuniCamera.Frigider);
                cbBalcon.Checked = cameraSelectata.Optiuni.HasFlag(OptiuniCamera.Balcon);

                foreach (Control control in panelButoaneCamera.Controls)
                {
                    if (control is Button btn && btn.Text == "Șterge")
                    {
                        btn.Enabled = true;
                        break;
                    }
                }

                lblMesajCamera.ForeColor = Color.FromArgb(64, 0, 64);
                lblMesajCamera.Text = "Cameră selectată. Puteți modifica, salva sau șterge.";
            }
            catch (Exception ex)
            {
                lblMesajCamera.ForeColor = Color.Red;
                lblMesajCamera.Text = $"Eroare la afișarea datelor camerei: {ex.Message}";
                ResetFormularCamera();
            }
        }

        private void ResetEtichete()
        {
            lblNume.ForeColor = lblPrenume.ForeColor = lblTelefon.ForeColor = lblEmail.ForeColor = Color.Black;
            lblMesajEroare.Text = "";
        }

        private void ClearForm()
        {
            try
            {
                if (txtNume != null)
                    txtNume.Text = "";
                if (txtPrenume != null)
                    txtPrenume.Text = "";
                if (txtTelefon != null)
                    txtTelefon.Text = "";
                if (txtEmail != null)
                    txtEmail.Text = "";
                if (lblMesajEroare != null)
                {
                    lblMesajEroare.Text = "";
                    lblMesajEroare.ForeColor = Color.Red;
                }
                if (lblNume != null)
                    lblNume.ForeColor = Color.Black;
                if (lblPrenume != null)
                    lblPrenume.ForeColor = Color.Black;
                if (lblTelefon != null)
                    lblTelefon.ForeColor = Color.Black;
                if (lblEmail != null)
                    lblEmail.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la curățarea formularului: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncarcaClienti()
        {
            try
            {
                if (dgvClienti == null)
                    return;

                dgvClienti.Rows.Clear();

                if (!File.Exists(caleFisierClienti))
                {
                    lblStatistici.Text = "Fișierul clienților nu există!";
                    return;
                }

                var adminClienti = new AdministrareClienti(caleFisierClienti);
                var clienti = adminClienti.AfisareClienti();

                if (clienti != null && clienti.Count > 0)
                {
                    foreach (var client in clienti)
                    {
                        if (client != null && !string.IsNullOrEmpty(client.Nume))
                        {
                            dgvClienti.Rows.Add(
                                client.Nume,
                                client.Prenume,
                                client.Telefon,
                                client.Email
                            );
                        }
                    }
                    lblStatistici.Text = $"Total clienți: {clienti.Count}";
                }
                else
                {
                    lblStatistici.Text = "Nu există clienți înregistrați.";
                }
            }
            catch (Exception ex)
            {
                lblStatistici.Text = $"Eroare la încărcarea clienților: {ex.Message}";
                MessageBox.Show($"Eroare la încărcarea clienților: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IncarcaCamere()
        {
            try
            {
                if (dgvCamere == null)
                    return;

                dgvCamere.Rows.Clear();

                string numeFisier = ConfigurationManager.AppSettings["NumeFisierCamere"];
                if (string.IsNullOrWhiteSpace(numeFisier))
                {
                    lblStatistici.Text = "Numele fișierului camerelor nu este setat în configurație!";
                    lblStatistici.ForeColor = Color.White;
                    return;
                }

                string caleCompletaFisier = Path.Combine(Application.StartupPath, numeFisier);
                if (!File.Exists(caleCompletaFisier))
                {
                    lblStatistici.Text = $"Fișierul camerelor nu există la calea: {caleCompletaFisier}";
                    lblStatistici.ForeColor = Color.White;
                    return;
                }

                AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);
                List<Camera> camere = adminCamere.AfisareCamere();

                if (camere != null && camere.Count > 0)
                {
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

                    int total = camere.Count;
                    int ocupate = camere.Count(c => c != null && c.EsteOcupata);
                    int libere = total - ocupate;

                    lblStatistici.Text = $"Total camere: {total} | Ocupate: {ocupate} | Libere: {libere}";
                    lblStatistici.ForeColor = Color.White;
                }
                else
                {
                    lblStatistici.Text = "Nu există camere înregistrate.";
                    lblStatistici.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                lblStatistici.Text = $"Eroare la încărcarea camerelor: {ex.Message}";
                lblStatistici.ForeColor = Color.White;
                MessageBox.Show($"Eroare la încărcarea camerelor: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActiveazaButonStergereClient()
        {
            if (pnlFormular != null)
            {
                foreach (Control control in pnlFormular.Controls)
                {
                    if (control is TableLayoutPanel panel)
                    {
                        foreach (Control panelControl in panel.Controls)
                        {
                            if (panelControl is FlowLayoutPanel flowPanel)
                            {
                                foreach (Control btn in flowPanel.Controls)
                                {
                                    if (btn is Button button && button.Text == "Șterge")
                                    {
                                        button.Enabled = true;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DezactiveazaButonStergereClient()
        {
            if (pnlFormular != null)
            {
                foreach (Control control in pnlFormular.Controls)
                {
                    if (control is TableLayoutPanel panel)
                    {
                        foreach (Control panelControl in panel.Controls)
                        {
                            if (panelControl is FlowLayoutPanel flowPanel)
                            {
                                foreach (Control btn in flowPanel.Controls)
                                {
                                    if (btn is Button button && button.Text == "Șterge")
                                    {
                                        button.Enabled = false;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ActiveazaButonStergereCamera()
        {
            if (panelButoaneCamera != null)
            {
                foreach (Control control in panelButoaneCamera.Controls)
                {
                    if (control is Button btn && btn.Text == "Șterge")
                    {
                        btn.Enabled = true;
                        return;
                    }
                }
            }
        }

        private void DezactiveazaButonStergereCamera()
        {
            if (panelButoaneCamera != null)
            {
                foreach (Control control in panelButoaneCamera.Controls)
                {
                    if (control is Button btn && btn.Text == "Șterge")
                    {
                        btn.Enabled = false;
                        return;
                    }
                }
            }
        }

        private void BtnStergeCamera_Click(object sender, EventArgs e)
        {
            try
            {
                if (cameraSelectata == null)
                {
                    MessageBox.Show("Selectați mai întâi o cameră pentru ștergere!", "Atenție",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    $"Sigur doriți să ștergeți camera {cameraSelectata.Numar}?",
                    "Confirmare ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string numeFisier = ConfigurationManager.AppSettings["NumeFisierCamere"];
                    if (string.IsNullOrEmpty(numeFisier))
                    {
                        throw new ConfigurationErrorsException("Calea fișierului camerelor nu este setată!");
                    }

                    string caleCompletaFisier = Path.Combine(Application.StartupPath, numeFisier);
                    var adminCamere = new AdministrareCamere(caleCompletaFisier);

                    adminCamere.StergeCamera(cameraSelectata.Numar);

                    lblMesajCamera.ForeColor = Color.Green;
                    lblMesajCamera.Text = "Camera a fost ștearsă cu succes!";
                    cameraSelectata = null;
                    ResetFormularCamera();
                    IncarcaCamere();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la ștergerea camerei: {ex.Message}", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblMesajCamera.ForeColor = Color.Red;
                lblMesajCamera.Text = "Eroare la ștergerea camerei!";
            }
        }
    }
}