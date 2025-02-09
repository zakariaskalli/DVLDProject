using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLDProject.ctrlAddEditPersonInfo;

namespace DVLDProject
{
    public partial class frmAddNewEditUser : Form
    {
        //int _PersonID = -1;
        //string _NationalNo = "";
        int _UserID = -1;

        bool _Next = false;

        clsAddNewEditUserBusiness _ClsAdd;


        enum enMode { AddNew = 1, Update = 2}

        enMode _Mode;

        public frmAddNewEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddNewEditUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            _Mode = enMode.Update;

        }

        /*
        private void ctrlFilterBy1_OnFilterBtn(string arg1, string arg2)
        {

            if (clsAddNewEditUserBusiness.IsFoundPerson(arg1, arg2))
            {
                ctrlFilterAndMakePersonInfo1._NationalNo = "";
                ctrlFilterAndMakePersonInfo1._PersonID = -1;

                ctrlShowPersonDetails1.ctrlFilterAndMakePersonInfo1._NationalNo = "";
                ctrlShowPersonDetails1.ctrlFilterAndMakePersonInfo1._PersonID = -1;

                if (arg1 == "NationalNo")
                {
                    ctrlShowPersonDetails1.ctrlFilterAndMakePersonInfo1._NationalNo = arg2;
                    ctrlFilterAndMakePersonInfo1._NationalNo = arg2;
                }
                else if (arg1 == "PersonID")
                {
                    ctrlShowPersonDetails1.ctrlFilterAndMakePersonInfo1._PersonID = int.Parse(arg2);
                    ctrlFilterAndMakePersonInfo1._PersonID = int.Parse(arg2);
                }



                ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
            }
            else if (arg1 == "NationalNo" && arg2 != "")
                MessageBox.Show("No Person With National No = " + arg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (arg1 == "PersonID" && arg2 != "")
                MessageBox.Show("No Person With Person ID = " + arg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("No Person With This Value Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_Next == true)
            {
                _Next = false;
                return;
            }

            if (tabControl1.SelectedTab == tabPage1)
            {
                tabControl1.SelectedTab = tabPage1;
                return;
            }

            if (_Mode == enMode.Update)
            {
                if (tabControl1.SelectedTab == tabPage1)
                    tabControl1.SelectedTab = tabPage1;
                else
                    tabControl1.SelectedTab = tabPage2;
                
                return;
            }


            if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
            {

                if (clsAddNewEditUserBusiness.IsFoundPerson(ctrlFilterAndMakePersonInfo1.GetFilterByName(), ctrlFilterAndMakePersonInfo1._NationalNo))
                {

                    if (!clsMethodsGeneralBusiness.IsFoundUserByNationalNo(ctrlFilterAndMakePersonInfo1._NationalNo))
                    {
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        e.Cancel = true;

                        MessageBox.Show($"User By National No:{ctrlFilterAndMakePersonInfo1._NationalNo} Is Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }
            else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
            {
                if (clsAddNewEditUserBusiness.IsFoundPerson(ctrlFilterAndMakePersonInfo1.GetFilterByName(), Convert.ToString(ctrlFilterAndMakePersonInfo1._PersonID)))
                {

                    if (!clsMethodsGeneralBusiness.IsFoundUserByPersonID((int)ctrlFilterAndMakePersonInfo1._PersonID))
                    {
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        e.Cancel = true;

                        MessageBox.Show($"User By Person ID:{ctrlFilterAndMakePersonInfo1._PersonID} Is Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
            }
            else
            {

                e.Cancel = true;

                MessageBox.Show("Enter True People", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (clsAddNewEditUserBusiness.IsFoundPerson(ctrlFilterAndMakePersonInfo1.GetFilterByName(), ctrlFilterAndMakePersonInfo1.GetData()))
            {
                if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                {
                    if (!clsMethodsGeneralBusiness.IsFoundUserByNationalNo(ctrlFilterAndMakePersonInfo1._NationalNo))
                    {
                        tabControl1.SelectedTab = tabPage2;
                        _Next = true;
                    }
                    else
                        MessageBox.Show($"User By National No:{ctrlFilterAndMakePersonInfo1._NationalNo} Is Found" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!clsMethodsGeneralBusiness.IsFoundUserByPersonID((int)ctrlFilterAndMakePersonInfo1._PersonID))
                    {
                        tabControl1.SelectedTab = tabPage2;
                        _Next = true;
                    }
                    else
                        MessageBox.Show($"User By Person ID:{ctrlFilterAndMakePersonInfo1._PersonID} Is Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
                MessageBox.Show("Enter True People", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }


        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {

            if (this.ActiveControl == button1)
            {
                e.Cancel = false;
                return;
            }

            if (tbUserName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "Enter Your UserName.");
            }
            else if (clsMethodsGeneralBusiness.IsUserNameFound(tbUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbUserName, "UserName By = " + tbUserName.Text + ", Enter Another");
            }
            else
                errorProvider1.SetError(tbUserName, string.Empty);


        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {

            if (this.ActiveControl == button1)
            {
                e.Cancel = false;
                return;
            }

            if (tbPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(tbPassword, "Enter Your Password.");
            }
            else
                errorProvider1.SetError(tbPassword, string.Empty);
        
        
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            
            if (this.ActiveControl == button1 )
            {
                e.Cancel = false;
                return;
            }

            if (tbPassword.Text == "")
                return;

            if ( this.ActiveControl == tbPassword)
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword, "Enter Your Match Password.");
                return;
            }

            if (tbConfirmPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Enter Your Password.");
                return;
            }
            else if (tbConfirmPassword.Text != tbPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(tbConfirmPassword, "Enter Your Confirm Password.");
                return;
            }

            errorProvider1.SetError(tbConfirmPassword, string.Empty);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string UserName = tbUserName.Text;
            string Password = tbPassword.Text;
            bool IsActive = chbIsActive.Checked;

            if (_UserID == -1 || _UserID == 0)
            {
                if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                    _ClsAdd = new clsAddNewEditUserBusiness(ctrlFilterAndMakePersonInfo1._NationalNo, UserName, Password, IsActive);
                else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
                    _ClsAdd = new clsAddNewEditUserBusiness((int)ctrlFilterAndMakePersonInfo1._PersonID, UserName, Password, IsActive);

                ctrlFilterAndMakePersonInfo1._PersonID = _ClsAdd.PersonID;
                ctrlFilterAndMakePersonInfo1._NationalNo = _ClsAdd.NationalNo;

            }
            else 
            {
                if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                    _ClsAdd = new clsAddNewEditUserBusiness(_UserID,ctrlFilterAndMakePersonInfo1._NationalNo, UserName, Password, IsActive);
                else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
                    _ClsAdd = new clsAddNewEditUserBusiness(_UserID, (int)ctrlFilterAndMakePersonInfo1._PersonID, UserName, Password, IsActive);

            }


            if (_ClsAdd.Save())
            {

                MessageBox.Show("User Add/Update Successfully :-)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlFilterAndMakePersonInfo1._PersonID = _ClsAdd.PersonID;
                ctrlFilterAndMakePersonInfo1._NationalNo = _ClsAdd.NationalNo;
                _UserID = _ClsAdd.UserID;
                _Mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Add User Denied :-(", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            frmAddNewEditUser_Load();

        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;

        }

        private void UploadAllData(clsAddNewEditUserBusiness Data)
        {


            //_ClsUpdate = Data;

            lblAddNewEditUser.Text = "Update User";



            ctrlFilterAndMakePersonInfo1._PersonID = Data.PersonID;

            ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

            ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
            ctrlFilterAndMakePersonInfo1.textBoxData(Data.PersonID.ToString());
            ctrlFilterAndMakePersonInfo1.DisabledFilterBy();

            lblUserID.Text = Data.UserID.ToString();
            tbUserName.Text = Data.UserName;
            
            //tbPassword.Text = Data.Password;
            //tbConfirmPassword.Text = Data.Password;

            tbPassword.Text = "********";
            tbConfirmPassword.Text = "********";

        }


        private void frmAddNewEditUser_Load()
        {
            // _UserID == 0
            if (_UserID != -1 || _UserID == 0)
            {
                _Mode = enMode.Update;

                clsAddNewEditUserBusiness Data = clsAddNewEditUserBusiness.UploadAllDataUserByUserID(_UserID);
                UploadAllData(Data);

            }
        }

        private void frmAddNewEditUser_Load(object sender, EventArgs e)
        {
            if (_UserID != -1 || _UserID == 0)
            {

                clsAddNewEditUserBusiness Data = clsAddNewEditUserBusiness.UploadAllDataUserByUserID(_UserID);
                UploadAllData(Data);

            }
        }



    }
}
