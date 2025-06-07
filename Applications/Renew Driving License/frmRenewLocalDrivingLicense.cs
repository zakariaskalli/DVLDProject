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
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _NewLicenseID = -1;


        public enum enChose
        {
            enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2,
            enReplacementForLostLicense = 3, enReplacementForDamagedLicense = 4,
            enDetain = 5
        }

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

            lnkLbl1.Enabled = false;
            lnkLbl2.Enabled = false;

            btnRenew.Enabled = false;

            ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enRenewLocalDrivingLicense);

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlFilterDriverLicenseInfo1__OnClickToSearch(int arg1, bool arg2)
        {

            ctrlApplicationNewLicenseInfo1.LicenseID = arg1;
            ctrlApplicationNewLicenseInfo1.RLApplicationID = -1;
            ctrlApplicationNewLicenseInfo1.RenewedLicenseID = -1;
            ctrlApplicationNewLicenseInfo1.LoadDataVariable();


            if (arg2 == false)
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnRenew.Enabled = false;
            }
            else
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                btnRenew.Enabled = true;
            }

        }

        private void lnkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsLicenses.FindByLicenseID((int)ctrlApplicationNewLicenseInfo1.LicenseID) != null)
            {

                int DriverID = (int)clsLicenses.FindByLicenseID(ctrlApplicationNewLicenseInfo1.LicenseID).DriverID;

                frmLicenseHistory frm = new frmLicenseHistory(DriverID);
                frm.ShowDialog();
            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to Renew the License?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LicenseID = ctrlApplicationNewLicenseInfo1.LicenseID;

            string UserName = clsGlobal.CurrenntUser.UserName;
            //string UserName = "user4";
            string Notes = ctrlApplicationNewLicenseInfo1.NotesText();

            int RenewLicenseApplicationLicenseID = -1;
            int RenewedLicenseID = -1;

            clsRenewLocalDrivingLicenseBusiness.RenewLocalDrivingLicense(LicenseID, UserName, Notes, ref RenewLicenseApplicationLicenseID, ref RenewedLicenseID);

            if (RenewLicenseApplicationLicenseID != -1 && RenewedLicenseID != -1)
            {
                ctrlApplicationNewLicenseInfo1.RLApplicationID = RenewLicenseApplicationLicenseID;
                ctrlApplicationNewLicenseInfo1.RenewedLicenseID = RenewedLicenseID;
                ctrlApplicationNewLicenseInfo1.LoadDataVariable();

                lnkLbl2.Enabled = true;
                btnRenew.Enabled = false;

                MessageBox.Show($"License Renewed Successfully with ID ={RenewedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error you can't Renewed License data Bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkLbl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            _NewLicenseID = ctrlApplicationNewLicenseInfo1.RenewedLicenseID;

            if (clsLicenses.FindByLicenseID((int)_NewLicenseID) != null)
            {
                Form frm = new frmDriverLicenseInfo(_NewLicenseID);
                //frm._InternationalID = _InternationalID;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {

        }

        private void ctrlFilterDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
