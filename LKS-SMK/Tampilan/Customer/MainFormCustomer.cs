using LKS_SMK.Store;
using LKS_SMK.Tampilan.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LKS_SMK.Tampilan.Customer
{
    public partial class MainFormCustomer : Form
    {
        public MainFormCustomer()
        {
            txtWelcome.Text = $"Welcome, {MainStore.user.Name}";
            InitializeComponent();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
