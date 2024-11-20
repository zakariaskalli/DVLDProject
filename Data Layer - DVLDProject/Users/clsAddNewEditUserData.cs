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
    public class clsAddNewEditUserData
    {

        public static bool IsFoundPerson(string ColumnName, string Data)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Found = 1 from People
                            where {ColumnName} = @Data";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@Data", Data);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
                else
                    IsFound = false;


            }
            finally
            {
                connection.Close();
            }

            return IsFound;
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

        
        public static int AddUserToTableByPersonID(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            Password = clsHashing.ConvertTextToHash(Password);

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"INSERT INTO Users
                                        (PersonID,UserName,Password,IsActive)
                            VALUES
                                        (@PersonID,@UserName,@Password,@IsActive)
                            select SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", UserName);

            Command.Parameters.AddWithValue("@Password", Password);

            if (IsActive == true)
                Command.Parameters.AddWithValue("@IsActive", 1);
            else
                Command.Parameters.AddWithValue("@IsActive", 0);

            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                    UserID = InsertID;
                else
                    UserID = -1;


            }
            finally
            {
                connection.Close();
            }

            return UserID;

        }

        public static int AddUserToTableByNationalNo(string NationalNo, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            Password = clsHashing.ConvertTextToHash(Password);

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"INSERT INTO Users
                                        ( PersonID,UserName,Password,IsActive)
                            VALUES
                                        (   (Select S1.PersonID from
				                            (SELECT      People.PersonID from People
				                            where NationalNo = @NationalNo)S1),
                                        @UserName,@Password,@IsActive)
                            select SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@UserName", UserName);

            Command.Parameters.AddWithValue("@Password", Password);

            if (IsActive == true)
                Command.Parameters.AddWithValue("@IsActive", 1);
            else
                Command.Parameters.AddWithValue("@IsActive", 0);

            try
            {
                connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertID))
                    UserID = InsertID;
                else
                    UserID = -1;


            }
            finally
            {
                connection.Close();
            }

            return UserID;

        }

        static public bool UpdateUserToTableByPersonID(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool IsUpdate = false;

            Password = clsHashing.ConvertTextToHash(Password);

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"UPDATE [dbo].[Users]
                              SET PersonID = @PersonID,
                                 UserName = @UserName,
                                 Password = @Password,
                                 IsActive = @IsActive
                            WHERE UserID = @UserID";


            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);


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

        static public bool UpdateUserToTableByNationalNo(int UserID, string NationalNo, string UserName, string Password, bool IsActive)
        {
            bool IsUpdate = false;

            Password = clsHashing.ConvertTextToHash(Password);

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"UPDATE [dbo].[Users]
                              SET PersonID = (Select S1.PersonID from
											(SELECT      People.PersonID from People
											where NationalNo = @NationalNo)S1   ),
                                 UserName = @UserName,
                                 Password = @Password,
                                 IsActive = @IsActive
                            WHERE UserID = @UserID";


            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@NationalNo", NationalNo);
            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);


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

        static public void LoadAllDataUserByUserID(int UserID, ref int PersonID,
            ref string UserName, ref bool IsActive)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from Users where UserID = @UserID;";

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


        static public bool UpdatePasswordUser(int UserID, string OldPassword, string NewPassword)
        {

            bool IsUpdate = false;

            OldPassword = clsHashing.ConvertTextToHash(OldPassword);
            NewPassword = clsHashing.ConvertTextToHash(NewPassword);


            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"Update Users
                            set Password = @NewPassword
                            where Users.UserID = @UserID and Password = @OldPassword";


            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@OldPassword", OldPassword);
            Command.Parameters.AddWithValue("@NewPassword", NewPassword);


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


    }
}
