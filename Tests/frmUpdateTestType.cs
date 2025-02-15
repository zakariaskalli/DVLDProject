using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
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

            clsTestTypes TestTypesInfo = clsTestTypes.FindByTestTypeID(_ID);

            lblID.Text = TestTypesInfo.TestTypeID.ToString();
            tbTitle.Text = TestTypesInfo.TestTypeTitle;
            tbFees.Text = TestTypesInfo.TestTypeFees.ToString();
            tbDescription.Text = TestTypesInfo.TestTypeDescription;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string Title = tbTitle.Text;
            string Description = tbDescription.Text;
            decimal Fees = decimal.Parse(tbFees.Text);

            if (clsTestTypes.UpdateTestTypesByID(_ID, Title, Description, Fees))
                MessageBox.Show("Data Update Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data Update Successfully :-(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like backspace, delete, etc.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Blocks the character from being entered
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && (tbFees.Text.Contains(".") || tbFees.Text.Length == 0))
            {
                e.Handled = true;
            }
        }

        private void tbFees_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ValidateChildren(); // Trigger Validating event on all controls
                btnSave.PerformClick();  // Simulate Save button click
                e.SuppressKeyPress = true; // Prevents the 'ding' sound on Enter key
            }
        }
    }
}
