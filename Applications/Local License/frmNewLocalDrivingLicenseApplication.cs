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

namespace DVLDProject
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        string _UserName = "";


        int _Fees = clsNewLocalDrivingLicenseApplicationBusiness.ApplicationFeesNewLocalDrivingLicenseService();
        
        
        //int _ApplicationID = -1;

        bool _Next = false;

        int UserID = -1;

        int _LocalDrivingLicenseApplicationID = -1;

        clsNewLocalDrivingLicenseApplicationBusiness _ClsAdd;
        enum enMode { AddNew = 1, Update = 2 }

        enMode _Mode;


        public frmNewLocalDrivingLicenseApplication(string UserName)
        {
            InitializeComponent();

            this._UserName = UserName;
            _Mode = enMode.AddNew;
        }

        public frmNewLocalDrivingLicenseApplication(string UserName, int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            this._UserName = UserName;
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            _Mode = enMode.Update;
        }

        private void ctrlShowPersonDetails1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadAllComboBox()
        {
            List<string> dataList = clsNewLocalDrivingLicenseApplicationBusiness.LoadAllApplicationTypes();

            if (dataList != null)
            {
                foreach(string item in dataList)
                {
                    cbLicenseClass.Items.Add(item);
                }

                cbLicenseClass.SelectedIndex = 2;

            }

            
        }

        void LoadDefaultData()
        {
            string _FullDate = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";

            lblApplicationDate.Text = _FullDate;
            lblCreatedBy.Text = _UserName;
            lblApplicationFees.Text = _Fees.ToString();

            LoadAllComboBox();

        }

        private void UploadAllData(clsNewLocalDrivingLicenseApplicationBusiness Data)
        {
            //_ClsUpdate = Data;

            lblAddNewEditUser.Text = "Update Local Driving License Application";

            ctrlFilterAndMakePersonInfo1._NationalNo = Data.NationalNo;

            ctrlFilterAndMakePersonInfo1._PersonID = Data.PersonID;
            ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

            ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
            ctrlFilterAndMakePersonInfo1.textBoxData(Data.PersonID.ToString());
            ctrlFilterAndMakePersonInfo1.DisabledFilterBy();


            lblAppliactionID.Text = Data.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = $"{Data.ApplicationDate.Day}/{Data.ApplicationDate.Month}/{Data.ApplicationDate.Year}";
            cbLicenseClass.SelectedIndex = Data.LicenseClassID -1;
            lblApplicationFees.Text = _Fees.ToString();
            lblCreatedBy.Text = clsMethodsGeneralBusiness.UserNameByUserID(Data.UserID);

            btnSave.Enabled = true;

            _Mode = enMode.Update;
        }

        private void frmNewLocalDrivingLicenseApplication_Load()
        {

            if (_LocalDrivingLicenseApplicationID != -1 || _LocalDrivingLicenseApplicationID == 0)
            {

                clsNewLocalDrivingLicenseApplicationBusiness Data = clsNewLocalDrivingLicenseApplicationBusiness.LoadAllDataUserByLocalDriveLicAppID(_LocalDrivingLicenseApplicationID);
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

                clsNewLocalDrivingLicenseApplicationBusiness Data = clsNewLocalDrivingLicenseApplicationBusiness.LoadAllDataUserByLocalDriveLicAppID(_LocalDrivingLicenseApplicationID);
                UploadAllData(Data);

            }
        
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
                string nationalNo = ctrlFilterAndMakePersonInfo1._NationalNo;

                bool personExistsByNationalNo = clsPeople.SearchData(clsPeople.PeopleColumn.NationalNo, nationalNo, clsPeople.SearchMode.ExactMatch).Rows.Count > 0;


                if (personExistsByNationalNo)
                {
                    tabControl1.SelectedTab = tabPage2; 
                }

            }
            else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
            {
                string personID = ctrlFilterAndMakePersonInfo1._PersonID?.ToString();

                bool userExistsByPersonID = clsUsers.SearchData(clsUsers.UsersColumn.PersonID, personID, clsUsers.SearchMode.ExactMatch).Rows.Count > 0;


                if (userExistsByPersonID)
                {
                    tabControl1.SelectedTab = tabPage2;
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


            /*
            if (clsAddNewEditUserBusiness.IsFoundPerson(ctrlFilterAndMakePersonInfo1.GetFilterByName(), ctrlFilterAndMakePersonInfo1.GetData()))
            {
                

                    if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                    {
                        tabControl1.SelectedTab = tabPage2;
                        _Next = true;
                    }
                    else
                    {
                        tabControl1.SelectedTab = tabPage2;
                        _Next = true;
                    }
            }
            else
                MessageBox.Show("Enter True People", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            */

        }

        //Test Debugging
        private void btnSave_Click(object sender, EventArgs e)
        {
             UserID = clsMethodsGeneralBusiness.UserIDByUserName(_UserName);

            //Test Debugging
            int LicenseClassID = cbLicenseClass.SelectedIndex + 1;

            if (_LocalDrivingLicenseApplicationID == -1 || _LocalDrivingLicenseApplicationID == 0)
            {
                if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                {
                    if (clsNewLocalDrivingLicenseApplicationBusiness.IsFoundApplicationMatchLocalDriveByNationalNo(
                        ctrlFilterAndMakePersonInfo1._NationalNo, LicenseClassID))
                    {
                        MessageBox.Show(@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = ??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _ClsAdd = new clsNewLocalDrivingLicenseApplicationBusiness(ctrlFilterAndMakePersonInfo1._NationalNo, UserID, LicenseClassID);

                }
                else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
                {

                    if (clsNewLocalDrivingLicenseApplicationBusiness.IsFoundApplicationMatchLocalDriveByPersonID(
                        (int)ctrlFilterAndMakePersonInfo1._PersonID, LicenseClassID))
                    {
                        MessageBox.Show(@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = ??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _ClsAdd = new clsNewLocalDrivingLicenseApplicationBusiness((int)ctrlFilterAndMakePersonInfo1._PersonID, UserID, LicenseClassID);
                }

                ctrlFilterAndMakePersonInfo1._PersonID = _ClsAdd.PersonID;
                ctrlFilterAndMakePersonInfo1._NationalNo = _ClsAdd.NationalNo;

            }
            else
            {

                if (ctrlFilterAndMakePersonInfo1._NationalNo != "")
                {
                    if (clsNewLocalDrivingLicenseApplicationBusiness.IsFoundApplicationMatchLocalDriveByNationalNo(
                        ctrlFilterAndMakePersonInfo1._NationalNo, LicenseClassID))
                    {
                        MessageBox.Show(@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = ??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ctrlFilterAndMakePersonInfo1._PersonID != -1)
                {
                    if (clsNewLocalDrivingLicenseApplicationBusiness.IsFoundApplicationMatchLocalDriveByPersonID(
                        (int)ctrlFilterAndMakePersonInfo1._PersonID, LicenseClassID))
                    {
                        MessageBox.Show(@"Choose another License Class, the selected Person Already have
                                        an active application for the selected Class with id = ??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                _ClsAdd = new clsNewLocalDrivingLicenseApplicationBusiness(_LocalDrivingLicenseApplicationID, LicenseClassID);

                
            
            }

            

            if (_ClsAdd.Save())
            {

                MessageBox.Show("User Add/Update Successfully :-)", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LocalDrivingLicenseApplicationID = _ClsAdd.LocalDrivingLicenseApplicationID;
                _Mode = enMode.Update;
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
