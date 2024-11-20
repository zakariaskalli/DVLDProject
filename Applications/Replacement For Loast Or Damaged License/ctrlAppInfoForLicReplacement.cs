using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class ctrlAppInfoForLicReplacement : UserControl
    {
        public int LRApplicationID = -1;
        public int ReplacedLicenseID = -1;
        public int OldLicenseID = -1;
        public bool ReplacementType = false;

        public ctrlAppInfoForLicReplacement()
        {
            InitializeComponent();
            LoadAllData();
        }

        public void LoadAllData()
        {
            // MessageBox.Show(DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US")));


            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            
            if (ReplacementType == false)
            {
                lblApplicationFees.Text = clsMethodsGeneralBusiness.FeesReplacementForDamagedDrivingLicensee().ToString();
            }
            else
            {
                lblApplicationFees.Text = clsMethodsGeneralBusiness.FeesReplacementForLostDrivingLicense().ToString();
            }
            lblCreatedBy.Text = Program._UserName;

            // Load Data Variable
            lblLRApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";

            if (LRApplicationID != -1)
                lblLRApplicationID.Text = LRApplicationID.ToString();

            if (ReplacedLicenseID != -1)
                lblReplacedLicenseID.Text = ReplacedLicenseID.ToString();


            if (OldLicenseID != -1)
                lblOldLicenseID.Text = OldLicenseID.ToString();
        }


        private void ctrlAppInfoForLicReplacement_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void gbAppBasicInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
