using Business_Layer_DVLDProject_clsPeople;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer___DVLDProject;
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
            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("User ID");
            cbSearchBy.Items.Add("User Name");
            cbSearchBy.Items.Add("Person ID");
            cbSearchBy.Items.Add("Full Name");
            cbSearchBy.Items.Add("Is Active");

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
            DataTable dataTable = clsManageUsersBussiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

            dataGridView1.Columns["UserID"].Width = 130;
            dataGridView1.Columns["PersonID"].Width = 130;
            dataGridView1.Columns["FullName"].Width = 390;
            dataGridView1.Columns["UserName"].Width = 130;
            dataGridView1.Columns["IsActive"].Width = 130;
            
        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilterBy();
            LoadComboBoxIsActive();

            LoadAllDataToDGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            frmUserInfo frm = new frmUserInfo(UserID);
            frm.ShowDialog();
            LoadAllDataToDGV();




        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            frmAddNewEditUser Form = new frmAddNewEditUser();
            Form.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            if (clsAddNewEditUserBusiness.UserIDIsFound(UserID))
            {
                frmAddNewEditUser Frm = new frmAddNewEditUser(UserID);
                Frm.ShowDialog();

                LoadAllDataToDGV();
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

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            if (clsManageUsersBussiness.UserIDIsFound(UserID))
            {
                string TextView = $"Are you sure you want to delete User [{UserID}]";

                if (MessageBox.Show(TextView, "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsManageUsersBussiness.DeleteUserByID(UserID))
                        MessageBox.Show("User Delete Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show($"Person [{UserID}] Is Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LoadAllDataToDGV();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

            frmChangePassword Frm = new frmChangePassword(UserID);
            Frm.ShowDialog();
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch(cbSearchBy.Text)
            {
                case "None":
                    tbFilterByData.Visible = false;
                    cbIsActive.Visible = false;
                    break;
                case "User ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "User Name":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Person ID":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Full Name":
                    tbFilterByData.Visible = true;
                    cbIsActive.Visible = false;
                    break;
                case "Is Active":
                    tbFilterByData.Visible = false;
                    cbIsActive.Visible = true;
                    cbIsActive.Location = new Point(230, cbIsActive.Location.Y);
                    break;
                default:
                    break;
            }

            
        }

        private void tbFilterByData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchBy.Text == "Person ID" || cbSearchBy.Text == "User ID")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }

        private string ColumnName()
        {
            switch (cbSearchBy.Text)
            {
                case "User ID":
                    return "UserID";
                case "User Name":
                    return "FullName";
                case "Person ID":
                    return "PersonID";
                case "Full Name":
                    return "FullName";
                default:
                    return cbSearchBy.Text;
            }

        }

        private void tbFilterByData_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsManageUsersBussiness.SearchInTable(ColumnName(), tbFilterByData.Text);
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


            DataTable dataTable = clsManageUsersBussiness.SearchDataIsActive(YesNo);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }
    }
}
