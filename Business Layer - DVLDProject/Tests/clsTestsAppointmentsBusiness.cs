using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Data_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Business_Layer___DVLDProject
{
    public class clsTestsAppointmentsBusiness
    {
        //enum enMode { UpdateMode = 0, AddNewMode = 1 }

        //enMode _Mode;

        public clsTestsAppointmentsBusiness(int LDLAppID, int NumTestSchedule)
        {
            this.LDLAppID = LDLAppID;
            this.TestNum = NumTestSchedule;
            this.DClass = "";
            this.Name = "";
            this.Trial = 0;
            this.Date = DateTime.Now;
            this.Fees = 0;

            //_Mode = enMode.UpdateMode;
        }

        public clsTestsAppointmentsBusiness(int TestAppointmentID, int NumTestSchedule, string Default = "")
        {
            this.TestAppointmentID = TestAppointmentID;
            this.LDLAppID = -1;
            this.TestNum = NumTestSchedule;
            this.DClass = "";
            this.Name = "";
            this.Trial = 0;
            this.Date = DateTime.Now;
            this.Fees = 0;

            //_Mode = enMode.UpdateMode;
        }

        public int TestAppointmentID { get; set; }

        public int LDLAppID { get; set; }
        public string DClass { get; set; }
        public string Name { get; set; }
        public int Trial { get; set; }
        public DateTime Date { get; set; }
        public int Fees { get; set; }
        public int TestNum { get; set; }

        
        static public DataTable LoadTestAppointmentByLDLAppID(int TestNum,int LDLAppID)
        {

            return clsTestsAppointmentsData.LoadTestsAppointmentByLDLAppID(TestNum, LDLAppID);
        }


        public void LoadDataForScheduleByLDLAppIDAndTestNum()
        {
            //clsTestsAppointmentsBusiness clsData = new clsTestsAppointmentsBusiness(LDLAppID, TestNum);

            string DClass = "";
            string Name = "";
            int Trial = 0;
            int Fees = 0;

            clsTestsAppointmentsData.LoadDataForScheduleByLDLAppIDAndTestNum(TestNum, LDLAppID, ref DClass, ref Name, ref Trial, ref Fees);

            this.LDLAppID = LDLAppID;
            this.TestNum = TestNum;
            this.DClass = DClass;
            this.Name = Name;
            this.Trial = Trial;
            this.Fees = Fees;

            //return clsData;
        }

        public int LoadUpdateDataForScheduleByAppointmentIDAndTestNum()
        {

            string DClass = "";
            int LDLAppID = -1;
            string Name = "";
            int Trial = 0;
            int Fees = 0;
            DateTime Date = DateTime.Now;

            TestAppointmentID = clsTestsAppointmentsData.LoadUpdateDataForScheduleByAppointmentIDAndTestNum(TestNum, TestAppointmentID, ref LDLAppID, ref DClass, ref Name, ref Trial, ref Fees, ref Date);

            this.TestNum = TestNum;
            this.TestAppointmentID = TestAppointmentID;
            this.LDLAppID = LDLAppID;
            this.DClass = DClass;
            this.Name = Name;
            this.Trial = Trial;
            this.Fees = Fees;
            this.Date = Date;


            return TestAppointmentID;
        }


        public bool AddDataToTestAppointments(DateTime date, string UserName)
        {
            bool IsUpdate = clsTestsAppointmentsData.AddDataToTestAppointments(this.LDLAppID, TestNum, date,
                                                                                    UserName);
            //_Mode = enMode.UpdateMode;

            return IsUpdate;

        }

        public bool UpdateDateToTestAppointments(DateTime date)
        {
            return clsTestsAppointmentsData.UpdateDateToTestAppointments(LDLAppID, TestNum, date);
        }

        public bool IsTestAppointmentsLockedByLDLAppID()
        {
            return clsTestsAppointmentsData.IsTestAppointmentsLockedByLDLAppID(LDLAppID, TestNum);
        }


        public bool IsTestAppointmentsLockedByAppointmentID()
        {
            return clsTestsAppointmentsData.IsTestAppointmentsLockedByAppointmentID(TestAppointmentID, TestNum);
        }

        static public bool IsTestAppointmentsLockedByAppointmentID(int TestAppointmentID, int TestNum)
        {
            return clsTestsAppointmentsData.IsTestAppointmentsLockedByAppointmentID(TestAppointmentID, TestNum);
        }


        public bool IHaveAnAppointmentByLDLAppIDAndTestNum()
        {
            return clsTestsAppointmentsData.IHaveAnAppointmentByLDLAppIDAndTestNum(LDLAppID, TestNum);
        }

        public bool IsTestAppointmentsFinished()
        {
            return clsTestsAppointmentsData.IsTestAppointmentsFinished(LDLAppID, TestNum);
        }

        public bool CanIUpdateTestAppointmentsByLDLAppID()
        {
            return clsTestsAppointmentsData.CanIUpdateTestAppointmentsByLDLAppID(LDLAppID, TestNum);
        }


        public bool CanIUpdateTestAppointmentsByTestAppID()
        {
            return clsTestsAppointmentsData.CanIUpdateTestAppointmentsByTestAppID(TestAppointmentID,TestNum);
        }

        static public bool CanIUpdateTestAppointmentsByTestAppID(int TestAppointmentID, int TestNum)
        {
            return clsTestsAppointmentsData.CanIUpdateTestAppointmentsByTestAppID(TestAppointmentID, TestNum);
        }


    }
}
