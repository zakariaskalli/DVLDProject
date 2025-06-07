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
    public class clsDriverLicensesBusiness
    {
        static public DataTable LoadAllLDLAppByDriverID(int DriverID)
        {
            clsDriverLicensesData clsData = new clsDriverLicensesData();


            DataTable dt = clsData.LoadAllLDLAppByDriverID(DriverID);

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable LoadAllInternationalLicensesByDriverID(int DriverID)
        {
            clsDriverLicensesData clsData = new clsDriverLicensesData();


            DataTable dt = clsData.LoadAllInternationalLicensesByDriverID(DriverID);

            if (dt != null)
                return dt;
            else
                return null;
        }

    }
}
