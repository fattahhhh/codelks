using LKS_SMK.DataAccess;
using LKS_SMK.Model;
using LKS_SMK.Store;
using LKS_SMK.Tampilan.Admin;
using LKS_SMK.Tampilan.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LKS_SMK.Tampilan.Login
{
    public partial class LoginForm : Form
    {
        LoginDataAccess da = new LoginDataAccess();
        public LoginForm()
        {
            InitializeComponent();
        }

 
        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsn.Text;
                string password = txtPass.Text;

                var user = da.Login(username, password);

                MainStore.user = user;

                Form newForm = null;
                switch (user.JobID)
                {
                    case 1:
                        newForm = new MainFormAdmin();
                        break;
                    case 2:
                        newForm = new MainFormCustomer();
                        break;
                } 
                this.Hide();
                newForm.Show();

                if (user.JobID == null) throw new Exception("User tidak ada dalam data");
            }
            catch (Exception ex)
            {
                txtWarning.Text = ex.Message;
            }
        }
    }
}
