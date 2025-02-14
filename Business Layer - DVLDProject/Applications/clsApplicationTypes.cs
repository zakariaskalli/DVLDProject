
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsApplicationTypes
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal? ApplicationFees { get; set; }


        public clsApplicationTypes()
        {
            this.ApplicationTypeID = null;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0m;
            Mode = enMode.AddNew;
        }


        private clsApplicationTypes(
int? ApplicationTypeID, string ApplicationTypeTitle, decimal? ApplicationFees)        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }


       private bool _AddNewApplicationTypes()
       {
        this.ApplicationTypeID = clsApplicationTypesData.AddNewApplicationTypes(
this.ApplicationTypeTitle, this.ApplicationFees);
        return (this.ApplicationTypeID != null);
       }


       public static bool AddNewApplicationTypes(
ref int? ApplicationTypeID, string ApplicationTypeTitle, decimal? ApplicationFees)        {
        ApplicationTypeID = clsApplicationTypesData.AddNewApplicationTypes(
ApplicationTypeTitle, ApplicationFees);

        return (ApplicationTypeID != null);

       }


       private bool _UpdateApplicationTypes()
       {
        return clsApplicationTypesData.UpdateApplicationTypesByID(
this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
       }


       public static bool UpdateApplicationTypesByID(
int? ApplicationTypeID, string ApplicationTypeTitle, decimal? ApplicationFees)        {
        return clsApplicationTypesData.UpdateApplicationTypesByID(
ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

        }


       public static clsApplicationTypes FindByApplicationTypeID(int? ApplicationTypeID)

        {
            if (ApplicationTypeID == null)
            {
                return null;
            }
            string ApplicationTypeTitle = "";
            decimal? ApplicationFees = 0m;
            bool IsFound = clsApplicationTypesData.GetApplicationTypesInfoByID(ApplicationTypeID,
 ref ApplicationTypeTitle,  ref ApplicationFees);

           if (IsFound)
               return new clsApplicationTypes(
ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;
            }


       public static DataTable GetAllApplicationTypes()
       {

        return clsApplicationTypesData.GetAllApplicationTypes();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplicationTypes())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplicationTypes();

            }
        
            return false;
        }



       public static bool DeleteApplicationTypes(int ApplicationTypeID)
       {

        return clsApplicationTypesData.DeleteApplicationTypes(ApplicationTypeID);

       }


        public enum ApplicationTypesColumn
         {
            ApplicationTypeID,
            ApplicationTypeTitle,
            ApplicationFees
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(ApplicationTypesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsApplicationTypesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
