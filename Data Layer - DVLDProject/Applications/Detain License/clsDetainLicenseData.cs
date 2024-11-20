using ConnectionToSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace Data_Layer___DVLDProject
{
    public class clsDetainLicenseData
    {
        static public void DetainLicense(int LicenseID, string CreatedBy, int Fees, ref int DetainedID)
        {
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Declare @LicenseID INT, @UserID INT, @Fees INT;

                                SET @LicenseID = @@LicenseID;  
                                
                                SET @UserID = (SELECT UserID FROM Users WHERE UserName = @UserName);

                                Set @Fees = @@Fees;


                                INSERT INTO DetainedLicenses
                                           (LicenseID,
                                            DetainDate,
                                            FineFees,
                                            CreatedByUserID,
                                            IsReleased,
                                            ReleaseDate,
                                            ReleasedByUserID,
                                            ReleaseApplicationID)
                                     VALUES
                                           (@LicenseID, 
                                            GETDATE(),
                                            @Fees, 
                                            @UserID, 
                                            0, 
                                            NULL, 
                                            NULL, 
                                            NULL);
                                
                                SELECT SCOPE_IDENTITY() As 'DetainedID';";
                        
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@UserName", CreatedBy);
            Command.Parameters.AddWithValue("@@Fees", Fees);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    DetainedID = reader["DetainedID"] != DBNull.Value
                            ? Convert.ToInt16(reader["DetainedID"])
                            : -1;

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
