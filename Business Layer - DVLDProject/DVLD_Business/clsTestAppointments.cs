
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointments
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? TestAppointmentID { get; set; }
        public int? TestTypeID { get; set; }
        public clsTestTypes TestTypesInfo { get; set; }
        public int? LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplications LocalDrivingLicenseApplicationsInfo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public decimal? PaidFees { get; set; }
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }
        public bool? IsLocked { get; set; }
        public int? RetakeTestApplicationID { get; set; }
        public clsApplications ApplicationsInfo { get; set; }


        public clsTestAppointments()
        {
            this.TestAppointmentID = null;
            this.TestTypeID = 0;
            this.LocalDrivingLicenseApplicationID = 0;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0m;
            this.CreatedByUserID = 0;
            this.IsLocked = false;
            this.RetakeTestApplicationID = 0;
            Mode = enMode.AddNew;
        }


        private clsTestAppointments(
int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID)        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.TestTypesInfo = clsTestTypes.FindByTestTypeID(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationsInfo = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.ApplicationsInfo = clsApplications.FindByApplicationID(RetakeTestApplicationID);
            Mode = enMode.Update;
        }


       private bool _AddNewTestAppointments()
       {
        this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointments(
this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        return (this.TestAppointmentID != null);
       }


       public static bool AddNewTestAppointments(
ref int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID)        {
        TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointments(
TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

        return (TestAppointmentID != null);

       }


       private bool _UpdateTestAppointments()
       {
        return clsTestAppointmentsData.UpdateTestAppointmentsByID(
this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
       }


       public static bool UpdateTestAppointmentsByID(
int? TestAppointmentID, int? TestTypeID, int? LocalDrivingLicenseApplicationID, DateTime? AppointmentDate, decimal? PaidFees, int? CreatedByUserID, bool? IsLocked, int? RetakeTestApplicationID)        {
        return clsTestAppointmentsData.UpdateTestAppointmentsByID(
TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

        }


       public static clsTestAppointments FindByTestAppointmentID(int? TestAppointmentID)

        {
            if (TestAppointmentID == null)
            {
                return null;
            }
            int? TestTypeID = 0;
            int? LocalDrivingLicenseApplicationID = 0;
            DateTime? AppointmentDate = DateTime.Now;
            decimal? PaidFees = 0m;
            int? CreatedByUserID = 0;
            bool? IsLocked = false;
            int? RetakeTestApplicationID = 0;
            bool IsFound = clsTestAppointmentsData.GetTestAppointmentsInfoByID(TestAppointmentID,
 ref TestTypeID,  ref LocalDrivingLicenseApplicationID,  ref AppointmentDate,  ref PaidFees,  ref CreatedByUserID,  ref IsLocked,  ref RetakeTestApplicationID);

           if (IsFound)
               return new clsTestAppointments(
TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
            }


       public static DataTable GetAllTestAppointments()
       {

        return clsTestAppointmentsData.GetAllTestAppointments();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestAppointments())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointments();

            }
        
            return false;
        }



       public static bool DeleteTestAppointments(int TestAppointmentID)
       {

        return clsTestAppointmentsData.DeleteTestAppointments(TestAppointmentID);

       }


        public enum TestAppointmentsColumn
         {
            TestAppointmentID,
            TestTypeID,
            LocalDrivingLicenseApplicationID,
            AppointmentDate,
            PaidFees,
            CreatedByUserID,
            IsLocked,
            RetakeTestApplicationID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(TestAppointmentsColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsTestAppointmentsData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
