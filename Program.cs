using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using DVLDProject.Global_Classes;
using DVLDProject.Login;


//using MyFirstWinFormsProject;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLDProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //static public string _UserName = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                        
            bool ReloadNewOrNot = false;
            do
            {
                ReloadNewOrNot = false;

                using (frmLogin  Form = new frmLogin())
                {
                    Application.Run(Form);
                }

                if (clsUsers.FindByUserName(clsGlobal.CurrenntUser.UserName) != null)
                {
                    DVLD Form = new DVLD(); // لم نعد بحاجة لإرسال اسم المستخدم والباسوورد
                    Form.NewOrNot += (YesOrNo) => { ReloadNewOrNot = YesOrNo; };
                    Application.Run(Form);
                }

            }
            while (ReloadNewOrNot == true);


            //TestForm Form = new TestForm();
            //Application.Run(Form);

            //frmReleaseDetainedLicense Form = new frmReleaseDetainedLicense();
            //Application.Run(Form);
            


        }
    }

}
