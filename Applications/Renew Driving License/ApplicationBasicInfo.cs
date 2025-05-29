using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDProject
{
    public partial class ApplicationBasicInfo : UserControl
    {
        public int _LDLAppID = -1;

        public ApplicationBasicInfo()
        {
            InitializeComponent();
        }
        private void LoadAllData()
        {
            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LDLAppID) != null)
            {

                clsLocalDrivingLicenseApplicationsBusiness clsA = clsLocalDrivingLicenseApplicationsBusiness.UploadAllDataByABI_LDLAppID(_LDLAppID);

                lblID.Text = clsA.ABI_ID.ToString();
                lblStatus.Text = clsA.ABI_Status.ToString();
                lblFees.Text = clsA.ABI_Fees.ToString();
                lblType.Text = clsA.ABI_Type.ToString();
                lblApplicant.Text = clsA.ABI_Applicant.ToString();
                lblDate.Text = clsA.ABI_Date.ToString();
                lblStatusDate.Text = clsA.ABI_StatusDate.ToString();
                lblCreatedBy.Text = clsA.ABI_CreatedBy.ToString();

            }
        }

        private void ApplicationBasicInfo_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        public void ApplicationBasicInfo_Load()
        {
            LoadAllData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string NationalNo = clsMethodsGeneralBusiness.NationalNoByLDLAppID(_LDLAppID);


            frmPersonDetails Frm = new frmPersonDetails(NationalNo);
            Frm.ShowDialog();

            LoadAllData();

        }

    }
}
