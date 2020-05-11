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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        userBLL u = new userBLL();
        userDAL daL = new userDAL();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

           

            //..........................................Getting Data from UI.........................................
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.user_type = cnbUserType.Text;
            u.added_date = DateTime.Now;
            u.gender = cnbGender.Text;

            //Getting User Name From Logged In User
            string LoggedInUser = frmLogin.LoggedIn;
            userBLL usr = daL.GetIDFromUserName(LoggedInUser);

            u.added_by = usr.id;

            //Inserting Data into Database 
             bool success = daL.Insert(u);
            //if data is successfully inserted value of success will be true
            if (success == true)
            {
                MessageBox.Show("User succesfully created.");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to create user");
            }
            //Refreshing Data Grid View
            DataTable dt = daL.Select();
            dgvUsers.DataSource = dt;

        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = daL.Select();
            dgvUsers.DataSource = dt;
        }
        private void clear()
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cnbUserType.Text = "";

           }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the index of the particular row
            int rowIndex = e.RowIndex;
            txtID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cnbUserType.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cnbGender.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the values from User UI
            u.id = Convert.ToInt32(txtID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.user_type = cnbUserType.Text;
            u.added_date = DateTime.Now;
            u.gender = cnbGender.Text;
            u.added_by = 1;

            //Updating Data into Database
            bool success = daL.Update(u);
            //if data is Updated the value of success will be true or else false
            if(success == true)
            {
                MessageBox.Show("UserData Updated Sucessfully");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Update UserData");
            }
            //Refereshing Data Grid view

            DataTable dt = daL.Select();
            dgvUsers.DataSource = dt; 


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Getting User Id from  
            u.id = Convert.ToInt32(txtID.Text);

            bool success = daL.Delete(u);
            if (success == true)
            {
                MessageBox.Show("User Deleted Succesfully");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete User");
            }
            //Refereshing Grid View
            DataTable dt = daL.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get Keyword from Text Box
            string keywords = txtSearch.Text;

            //Check if the keywords have value or not
            if (keywords != null)
            {
                DataTable dt = daL.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = daL.Select();
                dgvUsers.DataSource = dt;

            }
        }
    }
}
