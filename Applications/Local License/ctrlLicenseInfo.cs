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
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace DVLDProject
{
    public partial class ctrlLicenseInfo : UserControl
    {

        public int _LDLAppID = -1;
        
        
        static private string _ImageMalePath = @"C:/Programation Level 2/DVLDProject/Project Image/homme.png";
        static private string _ImageFemalePath = @"C:/Programation Level 2/DVLDProject/Project Image/medecin.png";
        static private string _ImageAutoPath = @"C:\Programation Level 2\DVLDProject\Project Image\question2.png";

        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        void LoadAllData()
        {

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(_LDLAppID))
            {
                clsLicenseInfoBusiness clsA = clsLicenseInfoBusiness.LoadDataLicenseInfoByPersonID(_LDLAppID);

                lblClass.Text = clsA.Class.ToString();
                lblName.Text = clsA.Name.ToString();
                lblLicenseID.Text = clsA.LicenseID.ToString();
                lblNationalNo.Text = clsA.NationalNo.ToString();
                lblGendor.Text = clsA.Gendor.ToString();
                lblIssueDate.Text = clsA.IssueDate.ToString();
                lblIssueReason.Text = clsA.IssueReason.ToString();

                if (clsA.Notes == "")
                    lblNotes.Text = "???";
                else
                    lblNotes.Text = clsA.Notes.ToString();

                lblIsActive.Text = clsA.IsActive.ToString();
                lblDateOfBirth.Text = clsA.DateOfBirth.ToString();
                lblDriverID.Text = clsA.DriverID.ToString();
                lblExpirationDate.Text = clsA.ExpirationDate.ToString();
                lblIsDetained.Text = clsA.IsDetained.ToString();

                string ImagePath = clsA.ImagePath;
                if (ImagePath == "" || !File.Exists(clsA.ImagePath))
                {
                    if (lblGendor.Text == "Male")
                        PBImage.ImageLocation = _ImageMalePath;
                    else
                        PBImage.ImageLocation = _ImageFemalePath;
                }
                else
                    PBImage.ImageLocation = clsA.ImagePath;


            }
        }


        
        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {
            //LoadAllData();

        }
        public void ctrlLicenseInfo_Load()
        {
            LoadAllData();
        }

        void LoadAutoData()
        {
            lblClass.Text = "???";
            lblName.Text = "???";
            lblLicenseID.Text = "???";
            lblNationalNo.Text = "???";
            lblGendor.Text = "???";

            lblIssueDate.Text = "???";
            lblIssueReason.Text = "???";
            lblNotes.Text = "???";
            lblIsActive.Text = "???";
            lblDateOfBirth.Text = "???";
            lblDriverID.Text = "???";

            lblExpirationDate.Text = "???";
            lblIsDetained.Text = "???";

            PBImage.ImageLocation = _ImageAutoPath;

        }


        public void ctrlLicenseInfo_LoadAutoData()
        {
            LoadAutoData();
        }

        public void ctrlLicenseInfo_Load(int LDLAppID)
        {
            _LDLAppID = LDLAppID;
            LoadAllData();
        }

        private void gbDriverLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
