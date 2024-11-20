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
    public partial class ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo : UserControl
    {
        public int _LDLAppID = -1;
        public int TestNum = -1;


        public ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo()
        {
            InitializeComponent();

        }

        void LoadDataToControls()
        {
            drivingLicenseApplicationInfo1._LDLAppID = _LDLAppID;
            applicationBasicInfo1._LDLAppID = _LDLAppID;
            drivingLicenseApplicationInfo1.ctrlDrivingLicenseApplicationInfo_Load();
            applicationBasicInfo1.ApplicationBasicInfo_Load();


            TestNum = drivingLicenseApplicationInfo1.TestNum;
        }


        private void ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo_Load(object sender, EventArgs e)
        {
            LoadDataToControls();
        }


        public void ctrlMatchAppLoad()
        {
            drivingLicenseApplicationInfo1.ctrlDrivingLicenseApplicationInfo_Load();
            applicationBasicInfo1.ApplicationBasicInfo_Load();
        }

    }
}
