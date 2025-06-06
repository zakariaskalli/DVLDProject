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


        static public void LoadDataLicenseInfoByLicenseID(int LicenseID, ref string Class,
            ref string Name, ref string NationalNo, ref int Gendor,
            ref string IssueDate, ref int IssueReason, ref string Notes, ref bool IsActive,
            ref string DateOfBirth, ref int DriverID, ref string ExpirationDate,
            ref int IsDetained , ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" 
Declare @LicenseID INT;

Set @LicenseID = @@LicenseID


SELECT 
    lc.ClassName AS Class,
    dv.FullName AS Name,
    dv.NationalNo,
    p.Gendor,
    (SELECT FORMAT(CAST(l.IssueDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'IssueDate',
    l.IssueReason,
    l.Notes,
    l.IsActive,
	(SELECT FORMAT(CAST(p.DateOfBirth AS DATETIME), 'dd/MMMM/yyyy')) AS 'DateOfBirth',
    l.DriverID,
	(SELECT FORMAT(CAST(l.ExpirationDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'ExpirationDate',
    CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM DetainedLicenses dl 
            WHERE dl.LicenseID = l.LicenseID AND dl.IsReleased = 0
        ) 
        THEN 1 
        ELSE 0 
    END AS IsDetained,
    p.ImagePath

FROM Licenses l
JOIN LicenseClasses lc ON l.LicenseClass = lc.LicenseClassID
JOIN Drivers_View dv ON dv.DriverID = l.DriverID
JOIN People p ON p.PersonID = dv.PersonID

WHERE l.LicenseID = @LicenseID;
 ";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@@LicenseID", LicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    Class = (string)reader["Class"];
                    Name = (string)reader["Name"];
                    //LicenseID = (int)reader["LicenseID"];
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
