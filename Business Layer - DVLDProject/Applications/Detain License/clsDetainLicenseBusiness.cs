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
    public class clsDetainLicenseBusiness
    {

        static public void DetainLicense(int LicenseID, string CreatedBy, int Fees, ref int DetainedID)
        {

            clsDetainLicenseData.DetainLicense(LicenseID, CreatedBy, Fees, ref DetainedID);

        }

    }
}
