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
    public class clsManageUsersData
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"SELECT Users.UserID, Users.PersonID,
                                (People.FirstName + ' ' + 
                                ISNULL(People.SecondName, '') + ' ' + 
                                ISNULL(People.ThirdName, '') + ' ' + 
                                ISNULL(People.LastName, '')) AS FullName,
                                Users.UserName, Users.IsActive
                            FROM   Users 
                            INNER JOIN People ON Users.PersonID = People.PersonID;";

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

        static public void LoadDataByUserID(int UserID, ref int PersonID , ref string UserName, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Users.UserID, Users.UserName, Users.PersonID, Users.IsActive from Users
                            where UserID = @UserID;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    IsActive = (bool)reader["IsActive"];

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }
        
        static public void LoadDataByUserName(string UserName, ref int PersonID ,ref int UserID, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Users.UserID, Users.UserName, Users.PersonID, Users.IsActive from Users
                            where UserName = @UserName;";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@UserName", UserName);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    IsActive = (bool)reader["IsActive"];

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        static public bool ColumnDataIsAvailableByInt(string ColumnName, int Data)
        {
            bool IsAvialable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"
                            select Find = 1 from Users
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

        static public bool ColumnDataIsAvailableByString(string ColumnName, string Data)
        {
            bool IsAvialable = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"
                            select Find = 1 from Users
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
        static public bool DeleteUserByID(int UserID)
        {
            bool IsDelete = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"Delete from Users
                            where UserID = @UserID;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);


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

        static public DataTable SearchData(string ColumnName, string Data)
        {
            DataTable dt = new DataTable();


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from
                                (
                                    SELECT Users.UserID, Users.PersonID,
                                    (People.FirstName + ' ' + 
                                    ISNULL(People.SecondName, '') + ' ' + 
                                    ISNULL(People.ThirdName, '') + ' ' + 
                                    ISNULL(People.LastName, '')) AS FullName,
                                    Users.UserName, Users.IsActive
                                    FROM   Users 
                                    INNER JOIN People ON Users.PersonID = People.PersonID
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

        static public DataTable SearchDataIsActive(int IsActive)
        {
            DataTable dt = new DataTable();

            //ColumnName = "PersonID";
            //Data = "10";

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);


            string query = $@"select * from
                                (
                                    SELECT Users.UserID, Users.PersonID,
                                    (People.FirstName + ' ' + 
                                    ISNULL(People.SecondName, '') + ' ' + 
                                    ISNULL(People.ThirdName, '') + ' ' + 
                                    ISNULL(People.LastName, '')) AS FullName,
                                    Users.UserName, Users.IsActive
                                    FROM   Users 
                                    INNER JOIN People ON Users.PersonID = People.PersonID
                                )S1
                                    where IsActive = @IsActive;";


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
