using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer___DVLDProject;

namespace DVLDProject
{
    public partial class frmUpdateApplicationType : Form
    {
        int _ID = -1;

        public frmUpdateApplicationType(int ID)
        {
            InitializeComponent();

            _ID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            clsManageApplicationTypesBusiness clsA = new clsManageApplicationTypesBusiness(_ID);

            clsA = clsManageApplicationTypesBusiness.UploadAllDataByID(_ID);

            lblID.Text = clsA.ID.ToString();
            tbTitle.Text = clsA.Title;
            tbFees.Text = clsA.Fees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string Title = tbTitle.Text;
            int Fees = int.Parse(tbFees.Text);

            clsManageApplicationTypesBusiness clsA = new clsManageApplicationTypesBusiness(_ID, Title, Fees);

            if (clsA.UpdateDataToTableByID())
                MessageBox.Show("Data Update Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data Update Successfully :-(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
