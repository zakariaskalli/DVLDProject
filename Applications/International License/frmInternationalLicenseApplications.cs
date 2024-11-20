using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmInternationalLicenseApplications : Form
    {
        public frmInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void LoadAllComboBoxFilterBy()
        {
            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("Int.License ID");
            cbSearchBy.Items.Add("Application ID");
            cbSearchBy.Items.Add("Driver ID");
            cbSearchBy.Items.Add("L.License ID");
            cbSearchBy.Items.Add("Is Active");

            cbSearchBy.SelectedIndex = 0;
        }

        private void LoadComboBoxStatus()
        {
            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("Yes");
            cbIsActive.Items.Add("No");

            cbIsActive.SelectedIndex = 0;

        }

        void MeasureDataGridView()
        {
            if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
            {
                // DataGridView is not null and has data
                dataGridView1.Columns["Int.License ID"].Width = 115;
                dataGridView1.Columns["Application ID"].Width = 115;
                dataGridView1.Columns["Driver ID"].Width = 115;
                dataGridView1.Columns["L.License ID"].Width = 140;
                dataGridView1.Columns["Issue Date"].Width = 150;
                dataGridView1.Columns["Expiration Date"].Width = 150;
                dataGridView1.Columns["Is Active"].Width = 100;
            }
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsInternationalLicenseApplicationsBusiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

            // Motwait 100
            MeasureDataGridView();



        }


        private void frmInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            LoadAllComboBoxFilterBy();
            LoadComboBoxStatus();

            LoadAllDataToDGV();



        }

        private void btnAddInternationalLicense_Click(object sender, EventArgs e)
        {

            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();

            frm.ShowDialog();

            LoadAllDataToDGV();
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cbSearchBy.Text)
            {
                case "None":
                    tbFilterByData.Visible = false;
                    cbIsActive.Visible = false;
                    break;
                case "Int.License ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Application ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Driver ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "L.License ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Is Active":
                    tbFilterByData.Visible = false;
                    cbIsActive.Visible = true;
                    cbIsActive.Location = new Point(230, cbIsActive.Location.Y);
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFilterByData_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsInternationalLicenseApplicationsBusiness.SearchInTable(cbSearchBy.Text, tbFilterByData.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
            
                MeasureDataGridView();

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "All")
            {
                LoadAllDataToDGV();
                return;
            }
            int SelectedItemNum = -1;

            if (cbIsActive.Text == "Yes")
            {
                SelectedItemNum = 1;
            }
            else if (cbIsActive.Text == "No")
            {
                SelectedItemNum = 0;

            }

            DataTable dataTable = clsInternationalLicenseApplicationsBusiness.SearchInTable(cbSearchBy.Text, SelectedItemNum.ToString());
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
            MeasureDataGridView();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsMethodsGeneralBusiness.PersonIDByApplicationID(int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString()));
            
            if (clsMethodsGeneralBusiness.IsFoundUserByPersonID(PersonID))
            {
                Form frm = new frmPersonDetails(PersonID);
                frm.ShowDialog();

                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This PersonID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLicenseDetailsStripMenuItem_Click(object sender, EventArgs e)
        {
            int IntLicenseID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            if (clsMethodsGeneralBusiness.IsInternationalLicenseIDFound(IntLicenseID))
            {
                Form frm = new frmInternationalDriverInfo( IntLicenseID);
                frm.ShowDialog();

                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This IntLicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = clsMethodsGeneralBusiness.LDLAppIDByLicenseID(int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString()));

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                Form frm = new frmLicenseHistory(LDLAppID);
                frm.ShowDialog();

                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This LicenseID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
    }
}
