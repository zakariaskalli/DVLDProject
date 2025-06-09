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

        public void LoadDataVariable()
        {
            // Define formatting once
            const string dateFormat = "dd/MMM/yyyy";
            var culture = new CultureInfo("en-US");

            string currentDate = DateTime.Now.ToString(dateFormat, culture);
            string nextYearDate = DateTime.Now.AddYears(1).ToString(dateFormat, culture);

            lblApplicationDate.Text = currentDate;
            lblIssueDate.Text = currentDate;
            lblExpirationDate.Text = nextYearDate;

            lblCreatedBy.Text = clsGlobal.CurrenntUser.UserName;

            SetLabelIfValid(lblILApplicationID, ILApplicationID);
            SetLabelIfValid(lblILLicenseID, ILLicenseID);
            SetLabelIfValid(lblLocalLicenseID, LocalLicenseID);

            // Optionally add back if needed
            // lblFees.Text = clsApplicationTypes.FindByApplicationTypeID(6).ApplicationFees.ToString();
            // lblFees.Text = clsMethodsGeneralBusiness.FeesNewInternationalLicenseApplication().ToString();
        }

        private void SetLabelIfValid(Label label, int value)
        {
            label.Text = value != -1 ? value.ToString() : "[???]";
        }


        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {
            LoadDataVariable();
        }

        private void gbAppBasicInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
