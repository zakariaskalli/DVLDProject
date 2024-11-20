using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using Data_Layer_DVLDProject_clsPeople;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Business_Layer___DVLDProject
{
    public class clsManageApplicationTypesBusiness
    {
        public clsManageApplicationTypesBusiness(int ID) 
        {
            this.ID = ID;
            Title = "";
            Fees = 0;
        
        }

        public clsManageApplicationTypesBusiness(int ID, string Title, int Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public int Fees { get; set; }

        static public DataTable LoadData()
        {
            clsManageAppliactionTypesData clsData = new clsManageAppliactionTypesData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public clsManageApplicationTypesBusiness UploadAllDataByID(int ID)
        {
            clsManageApplicationTypesBusiness clsData = new clsManageApplicationTypesBusiness(ID);

            string Title = "";
            int Fees = 0;

            clsManageAppliactionTypesData.LoadDataByID(ID, ref Title, ref Fees);

            clsData.Title = Title;
            clsData.Fees = Fees;

            return clsData;

        }

        //UpdateDataToTableByID
        public bool UpdateDataToTableByID()
        {

            bool IsUpdate = clsManageAppliactionTypesData.UpdateDataToTableByID(ID,Title,Fees);


            if (IsUpdate)
                return true;
            else
                return false;

        }

    }
}
