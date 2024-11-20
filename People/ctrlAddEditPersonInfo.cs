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

using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace DVLDProject
{
    public partial class ctrlAddEditPersonInfo : UserControl
    {
        public event EventHandler CloseButtonClicked;

        public event Action<int> OnSaveClicked;

        public int _PersonID = -1;

        public enum enMode { AddNew =  0, Edit = 1 }

        public enMode Mode = enMode.AddNew;

        clsAddEditPersonInfoBusiness _ClsAdd;

        clsAddEditPersonInfoBusiness _ClsUpdate;

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

            DataTable dt = clsAddEditPersonInfoBusiness.LoadAllData();
            
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

        private void UploadAllData(clsAddEditPersonInfoBusiness Data)
        {
            _ClsUpdate = Data;

            tbFirstName.Text = Data.FirstName;
            tbSecondName.Text = Data.SecondName;
            tbThirdName.Text = Data.ThirdName;
            tbLastName.Text = Data.LastName;
            tbNationalNo.Text = Data.NationalNo;
            DTP1.Value = Data.DateOfBirth;

            if (Data.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            tbPhone.Text = Data.Phone;
            tbEmail.Text = Data.Email;
            tbAddress.Text = Data.Address;


            cbCountry.SelectedIndex = (Data.NationalityCountryID - 1);

            tbEmail.Text = Data.Email;

            if (Data.ImagePath == "" || !File.Exists(Data.ImagePath))
            {
                    if (rbMale.Checked)
                    PB1.ImageLocation = _ImageMalePath;
                else
                    PB1.ImageLocation = _ImageFemalePath;
            }
            else
            {
                PB1.ImageLocation = Data.ImagePath;
                openFileDialog1.FileName = Data.ImagePath;
                linkLabelRemove.Visible = true;
            }


        }

        private void ctrlAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            DefaultOfCtrl();

            if (_PersonID != -1)
            {
                clsAddEditPersonInfoBusiness Data = clsAddEditPersonInfoBusiness.UploadAllDataByPersonID(_PersonID);
                UploadAllData(Data);
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

            

            string NationalNo = tbNationalNo.Text;
            string FirstName = tbFirstName.Text;
            string SecondName = tbSecondName.Text;
            string ThirdName = tbThirdName.Text;
            string LastName = tbLastName.Text;
            DateTime DateOfBirth = DTP1.Value;
            
            int Gendor = 0;

            if (rbMale.Checked != true)
                Gendor = 1;

            string Address = tbAddress.Text;
            string Phone = tbPhone.Text;
            string Email = tbEmail.Text;
            int NationalityCountryID = cbCountry.FindString(cbCountry.Text) + 1;
            
            string ImagePath = "";

            if (destinationPath != "")
                ImagePath = destinationPath;


            if (NationalNo == "" || FirstName == "" || Address == "" || Phone == "")
            {
                MessageBox.Show("Enter All Data Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_PersonID == -1)
            {
                _ClsAdd = new clsAddEditPersonInfoBusiness(NationalNo, FirstName, SecondName,
                ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
                _PersonID = _ClsAdd.PersonID;
            }
            else
            {
                _ClsAdd = new clsAddEditPersonInfoBusiness(_PersonID, NationalNo, FirstName, SecondName,
                ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);

            }


            if (_ClsAdd.Save())
            {
                // For XML
                XmlSerializer serializer = new XmlSerializer(typeof(clsAddEditPersonInfoBusiness));
                
                using (TextWriter writer = new StreamWriter("person.xml"))
                {
                    serializer.Serialize(writer, _ClsAdd);
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

                MessageBox.Show("Data Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _PersonID = _ClsAdd.PersonID;
            }
            else
            {
                MessageBox.Show("Saved Denied", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (OnSaveClicked != null)
                SaveClicked(_ClsAdd.PersonID);
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
                if (tbNationalNo.Text != _ClsUpdate.NationalNo && clsAddEditPersonInfoBusiness.NationalNoIsAvailable(tbNationalNo.Text))
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
                if (tbPhone.Text != _ClsUpdate.Phone && clsAddEditPersonInfoBusiness.PhoneIsAvailable(tbPhone.Text))
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


    }
}

