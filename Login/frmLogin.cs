using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer___DVLDProject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DVLD_BusinessLayer;
using System.Web;
using DVLDProject.Global_Classes;



namespace DVLDProject.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(clsGlobal.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            string _UserName = "";
            string _Password = "";


            if (clsRegistryUtility.LoadUserNameAndPasswordRememberMe(ref _UserName, ref _Password))
            {
                tbUserName.Text = tbUserName.Text.Trim();
                tbUserName.Text = _UserName;

                tbPassword.Text = tbPassword.Text.Trim();
                tbPassword.Text = _Password;
            }



            chbRememberMe.Checked = true;

            btnLogin.Select();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbUserName_Validating_1(object sender, CancelEventArgs e)
        {

            if (tbUserName.Text == "")
                errorProvider1.SetError(tbUserName, "Enter UserName");
            else
                errorProvider1.SetError(tbUserName, "");
        }

        private void tbPassword_Validating_1(object sender, CancelEventArgs e)
        {

            if (tbPassword.Text == "")
                errorProvider1.SetError(tbPassword, "Enter Password");
            else
                errorProvider1.SetError(tbPassword, "");
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {

            if (tbUserName.Text == "" || tbPassword.Text == "")
                return;

            this.Cursor = Cursors.WaitCursor; // Set to WaitCursor

            try
            {
                string UserName = tbUserName.Text;
                string Password = tbPassword.Text;

                bool IsValid = false;

                string Message = clsUsers.ValidateUser(UserName, Password, ref IsValid);

                if (IsValid)
                {
                    if (clsUsers.IsAccountActive(UserName))
                    {
                        clsGlobal.CurrenntUser = clsUsers.FindByUserName(UserName);

                        clsRegistryUtility.RememberMe(chbRememberMe.Checked, UserName, Password);

                        this.Close();
                    }
                    else
                        MessageBox.Show("This Account Is Not Active, Contact Your Admin", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                    MessageBox.Show(Message, "Wrong Cardinality", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow; // Always reset to Arrow
            }
        }
    }
}
