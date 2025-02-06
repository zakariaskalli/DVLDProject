
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsInternationalLicenses
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? InternationalLicenseID { get; set; }
        public int? ApplicationID { get; set; }
        public clsApplications ApplicationsInfo { get; set; }
        public int? DriverID { get; set; }
        public clsDrivers DriversInfo { get; set; }
        public int? IssuedUsingLocalLicenseID { get; set; }
        public clsLicenses LicensesInfo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }


        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = null;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.IssuedUsingLocalLicenseID = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }


        private clsInternationalLicenses(
int? InternationalLicenseID, int? ApplicationID, int? DriverID, int? IssuedUsingLocalLicenseID, DateTime? IssueDate, DateTime? ExpirationDate, bool? IsActive, int? CreatedByUserID)        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationsInfo = clsApplications.FindByApplicationID(ApplicationID);
            this.DriverID = DriverID;
            this.DriversInfo = clsDrivers.FindByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.LicensesInfo = clsLicenses.FindByLicenseID(IssuedUsingLocalLicenseID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }


       private bool _AddNewInternationalLicenses()
       {
        this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicenses(
this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        return (this.InternationalLicenseID != null);
       }


       public static bool AddNewInternationalLicenses(
ref int? InternationalLicenseID, int? ApplicationID, int? DriverID, int? IssuedUsingLocalLicenseID, DateTime? IssueDate, DateTime? ExpirationDate, bool? IsActive, int? CreatedByUserID)        {
        InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicenses(
ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

        return (InternationalLicenseID != null);

       }


       private bool _UpdateInternationalLicenses()
       {
        return clsInternationalLicensesData.UpdateInternationalLicensesByID(
this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
       }


       public static bool UpdateInternationalLicensesByID(
int? InternationalLicenseID, int? ApplicationID, int? DriverID, int? IssuedUsingLocalLicenseID, DateTime? IssueDate, DateTime? ExpirationDate, bool? IsActive, int? CreatedByUserID)        {
        return clsInternationalLicensesData.UpdateInternationalLicensesByID(
InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

        }


       public static clsInternationalLicenses FindByInternationalLicenseID(int? InternationalLicenseID)

        {
            if (InternationalLicenseID == null)
            {
                return null;
            }
            int? ApplicationID = 0;
            int? DriverID = 0;
            int? IssuedUsingLocalLicenseID = 0;
            DateTime? IssueDate = DateTime.Now;
            DateTime? ExpirationDate = DateTime.Now;
            bool? IsActive = false;
            int? CreatedByUserID = 0;
            bool IsFound = clsInternationalLicensesData.GetInternationalLicensesInfoByID(InternationalLicenseID,
 ref ApplicationID,  ref DriverID,  ref IssuedUsingLocalLicenseID,  ref IssueDate,  ref ExpirationDate,  ref IsActive,  ref CreatedByUserID);

           if (IsFound)
               return new clsInternationalLicenses(
InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            else
                return null;
            }


       public static DataTable GetAllInternationalLicenses()
       {

        return clsInternationalLicensesData.GetAllInternationalLicenses();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewInternationalLicenses())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateInternationalLicenses();

            }
        
            return false;
        }



       public static bool DeleteInternationalLicenses(int InternationalLicenseID)
       {

        return clsInternationalLicensesData.DeleteInternationalLicenses(InternationalLicenseID);

       }


        public enum InternationalLicensesColumn
         {
            InternationalLicenseID,
            ApplicationID,
            DriverID,
            IssuedUsingLocalLicenseID,
            IssueDate,
            ExpirationDate,
            IsActive,
            CreatedByUserID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(InternationalLicensesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsInternationalLicensesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
