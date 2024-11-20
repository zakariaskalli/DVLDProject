using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;

namespace Business_Layer___DVLDProject
{
    public class clsTakeTestBusiness
    {
        //enum enMode { UpdateMode = 0, AddNewMode = 1 }

        //enMode _Mode;

        public clsTakeTestBusiness(int LDLAppID, int NumTestSchedule)
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

        public clsTakeTestBusiness(int TestAppointmentID, int NumTestSchedule, string Default = "")
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

        public int LoadUpdateDataForScheduleByAppointmentIDAndTestNum()
        {

            string DClass = "";
            int LDLAppID = -1;
            string Name = "";
            int Trial = 0;
            int Fees = 0;
            DateTime Date = DateTime.Now;

            clsTakeTestData.LoadDataForTakeTestByAppointmentIDAndTestNum(TestNum, TestAppointmentID, ref LDLAppID, ref DClass, ref Name, ref Trial, ref Fees, ref Date);

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

        public bool DataPassOrFailByLDLAppIDAndTestNum(bool PassOrFail, int LDLAppID, int TestNum, string CreatedBy, string Notes)
        {
            return clsTakeTestData.DataPassOrFailByLDLAppIDAndTestNum(PassOrFail, LDLAppID, TestNum, CreatedBy, Notes);
        }

        public bool DataPassOrFailByAppointmentIDAndTestNum(bool PassOrFail, int AppointmentID, int TestNum, string CreatedBy, string Notes)
        {
            return clsTakeTestData.DataPassOrFailByAppointmentIDAndTestNum(PassOrFail, AppointmentID, TestNum, CreatedBy, Notes);
        }
    }
}
