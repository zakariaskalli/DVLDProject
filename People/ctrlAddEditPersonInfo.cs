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
using System.Xml.Serialization;
using Business_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using DVLD_BusinessLayer;

namespace DVLDProject
{
    public partial class ctrlAddEditPersonInfo : UserControl
    {
        public event EventHandler CloseButtonClicked;

        public event Action<int> OnSaveClicked;

        clsPeople PersonInfo = new clsPeople();

        public int _PersonID = -1;

        public enum enMode { AddNew =  0, Edit = 1 }

        public enMode Mode = enMode.AddNew;

        //clsAddEditPersonInfoBusiness _ClsAdd;

        //clsAddEditPersonInfoBusiness _ClsUpdate;

        protected virtual void SaveClicked(int PersonID)
        {
            Action<int> handler = OnSaveClicked;
            if (handler != null)
                handler(PersonID);
        }

        public ctrlAddEditPersonInfo()
        {
            InitializeComponent();
        }

        //public ctrlAddEditPersonInfo(int PersonID)
        //{
        //    InitializeComponent();
        //
        //    _PersonID = PersonID;
        //}

        static private string _ImageMalePath = @"C:/Programation Level 2/DVLDProject/Project Image/homme.png";
        static private string _ImageFemalePath = @"C:/Programation Level 2/DVLDProject/Project Image/medecin.png";

        private void DefaultOfCtrl()
        {
            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddYears(-18);
            DTP1.MaxDate = maxDate;

            rbMale.Checked = true;
            PB1.ImageLocation = _ImageMalePath;

            // Load All Countries From DataBase To ComboBox

            DataTable dt = clsCountries.GetAllCountries();

            if (dt != null)
            {
                foreach (DataRow RecordRow in dt.Rows) 
                {
                    cbCountry.Items.Add(RecordRow["CountryName"]);
                }
            }
            else
                cbCountry.Items.Add("None");


            cbCountry.SelectedIndex = cbCountry.FindString("Morocco");
        }

        private void UploadAllData()
        {
            tbFirstName.Text = PersonInfo.FirstName;
            tbSecondName.Text = PersonInfo.SecondName;
            tbThirdName.Text = PersonInfo.ThirdName;
            tbLastName.Text = PersonInfo.LastName;
            tbNationalNo.Text = PersonInfo.NationalNo;
            DTP1.Value = (DateTime)PersonInfo.DateOfBirth;

            if (PersonInfo.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            tbPhone.Text = PersonInfo.Phone;
            tbEmail.Text = PersonInfo.Email;
            tbAddress.Text = PersonInfo.Address;


            cbCountry.SelectedIndex = (int)(PersonInfo.NationalityCountryID - 1);

            tbEmail.Text = PersonInfo.Email;

            if (PersonInfo.ImagePath == "" || !File.Exists(PersonInfo.ImagePath))
            {
                    if (rbMale.Checked)
                    PB1.ImageLocation = _ImageMalePath;
                else
                    PB1.ImageLocation = _ImageFemalePath;
            }
            else
            {
                PB1.ImageLocation = PersonInfo.ImagePath;
                openFileDialog1.FileName = PersonInfo.ImagePath;
                linkLabelRemove.Visible = true;
            }


        }

        private void ctrlAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            DefaultOfCtrl();

            if (PersonInfo.PersonID != null)
            {
                PersonInfo = clsPeople.FindByPersonID(PersonInfo.PersonID);

                //clsAddEditPersonInfoBusiness Data = clsAddEditPersonInfoBusiness.UploadAllDataByPersonID(_PersonID);
                
                UploadAllData();
                Mode = enMode.Edit;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked && openFileDialog1.FileName == "openFileDialog1") 
                PB1.ImageLocation = _ImageFemalePath;

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

            if (rbMale.Checked && openFileDialog1.FileName == "openFileDialog1")
                PB1.ImageLocation = _ImageMalePath;

        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnClose)
            {
                e.Cancel = false;
                return;
            }

            if (tbEmail.Text == "")
                return;


            if (tbEmail.TextLength < 10)
            {
                e.Cancel = true; 
                errorProvider1.SetError(tbEmail, "Enter A Valid Length Email.");
            }
            else if (tbEmail.Text.Substring(tbEmail.TextLength - 10, 10) != "@gmail.com")
            {

                e.Cancel = true;
                errorProvider1.SetError(tbEmail, "Enter A Valid Email.");
            }
            else
                errorProvider1.SetError(tbEmail, string.Empty);

            // Code Validating Email Is Available In DataBase

        }

        private void linkLableSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Select an Image File";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;

