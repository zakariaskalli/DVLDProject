
using System;
using System.Data;
using System.Linq;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsPeople
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; } = null;
        public string ThirdName { get; set; } = null;
        public string LastName { get; set; } = null;
        public DateTime? DateOfBirth { get; set; }
        public byte? Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } = null;
        public int? NationalityCountryID { get; set; }
        public clsCountries CountriesInfo { get; set; }
        public string ImagePath { get; set; } = null;

        private string _fullName;
        public string FullName
        {
            get
            {

               _fullName = string.Join(" ", new[]
                    {
                this.FirstName?.Trim(),
                this.SecondName?.Trim(),
                this.ThirdName?.Trim(),
                this.LastName?.Trim()
            }.Where(name => !string.IsNullOrWhiteSpace(name)));
                
                return _fullName;
            }
        }

        public clsPeople()
        {
            this.PersonID = null;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = null;
            this.ThirdName = null;
            this.LastName = null;
            this.DateOfBirth = DateTime.Now;
            this.Gendor = default(byte);
            this.Address = "";
            this.Phone = "";
            this.Email = null;
            this.NationalityCountryID = 0;
            this.ImagePath = null;
            Mode = enMode.AddNew;
        }


        public clsPeople(
int? PersonID, string NationalNo, string FirstName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, int? NationalityCountryID, string SecondName = null, string ThirdName = null, string LastName = null, string Email = null, string ImagePath = null) {
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
            this.CountriesInfo = clsCountries.FindByCountryID(NationalityCountryID);
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }


        public clsPeople(
 string NationalNo, int? PersonID, string FirstName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, int? NationalityCountryID, string SecondName = null, string ThirdName = null, string LastName = null, string Email = null, string ImagePath = null)
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
            this.CountriesInfo = clsCountries.FindByCountryID(NationalityCountryID);
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }

        private bool _AddNewPeople()
        {
            this.PersonID = clsPeopleData.AddNewPeople(
    this.NationalNo, this.FirstName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.NationalityCountryID, this.SecondName, this.ThirdName, this.LastName, this.Email, this.ImagePath);
            return (this.PersonID != null);
        }


        public static bool AddNewPeople(
 ref int? PersonID, string NationalNo, string FirstName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, int? NationalityCountryID, string SecondName = null, string ThirdName = null, string LastName = null, string Email = null, string ImagePath = null) {
            PersonID = clsPeopleData.AddNewPeople(
    NationalNo, FirstName, DateOfBirth, Gendor, Address, Phone, NationalityCountryID, SecondName, ThirdName, LastName, Email, ImagePath);

            return (PersonID != null);

        }


        private bool _UpdatePeople()
        {
            return clsPeopleData.UpdatePeopleByID(
    this.PersonID, this.NationalNo, this.FirstName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.NationalityCountryID, this.SecondName, this.ThirdName, this.LastName, this.Email, this.ImagePath);
        }


        public static bool UpdatePeopleByID(
 int? PersonID, string NationalNo, string FirstName, DateTime? DateOfBirth, byte? Gendor, string Address, string Phone, int? NationalityCountryID, string SecondName = null, string ThirdName = null, string LastName = null, string Email = null, string ImagePath = null) {
            return clsPeopleData.UpdatePeopleByID(
    PersonID, NationalNo, FirstName, DateOfBirth, Gendor, Address, Phone, NationalityCountryID, SecondName, ThirdName, LastName, Email, ImagePath);

        }


        public static clsPeople FindByPersonID(int? PersonID)

        {
            if (PersonID == null)
            {
                return null;
            }
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime? DateOfBirth = DateTime.Now;
            byte? Gendor = default(byte);
            string Address = "";
            string Phone = "";
            string Email = "";
            int? NationalityCountryID = 0;
            string ImagePath = "";
            bool IsFound = clsPeopleData.GetPeopleInfoByID(PersonID,
 ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (IsFound)
                return new clsPeople(
 PersonID, NationalNo, FirstName, DateOfBirth, Gendor, Address, Phone, NationalityCountryID, SecondName, ThirdName, LastName, Email, ImagePath);
            else
                return null;
        }

        public static clsPeople FindByNationalNo(string NationalNo)
        {
            if (NationalNo == null || NationalNo == "")
            {
                return null;
            }

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime? DateOfBirth = DateTime.Now;
            byte? Gendor = default(byte);
            string Address = "";
            string Phone = "";
            string Email = "";
            int? NationalityCountryID = 0;
            string ImagePath = "";
            bool IsFound = clsPeopleData.GetPeopleInfoByNationalNo(NationalNo.Trim(), ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (IsFound)
                return new clsPeople(
NationalNo,PersonID, FirstName, DateOfBirth, Gendor, Address, Phone, NationalityCountryID, SecondName, ThirdName, LastName, Email, ImagePath);
            else
                return null;
        }



        public static DataTable GetAllPeople()
       {

        return clsPeopleData.GetAllPeople();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewPeople())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePeople();

            }
        
            return false;
        }



       public static bool DeletePeople(int PersonID)
       {

        return clsPeopleData.DeletePeople(PersonID);

       }


        public enum PeopleColumn
         {
            PersonID,
            NationalNo,
            FirstName,
            SecondName,
            ThirdName,
            LastName,
            DateOfBirth,
            Gendor,
            Address,
            Phone,
            Email,
            NationalityCountryID,
            ImagePath
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(PeopleColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsPeopleData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }

        public int GetPersonIdByNationalNo(string NationalNo)
        {
            DataTable dataTable = SearchData(PeopleColumn.NationalNo, NationalNo, SearchMode.ExactMatch);

            // Default value if PersonId is not found
            int personId = 0;

            // Check if the DataTable is not empty
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                // Access the first row
                DataRow row = dataTable.Rows[0];

                // Check if the "PersonId" column exists
                if (dataTable.Columns.Contains("PersonId"))
                {
                    // Save the value of the "PersonId" column
                    personId = Convert.ToInt32(row["PersonId"]);
                }
                else
                {
                    //Console.WriteLine("The column 'PersonId' does not exist in the DataTable.");
                }
            }
            else
            {
                //Console.WriteLine("The DataTable is empty or null.");
            }

            // Return the PersonId
            return personId;
        }

    }
}
