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

namespace DVLDProject.Applications.Local_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        public frmLocalDrivingLicenseApplicationInfo(int lDLAppID)
        {
            InitializeComponent();
            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1._LDLAppID = lDLAppID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.ctrlMatchAppLoad();

        }
    }
}
