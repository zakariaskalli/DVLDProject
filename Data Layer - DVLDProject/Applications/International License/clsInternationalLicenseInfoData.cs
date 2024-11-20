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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Data_Layer___DVLDProject
{
    public class clsInternationalLicenseInfoData
    {

        static public void LoadDataInternationalLicenseInfoByIntLicenseID(int InternationalLicenseID,
            ref string Name, ref int LicenseID, ref string NationalNo, ref int Gendor,
            ref string IssueDate,ref int ApplicationID,ref bool IsActive,
            ref string DateOfBirth, ref int DriverID, ref string ExpirationDate, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"


Declare @InternationalLicenseID INT, @LocalLicenseID INT, @AppID INT,
                                    @LDLAppID INT, @PersonID INT;
                                    --30
                                    Set @InternationalLicenseID = @@InternationalLicenseID
									--46
                                    Set @LocalLicenseID = (SELECT IssuedUsingLocalLicenseID from InternationalLicenses 
                                    									where InternationalLicenseID = @InternationalLicenseID)
									--126
                                    Set @AppID = (select ApplicationID from InternationalLicenses
                                    			where InternationalLicenseID = @InternationalLicenseID)
									-- 70
                                    Set @LDLAppID = (select LocalDrivingLicenseApplicationID from LocalDrivingLicenseApplications where ApplicationID = @AppID)
									--1024
                                    Set @PersonID  = (select PersonID from People where NationalNo = (select NationalNo from LocalDrivingApplicationTable 
                                    																	where LDLAppID =@LDLAppID))
                                    
                                    select 
                                    (select FullName from LocalDrivingApplicationTable where LDLAppID = 36) As 'Name',
                                    IssuedUsingLocalLicenseID,
                                    (select NationalNo from People where PersonID = @PersonID) As 'NationalNo',
                                    (select Gendor from People where PersonID = @PersonID) As 'Gendor',
                                    (SELECT FORMAT(CAST(IssueDate AS DATETIME), 'dd/MMM/yyyy')) AS 'IssueDate',
                                    @AppID As 'ApplicationID',
                                    IsActive,
                                    (SELECT FORMAT(CAST((select DateOfBirth from People where PersonID = @PersonID) AS DATETIME), 'dd/MMM/yyyy')) AS 'DateOfBirth',
                                    DriverID,
                                    (SELECT FORMAT(CAST(ExpirationDate AS DATETIME), 'dd/MMM/yyyy')) AS 'ExpirationDate',
                                    (select ImagePath from People where PersonID = @PersonID) As 'ImagePath'
                                    
                                    from InternationalLicenses
                                    where InternationalLicenseID = @InternationalLicenseID  ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@InternationalLicenseID", InternationalLicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    Name = (string)reader["Name"];
                    LicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    IssueDate = (string)reader["IssueDate"];
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    IsActive = (bool)reader["IsActive"];
                    DateOfBirth = (string)reader["DateOfBirth"];
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    ExpirationDate = (string)reader["ExpirationDate"];

                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
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
