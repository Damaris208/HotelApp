using System;
using System.Windows.Forms;

namespace InterfataUtilizator_WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Meniu Principal";
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormClient formClient = new FormClient();
            formClient.ShowDialog();
            this.Show();
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormManager formManager = new FormManager();
            formManager.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}