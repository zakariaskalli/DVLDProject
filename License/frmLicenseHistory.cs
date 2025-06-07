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
        clsDrivers DriverInfo = null;

        // Check Drivver ID is excist after call that
        public frmLicenseHistory(int DriverID)
        {
            InitializeComponent();

            if (clsDrivers.FindByDriverID(DriverID) != null)
            {
                DriverInfo = clsDrivers.FindByDriverID(DriverID);
            }

            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();
        }

        void LoadAllData()
        {
            if (DriverInfo != null)
            {
                ctrlFilterAndMakePersonInfo1._PersonID = DriverInfo.PersonID;
                ctrlFilterAndMakePersonInfo1.ctrlShowPersonDetails_Load();

                ctrlDriverLicenses1._DriverID = (int)DriverInfo.DriverID;
                ctrlDriverLicenses1.ctrlDriverLicenses_Load();



                ctrlFilterAndMakePersonInfo1.SelectCombobox(1);
                ctrlFilterAndMakePersonInfo1.textBoxData(DriverInfo.PersonID.ToString());
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

    }
}
