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
    public class clsLocalDrivingLicenseApplicationsBusiness
    {
       // enum enMode { UpdateMode = 0, AddNewMode = 1 }
       //
       // enMode _Mode;

        public clsLocalDrivingLicenseApplicationsBusiness(int DLAppInfo_LDLAppID)
        {
            this.DLAppInfo_LDLAppID = DLAppInfo_LDLAppID;
            this.DLAppInfo_LicenseName = "";
            this.DLAppInfo_NumTestsPassed = 0;

            //_Mode = enMode.UpdateMode;
        }

        public clsLocalDrivingLicenseApplicationsBusiness(int ABI_LDLAppID, string Empty)
        {
            this.ABI_ID = ABI_ID;
            this.ABI_Status = "";
            this.ABI_Fees = -1;
            this.ABI_Type = "";
            this.ABI_Applicant = "";
            this.ABI_Date = "";
            this.ABI_StatusDate = "";
            this.ABI_CreatedBy = "";

            //_Mode = enMode.UpdateMode;
        }


        public int DLAppInfo_LDLAppID { get; set; }
        public string DLAppInfo_LicenseName { get; set; }
        public int DLAppInfo_NumTestsPassed { get; set; }


        
        public int ABI_ID { get;set; }
        public string ABI_Status { get; set; }
        public int ABI_Fees { get; set; }
        public string ABI_Type {  get; set; }
        public string ABI_Applicant {  get; set; }
        public string ABI_Date { get; set; }
        public string ABI_StatusDate { get; set; }
        public string ABI_CreatedBy { get; set; }


        static public DataTable LoadData()
        {


            clsLocalDrivingLicenseApplicationsData clsData = new clsLocalDrivingLicenseApplicationsData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public bool CancelLicenseByNationalNo(string NationalNo, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationsData.CancelLicenseByNationalNo(NationalNo, LicenseClassID);
        }

        static public bool DeleteLicenseByLDLAppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.DeleteLicenseByLDLAppID(LDLAppID);
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
         {
            DataTable dt = clsLocalDrivingLicenseApplicationsData.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public clsLocalDrivingLicenseApplicationsBusiness UploadAllDataByDLAppInfo_LDLAppID(int DLAppInfo_LDlAppID)
        {
            clsLocalDrivingLicenseApplicationsBusiness clsData = new clsLocalDrivingLicenseApplicationsBusiness(DLAppInfo_LDlAppID);

            string LicenseName = "";
            int NumTestsPassed = 0;


            clsLocalDrivingLicenseApplicationsData.LoadDataDrivingLicenseAppInfo(DLAppInfo_LDlAppID, ref LicenseName, ref NumTestsPassed);

            clsData.DLAppInfo_LDLAppID = DLAppInfo_LDlAppID;
            clsData.DLAppInfo_LicenseName = LicenseName;
            clsData.DLAppInfo_NumTestsPassed = NumTestsPassed;

            return clsData;
        }

        static public clsLocalDrivingLicenseApplicationsBusiness UploadAllDataByABI_LDLAppID(int ABI_LDLAppID)
        {
            clsLocalDrivingLicenseApplicationsBusiness clsData = new clsLocalDrivingLicenseApplicationsBusiness(ABI_LDLAppID, "");

            int ABI_ID = -1;
            string ABI_Status = "";
            int ABI_Fees = -1;
            string ABI_Type = "";
            string ABI_Applicant = "";
            string ABI_Date = "";
            string ABI_StatusDate = "";
            string ABI_CreatedBy = "";

            clsLocalDrivingLicenseApplicationsData.LoadDataApplicationBasicInfo(ABI_LDLAppID,ref ABI_ID, ref ABI_Status, ref ABI_Fees, ref ABI_Type
                , ref ABI_Applicant, ref ABI_Date, ref ABI_StatusDate, ref ABI_CreatedBy);
            
            clsData.ABI_ID = ABI_ID;
            clsData.ABI_Status = ABI_Status;
            clsData.ABI_Fees = ABI_Fees;
            clsData.ABI_Type = ABI_Type;
            clsData.ABI_Applicant = ABI_Applicant;
            clsData.ABI_Date = ABI_Date;
            clsData.ABI_StatusDate = ABI_StatusDate;
            clsData.ABI_CreatedBy = ABI_CreatedBy;


            return clsData;
        }


    }
}
