using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using Data_Layer_DVLDProject_clsPeople;

namespace Business_Layer___DVLDProject
{
    public class clsManageUsersBussiness
    {
        //enum enMode { UpdateMode = 0, AddNewMode = 1 }

        //enMode _Mode;

        public clsManageUsersBussiness(int UserID) 
        {
            this.UserID = UserID;
            //this.PersonID = -1;
            this.UserName = "";
            this.IsActive = false;

            //_Mode = enMode.UpdateMode;
        }

        public clsManageUsersBussiness(string UserName)
        {
            this.UserName = UserName;
            //this.PersonID = -1;
            this.UserID = -1;
            this.IsActive = false;

            //_Mode = enMode.UpdateMode;
        }


        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public bool IsActive {  get; set; }


        static public clsManageUsersBussiness UploadAllDataByUserID(int UserID)
        {
            clsManageUsersBussiness clsData = new clsManageUsersBussiness(UserID);

            int PersonID = -1;
            string UserName = "";
            bool IsActive = false;

            clsManageUsersData.LoadDataByUserID(UserID,ref PersonID, ref UserName,ref IsActive);

            clsData.UserID = UserID;
            clsData.PersonID = PersonID;
            clsData.UserName = UserName;
            clsData.IsActive = IsActive;


            return clsData;
        }

        static public bool UserIDIsFound(int UserID)
        {
            if (clsManageUsersData.ColumnDataIsAvailableByInt("UserID", UserID))
                return true;
            else
                return false;
        }

        static public clsManageUsersBussiness UploadAllDataByUserName(string UserName)
        {
            clsManageUsersBussiness clsData = new clsManageUsersBussiness(UserName);

            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;

            clsManageUsersData.LoadDataByUserName(UserName,ref PersonID, ref UserID, ref IsActive);
            
            clsData.UserName = UserName;
            clsData.PersonID = PersonID;
            clsData.UserID = UserID;
            clsData.IsActive = IsActive;


            return clsData;
        }

        static public bool UserNameIsFound(string UserName)
        {
            if (clsManageUsersData.ColumnDataIsAvailableByString("UserName", UserName))
                return true;
            else
                return false;
        }

        static public DataTable LoadData()
        {
            clsManageUsersData clsData = new clsManageUsersData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public bool DeleteUserByID(int UserID)
        {
            if (clsManageUsersData.DeleteUserByID(UserID))
                return true;
            else
                return false;
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
        {
            DataTable dt = clsManageUsersData.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchDataIsActive(int IsActive)
        {
            DataTable dt = clsManageUsersData.SearchDataIsActive(IsActive);

            if (dt != null)
                return dt;
            else
                return null;
        }

    }
}
