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
    public class clsRenewLocalDrivingLicenseData
    {

        static public void RenewLocalDrivingLicense(int LicenseID, string CreatedBy, string Notes, ref int RLAppID, ref int RenewedLicenseID)
        {
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"DECLARE @LicenseID INT,
                                        @UserName NVARCHAR(50),
                                        @Notes NVARCHAR(500),  
                                        @UserID INT,
                                        @LicenseClassID INT, 
                                        @PersonID INT,
                                		@AppID INT,
                                		@NewLicenseID INT;
                                
                                SET @LicenseID = @@LicenseID;
                                SET @UserName = @@UserName; 
                                SET @Notes = @@Notes;
                                
                                -- If @Notes is an empty string, set it to NULL
                                IF @Notes = '' 
                                    SET @Notes = NULL;
                                
                                -- Get the UserID based on the UserName
                                SET @UserID = (SELECT UserID FROM Users WHERE UserName = @UserName);
                                
                                -- Get the LicenseClassID based on the LicenseID
                                SET @LicenseClassID = (SELECT LicenseClass FROM Licenses WHERE LicenseID = @LicenseID);
                                
                                -- Get the PersonID based on the LicenseID
                                SET @PersonID = (SELECT ApplicantPersonID 
                                                 FROM Applications 
                                                 WHERE ApplicationID = (SELECT ApplicationID 
                                                                        FROM Licenses 
                                                                        WHERE LicenseID = @LicenseID));
                                
                                -- Check if the license is inactive
                                                                IF EXISTS ((SELECT TOP 1 1 FROM Licenses WHERE LicenseID = @LicenseID AND IsActive = 0))
											and
								   EXISTS (select top 1 1 from Licenses where LicenseID = (select top 1 LicenseID from Licenses where DriverID = (select DriverID from Drivers where PersonID = @PersonID) and LicenseClass = @LicenseClassID order by LicenseID desc)
												and IsActive = 0)
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
                                			 2, 
                                			 3, 
                                			 GETDATE(), 
                                			 (SELECT ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = 2),
                                			 @UserID);
                                	SET @AppID = SCOPE_IDENTITY();
                                
                                	-- Insert a new license and capture the new LicenseID
                                	INSERT INTO Licenses
                                	           (ApplicationID,
                                	            DriverID,
                                	            LicenseClass,
                                	            IssueDate,
                                	            ExpirationDate,
                                	            Notes,
                                	            PaidFees,
                                	            IsActive,
                                	            IssueReason,
                                	            CreatedByUserID)
                                	SELECT
                                	    @AppID,
                                	    (SELECT DriverID 
                                	     FROM Drivers 
                                	     WHERE PersonID = @PersonID),
                                		@LicenseClassID,
                                	    GETDATE(), 
                                	    DATEADD(YEAR, (SELECT DefaultValidityLength FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID), GETDATE()),
                                	    @Notes, -- @Notes will be NULL if it was initially an empty string
                                	    (SELECT ClassFees FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID),
                                	    1, 
                                	    (SELECT ApplicationTypeID FROM Applications WHERE ApplicationID = @AppID), 
                                	    @UserID;
                                
                                	SET @NewLicenseID = SCOPE_IDENTITY();
									    -- Create New LDLApp
	                                        INSERT INTO LocalDrivingLicenseApplications
	                                              (ApplicationID,
	                                        	  LicenseClassID)
	                                        VALUES
	                                              (@ApplicationID,
	                                              6)
                                END;
                                
                                -- Return the new ApplicationID and LicenseID
                                SELECT @AppID AS NewApplicationID, @NewLicenseID AS NewLicenseID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@@UserName", CreatedBy);
            Command.Parameters.AddWithValue("@@Notes", Notes);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    RLAppID = reader["NewApplicationID"] != DBNull.Value
                            ? Convert.ToInt16(reader["NewApplicationID"])
                            : -1;

                    RenewedLicenseID = reader["NewLicenseID"] != DBNull.Value
                                       ? Convert.ToInt16(reader["NewLicenseID"])
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
