
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsDrivers
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? DriverID { get; set; }
        public int? PersonID { get; set; }
        public clsPeople PeopleInfo { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }
        public DateTime? CreatedDate { get; set; }


        public clsDrivers()
        {
            this.DriverID = null;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }


        private clsDrivers(
int? DriverID, int? PersonID, int? CreatedByUserID, DateTime? CreatedDate)        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.CreatedDate = CreatedDate;
            Mode = enMode.Update;
        }


       private bool _AddNewDrivers()
       {
        this.DriverID = clsDriversData.AddNewDrivers(
this.PersonID, this.CreatedByUserID, this.CreatedDate);
        return (this.DriverID != null);
       }


       public static bool AddNewDrivers(
ref int? DriverID, int? PersonID, int? CreatedByUserID, DateTime? CreatedDate)        {
        DriverID = clsDriversData.AddNewDrivers(
PersonID, CreatedByUserID, CreatedDate);

        return (DriverID != null);

       }


       private bool _UpdateDrivers()
       {
        return clsDriversData.UpdateDriversByID(
this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
       }


       public static bool UpdateDriversByID(
int? DriverID, int? PersonID, int? CreatedByUserID, DateTime? CreatedDate)        {
        return clsDriversData.UpdateDriversByID(
DriverID, PersonID, CreatedByUserID, CreatedDate);

        }


       public static clsDrivers FindByDriverID(int? DriverID)

        {
            if (DriverID == null)
            {
                return null;
            }
            int? PersonID = 0;
            int? CreatedByUserID = 0;
            DateTime? CreatedDate = DateTime.Now;
            bool IsFound = clsDriversData.GetDriversInfoByID(DriverID,
 ref PersonID,  ref CreatedByUserID,  ref CreatedDate);

           if (IsFound)
               return new clsDrivers(
DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
            }


       public static DataTable GetAllDrivers()
       {

        return clsDriversData.GetAllDrivers();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDrivers())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDrivers();

            }
        
            return false;
        }



       public static bool DeleteDrivers(int DriverID)
       {

        return clsDriversData.DeleteDrivers(DriverID);

       }


        public enum DriversColumn
        {
           DriverID,
           PersonID,
           NationalNo,
           FullName
        }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(DriversColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsDriversData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
