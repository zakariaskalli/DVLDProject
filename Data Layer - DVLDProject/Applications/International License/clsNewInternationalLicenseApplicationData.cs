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
using System.ComponentModel;

namespace Data_Layer___DVLDProject
{
    public class clsNewInternationalLicenseApplicationData
    {

        public static bool IssueInternationalLicense(int LicenseID, string UserName, ref int InternationalLicenseID, ref int ApplicationID)
        {
            bool IssueSucceed = false;

            using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
            {
                using (SqlCommand command = new SqlCommand("sp_IssueInternationalLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@UserName", UserName);

                    SqlParameter paramInternationalLicenseID = new SqlParameter("@NewInternationalLicenseID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(paramInternationalLicenseID);

                    SqlParameter paramApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(paramApplicationID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        InternationalLicenseID = (int)paramInternationalLicenseID.Value;
                        ApplicationID = (int)paramApplicationID.Value;

                        if (InternationalLicenseID != -1 && ApplicationID != -1)
                        {
                            IssueSucceed = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: log exception
                        throw new Exception("Error issuing international license", ex);
                    }
                }
            }

            return IssueSucceed;
        }

    }
}
