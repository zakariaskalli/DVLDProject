using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;

namespace Business_Layer___DVLDProject
{
    public class clsMethodsGeneralBusiness
    {

        static public int PersonIDByLDLAppID(int LDLAppID)
        {
            return clsMethodsGeneralData.PersonIDByLDLAppID(LDLAppID);
        }

        static public string NameCountryByNumber(int NationalityCountryID)
        {
            return clsMethodsGeneralData.NameCountryByNumber(NationalityCountryID);

        }

        static public int PersonIDByNationalNo(string NationalNo)
        {
            return clsMethodsGeneralData.PersonIDByNationalNo(NationalNo);
        }

        public static bool IsFoundUserByPersonID(int PersonID)
        {
            return clsMethodsGeneralData.IsFoundUserByPersonID(PersonID);
        }

        public static bool IsFoundUserByNationalNo(string NationalNo)
        {
            return clsMethodsGeneralData.IsFoundUserByNationalNo(NationalNo);
        }

        public static bool IsUserNameFound(string UserName)
        {
            return clsMethodsGeneralData.IsUserNameFound(UserName);
        }

        public static int UserIDByUserName(string UserName)
        {
            return clsMethodsGeneralData.UserIDByUserName(UserName);
        }

        public static string UserNameByUserID(int UserID)
        {
            return clsMethodsGeneralData.UserNameByUserID(UserID);
        }

        static public bool IsLDLAppIDFound(int LDLAppID)
        {
            if (LDLAppID == -1)
                return false;

            return clsMethodsGeneralData.IsLDLAppIDFound(LDLAppID);
        }

        static public int LicenseNumberbyLicenseName(string LicenseName)
        {

            return clsMethodsGeneralData.LicenseNumberbyLicenseName(LicenseName);
        }

        static public string NationalNoByLDLAppID(int LDLAppID)
        {

            return clsMethodsGeneralData.NationalNoByLDLAppID(LDLAppID);

        }

        public static bool IsFoundLicenseByLicenseID(int LicenseID)
        {
            return clsMethodsGeneralData.IsFoundLicenseByLicenseID(LicenseID);
        }

        static public int LDLAppIDByLicenseID(int LicenseID)
        {
            return clsMethodsGeneralData.LDLAppIDByLicenseID(LicenseID);
        }

        static public int FeesNewInternationalLicenseApplication()
        {
            return clsMethodsGeneralData.FeesApplicationTypesByApplicationTypeID(6);
        }

        static public int FeesRenewDrivingLicenseService()
        {
            return clsMethodsGeneralData.FeesApplicationTypesByApplicationTypeID(2);
        }

        static public int FeesReplacementForDamagedDrivingLicensee()
        {
            return clsMethodsGeneralData.FeesApplicationTypesByApplicationTypeID(4);
        }

        static public int FeesReplacementForLostDrivingLicense()
        {
            return clsMethodsGeneralData.FeesApplicationTypesByApplicationTypeID(3);
        }

        static public int FeesReleaseDetainedDrivingLicense()
        {
            return clsMethodsGeneralData.FeesApplicationTypesByApplicationTypeID(5);
        }

        static public bool IsInternationalLicenseIDFound(int InternationalLicenseID)
        {
            if (InternationalLicenseID == -1)
                return false;

            return clsMethodsGeneralData.IsInternationalLicenseIDFound(InternationalLicenseID);
        }

        static public int PersonIDByApplicationID(int ApplicationID)
        {
            return clsMethodsGeneralData.PersonIDByApplicationID(ApplicationID);
        }

        static public void UpdateDataFormAllLicenses()
        {
            clsMethodsGeneralData.UpdateDataFormAllLicenses();
        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            if (LicenseID == -1)
                return false;

            return clsMethodsGeneralData.IsLicenseDetained(LicenseID);
        }

        static public bool IsHaveDataInLicense(int LicenseID)
        {
            if (LicenseID == -1)
                return false;

            return clsMethodsGeneralData.IsHaveDataInLicense(LicenseID);
        }

        static public bool IsDetainedIDFound(int DetainedID)
        {
            if (DetainedID == -1)
                return false;

            return clsMethodsGeneralData.IsDetainedIDFound(DetainedID);
        }

        static public bool IsDetainedCompleteByLicenseID(int LicenseID)
        {
            if (LicenseID == -1)
                return false;

            return clsMethodsGeneralData.IsDetainedCompleteByLicenseID(LicenseID);
        }

        static public int DetainedIDByLicenseID(int LicenseID)
        {
            if (LicenseID == -1)
                return -1;

            return clsMethodsGeneralData.DetainedIDByLicenseID(LicenseID);
        }

        static public int LicenseIDByLDLAppID(int LDLAppID)
        {
            if (LDLAppID == -1)
                return -1;

            return clsMethodsGeneralData.LicenseIDByLDLAppID(LDLAppID);
        }
    }
}
