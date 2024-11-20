using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmScheduleTest : Form
    {
        string UserName = Program._UserName;

        int TestAppointmentID = -1;


        clsTestsAppointmentsBusiness clsA = null;

        public frmScheduleTest(int lDLAppID, int testNum)
        {
            InitializeComponent();

            clsA = new clsTestsAppointmentsBusiness(lDLAppID, testNum);
        }

        public frmScheduleTest(int TestAppointmentID, int NumTestSchedule, string Default = "")
        {
            InitializeComponent();


            clsA = new clsTestsAppointmentsBusiness(TestAppointmentID, NumTestSchedule, Default);
        }

        private void LoadAllData()
        {
            if (clsMethodsGeneralBusiness.IsLDLAppIDFound(clsA.LDLAppID) 
                ||
                clsA.CanIUpdateTestAppointmentsByTestAppID() 
                ||
                clsA.IsTestAppointmentsLockedByAppointmentID())
            {

                if (clsA.TestNum == 1)
                {
                    pictureBox1.ImageLocation = @"C:\Programation Level 2\DVLDProject\Project Image\eye.png";
                    gbScheduleTest.Text = "Vision Test";
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (clsA.TestNum== 2)
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


                if ((clsA.TestAppointmentID != -1 && clsA.CanIUpdateTestAppointmentsByTestAppID())
                    ||
                    clsA.IsTestAppointmentsLockedByAppointmentID())
                {
                    TestAppointmentID = clsA.LoadUpdateDataForScheduleByAppointmentIDAndTestNum();
                    DTP1.Value = clsA.Date;
                    if (DTP1.Value > DateTime.Now)
                    {
                        DTP1.MinDate = DateTime.Now;
                    }
                    else
                    {
                        DTP1.MinDate = clsA.Date;
                    }
                }
                else
                {
                    clsA.LoadDataForScheduleByLDLAppIDAndTestNum();
                    DTP1.MinDate = DateTime.Now;
                }



                lblDLAppID.Text = clsA.LDLAppID.ToString();
                lblDClass.Text = clsA.DClass.ToString();
                lblName.Text = clsA.Name.ToString();

                lblTrial.Text = clsA.Trial.ToString();
                lblTotalFees.Text = clsA.Fees.ToString();
                lblFees.Text = clsA.Fees.ToString();


                // Have Any Test Last

                if (clsA.Trial > 0)
                {
                    gbRetakeTestInfo.Enabled = true;
                    lblRAppFees.Text = "5";
                    lblTotalFees.Text = Convert.ToString(Convert.ToInt16(lblFees.Text) + 5);

                    if (!clsA.IsTestAppointmentsLockedByAppointmentID())
                    {
                        lblRTestAppID.Text = TestAppointmentID.ToString();
                    }
                }




            }
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            
            LoadAllData();
            //DTP1.MinDate = DateTime.Now;
            // IsTestAppointmentsLocked
            if (clsA.IsTestAppointmentsLockedByAppointmentID())
            {
                lblSecheduleName.Text = "Schedule Retake Test";
                lblSecheduleName.Location =new Point(130, 135);
                lblCanNotUpdate.Text = "Person already sat for the test, Appointment locked";

                DTP1.Enabled = false;
                btnSave.Enabled = false;
                // Continue Disabled Save And Date
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Test IsTestAppointmentLocked
            if ((!clsA.IHaveAnAppointmentByLDLAppIDAndTestNum())
                ||
                (clsA.IsTestAppointmentsLockedByLDLAppID() && !clsA.IsTestAppointmentsFinished()))
            {
                DateTime Date = DTP1.Value;


                if (clsA.AddDataToTestAppointments(Date, UserName))
                {
                    MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("We Can't Saved Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            else if (clsA.CanIUpdateTestAppointmentsByLDLAppID())
            {
            // Add Text "this appointment are fnished you con't edit date" if i load a form
                DateTime Date = DTP1.Value;


                if (clsA.UpdateDateToTestAppointments(Date))
                {
                    MessageBox.Show("Data Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("We Can't Update Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            else
            {
                MessageBox.Show("We Can't Saved Or Update Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
        }
    }
}
