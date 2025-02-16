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
using DVLD_BusinessLayer;

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
            clsApplicationTypes ApplicationInfo = clsApplicationTypes.FindByApplicationTypeID(_ID);


            lblID.Text = ApplicationInfo.ApplicationTypeID.ToString();
            tbTitle.Text = ApplicationInfo.ApplicationTypeTitle;
            tbFees.Text = ApplicationInfo.ApplicationFees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string Title = tbTitle.Text;
            decimal Fees = decimal.Parse(tbFees.Text);


            if (clsApplicationTypes.UpdateApplicationTypesByID(_ID, Title, Fees))
                MessageBox.Show("Data Update Successfully :-)", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data Update Successfully :-(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
