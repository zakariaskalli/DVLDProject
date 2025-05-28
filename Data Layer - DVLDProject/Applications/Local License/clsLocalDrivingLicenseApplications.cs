
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
    public class clsLocalDrivingLicenseApplicationsData
    {
        //#nullable enable

        public static bool GetLocalDrivingLicenseApplicationsInfoByID(int? LocalDrivingLicenseApplicationID , ref int? ApplicationID, ref int? LicenseClassID)
{
    bool isFound = false;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "SP_Get_LocalDrivingLicenseApplications_ByID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Ensure correct parameter assignment
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID ?? (object)DBNull.Value);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                { 
                    if (reader.Read())
                    {
                        // The record was found
                        isFound = true;

                                ApplicationID = (int)reader["ApplicationID"];
                                LicenseClassID = (int)reader["LicenseClassID"];

                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(GetLocalDrivingLicenseApplicationsInfoByID), $"Parameter: LocalDrivingLicenseApplicationID = " + LocalDrivingLicenseApplicationID);
    }

    return isFound;
}

        public static DataTable GetAllLocalDrivingLicenseApplications()
{
    DataTable dt = new DataTable();

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "SP_Get_All_LocalDrivingLicenseApplications";

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
        ErrorHandler.HandleException(ex, nameof(GetAllLocalDrivingLicenseApplications), "No parameters for this method.");
    }

    return dt;
}

        public static int? AddNewLocalDrivingLicenseApplications(int? ApplicationID, int? LicenseClassID)
    {
        int? LocalDrivingLicenseApplicationID = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SP_Add_LocalDrivingLicenseApplications";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


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
                        LocalDrivingLicenseApplicationID = (int)outputIdParam.Value;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            // Handle all exceptions in a general way
            ErrorHandler.HandleException(ex, nameof(AddNewLocalDrivingLicenseApplications), $"Parameters: int? ApplicationID, int? LicenseClassID");
        }

        return LocalDrivingLicenseApplicationID;
    }

        public static bool UpdateLocalDrivingLicenseApplicationsByID(int? LocalDrivingLicenseApplicationID, int? ApplicationID, int? LicenseClassID)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Update_LocalDrivingLicenseApplications_ByID"; 

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Create the parameters for the stored procedure
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


                // Open the connection and execute the update
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions
        ErrorHandler.HandleException(ex, nameof(UpdateLocalDrivingLicenseApplicationsByID), $"Parameter: LocalDrivingLicenseApplicationID = " + LocalDrivingLicenseApplicationID);
    }

    return (rowsAffected > 0);
}

        public static bool DeleteLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Delete_LocalDrivingLicenseApplications_ByID";  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way, this includes errors from SP_HandleError if any
        ErrorHandler.HandleException(ex, nameof(DeleteLocalDrivingLicenseApplications), $"Parameter: LocalDrivingLicenseApplicationID = " + LocalDrivingLicenseApplicationID);
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
            string query = $@"SP_Search_LocalDrivingLicenseApplications_ByColumn";

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

        public static bool IsFoundApplicationMatchLocalDriveByNationalNo(string NationalNo, int LicenseClassID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    string query = "SP_IsFoundApplicationMatchLocalDriveByNationalNo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        IsFound = result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex, nameof(IsFoundApplicationMatchLocalDriveByNationalNo),
                    $"Parameters: NationalNo = {NationalNo}, LicenseClassID = {LicenseClassID}");
            }

            return IsFound;
        }

        public static bool IsFoundApplicationMatchLocalDriveByPersonID(int PersonID, int LicenseClassID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    string query = "SP_IsFoundApplicationMatchLocalDriveByPersonID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        IsFound = result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex, nameof(IsFoundApplicationMatchLocalDriveByPersonID),
                    $"Parameters: PersonID = {PersonID}, LicenseClassID = {LicenseClassID}");
            }

            return IsFound;
        }

        public static int ApplicationNumMatchPersonIDAndLicenseClassID(int PersonID, int LicenseClassID)
        {
            int applicationNum = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    string query = "SP_GetApplicationNumberByPersonIDAndLicenseClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int appNum))
                        {
                            applicationNum = appNum;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex, nameof(ApplicationNumMatchPersonIDAndLicenseClassID),
                    $"Parameters: PersonID = {PersonID}, LicenseClassID = {LicenseClassID}");
            }

            return applicationNum;
        }

        public static int ApplicationNumMatchNationalNoAndLicenseClassID(string NationalNo, int LicenseClassID)
        {
            int applicationNum = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    string query = "SP_GetApplicationNumberByNationalNoAndLicenseClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int appNum))
                        {
                            applicationNum = appNum;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex, nameof(ApplicationNumMatchNationalNoAndLicenseClassID),
                    $"Parameters: NationalNo = {NationalNo}, LicenseClassID = {LicenseClassID}");
            }

            return applicationNum;
        }

        public static bool CancelLicenseByNationalNoAndLicenseClassID(string NationalNo, int LicenseClassID)
        {
            bool isCancelled = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    string query = "SP_CancelLicenseByNationalNoAndLicenseClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        isCancelled = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex, nameof(CancelLicenseByNationalNoAndLicenseClassID),
                    $"Parameters: NationalNo = {NationalNo}, LicenseClassID = {LicenseClassID}");
            }

            return isCancelled;
        }

    }
}
