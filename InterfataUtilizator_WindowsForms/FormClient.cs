using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace InterfataUtilizator_WindowsForms
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ConfigureazaFormular();
        }

        private void InitializeComponent()
        {
           
        }

        private void ConfigureazaFormular()
        {
            this.Text = "Interfață Client";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Buton înapoi
            Button btnBack = new Button();
            btnBack.Text = "Înapoi la meniu";
            btnBack.Size = new Size(120, 30);
            btnBack.Location = new Point(20, 20);
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);

            // Etichetă titlu
            Label lblTitle = new Label();
            lblTitle.Text = "Camere Disponibile";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(300, 20);
            this.Controls.Add(lblTitle);

            // DataGridView pentru camere
            DataGridView dgvCamere = new DataGridView();
            dgvCamere.Location = new Point(50, 70);
            dgvCamere.Size = new Size(700, 400);
            dgvCamere.BackgroundColor = Color.White;
            dgvCamere.BorderStyle = BorderStyle.Fixed3D;
            dgvCamere.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCamere.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Adaugă coloane
            dgvCamere.Columns.Add("Numar", "Număr");
            dgvCamere.Columns.Add("Tip", "Tip Cameră");
            dgvCamere.Columns.Add("Pret", "Preț/noapte");
            dgvCamere.Columns.Add("Optiuni", "Facilități");

            // Adaugă date de exemplu (în loc de a încărca din fișier)
            dgvCamere.Rows.Add("101", "Single", "250 RON", "WiFi, TV");
            dgvCamere.Rows.Add("102", "Double", "350 RON", "WiFi, TV, Frigider");
            dgvCamere.Rows.Add("201", "Suite", "500 RON", "WiFi, TV, AC, Frigider, Balcon");

            this.Controls.Add(dgvCamere);

            // Buton rezervă
            Button btnRezerva = new Button();
            btnRezerva.Text = "Rezervă Camera";
            btnRezerva.Size = new Size(200, 40);
            btnRezerva.Location = new Point(300, 500);
            btnRezerva.BackColor = Color.SteelBlue;
            btnRezerva.ForeColor = Color.White;
            btnRezerva.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(btnRezerva);
        }
    }
}