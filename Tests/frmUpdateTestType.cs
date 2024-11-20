using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmUpdateTestType : Form
    {
        int _ID = -1;

        public frmUpdateTestType(int ID)
        {
            InitializeComponent();

            _ID = ID;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            clsManageTestTypesBusiness clsA = new clsManageTestTypesBusiness(_ID);
            
            clsA = clsManageTestTypesBusiness.UploadAllDataByID(_ID);

            lblID.Text = clsA.ID.ToString();
            tbTitle.Text = clsA.Title;
            tbFees.Text = clsA.Fees.ToString();
            tbDescription.Text = clsA.Description;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string Title = tbTitle.Text;
            string Description = tbDescription.Text;
            int Fees = int.Parse(tbFees.Text);


            clsManageTestTypesBusiness clsA = new clsManageTestTypesBusiness(_ID, Title, Description, Fees);

            if (clsA.UpdateDataToTableByID())
                MessageBox.Show("Data Update Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data Update Successfully :-(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
