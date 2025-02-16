using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
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
        int? _UserID = -1;

        bool _Next = false;

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

        // Just Add new Mode
        private void NavigateToTab(TabPage tabPage)
        {
            tabControl1.SelectedTab = tabPage;
            _Next = true;
        }

        private bool CheckPersonAndUserExistence(string nationalNo, string personID)
        {
            // Check by Person ID
            if (!string.IsNullOrEmpty(personID))
            {
                bool personExistsByPersonID = clsPeople.SearchData(clsPeople.PeopleColumn.PersonID, personID, clsPeople.SearchMode.ExactMatch).Rows.Count > 0;
                bool userExistsByPersonID = clsUsers.SearchData(clsUsers.UsersColumn.PersonID, personID, clsUsers.SearchMode.ExactMatch).Rows.Count > 0;

                if (_Mode == enMode.AddNew)
                {
                    if (personExistsByPersonID)
                    {
                        if (!userExistsByPersonID)
                        {
                            NavigateToTab(tabPage2);
                            return true;
                        }
                        else
                        {
                            ShowErrorMessage($"User with Person ID: {personID}  Isn't The Same.");
                            return true;
                        }
                    }
                }
                else if (_Mode == enMode.Update)
                {
                    if (personExistsByPersonID)
                    {
                        if (userExistsByPersonID)
                        {
                            NavigateToTab(tabPage2);
                            return true;
                        }
                        else
                        {
                            ShowErrorMessage($"User with Person ID: {personID} Isn.");
                            return true;
                        }
                    }
                }

            }


            // Check by National Number
            if (!string.IsNullOrEmpty(nationalNo))
            {
                bool personExistsByNationalNo = clsPeople.SearchData(clsPeople.PeopleColumn.NationalNo, nationalNo, clsPeople.SearchMode.ExactMatch).Rows.Count > 0;
                bool userExistsByNationalNo = clsUsers.IsFoundUserByNationalNo(nationalNo);

                if (_Mode == enMode.AddNew)
                {
                    if (personExistsByNationalNo)
                    {
                        if (!userExistsByNationalNo)
                        {
                            NavigateToTab(tabPage2);
                            return true;
                        }
                        else
                        {
                            ShowErrorMessage($"User with National No: {nationalNo} already exists.");
                            return true;
                        }
                    }
                }
                else if (_Mode == enMode.Update)
                {
                    if (!personExistsByNationalNo)
                    {
                        if (userExistsByNationalNo)
                        {
                            NavigateToTab(tabPage2);
                            return true;
                        }
                        else
                        {
                            ShowErrorMessage($"User with National No: {nationalNo} Isn't The Same.");
                            return true;
                        }
                    }

                }


            }


            return false;
        }


        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_Next)
            {
                _Next = false;
                return;
            }

            if (tabControl1.SelectedTab == tabPage1)
            {
                return; // Stay on tabPage1
            }

            string nationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;
            string personID = ctrlFilterAndMakePersonInfo1._PersonID?.ToString();

            // Check if a valid person exists and navigate accordingly
            if (CheckPersonAndUserExistence(nationalNo, personID))
            {
                return;
            }

            // If no valid person is found
            ShowErrorMessage("No valid person found. Please enter correct information.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string nationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;
            string personID = ctrlFilterAndMakePersonInfo1._PersonID?.ToString();

            // Check if the person exists by National Number
            if (CheckPersonAndUserExistence(nationalNo, personID))
            {
                return;
            }

            // If no valid person is found
            ShowErrorMessage("No valid person found. Please enter correct information.");
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            string userName = tbUserName.Text.Trim();

            // Check if username is empty
            if (string.IsNullOrWhiteSpace(userName))
            {
                errorProvider1.SetError(tbUserName, "Enter your username.");
                return;
            }

            // Check if username is at least 4 characters long
            if (userName.Length < 4)
            {
                errorProvider1.SetError(tbUserName, "Username must be at least 4 characters long.");
                return;
            }

            // Check if username already exists
            if (clsUsers.SearchData(clsUsers.UsersColumn.UserName, userName, clsUsers.SearchMode.ExactMatch).Rows.Count != 0)
            {
                errorProvider1.SetError(tbUserName, $"Username '{userName}' is already taken. Enter another.");
                return;
            }

            // No errors, clear error provider
            errorProvider1.SetError(tbUserName, string.Empty);
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            string password = tbPassword.Text.Trim();

            // Check if password is empty
            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider1.SetError(tbPassword, "Enter your password.");
                return;
            }

            // Check if password meets minimum length (e.g., 6 characters)
            if (password.Length < 6)
            {
                errorProvider1.SetError(tbPassword, "Password must be at least 6 characters long.");
                return;
            }

            // No errors, clear error provider
            errorProvider1.SetError(tbPassword, string.Empty);
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            string password = tbPassword.Text.Trim();
            string confirmPassword = tbConfirmPassword.Text.Trim();

            // Check if password field is empty
            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider1.SetError(tbConfirmPassword, "Enter your password first.");
                return;
            }

            // Check if password length is less than 6 characters
            if (password.Length < 6)
            {
                errorProvider1.SetError(tbConfirmPassword, "Password must be at least 6 characters long.");
                return;
            }

            // Check if confirm password field is empty
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorProvider1.SetError(tbConfirmPassword, "Enter your confirm password.");
                return;
            }

            // Check if confirm password matches the original password
            if (confirmPassword != password)
            {
                errorProvider1.SetError(tbConfirmPassword, "Confirm password does not match.");
                return;
            }

            // No errors, clear the error provider
            errorProvider1.SetError(tbConfirmPassword, string.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(tbUserName) != "" ||
                errorProvider1.GetError(tbPassword) != "" ||
                errorProvider1.GetError(tbConfirmPassword) != "")
            {
                // There are errors, so stop the save operation
                MessageBox.Show("Please correct the errors before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string userName = tbUserName.Text.Trim();
            string password = tbPassword.Text.Trim();
            bool isActive = chbIsActive.Checked;
            int? personID = null;

            // Try finding PersonID using NationalNo first
            if (!string.IsNullOrWhiteSpace(ctrlFilterAndMakePersonInfo1._NationalNo))
            {
                var person = clsPeople.FindByNationalNo(ctrlFilterAndMakePersonInfo1._NationalNo);
                if (person != null)
                    personID = person.PersonID;
            }

            // If not found, try finding by PersonID
            if (personID == null && ctrlFilterAndMakePersonInfo1._PersonID != null)
            {
                var person = clsPeople.FindByPersonID(ctrlFilterAndMakePersonInfo1._PersonID);
                if (person != null)
                    personID = ctrlFilterAndMakePersonInfo1._PersonID;
            }

            // If no person found, show an error and exit
            if (personID == null)
            {
                MessageBox.Show("No person found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string messageError = "";

            // Perform Add or Update operation
            if (_Mode == enMode.AddNew)
            {
                messageError = clsUsers.AddNewUsers(ref _UserID, (int)personID, userName, password, isActive);
                if (string.IsNullOrEmpty(messageError))
                {
                    MessageBox.Show("User added successfully :-)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                }
            }
            else if (_Mode == enMode.Update)
            {
                messageError = clsUsers.UpdateUsersByID((int)_UserID, (int)personID, userName, password, isActive);
                if (string.IsNullOrEmpty(messageError))
                {
                    MessageBox.Show("User updated successfully :-)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Add user denied :-(", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Show error message if any
            if (!string.IsNullOrEmpty(messageError))
            {
                ShowErrorMessage(messageError);
                return;
            }

            frmAddNewEditUser_Load();
        }


        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;

        }

        private void UploadAllData(clsUsers UserInfo)
        {


            //_ClsUpdate = Data;

            lblAddNewEditUser.Text = "Update User";



            ctrlFilterAndMakePersonInfo1._PersonID = UserInfo.PersonID;

            ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

            ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
            ctrlFilterAndMakePersonInfo1.textBoxData(UserInfo.PersonID.ToString());
            ctrlFilterAndMakePersonInfo1.DisabledFilterBy();

            lblUserID.Text = UserInfo.UserID.ToString();
            tbUserName.Text = UserInfo.UserName;
            
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

                clsUsers UserInfo = clsUsers.FindByUserID(_UserID);

                UploadAllData(UserInfo);

            }
        }

        private void frmAddNewEditUser_Load(object sender, EventArgs e)
        {
            if (_UserID != -1 || _UserID == 0)
            {
                clsUsers UserInfo = clsUsers.FindByUserID(_UserID);

                UploadAllData(UserInfo);

            }
        }



    }
}
