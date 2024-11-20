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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Data_Layer___DVLDProject
{
    public class clsTestsAppointmentsData
    {
        static public DataTable LoadTestsAppointmentByLDLAppID(int TestNum, int LDLAppID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" select TestAppointmentID,
                                    AppointmentDate,
                                   PaidFees,
                                   IsLocked from TestAppointments
                                   where LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestNum
                            Order By TestAppointmentID desc";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@TestNum", TestNum);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);


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

        static public void LoadDataForScheduleByLDLAppIDAndTestNum(int TestNum, int LDLAppID, ref string DClass,
                                                        ref string Name, ref int Trial, ref int Fees)
        {


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" select LDLAppID, [Driving Class], FullName, (select Count(*) from TestAppointments
                                    where LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestNum and IsLocked = 1) As 'Trial', 
                                    (select TestTypeFees from TestTypes
                                    where TestTypeID = @TestNum) As 'Fees'
                                    from LocalDrivingApplicationTable
                                    where LDLAppID = @LDLAppID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestNum", TestNum);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    DClass = (string)reader["Driving Class"];
                    Name = (string)reader["FullName"];
                    Trial = Convert.ToInt16(reader["Trial"]);
                    Fees = (int)((decimal)reader["Fees"]);

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        static public int LoadUpdateDataForScheduleByAppointmentIDAndTestNum(int TestNum, int AppointmentID,ref int LDLAppID, ref string DClass,
                                                        ref string Name, ref int Trial, ref int Fees, ref DateTime Date)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" 

SELECT      LocalDrivingApplicationTable.LDLAppID, LocalDrivingApplicationTable.[Driving Class], LocalDrivingApplicationTable.FullName,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            TestAppointments
                               WHERE        (LocalDrivingLicenseApplicationID =
                                                             (SELECT        LocalDrivingLicenseApplicationID
                                                               FROM            TestAppointments AS TestAppointments_2
                                                               WHERE        (TestAppointmentID = @AppointmentID))) AND (TestTypeID = @TestNum) AND (IsLocked = 1)) AS 'Trial',
                             (SELECT        TestTypeFees
                               FROM            TestTypes
                               WHERE        (TestTypeID = @TestNum)) AS 'Fees', TestAppointments_3.AppointmentDate, TestAppointmentID
FROM            LocalDrivingApplicationTable CROSS JOIN
                         TestAppointments AS TestAppointments_3
WHERE        (LocalDrivingApplicationTable.LDLAppID =
                             (SELECT        LocalDrivingLicenseApplicationID
                               FROM            TestAppointments AS TestAppointments_1
                               WHERE        (TestAppointmentID = @AppointmentID)))
							   and TestAppointmentID = @AppointmentID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            Command.Parameters.AddWithValue("@TestNum", TestNum);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    LDLAppID = (int)reader["LDLAppID"];
                    DClass = (string)reader["Driving Class"];
                    Name = (string)reader["FullName"];
                    Trial = Convert.ToInt16(reader["Trial"]);
                    Fees = (int)((decimal)reader["Fees"]);
                    Date = (DateTime)reader["AppointmentDate"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            return TestAppointmentID;
        }


        static public bool AddDataToTestAppointments(int LDLAppID, int TestTypeID, DateTime Date, string UserName)
        {

            bool IsAdd = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"INSERT INTO TestAppointments
                  (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked)
            VALUES
                  (@TestTypeID, @LDLAppID, @Date, (select TestTypeFees from TestTypes where TestTypeID = @TestTypeID),
                    ( select UserID from Users where UserName = @UserName), 0)
            select SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@Date", Date);
            Command.Parameters.AddWithValue("@UserName", UserName);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsAdd = true;
                else
                    IsAdd = false;


            }
            finally
            {
                connection.Close();
            }

            return IsAdd;
        }

        static public bool UpdateDateToTestAppointments(int LDLAppID, int TestTypeID, DateTime Date)
        {

            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"UPDATE TestAppointments
                                   SET AppointmentDate = @Date
                                WHERE LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestTypeID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@Date", Date);

            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();

                if (result > 0)
                    IsUpdate = true;
                else
                    IsUpdate = false;


            }
            finally
            {
                connection.Close();
            }

            return IsUpdate;
        }

        static public bool IsTestAppointmentsLockedByLDLAppID(int LDLAppID, int TestTypeID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT TOP 1 1 AS IsNotPossible
                                FROM TestAppointments
                                WHERE LocalDrivingLicenseApplicationID = @LDLAppID And TestTypeID = @TestTypeID
                                                                                        AND IsLocked = 1";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IsTestAppointmentsLockedByAppointmentID(int AppointmentID, int TestTypeID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT Top 1 1 AS IsLocked
                                FROM TestAppointments
                                WHERE LocalDrivingLicenseApplicationID = (SELECT        LocalDrivingLicenseApplicationID
                               FROM            TestAppointments AS TestAppointments_1
                               WHERE        (TestAppointmentID = @AppointmentID))
				And TestTypeID = @TestTypeID
                                                                                        AND IsLocked = 1
                                    And TestAppointmentID = @AppointmentID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IHaveAnAppointmentByLDLAppIDAndTestNum(int LDLAppID, int TestTypeID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT TOP 1 1 AS IsNotPossible
                                FROM TestAppointments
                                WHERE LocalDrivingLicenseApplicationID = @LDLAppID And TestTypeID = @TestTypeID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool IsTestAppointmentsFinished(int LDLAppID, int TestTypeID)
        {
            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"SELECT TOP 1 1 AS IsNotPossible
                                FROM TestAppointments
                                WHERE LocalDrivingLicenseApplicationID = @LDLAppID And TestTypeID = @TestTypeID AND 
                                ISNULL(
                                	(SELECT TestResult
                                	 FROM Tests
                                	 WHERE TestAppointmentID = 
                                	     (SELECT TOP 1 TestAppointmentID
                                	      FROM TestAppointments
                                	      WHERE LocalDrivingLicenseApplicationID = @LDLAppID
                                	        AND TestTypeID = @TestTypeID
                                	      ORDER BY TestAppointmentID DESC)
                                	), 1) = 1";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool CanIUpdateTestAppointmentsByLDLAppID(int LDLAppID, int TestTypeID)
         {

            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select TOP 1 1 from TestAppointments
                                where LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestTypeID
                                and IsLocked = 0";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

        static public bool CanIUpdateTestAppointmentsByTestAppID(int TestAppointmentID, int TestTypeID)
        {

            bool IsAvailable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select TOP 1 1 from TestAppointments
                                where TestAppointmentID = @TestAppointmentID and TestTypeID = @TestTypeID
                                and IsLocked = 0";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvailable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvailable;
        }

    }
}
