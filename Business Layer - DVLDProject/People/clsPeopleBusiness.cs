using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using Data_Layer_DVLDProject_clsPeople;

namespace Business_Layer_DVLDProject_clsPeople
{
    public class clsBusinessDVLD_clsPeople
    {

        static public DataTable LoadData()
        {
            clsDataDVLD_clsPeople clsData = new clsDataDVLD_clsPeople();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public DataTable SearchInTable(string ColumnName, string Data)
        {
            DataTable dt = clsDataDVLD_clsPeople.SearchData(ColumnName, Data);

            if (dt != null)
                return dt;
            else
                return null;
        }


        static public bool DeletePersonByID(int PeronID)
        {
            if (clsDataDVLD_clsPeople.DeletePersonByID(PeronID))
                return true;
            else
                return false;
        }

    }
}
