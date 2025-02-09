using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmChangePassword : Form
    {
        int? _PersonID = null;
        int _UserID = -1;


        public frmChangePassword(string UserName)
        {
            InitializeComponent();


            clsManageUsersBussiness clsData = clsManageUsersBussiness.UploadAllDataByUserName(UserName);

            _PersonID = clsData.PersonID;
            _UserID = clsData.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }
        //UserIDIsFound

        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            
            clsManageUsersBussiness clsData = clsManageUsersBussiness.UploadAllDataByUserID(UserID);

            _PersonID = clsData.PersonID;
            _UserID = clsData.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (tbNewPassword.Text != tbConfirmPassword.Text)
                errorProvider1.SetError(tbConfirmPassword, "Enter A Match Password.");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (
                (tbConfirmPassword.Text == "" || tbCurrentPasword.Text == "" || tbNewPassword.Text == "")
                || 
                (tbNewPassword.Text != tbConfirmPassword.Text)
                
                )
            {
                MessageBox.Show("Enter True All Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string OldPassword = tbCurrentPasword.Text;
            string NewPassword = tbNewPassword.Text;

            if (clsAddNewEditUserBusiness.UpdatePasswordUser(_UserID, OldPassword, NewPassword))
            {
                MessageBox.Show("Password Update Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Password Update Denied :-(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
