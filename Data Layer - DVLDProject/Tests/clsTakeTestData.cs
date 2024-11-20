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
    public class clsTakeTestData
    {
        static public void LoadDataForTakeTestByAppointmentIDAndTestNum(int TestNum, int AppointmentID, ref int LDLAppID, ref string DClass,
                                                ref string Name, ref int Trial, ref int Fees, ref DateTime Date)
        {

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
                               WHERE        (TestTypeID = @TestNum)) AS 'Fees', TestAppointments_3.AppointmentDate
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
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        static public bool DataPassOrFailByLDLAppIDAndTestNum(bool PassOrFail, int LDLAppID, int TestNum, string CreatedBy, string Notes)
        {
            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" UPDATE TestAppointments
   SET IsLocked = 1
 WHERE TestAppointmentID = (select TestAppointmentID from TestAppointments
where LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestNum and IsLocked = 0)


INSERT INTO Tests
           (TestAppointmentID,
		   TestResult,
		   Notes,
		   CreatedByUserID)
     VALUES
           (
		   (select top 1 TestAppointmentID from TestAppointments
				where LocalDrivingLicenseApplicationID = @LDLAppID and TestTypeID = @TestNum and IsLocked = 1
				Order by TestAppointmentID desc),
           @PassOrFail,
		   @Notes,
		   (select UserID from Users
				where UserName = @CreatedBy)
			)";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            Command.Parameters.AddWithValue("@TestNum", TestNum);
            Command.Parameters.AddWithValue("@PassOrFail", PassOrFail);
            Command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            Command.Parameters.AddWithValue("@Notes", Notes);


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

        static public bool DataPassOrFailByAppointmentIDAndTestNum(bool PassOrFail, int AppointmentID, int TestNum, string CreatedBy, string Notes)
        {
            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" UPDATE TestAppointments
   SET IsLocked = 1
 WHERE TestAppointmentID = @AppointmentID


INSERT INTO Tests
           (TestAppointmentID,
		   TestResult,
		   Notes,
		   CreatedByUserID)
     VALUES
           (@AppointmentID,
           @PassOrFail,
		   @Notes,
		   (select UserID from Users
				where UserName = @CreatedBy)
			)";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            Command.Parameters.AddWithValue("@TestNum", TestNum);
            Command.Parameters.AddWithValue("@PassOrFail", PassOrFail);
            Command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            Command.Parameters.AddWithValue("@Notes", Notes);


            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();

                if (result == 2)
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


    }
}
