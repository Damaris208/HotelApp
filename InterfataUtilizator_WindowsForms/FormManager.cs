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
    public partial class FormManager : Form
    {
        private const int LUNGIME_MINIMA_NUME = 2;
        private const int LUNGIME_MINIMA_TELEFON = 10;

        private DataGridView dgvCamere;
        private DataGridView dgvClienti; // New DataGridView for clients
        private Label lblStatistici;
        private TextBox txtCauta;
        private ComboBox cmbCriteriuCautare;
        private Label lblRezultatCautare;

        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail, txtNumarCamera;
        private Label lblNume, lblPrenume, lblTelefon, lblEmail, lblMesajEroare, lblNumarCamera, lblMesajCamera;
        private CheckBox cbOcupata, cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon;
        private RadioButton rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe;

        private void btnInapoi_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Label lblAntet;
        private Button btnInapoi;
        private Panel pnlAntet;
        private Camera cameraSelectata = null;
        private Client clientSelectat = null; // New field for selected client
        private Button btnClienti, btnCamere;
        private Panel pnlFormular;
        private bool showClientForm = true;

        public FormManager()
        {
            InitializeComponent();
            ConfigureazaFormular();
            AdaugaControale();
            IncarcaCamere();
            IncarcaClienti(); // Load clients initially
            ShowClientForm(); // Show client form by default
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

            // Create toggle buttons panel
            var panelToggleButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10, 5, 0, 0)
            };

            btnClienti = new Button
            {
                Text = "Gestionare Clienți",
                Width = 150,
                Height = 30,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Tag = "clienti"
            };
            btnClienti.Click += ToggleFormView;

            btnCamere = new Button
            {
                Text = "Gestionare Camere",
                Width = 150,
                Height = 30,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Tag = "camere",
                Margin = new Padding(5, 0, 0, 0)
            };
            btnCamere.Click += ToggleFormView;

            panelToggleButtons.Controls.Add(btnClienti);
            panelToggleButtons.Controls.Add(btnCamere);

            // Create form container panel
            pnlFormular = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(0, 45, 0, 0) // Add margin to prevent overlap
            };

            var panelFormContainer = new Panel
            {
                Dock = DockStyle.Fill
            };
            panelFormContainer.Controls.Add(pnlFormular);
            panelFormContainer.Controls.Add(panelToggleButtons);

            zonaMijloc.Controls.Add(panelFormContainer, 0, 0);

            // Create rooms table panel with search at top
            var panelTabel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Search panel at top right
            var panelCautare = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10, 5, 10, 5)
            };

            cmbCriteriuCautare = new ComboBox
            {
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbCriteriuCautare.Items.AddRange(new string[] { "Nume și Prenume", "Număr Telefon", "Adresă Email" });
            cmbCriteriuCautare.SelectedIndex = 0;

            txtCauta = new TextBox
            {
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(5, 0, 0, 0)
            };

            var btnCauta = CreeazaButon("Caută", Color.FromArgb(46, 204, 113), (s, e) => ExecutaCautare(), 80, 28);

            lblRezultatCautare = new Label
            {
                AutoSize = true,
                ForeColor = Color.DarkBlue,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panelCautare.Controls.Add(cmbCriteriuCautare);
            panelCautare.Controls.Add(txtCauta);
            panelCautare.Controls.Add(btnCauta);
            panelCautare.Controls.Add(lblRezultatCautare);

            // TabControl to switch between clients and rooms tables
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
                ItemSize = new Size(0, 1),
                SizeMode = TabSizeMode.Fixed,
                Margin = new Padding(0, 45, 0, 0)
            };

            // Clients tab
            var tabClienti = new TabPage();
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
                GridColor = Color.FromArgb(230, 230, 230)
            };
            ConfigureazaDataGridViewClienti();
            tabClienti.Controls.Add(dgvClienti);

            // Rooms tab
            var tabCamere = new TabPage();
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
            ConfigureazaDataGridViewCamere();
            tabCamere.Controls.Add(dgvCamere);

            tabControl.TabPages.Add(tabClienti);
            tabControl.TabPages.Add(tabCamere);
            tabControl.SelectedIndex = 0; // Show clients by default

            // Add event handlers
            dgvClienti.SelectionChanged += DgvClienti_SelectionChanged;
            dgvCamere.SelectionChanged += DgvCamere_SelectionChanged;

            panelTabel.Controls.Add(tabControl);
            panelTabel.Controls.Add(panelCautare);
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

        private void ConfigureazaDataGridViewClienti()
        {
            dgvClienti.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClienti.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvClienti.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClienti.EnableHeadersVisualStyles = false;
            dgvClienti.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvClienti.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvClienti.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Add columns
            dgvClienti.Columns.Clear();
            dgvClienti.Columns.Add("Nume", "Nume");
            dgvClienti.Columns.Add("Prenume", "Prenume");
            dgvClienti.Columns.Add("Telefon", "Telefon");
            dgvClienti.Columns.Add("Email", "Email");

            // Set column widths
            dgvClienti.Columns["Nume"].Width = 120;
            dgvClienti.Columns["Prenume"].Width = 120;
            dgvClienti.Columns["Telefon"].Width = 120;
            dgvClienti.Columns["Email"].Width = 180;
        }

        private void ConfigureazaDataGridViewCamere()
        {
            dgvCamere.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCamere.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvCamere.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCamere.EnableHeadersVisualStyles = false;
            dgvCamere.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvCamere.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dgvCamere.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Add columns
            dgvCamere.Columns.Clear();
            dgvCamere.Columns.Add("Numar", "Număr");
            dgvCamere.Columns.Add("Optiuni", "Opțiuni");
            dgvCamere.Columns.Add("Tip", "Tip");
            dgvCamere.Columns.Add("EsteOcupata", "Ocupată");

            // Set column widths
            dgvCamere.Columns["Numar"].Width = 80;
            dgvCamere.Columns["Optiuni"].Width = 250;
            dgvCamere.Columns["Tip"].Width = 80;
            dgvCamere.Columns["EsteOcupata"].Width = 80;

            dgvCamere.Columns["EsteOcupata"].DefaultCellStyle.ForeColor = Color.Red;
            dgvCamere.Columns["EsteOcupata"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }

        private void ExecutaCautare()
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
                
                // Highlight the found client in the grid
                foreach (DataGridViewRow row in dgvClienti.Rows)
                {
                    if (row.Cells["Nume"].Value.ToString() == clientGasit.Nume && 
                        row.Cells["Prenume"].Value.ToString() == clientGasit.Prenume)
                    {
                        row.Selected = true;
                        dgvClienti.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
            else
            {
                lblRezultatCautare.ForeColor = Color.Red;
                lblRezultatCautare.Text = "Niciun client găsit.";
            }
        }

        private void ToggleFormView(object sender, EventArgs e)
        {
            var button = (Button)sender;
            showClientForm = (string)button.Tag == "clienti";

            // Update button colors
            btnClienti.BackColor = showClientForm ? Color.FromArgb(52, 152, 219) : Color.FromArgb(70, 130, 180);
            btnCamere.BackColor = !showClientForm ? Color.FromArgb(52, 152, 219) : Color.FromArgb(70, 130, 180);

            if (showClientForm)
            {
                ShowClientForm();
            }
            else
            {
                ShowRoomForm();
            }
        }

        private void ShowClientForm()
        {
            pnlFormular.Controls.Clear();

            var panelFormularContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 7,
                AutoSize = true,
                Padding = new Padding(15)
            };

            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            for (int i = 0; i < 7; i++)
                panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 5 ? 45 : 35));

            lblNume = new Label { Text = "Nume:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            txtNume = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 8) };
            lblPrenume = new Label { Text = "Prenume:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            txtPrenume = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 8) };
            lblTelefon = new Label { Text = "Telefon:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            txtTelefon = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 8) };
            lblEmail = new Label { Text = "Email:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            txtEmail = new TextBox { Width = 200, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 8) };

            var panelButoaneClient = new FlowLayoutPanel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var btnAdaugaClient = CreeazaButon("Salvează Client", Color.FromArgb(52, 152, 219), BtnAdaugaClient_Click, 150, 30);
            var btnAnuleazaClient = CreeazaButon("Anulează", Color.FromArgb(231, 76, 60), BtnAnuleazaClient_Click, 100, 30);
            var btnRefreshClienti = CreeazaButon("Reîncarcă", Color.FromArgb(46, 204, 113), BtnRefreshClienti_Click, 100, 30);

            panelButoaneClient.Controls.Add(btnAdaugaClient);
            panelButoaneClient.Controls.Add(btnRefreshClienti);
            panelButoaneClient.Controls.Add(btnAnuleazaClient);

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
            panelFormularContent.Controls.Add(lblMesajEroare, 1, 5);

            pnlFormular.Controls.Add(panelFormularContent);
        }

        private void ShowRoomForm()
        {
            pnlFormular.Controls.Clear();

            var panelFormularContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 10,
                AutoSize = true,
                Padding = new Padding(15)
            };

            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            panelFormularContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            for (int i = 0; i < 10; i++)
                panelFormularContent.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 4 || i == 8 ? 45 : 35));

            lblNumarCamera = new Label { Text = "Număr cameră:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            txtNumarCamera = new TextBox { Width = 100, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 8) };

            var lblTipCamera = new Label { Text = "Tip cameră:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            var panelTipCamera = new FlowLayoutPanel { AutoSize = true, Anchor = AnchorStyles.Left, FlowDirection = FlowDirection.LeftToRight };
            rbSingle = new RadioButton { Text = "Single", Checked = true, AutoSize = true, Font = new Font("Segoe UI", 8) };
            rbDouble = new RadioButton { Text = "Double", AutoSize = true, Font = new Font("Segoe UI", 8) };
            rbQuad = new RadioButton { Text = "Quad", AutoSize = true, Font = new Font("Segoe UI", 8) };
            rbSuite = new RadioButton { Text = "Suite", AutoSize = true, Font = new Font("Segoe UI", 8) };
            rbDeluxe = new RadioButton { Text = "Deluxe", AutoSize = true, Font = new Font("Segoe UI", 8) };
            panelTipCamera.Controls.AddRange(new Control[] { rbSingle, rbDouble, rbQuad, rbSuite, rbDeluxe });

            var lblOptiuniCamera = new Label { Text = "Opțiuni:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            var panelOptiuniCamera = new FlowLayoutPanel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            cbWiFi = new CheckBox { Text = "WiFi", AutoSize = true, Font = new Font("Segoe UI", 8) };
            cbTV = new CheckBox { Text = "TV", AutoSize = true, Font = new Font("Segoe UI", 8) };
            cbAC = new CheckBox { Text = "Aer cond.", AutoSize = true, Font = new Font("Segoe UI", 8) };
            cbFrigider = new CheckBox { Text = "Frigider", AutoSize = true, Font = new Font("Segoe UI", 8) };
            cbBalcon = new CheckBox { Text = "Balcon", AutoSize = true, Font = new Font("Segoe UI", 8) };

            panelOptiuniCamera.Controls.AddRange(new Control[] { cbWiFi, cbTV, cbAC, cbFrigider, cbBalcon });

            var lblOcupareCamera = new Label { Text = "Ocupată:", Anchor = AnchorStyles.Left, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold) };
            var panelOcupare = new FlowLayoutPanel { AutoSize = true, Anchor = AnchorStyles.Left };
            cbOcupata = new CheckBox { AutoSize = true, Font = new Font("Segoe UI", 8) };
            var lblDa = new Label { Text = "Da", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 6, 0, 0), Font = new Font("Segoe UI", 8) };
            panelOcupare.Controls.Add(cbOcupata);
            panelOcupare.Controls.Add(lblDa);

            var panelButoaneCamera = new FlowLayoutPanel
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var btnAdaugaCamera = CreeazaButon("Salvează cameră", Color.FromArgb(52, 152, 219), (s, e) =>
            {
                if (string.IsNullOrEmpty(txtNumarCamera.Text))
                {
                    lblMesajCamera.Text = "Introduceți numărul camerei!";
                    return;
                }

                int numarNou;
                if (!int.TryParse(txtNumarCamera.Text, out numarNou))
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
                bool esteOcupata = cbOcupata.Checked;

                try
                {
                    string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
                    string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                    string caleCompletaFisier = Path.Combine(locatieFisierSolutie, numeFisier);

                    AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);

                    if (cameraSelectata == null)
                    {
                        var cameraNoua = new Camera(
                            int.Parse(txtNumarCamera.Text),
                            tip,
                            optiuni,
                            cbOcupata.Checked
                        );

                        adminCamere.AdaugaCamera(cameraNoua);
                        lblMesajCamera.ForeColor = Color.Green;
                        lblMesajCamera.Text = "Camera adăugată cu succes!";
                    }
                    else
                    {
                        cameraSelectata.Numar = int.Parse(txtNumarCamera.Text);
                        cameraSelectata.Tip = tip;
                        cameraSelectata.Optiuni = optiuni;
                        cameraSelectata.EsteOcupata = cbOcupata.Checked;

                        adminCamere.ActualizeazaCamera(cameraSelectata);
                        lblMesajCamera.ForeColor = Color.Green;
                        lblMesajCamera.Text = "Camera actualizată cu succes!";
                    }

                    txtNumarCamera.Text = "";
                    cbOcupata.Checked = false;
                    cbWiFi.Checked = cbTV.Checked = cbAC.Checked = cbFrigider.Checked = cbBalcon.Checked = false;
                    cameraSelectata = null;
                    IncarcaCamere();
                }
                catch (Exception ex)
                {
                    lblMesajCamera.Text = $"Eroare: {ex.Message}";
                }
            }, 150, 30);

            var btnAnuleaza = CreeazaButon("Anulează", Color.FromArgb(231, 76, 60), (s, e) =>
            {
                cameraSelectata = null;
                txtNumarCamera.Text = "";
                cbOcupata.Checked = false;
                cbWiFi.Checked = cbTV.Checked = cbAC.Checked = cbFrigider.Checked = cbBalcon.Checked = false;
                lblMesajCamera.Text = "";
            }, 100, 30);

            var btnRefresh = CreeazaButon("Reîncarcă", Color.FromArgb(46, 204, 113), (s, e) => IncarcaCamere(), 100, 30);

            panelButoaneCamera.Controls.Add(btnAdaugaCamera);
            panelButoaneCamera.Controls.Add(btnRefresh);
            panelButoaneCamera.Controls.Add(btnAnuleaza);

            lblMesajCamera = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 8)
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

            try
            {
                var clientNou = new Client(nume, prenume, telefon, email);
                var adminClienti = new AdministrareClienti();

                if (clientSelectat == null)
                {
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
                    
                    adminClienti.ActualizeazaClient(clientSelectat);
                    lblMesajEroare.ForeColor = Color.Green;
                    lblMesajEroare.Text = "Client actualizat cu succes!";
                    clientSelectat = null;
                }

                ClearForm();
                IncarcaClienti();
            }
            catch (Exception ex)
            {
                lblMesajEroare.Text = $"Eroare: {ex.Message}";
            }
        }

        private void BtnAnuleazaClient_Click(object sender, EventArgs e)
        {
            clientSelectat = null;
            ClearForm();
            lblMesajEroare.Text = "";
        }

        private void BtnRefreshClienti_Click(object sender, EventArgs e)
        {
            IncarcaClienti();
        }

        private void DgvClienti_SelectionChanged(object sender, EventArgs e)
        {
            if (showClientForm && dgvClienti.SelectedRows.Count > 0)
            {
                string nume = dgvClienti.SelectedRows[0].Cells["Nume"].Value.ToString();
                string prenume = dgvClienti.SelectedRows[0].Cells["Prenume"].Value.ToString();

                var adminClienti = new AdministrareClienti();
                clientSelectat = adminClienti.CautaClient(nume, prenume);

                if (clientSelectat != null)
                {
                    AfiseazaClientSelectat();
                }
            }
        }

        private void AfiseazaClientSelectat()
        {
            txtNume.Text = clientSelectat.Nume;
            txtPrenume.Text = clientSelectat.Prenume;
            txtTelefon.Text = clientSelectat.Telefon;
            txtEmail.Text = clientSelectat.Email;

            lblMesajEroare.ForeColor = Color.Blue;
            lblMesajEroare.Text = "Client selectat. Puteți modifica și salva.";
        }

        private void DgvCamere_SelectionChanged(object sender, EventArgs e)
        {
            if (!showClientForm && dgvCamere.SelectedRows.Count > 0)
            {
                int numarCamera = Convert.ToInt32(dgvCamere.SelectedRows[0].Cells["Numar"].Value);

                string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
                string locatieFisierSolutie = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string caleCompletaFisier = Path.Combine(locatieFisierSolutie, numeFisier);

                AdministrareCamere adminCamere = new AdministrareCamere(caleCompletaFisier);
                cameraSelectata = adminCamere.CautaCameraDupaNumar(numarCamera);

                if (cameraSelectata != null)
                {
                    AfiseazaCameraSelectata();
                }
            }
        }

        private void AfiseazaCameraSelectata()
        {
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

            lblMesajCamera.Text = "Camera selectată. Puteți modifica și salva.";
            lblMesajCamera.ForeColor = Color.Blue;
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

        private void IncarcaClienti()
        {
            try
            {
                var adminClienti = new AdministrareClienti();
                List<Client> clienti = adminClienti.AfisareClienti();

                dgvClienti.Rows.Clear();

                foreach (var client in clienti)
                {
                    dgvClienti.Rows.Add(
                        client.Nume,
                        client.Prenume,
                        client.Telefon,
                        client.Email
                    );
                }

                // Update statistics
                int totalClienti = clienti.Count;
                lblStatistici.Text = $"Total clienți: {totalClienti}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea clienților: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                dgvCamere.Rows.Clear();

                foreach (var camera in camere)
                {
                    dgvCamere.Rows.Add(
                        camera.Numar,
                        camera.Optiuni.ToString(),
                        camera.Tip.ToString(),
                        camera.EsteOcupata ? "DA" : "NU"
                    );
                }

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