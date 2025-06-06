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

DECLARE @InternationalLicenseID INT;

Set @InternationalLicenseID = @@InternationalLicenseID;

SELECT 
    D.FullName AS [Name],
    I.InternationalLicenseID,
    I.IssuedUsingLocalLicenseID AS [LicenseID],
    D.NationalNo,

    -- Keep Gendor as is (0 or 1)
    P.Gendor,

    -- Format IssueDate
    REPLACE(CONVERT(VARCHAR(11), I.IssueDate, 106), ' ', '/') AS IssueDate,
    I.ApplicationID,

    -- Keep IsActive as is (0 or 1)
    I.IsActive,

    -- Format DateOfBirth
    REPLACE(CONVERT(VARCHAR(11), P.DateOfBirth, 106), ' ', '/') AS DateOfBirth,
    I.DriverID,

    -- Format ExpirationDate
    REPLACE(CONVERT(VARCHAR(11), I.ExpirationDate, 106), ' ', '/') AS ExpirationDate,

    -- Subquery for ImagePath
    (SELECT ImagePath FROM People WHERE NationalNo = D.NationalNo) AS ImagePath

FROM InternationalLicenses I
JOIN Drivers_View D ON D.DriverID = I.DriverID
JOIN People P ON P.PersonID = D.PersonID
WHERE I.InternationalLicenseID = @InternationalLicenseID;


";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@InternationalLicenseID", InternationalLicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    Name = (string)reader["Name"];
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
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
