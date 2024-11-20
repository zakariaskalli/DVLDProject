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
    public class clsInternationalLicenseInfoBusiness
    {

        public clsInternationalLicenseInfoBusiness(int InternationalLicenseID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.Name = "";
            this.LicenseID = -1;
            this.NationalNo = "";
            this.Gendor = "";
            this.IssueDate = "";
            this.ApplicationID = -1;
            this.IsActive = "";
            this.DateOfBirth = "";
            this.DriverID = -1;
            this.ExpirationDate = "";
            this.ImagePath = "";
        }

        public int InternationalLicenseID { get; set; }
        public string Name { get; set; }
        public int LicenseID { get; set; }
        public string NationalNo { get; set; }
        public string Gendor { get; set; }
        public string IssueDate { get; set; }
        public int ApplicationID { get; set; }
        public string IsActive { get; set; }
        public string DateOfBirth { get; set; }
        public int DriverID { get; set; }
        public string ExpirationDate { get; set; }
        public string ImagePath { get; set; }


        
        static public clsInternationalLicenseInfoBusiness LoadDataInternationalLicenseInfoByIntLicenseID(int InternationalLicenseID)
        {

            clsInternationalLicenseInfoBusiness clsA = new clsInternationalLicenseInfoBusiness(InternationalLicenseID);


            string Name = "";
            int LicenseID = -1;
            string NationalNo = "";
            int Gendor = 0;
            string IssueDate = "";
            int ApplicationID = -1;
            bool IsActive = false;
            string DateOfBirth = "";
            int DriverID = -1;
            string ExpirationDate = "";

            string ImagePath = "";


            clsInternationalLicenseInfoData.LoadDataInternationalLicenseInfoByIntLicenseID(InternationalLicenseID, ref Name, ref LicenseID, ref NationalNo,
                ref Gendor, ref IssueDate, ref ApplicationID, ref IsActive, ref DateOfBirth, ref DriverID, ref ExpirationDate, ref ImagePath);


            clsA.InternationalLicenseID = InternationalLicenseID;
            clsA.Name = Name;
            clsA.LicenseID = LicenseID;
            clsA.NationalNo = NationalNo;

            if (Gendor == 0)
                clsA.Gendor = "Male";
            else
                clsA.Gendor = "Female";

            clsA.IssueDate = IssueDate;
            clsA.ApplicationID = ApplicationID;

            if (IsActive)
                clsA.IsActive = "Yes";
            else
                clsA.IsActive = "No";

            clsA.DateOfBirth = DateOfBirth;
            clsA.DriverID = DriverID;
            clsA.ExpirationDate = ExpirationDate;


            clsA.ImagePath = ImagePath;


            return clsA;
        }


    }
}
