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

        public static bool IssueInternationalLicense(int LicenseID, string UserName, ref int InternationalLicenseID,
                            ref int ApplicationID)
        {
            bool IssueSucceed = false;


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"


DECLARE @LicenseID INT, @UserID INT, @PersonID INT, @ApplicationID INT, @DriverID INT;
DECLARE @NewInternationalLicenseID INT = -1; -- Initialize with -1

SET @LicenseID = @@LicenseID;

-- Set UserID based on the username
SET @UserID = (SELECT UserID FROM Users WHERE UserName = @@UserName);

-- Check if @UserID is NULL, if so return -1 for both variables
IF @UserID IS NULL
BEGIN
    SELECT -1 AS NewInternationalLicenseID, -1 AS ApplicationID;
    RETURN;
END

-- Set DriverID based on LicenseID
SET @DriverID = (SELECT DriverID FROM Licenses WHERE LicenseID = @LicenseID);

-- Check if @DriverID is NULL, if so return -1 for both variables
IF @DriverID IS NULL
BEGIN
    SELECT -1 AS NewInternationalLicenseID, -1 AS ApplicationID;
    RETURN;
END

-- Set PersonID based on Application linked to LicenseID  
-- 1024
SET @PersonID = (SELECT ApplicantPersonID 
                 FROM Applications 
                 WHERE ApplicationID = (SELECT ApplicationID 
                                        FROM Licenses 
                                        WHERE LicenseID = @LicenseID));

-- Check if @PersonID is NULL, if so return -1 for both variables
IF @PersonID IS NULL
BEGIN
    SELECT -1 AS NewInternationalLicenseID, -1 AS ApplicationID;
    RETURN;
END

-- Proceed with the main logic if all the values are available
IF NOT EXISTS (
    SELECT 1 
    FROM InternationalLicenses 
    WHERE IssuedUsingLocalLicenseID = @LicenseID 
    AND IsActive = 1
)
AND EXISTS (
    SELECT 1 
    FROM Licenses
    WHERE LicenseID = @LicenseID 
    AND IsActive = 0
)
AND NOT EXISTS (
    SELECT 1 
    FROM Licenses
    WHERE LicenseID = @LicenseID 
    AND ExpirationDate <= GETDATE()
)
BEGIN
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
             6, 
             3, 
             GETDATE(), 
             (SELECT ApplicationFees FROM ApplicationTypes WHERE ApplicationTypeID = 6),
             @UserID);

    SET @ApplicationID = SCOPE_IDENTITY();

    -- Check if @ApplicationID is NULL, if so return -1 for both variables
    IF @ApplicationID IS NULL
    BEGIN
        SELECT -1 AS NewInternationalLicenseID, -1 AS ApplicationID;
        RETURN;
    END

    INSERT INTO InternationalLicenses
           (ApplicationID,
            DriverID,
            IssuedUsingLocalLicenseID,
            IssueDate,
            ExpirationDate,
            IsActive,
            CreatedByUserID)
     VALUES
           (@ApplicationID,
            @DriverID, 
            @LicenseID,
            GETDATE(), 
            DATEADD(YEAR, 1, GETDATE()),
            1, 
            @UserID);
		
    

    -- Set NewInternationalLicenseID to the newly inserted record ID
    SET @NewInternationalLicenseID = SCOPE_IDENTITY();

        -- Create New LDLApp
	        INSERT INTO LocalDrivingLicenseApplications
	              (ApplicationID,
	        	  LicenseClassID)
	        VALUES
	              (@ApplicationID,
	              6)
            -- Return the newly inserted IDs
    SELECT @NewInternationalLicenseID AS NewInternationalLicenseID, @ApplicationID AS ApplicationID;
END
ELSE
BEGIN
    -- If any condition for insert is not met, return -1 for both variables
    SELECT -1 AS NewInternationalLicenseID, -1 AS ApplicationID;
END
";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    InternationalLicenseID = Convert.ToInt16(reader["NewInternationalLicenseID"]);
                    ApplicationID = Convert.ToInt16(reader["ApplicationID"]);

                    if (InternationalLicenseID != -1 && ApplicationID != -1)
                    {
                        IssueSucceed = true;
                    }
                }

                reader.Close();


            }
            finally
            {
                connection.Close();
            }

            return IssueSucceed;

        }


    }
}
