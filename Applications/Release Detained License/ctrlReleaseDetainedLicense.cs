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
    public partial class ctrlReleaseDetainedLicense : UserControl
    {
        public int DetainID = -1;
        public int LicenseID = -1;
        public int ApplicationID = -1;
        public string UserName = "";

        public ctrlReleaseDetainedLicense()
        {
            InitializeComponent();

            LoadAllData();
        }

        public void LoadAllData()
        {
            // MessageBox.Show(DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US")));
            lblDetainID.Text = lblDetainID.Tag.ToString();
            lblDetainDate.Text = lblDetainID.Tag.ToString();
            lblApplicationFees.Text = lblApplicationFees.Tag.ToString();
            lblTotalFees.Text = lblTotalFees.Tag.ToString();
            lblLicenseID.Text = lblLicenseID.Tag.ToString();
            lblCreatedBy.Text = lblCreatedBy.Tag.ToString();
            lblFineFees.Text = lblFineFees.Tag.ToString();
            lblApplicationID.Text = lblApplicationID.Tag.ToString();


            if (clsMethodsGeneralBusiness.IsDetainedIDFound(DetainID))
            {
                string DetainDate = "";
                int FineFees = -1;
                int CreatedByID = -1;

                clsReleaseDetainedLicenseBusiness.RenewLocalDrivingLicense(DetainID, ref DetainDate,
                                                                ref FineFees, ref CreatedByID);

                lblDetainID.Text = DetainID.ToString();
                lblDetainDate.Text = DetainDate;
                lblFineFees.Text = FineFees.ToString();
                lblCreatedBy.Text = clsMethodsGeneralBusiness.UserNameByUserID(CreatedByID);

                lblApplicationFees.Text = Convert.ToString(clsMethodsGeneralBusiness.FeesReleaseDetainedDrivingLicense());

                if (Convert.ToInt16(lblApplicationFees.Text) > 0 
                    &&
                    Convert.ToInt16(lblFineFees.Text) > 0)
                {
                    lblTotalFees.Text = (Convert.ToInt16(lblApplicationFees.Text) + Convert.ToInt16(lblFineFees.Text)).ToString();
                }
            }

            // Load Data Variable


            if (LicenseID != -1)
                lblLicenseID.Text = LicenseID.ToString();

            if (ApplicationID != -1)
            {
                lblApplicationID.Text = ApplicationID.ToString();
                lblCreatedBy.Text = UserName;
            }

        }

        private void ctrlReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }
    }
}
