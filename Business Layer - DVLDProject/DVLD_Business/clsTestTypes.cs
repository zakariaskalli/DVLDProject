
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsTestTypes
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal? TestTypeFees { get; set; }


        public clsTestTypes()
        {
            this.TestTypeID = null;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0m;
            Mode = enMode.AddNew;
        }


        private clsTestTypes(
int? TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal? TestTypeFees)        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            Mode = enMode.Update;
        }


       private bool _AddNewTestTypes()
       {
        this.TestTypeID = clsTestTypesData.AddNewTestTypes(
this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        return (this.TestTypeID != null);
       }


       public static bool AddNewTestTypes(
ref int? TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal? TestTypeFees)        {
        TestTypeID = clsTestTypesData.AddNewTestTypes(
TestTypeTitle, TestTypeDescription, TestTypeFees);

        return (TestTypeID != null);

       }


       private bool _UpdateTestTypes()
       {
        return clsTestTypesData.UpdateTestTypesByID(
this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
       }


       public static bool UpdateTestTypesByID(
int? TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal? TestTypeFees)        {
        return clsTestTypesData.UpdateTestTypesByID(
TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);

        }


       public static clsTestTypes FindByTestTypeID(int? TestTypeID)

        {
            if (TestTypeID == null)
            {
                return null;
            }
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal? TestTypeFees = 0m;
            bool IsFound = clsTestTypesData.GetTestTypesInfoByID(TestTypeID,
 ref TestTypeTitle,  ref TestTypeDescription,  ref TestTypeFees);

           if (IsFound)
               return new clsTestTypes(
TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;
            }


       public static DataTable GetAllTestTypes()
       {

        return clsTestTypesData.GetAllTestTypes();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestTypes())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestTypes();

            }
        
            return false;
        }



       public static bool DeleteTestTypes(int TestTypeID)
       {

        return clsTestTypesData.DeleteTestTypes(TestTypeID);

       }


        public enum TestTypesColumn
         {
            TestTypeID,
            TestTypeTitle,
            TestTypeDescription,
            TestTypeFees
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(TestTypesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsTestTypesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
