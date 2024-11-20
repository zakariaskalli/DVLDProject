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
    public class clsApplicationNewLicenseInfoData
    {

        static public void LoadDataByLicenseID(int LicenseID, ref string LicenseFees,
                                                        ref string ExpirationDate)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LicenseID INT;
                                
                                set @LicenseID = @@LicenseID;
                                                                
                                select 
                                (select ClassFees from LicenseClasses
                                			where LicenseClassID = LicenseClass) As 'LicenseFees',
                                
                                (SELECT FORMAT(CAST(ExpirationDate AS DATETIME), 'dd/MMM/yyyy')) AS 'ExpirationDate' 
                                from Licenses
                                where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseFees = (Convert.ToInt16(reader["LicenseFees"])).ToString();
                    ExpirationDate = (string)reader["ExpirationDate"];

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
