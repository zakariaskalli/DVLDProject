
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsLicenses
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? LicenseID { get; set; }
        public int? ApplicationID { get; set; }
        public clsApplications ApplicationsInfo { get; set; }
        public int? DriverID { get; set; }
        public clsDrivers DriversInfo { get; set; }
        public int? LicenseClass { get; set; }
        public clsLicenseClasses LicenseClassesInfo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Notes { get; set; } = null;
        public decimal? PaidFees { get; set; }
        public bool? IsActive { get; set; }
        public byte? IssueReason { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }


        public clsLicenses()
        {
            this.LicenseID = null;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.LicenseClass = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = null;
            this.PaidFees = 0m;
            this.IsActive = false;
            this.IssueReason = default(byte);
            this.CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }


        private clsLicenses(
int? LicenseID, int? ApplicationID, int? DriverID, int? LicenseClass, DateTime? IssueDate, DateTime? ExpirationDate, decimal? PaidFees, bool? IsActive, byte? IssueReason, int? CreatedByUserID, string Notes = null)        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationsInfo = clsApplications.FindByApplicationID(ApplicationID);
            this.DriverID = DriverID;
            this.DriversInfo = clsDrivers.FindByDriverID(DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassesInfo = clsLicenseClasses.FindByLicenseClassID(LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }


       private bool _AddNewLicenses()
       {
        this.LicenseID = clsLicensesData.AddNewLicenses(
this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID, this.Notes);
        return (this.LicenseID != null);
       }


       public static bool AddNewLicenses(
ref int? LicenseID, int? ApplicationID, int? DriverID, int? LicenseClass, DateTime? IssueDate, DateTime? ExpirationDate, decimal? PaidFees, bool? IsActive, byte? IssueReason, int? CreatedByUserID, string Notes = null)        {
        LicenseID = clsLicensesData.AddNewLicenses(
ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, PaidFees, IsActive, IssueReason, CreatedByUserID, Notes);

        return (LicenseID != null);

       }


       private bool _UpdateLicenses()
       {
        return clsLicensesData.UpdateLicensesByID(
this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID, this.Notes);
       }


       public static bool UpdateLicensesByID(
int? LicenseID, int? ApplicationID, int? DriverID, int? LicenseClass, DateTime? IssueDate, DateTime? ExpirationDate, decimal? PaidFees, bool? IsActive, byte? IssueReason, int? CreatedByUserID, string Notes = null)        {
        return clsLicensesData.UpdateLicensesByID(
LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, PaidFees, IsActive, IssueReason, CreatedByUserID, Notes);

        }


       public static clsLicenses FindByLicenseID(int? LicenseID)

        {
            if (LicenseID == null)
            {
                return null;
            }
            int? ApplicationID = 0;
            int? DriverID = 0;
            int? LicenseClass = 0;
            DateTime? IssueDate = DateTime.Now;
            DateTime? ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal? PaidFees = 0m;
            bool? IsActive = false;
            byte? IssueReason = default(byte);
            int? CreatedByUserID = 0;
            bool IsFound = clsLicensesData.GetLicensesInfoByID(LicenseID,
 ref ApplicationID,  ref DriverID,  ref LicenseClass,  ref IssueDate,  ref ExpirationDate,  ref Notes,  ref PaidFees,  ref IsActive,  ref IssueReason,  ref CreatedByUserID);

           if (IsFound)
               return new clsLicenses(
LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, PaidFees, IsActive, IssueReason, CreatedByUserID, Notes);
            else
                return null;
            }


       public static DataTable GetAllLicenses()
       {

        return clsLicensesData.GetAllLicenses();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenses())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenses();

            }
        
            return false;
        }



       public static bool DeleteLicenses(int LicenseID)
       {

        return clsLicensesData.DeleteLicenses(LicenseID);

       }


        public enum LicensesColumn
         {
            LicenseID,
            ApplicationID,
            DriverID,
            LicenseClass,
            IssueDate,
            ExpirationDate,
            Notes,
            PaidFees,
            IsActive,
            IssueReason,
            CreatedByUserID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(LicensesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsLicensesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
