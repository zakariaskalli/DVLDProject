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

namespace Data_Layer___DVLDProject
{
    public class clsMethodsGeneralData
    {
        static public int PersonIDByLDLAppID(int LDLAppID)
        {

            int PersonID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LDLAppID INT;
                                
                                Set @LDLAppID = @@LDLAppID
                                
                                select PersonID from People
                                where NationalNo = (select NationalNo from LocalDrivingApplicationTable
                                						where LDLAppID = @LDLAppID ) ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    PersonID = Convert.ToInt32(result);


            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        static public string NameCountryByNumber(int NationalityCountryID)
        {
            string CountryName = "";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select CountryName from Countries
                            where CountryID = @NationalityCountryID; ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    CountryName = (string)result;

            }
            finally
            {
                connection.Close();
            }

            return CountryName;
        }

        static public int PersonIDByNationalNo(string NationalNo)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select PersonID from People
                                where NationalNo = @NationalNo; ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    PersonID = Convert.ToInt32(result);

            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool IsLicenseActiveByLicenseID(int PersonID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Found = 1 from Users
                                where PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsFoundUserByNationalNo(string NationalNo)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT        Found = 1
                                FROM            Users INNER JOIN
                                People ON Users.PersonID = People.PersonID
                                where NationalNo = @NationalNo;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool IsFoundUserByPersonID(int PersonID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 Find from Users
                                    where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsUserNameFound(string UserName)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Found = 1 from Users
                            where UserName = @UserName;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserName", UserName);

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

        static public int UserIDByUserName(string UserName)
        {

            int UserID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select UserID from Users
                                where UserName = @UserName";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                    UserID = InsertID;
                else
                    UserID = -1;



            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        static public string UserNameByUserID(int UserID)
        {

            string UserName = "";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select UserName from Users
                                where UserID = @UserID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();


                if (result != null)
                    UserName = Convert.ToString(result.ToString());
                else
                    UserName = "";



            }
            finally
            {
                connection.Close();
            }

            return UserName;
        }

        static public bool IsLDLAppIDFound(int LDLAppID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 Found from LocalDrivingLicenseApplications
                                where LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public int LicenseNumberbyLicenseName(string LicenseName)
        {
            int LicenseNumber = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select LicenseClassID from LicenseClasses
                                where ClassName = @LicenseName";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LicenseName", LicenseName);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();


                if (result != null)
                    LicenseNumber = Convert.ToInt32(result.ToString());
                else
                    LicenseNumber = -1;


            }
            finally
            {
                connection.Close();
            }

            return LicenseNumber;

        }

        static public string NationalNoByLDLAppID(int LDLAppID)
        {
            string NationalNo = "";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select NationalNo from LocalDrivingApplicationTable
                                where LDLAppID = @LDLAppID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();


                if (result != null)
                    NationalNo = (string)result.ToString();
                else
                    NationalNo = "";


            }
            finally
            {
                connection.Close();
            }

            return NationalNo;
        }

        public static bool IsFoundLicenseByLicenseID(int LicenseID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 Find from Licenses
                                    where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        static public int LDLAppIDByLicenseID(int LicenseID)
        {

            int License_ID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select LocalDrivingLicenseApplicationID from LocalDrivingLicenseApplications
                                where ApplicationID = (select ApplicationID from Licenses
                                									where LicenseID = @LicenseID) ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    License_ID = Convert.ToInt32(result);


            }
            finally
            {
                connection.Close();
            }

            return License_ID;
        }

        static public int FeesApplicationTypesByApplicationTypeID(int ApplicationTypeID)
        {

            int Fees = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select ApplicationFees from ApplicationTypes
                                    where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    Fees = Convert.ToInt32(result);


            }
            finally
            {
                connection.Close();
            }

            return Fees;
        }

