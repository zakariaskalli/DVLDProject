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

namespace DVLDProject
{
    public partial class frmLicenseHistory : Form
    {
        int _LDLAppID = -1;
        int _PersonID = -1;

        public frmLicenseHistory(int lDLAppID)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;

            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LDLAppID) != null)
            {
                _PersonID = clsMethodsGeneralBusiness.PersonIDByLDLAppID(_LDLAppID);
            }

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();
        }

        void LoadAllData()
        {
            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LDLAppID) != null)
            {
                ctrlFilterAndMakePersonInfo1._PersonID = _PersonID;
                ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

                ctrlDriverLicenses1._LDLAppID = _LDLAppID;
                ctrlDriverLicenses1.ctrlDriverLicenses_Load();



                ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
                ctrlFilterAndMakePersonInfo1.textBoxData(_PersonID.ToString());
                ctrlFilterAndMakePersonInfo1.DisabledFilterBy();
            }

        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        public void frmLicenseHistory_Load()
        {
            LoadAllData();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenses1_Load(object sender, EventArgs e)
        {

        }
    }
}
