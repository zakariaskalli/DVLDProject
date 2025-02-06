
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplications
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? LocalDrivingLicenseApplicationID { get; set; }
        public int? ApplicationID { get; set; }
        public clsApplications ApplicationsInfo { get; set; }
        public int? LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassesInfo { get; set; }


        public clsLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = null;
            this.ApplicationID = 0;
            this.LicenseClassID = 0;
            Mode = enMode.AddNew;
        }


        private clsLocalDrivingLicenseApplications(
int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID)        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationsInfo = clsApplications.FindByApplicationID(ApplicationID);
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


       public static bool AddNewLocalDrivingLicenseApplications(
ref int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID)        {
        LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplications(
ApplicationID, LicenseClassID);

        return (LocalDrivingLicenseApplicationID != null);

       }


       private bool _UpdateLocalDrivingLicenseApplications()
       {
        return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplicationsByID(
this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
       }


       public static bool UpdateLocalDrivingLicenseApplicationsByID(
int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID)        {
        return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplicationsByID(
LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);

        }


       public static clsLocalDrivingLicenseApplications FindByLocalDrivingLicenseApplicationID(int? LocalDrivingLicenseApplicationID)

        {
            if (LocalDrivingLicenseApplicationID == null)
            {
                return null;
            }
            int? ApplicationID = 0;
            int? LicenseClassID = 0;
            bool IsFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationsInfoByID(LocalDrivingLicenseApplicationID,
 ref ApplicationID,  ref LicenseClassID);

           if (IsFound)
               return new clsLocalDrivingLicenseApplications(
LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
            else
                return null;
            }


       public static DataTable GetAllLocalDrivingLicenseApplications()
       {

        return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLocalDrivingLicenseApplications())
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
            LocalDrivingLicenseApplicationID,
            ApplicationID,
            LicenseClassID
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



    }
}
