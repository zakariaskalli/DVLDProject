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
    public class clsInternationalLicenseApplicationsData
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = @"select InternationalLicenseID As 'Int.License ID',
                                ApplicationID As 'Application ID',
                                DriverID As 'Driver ID',
                                IssuedUsingLocalLicenseID As 'L.License ID',
                                IssueDate As 'Issue Date',
                                ExpirationDate As 'Expiration Date',
                                IsActive As 'Is Active'
                                from InternationalLicenses";

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

            string query = $@"select * from (select InternationalLicenseID As 'Int.License ID',
                                    ApplicationID As 'Application ID',
                                    DriverID As 'Driver ID',
                                    IssuedUsingLocalLicenseID As 'L.License ID',
                                    IssueDate As 'Issue Date',
                                    ExpirationDate As 'Expiration Date',
                                    IsActive As 'Is Active'
                                    from InternationalLicenses)S1
                                                    where S1.[{ColumnName}] Like '' + @Data + '%';";

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
