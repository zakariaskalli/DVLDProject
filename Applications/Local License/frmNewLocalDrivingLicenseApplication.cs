using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer___DVLDProject;
using static DVLDProject.ctrlAddEditPersonInfo;
using System.Xml.Serialization;
using System.IO;
using DVLD_BusinessLayer;
using DVLDProject.Global_Classes;

namespace DVLDProject
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {

        bool _Next = false;

        int _LocalDrivingLicenseApplicationID = -1;

        clsLocalDrivingLicenseApplications LDLAppInfo = new clsLocalDrivingLicenseApplications();


        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();

        }

        public frmNewLocalDrivingLicenseApplication( int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadAllComboBox()
        {
            cbLicenseClass.Items.Clear();


            DataTable dtLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            } 
            cbLicenseClass.SelectedIndex = 2;

        }

        void LoadDefaultData()
        {
            string _FullDate = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";

            lblApplicationDate.Text = _FullDate;
            lblCreatedBy.Text = clsGlobal.CurrenntUser.UserName;
            lblApplicationFees.Text = ((int)clsApplicationTypes.FindByApplicationTypeID(1).ApplicationFees).ToString();

            btnSave.Enabled = false;
            tpApplicationInfo.Enabled = false;

            LoadAllComboBox();

        }

        private void UploadAllData(clsLocalDrivingLicenseApplications Data)
        {

            lblAddNewEditUser.Text = "Update Local Driving License Application";

            ctrlFilterAndMakePersonInfo1._NationalNo = Data.PeopleInfo.NationalNo;

            ctrlFilterAndMakePersonInfo1._PersonID = Data.PeopleInfo.PersonID;
            ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

            ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
            ctrlFilterAndMakePersonInfo1.textBoxData(Data.PeopleInfo.PersonID.ToString());
            ctrlFilterAndMakePersonInfo1.DisabledFilterBy();


            lblAppliactionID.Text = Data.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = $"{Data.ApplicationDate.Value.Day}/{Data.ApplicationDate.Value.Month}/{Data.ApplicationDate.Value.Year}";
            cbLicenseClass.SelectedIndex = (int)Data.LicenseClassID -1;
            lblApplicationFees.Text = ((int)clsApplicationTypes.FindByApplicationTypeID(1).ApplicationFees).ToString();
            lblCreatedBy.Text = clsMethodsGeneralBusiness.UserNameByUserID((int)Data.UsersInfo.UserID);

            btnSave.Enabled = true;
            tpApplicationInfo.Enabled = true;
        }

        private void frmNewLocalDrivingLicenseApplication_Load()
        {
            LoadDefaultData();


            if (_LocalDrivingLicenseApplicationID != -1 || _LocalDrivingLicenseApplicationID == 0)
            {

                clsLocalDrivingLicenseApplications Data = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

                UploadAllData(Data);

            }
            else
                LoadDefaultData();

        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            LoadDefaultData();

            if (_LocalDrivingLicenseApplicationID != -1 || _LocalDrivingLicenseApplicationID == 0)
            {
                clsLocalDrivingLicenseApplications Data = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

                UploadAllData(Data);

            }
            else
                LoadDefaultData();

        }



        private bool CheckPersonAndUserExistence(string nationalNo, string personID)
        {
            // Check by Person ID
            if (!string.IsNullOrEmpty(personID))
            {
                bool personExistsByPersonID = clsPeople.SearchData(clsPeople.PeopleColumn.PersonID, personID, clsPeople.SearchMode.ExactMatch).Rows.Count > 0;

                    if (personExistsByPersonID)
                    {
                        tabControl1.SelectedTab = tpApplicationInfo;
                        _Next = true;
                        return true;
                    }
            }

            if (!string.IsNullOrEmpty(nationalNo))
            {
                bool personExistsByNationalNo = clsPeople.SearchData(clsPeople.PeopleColumn.NationalNo, nationalNo, clsPeople.SearchMode.ExactMatch).Rows.Count > 0;

                if (personExistsByNationalNo)
                {
                    tabControl1.SelectedTab = tpApplicationInfo;
                    _Next = true;
                    return true;
                        
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

            if (LDLAppInfo.Mode == clsLocalDrivingLicenseApplications.enMode.Update)
            {
                if (tabControl1.SelectedTab == tabPage1)
                    tabControl1.SelectedTab = tabPage1;
                else
                    tabControl1.SelectedTab = tpApplicationInfo;
                return;
            }

            string nationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;
            string personID = ctrlFilterAndMakePersonInfo1._PersonID?.ToString();

            // Check if a valid person exists and navigate accordingly
            if (CheckPersonAndUserExistence(nationalNo, personID))
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;

                return;
            }
            //ShowErrorMessage("No valid person found. Please enter correct information.");


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string nationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;
            string personID = ctrlFilterAndMakePersonInfo1._PersonID?.ToString();

            // Check if the person exists by National Number
            if (CheckPersonAndUserExistence(nationalNo, personID))
            {
                btnSave.Enabled = true;

                tpApplicationInfo.Enabled = true;
                return;
            }


            // If no valid person is found
            ShowErrorMessage("No valid person found. Please enter correct information.");

        }

        //Test Debugging
        private void btnSave_Click(object sender, EventArgs e)
        {
            //UserID = clsMethodsGeneralBusiness.UserIDByUserName(_UserName);

            //Test Debugging
            int LicenseClassID = cbLicenseClass.SelectedIndex + 1;

            string NationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;
            int? PersonID = ctrlFilterAndMakePersonInfo1._PersonID;

            if (_LocalDrivingLicenseApplicationID == -1 
                ||
                _LocalDrivingLicenseApplicationID == 0
                ||
                clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LocalDrivingLicenseApplicationID) == null)
            {
                if (!string.IsNullOrEmpty(NationalNo)
                    ||
                    (clsPeople.FindByNationalNo(NationalNo) != null))
                {
                    if (clsLocalDrivingLicenseApplications.IsFoundApplicationMatchLocalDriveByNationalNo(NationalNo, LicenseClassID))
                    {
                        int ApplicationID = clsLocalDrivingLicenseApplications.ApplicationNumMatchNationalNoAndLicenseClassID(NationalNo, LicenseClassID);

                        MessageBox.Show($@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    LDLAppInfo = new clsLocalDrivingLicenseApplications(NationalNo, (int)clsGlobal.CurrenntUser.UserID, LicenseClassID);

                }
                else if (PersonID != -1 && PersonID != null)
                {

                    if (clsLocalDrivingLicenseApplications.IsFoundApplicationMatchLocalDriveByPersonID((int)PersonID, LicenseClassID))
                    {
                        int ApplicationID = clsLocalDrivingLicenseApplications.ApplicationNumMatchPersonIDAndLicenseClassID((int)PersonID, LicenseClassID);

                            MessageBox.Show($@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    LDLAppInfo = new clsLocalDrivingLicenseApplications((int)PersonID, (int)clsGlobal.CurrenntUser.UserID, LicenseClassID);

                }

                ctrlFilterAndMakePersonInfo1._PersonID = LDLAppInfo.ApplicantPersonID;
                ctrlFilterAndMakePersonInfo1._NationalNo = LDLAppInfo.PeopleInfo.NationalNo;

            }
            else if (
                clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LocalDrivingLicenseApplicationID) != null
                )
            {

                if (!string.IsNullOrEmpty(NationalNo)
                    ||
                    (clsPeople.FindByNationalNo(NationalNo) != null))
                {
                    if (clsLocalDrivingLicenseApplications.IsFoundApplicationMatchLocalDriveByNationalNo(NationalNo, LicenseClassID))
                    {
                        int ApplicationID = clsLocalDrivingLicenseApplications.ApplicationNumMatchNationalNoAndLicenseClassID(NationalNo, LicenseClassID);

                        MessageBox.Show($@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        return;
                    }
                }
                else if (PersonID != -1 && PersonID != null)
                {
                    if (clsLocalDrivingLicenseApplications.IsFoundApplicationMatchLocalDriveByPersonID((int)PersonID, LicenseClassID) )
                    {
                        int ApplicationID = clsLocalDrivingLicenseApplications.ApplicationNumMatchPersonIDAndLicenseClassID((int)PersonID, LicenseClassID);

                        MessageBox.Show($@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        
                        return;
                    }
                }

                LDLAppInfo = new clsLocalDrivingLicenseApplications(_LocalDrivingLicenseApplicationID, LicenseClassID);
            }


            LDLAppInfo.ApplicantPersonID = PersonID;
            LDLAppInfo.ApplicationDate = DateTime.Now;
            LDLAppInfo.ApplicationTypeID = 1;
            LDLAppInfo.ApplicationStatus = clsApplications.enApplicationStatus.New;
            LDLAppInfo.LastStatusDate = DateTime.Now;
            LDLAppInfo.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            LDLAppInfo.CreatedByUserID = clsGlobal.CurrenntUser.UserID;
            LDLAppInfo.LicenseClassID = LicenseClassID;






            if (LDLAppInfo.Save())
            {

                MessageBox.Show("User Add/Update Successfully :-)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LocalDrivingLicenseApplicationID = (int)LDLAppInfo.LocalDrivingLicenseApplicationID;
            }
            else
            {
                MessageBox.Show("Add User Denied :-(", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            frmNewLocalDrivingLicenseApplication_Load();

        }

        private void ctrlFilterAndMakePersonInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmNewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlFilterAndMakePersonInfo1.SelectedTextBox();
        }
    }
}
