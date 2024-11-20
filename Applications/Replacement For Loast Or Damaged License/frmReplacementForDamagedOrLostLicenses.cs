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

namespace DVLDProject
{
    public partial class frmReplacementForDamagedOrLostLicenses : Form
    {
        int _NewLicenseID = -1;

        public enum enChose
        {
            enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2, enReplacementForLostLicense = 3, enReplacementForDamagedLicense = 4
        }


        public frmReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

            lnkLbl1.Enabled = false;
            lnkLbl2.Enabled = false;

            btnIssueReplacement.Enabled = false;

            if (rbDamagedLicense.Checked == true)
            {
                ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReplacementForDamagedLicense);
            }
            else
            {
                ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReplacementForLostLicense);
            }

            gbReplacementFor.Enabled = true;
        }


        void ChangedChangedComboBox()
        {
            if (rbDamagedLicense.Checked == true)
            {
                lblTextTitle.Text = "Replacement For Damaged License";
                this.Text = "Replacement For Damaged License";

                ctrlAppInfoForLicReplacement1.ReplacementType = false;
                ctrlAppInfoForLicReplacement1.LoadAllData();

            }
            else
            {
                lblTextTitle.Text = "Replacement For Lost License";
                this.Text = "Replacement For Lost License";

                ctrlAppInfoForLicReplacement1.ReplacementType = true;
                ctrlAppInfoForLicReplacement1.LoadAllData();
            }
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            ChangedChangedComboBox();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReplacementForDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked == true)
            {
                ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReplacementForDamagedLicense);
            }
            else
            {
                ctrlFilterDriverLicenseInfo1.ChoseTypeFilter((int)enChose.enReplacementForLostLicense);
            }
        }

        private void ctrlFilterDriverLicenseInfo1__OnClickToSearch(int arg1, bool arg2)
        {

            ctrlAppInfoForLicReplacement1.OldLicenseID = arg1;
            ctrlAppInfoForLicReplacement1.LRApplicationID = -1;
            ctrlAppInfoForLicReplacement1.ReplacedLicenseID = -1;

            ctrlAppInfoForLicReplacement1.LoadAllData();


            ChangedChangedComboBox();




            if (arg2 == false)
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                gbReplacementFor.Enabled = true;

                btnIssueReplacement.Enabled = false;
            }
            else
            {
                lnkLbl1.Enabled = true;
                lnkLbl2.Enabled = false;

                gbReplacementFor.Enabled = true;


                btnIssueReplacement.Enabled = true;
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            ChangedChangedComboBox();
        }

        private void lnkLbl1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsMethodsGeneralBusiness.IsFoundLicenseByLicenseID(ctrlAppInfoForLicReplacement1.OldLicenseID))
            {
                //I Find LDLAppID
                int LDLAppID = clsMethodsGeneralBusiness.LDLAppIDByLicenseID(ctrlAppInfoForLicReplacement1.OldLicenseID);

                frmLicenseHistory frm = new frmLicenseHistory(LDLAppID);
                frm.ShowDialog();

            }
        }

        private void lnkLbl2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            _NewLicenseID = ctrlAppInfoForLicReplacement1.ReplacedLicenseID;

            if (clsMethodsGeneralBusiness.IsFoundLicenseByLicenseID(_NewLicenseID))
            {
                Form frm = new frmDriverLicenseInfo(clsMethodsGeneralBusiness.LDLAppIDByLicenseID(_NewLicenseID));
                //frm._InternationalID = _InternationalID;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to Issue to Replacement of the License?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LicenseID = ctrlAppInfoForLicReplacement1.OldLicenseID;

            string UserName = Program._UserName;
            //string UserName = "user4";

            int LRApplicationID = -1;
            int ReplacedLicenseID = -1;


            if (rbDamagedLicense.Checked == true)
            {
                clsReplacementForDamagedOrLostLicensesBusiness.ReplacementLicenseForDamaged(LicenseID, UserName, ref LRApplicationID, ref ReplacedLicenseID);
            }
            else
            {
                clsReplacementForDamagedOrLostLicensesBusiness.ReplacementLicenseForLost(LicenseID, UserName, ref LRApplicationID, ref ReplacedLicenseID);
            }


            if (LRApplicationID != -1 && ReplacedLicenseID != -1)
            {
                ctrlAppInfoForLicReplacement1.LRApplicationID = LRApplicationID;
                ctrlAppInfoForLicReplacement1.ReplacedLicenseID = ReplacedLicenseID;

                ctrlAppInfoForLicReplacement1.LoadAllData();
                
                lnkLbl2.Enabled = true;
                btnIssueReplacement.Enabled = false;

                gbReplacementFor.Enabled = false;

                MessageBox.Show($"License Replaced Successfully with ID ={ReplacedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error you can't Replaced License data Bug", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
