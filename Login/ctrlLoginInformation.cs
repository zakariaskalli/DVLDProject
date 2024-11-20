using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDProject
{
    public partial class ctrlLoginInformation : UserControl
    {
        public int _UserID = -1;

        public ctrlLoginInformation()
        {
            InitializeComponent();
        }


        private void LoadAllData()
        {
            if (clsManageUsersBussiness.UserIDIsFound(_UserID))
            {
                clsManageUsersBussiness clsA = clsManageUsersBussiness.UploadAllDataByUserID(_UserID);

                lblUserID.Text = clsA.UserID.ToString();
                lblUserName.Text = clsA.UserName.ToString();

                if (clsA.IsActive)
                    lblIsActive.Text = "Is Active";
                else
                    lblIsActive.Text = "Is Not Active";

            }
        }

        private void ctrlLoginInformation_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }
    }
}
