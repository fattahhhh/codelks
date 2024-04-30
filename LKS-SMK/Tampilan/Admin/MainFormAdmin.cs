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

namespace LKS_SMK.Tampilan.Admin
{
    public partial class MainFormAdmin : Form
    {
        public MainFormAdmin()
        {
            InitializeComponent();
            txtWelcome.Text = $"Welcome, {MainStore.user.Name}";
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

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new MasterRoomTypeAdmin().Show();
        }

        private void MainFormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmployeeForm().Show(); 
        }

        private void foodAndDrinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MasterFoodDrink().Show();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MasterItem().Show();
        }

        private void roomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MasterRoomAdmin().Show();
        }
    }
}
