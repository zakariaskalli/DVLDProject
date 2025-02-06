
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsDetainedLicenses
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? DetainID { get; set; }
        public int? LicenseID { get; set; }
        public clsLicenses LicensesInfo { get; set; }
        public DateTime? DetainDate { get; set; }
        public decimal? FineFees { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }
        public bool? IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        //public clsUsers UsersInfo { get; set; }
        public int? ReleaseApplicationID { get; set; }
        public clsApplications ApplicationsInfo { get; set; }


        public clsDetainedLicenses()
        {
            this.DetainID = null;
            this.LicenseID = 0;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0m;
            this.CreatedByUserID = 0;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = 0;
            Mode = enMode.AddNew;
        }


        private clsDetainedLicenses(
int? DetainID, int? LicenseID, DateTime? DetainDate, decimal? FineFees, int? CreatedByUserID, bool? IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.LicensesInfo = clsLicenses.FindByLicenseID(LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            //this.UsersInfo = clsUsers.FindByUserID(ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ApplicationsInfo = clsApplications.FindByApplicationID(ReleaseApplicationID);
            Mode = enMode.Update;
        }


       private bool _AddNewDetainedLicenses()
       {
        this.DetainID = clsDetainedLicensesData.AddNewDetainedLicenses(
this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        return (this.DetainID != null);
       }


       public static bool AddNewDetainedLicenses(
ref int? DetainID, int? LicenseID, DateTime? DetainDate, decimal? FineFees, int? CreatedByUserID, bool? IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)        {
        DetainID = clsDetainedLicensesData.AddNewDetainedLicenses(
LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);

        return (DetainID != null);

       }


       private bool _UpdateDetainedLicenses()
       {
        return clsDetainedLicensesData.UpdateDetainedLicensesByID(
this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
       }


       public static bool UpdateDetainedLicensesByID(
int? DetainID, int? LicenseID, DateTime? DetainDate, decimal? FineFees, int? CreatedByUserID, bool? IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)        {
        return clsDetainedLicensesData.UpdateDetainedLicensesByID(
DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);

        }


       public static clsDetainedLicenses FindByDetainID(int? DetainID)

        {
            if (DetainID == null)
            {
                return null;
            }
            int? LicenseID = 0;
            DateTime? DetainDate = DateTime.Now;
            decimal? FineFees = 0m;
            int? CreatedByUserID = 0;
            bool? IsReleased = false;
            DateTime? ReleaseDate = DateTime.Now;
            int? ReleasedByUserID = 0;
            int? ReleaseApplicationID = 0;
            bool IsFound = clsDetainedLicensesData.GetDetainedLicensesInfoByID(DetainID,
 ref LicenseID,  ref DetainDate,  ref FineFees,  ref CreatedByUserID,  ref IsReleased,  ref ReleaseDate,  ref ReleasedByUserID,  ref ReleaseApplicationID);

           if (IsFound)
               return new clsDetainedLicenses(
DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
            }


       public static DataTable GetAllDetainedLicenses()
       {

        return clsDetainedLicensesData.GetAllDetainedLicenses();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDetainedLicenses())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicenses();

            }
        
            return false;
        }



       public static bool DeleteDetainedLicenses(int DetainID)
       {

        return clsDetainedLicensesData.DeleteDetainedLicenses(DetainID);

       }


        public enum DetainedLicensesColumn
         {
            DetainID,
            LicenseID,
            DetainDate,
            FineFees,
            CreatedByUserID,
            IsReleased,
            ReleaseDate,
            ReleasedByUserID,
            ReleaseApplicationID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(DetainedLicensesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsDetainedLicensesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
