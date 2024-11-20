using Business_Layer___DVLDProject;
using DVLDProject.Applications.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDProject
{
    public partial class frmLocalDrivingLicenseAppliactions : Form
    {
        readonly string _UserName = "";
        
        public frmLocalDrivingLicenseAppliactions(string UserName)
        {
            InitializeComponent();

            _UserName = UserName;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsLocalDrivingLicenseApplicationsBusiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

            // Motwait 95

            dataGridView1.Columns["LDLAppID"].Width = 90;
            dataGridView1.Columns["Driving Class"].Width = 185;
            dataGridView1.Columns["NationalNo"].Width = 80;
            dataGridView1.Columns["FullName"].Width = 208;
            dataGridView1.Columns["ApplicationDate"].Width = 115;
            dataGridView1.Columns["PassedTestCount"].Width = 110;
            dataGridView1.Columns["Status"].Width = 80;


        }

        private void LoadAllComboBoxFilterBy()
        {
            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("LDLAppID");
            cbSearchBy.Items.Add("NationalNo");
            cbSearchBy.Items.Add("FullName");
            cbSearchBy.Items.Add("Status");

            cbSearchBy.SelectedIndex = 0;
        }

        private void LoadComboBoxStatus()
        {
            cbStatus.Items.Add("All");
            cbStatus.Items.Add("New");
            cbStatus.Items.Add("Cancelled");
            cbStatus.Items.Add("Completed");

            cbStatus.SelectedIndex = 0;
        
        }


        private void frmLocalDrivingLicenseAppliactions_Load(object sender, EventArgs e)
        {
            LoadAllComboBoxFilterBy();
            LoadComboBoxStatus();

            LoadAllDataToDGV();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication(_UserName);

            frm.ShowDialog();
            LoadAllDataToDGV();

        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSearchBy.Text)
            {
                case "None":
                    tbFilterByData.Visible = false;
                    cbStatus.Visible = false;
                    break;
                case "LDLAppID":
                    tbFilterByData.Visible = true;
                    cbStatus.Visible = false;
                    break;
                case "National No":
                    tbFilterByData.Visible = true;
                    cbStatus.Visible = false;
                    break;
                case "FullName":
                    tbFilterByData.Visible = true;
                    cbStatus.Visible = false;
                    break;
                case "Status":
                    tbFilterByData.Visible = false;
                    cbStatus.Visible = true;
                    cbStatus.Location = new Point(230, cbStatus.Location.Y);
                    break;
                default:
                    break;
            }
        }


        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int _LocalDrivingLicenseApplicationID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication(_UserName, _LocalDrivingLicenseApplicationID);

            frm.ShowDialog();
            LoadAllDataToDGV();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to cancel this application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            
            
            
            string NationalNo = (string)dataGridView1.CurrentRow.Cells[2].Value;
            int LicenseClassID = clsMethodsGeneralBusiness.LicenseNumberbyLicenseName((string)dataGridView1.CurrentRow.Cells[1].Value);



            if (clsLocalDrivingLicenseApplicationsBusiness.CancelLicenseByNationalNo(NationalNo, LicenseClassID))
            {
                MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Application wasn't Cancelled because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadAllDataToDGV();

        }

        private void tbFilterByData_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsLocalDrivingLicenseApplicationsBusiness.SearchInTable(cbSearchBy.Text, tbFilterByData.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbStatus.Text == "All") 
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsLocalDrivingLicenseApplicationsBusiness.SearchInTable(cbSearchBy.Text, cbStatus.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";



        }

        private void EnabledAllContextMenuStrip()
        {
            
            showApplicationDetailsToolStripMenuItem.Enabled = true;
            editApplicationToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            sechToolStripMenuItem.Enabled = true;


            sechduleVisionTestToolStripMenuItem.Enabled = true;
            sechduleWrittenTestToolStripMenuItem.Enabled = true;
            scheduleToolStripMenuItem.Enabled = true;


            issueDrivingLicenseToolStripMenuItem.Enabled = true;
            ShowLicenseToolStripMenuItem.Enabled = true;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //sechToolStripMenuItem

            int PassedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;
            string Status = (string)dataGridView1.CurrentRow.Cells[6].Value;

            EnabledAllContextMenuStrip();

            if (PassedTests == 0 && Status == "New")
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem.Enabled = false;
                sechduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleToolStripMenuItem.Enabled = false;
            }
            else if (PassedTests == 1 && Status == "New")
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem.Enabled = false;
                sechduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleToolStripMenuItem.Enabled = false;
            }
            else if (PassedTests == 2 && Status == "New")
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem.Enabled = false;
                sechduleVisionTestToolStripMenuItem.Enabled = false;
                sechduleWrittenTestToolStripMenuItem.Enabled = false;
            }
            else if (PassedTests == 3 && Status == "New")
            {
                sechToolStripMenuItem.Enabled = false;
                //sechduleVisionTestToolStripMenuItem.Enabled = false;
                //sechduleWrittenTestToolStripMenuItem.Enabled = false;
                //scheduleToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem.Enabled = false;

            }
            else if (PassedTests == 3 && Status == "Completed")
            {
                

                sechToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;


            }
            else if (Status == "Cancelled")
            {
                cancelApplicationToolStripMenuItem.Enabled = false;
                sechToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem.Enabled = false;

            }
        }
        private void TakeAScheduleTest()
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            int TestNum = (int)dataGridView1.CurrentRow.Cells[5].Value + 1;

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                frmTestAppointements Frm = new frmTestAppointements(LDLAppID, TestNum);

                Frm.ShowDialog();
            }
            else
                MessageBox.Show("LDLAppID IS Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            LoadAllDataToDGV();
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeAScheduleTest();
        }

        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeAScheduleTest();
        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeAScheduleTest();
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            int TestNum = (int)dataGridView1.CurrentRow.Cells[5].Value + 1;

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                frmIssueDriverLicenseForTheFirstTime Frm = new frmIssueDriverLicenseForTheFirstTime(LDLAppID, TestNum);

                Frm.ShowDialog();
            }
            else
                MessageBox.Show("LDLAppID IS Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            LoadAllDataToDGV();
        }

        private void ShowLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";


                frmDriverLicenseInfo Frm = new frmDriverLicenseInfo(LDLAppID);

                Frm.ShowDialog();

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This LDLAppID Is Not Found", "Error Not Found", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            Form frm = new frmLocalDrivingLicenseApplicationInfo(LDLAppID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";


                frmLicenseHistory Frm = new frmLicenseHistory(LDLAppID);

                Frm.ShowDialog();

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This LDLAppID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm", "Are you sure do want to Delete this application?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            if (clsLocalDrivingLicenseApplicationsBusiness.DeleteLicenseByLDLAppID(LDLAppID))
            {
                MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Application wasn't Deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadAllDataToDGV();
        }

        private void showPersoLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                cbSearchBy.SelectedIndex = 0;
                tbFilterByData.Text = "";


                frmLicenseHistory Frm = new frmLicenseHistory(LDLAppID);

                Frm.ShowDialog();

                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("This LDLAppID Is Not Found", "Error Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelApplicationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            int TestNum = (int)dataGridView1.CurrentRow.Cells[5].Value + 1;

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(LDLAppID))
            {
                frmIssueDriverLicenseForTheFirstTime Frm = new frmIssueDriverLicenseForTheFirstTime(LDLAppID, TestNum);

                Frm.ShowDialog();
            }
            else
                MessageBox.Show("LDLAppID IS Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            LoadAllDataToDGV();
        }
    }
}
