using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Business_Layer___DVLDProject
{
    public class clsReleaseDetainedLicenseBusiness
    {
        static public void RenewLocalDrivingLicense(int DetainID, ref string DetainDate, ref int FineFees, ref int UserID)
        {

            clsReleaseDetainedLicenseData.RenewLocalDrivingLicense(DetainID,ref DetainDate, ref FineFees, ref UserID);

        }

        static public void ReleaseDetainedDrivingLicense(int LicenseID, string UserName, ref int ApplicationID)
        {
            clsReleaseDetainedLicenseData.ReleaseDetainedDrivingLicense(LicenseID, UserName, ref ApplicationID);

        }
    }
}
