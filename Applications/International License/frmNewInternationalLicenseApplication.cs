using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using DVLDProject.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDProject
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        int _InternationalID = -1;

        public enum enChose { enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2 }

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();


            lnkLbl1.Enabled = false;
            lnkLbl2.Enabled = false;

            btnIssue.Enabled = false;
            ctrlFilterDriverLicenseInfo1.FocusTextBox();
            ctrlFilterDriverLicenseInfo1.Focus();

            // Filter

            ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enNewInternationalLicenseLicense);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlApplicationInfo1.LocalLicenseID) != null)
            {
                //I Find LDLAppID
                int LDLAppID = clsMethodsGeneralBusiness.LDLAppIDByLicenseID(ctrlApplicationInfo1.LocalLicenseID);

                frmLicenseHistory frm = new frmLicenseHistory(LDLAppID);
                frm.ShowDialog();

            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to Issue the License?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string UserName = clsGlobal.CurrenntUser.UserName;
            int LicenseID = ctrlApplicationInfo1.LocalLicenseID;

            int InternationalLicenseID = -1;
            int ApplicationID = -1;

            if (clsNewInternationalLicenseApplicationBusiness.AddNewRecordToTable(LicenseID, UserName, ref InternationalLicenseID,
                                                                                        ref ApplicationID))
            {
                ctrlApplicationInfo1.ILLicenseID = InternationalLicenseID;
                ctrlApplicationInfo1.ILApplicationID = ApplicationID;
                ctrlApplicationInfo1.LoadDataVariable();

                lnkLbl2.Enabled = true;
                btnIssue.Enabled = false;
                ctrlFilterDriverLicenseInfo1.FilterDisabled();

                MessageBox.Show($"International License Issued Successfully with ID ={InternationalLicenseID}", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error you can't Issued License data Bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlFilterDriverLicenseInfo1.Focus();
            ctrlFilterDriverLicenseInfo1.FocusTextBox();

        }

        private void ctrlFilterDriverLicenseInfo1__OnClickToSearch(int arg1, bool arg2)
        {

            ctrlApplicationInfo1.LocalLicenseID = arg1;
            ctrlApplicationInfo1.ILLicenseID = ctrlFilterDriverLicenseInfo1._InternationalID;
            ctrlApplicationInfo1.ILApplicationID = -1;
            ctrlApplicationInfo1.LoadDataVariable();


            if (arg2 == true
                ||
                !clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(ctrlApplicationInfo1.ILLicenseID))
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnIssue.Enabled = true;
            }
            else
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = true;

                btnIssue.Enabled = false;
            }

        }

        private void lnkLbl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _InternationalID = ctrlApplicationInfo1.ILLicenseID;

            if (clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(_InternationalID))
            {
                frmInternationalDriverInfo frm = new frmInternationalDriverInfo(_InternationalID);
                //frm._InternationalID = _InternationalID;
                frm.ShowDialog();
            }
        }

        private void ctrlFilterDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
