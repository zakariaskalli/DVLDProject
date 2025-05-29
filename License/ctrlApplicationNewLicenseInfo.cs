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
        public int RLApplicationID= -1;
        public int RenewedLicenseID= -1;

        public ctrlApplicationNewLicenseInfo()
        {
            InitializeComponent();
            LoadAutoInfo();
        }


        void LoadAutoInfo()
        {

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            //FeesRenewDrivingLicenseService
            lblApplicationFees.Text = clsApplicationTypes.FindByApplicationTypeID(2).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrenntUser.UserName;

        }

        public void LoadDataVariable()
        {
            lblRLApplicationID.Text = "[???]";
            lblLicenseFees.Text = "[$$$]";
            
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = "???";
            lblTotalFees.Text = "[???]";
            tbNotes.Text = "";


            if (clsLicenses.FindByLicenseID((int)LicenseID) != null)
            {
                lblOldLicenseID.Text = LicenseID.ToString();


                clsLicenses LicenseData = clsLicenses.FindByLicenseID(LicenseID);
             

                string LicenseFees = "";
                string ExpirationDate = "";

                clsApplicationNewLicenseInfoBusiness.LoadDataByLicenseID(LicenseID,ref LicenseFees, ref ExpirationDate);

                lblLicenseFees.Text = ((int)LicenseData.PaidFees).ToString();
                lblExpirationDate.Text = ((DateTime)LicenseData.ExpirationDate).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                // Conclusion Data


                // to completed
                lblTotalFees.Text = (Convert.ToInt16(lblApplicationFees.Text) + Convert.ToInt16(lblLicenseFees.Text)).ToString();

            }
                

            if (RLApplicationID != -1)
                lblRLApplicationID.Text = RLApplicationID.ToString();


            if (RenewedLicenseID != -1)
            {
                string LicenseFees = "";
                string ExpirationDate = "";

                clsApplicationNewLicenseInfoBusiness.LoadDataByLicenseID(RenewedLicenseID, ref LicenseFees, ref ExpirationDate);

                lblLicenseFees.Text = LicenseFees;
                lblExpirationDate.Text = ExpirationDate;

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
