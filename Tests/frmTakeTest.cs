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
using System.Globalization;
using DVLD_BusinessLayer;
using DVLDProject.Global_Classes;

namespace DVLDProject
{
    public partial class frmTakeTest : Form
    {
        
        int TestAppointmentID = -1;

        clsTakeTestBusiness clsA = null;

        public frmTakeTest(int TestAppointmentID, int NumTestSchedule)
        {
            InitializeComponent();

            this.TestAppointmentID = TestAppointmentID;
            clsA = new clsTakeTestBusiness(TestAppointmentID, NumTestSchedule, "");
        }

        


        public void LoadData()
        {
            if (clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID((int)clsA.LDLAppID) != null
                ||
                clsTestsAppointmentsBusiness.CanIUpdateTestAppointmentsByTestAppID(clsA.TestAppointmentID, clsA.TestNum))
            {

                if (clsA.TestNum == 1)
                {
                    pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\eye.png";
                    gbScheduleTest.Text = "Vision Test";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (clsA.TestNum == 2)
                {
                    pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\test.png";
                    gbScheduleTest.Text = "Written Test";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (clsA.TestNum == 3)
                {
                    pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\StreetTest.png";
                    gbScheduleTest.Text = "Street Test";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                }

                clsA.LoadUpdateDataForScheduleByAppointmentIDAndTestNum();

                lblDLAppID.Text = clsA.LDLAppID.ToString();
                lblDClass.Text = clsA.DClass.ToString();
                lblName.Text = clsA.Name.ToString();

                lblTrial.Text = clsA.Trial.ToString();

                //Date on dd/mm/yyyy

                lblDate.Text = clsA.Date.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture).ToLower();
                lblFees.Text = clsA.Fees.ToString();

            }
        }


        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool PassOrFail = false;

            if (rbPass.Checked == true)
                PassOrFail = true;
            else
                PassOrFail = false;

            string Notes = tbNote.Text;


            if (clsA.DataPassOrFailByAppointmentIDAndTestNum(PassOrFail, clsA.TestAppointmentID, clsA.TestNum, clsGlobal.CurrenntUser.UserName, Notes))
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("We Can't Saved Or Update Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
