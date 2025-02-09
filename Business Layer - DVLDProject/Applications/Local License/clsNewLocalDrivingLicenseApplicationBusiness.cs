using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Serialization;


namespace Business_Layer___DVLDProject
{
    [Serializable]
    public class clsNewLocalDrivingLicenseApplicationBusiness
    {
        enum enMode { UpdateMode = 0, AddNewMode = 1 }

        enMode _Mode;

        public clsNewLocalDrivingLicenseApplicationBusiness(int LocalDrivingLicenseApplicationID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.DrivingID = -1;
            this.NationalNo = "";
            this.ApplicationDate = DateTime.Now;
            this.LicenseClassID = -1;
            this.UserID = -1;

            _Mode = enMode.UpdateMode;
        }


        public clsNewLocalDrivingLicenseApplicationBusiness(int PersonID, int UserID, int LicenseClassID)
        {
            this.PersonID = PersonID;
            this.UserID = UserID;
            this.LicenseClassID = LicenseClassID;

            this.NationalNo = "";
            _Mode = enMode.AddNewMode;
        }

        public clsNewLocalDrivingLicenseApplicationBusiness(string NationalNo, int UserID, int LicenseClassID)
        {
            this.NationalNo = NationalNo;
            this.UserID = UserID;
            this.LicenseClassID = LicenseClassID;


            this.PersonID = -1;
            _Mode = enMode.AddNewMode;
        }

        public clsNewLocalDrivingLicenseApplicationBusiness(int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;

            _Mode = enMode.UpdateMode;
        }



        public int LocalDrivingLicenseApplicationID { get; set; }
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public int UserID { get; set; }
        public int LicenseClassID { get; set; }


        public int DrivingID { get; set; }
        public DateTime ApplicationDate { get; set; }


        public static int ApplicationFeesNewLocalDrivingLicenseService()
        {
            return clsNewLocalDrivingLicenseApplicationData.ApplicationFeesNewLocalDrivingLicenseService();
        }

        public static List<string> LoadAllApplicationTypes()
        {
            List<string> dataList = clsNewLocalDrivingLicenseApplicationData.LoadAllApplicationTypes();


            if (dataList != null)
                return dataList;
            else
                return null;
        }

        private bool AddLocalDriveLicAppToTableByPersonID()
        {
            int LocalDrivingLicenseApplicationID = clsNewLocalDrivingLicenseApplicationData.AddLocalDriveLicAppToTableByPersonID(PersonID, UserID, LicenseClassID);

            if (LocalDrivingLicenseApplicationID == -1)
                return false;
            else
            {
                this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
                return true;
            }
        }

        private bool AddLocalDriveLicAppToTableByNationalNo()
        {
            int LocalDrivingLicenseApplicationID = clsNewLocalDrivingLicenseApplicationData.AddLocalDriveLicAppToTableByNationalNo(NationalNo, UserID, LicenseClassID);

            if (LocalDrivingLicenseApplicationID == -1)
                return false;
            else
            {
                this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
                return true;
            }
        }

        private bool UpdateLocalDriveLicAppToTable()
        {
            bool IsUpdate = clsNewLocalDrivingLicenseApplicationData.UpdateLocalDriveLicAppToTable(LocalDrivingLicenseApplicationID, LicenseClassID);

            if (IsUpdate)
                return true;
            else
                return false;

        }

        static public clsNewLocalDrivingLicenseApplicationBusiness LoadAllDataUserByLocalDriveLicAppID(int LocalDrivingLicenseApplicationID)
        {
            clsNewLocalDrivingLicenseApplicationBusiness clsA = new clsNewLocalDrivingLicenseApplicationBusiness(LocalDrivingLicenseApplicationID);


            int DrivingID = -1;
            int PersonID = -1;
            int LicenseClassID = -1;
            int UserID = -1;
            DateTime ApplicationDate = DateTime.Now;
            


            clsNewLocalDrivingLicenseApplicationData.LoadAllDataUserByLocalDriveLicAppID(LocalDrivingLicenseApplicationID,
                                    ref DrivingID, ref PersonID,ref LicenseClassID,ref UserID, ref ApplicationDate);

            clsA.DrivingID = DrivingID;
            clsA.PersonID = PersonID;
            clsA.ApplicationDate = ApplicationDate;
            clsA.LicenseClassID = LicenseClassID;
            clsA.UserID = UserID;

            return clsA;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (NationalNo != "")
                    {
                        if (AddLocalDriveLicAppToTableByNationalNo())
                        {
                            _Mode = enMode.UpdateMode;
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (PersonID != -1)
                    {
                        if (AddLocalDriveLicAppToTableByPersonID())
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
                    if (UpdateLocalDriveLicAppToTable())
                    {
                        _Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                        return false;

            }

            return false;
        }

        static public bool IsFoundApplicationMatchLocalDriveByPersonID(int PersonID, int LicenseClassID)
        {
            return clsNewLocalDrivingLicenseApplicationData.IsFoundApplicationMatchLocalDriveByPersonID(PersonID, LicenseClassID);
        }

        static public bool IsFoundApplicationMatchLocalDriveByNationalNo(string NationalNo, int LicenseClassID)
        {
            return clsNewLocalDrivingLicenseApplicationData.IsFoundApplicationMatchLocalDriveByNationalNo(NationalNo, LicenseClassID);
        }

    }
}
