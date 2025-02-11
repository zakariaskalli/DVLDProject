using Data_Layer___DVLDProject;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsRegistryUtility
    {
        static public void DeleteAllDataInRememberMeTable()
        {
            string keypath = @"SOFTWARE\DVLD";
            string valueUserName = "UserName";
            string valuePasswordName = "Password";

            try
            {
                using (RegistryKey basekey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = basekey.OpenSubKey(keypath, true))
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
                    }
                }
            }
            catch (Exception ex)
            {
                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "Delete Data In Registry because: " + ex.ToString(), EventLogEntryType.Error);
            }
        }

        static public void RememberMeInfo(string UserName, string Password)
        {

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

        static public bool LoadUserNameAndPasswordRememberMe(ref string UserName, ref string Password)
        {
            bool IsAvailableData = false;

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
            catch (Exception ex)
            {
                string sourceName = "DVLD";

                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "Don't Add User Name And Password For Remember Me because: " + ex.ToString(), EventLogEntryType.Error);

            }

            return IsAvailableData;
        }


        static public void RememberMe(bool RememberOrNot, string UserName, string Password)
        {


            if (RememberOrNot)
            {
                DeleteAllDataInRememberMeTable();
                RememberMeInfo(UserName, Password);
            }
            else
                DeleteAllDataInRememberMeTable();

        }
    }
}
