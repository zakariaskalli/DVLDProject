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
    public class clsReplacementForDamagedOrLostLicensesBusiness
    {

        static public void ReplacementLicenseForDamaged(int LicenseID, string CreatedBy, ref int LRAppID, ref int ReplacedLicenseID)
        {

            clsReplacementForDamagedOrLostLicensesData.ReplacementLicenseForDamaged(LicenseID, CreatedBy, ref LRAppID, ref ReplacedLicenseID);

        }

        static public void ReplacementLicenseForLost(int LicenseID, string CreatedBy, ref int LRAppID, ref int ReplacedLicenseID)
        {

            clsReplacementForDamagedOrLostLicensesData.ReplacementLicenseForLost(LicenseID, CreatedBy, ref LRAppID, ref ReplacedLicenseID);

        }

    }
}
