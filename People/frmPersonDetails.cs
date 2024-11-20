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
    public partial class frmPersonDetails : Form
    {        
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();

            ctrlShowPersonDetails1._PersonID = PersonID;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }

        public frmPersonDetails(string NationalNo)
        {
            InitializeComponent();

            ctrlShowPersonDetails1._NationalNo = NationalNo;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
        }

        private void ctrlShowPersonDetails1_Load(object sender, EventArgs e)
        {
            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
        }
    }
}
