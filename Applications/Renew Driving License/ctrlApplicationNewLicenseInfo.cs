using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using DVLDProject.Global_Classes;
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
    public partial class ctrlApplicationNewLicenseInfo : UserControl
    {
        public int LicenseID = -1;
        public int RLApplicationID = -1;
        public int RenewedLicenseID = -1;

        public ctrlApplicationNewLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadDataVariable()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));

            // Defensive: Application Fees
            var appType = clsApplicationTypes.FindByApplicationTypeID(2);
            lblApplicationFees.Text = appType != null ? ((int)appType.ApplicationFees).ToString() : "0";

            // Defensive: Created By
            lblCreatedBy.Text = clsGlobal.CurrenntUser != null ? clsGlobal.CurrenntUser.UserName : "[Unknown]";

            lblRLApplicationID.Text = "[???]";
            lblLicenseFees.Text = "[$$$]";
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = "???";
            lblTotalFees.Text = "[???]";
            tbNotes.Text = "";

            // Defensive: License Data
            var licenseData = clsLicenses.FindByLicenseID(LicenseID);
            if (licenseData != null)
            {
                lblOldLicenseID.Text = LicenseID.ToString();

                string licenseFees = "";
                string expirationDate = "";

                clsApplicationNewLicenseInfoBusiness.LoadDataByLicenseID(LicenseID, ref licenseFees, ref expirationDate);

                lblLicenseFees.Text = ((int)licenseData.PaidFees).ToString();
                lblExpirationDate.Text = licenseData.ExpirationDate != null
                    ? ((DateTime)licenseData.ExpirationDate).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture)
                    : "???";

                // Defensive: Total Fees
                int applicationFees = 0;
                int.TryParse(lblApplicationFees.Text, out applicationFees);

                int licenseFeesInt = 0;
                int.TryParse(lblLicenseFees.Text, out licenseFeesInt);

                lblTotalFees.Text = (applicationFees + licenseFeesInt).ToString();
            }

            if (RLApplicationID != -1)
                lblRLApplicationID.Text = RLApplicationID.ToString();

            if (RenewedLicenseID != -1)
            {
                string licenseFees = "";
                string expirationDate = "";

                clsApplicationNewLicenseInfoBusiness.LoadDataByLicenseID(RenewedLicenseID, ref licenseFees, ref expirationDate);

                lblLicenseFees.Text = !string.IsNullOrEmpty(licenseFees) ? licenseFees : "[$$$]";
                lblExpirationDate.Text = !string.IsNullOrEmpty(expirationDate) ? expirationDate : "???";
                lblRenewedLicenseID.Text = RenewedLicenseID.ToString();
            }
        }

        private void ctrlApplicationNewLicenseInfo_Load(object sender, EventArgs e)
        {
            LoadDataVariable();
        }

        public void ctrlApplicationNewLicenseInfo_Load()
        {
            LoadDataVariable();
        }

        public string NotesText()
        {
            return tbNotes.Text;
        }

        private void gbAppBasicInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
