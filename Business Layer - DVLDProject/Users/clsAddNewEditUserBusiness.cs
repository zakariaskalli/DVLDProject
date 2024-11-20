using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Business_Layer___DVLDProject
{
    public class clsAddNewEditUserBusiness
    {
        enum enMode { UpdateMode = 0, AddNewMode = 1 }

        enMode _Mode;

        public clsAddNewEditUserBusiness(int UserID)
        {
            this.UserID = UserID;
            this.PersonID = -1;
            this.NationalNo = "";
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

            _Mode = enMode.UpdateMode;
        }

        public clsAddNewEditUserBusiness(int PersonID, string UserName, string Password, bool IsActive)
        {
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.NationalNo = "";
            _Mode = enMode.AddNewMode;
        }

        public clsAddNewEditUserBusiness(string NationalNo, string UserName, string Password, bool IsActive)
        {
            this.NationalNo = NationalNo;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.PersonID = -1;
            _Mode = enMode.AddNewMode;
        }

        public clsAddNewEditUserBusiness(int UserID, string NationalNo, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.NationalNo = NationalNo;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.PersonID = -1;
            _Mode = enMode.UpdateMode;
        }

        public clsAddNewEditUserBusiness(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.NationalNo = "";
            _Mode = enMode.UpdateMode;
        }

        

        public int UserID { get; set; }
        public int PersonID {  get; set; }
        public string NationalNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }



        public static bool IsFoundPerson(string ColumnName, string Data)
        {
            return clsAddNewEditUserData.IsFoundPerson(ColumnName, Data);
        }
        

        private bool AddUserToTableByPersonID()
        {

            int UserID = clsAddNewEditUserData.AddUserToTableByPersonID(PersonID, UserName, Password, IsActive);

            if (UserID == -1)
                return false;
            else
            {
                this.UserID = UserID;
                return true;
            }

        }

        private bool AddUserToTableByNationalNo()
        {
            int UserID =  clsAddNewEditUserData.AddUserToTableByNationalNo(NationalNo, UserName, Password, IsActive);

            if (UserID == -1)
                return false;
            else
            {
                this.UserID = UserID;
                return true;
            }
        }

        private bool UpdateUserToTableByPersonID()
        {

            bool IsUpdate = clsAddNewEditUserData.UpdateUserToTableByPersonID(UserID, PersonID, UserName, Password, IsActive);


            if (IsUpdate)
                return true;
            else
                return false;

        }

        private bool UpdateUserToTableByNationalNo()
        {

            bool IsUpdate = clsAddNewEditUserData.UpdateUserToTableByNationalNo(UserID, NationalNo, UserName, Password, IsActive);


            if (IsUpdate)
                return true;
            else
                return false;

        }

        static public clsAddNewEditUserBusiness UploadAllDataUserByUserID(int UserID)
        {
            clsAddNewEditUserBusiness clsA = new clsAddNewEditUserBusiness(UserID);

            int PersonID = -1;
            string UserName = "";
            bool IsActive = false;

            clsAddNewEditUserData.LoadAllDataUserByUserID(UserID, ref PersonID,ref UserName,ref IsActive);

            clsA.PersonID = PersonID;
            clsA.UserName = UserName;
            clsA.IsActive = IsActive;


            return  clsA;
        }

        static public bool UserIDIsFound(int UserID)
        {
            if (clsAddNewEditUserData.ColumnDataIsAvailableByInt("UserID", UserID))
                return true;
            else
                return false;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (NationalNo != "")
                    {
                        if (AddUserToTableByNationalNo())
                        {
                            _Mode = enMode.UpdateMode;
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (PersonID != -1)
                    {
                        if (AddUserToTableByPersonID())
                        {
                            _Mode = enMode.UpdateMode;
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                case enMode.UpdateMode:
                    if (NationalNo != "")
                    {
                        if (UpdateUserToTableByNationalNo())
                        {
                            _Mode = enMode.UpdateMode;
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (PersonID != -1)
                    {
                        if (UpdateUserToTableByPersonID())
                        {
                            _Mode = enMode.UpdateMode;
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
            }

            return false;
        }


        public static bool UpdatePasswordUser(int UserID, string OldPassword, string NewPassword)
        {
            return clsAddNewEditUserData.UpdatePasswordUser(UserID, OldPassword, NewPassword);
        }


    }
}
