
using System;
using System.Data;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsUsers
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? UserID { get; set; }
        public int? PersonID { get; set; }
        public clsPeople PeopleInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }


        public clsUsers()
        {
            this.UserID = null;
            this.PersonID = 0;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }


        private clsUsers(
int? UserID, int? PersonID, string UserName, string Password, bool? IsActive)        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }


       private bool _AddNewUsers()
       {
        this.UserID = clsUsersData.AddNewUsers(
this.PersonID, this.UserName, this.Password, this.IsActive);
        return (this.UserID != null);
       }


       public static bool AddNewUsers(
ref int? UserID, int? PersonID, string UserName, string Password, bool? IsActive)        {
        UserID = clsUsersData.AddNewUsers(
PersonID, UserName, Password, IsActive);

        return (UserID != null);

       }


       private bool _UpdateUsers()
       {
        return clsUsersData.UpdateUsersByID(
this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
       }


       public static bool UpdateUsersByID(
int? UserID, int? PersonID, string UserName, string Password, bool? IsActive)        {
        return clsUsersData.UpdateUsersByID(
UserID, PersonID, UserName, Password, IsActive);

        }


       public static clsUsers FindByUserID(int? UserID)

        {
            if (UserID == null)
            {
                return null;
            }
            int? PersonID = 0;
            string UserName = "";
            string Password = "";
            bool? IsActive = false;
            bool IsFound = clsUsersData.GetUsersInfoByID(UserID,
 ref PersonID,  ref UserName,  ref Password,  ref IsActive);

           if (IsFound)
               return new clsUsers(
UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
            }


       public static DataTable GetAllUsers()
       {

        return clsUsersData.GetAllUsers();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewUsers())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUsers();

            }
        
            return false;
        }



       public static bool DeleteUsers(int UserID)
       {

        return clsUsersData.DeleteUsers(UserID);

       }


        public enum UsersColumn
         {
            UserID,
            PersonID,
            UserName,
            Password,
            IsActive
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(UsersColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsUsersData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}
