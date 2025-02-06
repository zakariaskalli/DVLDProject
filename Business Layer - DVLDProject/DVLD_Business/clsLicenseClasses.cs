
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsLicenseClasses
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte? MinimumAllowedAge { get; set; }
        public byte? DefaultValidityLength { get; set; }
        public decimal? ClassFees { get; set; }


        public clsLicenseClasses()
        {
            this.LicenseClassID = null;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = default(byte);
            this.DefaultValidityLength = default(byte);
            this.ClassFees = 0m;
            Mode = enMode.AddNew;
        }


        private clsLicenseClasses(
int? LicenseClassID, string ClassName, string ClassDescription, byte? MinimumAllowedAge, byte? DefaultValidityLength, decimal? ClassFees)        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }


       private bool _AddNewLicenseClasses()
       {
        this.LicenseClassID = clsLicenseClassesData.AddNewLicenseClasses(
this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        return (this.LicenseClassID != null);
       }


       public static bool AddNewLicenseClasses(
ref int? LicenseClassID, string ClassName, string ClassDescription, byte? MinimumAllowedAge, byte? DefaultValidityLength, decimal? ClassFees)        {
        LicenseClassID = clsLicenseClassesData.AddNewLicenseClasses(
ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

        return (LicenseClassID != null);

       }


       private bool _UpdateLicenseClasses()
       {
        return clsLicenseClassesData.UpdateLicenseClassesByID(
this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
       }


       public static bool UpdateLicenseClassesByID(
int? LicenseClassID, string ClassName, string ClassDescription, byte? MinimumAllowedAge, byte? DefaultValidityLength, decimal? ClassFees)        {
        return clsLicenseClassesData.UpdateLicenseClassesByID(
LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

        }


       public static clsLicenseClasses FindByLicenseClassID(int? LicenseClassID)

        {
            if (LicenseClassID == null)
            {
                return null;
            }
            string ClassName = "";
            string ClassDescription = "";
            byte? MinimumAllowedAge = default(byte);
            byte? DefaultValidityLength = default(byte);
            decimal? ClassFees = 0m;
            bool IsFound = clsLicenseClassesData.GetLicenseClassesInfoByID(LicenseClassID,
 ref ClassName,  ref ClassDescription,  ref MinimumAllowedAge,  ref DefaultValidityLength,  ref ClassFees);

           if (IsFound)
               return new clsLicenseClasses(
LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
            }


       public static DataTable GetAllLicenseClasses()
       {

        return clsLicenseClassesData.GetAllLicenseClasses();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenseClasses())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenseClasses();

            }
        
            return false;
        }



       public static bool DeleteLicenseClasses(int LicenseClassID)
       {

        return clsLicenseClassesData.DeleteLicenseClasses(LicenseClassID);

       }


        public enum LicenseClassesColumn
         {
            LicenseClassID,
            ClassName,
            ClassDescription,
            MinimumAllowedAge,
            DefaultValidityLength,
            ClassFees
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(LicenseClassesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsLicenseClassesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
