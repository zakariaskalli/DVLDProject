using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Business_Layer___DVLDProject
{
    public class clsNewInternationalLicenseApplicationBusiness
    {

        static public bool AddNewRecordToTable(int LicenseID, string UserName, ref int InternationalLicenseID,
                            ref int ApplicationID)
        {

            return clsNewInternationalLicenseApplicationData.IssueInternationalLicense(LicenseID, UserName,
                                                                                    ref InternationalLicenseID,ref ApplicationID);
        
        }


    }
}
