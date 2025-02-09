using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;


namespace Business_Layer___DVLDProject
{
    public class clsListDetainedLicensesBusiness
    {
        static public DataTable LoadData()
        {
            clsListDetainedLicensesData clsData = new clsListDetainedLicensesData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
        {
            DataTable dt = clsListDetainedLicensesData.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchDataIsActive(int IsActive)
        {
            DataTable dt = clsListDetainedLicensesData.SearchDataIsActive(IsActive);

            if (dt != null)
                return dt;
            else
                return null;
        }


    }
}
