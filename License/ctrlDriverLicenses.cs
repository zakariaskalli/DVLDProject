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
    public partial class ctrlDriverLicenses : UserControl
    {
        public int _LDLAppID = -1;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();
        }


        void LoadAllData()
        {
            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)_LDLAppID) != null)
            {
                // Load Data To LocalDrivingApplication Data GridViw
                DataTable dataTable1 = clsDriverLicensesBusiness.LoadAllLDLApp(_LDLAppID);
                dataGridView1.DataSource = dataTable1;
                lblRecord1.Text = $"# Record: {dataGridView1.RowCount}";

                // Load Data To LocalDrivingApplication Data GridViw
                DataTable dataTable2 = clsDriverLicensesBusiness.LoadAllInternationalLicenses(_LDLAppID);
                dataGridView2.DataSource = dataTable2;
                lblRecord2.Text = $"# Record: {dataGridView2.RowCount}";

                // Load DataGridView1  884  880
                dataGridView1.Columns["Lic ID"].Width = 85;
                dataGridView1.Columns["App ID"].Width = 90;
                dataGridView1.Columns["Class Name"].Width = 210;
                dataGridView1.Columns["IssueDate"].Width = 150;
                dataGridView1.Columns["ExpirationDate"].Width = 150;
                dataGridView1.Columns["IsActive"].Width = 140;
                // Load DataGridView2
                if (dataTable2 != null)
                {
                    dataGridView2.Columns["Int.License ID"].Width = 85;
                    dataGridView2.Columns["Application ID"].Width = 90;
                    dataGridView2.Columns["L.License ID"].Width = 210;
                    dataGridView2.Columns["IssueDate"].Width = 150;
                    dataGridView2.Columns["ExpirationDate"].Width = 150;
                    dataGridView2.Columns["IsActive"].Width = 140;

                }


            }

        }

        private void ctrlDriverLicenses_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        public void ctrlDriverLicenses_Load()
        {
            LoadAllData();
        }

        private void gbDriverLicences_Enter(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            if (clsLicenses.FindByLicenseID((int)LicenseID) != null)
            {
                Form frm = new frmDriverLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int IntLicenseID = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());

            if (clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(IntLicenseID))
            {
                Form frm = new frmInternationalDriverInfo(IntLicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("This IntLicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
