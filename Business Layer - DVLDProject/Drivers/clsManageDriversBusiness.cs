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
    public class clsManageDriversBusiness
    {
        static public DataTable LoadData()
        {
            clsManageDriversData clsData = new clsManageDriversData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
        {
            DataTable dt = clsManageDriversData.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }



    }
}
