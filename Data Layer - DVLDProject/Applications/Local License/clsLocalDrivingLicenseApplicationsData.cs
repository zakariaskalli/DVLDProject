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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Data_Layer___DVLDProject
{
    public class clsLocalDrivingLicenseApplicationsData
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS LDLAppID, dbo.LicenseClasses.ClassName AS [Driving Class], dbo.People.NationalNo, dbo.People.FirstName + ' ' + ISNULL(dbo.People.SecondName, 
                         '') + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + ISNULL(dbo.People.LastName, '') AS FullName, App.ApplicationDate,
                             (SELECT        COUNT(dbo.TestAppointments.IsLocked) AS PassedTests
                               FROM            dbo.TestAppointments INNER JOIN
                                                         dbo.Tests ON dbo.TestAppointments.TestAppointmentID = dbo.Tests.TestAppointmentID
                               WHERE        (dbo.TestAppointments.LocalDrivingLicenseApplicationID = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID) AND (dbo.Tests.TestResult = 1)) AS PassedTestCount, 
                         CASE WHEN App.ApplicationStatus = 1 THEN 'New' WHEN App.ApplicationStatus = 2 THEN 'Cancelled' WHEN App.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                          ( select * from Applications where ApplicationTypeID != 2 and ApplicationTypeID != 3 and ApplicationTypeID != 4 )App ON dbo.LocalDrivingLicenseApplications.ApplicationID = App.ApplicationID INNER JOIN
                         dbo.People ON App.ApplicantPersonID = dbo.People.PersonID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        static public bool CancelLicenseByNationalNo(string NationalNo, int LicenseClassID)
        {
            bool IsEdit = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"Update Applications
                                Set Applications.ApplicationStatus = 2, LastStatusDate = GETDATE()
                                From Applications
                                JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                                Where Applications.ApplicantPersonID = (select PersonID from People where NationalNo = @NationalNo)
                                					and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID and Applications.ApplicationStatus = 1";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();



                int result = Command.ExecuteNonQuery();

                if (result > 0)
                    IsEdit = true;
                else
                    IsEdit = false;


            }
            finally
            {
                connection.Close();
            }

            return IsEdit;

        }

        static public bool DeleteLicenseByLDLAppID(int LDLAppID)
        {
            bool IsEdit = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" DECLARE @LDLAppID INT = @@LDLAppID, 
                                            @ApplicationID INT;
                                
                                    IF NOT EXISTS (SELECT 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LDLAppID)
                                    BEGIN
                                        SET @ApplicationID = (SELECT ApplicationID 
                                                              FROM Applications
                                                              WHERE ApplicationID = (SELECT ApplicationID 
                                                                                     FROM LocalDrivingLicenseApplications
                                                                                     WHERE LocalDrivingLicenseApplicationID = @LDLAppID));
                                        
                                        DELETE FROM LocalDrivingLicenseApplications
                                        WHERE LocalDrivingLicenseApplicationID = @LDLAppID;
                                        
                                        DELETE FROM Applications
                                        WHERE ApplicationID = @ApplicationID;
                                
                                        SELECT 1 AS Success;
                                    END
                                    ELSE
                                    BEGIN
                                        SELECT 0 AS Success; 
                                    END";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@@LDLAppID", LDLAppID);

            try
            {
                connection.Open();


                int result = Command.ExecuteNonQuery();

                if (result == 2)
                    IsEdit = true;
                else
                    IsEdit = false;


            }
            finally
            {
                connection.Close();
            }

            return IsEdit;

        }


        static public DataTable SearchData(string ColumnName, string Data)
        {
            DataTable dt = new DataTable();


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from LocalDrivingApplicationTable
                                    where {ColumnName} Like '' + @Data + '%';";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@Data", Data);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        static public void LoadDataDrivingLicenseAppInfo(int LDLAppID, ref string LicenseName, ref int NumTestsPassed)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select LDLAppID, [Driving Class], PassedTestCount from LocalDrivingApplicationTable
                                    where LDLAppID = @LDLAppID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    LicenseName = (string)reader["Driving Class"];
                    NumTestsPassed = (int)reader["PassedTestCount"];
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        static public void LoadDataApplicationBasicInfo(int LDLAppID, ref int ID, ref string Status, ref int Fees, ref string Type,
                                                        ref string Applicant, ref string Date, ref string StatusDate, ref string CreatedBy)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"
select ApplicationID,
                                Case 
                                When ApplicationStatus = 1 Then 'New'
                                When ApplicationStatus = 2 Then 'Cancelled'
                                When ApplicationStatus = 3 Then 'Completed'
                                End As 'Status',
                                PaidFees, 
                                ( select ApplicationTypeTitle From ApplicationTypes
                                where ApplicationTypes.ApplicationTypeID = Applications.ApplicationTypeID) As 'Type',
                                (
                                select top 1 FullName from LocalDrivingApplicationTable
                                where NationalNo = (select NationalNo from People
                                where PersonID = Applications.ApplicantPersonID)
                                ) As 'Applicant',
                                (SELECT FORMAT(CAST(ApplicationDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'Date',
                                
                                (SELECT FORMAT(CAST(LastStatusDate AS DATETIME), 'dd/MMMM/yyyy')) AS 'DateStatus',
                                ( select UserName from Users where UserID = CreatedByUserID) As 'CreatedByUserID'
                                
                                from Applications
                                where
                                ApplicationID In ( Select ApplicationID from LocalDrivingLicenseApplications
                                where LicenseClassID = (select LicenseClassID from LicenseClasses
                                where ClassName = (
                                select [Driving Class] from LocalDrivingApplicationTable
                                where LDLAppID = @LDLAppID)))
                                and
                                ApplicantPersonID = (select PersonID from People
                                where NationalNo = ( select NationalNo from LocalDrivingApplicationTable  where LDLAppID = @LDLAppID) )
                                and ApplicationStatus = (select 
                                Case 
                                When Status = 'New' Then 1
                                when Status = 'Cancelled' Then 2
                                When Status = 'Completed' Then 3
                                End
                                Status
                                from LocalDrivingApplicationTable
                                where LDLAppID = @LDLAppID)";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    ID = (int)reader["ApplicationID"];
                    Status = (string)reader["Status"];
                    Fees = (int)((decimal)reader["PaidFees"]);
                    Type = (string)reader["Type"];
                    Applicant = (string)reader["Applicant"];
                    Date = (string)reader["Date"];
                    StatusDate = (string)reader["DateStatus"];
                    CreatedBy = (string)reader["CreatedByUserID"];
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
