using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmListDetainedLicenses : Form
    {
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadComboBoxFilterBy()
        {
            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("Detain ID");
            cbSearchBy.Items.Add("Is Released");
            cbSearchBy.Items.Add("National No");
            cbSearchBy.Items.Add("Full Name");
            cbSearchBy.Items.Add("Release Application ID");

            cbSearchBy.SelectedIndex = 0;

        }

        private void LoadComboBoxIsActive()
        {
            cbIsReleased.Items.Add("All");
            cbIsReleased.Items.Add("Yes");
            cbIsReleased.Items.Add("No");

            cbIsReleased.SelectedIndex = 0;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsListDetainedLicensesBusiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";


            //D.ID
            //L.ID
            //D.Date
            //Is Released
            //Fine Fees
            //Release Date
            //N.No
            //Full Name
            //Release App.ID

            //980

            dataGridView1.Columns["D.ID"].Width = 75;
            dataGridView1.Columns["L.ID"].Width = 75;
            dataGridView1.Columns["D.Date"].Width = 135;
            dataGridView1.Columns["Is Released"].Width = 70;
            dataGridView1.Columns["Fine Fees"].Width = 80;
            dataGridView1.Columns["Release Date"].Width = 110;
            dataGridView1.Columns["N.No"].Width = 65;
            dataGridView1.Columns["Full Name"].Width = 200;
            dataGridView1.Columns["Release App.ID"].Width = 120;

        }

        private void frmListDetainedLicencses_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilterBy();
            LoadComboBoxIsActive();

            LoadAllDataToDGV();
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSearchBy.Text)
            {
                case "None":
                    tbFilterByData.Visible = false;
                    cbIsReleased.Visible = false;
                    break;
                case "Detain ID":
                    tbFilterByData.Visible = true;
                    cbIsReleased.Visible = false;
                    break;
                case "Is Released":
                    tbFilterByData.Visible = false;
                    cbIsReleased.Visible = true;
                    cbIsReleased.Location = new Point(230, cbIsReleased.Location.Y);
                    break;
                case "National No":
                    tbFilterByData.Visible = true;
                    cbIsReleased.Visible = false;
                    break;
                case "Full Name":
                    tbFilterByData.Visible = true;
                    cbIsReleased.Visible = false;
                    break;
                case "Release Application ID":
                    tbFilterByData.Visible = true;
                    cbIsReleased.Visible = false;
                    break;
                default:
                    break;
            }

        }

        private void tbFilterByData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchBy.Text == "Detain ID" || cbSearchBy.Text == "Release Application ID")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }

        private string ColumnName()
        {
            switch (cbSearchBy.Text)
            {
                case "Detain ID":
                    return "D.ID";
                case "National No":
                    return "N.No";
                case "Release Application ID":
                    return "Release App.ID";
                default:
                    return cbSearchBy.Text;
            }

        }


        private void tbFilterByData_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsListDetainedLicensesBusiness.SearchInTable(ColumnName(), tbFilterByData.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbIsReleased.Text == "All")
            {
                LoadAllDataToDGV();
                return;
            }

            int YesNo;

            if (cbIsReleased.Text == "No")
                YesNo = 0;
            else
                YesNo = 1;


            DataTable dataTable = clsListDetainedLicensesBusiness.SearchDataIsActive(YesNo);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsMethodsGeneralBusiness.PersonIDByNationalNo(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            
            if (PersonID != -1)
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

        private void btnRelease_Click_1(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            LoadAllDataToDGV();
        }

        private void clsDetain_Click_1(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            LoadAllDataToDGV();
        }

        private void ShowLicenseDetailsStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());

            if (clsLicenses.FindByLicenseID((int)LicenseID) != null)
            {
                Form frm = new frmDriverLicenseInfo(LicenseID);
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int LDLAppID = clsMethodsGeneralBusiness.LDLAppIDByLicenseID(int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString()));

            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)LDLAppID) != null)
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

        private void ReleaseDetainedLicenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());


            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
            LoadAllDataToDGV();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Convert.ToInt16(dataGridView1.CurrentRow.Cells[3].Value) == 0)
                ReleaseDetainedLicenesToolStripMenuItem.Enabled = true;
            else
                ReleaseDetainedLicenesToolStripMenuItem.Enabled = false;

        }
    }
}
