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
    public partial class frmAddEditPersonInfo : Form
    {
        int? _PersonID = -1;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();

        }

        public frmAddEditPersonInfo(string NationalNo)
        {
            InitializeComponent();



            _PersonID =  clsMethodsGeneralBusiness.PersonIDByNationalNo(NationalNo);

            ctrlAddEditPersonInfo1.PersonInfo.PersonID = _PersonID;
            
            lblAddOrUpdate.Text = "Update Person";
            lblPersonID.Text = _PersonID.ToString();
        }

        public int? GetPersonID()
        {
            return (int?)ctrlAddEditPersonInfo1.PersonInfo.PersonID;
        }

        public frmAddEditPersonInfo(int? PersonID)
        {
            
            InitializeComponent();

            ctrlAddEditPersonInfo1.PersonInfo.PersonID = PersonID;


            lblAddOrUpdate.Text = "Update Person";
            lblPersonID.Text = PersonID.ToString(); 
            
        }

        //public frmAddEditPersonInfo(string NationalNo)
        //{
        //    InitializeComponent();
        //
        //    ctrlAddEditPersonInfo1._NationalNo = NationalNo;
        //
        //
        //    lblAddOrUpdate.Text = "Update Person";
        //    .Text = PersonID.ToString();
        //}



        private void ctrlAddEditPersonInfo1_CloseButtonClicked_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ctrlAddEditPersonInfo1_OnSaveClicked_1(int obj)
        {
            _PersonID = obj;
            lblPersonID.Text = obj.ToString();
            lblAddOrUpdate.Text = "Update Person";
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {

        }

        private void ctrlAddEditPersonInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
