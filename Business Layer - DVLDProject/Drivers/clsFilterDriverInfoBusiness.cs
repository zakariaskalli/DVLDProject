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
    public class clsFilterDriverInfoBusiness
    {
        static public int IHaveInternationalLicenseByLicenseID(int LicenseID)
        {
            return clsFilterDriverInfoData.IHaveInternationalLicenseByLicenseID(LicenseID);
        }

        public static bool IsAllLicensesActiveByLicenseID(int LicenseID)
        {
            return clsFilterDriverInfoData.IsAllLicensesActiveByLicenseID(LicenseID);
        }

        public static bool IsJustLicenseActiveByLicenseID(int LicenseID)
        {
            return clsFilterDriverInfoData.IsJustLicenseActiveByLicenseID(LicenseID);
        }

        public static bool IsLicensePastExpirationDate(int LicenseID)
        {
            return clsFilterDriverInfoData.IsLicensePastExpirationDate(LicenseID);
        }

        public static bool CanIAddInternationalLicenseByLicenseID(int LicenseID)
        {
            return (IHaveInternationalLicenseByLicenseID(LicenseID) == -1
                        &&
                            IsAllLicensesActiveByLicenseID(LicenseID)
                                &&
                                    !IsLicensePastExpirationDate(LicenseID));
            
        }

        static public string ExpirationDateLicenseByLicenseID(int LicenseID)
        {
            return clsFilterDriverInfoData.ExpirationDateLicenseByLicenseID(LicenseID);
        }

    }
}
