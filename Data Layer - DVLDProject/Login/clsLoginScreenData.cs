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
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System.Diagnostics;
using System.Configuration;

namespace Data_Layer___DVLDProject
{
    public class clsLoginScreenData
    {
        static public bool IsUserNameAndPasswordExciting(string UserName, string Password)
        {
            bool IsFound = false;

            //Convert Password To Hash
            Password = clsHashing.ConvertTextToHash(Password);

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Find = 1 from Users
                            where UserName = @UserName and Password = @Password;";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);


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

        static public bool IsAccountActive(string UserName)
        {
            bool IsActive = false;

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select Find = 1 from Users
                            where UserName = @UserName  and IsActive = 1";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null)
                    IsActive = true;
                else
                    IsActive = false;


            }
            finally
            {
                connection.Close();
            }

            return IsActive;
        }

        static public void RememberMeInfo(string UserName, string Password)
        {
            // Using DB

            /*
            UserName = UserName.Trim();
            Password = Password.Trim();

            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"INSERT INTO RememberMe
                            (UserName
                            ,Password)
                             VALUES
                            (@UserName
                            ,@Password)";

            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@UserName", UserName);
            Command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();


            }
            finally
            {
                connection.Close();
            }
            */

            // Using Windows Registry



            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            string valueUserName = "UserName";
            string valueUserNameData = UserName;

            string valuePasswordName = "Password";
            string valuePasswordData = Password;

            try
            {

                Registry.SetValue(keyPath, valueUserName, valueUserNameData, RegistryValueKind.String);
                Registry.SetValue(keyPath, valuePasswordName, valuePasswordData, RegistryValueKind.String);

                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "The Remember Me Is Active in Registry.", EventLogEntryType.Information);

            }
            catch (Exception ex)
            {
                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "The Remember Me Is Not Active in Registry because: " + ex.ToString(), EventLogEntryType.Error);

            }
        }

        static public void DeleteAllDataInRememberMeTable()
        {
            /*
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@" truncate table RememberMe";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                int result = Command.ExecuteNonQuery();


            }
            finally
            {
                connection.Close();
            }
            */

            string keypath = @"SOFTWARE\DVLD";
            string valueUserName = "UserName";
            string valuePasswordName = "Password";
        
            try
            {
                using (RegistryKey basekey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key  = basekey.OpenSubKey(keypath, true))
                    {
                        if (key != null)
                        {
                            key.DeleteValue(valueUserName);
                            key.DeleteValue(valuePasswordName);

                            string sourceName = "DVLD";

                            if (!EventLog.SourceExists(sourceName))
                            {
                                EventLog.CreateEventSource(sourceName, "Application");
                            }

                            EventLog.WriteEntry(sourceName, "Delete Data In Registry.", EventLogEntryType.Information);

                        }
                        else
                        {
                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "Delete Data In Registry because: " + ex.ToString(), EventLogEntryType.Error);

            }
        }

        static public bool LoadUserNameAndPasswordRememberMe(ref string UserName,ref string Password)
        {
            bool IsAvailableData = false;

            // Using DB

            /*
            SqlConnection connection = new SqlConnection(ConnectionSQL.connectionStarting);

            string query = $@"select * from RememberMe";

            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {
                    UserName = (string)reader["UserName"];
                    UserName = UserName.Trim();
                    Password = (string)reader["Password"];
                    Password = Password.Trim();
                    IsAvailableData = true;
                }
                else
                    IsAvailableData = false;

                reader.Close();
            }
            finally
            {
                connection.Close();
            }
            */

            // Using Windows Registry

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            string valueUserName = "UserName";

            string valuePasswordName = "Password";

            try
            {
                UserName = Registry.GetValue(keyPath, valueUserName, null) as string;
                Password = Registry.GetValue(keyPath, valuePasswordName, null) as string;

                if (UserName != null && Password != null)
                {
                    IsAvailableData = true;

                    string sourceName = "DVLD";

                    if (!EventLog.SourceExists(sourceName))
                    {
                        EventLog.CreateEventSource(sourceName, "Application");
                    }

                    EventLog.WriteEntry(sourceName, "Add User Name And Password For Remember Me.", EventLogEntryType.Information);

                }
                else
                    IsAvailableData = false;


            }
            catch(Exception ex)
            {
                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "Don't Add User Name And Password For Remember Me because: "+ ex.ToString(), EventLogEntryType.Error);

            }

            return IsAvailableData;
        }
    
    }
}
