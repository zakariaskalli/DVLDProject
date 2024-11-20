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
    public class clsRenewLocalDrivingLicenseBusiness
    {
        static public void RenewLocalDrivingLicense(int LicenseID, string CreatedBy, string Notes, ref int RLAppID, ref int RenewedLicenseID)
        {

            clsRenewLocalDrivingLicenseData.RenewLocalDrivingLicense(LicenseID, CreatedBy, Notes, ref RLAppID, ref RenewedLicenseID);

        }

    }
}
