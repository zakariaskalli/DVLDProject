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
    public class clsLicenseInfoData
    {


        static public void LoadDataLicenseInfoByLDLAppID(int LDLAppID, ref string Class,
            ref string Name, ref int LicenseID, ref string NationalNo, ref int Gendor,
            ref string IssueDate, ref int IssueReason, ref string Notes, ref bool IsActive,
            ref string DateOfBirth, ref int DriverID, ref string ExpirationDate,
            ref int IsDetained , ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" 

Declare @LDLAppID INT, @AppID INT, @LicenseClassID INT, @PersonID INT;
                                
                                Set @LDLAppID = @@LDLAppID;
                                
                                Set @AppID = (select ApplicationID from LocalDrivingLicenseApplications
                                					where LocalDrivingLicenseApplicationID = @LDLAppID)
                                
                                
                                Set @LicenseClassID = (select LicenseClassID from LocalDrivingLicenseApplications
                                					where LocalDrivingLicenseApplicationID = @LDLAppID)
                                
                                Set @PersonID = (select PersonID from  People
                                								where NationalNo = 
                                								( select NationalNo from LocalDrivingApplicationTable 
                                													where LDLAppID = @LDLAppID))
                                
                                select ( select ClassName from LicenseClasses where LicenseClassID = LicenseClass) As 'Class',
                                (select FullName from LocalDrivingApplicationTable where LDLAppID = @LDLAppID) As 'Name',
                                LicenseID,
                                (select NationalNo from People where PersonID = @PersonID) As 'NationalNo',
                                (select Gendor from People where PersonID = @PersonID) As 'Gendor', 
                                
                                (SELECT FORMAT(CAST(IssueDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'IssueDate',
                                IssueReason,
                                Notes,
                                Licenses.IsActive,
								(SELECT FORMAT(CAST((select DateOfBirth from People where PersonID = @PersonID) AS DATETIME), 'dd/MMMM/yyyy')) AS 'DateOfBirth',
                                DriverID,
								(SELECT FORMAT(CAST(ExpirationDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'ExpirationDate',
                                    CASE WHEN EXISTS (SELECT 1 FROM DetainedLicenses WHERE LicenseID = Licenses.LicenseID and IsReleased = 0) 
                                         THEN 1 
                                         ELSE 0 
                                    END AS IsDetained,
                                (select ImagePath from People where PersonID = @PersonID) As 'ImagePath'
                                from Licenses
                                where ApplicationID = @AppID And LicenseClass = @LicenseClassID ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);



            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    Class = (string)reader["Class"];
                    Name = (string)reader["Name"];
                    LicenseID = (int)reader["LicenseID"];
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    IssueDate = (string)reader["IssueDate"];
                    IssueReason = Convert.ToInt32(reader["IssueReason"]);
                    Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
                    IsActive = (bool)reader["IsActive"];
                    DateOfBirth = (string)reader["DateOfBirth"];
                    DriverID = (int)reader["DriverID"];
                    ExpirationDate = (string)reader["ExpirationDate"];
                    IsDetained = Convert.ToInt32(reader["IsDetained"]);
                    
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
