using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer___DVLDProject;

namespace DVLDProject
{
    public partial class ctrlShowPersonDetails : UserControl
    {
        public int _PersonID = -1;
        public string _NationalNo = "";

        static private string _ImageMalePath = @"C:/Programation Level 2/DVLDProject/Project Image/homme.png";
        static private string _ImageFemalePath = @"C:/Programation Level 2/DVLDProject/Project Image/medecin.png";

        public ctrlShowPersonDetails()
        {
            InitializeComponent();

            //_PersonID = PersonID;

        }

        private void LoadAllDataByPersonID()
        {
            if (clsAddEditPersonInfoBusiness.PersonIDIsFound(_PersonID))
            {

                clsAddEditPersonInfoBusiness clsA = clsAddEditPersonInfoBusiness.UploadAllDataByPersonID(_PersonID);

                _NationalNo = clsA.NationalNo; 
                lblPersonID.Text = clsA.PersonID.ToString();
                lblName.Text = clsA.FirstName.ToString() + " " + clsA.SecondName.ToString()
                    + " " + clsA.ThirdName.ToString() + " " + clsA.LastName.ToString();
                lblNationalNo.Text = clsA.NationalNo.ToString();

                if (clsA.Gendor == 0)
                    lblGendor.Text = "Male";
                else
                    lblGendor.Text = "Female";

                lblEmail.Text = clsA.Email.ToString();
                lblAddress.Text = clsA.Address.ToString();
                lblDateOfBirth.Text = clsA.DateOfBirth.ToString();
                lblPhone.Text = clsA.Phone.ToString();

                string CountryName = "";
                CountryName = clsMethodsGeneralBusiness.NameCountryByNumber(clsA.NationalityCountryID);
                if (CountryName != "")
                    lblCountry.Text = CountryName;
                else
                    lblCountry.Text = "None";

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

        private void LoadAllDataByNationalNo()
        {
            if (clsAddEditPersonInfoBusiness.NationalNoIsAvailable(_NationalNo))
            {

                clsAddEditPersonInfoBusiness clsA = clsAddEditPersonInfoBusiness.UploadAllDataByNationalNo(_NationalNo);

                _NationalNo = clsA.NationalNo;
                lblPersonID.Text = clsA.PersonID.ToString();
                lblName.Text = clsA.FirstName.ToString() + " " + clsA.SecondName.ToString()
                    + " " + clsA.ThirdName.ToString() + " " + clsA.LastName.ToString();
                lblNationalNo.Text = clsA.NationalNo.ToString();

                if (clsA.Gendor == 0)
                    lblGendor.Text = "Male";
                else
                    lblGendor.Text = "Female";

                lblEmail.Text = clsA.Email.ToString();
                lblAddress.Text = clsA.Address.ToString();
                lblDateOfBirth.Text = clsA.DateOfBirth.ToString();
                lblPhone.Text = clsA.Phone.ToString();

                string CountryName = "";
                CountryName = clsMethodsGeneralBusiness.NameCountryByNumber(clsA.NationalityCountryID);
                if (CountryName != "")
                    lblCountry.Text = CountryName;
                else
                    lblCountry.Text = "None";

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

        public void ctrlShowPersonDetails_Load()
        {
            if (_PersonID != -1 && _PersonID != 0)
                LoadAllDataByPersonID();
            else if (_NationalNo != "")
                LoadAllDataByNationalNo();

        } 

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lnkLblEditPeronInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != -1)
            {
                frmAddEditPersonInfo Frm = new frmAddEditPersonInfo(_PersonID);
                Frm.ShowDialog();
            }
            else if (_NationalNo != "")
            {
                frmAddEditPersonInfo Frm = new frmAddEditPersonInfo(_NationalNo);
                Frm.ShowDialog();
            }


            if (_PersonID != -1)
                LoadAllDataByPersonID();
            else if (_NationalNo != "")
                LoadAllDataByNationalNo();

        }
    }
}
