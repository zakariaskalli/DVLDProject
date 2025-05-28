
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsApplications
    {
        //#nullable enable
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? ApplicationID { get; set; }
        public int? ApplicantPersonID { get; set; }
        public clsPeople PeopleInfo { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public int? ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypesInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public decimal? PaidFees { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }


        public clsApplications()
        {
            this.ApplicationID = null;
            this.ApplicantPersonID = 0;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 0;
            this.ApplicationStatus = default(byte);
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0m;
            this.CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }


        private clsApplications(
int? ApplicationID, int? ApplicantPersonID, DateTime? ApplicationDate, int? ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime? LastStatusDate, decimal? PaidFees, int? CreatedByUserID)        
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypesInfo = clsApplicationTypes.FindByApplicationTypeID(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }


       private bool _AddNewApplications()
       {
        this.ApplicationID = clsApplicationsData.AddNewApplications(
this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        return (this.ApplicationID != null);
       }

        // I don't Need To Use

        /*
       public static bool AddNewApplications(
ref int? ApplicationID, int? ApplicantPersonID, DateTime? ApplicationDate, int? ApplicationTypeID, byte? ApplicationStatus, DateTime? LastStatusDate, decimal? PaidFees, int? CreatedByUserID)        {
        ApplicationID = clsApplicationsData.AddNewApplications(
ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

        return (ApplicationID != null);

       }
        */

       private bool _UpdateApplications()
       {
        return clsApplicationsData.UpdateApplicationsByID(
this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
       }


        // I don't Need To Use

        /*
       public static bool UpdateApplicationsByID(
int? ApplicationID, int? ApplicantPersonID, DateTime? ApplicationDate, int? ApplicationTypeID, byte? ApplicationStatus, DateTime? LastStatusDate, decimal? PaidFees, int? CreatedByUserID)        {
        return clsApplicationsData.UpdateApplicationsByID(
ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

        }

        */



        public static clsApplications FindByApplicationID(int? ApplicationID)
        {
            if (ApplicationID == null)
            {
                return null;
            }
            int? ApplicantPersonID = 0;
            DateTime? ApplicationDate = DateTime.Now;
            int? ApplicationTypeID = 0;
            byte? ApplicationStatus = null; // Change enApplicationStatus? to byte?
            DateTime? LastStatusDate = DateTime.Now;
            decimal? PaidFees = 0m;
            int? CreatedByUserID = 0;
            bool IsFound = clsApplicationsData.GetApplicationsInfoByID(ApplicationID,
                ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (IsFound)
                return new clsApplications(
                    ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }


       public static DataTable GetAllApplications()
       {

        return clsApplicationsData.GetAllApplications();

       }



        public bool Save()
        {
            if (
                (clsPeople.FindByNationalNo(this.PeopleInfo.NationalNo) != null)
                &&
                (clsPeople.FindByPersonID(this.ApplicantPersonID) == null)
                )
            {
                this.ApplicantPersonID = clsPeople.FindByNationalNo(this.PeopleInfo.NationalNo).PersonID;
            }


            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplications())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplications();

            }
        
            return false;
        }



       public static bool DeleteApplications(int ApplicationID)
       {

        return clsApplicationsData.DeleteApplications(ApplicationID);

       }


        public enum ApplicationsColumn
         {
            ApplicationID,
            ApplicantPersonID,
            ApplicationDate,
            ApplicationTypeID,
            ApplicationStatus,
            LastStatusDate,
            PaidFees,
            CreatedByUserID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(ApplicationsColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsApplicationsData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
