using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
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
        public int? _UserID = null;

        public ctrlLoginInformation()
        {
            InitializeComponent();
        }


        private void LoadAllData()
        {
            if (clsManageUsersBussiness.UserIDIsFound((int)_UserID))
            {
                clsManageUsersBussiness clsA = clsManageUsersBussiness.UploadAllDataByUserID((int)_UserID);

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

    }
}
