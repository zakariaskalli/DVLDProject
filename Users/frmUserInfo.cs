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

namespace DVLDProject
{
    public partial class frmUserInfo : Form
    {

        int _PersonID = -1;
        int _UserID = -1;

        

        public frmUserInfo(string UserName)
        {
            InitializeComponent();

            clsManageUsersBussiness clsData = new clsManageUsersBussiness(UserName);


            clsData = clsManageUsersBussiness.UploadAllDataByUserName(UserName);
            
            _PersonID = clsData.PersonID;
            _UserID = clsData.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }
        //UserIDIsFound

        public frmUserInfo(int UserID)
        {
            InitializeComponent();

            clsManageUsersBussiness clsData = new clsManageUsersBussiness(UserID);


            clsData = clsManageUsersBussiness.UploadAllDataByUserID(UserID);

            _PersonID = clsData.PersonID;
            _UserID = clsData.UserID;

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlLoginInformation1._UserID = _UserID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            //ctrlShowPersonDetails1._PersonID = _PersonID;
            //ctrlLoginInformation1._UserID = _UserID;
        
        }
    }
}
