
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsTests
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? TestID { get; set; }
        public int? TestAppointmentID { get; set; }
        public clsTestAppointments TestAppointmentsInfo { get; set; }
        public bool? TestResult { get; set; }
        public string Notes { get; set; } = null;
        public int? CreatedByUserID { get; set; }
        public clsUsers UsersInfo { get; set; }


        public clsTests()
        {
            this.TestID = null;
            this.TestAppointmentID = 0;
            this.TestResult = false;
            this.Notes = null;
            this.CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }


        private clsTests(
int? TestID, int? TestAppointmentID, bool? TestResult, int? CreatedByUserID, string Notes = null)        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentsInfo = clsTestAppointments.FindByTestAppointmentID(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.UsersInfo = clsUsers.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }


       private bool _AddNewTests()
       {
        this.TestID = clsTestsData.AddNewTests(
this.TestAppointmentID, this.TestResult, this.CreatedByUserID, this.Notes);
        return (this.TestID != null);
       }


       public static bool AddNewTests(
ref int? TestID, int? TestAppointmentID, bool? TestResult, int? CreatedByUserID, string Notes = null)        {
        TestID = clsTestsData.AddNewTests(
TestAppointmentID, TestResult, CreatedByUserID, Notes);

        return (TestID != null);

       }


       private bool _UpdateTests()
       {
        return clsTestsData.UpdateTestsByID(
this.TestID, this.TestAppointmentID, this.TestResult, this.CreatedByUserID, this.Notes);
       }


       public static bool UpdateTestsByID(
int? TestID, int? TestAppointmentID, bool? TestResult, int? CreatedByUserID, string Notes = null)        {
        return clsTestsData.UpdateTestsByID(
TestID, TestAppointmentID, TestResult, CreatedByUserID, Notes);

        }


       public static clsTests FindByTestID(int? TestID)

        {
            if (TestID == null)
            {
                return null;
            }
            int? TestAppointmentID = 0;
            bool? TestResult = false;
            string Notes = "";
            int? CreatedByUserID = 0;
            bool IsFound = clsTestsData.GetTestsInfoByID(TestID,
 ref TestAppointmentID,  ref TestResult,  ref Notes,  ref CreatedByUserID);

           if (IsFound)
               return new clsTests(
TestID, TestAppointmentID, TestResult, CreatedByUserID, Notes);
            else
                return null;
            }


       public static DataTable GetAllTests()
       {

        return clsTestsData.GetAllTests();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTests())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTests();

            }
        
            return false;
        }



       public static bool DeleteTests(int TestID)
       {

        return clsTestsData.DeleteTests(TestID);

       }


        public enum TestsColumn
         {
            TestID,
            TestAppointmentID,
            TestResult,
            Notes,
            CreatedByUserID
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(TestsColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsTestsData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
