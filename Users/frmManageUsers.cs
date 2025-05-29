
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLDProject
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void LoadComboBoxFilterBy()
        {
            cbSearchBy.Items.Clear();

            cbSearchBy.Items.Add("None");

            foreach (string column in Enum.GetNames(typeof(clsUsers.UsersColumn)))
            {
                cbSearchBy.Items.Add(column);
            }

            cbSearchBy.SelectedIndex = 0;
        }

        private void LoadComboBoxIsActive()
        {
            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("Yes");
            cbIsActive.Items.Add("No");

            cbIsActive.SelectedIndex = 0;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsUsers.GetAllUsers();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

            //dataGridView1.Columns["UserID"].Width = 130;
            //dataGridView1.Columns["PersonID"].Width = 130;
            //dataGridView1.Columns["FullName"].Width = 390;
            //dataGridView1.Columns["UserName"].Width = 130;
            //dataGridView1.Columns["IsActive"].Width = 130;
            
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilterBy();
            LoadComboBoxIsActive();

            LoadAllDataToDGV();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            frmAddNewEditUser Form = new frmAddNewEditUser();
            Form.ShowDialog();

            LoadAllDataToDGV();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmUserInfo frm = new frmUserInfo(UserID);
            frm.ShowDialog();



        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewEditUser Form = new frmAddNewEditUser();
            Form.ShowDialog();

            LoadAllDataToDGV();
            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            //cbSearchBy.SelectedIndex = 0;
            //tbFilterByData.Text = "";

            if (clsUsers.FindByUserID(UserID) != null)
            {
                frmAddNewEditUser Frm = new frmAddNewEditUser(UserID);
                Frm.ShowDialog();

                //LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("Sorry, UserID Is not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadAllDataToDGV();

            }

        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            
            //clsManageUsersBussiness.UserIDIsFound(UserID);

            if (clsUsers.FindByUserID(UserID) != null)
            {
                string TextView = $"Are you sure you want to delete User [{UserID}]";

                if (MessageBox.Show(TextView, "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsUsers.DeleteUsers(UserID))
                        MessageBox.Show("User Delete Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show($"Person [{UserID}] Is Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
            LoadAllDataToDGV();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            frmChangePassword Frm = new frmChangePassword(UserID);
            Frm.ShowDialog();


            LoadAllDataToDGV();

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide both controls by default
            tbFilterByData.Visible = false;
            cbIsActive.Visible = false;

            if (cbSearchBy.Text != "None")
            {
                if (Enum.TryParse(cbSearchBy.Text, out clsUsers.UsersColumn selectedColumn))
                {
                    switch (selectedColumn)
                    {
                        case clsUsers.UsersColumn.UserID:
                        case clsUsers.UsersColumn.PersonID:
                        case clsUsers.UsersColumn.UserName:
                        case clsUsers.UsersColumn.FullName:
                            tbFilterByData.Visible = true;
                            tbFilterByData.Text = "";
                            tbFilterByData.Focus();
                            LoadAllDataToDGV();
                            break;

                        case clsUsers.UsersColumn.IsActive:
                            cbIsActive.Visible = true;
                            cbIsActive.Location = new Point(230, cbIsActive.Location.Y);
                            LoadAllDataToDGV();
                            break;

                        default:
                            // Log or handle unexpected values
                            MessageBox.Show("Unexpected search column selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    // Handle invalid enum values
                    MessageBox.Show("Invalid search column selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




            
        
        private void tbFilterByData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchBy.Text == "PersonID" || cbSearchBy.Text == "UserID")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }


        private void tbFilterByData_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }

            DataTable dataTable = clsUsers.SearchData((clsUsers.UsersColumn)Enum.Parse(typeof(clsUsers.UsersColumn), cbSearchBy.Text), tbFilterByData.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbIsActive.Text == "All")
            {
                LoadAllDataToDGV();
                return;
            }

            int YesNo;

            if (cbIsActive.Text == "No")
                YesNo = 0;
            else
                YesNo = 1;


            DataTable dataTable = clsUsers.SearchData(clsUsers.UsersColumn.IsActive, YesNo.ToString());
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }
    }
}
