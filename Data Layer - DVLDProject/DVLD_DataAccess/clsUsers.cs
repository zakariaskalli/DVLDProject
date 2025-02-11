
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using DVLD_DataAccess;
using Newtonsoft.Json;
using ConnectionToSQL;

namespace DVLD_DataLayer
{
    public class clsUsersData
    {
        //#nullable enable

        public static bool GetUsersInfoByID(int UserID, ref int PersonID, ref string UserName, ref string FullName, ref bool IsActive)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SP_Get_Users_ByID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Ensure correct parameter assignment
                        command.Parameters.AddWithValue("@UserID", UserID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = reader["PersonID"] != DBNull.Value ? (int)reader["PersonID"] : 0; 

                                UserName = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : string.Empty; 

                                FullName = reader["FullName"] != DBNull.Value ? (string)reader["FullName"] : string.Empty; 

                                IsActive = reader["IsActive"] != DBNull.Value ? (bool)reader["IsActive"] : false; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle all exceptions in a general way
                ErrorHandler.HandleException(ex, nameof(GetUsersInfoByID), $"Parameter: UserID = {UserID}");
                throw; // Rethrow the exception to propagate it up the call stack
            }

            return isFound;
        }
        
        public static DataTable GetAllUsers()
{
    DataTable dt = new DataTable();

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "SP_Get_All_Users";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure; 

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(GetAllUsers), "No parameters for this method.");
    }

    return dt;
}

        public static int? AddNewUsers(int? PersonID, string UserName, string Password, bool? IsActive)
    {
        int? UserID = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SP_Add_Users";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);


                    SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Bring added value
                    if (outputIdParam.Value != DBNull.Value)
                    {
                        UserID = (int)outputIdParam.Value;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            // Handle all exceptions in a general way
            ErrorHandler.HandleException(ex, nameof(AddNewUsers), $"Parameters: int? PersonID, string UserName, string Password, bool? IsActive");
        }

        return UserID;
    }

        public static bool UpdateUsersByID(int? UserID, int PersonID, string UserName, string Password, bool IsActive)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Update_Users_ByID"; 

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Create the parameters for the stored procedure
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);


                // Open the connection and execute the update
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions
        ErrorHandler.HandleException(ex, nameof(UpdateUsersByID), $"Parameter: UserID = " + UserID);
    }

    return (rowsAffected > 0);
}

        public static bool DeleteUsers(int UserID)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Delete_Users_ByID";  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", UserID);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way, this includes errors from SP_HandleError if any
        ErrorHandler.HandleException(ex, nameof(DeleteUsers), $"Parameter: UserID = " + UserID);
    }

    return (rowsAffected > 0);
}
        
        public static DataTable SearchData(string ColumnName, string SearchValue, string Mode = "Anywhere")
{
    DataTable dt = new DataTable();

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Search_Users_ByColumn";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ColumnName", ColumnName);
                command.Parameters.AddWithValue("@SearchValue", SearchValue);
                command.Parameters.AddWithValue("@Mode", Mode);  // Added Mode parameter

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                    reader.Close();
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(SearchData), $"ColumnName: {ColumnName}, SearchValue: {SearchValue}, Mode: {Mode}");
    }

    return dt;
}

        public static bool ValidateUser(string UserName, string Password)
        {
            bool IsValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    // Prepare parameters for the stored procedure
                    SqlCommand command = new SqlCommand("SP_ValidateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);

                    // Output parameter for validation result
                    SqlParameter outputIsValid = new SqlParameter("@IsValid", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIsValid);

                    connection.Open();

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    if (outputIsValid.Value != DBNull.Value)
                    {
                        IsValid = (bool)outputIsValid.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                ErrorHandler.HandleException(ex, nameof(ValidateUser), $"Parameter: UserName = {UserName}");
            }

            return IsValid;
        }

        static public bool IsAccountActive(string UserName)
        {
            bool IsActive = false;

            using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
            {
                string query = "SP_IsAccountActive"; // Use the stored procedure

                using (SqlCommand Command = new SqlCommand(query, connection))
                {
                    Command.CommandType = CommandType.StoredProcedure; // Specify it's a stored procedure
                    Command.Parameters.AddWithValue("@UserName", UserName);

                    SqlParameter outputParam = new SqlParameter("@IsActive", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParam);

                    try
                    {
                        connection.Open();
                        Command.ExecuteNonQuery();

                        // Retrieve the value from the output parameter
                        IsActive = outputParam.Value != DBNull.Value && (bool)outputParam.Value;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception here if needed
                        ErrorHandler.HandleException(ex, nameof(IsAccountActive), $"Parameter: UserName = {UserName}");
                    }
                }
            }

            return IsActive;
        }

        public static bool IsFoundUserByNationalNo(string NationalNo)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    // Define the stored procedure name
                    string query = "SP_IsFoundUserByNationalNo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameter for the NationalNo
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open();

                        // Execute the query and check if any result is returned
                        object result = command.ExecuteScalar();
                        IsFound = result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception and log it
                ErrorHandler.HandleException(ex, nameof(IsFoundUserByNationalNo), $"Parameter: NationalNo = {NationalNo}");
            }

            return IsFound;
        }

    }
}
