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

namespace DVLDProject
{
    public partial class frmDetainLicense : Form
    {

        public enum enChose
        {
            enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2,
            enReplacementForLostLicense = 3, enReplacementForDamagedLicense = 4,
            enDetain = 5, enReleaseDetainedLicense = 6
        }

        public frmDetainLicense()
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

            lnkLbl1.Enabled = false;
            lnkLbl2.Enabled = false;

            btnDetain.Enabled = false;

            ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enDetain);

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            ctrlDetainInfo1.btnClose(true);

            this.Close();
        }

        private void ctrlFilterDriverLicenseInfo1__OnClickToSearch(int arg1, bool arg2)
        {

            ctrlDetainInfo1.LicenseID = arg1;
            ctrlDetainInfo1.DetainID = -1;
            ctrlDetainInfo1.LoadAllData();


            if (arg2 == false)
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;
                ctrlDetainInfo1.DisabledFineFeesTextBox();
                btnDetain.Enabled = false;
            }
            else
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnDetain.Enabled = true;
            }
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {

        }

        private void lnkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlDetainInfo1.LicenseID) != null)
            {
                int DriverID = (int)clsLicenses.FindByLicenseID(ctrlDetainInfo1.LicenseID).DriverID;

                frmLicenseHistory frm = new frmLicenseHistory(DriverID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkLbl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlDetainInfo1.LicenseID) != null)
            {
                Form frm = new frmDriverLicenseInfo(ctrlDetainInfo1.LicenseID);
                //frm._InternationalID = _InternationalID;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (ctrlDetainInfo1.FineFees() == -1)
            {
                MessageBox.Show("Enter A Fine Fees", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlDetainInfo1.FineFeesFocus();
                    return;
            }

            if (MessageBox.Show("Confirm", "Are you sure do want to Detain the License?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LicenseID = ctrlDetainInfo1.LicenseID;

            string UserName = clsGlobal.CurrenntUser.UserName;
            //string UserName = "user4";
            int Fees = ctrlDetainInfo1.FineFees();

            int DetainedID = -1;

            clsDetainLicenseBusiness.DetainLicense(LicenseID, UserName, Fees, ref DetainedID);

            if (DetainedID != -1 )
            {
                ctrlDetainInfo1.DetainID = DetainedID;
                ctrlDetainInfo1.LoadAllData();
                ctrlDetainInfo1.DisabledFineFeesTextBox();
                ctrlFilterDriverLicenseInfo1.FilterDisabled();
                lnkLbl2.Enabled = true;
                btnDetain.Enabled = false;

                MessageBox.Show($"License Detained Successfully with ID ={DetainedID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error you can't Detained License data Bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
