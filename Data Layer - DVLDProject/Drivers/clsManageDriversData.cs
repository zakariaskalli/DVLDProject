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
    public class clsManageDriversData
    {

        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"
select DriverID, PersonID, NationalNo, FullName, CreatedDate As Date, NumberOfActiveLicenses As 'Active Licenses'

from Drivers_View";

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

            string query = $@"select * from Drivers_View
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


    }
}
