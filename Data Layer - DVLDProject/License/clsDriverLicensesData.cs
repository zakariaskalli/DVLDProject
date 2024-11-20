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
    public class clsDriverLicensesData
    {

        public DataTable LoadAllLDLApp(int LDLAppID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" Declare @LDLAppID INT;

                    set @LDLAppID = @@LDLAppID;
                    
                    
                    select LicenseID As 'Lic ID',
                    ApplicationID As 'App ID',
                    (select ClassName from LicenseClasses where LicenseClassID = LicenseClass) As 'Class Name',
                    IssueDate,
                    ExpirationDate,
                    IsActive
                    
                    from Licenses
                    where DriverID = (
                    			select DriverID from Drivers
                    			where PersonID = (select PersonID from People
                    									where NationalNo = 
                    										(select NationalNo from LocalDrivingApplicationTable where LDLAppID = @LDLAppID))   )
                    --and IsActive = 1";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public DataTable LoadAllInternationalLicenses(int LDLAppID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" Declare @LDLAppID INT;
                set @LDLAppID = @@LDLAppID;
                
                
                select InternationalLicenseID As 'Int.License ID',
                ApplicationID As 'Application ID',
                IssuedUsingLocalLicenseID As 'L.License ID',
                IssueDate,
                ExpirationDate,
                IsActive
                
                from InternationalLicenses
                where DriverID = (
                			select DriverID from Drivers
                			where PersonID = (select PersonID from People
                									where NationalNo = 
                										(select NationalNo from LocalDrivingApplicationTable where LDLAppID = @LDLAppID ))   )
                --and IsActive = 1
				--and EXISTS (select top 1 1 Find from Licenses where LicenseID = IssuedUsingLocalLicenseID 
				--								and IsActive = 1
				--								and ExpirationDate >= GETDATE())";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                else
                    dt = null;

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

            
    }
}
