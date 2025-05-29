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
    public partial class frmTestAppointements : Form
    {

        int TestAppointmentID = -1;
        int TestNum = -1;

        clsTestsAppointmentsBusiness clsA = null;


        public frmTestAppointements(int lDLAppID, int TestNum)
        {
            InitializeComponent();
            
            clsA = new clsTestsAppointmentsBusiness(lDLAppID, TestNum);

            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1._LDLAppID = clsA.LDLAppID;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsTestsAppointmentsBusiness.LoadTestAppointmentByLDLAppID(clsA.TestNum, clsA.LDLAppID);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

        }


        private void frmVisionTestAppointements_Load(object sender, EventArgs e)
        {
            //ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1
            if (clsA.TestNum == 1)
            {
                pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\eye.png";
                lblTestAppointmentName.Text = "Vision Test Appointments";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Text = "Vision Test Appointments";


            }
            else if (clsA.TestNum == 2)
            {
                pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\test.png";
                lblTestAppointmentName.Text = "Written Test Appointments";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Text = "Written Test Appointments";
            }
            else if (clsA.TestNum == 3)
            {
                pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\StreetTest.png";
                lblTestAppointmentName.Text = "Street Test Appointments";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Text = "Street Test Appointments";
            }

            LoadAllDataToDGV();
            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.ctrlMatchAppLoad();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if  (
                 (!clsA.IHaveAnAppointmentByLDLAppIDAndTestNum()) 
                 ||
                 (clsA.IsTestAppointmentsLockedByLDLAppID() && !clsA.IsTestAppointmentsFinished())
                )
            {
                frmScheduleTest frm = new frmScheduleTest(clsA.LDLAppID, clsA.TestNum);
                frm.ShowDialog();
            }
            else if (!clsA.IsTestAppointmentsLockedByLDLAppID())
            {
                MessageBox.Show(
                    "Person Already have an active appointment for this test, You Cannot add new appointment",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (clsA.IsTestAppointmentsFinished())
            {
                MessageBox.Show(
                "Person Already passed this test before, You Can only retake failed test",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadAllDataToDGV();

            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.ctrlMatchAppLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            TestNum = ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.TestNum;

            frmScheduleTest frm  = new frmScheduleTest(TestAppointmentID, TestNum, "");
            frm.ShowDialog();

            LoadAllDataToDGV();

            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.ctrlMatchAppLoad();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            int TestNum = ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.TestNum;

            if (clsTestsAppointmentsBusiness.IsTestAppointmentsLockedByAppointmentID(TestAppointmentID, TestNum))
            {
                MessageBox.Show(
                "Person Already passed this test before, You Can only retake failed test",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, TestNum);
            frm.ShowDialog();

            LoadAllDataToDGV();

            ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.ctrlMatchAppLoad();
        }
    }
}
