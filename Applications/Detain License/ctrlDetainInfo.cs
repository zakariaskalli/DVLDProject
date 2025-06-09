using Business_Layer___DVLDProject;
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
    public partial class ctrlDetainInfo : UserControl
    {
        public int DetainID = -1;
        public int LicenseID = -1;

        public ctrlDetainInfo()
        {
            InitializeComponent();

            LoadAllData();
        }

        public void LoadAllData()
        {
            // MessageBox.Show(DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US")));


            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy", new CultureInfo("en-US"));

            lblCreatedBy.Text = clsGlobal.CurrenntUser.UserName;

            // Load Data Variable
            lblDetainID.Text = "[???]";
            lblLicenseID.Text = "[???]";

            if (DetainID != -1)
                lblDetainID.Text = DetainID.ToString();

            if (LicenseID != -1)
                lblLicenseID.Text = LicenseID.ToString();

        }

        public int FineFees()
        {
            int fineFees;

            // Try to parse the text as an integer
            if (int.TryParse(tbFineFees.Text, out fineFees))
                return fineFees; // Return the parsed integer if successful
            else
                return -1; // Return -1 if parsing fails (invalid number)

        }

        public void FineFeesFocus()
        {
            tbFineFees.Focus();
        }

        public void DisabledFineFeesTextBox()
        {
            tbFineFees.Enabled = false;
        }

        public void EnabledFineFeesTextBox()
        {
            tbFineFees.Enabled = true;
        }

        private void ctrlDetainInfo_Load(object sender, EventArgs e)
        {

        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public bool btnClose(bool YesOrNo = false)
       {
            return YesOrNo;
            
       }

        private void tbFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (btnClose())
            {
                e.Cancel = false;
                return;
            }

            if (tbFineFees.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFineFees, "Enter A The Fine Fees.");
            }
            else
                errorProvider1.SetError(tbFineFees, string.Empty);

        }

    }
}
