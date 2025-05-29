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
    public partial class frmReleaseDetainedLicense : Form
    {
        public enum enChose
        {
            enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2,
            enReplacementForLostLicense = 3, enReplacementForDamagedLicense = 4,
            enDetain = 5, enReleaseDetainedLicense = 6
        }

         readonly int _LicenseID = -1;


        public frmReleaseDetainedLicense()
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

            lnkLbl1.Enabled = false;
            lnkLbl2.Enabled = false;

            btnRelease.Enabled = false;

            ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReleaseDetainedLicense);

        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

            lnkLbl1.Enabled = true;
            lnkLbl2.Enabled = false;

             btnRelease.Enabled = true;
            _LicenseID = LicenseID;
            //ctrlFilterDriverLicenseInfo1__OnClickToSearch(LicenseID, true);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlFilterDriverLicenseInfo1__OnClickToSearch(int arg1, bool arg2)
        {
            ctrlReleaseDetainedLicense1.LicenseID = arg1;
            ctrlReleaseDetainedLicense1.DetainID = clsMethodsGeneralBusiness.DetainedIDByLicenseID(arg1);
            ctrlReleaseDetainedLicense1.ApplicationID = -1;
            ctrlReleaseDetainedLicense1.LoadAllData();


            if (arg2 == true)
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnRelease.Enabled = true;
            }
            else
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnRelease.Enabled = false;
            }
        }

        private void ctrlReleaseDetainedLicense1_Load(object sender, EventArgs e)
        {

        }

        private void lnkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlReleaseDetainedLicense1.LicenseID) != null)
            {
                //I Find LDLAppID
                int LDLAppID = clsMethodsGeneralBusiness.LDLAppIDByLicenseID(ctrlReleaseDetainedLicense1.LicenseID);

                frmLicenseHistory frm = new frmLicenseHistory(LDLAppID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkLbl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlReleaseDetainedLicense1.LicenseID) != null)
            {
                Form frm = new frmDriverLicenseInfo(clsMethodsGeneralBusiness.LDLAppIDByLicenseID(ctrlReleaseDetainedLicense1.LicenseID));
                //frm._InternationalID = _InternationalID;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to Release the License?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LicenseID = ctrlReleaseDetainedLicense1.LicenseID;

            string UserName = clsGlobal.CurrenntUser.UserName;
            //string UserName = "user4";

            int ApplicationID = -1;

            clsReleaseDetainedLicenseBusiness.ReleaseDetainedDrivingLicense(LicenseID, UserName, ref ApplicationID);

            if (ApplicationID != -1)
            {
                ctrlFilterDriverLicenseInfo1.FilterDisabled();
                ctrlReleaseDetainedLicense1.ApplicationID = ApplicationID;
                ctrlReleaseDetainedLicense1.LoadAllData();

                lnkLbl2.Enabled = true;
                btnRelease.Enabled = false;

                MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error you can't Released License data Bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)_LicenseID) != null)
            {
                ctrlFilterDriverLicenseInfo1.FilterDisabled();
                ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReleaseDetainedLicense);
                ctrlFilterDriverLicenseInfo1.LicenseIDLoadData(_LicenseID);
            }


        }
    }
}
