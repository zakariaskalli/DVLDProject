using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
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
            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LDLAppID) != null)
            {

                DataTable dt = clsLocalDrivingLicenseApplications.SearchData(clsLocalDrivingLicenseApplications.LocalDrivingLicenseApplicationsColumn.LDLAppID, _LDLAppID.ToString());

                lblDLAppID.Text = dt.Rows[0]["LocalDrivingLicenseApplicationID"].ToString();
                lblAppliedForLicense.Text = dt.Rows[0]["ClassName"].ToString();
                lblPassedTests.Text = dt.Rows[0]["PassedTestCount"].ToString() + "/" + "3";
                TestNum = (int)dt.Rows[0]["PassedTestCount"] + 1;

                lblShowLicenseInfo.Enabled = true;
            }
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
