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
using DVLD_BusinessLayer;

namespace DVLDProject
{
    public partial class ctrlShowPersonDetails : UserControl
    {
        public int? _PersonID = null;
        public string _NationalNo = "";
static private string _ImageMalePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Project Image\homme.png");
static private string _ImageFemalePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Project Image\medecin.png");

        public ctrlShowPersonDetails()
        {
            InitializeComponent();

            //_PersonID = PersonID;

        }

        private void LoadAllDataByPersonID()
        {
            if (clsPeople.FindByPersonID(_PersonID) != null)
            {
                clsPeople PersonInfo = clsPeople.FindByPersonID(_PersonID);

                _NationalNo = PersonInfo.NationalNo; 
                lblPersonID.Text = PersonInfo.PersonID.ToString();

                lblName.Text = PersonInfo.FullName;
                
                lblNationalNo.Text = PersonInfo.NationalNo.ToString();

                if (PersonInfo.Gendor == 0)
                    lblGendor.Text = "Male";
                else
                    lblGendor.Text = "Female";

                lblEmail.Text = PersonInfo.Email.ToString();
                lblAddress.Text = PersonInfo.Address.ToString();
                lblDateOfBirth.Text = PersonInfo.DateOfBirth.ToString();
                lblPhone.Text = PersonInfo.Phone.ToString();


                if (PersonInfo.CountriesInfo.CountryName != "")
                    lblCountry.Text = PersonInfo.CountriesInfo.CountryName;
                else
                    lblCountry.Text = "None";

                string ImagePath = PersonInfo.ImagePath;
                if (ImagePath == "" || !File.Exists(PersonInfo.ImagePath))
                {
                    if (lblGendor.Text == "Male")
                        PBImage.ImageLocation = _ImageMalePath;
                    else
                        PBImage.ImageLocation = _ImageFemalePath;
                }
                else
                    PBImage.ImageLocation = PersonInfo.ImagePath;

            }

        }

        private void LoadAllDataByNationalNo()
        {
            if (clsPeople.SearchData(clsPeople.PeopleColumn.NationalNo, _NationalNo, clsPeople.SearchMode.ExactMatch).Rows.Count != 0)
            {
                clsPeople PersonInfo = clsPeople.FindByNationalNo(_NationalNo);

                _NationalNo = PersonInfo.NationalNo;
                lblPersonID.Text = PersonInfo.PersonID.ToString();
                lblName.Text = PersonInfo.FullName;
                lblNationalNo.Text = PersonInfo.NationalNo.ToString();

                if (PersonInfo.Gendor == 0)
                    lblGendor.Text = "Male";
                else
                    lblGendor.Text = "Female";

                lblEmail.Text = PersonInfo.Email.ToString();
                lblAddress.Text = PersonInfo.Address.ToString();
                lblDateOfBirth.Text = PersonInfo.DateOfBirth.ToString();
                lblPhone.Text = PersonInfo.Phone.ToString();


                if (PersonInfo.CountriesInfo.CountryName != "")
                    lblCountry.Text = PersonInfo.CountriesInfo.CountryName;
                else
                    lblCountry.Text = "None";

                string ImagePath = PersonInfo.ImagePath;
                if (ImagePath == "" || !File.Exists(PersonInfo.ImagePath))
                {
                    if (lblGendor.Text == "Male")
                        PBImage.ImageLocation = _ImageMalePath;
                    else
                        PBImage.ImageLocation = _ImageFemalePath;
                }
                else
                    PBImage.ImageLocation = PersonInfo.ImagePath;

            }
        }

        public void ctrlShowPersonDetails_Load()
        {
            if (_PersonID != -1 && _PersonID != 0 && _PersonID != null)
                LoadAllDataByPersonID();
            else if (_NationalNo != "")
                LoadAllDataByNationalNo();

        } 


        private void lnkLblEditPeronInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != null)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
