using Data_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer___DVLDProject
{
    public class clsApplicationNewLicenseInfoBusiness
    {

        static public void LoadDataByLicenseID(int LicenseID, ref string LicenseFees,
                                                        ref string ExpirationDate)
        {

            clsApplicationNewLicenseInfoData.LoadDataByLicenseID(LicenseID,ref LicenseFees,ref ExpirationDate);

        }


    }
}
