using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using System.Xml.Serialization;

namespace Business_Layer___DVLDProject
{
    [Serializable]
    public class clsAddEditPersonInfoBusiness
    {
        enum enMode { UpdateMode = 0, AddNewMode = 1 }

        enMode _Mode;


        public clsAddEditPersonInfoBusiness()
        {
            // Initialize default values if necessary
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = 1;
            this.ImagePath = "";

            _Mode = enMode.AddNewMode;
        }

        public clsAddEditPersonInfoBusiness(int PersonID)
        {
            this.PersonID = PersonID;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = 1;
            this.ImagePath = "";

            _Mode = enMode.UpdateMode;
        }

        public clsAddEditPersonInfoBusiness(string NationalNo)
        {
            this.NationalNo = NationalNo;
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = 1;
            this.ImagePath = "";

            _Mode = enMode.UpdateMode;
        }

        public clsAddEditPersonInfoBusiness(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            _Mode = enMode.AddNewMode;

        }

        public clsAddEditPersonInfoBusiness(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            _Mode = enMode.UpdateMode;

        }

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }


        static public DataTable LoadAllData()
        {
            return clsAddEditPersonInfoData.LoadAllCountriesName();
        }

        private bool AddNewRecordToTable()
        {
            int PersonID = clsAddEditPersonInfoData.AddRecordToTable(NationalNo, FirstName,
                           SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address,
                           Phone, Email, NationalityCountryID, ImagePath);


            if (PersonID == -1)
                return false;
            else
                this.PersonID = PersonID;
            return true;

        }

        private bool UpdateRecordToTable()
        {
            bool IsUpdate = clsAddEditPersonInfoData.UpdateRecordToTable(PersonID, NationalNo, FirstName,
                           SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address,
                           Phone, Email, NationalityCountryID, ImagePath);


            if (IsUpdate)
                return true;
            else
                return false;

        }

        static public bool NationalNoIsAvailable(string Data)
        {
            if (clsAddEditPersonInfoData.ColumnDataIsAvailableByString("NationalNo", Data))
                return true;
            else
                return false;
        }

        static public bool PhoneIsAvailable(string Data)
        {
            if (clsAddEditPersonInfoData.ColumnDataIsAvailableByString("Phone", Data))
                return true;
            else
                return false;
        }

        static public bool PersonIDIsFound(int PersonID)
        {
            if (clsAddEditPersonInfoData.ColumnDataIsAvailableByInt("PersonID", PersonID))
                return true;
            else
                return false;
        }


        static public bool EmailIsAvailable(string Data)
        {
            if (clsAddEditPersonInfoData.ColumnDataIsAvailableByString("Email", Data))
                return true;
            else
                return false;
        }

        static public clsAddEditPersonInfoBusiness UploadAllDataByPersonID(int PersonID)
        {
            clsAddEditPersonInfoBusiness clsA = new clsAddEditPersonInfoBusiness(PersonID);

            //PersonID = PersonID;
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";

            DateTime today = DateTime.Today;
            DateTime DateOfBirth = today.AddYears(-18);

            int Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = 1;
            string ImagePath = "";

            clsAddEditPersonInfoData.LoadDataByPersonID(PersonID, ref NationalNo,
                            ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                            ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email,
                            ref NationalityCountryID, ref ImagePath);


            clsA.PersonID = PersonID;
            clsA.NationalNo = NationalNo;
            clsA.FirstName = FirstName;
            clsA.SecondName = SecondName;
            clsA.ThirdName = ThirdName;
            clsA.LastName = LastName;
            clsA.DateOfBirth = DateOfBirth;
            clsA.Gendor = Gendor;
            clsA.Address = Address;
            clsA.Phone = Phone;
            clsA.Email = Email;
            clsA.NationalityCountryID = NationalityCountryID;
            clsA.ImagePath = ImagePath;


            return clsA;
        }

        static public clsAddEditPersonInfoBusiness UploadAllDataByNationalNo(string NationalNo)
        {
            clsAddEditPersonInfoBusiness clsA = new clsAddEditPersonInfoBusiness(NationalNo);

            //PersonID = PersonID;
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";

            DateTime today = DateTime.Today;
            DateTime DateOfBirth = today.AddYears(-18);

            int Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = 1;
            string ImagePath = "";

            clsAddEditPersonInfoData.LoadDataByNationalNo(NationalNo, ref PersonID,
                            ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                            ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email,
                            ref NationalityCountryID, ref ImagePath);


            clsA.NationalNo = NationalNo;
            clsA.PersonID = PersonID;
            clsA.FirstName = FirstName;
            clsA.SecondName = SecondName;
            clsA.ThirdName = ThirdName;
            clsA.LastName = LastName;
            clsA.DateOfBirth = DateOfBirth;
            clsA.Gendor = Gendor;
            clsA.Address = Address;
            clsA.Phone = Phone;
            clsA.Email = Email;
            clsA.NationalityCountryID = NationalityCountryID;
            clsA.ImagePath = ImagePath;


            return clsA;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (AddNewRecordToTable())
                    {
                        _Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                        return false;
                case enMode.UpdateMode: 
                    if (UpdateRecordToTable())
                        return true;
                    else
                        return false;
            }
            return false;
        }
    }

}
