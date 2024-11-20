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
    public partial class DrivingLicenseApplicationInfo : UserControl
    {

        public int _LDLAppID = -1;

        public int TestNum = -1;


        public DrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void LoadAllData()
        {
            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(_LDLAppID))
            {

                clsLocalDrivingLicenseApplicationsBusiness clsA = clsLocalDrivingLicenseApplicationsBusiness.UploadAllDataByDLAppInfo_LDLAppID(_LDLAppID);

                lblDLAppID.Text = clsA.DLAppInfo_LDLAppID.ToString();
                lblAppliedForLicense.Text = clsA.DLAppInfo_LicenseName.ToString();
                lblPassedTests.Text = clsA.DLAppInfo_NumTestsPassed.ToString() + "/" + "3";
                TestNum = clsA.DLAppInfo_NumTestsPassed + 1;

                lblShowLicenseInfo.Enabled = true;
            }
        }

        private void gbDrivingLicenseApplicationInfo_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        public void ctrlDrivingLicenseApplicationInfo_Load()
        {
            LoadAllData();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(_LDLAppID);
            frm.ShowDialog();
        }
    }
}
