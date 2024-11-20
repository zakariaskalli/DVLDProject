using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;

namespace Business_Layer___DVLDProject
{
    public class clsLicenseInfoBusiness
    {
        enum enIssueReason { enFirstTime = 1, enLostItem = 2, enCompensationForLoss = 3, enRenewal = 4 }
        
        public clsLicenseInfoBusiness(int LDLAppID)
        {
            this.LDLAppID = LDLAppID;
            this.Class = "";
            this.Name = "";
            this.LicenseID = -1;
            this.NationalNo = "";
            this.Gendor = "";
            this.IssueDate = "";
            this.IssueReason = "";
            this.Notes = "";
            this.IsActive = "";
            this.DateOfBirth = "";
            this.DriverID = -1;
            this.ExpirationDate = "";
            this.IsDetained = "";
            this.ImagePath = "";
        }

        public int LDLAppID { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public int LicenseID { get; set; }
        public string NationalNo { get; set; }
        public string Gendor { get; set; }
        public string IssueDate { get; set; }
        public string IssueReason { get; set; }
        public string Notes { get; set; }
        public string IsActive { get; set; }
        public string DateOfBirth { get; set; }
        public int DriverID { get; set; }
        public string ExpirationDate {  get; set; }
        public string IsDetained {  get; set; }
        public string ImagePath { get; set; }


        static string IssueReasonByID(int IssueReasonID)
        {
            enIssueReason enRaison = (enIssueReason)IssueReasonID;

            switch (enRaison)
            {
                case enIssueReason.enFirstTime:
                    return "First Time";
                case enIssueReason.enLostItem:
                    return "Renew";
                case enIssueReason.enCompensationForLoss:
                    return "Replacement For Lost";
                case enIssueReason.enRenewal:
                    return "Replacement For Damage";
                default:
                    return "";
            }
        }

        static public clsLicenseInfoBusiness LoadDataLicenseInfoByPersonID(int LDLAppID)
        {

            clsLicenseInfoBusiness clsA = new clsLicenseInfoBusiness(LDLAppID);




            string Class = "";
            string Name = "";
            int LicenseID = -1;
            string NationalNo = "";
            int Gendor = 0;
            string IssueDate = "";
            int IssueReason = 0;
            string Notes = "";
            bool IsActive = false;
            string DateOfBirth = "";
            int DriverID = -1;
            string ExpirationDate = "";
            int IsDetained = 0;

            string ImagePath = "";

            clsLicenseInfoData.LoadDataLicenseInfoByLDLAppID(LDLAppID, ref Class, ref Name, ref LicenseID,
                ref NationalNo, ref Gendor, ref IssueDate, ref IssueReason, ref Notes, ref IsActive,
                ref DateOfBirth, ref DriverID, ref ExpirationDate, ref IsDetained, ref ImagePath);

            clsA.LDLAppID = LDLAppID;
            clsA.Class = Class;
            clsA.Name = Name;
            clsA.LicenseID = LicenseID;
            clsA.NationalNo = NationalNo;

            if (Gendor == 0)
                clsA.Gendor = "Male";
            else
                clsA.Gendor = "Female";

            clsA.IssueDate = IssueDate;
            clsA.IssueReason = IssueReasonByID(IssueReason);
            clsA.Notes = Notes;

            if (IsActive)
                clsA.IsActive = "Yes";
            else
                clsA.IsActive = "No";

            clsA.DateOfBirth = DateOfBirth;
            clsA.DriverID = DriverID;
            clsA.ExpirationDate = ExpirationDate;
            
            if (IsDetained == 1)
                clsA.IsDetained = "Yes";
            else
                clsA.IsDetained = "No";


            clsA.ImagePath = ImagePath;


            return clsA;
        }



    }
}
