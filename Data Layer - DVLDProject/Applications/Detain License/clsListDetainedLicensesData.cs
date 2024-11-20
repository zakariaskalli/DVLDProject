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
    public class clsListDetainedLicensesData
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"select 
									DetainID As 'D.ID',
									LicenseID As 'L.ID',
									DetainDate As 'D.Date',
									IsReleased As 'Is Released',
									FineFees As 'Fine Fees',
									ReleaseDate As 'Release Date',
									(select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID ))) As 'N.No',
									(select top 1 FullName from LocalDrivingApplicationTable 
																				where NationalNo = (select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID )))) As 'Full Name',
									ReleaseApplicationID As 'Release App.ID'
									from DetainedLicenses
                                ";

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

        static public DataTable SearchData(string ColumnName, string Data)
        {
            DataTable dt = new DataTable();


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from
                                (select 
									DetainID As 'D.ID',
									LicenseID As 'L.ID',
									DetainDate As 'D.Date',
									IsReleased As 'Is Released',
									FineFees As 'Fine Fees',
									ReleaseDate As 'Release Date',
									(select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID ))) As 'N.No',
									(select top 1 FullName from LocalDrivingApplicationTable 
																				where NationalNo = (select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID )))) As 'Full Name',
									ReleaseApplicationID As 'Release App.ID'
									from DetainedLicenses
                                )S1
                                    where [{ColumnName}] Like '' + @Data + '%';";

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

        static public DataTable SearchDataIsActive(int IsActive)
        {
            DataTable dt = new DataTable();

            //ColumnName = "PersonID";
            //Data = "10";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);


            string query = $@"select * from
                                (
								select 
									DetainID As 'D.ID',
									LicenseID As 'L.ID',
									DetainDate As 'D.Date',
									IsReleased As 'Is Released',
									FineFees As 'Fine Fees',
									ReleaseDate As 'Release Date',
									(select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID ))) As 'N.No',
									(select top 1 FullName from LocalDrivingApplicationTable 
																				where NationalNo = (select NationalNo from People  where PersonID = (select ApplicantPersonID from Applications
													where ApplicationID =  (select ApplicationID from Licenses 
																					where Licenses.LicenseID = DetainedLicenses.LicenseID )))) As 'Full Name',
									ReleaseApplicationID As 'Release App.ID'
									from DetainedLicenses
                                )S1
                                    where [Is Released] = @IsActive;";


            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@IsActive", IsActive);


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

    }
}
