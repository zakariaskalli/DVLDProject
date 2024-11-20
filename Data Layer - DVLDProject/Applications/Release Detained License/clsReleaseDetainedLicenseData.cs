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
    public class clsReleaseDetainedLicenseData
    {

        static public void RenewLocalDrivingLicense(int DetainID,ref string DetainDate,ref int FineFees, ref int UserID)
        {
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select (SELECT FORMAT(CAST(DetainDate AS DATETIME), 'dd/MMM/yyyy')) AS 'DetainDate',
                                FineFees,
                                CreatedByUserID As 'UserID'
                                from DetainedLicenses
                                where DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@DetainID", DetainID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    DetainDate = (string)reader["DetainDate"];
                    FineFees = Convert.ToInt16(reader["FineFees"]);
                    UserID = (int)reader["UserID"];

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }


        static public void ReleaseDetainedDrivingLicense(int LicenseID, string UserName, ref int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"


DECLARE @LicenseID INT,
                                        @PersonID INT,
                                        @UserName NVARCHAR(50),
										@UserID INT,
                                		@AppID INT;
                                
                                SET @LicenseID = @@LicenseID;
                                SET @UserName = @@UserName; 
                                
                                
                                -- Get the UserID based on the UserName 

								-- 15
                                SET @UserID = (SELECT UserID FROM Users WHERE UserName =  @UserName);
                                
                                -- Get the LicenseClassID based on the LicenseID
                                -- Get the PersonID based on the LicenseID

								-- 1024
                                SET @PersonID = (SELECT ApplicantPersonID 
                                                 FROM Applications 
                                                 WHERE ApplicationID = (SELECT ApplicationID 
                                                                        FROM Licenses 
                                                                        WHERE LicenseID =  @LicenseID));
                                
                                -- Check if the license is inactive
                                                                IF EXISTS(select top 1 1 from Licenses
																		where LicenseID = @LicenseID and Exists(select top 1 1 from DetainedLicenses 
																			where LicenseID = @LicenseID and IsReleased = 0 order by DetainID desc))
																	
                                BEGIN

                                	-- Insert a new application and capture the new ApplicationID
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
                                			 5, 
                                			 3, 
                                			 GETDATE(), 
                                			 (SELECT ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = 5),
                                			 @UserID);
                                	SET @AppID = SCOPE_IDENTITY();
									
									-- Release Detained License

									UPDATE DetainedLicenses
									   SET IsReleased = 1,
											ReleaseDate = GetDate(),
											ReleasedByUserID = @UserID,
											ReleaseApplicationID = @AppID
										where LicenseID = @LicenseID

                                END;
                                
                                -- Return the new ApplicationID and LicenseID
                                SELECT @AppID AS NewApplicationID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@@UserName", UserName);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = reader["NewApplicationID"] != DBNull.Value
                            ? Convert.ToInt16(reader["NewApplicationID"])
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
