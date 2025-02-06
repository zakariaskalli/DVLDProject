using Business_Layer___DVLDProject;
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

        static public string _UserName = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string _Password = null;
            
            bool ReloadNewOrNot = false;
            do
            {
                ReloadNewOrNot = false;
            
                using (frmLoginScreen Form = new frmLoginScreen())
                {
                    Form.DataBack += (UserName, Password) => { _UserName = UserName; _Password = Password; };
                    Application.Run(Form);
            
                }
            
                if (!string.IsNullOrEmpty(_UserName) && !string.IsNullOrEmpty(_Password))
                {
                    DVLD Form = new DVLD(_UserName, _Password);
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
