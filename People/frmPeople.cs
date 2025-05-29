using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDProject
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
        }

        private void LoadComboBox()
        {

            cbSearchBy.Items.Clear(); // Clear previous items

            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("PersonID");
            cbSearchBy.Items.Add("NationalNo");
            cbSearchBy.Items.Add("FirstName");
            cbSearchBy.Items.Add("SecondName");
            cbSearchBy.Items.Add("ThirdName");
            cbSearchBy.Items.Add("LastName");
            cbSearchBy.Items.Add("GendorCaption");
            cbSearchBy.Items.Add("DateOfBirth");
            cbSearchBy.Items.Add("CountryName");
            cbSearchBy.Items.Add("Phone");
            cbSearchBy.Items.Add("Email");
            
            /*
            foreach (string column in Enum.GetNames(typeof(clsPeople.PeopleColumn)))
            {
                cbSearchBy.Items.Add(column);
            }
            */

            cbSearchBy.SelectedIndex = 0;
        }

        // Upload Data To Table
        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsPeople.GetAllPeople_DataGridView();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            LoadComboBox();

            // Upload Data To Table
            LoadAllDataToDGV();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchBy.Text != "None")
            {
                tbFilterByData.Visible = true;
                tbFilterByData.Focus();
            }
            else
                tbFilterByData.Visible = false;

            tbFilterByData.Text = "";
            LoadAllDataToDGV();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchBy.Text == "PersonID" || cbSearchBy.Text == "Phone")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterByData.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }

            DataTable dataTable = clsPeople.SearchData_DataGridView(cbSearchBy.Text, tbFilterByData.Text);

            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            LoadAllDataToDGV();

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            LoadAllDataToDGV();

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());


            if (clsPeople.FindByPersonID(PersonID) != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(PersonID);
                frm.ShowDialog();
                LoadAllDataToDGV();
            }
            else
            {
                MessageBox.Show("Sorry, PeronID Is not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadAllDataToDGV();

            }

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            frmPersonDetails Frm = new frmPersonDetails(PersonID);
            
            Frm.ShowDialog();

        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[0].Value;





            if (clsPeople.FindByPersonID(PersonID) != null)
            {
                string TextView = $"Are you sure you want to delete Person [{PersonID}]";

                if (MessageBox.Show(TextView, "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (clsPeople.DeletePeople(PersonID))
                        MessageBox.Show("Person Delete Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show($"Person [{PersonID}] Is Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cbSearchBy.SelectedIndex = 0;
            tbFilterByData.Text = "";

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

    }
}
