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

        private void ctrlLoginInformation_Load(object sender, EventArgs e)
        {
            if (clsUsers.FindByUserID(_UserID) != null)
            {
                clsUsers UserInfo = clsUsers.FindByUserID(_UserID);


                lblUserID.Text = UserInfo.UserID.ToString();
                lblUserName.Text = UserInfo.UserName.ToString();

                if (UserInfo.IsActive)
                    lblIsActive.Text = "Is Active";
                else
                    lblIsActive.Text = "Is Not Active";

            }
        }

    }
}
