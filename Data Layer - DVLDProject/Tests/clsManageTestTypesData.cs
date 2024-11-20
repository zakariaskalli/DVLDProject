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
    public class clsManageTestTypesData
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @" select TestTypeID As ID, TestTypeTitle As Title, TestTypeDescription As Description,
                                TestTypeFees As Fees from TestTypes";

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

        static public void LoadDataByID(int ID, ref string Title, ref string Description, ref int Fees)
        {

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"select TestTypeID As ID, TestTypeTitle As Title, TestTypeDescription As Description, TestTypeFees As Fees
                                from TestTypes
                            where TestTypeID = @ID;";


            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    Title = (string)reader["Title"];
                    Description = (string)reader["Description"];
                    Fees = (int)((decimal)reader["Fees"]);

                }

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }

        static public bool UpdateDataToTableByID(int ID, string Title, string Description, int Fees)
        {
            bool IsUpdate = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"UPDATE TestTypes
                              SET TestTypeTitle = @Title,
                                  TestTypeDescription = @Description,
                                  TestTypeFees = @Fees
                            WHERE TestTypeID = @ID";


            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
            Command.Parameters.AddWithValue("@Fees", Fees);

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
