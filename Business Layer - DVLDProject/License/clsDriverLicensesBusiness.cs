using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using Data_Layer_DVLDProject_clsPeople;


namespace Business_Layer___DVLDProject
{
    public class clsDriverLicensesBusiness
    {
        static public DataTable LoadAllLDLApp(int LDLAppID)
        {
            clsDriverLicensesData clsData = new clsDriverLicensesData();


            DataTable dt = clsData.LoadAllLDLApp(LDLAppID);

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable LoadAllInternationalLicenses(int LDLAppID)
        {
            clsDriverLicensesData clsData = new clsDriverLicensesData();


            DataTable dt = clsData.LoadAllInternationalLicenses(LDLAppID);

            if (dt != null)
                return dt;
            else
                return null;
        }

    }
}
