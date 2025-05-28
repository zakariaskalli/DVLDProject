
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplications : clsApplications
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? LocalDrivingLicenseApplicationID { get; set; }
        //public int? ApplicationID { get; set; }
        //public clsApplications ApplicationsInfo { get; set; }
        public int? LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassesInfo { get; set; }
        public clsLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = null;
            this.ApplicationID = 0;
            this.LicenseClassID = 0;
            Mode = enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplications(string NationalNo, int CreatedByUserID, int LicenseClassID)
        {
            clsPeople PersonInfo = clsPeople.FindByNationalNo(NationalNo);
            this.PeopleInfo = PersonInfo;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            Mode = enMode.AddNew;

        }

        public clsLocalDrivingLicenseApplications(int PersonID, int CreatedByUserID, int LicenseClassID)
        {
            clsPeople PersonInfo = clsPeople.FindByPersonID(PersonID);
            this.PeopleInfo = PersonInfo;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;


            Mode = enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;

            Mode = enMode.Update;
        }

        private clsLocalDrivingLicenseApplications(
int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? ApplicantPersonID, DateTime? ApplicationDate, int? ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime? LastStatusDate, decimal? PaidFees, int? CreatedByUserID, int? LicenseClassID) {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassesInfo = clsLicenseClasses.FindByLicenseClassID(LicenseClassID);
            Mode = enMode.Update;
        }


        private bool _AddNewLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplications(
    this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != null);
        }


        private bool _UpdateLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplicationsByID(
    this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }


        public static clsLocalDrivingLicenseApplications FindByLocalDrivingLicenseApplicationID(int? LocalDrivingLicenseApplicationID)
        {
            if (LocalDrivingLicenseApplicationID == null)
            {
                return null;
            }
            int? ApplicationID = null;
            int? LicenseClassID = null;

            bool IsFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsInfoByID(LocalDrivingLicenseApplicationID,
 ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                //now we find the base application
                clsApplications Application = clsApplications.FindByApplicationID(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplications(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (clsApplications.enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;

        }


        public static DataTable GetAllLocalDrivingLicenseApplications()
        {

            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();

        }



        public bool Save()
        {

            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (
                        (clsPeople.FindByNationalNo(this.PeopleInfo.NationalNo) != null)
                        &&
                        (clsPeople.FindByPersonID(this.ApplicantPersonID) == null)
                        )
                    {
                        this.ApplicantPersonID = clsPeople.FindByNationalNo(this.PeopleInfo.NationalNo).PersonID;
                    }

                    if (_AddNewLocalDrivingLicenseApplications())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplications();

            }

            return false;
        }



        public static bool DeleteLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
        {

            return clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID);

        }


        public enum LocalDrivingLicenseApplicationsColumn
        {
            LDLAppID,
            NationalNo,
            FullName,
            Status
        }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }


        public static DataTable SearchData(LocalDrivingLicenseApplicationsColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsLocalDrivingLicenseApplicationsData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }

        public static bool IsFoundApplicationMatchLocalDriveByNationalNo(string NationalNo, int LicenseClassID)
        {
            // Basic validation to check if the NationalNo is provided and is not empty
            if (string.IsNullOrWhiteSpace(NationalNo) || (LicenseClassID < 1 && LicenseClassID > 7))
            {
                return false;
            }


            // Call the Data Access Layer method to check if the user exists by NationalNo
            bool isFound = clsLocalDrivingLicenseApplicationsData.IsFoundApplicationMatchLocalDriveByNationalNo(NationalNo, LicenseClassID);

            // If user found or not, return the result
            return isFound;

        }

        public static bool IsFoundApplicationMatchLocalDriveByPersonID(int PersonID, int LicenseClassID)
        {
            // Basic validation to check if the PersonID and LicenseClassID are valid
            if (PersonID <= 0 || LicenseClassID < 1 || LicenseClassID > 7)
            {
                return false;
            }

            // Call the Data Access Layer method to check if the application exists by PersonID
            bool isFound = clsLocalDrivingLicenseApplicationsData.IsFoundApplicationMatchLocalDriveByPersonID(PersonID, LicenseClassID);

            // Return the result
            return isFound;
        }

        public static int ApplicationNumMatchPersonIDAndLicenseClassID(int PersonID, int LicenseClassID)
        {
            // Basic validation to check if the PersonID and LicenseClassID are valid
            if (PersonID <= 0 || LicenseClassID < 1 || LicenseClassID > 7)
            {
                return 0;
            }
            // Call the Data Access Layer method to get the application number by PersonID and LicenseClassID
            int applicationNum = clsLocalDrivingLicenseApplicationsData.ApplicationNumMatchPersonIDAndLicenseClassID(PersonID, LicenseClassID);
            // Return the application number
            return applicationNum;
        }

        public static int ApplicationNumMatchNationalNoAndLicenseClassID(string NationalNo, int LicenseClassID)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(NationalNo) || LicenseClassID < 1 || LicenseClassID > 7)
            {
                return 0;
            }

            // Call the Data Access Layer method
            int applicationNum = clsLocalDrivingLicenseApplicationsData.ApplicationNumMatchNationalNoAndLicenseClassID(NationalNo, LicenseClassID);

            return applicationNum;
        }

        public static bool CancelLicenseByNationalNoAndLicenseClassID(string NationalNo, int LicenseClassID)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(NationalNo) || LicenseClassID < 1 || LicenseClassID > 7)
            {
                return false;
            }

            // Call the Data Access Layer method
            bool isCancelled = clsLocalDrivingLicenseApplicationsData.CancelLicenseByNationalNoAndLicenseClassID(NationalNo, LicenseClassID);

            return isCancelled;
        }

    }
}
