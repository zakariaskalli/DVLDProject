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
    public class clsIssueDriverLicenseData
    {

        static int SelectLastLicenseIDAdd()
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select top 1 LicenseID from Licenses
                                order by LicenseID desc";

            SqlCommand Command = new SqlCommand(query, connection);


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

        static public bool IssueDriverLicenseFirstTime(int LDLAppID, string CreatedBy, string Notes, ref int LicenseID)
        {
            bool Successfully = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"

DECLARE @LDLAppID INT,
        @UserName NVARCHAR(50),
        @Notes NVARCHAR(500),  
        @UserID INT, 
        @AppID INT,
        @LicenseClassID INT, 
        @PersonID INT, 
        @ClassName NVARCHAR(50);

SET @LDLAppID = @@LDLAppID;
SET @UserName = @@CreatedBy; 
SET @Notes = @@Notes;

-- If @Notes is an empty string, set it to NULL
IF @Notes = '' 
    SET @Notes = NULL;

SET @UserID = (SELECT UserID FROM Users WHERE UserName = @UserName);
SET @AppID = (SELECT ApplicationID FROM LocalDrivingLicenseApplications
              WHERE LocalDrivingLicenseApplicationID = @LDLAppID);

SET @LicenseClassID = (SELECT LicenseClassID FROM LicenseClasses
                       WHERE ClassName = (SELECT [Driving Class] 
                                          FROM LocalDrivingApplicationTable
                                          WHERE LDLAppID = @LDLAppID));

SET @PersonID = (SELECT PersonID FROM People WHERE NationalNo = (
                SELECT NationalNo FROM LocalDrivingApplicationTable
                WHERE LDLAppID = @LDLAppID));

SET @ClassName = (SELECT [Driving Class] 
                  FROM LocalDrivingApplicationTable
                  WHERE LDLAppID = @LDLAppID);

-- Create A Driver
INSERT INTO Drivers
           (PersonID,
            CreatedByUserID,
            CreatedDate)
SELECT 
    @PersonID,
    @UserID,
    GETDATE()
WHERE NOT EXISTS (
    SELECT top 1 1 Find
    FROM Drivers 
    WHERE PersonID = @PersonID);

-- Update Application Status New to Completed
UPDATE Applications
   SET ApplicationStatus = 3,
       LastStatusDate = GETDATE()
WHERE ApplicationID = @AppID 
  AND ApplicationStatus = 1;

-- Create New License ID
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
    (SELECT LicenseClassID 
     FROM LicenseClasses 
     WHERE ClassName = @ClassName),
    GETDATE(), 
    DATEADD(YEAR, (select DefaultValidityLength from LicenseClasses where LicenseClassID = @LicenseClassID), GETDATE()),
    @Notes, -- @Notes will be NULL if it was initially an empty string
    (SELECT ClassFees 
     FROM LicenseClasses 
     WHERE ClassName = @ClassName),
    1, 
    (select ApplicationTypeID from Applications where ApplicationID = @AppID), 
    @UserID
WHERE NOT EXISTS (SELECT TOP 1 1 
                  FROM Licenses 
                  WHERE ApplicationID = @AppID)
  AND (SELECT ApplicationStatus 
       FROM Applications 
       WHERE ApplicationID = @AppID) = 3
  AND EXISTS (SELECT TOP 1 1 
              FROM Drivers 
              WHERE PersonID = @PersonID)
--  AND EXISTS (SELECT top 1 1 
--	FROM Licenses 
--WHERE  DriverID = (SELECT DriverID  FROM Drivers WHERE PersonID = @PersonID)
--		and   
--		IssueReason = 1
--		and
--		LicenseClass != @LicenseClassID)

";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@@CreatedBy", CreatedBy);
            Command.Parameters.AddWithValue("@@Notes", Notes);


            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();

                if (result >= 2)
                {
                    Successfully = true;
                    LicenseID = SelectLastLicenseIDAdd();
                }
                else
                    Successfully = false;
            }
            finally
            {
                connection.Close();
            }
            return Successfully;
        }


    }
}
