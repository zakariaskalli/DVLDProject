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
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        int LDLAppID = -1;
        int TestNum = -1;
        string UserName = Program._UserName;



        public frmIssueDriverLicenseForTheFirstTime(int lDLAppID, int TestNum)
        {
            InitializeComponent();

            this.LDLAppID = lDLAppID;
            this.TestNum = TestNum;

            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1._LDLAppID = LDLAppID;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Notes = tbNote.Text;
            int LicenseID = -1;


            if (clsIssueDriverLicenseBusiness.IssueDriverLicenseFirstTime(LDLAppID, UserName, Notes, ref LicenseID))
            {
                MessageBox.Show($"License Issued Successfully with License ID = {LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            { 
                MessageBox.Show($"Error In This Add License, test another time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {

        }
    }
}
