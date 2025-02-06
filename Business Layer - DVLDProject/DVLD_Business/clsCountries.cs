
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsCountries
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? CountryID { get; set; }
        public string CountryName { get; set; }


        public clsCountries()
        {
            this.CountryID = null;
            this.CountryName = "";
            Mode = enMode.AddNew;
        }


        private clsCountries(
int? CountryID, string CountryName)        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }


       private bool _AddNewCountries()
       {
        this.CountryID = clsCountriesData.AddNewCountries(
this.CountryName);
        return (this.CountryID != null);
       }


       public static bool AddNewCountries(
ref int? CountryID, string CountryName)        {
        CountryID = clsCountriesData.AddNewCountries(
CountryName);

        return (CountryID != null);

       }


       private bool _UpdateCountries()
       {
        return clsCountriesData.UpdateCountriesByID(
this.CountryID, this.CountryName);
       }


       public static bool UpdateCountriesByID(
int? CountryID, string CountryName)        {
        return clsCountriesData.UpdateCountriesByID(
CountryID, CountryName);

        }


       public static clsCountries FindByCountryID(int? CountryID)

        {
            if (CountryID == null)
            {
                return null;
            }
            string CountryName = "";
            bool IsFound = clsCountriesData.GetCountriesInfoByID(CountryID,
 ref CountryName);

           if (IsFound)
               return new clsCountries(
CountryID, CountryName);
            else
                return null;
            }


       public static DataTable GetAllCountries()
       {

        return clsCountriesData.GetAllCountries();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewCountries())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountries();

            }
        
            return false;
        }



       public static bool DeleteCountries(int CountryID)
       {

        return clsCountriesData.DeleteCountries(CountryID);

       }


        public enum CountriesColumn
         {
            CountryID,
            CountryName
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(CountriesColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsCountriesData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
