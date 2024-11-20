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
    public partial class frmInternationalDriverInfo : Form
    {
        public int _InternationalID = -1;

        public frmInternationalDriverInfo(int internationalID)
        {
            InitializeComponent();
            _InternationalID = internationalID;
            //ctrlInternationalLicenseInfo1._InternationalLicenseID = internationalID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalDriverInfo_Load(object sender, EventArgs e)
        {
            if (clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(_InternationalID))
            {
                ctrlInternationalLicenseInfo1.ctrlInternationalLicenseInfo_Load(_InternationalID);
            }

        }
    }
}
