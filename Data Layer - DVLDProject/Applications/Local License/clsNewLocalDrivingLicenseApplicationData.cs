using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToSQL;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Collections.Specialized;

namespace Data_Layer___DVLDProject
{
    public class clsNewLocalDrivingLicenseApplicationData
    {

        public static int ApplicationFeesNewLocalDrivingLicenseService()
        {
            int Fees = -1;


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"select ApplicationFees from ApplicationTypes
                                where ApplicationTypeID = 1";

            SqlCommand Command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && decimal.TryParse(result.ToString(), out decimal TheFees))
                    Fees = (int)TheFees;
                else
                    Fees = -1;


            }
            finally
            {
                connection.Close();
            }

            return Fees;
        }

        public static List<string> LoadAllApplicationTypes()
        {
            List<string> dataList = new List<string>();


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"select ClassName from LicenseClasses";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    dataList.Add(reader["ClassName"].ToString());

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dataList;
        }


        public static bool IsFoundApplicationMatchLocalDriveByPersonID(int PersonID, int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 Found = 1 from LocalDrivingApplicationTable
                                where NationalNo = (select NationalNo from People where PersonID = @PersonID)
                                and [Driving Class] =  (select ClassName from LicenseClasses where LicenseClassID = @LicenseClassID) 
                                    and Status != 'Cancelled'";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
                else
                    IsFound = false;

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool IsFoundApplicationMatchLocalDriveByNationalNo(string NationalNo, int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 Found = 1 from LocalDrivingApplicationTable
                                where NationalNo = @NationalNo
                                and [Driving Class] =  (select ClassName from LicenseClasses where LicenseClassID = @LicenseClassID) 
                                		and Status != 'Cancelled'";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
                else
                    IsFound = false;

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static int AddLocalDriveLicAppToTableByPersonID(int PersonID, int UserID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"DECLARE @NewApplicationID INT;
                                
                                INSERT INTO Applications
                                           (ApplicantPersonID,
                                            ApplicationDate,
                                            ApplicationTypeID,
                                            ApplicationStatus,
                                            LastStatusDate,
                                            PaidFees,
                                            CreatedByUserID)
                                     VALUES
                                           (@PersonID,
                                           GETDATE(),
                                           1,
                                           1,
                                           GETDATE(),
                                           (

                                            select ApplicationFees from ApplicationTypes
                                            where ApplicationTypeID = 1

                                            ),
                                           @UserID);
                                
                                SET @NewApplicationID = SCOPE_IDENTITY();
                                
                                
                                INSERT INTO LocalDrivingLicenseApplications
                                           (ApplicationID,
                                            LicenseClassID)
                                     VALUES
                                           (@NewApplicationID,
                                            @LicenseClassID)
                                        select SCOPE_IDENTITY();";



            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                    LocalDrivingLicenseApplicationID = InsertID;
                else
                    LocalDrivingLicenseApplicationID = -1;


            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;

        }

        public static int AddLocalDriveLicAppToTableByNationalNo(string NationalNo, int UserID, int LicenseClassID)
        {

            int LocalDrivingLicenseApplicationID = -1;


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"DECLARE @NewApplicationID INT;
                                
                                INSERT INTO Applications
                                           (ApplicantPersonID,
                                            ApplicationDate,
                                            ApplicationTypeID,
                                            ApplicationStatus,
                                            LastStatusDate,
                                            PaidFees,
                                            CreatedByUserID)
                                     VALUES
                                           (
							                    (Select S1.PersonID from
				                                (SELECT      People.PersonID from People
				                            where NationalNo = @NationalNo)S1),
                                           GETDATE(),
                                           1,
                                           1,
                                           GETDATE(),
                                           (

                                            select ApplicationFees from ApplicationTypes
                                            where ApplicationTypeID = 1

                                            ),
                                           @UserID);
                                
                                SET @NewApplicationID = SCOPE_IDENTITY();
                                
                                
                                INSERT INTO LocalDrivingLicenseApplications
                                           (ApplicationID,
                                            LicenseClassID)
                                     VALUES
                                           (@NewApplicationID,
                                            @LicenseClassID)
                                        select SCOPE_IDENTITY();";



            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);



            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                    LocalDrivingLicenseApplicationID = InsertID;
                else
                    LocalDrivingLicenseApplicationID = -1;


            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;

        }


        static public bool UpdateLocalDriveLicAppToTable(int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"UPDATE Applications
                                   SET LastStatusDate = GetDate()
                                      WHERE ApplicationID = (
                                								select ApplicationID from LocalDrivingLicenseApplications
                                							WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                                							)
                                
                                
                                 UPDATE LocalDrivingLicenseApplications
                                   SET LicenseClassID = @LicenseClassID
                                WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";


            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();

                if (result > 0)
                    IsUpdate = true;
                else
                    IsUpdate = false;


            }
            finally
            {
                connection.Close();
            }

            return IsUpdate;
        }

        static public void LoadAllDataUserByLocalDriveLicAppID(int LocalDrivingLicenseApplicationID, ref int DrivingID,
            ref int PersonID, ref int LicenseID, ref int UserID, ref DateTime ApplicationDate)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT        LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS 'L.D.L.AppID', LicenseClasses.LicenseClassID AS 'Driving ID', Applications.ApplicationDate AS 'Application Date', 
                            LocalDrivingLicenseApplications.LicenseClassID, Applications.CreatedByUserID, People.PersonID
                            FROM            LocalDrivingLicenseApplications INNER JOIN
                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
						 WHERE        (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)";


            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    DrivingID = (int)reader["Driving ID"];
                    PersonID = (int)reader["PersonID"];
                    ApplicationDate = (DateTime)reader["Application Date"];
                    LicenseID = (int)reader["LicenseClassID"];
                    UserID = (int)reader["CreatedByUserID"];
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

        }


    }
}
