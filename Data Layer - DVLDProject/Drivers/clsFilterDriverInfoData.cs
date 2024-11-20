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
    public class clsFilterDriverInfoData
    {
        public static bool IsAllLicensesActiveByLicenseID(int LicenseID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"declare @LicenseID Int, @DriverID Int, @LicenseClass Int;
                                --25
                                set @LicenseID = @@LicenseID;
                                --19
                                set @DriverID = (select DriverID from Licenses where LicenseID = @LicenseID)
                                -- 1 
                                set @LicenseClass = (select LicenseClass from Licenses where LicenseID = @LicenseID)
                                
                                
                                select top 1 1 from Licenses where DriverID = @DriverID and LicenseClass = @LicenseClass and IsActive = 1
                                order by LicenseID desc ";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);

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

        public static bool IsJustLicenseActiveByLicenseID(int LicenseID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"declare @LicenseID Int, @DriverID Int, @LicenseClass Int;
                                --25
                                set @LicenseID = @@LicenseID;
                                --19
                                set @DriverID = (select DriverID from Licenses where LicenseID = @LicenseID)
                                -- 1 
                                set @LicenseClass = (select LicenseClass from Licenses where LicenseID = @LicenseID)
                                
                                select * from Licenses where LicenseID = @LicenseID and IsActive = 1 ";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);

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


        public static int IHaveInternationalLicenseByLicenseID(int LicenseID)
        {

            int InternationalID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LicenseID INT;
                                    Set @LicenseID = @@LicenseID;

                                    select 
                                        CASE 
                                            WHEN EXISTS (
                                                select 1 from InternationalLicenses where IssuedUsingLocalLicenseID = @LicenseID and IsActive = 1
                                            ) 
                                            THEN (select InternationalLicenseID from InternationalLicenses where IssuedUsingLocalLicenseID = @LicenseID
											and IsActive = 1)
                                            ELSE -1 
                                        END as InternationalLicenseID;
";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    InternationalID = Convert.ToInt32(result);
                else
                    InternationalID = -1;

            }
            finally
            {
                connection.Close();
            }

            return InternationalID;
        }

        public static bool IsLicensePastExpirationDate(int LicenseID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 1 Find from Licenses
                                where LicenseID = @LicenseID and ExpirationDate <= GETDATE()";

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

        public static string ExpirationDateLicenseByLicenseID(int LicenseID)
        {

            string DateExpirationDate = "";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select (SELECT FORMAT(CAST(ExpirationDate AS DATETIME), 'dd/MMM/yyyy')) AS 'ExpirationDate' from Licenses
                                    where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null)
                    DateExpirationDate = (string)result;
                else
                    DateExpirationDate = "";

            }
            finally
            {
                connection.Close();
            }

            return DateExpirationDate;
        }


    }
}
