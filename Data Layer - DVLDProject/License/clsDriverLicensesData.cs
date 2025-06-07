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

        public DataTable LoadAllLDLAppByDriverID(int DriverID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"

 Declare @DriverID INT;

                    set @DriverID = @@DriverID;
                    
                    
                    select LicenseID As 'Lic ID',
                    ApplicationID As 'App ID',
                    (select ClassName from LicenseClasses where LicenseClassID = LicenseClass) As 'Class Name',
                    IssueDate,
                    ExpirationDate,
                    IsActive
                    
                    from Licenses
                    where DriverID = @DriverID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@DriverID", DriverID);

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


        public DataTable LoadAllInternationalLicensesByDriverID(int DriverID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" 

Declare @DriverID INT;
                set @DriverID = @@DriverID;
                
                
                select InternationalLicenseID As 'Int.License ID',
                ApplicationID As 'Application ID',
                IssuedUsingLocalLicenseID As 'L.License ID',
                IssueDate,
                ExpirationDate,
                IsActive
                
                from InternationalLicenses
                where DriverID = @DriverID

";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@DriverID", DriverID);

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
