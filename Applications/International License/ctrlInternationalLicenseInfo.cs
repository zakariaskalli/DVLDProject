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
using System.IO;

namespace DVLDProject
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public int _InternationalLicenseID = -1;

        static private string _ImageMalePath = @"C:/Programation Level 2/DVLDProject/Project Image/homme.png";
        static private string _ImageFemalePath = @"C:/Programation Level 2/DVLDProject/Project Image/medecin.png";
        //static private string _ImageAutoPath = @"C:\Programation Level 2\DVLDProject\Project Image\question2.png";


        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

        }

        void LoadAllData()
        {

            if (clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(_InternationalLicenseID))
            {
                clsInternationalLicenseInfoBusiness clsA = clsInternationalLicenseInfoBusiness.LoadDataInternationalLicenseInfoByIntLicenseID(_InternationalLicenseID);

                lblName.Text = clsA.Name.ToString();
                lblIntLicenseID.Text = clsA.InternationalLicenseID.ToString();
                lblLicenseID.Text = clsA.LicenseID.ToString();
                lblNationalNo.Text = clsA.NationalNo.ToString();
                lblGendor.Text = clsA.Gendor.ToString();
                lblIssueDate.Text = clsA.IssueDate.ToString();

                lblApplicationID.Text = clsA.ApplicationID.ToString();
                lblIsActive.Text = clsA.IsActive.ToString();
                lblDateOfBirth.Text = clsA.DateOfBirth.ToString();
                lblDriverID.Text = clsA.DriverID.ToString();
                lblExpirationDate.Text = clsA.ExpirationDate.ToString();

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



        private void ctrlInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        public void ctrlInternationalLicenseInfo_Load(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            LoadAllData();
        }



        private void gbDriverLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