        static public bool IsInternationalLicenseIDFound(int InternationalLicenseID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 Found  from InternationalLicenses
                                    where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public int PersonIDByApplicationID(int ApplicationID)
        {

            int PersonID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @ApplicationID INT;
                                
                                Set @ApplicationID = @@ApplicationID
                                
                                select PersonID from People
								where PersonID =  (select ApplicantPersonID from Applications where ApplicationID = @ApplicationID)";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@ApplicationID", ApplicationID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    PersonID = Convert.ToInt32(result);


            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        static public void UpdateDataFormAllLicenses()
        {
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"UPDATE Licenses
                                SET IsActive = 0
                                WHERE ExpirationDate < GETDATE();
                                
                                
                                UPDATE InternationalLicenses
                                SET IsActive = 0
                                FROM InternationalLicenses l
                                JOIN Licenses i ON l.IssuedUsingLocalLicenseID = i.LicenseID
                                WHERE l.ExpirationDate < GETDATE()
                                	or i.IsActive = 0;";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                int result = Command.ExecuteNonQuery();

            }
            finally
            {
                connection.Close();
            }
        }

        static public bool IsLicenseDetained(int LicenseID)
        {

            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LicenseID INT;
                                
                                set @LicenseID = @@LicenseID;
                                
                                select top 1 1 from Licenses
                                where LicenseID = @LicenseID and Exists(select top 1 1 from DetainedLicenses 
                                                        where LicenseID = @LicenseID and IsReleased = 0 order by DetainID desc)";

            SqlCommand Command = new SqlCommand(query, connection);
            
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IsHaveDataInLicense(int LicenseID)
        {

            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"

                                Declare @LicenseID Int, @LDLAppID INT, @AppID INT, @LicenseClassID INT, @PersonID INT;
                       
								set @LicenseID = @@LicenseID

                                Set @LDLAppID = (select LocalDrivingLicenseApplicationID from LocalDrivingLicenseApplications 
																where ApplicationID = (select ApplicationID from Licenses 
																									where LicenseID = @LicenseID));
                                
                                Set @AppID = (select ApplicationID from LocalDrivingLicenseApplications
                                					where LocalDrivingLicenseApplicationID = @LDLAppID)
                                
                                
                                Set @LicenseClassID = (select LicenseClassID from LocalDrivingLicenseApplications
                                					where LocalDrivingLicenseApplicationID = @LDLAppID)
                                
                                Set @PersonID = (select PersonID from  People
                                								where NationalNo = 
                                								( select NationalNo from LocalDrivingApplicationTable 
                                													where LDLAppID = @LDLAppID))
                                select top 1 1 Find From (
                                select ( select ClassName from LicenseClasses where LicenseClassID = LicenseClass) As 'Class',
                                (select FullName from LocalDrivingApplicationTable where LDLAppID = @LDLAppID) As 'Name',
                                LicenseID,
                                (select NationalNo from People where PersonID = @PersonID) As 'NationalNo',
                                (select Gendor from People where PersonID = @PersonID) As 'Gendor', 
                                
                                (SELECT FORMAT(CAST(IssueDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'IssueDate',
                                IssueReason,
                                Notes,
                                Licenses.IsActive,
								(SELECT FORMAT(CAST((select DateOfBirth from People where PersonID = @PersonID) AS DATETIME), 'dd/MMMM/yyyy')) AS 'DateOfBirth',
                                DriverID,
								(SELECT FORMAT(CAST(ExpirationDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'ExpirationDate',
                                    CASE WHEN EXISTS (SELECT 1 FROM DetainedLicenses WHERE LicenseID = Licenses.LicenseID and IsReleased = 0) 
                                         THEN 1 
                                         ELSE 0 
                                    END AS IsDetained,
                                (select ImagePath from People where PersonID = @PersonID) As 'ImagePath'
                                from Licenses
                                where ApplicationID = @AppID And LicenseClass = @LicenseClassID )S1";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IsDetainedIDFound(int DetainID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 from DetainedLicenses
                                where DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IsDetainedCompleteByLicenseID(int LicenseID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1  from DetainedLicenses
                                where LicenseID = @LicenseID and 
                                    (select top 1 IsReleased from DetainedLicenses 
                                                                    where  LicenseID = @LicenseID order by DetainID desc ) = 1 ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public int DetainedIDByLicenseID(int LicenseID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 DetainID  from DetainedLicenses
                                where LicenseID = @LicenseID 
                                order by DetainID desc";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    DetainID = Convert.ToInt32(result);

            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        static public int LicenseIDByLDLAppID(int LDLAppID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LDLAppID Int;

                                set @LDLAppID = @@LDLAppID;
                                
                                
                                select LicenseID from LocalDrivingLicenseApplications
                                Join Licenses ON LocalDrivingLicenseApplications.ApplicationID = Licenses.ApplicationID 
                                where LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    LicenseID = Convert.ToInt32(result);

            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }
    }
}