                if (System.IO.File.Exists(FilePath))
                {
                    // إذا كان الملف موجودًا، قم بتعيين الصورة إلى PictureBox
                    PB1.ImageLocation = FilePath;
                    linkLabelRemove.Visible = true;
                }
                else
                {
                    // إذا لم يكن الملف موجودًا، قم بتعيين المسار كقيمة فارغة
                    FilePath = "";
                    MessageBox.Show("The selected image file does not exist.");
                }
            }
        }

        private void linkLabelRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.FileName = "";

            if (rbFemale.Checked == true)
                PB1.ImageLocation = _ImageFemalePath;
            else
                PB1.ImageLocation = _ImageMalePath;
            
            
            linkLabelRemove.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (CloseButtonClicked != null)
            {
                CloseButtonClicked(this, e);
            }

        }

        //[Serializable]
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Image Upload To My Folder

            string FilePath = openFileDialog1.FileName;
            string destinationPath = "";

            // NewID()

            if (FilePath != "openFileDialog1" )
            {
                try
                {
                    if (FilePath != "")
                    {
                        string guid = Guid.NewGuid().ToString();
                        destinationPath = @"C:\Programation Level 2\DVLDProject\Project Image\Image Copies\" + guid + Path.GetExtension(FilePath);

                        File.Copy(FilePath, destinationPath, true);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }
            }


            

            PersonInfo.NationalNo = tbNationalNo.Text;
            PersonInfo.FirstName = tbFirstName.Text;
            PersonInfo.SecondName = tbSecondName.Text;
            PersonInfo.ThirdName = tbThirdName.Text;
            PersonInfo.LastName = tbLastName.Text;
            PersonInfo.DateOfBirth = DTP1.Value;

            PersonInfo.Gendor = 0;

            if (rbMale.Checked != true)
                PersonInfo.Gendor = 1;

            PersonInfo.Address = tbAddress.Text;
            PersonInfo.Phone = tbPhone.Text;
            PersonInfo.Email = tbEmail.Text;
            PersonInfo.NationalityCountryID = cbCountry.FindString(cbCountry.Text) + 1;

            PersonInfo.ImagePath = "";

            if (destinationPath != "")
                PersonInfo.ImagePath = destinationPath;


            if (PersonInfo.NationalNo == "" || PersonInfo.FirstName == "" || PersonInfo.Address == "" || PersonInfo.Phone == "")
            {
                MessageBox.Show("Enter All Data Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PersonInfo.PersonID != null)
            {
                PersonInfo = new clsPeople(PersonInfo.PersonID, PersonInfo.NationalNo, PersonInfo.FirstName, PersonInfo.DateOfBirth, PersonInfo.Gendor, PersonInfo.Address, PersonInfo.Phone, PersonInfo.NationalityCountryID, PersonInfo.SecondName, PersonInfo.ThirdName, PersonInfo.LastName, PersonInfo.Email, PersonInfo.ImagePath);


            }
            else
            {

                /*
                _ClsAdd = new clsAddEditPersonInfoBusiness(PersonInfo.PersonID, PersonInfo.NationalNo, PersonInfo.FirstName, PersonInfo.SecondName,
                PersonInfo.ThirdName, PersonInfo.LastName, PersonInfo.DateOfBirth, PersonInfo.Gendor, PersonInfo.Address, PersonInfo.Phone, PersonInfo.Email, PersonInfo.NationalityCountryID, PersonInfo.ImagePath);
                */

            }

            if (PersonInfo.Save())
            {
                // Serializable

                /*
                
                // For XML
                XmlSerializer serializer = new XmlSerializer(typeof(clsAddEditPersonInfoBusiness));
                
                using (TextWriter writer = new StreamWriter("person.xml"))
                {
                    serializer.Serialize(writer, PersonInfo._ClsAdd);
                }

                // For Json
                DataContractJsonSerializer serializerJson = new DataContractJsonSerializer(typeof(clsAddEditPersonInfoBusiness));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializerJson.WriteObject(stream, _ClsAdd);

                    string jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                    File.WriteAllText("person.json", jsonString);
                }

                //For Binary
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream stream = new FileStream("person.bin", FileMode.Create))
                {
                    formatter.Serialize(stream, _ClsAdd);
                }

                */


                MessageBox.Show("Data Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //_PersonID = _ClsAdd.PersonID;
            }
            else
            {
                MessageBox.Show("Saved Denied", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (OnSaveClicked != null)
                SaveClicked((int)PersonInfo.PersonID);
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnClose)
            {
                e.Cancel = false;
                return;
            }

            if (tbFirstName.Text == "")
            {
                e.Cancel = true;
                errorProvider2.SetError(tbFirstName, "Enter A The FirstName.");
            }
            else
                errorProvider2.SetError(tbFirstName, string.Empty);


        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnClose)
            {
                e.Cancel = false;
                return;
            }

            if (tbNationalNo.Text == "")
            {
                e.Cancel = true;
                
                errorProvider2.SetError(tbNationalNo, "Enter A The NationalNo.");

            }
            else
                errorProvider2.SetError(tbNationalNo, string.Empty);


            if (Mode == enMode.Edit)
            {
                if (tbNationalNo.Text != PersonInfo.NationalNo && clsAddEditPersonInfoBusiness.NationalNoIsAvailable(tbNationalNo.Text))
                {
                    e.Cancel = true;
                    errorProvider2.SetError(tbNationalNo, "This NationalNo Is Available.");

                }
            }
            else if (clsAddEditPersonInfoBusiness.NationalNoIsAvailable(tbNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider2.SetError(tbNationalNo, "This NationalNo Is Available.");
            }
            else
                errorProvider2.SetError(tbNationalNo, string.Empty); 
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnClose)
            {
                e.Cancel = false;
                return;
            }

            if (tbPhone.Text == "")
            {
                e.Cancel = true;
                errorProvider2.SetError(tbPhone, "Enter A The Phone.");
            }
            else
                errorProvider2.SetError(tbPhone, string.Empty);

            if (Mode == enMode.Edit)
            {
                if (tbPhone.Text != PersonInfo.Phone && clsAddEditPersonInfoBusiness.PhoneIsAvailable(tbPhone.Text))
                {
                    e.Cancel = true;
                    errorProvider2.SetError(tbPhone, "This Phone Is Available.");
                }
            }
            else if (clsAddEditPersonInfoBusiness.PhoneIsAvailable(tbPhone.Text))
            {
                e.Cancel = true;
                errorProvider2.SetError(tbPhone, "This Phone Is Available.");
            }
            else
                errorProvider2.SetError(tbPhone, string.Empty);
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl == btnClose)
            {
                e.Cancel = false;
                return;
            }

            if (tbAddress.Text == "")
            {
                e.Cancel = true;
                errorProvider2.SetError(tbAddress, "Enter A Your Address.");
            }
            else
                errorProvider2.SetError(tbAddress, string.Empty);

        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

