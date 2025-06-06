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
    public partial class frmDriverLicenseInfo : Form
    {
        readonly int _LicenseID = -1;

        public frmDriverLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

            ctrlLicenseInfo1._LicenseID = LicenseID;
        }

        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfo1.ctrlLicenseInfo_Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
