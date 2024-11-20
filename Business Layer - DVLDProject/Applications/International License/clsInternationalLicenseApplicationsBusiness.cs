using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using Data_Layer_DVLDProject_clsPeople;

namespace Business_Layer___DVLDProject
{
    public class clsInternationalLicenseApplicationsBusiness
    {

        static public DataTable LoadData()
        {
            clsInternationalLicenseApplicationsData clsData = new clsInternationalLicenseApplicationsData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
        {
            DataTable dt = clsInternationalLicenseApplicationsData.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }

    }
}
