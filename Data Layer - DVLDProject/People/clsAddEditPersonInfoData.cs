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
     public class clsAddEditPersonInfoData
     {

        static public DataTable LoadAllCountriesName() 
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" Select CountryName from Countries;";

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

        // ?? Gendor
        static public int AddRecordToTable(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int? PersonID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting))
                {
                    connection.Open();
                    string query = @"
                                INSERT INTO [dbo].[People]
                                       (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
                                        Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
                            VALUES
                                       (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,
                                        @Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath)
                             select SCOPE_IDENTITY();";

                    using (SqlCommand Command = new SqlCommand(query, connection))
                    {
                        Command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        Command.Parameters.AddWithValue("@FirstName", FirstName);

                        if (SecondName == "")
                            Command.Parameters.AddWithValue("@SecondName", System.DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@SecondName", SecondName);

                        if (ThirdName == "")
                            Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@ThirdName", ThirdName);

                        if (LastName == "")
                            Command.Parameters.AddWithValue("@LastName", System.DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@LastName", LastName);

                        Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        Command.Parameters.AddWithValue("@Gendor", Gendor);
                        Command.Parameters.AddWithValue("@Address", Address);
                        Command.Parameters.AddWithValue("@Phone", Phone);

                        if (Email == "")
                            Command.Parameters.AddWithValue("@Email", System.DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@Email", Email);

                        Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

                        if (ImagePath == "")
                            Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                        else
                            Command.Parameters.AddWithValue("@ImagePath", ImagePath);

                        object result = Command.ExecuteScalar();

                        //if (result != null && int.TryParse(result.ToString(), out int InsertID))
                        if (int.TryParse(result.ToString(), out int InsertID))
                            PersonID = InsertID;
                        else
                            PersonID = -1;
                    }
                }
            }
            catch
            {

            }
            
            
            

            return PersonID ?? -1;
        }

        static public bool UpdateRecordToTable(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" UPDATE [dbo].[People]
                            SET NationalNo = @NationalNo
                            ,FirstName = @FirstName
                            ,SecondName = @SecondName
                            ,ThirdName = @ThirdName
                            ,LastName = @LastName
                            ,DateOfBirth = @DateOfBirth
                            ,Gendor = @Gendor
                            ,Address = @Address
                            ,Phone = @Phone
                            ,Email = @Email
                            ,NationalityCountryID = @NationalityCountryID
                            ,ImagePath = @ImagePath
                            WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@FirstName", FirstName);

            if (SecondName == "")
                Command.Parameters.AddWithValue("@SecondName", System.DBNull.Value);
            else
                Command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName == "")
                Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);

            if (LastName == "")
                Command.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            else
                Command.Parameters.AddWithValue("@LastName", LastName);

            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", Gendor);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@Phone", Phone);

            if (Email == "")
                Command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            else
                Command.Parameters.AddWithValue("@Email", Email);

            Command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath == "")
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            else
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            
            Command.Parameters.AddWithValue("@PersonID", PersonID);


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

        static public bool ColumnDataIsAvailableByString(string ColumnName, string Data)
        {
            bool IsAvialable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"
                            select Find = 1 from People
                            where {ColumnName} = @Data;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@Data", Data);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvialable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvialable;
        }

        static public bool ColumnDataIsAvailableByInt(string ColumnName, int Data)
        {
            bool IsAvialable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"
                            select Find = 1 from People
                            where {ColumnName} = @Data;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@Data", Data);


            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                {
                    IsAvialable = true;
                }
            }
            finally
            {
                connection.Close();
            }

            return IsAvialable;
        }


        // Commant system null in Image Path
        static public void LoadDataByPersonID(int PersonID, ref string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref int Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from People where PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];

                    SecondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

        }

        static public void LoadDataByNationalNo( string NationalNo, ref int PersonID,
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref int Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from People where NationalNo = @NationalNo;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];

                    SecondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                    ThirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

        }

        // SearchDataByPersonID



        /*
        static public void SearchDataByPersonID(int PersonID, ref string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref  int Gendor, ref string Address, ref string Phone,
            ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from People where PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (int)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = (string)reader["ImagePath"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

        }
        */
    }
}
