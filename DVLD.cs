using Business_Layer___DVLDProject;
using DVLDProject.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    //Sdk="Microsoft.NET.Sdk"

    public partial class DVLD : Form
    {

        public delegate void ReloadNewOrNot(bool YesOrNo);
        public event ReloadNewOrNot NewOrNot;

        void GeneralStyleForAllForms(Form frm)
        {
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        public DVLD()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeople frmPeople = new frmPeople();
            GeneralStyleForAllForms(frmPeople);
            frmPeople.ShowDialog();
        }


        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewOrNot?.Invoke(true);
            this.Close();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrenntUser.UserName);
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers Form = new frmManageUsers();
            GeneralStyleForAllForms(Form);
            Form.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword Frm = new frmChangePassword(clsGlobal.CurrenntUser.UserName);
            GeneralStyleForAllForms(Frm);
            Frm.ShowDialog();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseAppliactions frm = new frmLocalDrivingLicenseAppliactions();
            GeneralStyleForAllForms(frm); 
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDriver frm = new frmManageDriver();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void internationationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplications frm = new frmInternationalLicenseApplications();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frm = new frmRenewLocalDrivingLicense();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedOrLostLicenses frm = new frmReplacementForDamagedOrLostLicenses();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            GeneralStyleForAllForms(frm);
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseAppliactions frm = new frmLocalDrivingLicenseAppliactions();
            GeneralStyleForAllForms(frm); 
            frm.ShowDialog();
        }

    }
}
