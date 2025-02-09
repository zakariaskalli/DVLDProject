using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Business_Layer___DVLDProject
{
    public class clsManageTestTypesBusiness
    {

        public clsManageTestTypesBusiness(int ID)
        {
            this.ID = ID;
            Title = "";
            Description = "";
            Fees = 0;

        }

        public clsManageTestTypesBusiness(int ID,string Title, string Description, int Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public int Fees { get; set; }
        public string Description { get; set; }


        static public DataTable LoadData()
        {
            clsManageTestTypesData clsData = new clsManageTestTypesData();

            DataTable dt = clsData.LoadData();

            if (dt != null)
                return dt;
            else
                return null;
        }

        static public clsManageTestTypesBusiness UploadAllDataByID(int ID)
        {
            clsManageTestTypesBusiness clsData = new clsManageTestTypesBusiness(ID);

            string Title = "";
            string Description = "";
            int Fees = 0;

            clsManageTestTypesData.LoadDataByID(ID,ref Title, ref Description, ref Fees);

            //clsManageTestTypesData

            clsData.Title = Title;
            clsData.Fees = Fees;
            clsData.Description = Description;

            return clsData;

        }

        //UpdateDataToTableByID
        public bool UpdateDataToTableByID()
        {

            bool IsUpdate = clsManageTestTypesData.UpdateDataToTableByID(ID, Title, Description, Fees);


            if (IsUpdate)
                return true;
            else
                return false;

        }
    }
}
