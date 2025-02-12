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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLDProject
{
    public partial class frmUserInfo : Form
    {

        int? _PersonID = null;
        int? _UserID = null;

        

        public frmUserInfo(string UserName)
        {
            InitializeComponent();

            clsUsers UserInfo = clsUsers.FindByUserName(UserName);
            
            _PersonID = UserInfo.PersonID;
            _UserID = UserInfo.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }
        //UserIDIsFound

        public frmUserInfo(int? UserID)
        {
            InitializeComponent();

            clsUsers UserInfo = clsUsers.FindByUserID(UserID);

            _PersonID = UserInfo.PersonID;
            _UserID = UserInfo.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }

    }
}
