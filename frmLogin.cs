using Clinic_System.BLL;
using Clinic_System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic_System.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string LoggedIn;


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cnbUserType.Text.Trim();

            //Checking the login creditals 
            bool success = dal.logicCheck(l);
            if (success == true)
            {
                MessageBox.Show("Login Succesfull !");
                LoggedIn = l.username;

                //Have to open Respective Froms based on User Type
                switch (l.user_type)
                {
                    case "ADMIN":
                        {
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "USER":
                        {
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;
                }
                    
                       // {
                            //Display Message
                          //  MessageBox.Show("Invalid User Type");
                       // }
                
            }
            else
            {
                MessageBox.Show("Login Failed. Try again.");
            }
        }
    }
}
