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
using System.Globalization;
using DVLDProject.Global_Classes;
using DVLD_BusinessLayer;

namespace DVLDProject
{
    public partial class ctrlApplicationInfo : UserControl
    {
        public int ILApplicationID = -1;
        public int ILLicenseID = -1;
        public int LocalLicenseID = -1;



        public ctrlApplicationInfo()
        {
            InitializeComponent();

            //LoadAutoInfo();
        }

        void LoadAutoInfo()
        {
            // MessageBox.Show(DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US")));


            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));

            // FeesNewInternationalLicenseApplication
            //lblFees.Text = clsApplicationTypes.FindByApplicationTypeID(6).ApplicationFees.ToString();

            //lblFees.Text = clsMethodsGeneralBusiness.FeesNewInternationalLicenseApplication().ToString();

            DateTime nextYearDate = DateTime.Now.AddYears(1);
            string formattedDate = nextYearDate.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));
            lblExpirationDate.Text = formattedDate;

            lblCreatedBy.Text = clsGlobal.CurrenntUser.UserName;

        }

        public void LoadDataVariable()
        {
            lblILApplicationID.Text = "[???]";
            lblILLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";

            if (ILApplicationID != -1)
                lblILApplicationID.Text = ILApplicationID.ToString();

            if (ILLicenseID != -1)
                lblILLicenseID.Text = ILLicenseID.ToString();


            if (LocalLicenseID != -1)
                lblLocalLicenseID.Text = LocalLicenseID.ToString();
        }

        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {
            LoadAutoInfo();
            LoadDataVariable();
        }

    }
}
