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
    public class clsIssueDriverLicenseBusiness
    {

        static public bool IssueDriverLicenseFirstTime(int LDLAppID, string CreatedBy, string Notes, ref int LicenseID)
        {
            if (clsIssueDriverLicenseData.IssueDriverLicenseFirstTime(LDLAppID, CreatedBy, Notes,ref LicenseID))
            {
                if (LicenseID != -1)
                    return true;
                else
                    return false;
            }
            return false;
        }

    }
}
