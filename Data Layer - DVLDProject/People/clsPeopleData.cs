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

namespace Data_Layer_DVLDProject_clsPeople
{
    public class clsDataDVLD_clsPeople
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                            People.ThirdName, People.LastName,
                            Case
                            	when People.Gendor = 0 then 'Male'
                            	when People.Gendor = 1 then 'Female'
                            	Else 'None'
                            END as Gendor,
                            People.DateOfBirth,
                            Countries.CountryName as Nationality, People.Phone, People.Email
                            FROM            People INNER JOIN
                                                     Countries ON People.NationalityCountryID = Countries.CountryID";

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

            //ColumnName = "PersonID";
            //Data = "10";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from 
                            ( SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName,
                            People.ThirdName, People.LastName,
                            Case
                            	when People.Gendor = 0 then 'Male'
                            	when People.Gendor = 1 then 'Female'
                            	Else 'None'
                            END as Gendor,
                            People.DateOfBirth,
                            Countries.CountryName as Nationality, People.Phone, People.Email
                            FROM            People INNER JOIN
                                                     Countries ON People.NationalityCountryID = Countries.CountryID
                            )S1
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

        static public bool DeletePersonByID(int PersonID)
        {
            bool IsDelete = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"Delete from People
                            where PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            

            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();

                if (result > 0)
                    IsDelete = true;
                else
                    IsDelete = false;


            }
            finally
            {
                connection.Close();
            }
            return IsDelete;
        }

    }
}
