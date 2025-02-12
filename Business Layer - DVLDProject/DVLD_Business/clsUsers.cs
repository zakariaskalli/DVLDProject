
using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DataLayer;

namespace DVLD_BusinessLayer
{
    public class clsUsers
    {
        //#nullable enable

        public int? UserID { get; set; }
        public int PersonID { get; set; }
        public clsPeople PeopleInfo { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }


        public clsUsers()
        {
            this.UserID = null;
            this.PersonID = 0;
            this.UserName = "";
            this.FullName = "";
            this.IsActive = false;
        }


        private clsUsers(
int? UserID, int PersonID, string UserName, string FullName, bool IsActive)        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(PersonID);
            this.UserName = UserName;
            this.FullName = FullName;
            this.IsActive = IsActive;
        }

        private clsUsers(
string UserName, int UserID, int PersonID, string FullName, bool IsActive)
        {
            this.UserName = UserName;
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PeopleInfo = clsPeople.FindByPersonID(PersonID);
            this.FullName = FullName;
            this.IsActive = IsActive;
        }


        /*
        
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

       private bool _AddNewUsers()
       {
        this.UserID = clsUsersData.AddNewUsers(
this.PersonID, this.UserName, this.FullName, this.IsActive);
        return (this.UserID != null);
       }

       private bool _UpdateUsers()
       {
        return clsUsersData.UpdateUsersByID(
this.UserID, this.PersonID, this.UserName, this.FullName, this.IsActive);
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


        */

        public static string AddNewUsers(ref int? UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            // Validate input data and return an error message instead of throwing an exception
            if (PersonID <= 0)
            {
                return "Error: PersonID must be greater than 0.";
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                return "Error: Username cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return "Error: Password cannot be empty.";
            }

            try
            {
                // Check if the username already exists to prevent duplication
                if (SearchData(clsUsers.UsersColumn.UserName, UserName, SearchMode.ExactMatch).Rows.Count > 0)
                {
                    return "Error: The username already exists. Please choose a different one.";
                }

                if (SearchData(clsUsers.UsersColumn.PersonID, PersonID.ToString(), SearchMode.ExactMatch).Rows.Count > 0)
                {
                    return "Error: The PersonID already User. Please choose a different one.";
                }

                // Attempt to add the user
                UserID = clsUsersData.AddNewUsers(PersonID, UserName, Password, IsActive);

                // Validate the result
                if (UserID == null)
                {
                    return "Error: Failed to add the user.";
                }

                return "";
            }
            catch (SqlException sqlEx)
            {
                // Handle database-specific errors
                ErrorHandler.HandleException(sqlEx, nameof(AddNewUsers), $"Parameters: PersonID={PersonID}, UserName={UserName}");
                return "Database error: Unable to add the user.";
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                ErrorHandler.HandleException(ex, nameof(AddNewUsers), $"Parameters: PersonID={PersonID}, UserName={UserName}");
                return "An unexpected error occurred while trying to add the user.";
            }
        }

        public static string UpdateUsersByID(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            // Validate input data and return an error message instead of throwing an exception
            if (UserID <= 0)
            {
                return "Error: UserID must be a valid number greater than 0.";
            }

            if (PersonID <= 0)
            {
                return "Error: PersonID must be a valid number greater than 0.";
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                return "Error: Username cannot be empty.";
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return "Error: Password cannot be empty.";
            }

            try
            {
                // Check if the user exists before attempting an update
                if (!(SearchData(clsUsers.UsersColumn.UserID, UserID.ToString(), SearchMode.ExactMatch).Rows.Count > 0))
                {
                    return "Error: No user found with the provided UserID.";
                }

                // Check if the new username is already taken by another user
                if (!(SearchData(clsUsers.UsersColumn.UserName, UserName, SearchMode.ExactMatch).Rows.Count > 0))
                {
                    return "Error: The username is already in use by another user.";
                }

                // Attempt to update the user
                bool isUpdated = clsUsersData.UpdateUsersByID(UserID, PersonID, UserName, Password, IsActive);

                return isUpdated ? "" : "Error: Failed to update the user.";
            }
            catch (SqlException sqlEx)
            {
                // Handle database-specific errors
                ErrorHandler.HandleException(sqlEx, nameof(UpdateUsersByID), $"Parameters: UserID={UserID}, PersonID={PersonID}, UserName={UserName}");
                return "Database error: Unable to update the user.";
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                ErrorHandler.HandleException(ex, nameof(UpdateUsersByID), $"Parameters: UserID={UserID}, PersonID={PersonID}, UserName={UserName}");
                return "An unexpected error occurred while trying to update the user.";
            }
        }


        public static clsUsers FindByUserID(int? UserID)
        {
            if (UserID == null)
            {
                return null;
            }

            int PersonID = 0;
            string UserName = "";
            string FullName = "";
            bool IsActive = false;
            bool IsFound = clsUsersData.GetUsersInfoByID((int)UserID,
 ref PersonID,  ref UserName,  ref FullName,  ref IsActive);

           if (IsFound)
               return new clsUsers(
UserID, PersonID, UserName, FullName, IsActive);
            else
                return null;
       }

        public static clsUsers FindByUserName(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return null;
            }
            
            int UserID = 0;
            int PersonID = 0;
            string FullName = "";
            bool IsActive = false;
            bool IsFound = clsUsersData.GetUsersInfoByUserName(UserName, ref UserID, ref PersonID, ref FullName, ref IsActive);

            if (IsFound)
                return new clsUsers(
 UserName, UserID, PersonID, FullName, IsActive);
            else
                return null;
        }


        public static DataTable GetAllUsers()
       {

        return clsUsersData.GetAllUsers();

       }

       public static bool DeleteUsers(int UserID)
       {

        return clsUsersData.DeleteUsers(UserID);

       }


        public enum UsersColumn
         {
            UserID,
            PersonID,
            FullName,
            UserName,
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

        public static string ValidateUser(string UserName, string Password,ref bool IsValid )
        {
            try
            {
                IsValid = false;
                // Basic validations for empty or invalid input
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                {
                    return "Please enter a valid username and password.";
                }

                // Check for invalid characters using a helper method (assuming SqlHelper checks for SQL injection)
                if (!SqlHelper.IsSafeInput(UserName) || !SqlHelper.IsSafeInput(Password))
                {
                    return "Invalid characters in username or password.";
                }

                // Check if the username and password length are within the valid range
                if (UserName.Length > 20 || Password.Length > 50)
                {
                    return "Username and password must be less than 50 characters.";
                }

                IsValid = clsUsersData.ValidateUser(UserName, Password);

                return IsValid != false ? "" : "This UserName/Password Is NotFound";

            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions and pass them to the error handler
                ErrorHandler.HandleException(ex, nameof(ValidateUser), $"Parameter: UserName = {UserName}, Password = {Password.Substring(0, 1)}****");  // Mask password for security
                return "An error occurred while validating the user. Please try again.";
            }
        }

        public static bool IsAccountActive(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return false;
            }

            bool isActive = clsUsersData.IsAccountActive(UserName); 

            return isActive;
        }

        public static bool IsFoundUserByNationalNo(string NationalNo)
        {
            // Basic validation to check if the NationalNo is provided and is not empty
            if (string.IsNullOrWhiteSpace(NationalNo))
            {
                return false;
            }


            // Call the Data Access Layer method to check if the user exists by NationalNo
            bool isFound = clsUsersData.IsFoundUserByNationalNo(NationalNo);

            // If user found or not, return the result
            return isFound;

        }

    }
}
