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

            foreach (string column in Enum.GetNames(typeof(clsPeople.PeopleColumn)))
            {
                cbSearchBy.Items.Add(column);
            }

            cbSearchBy.SelectedIndex = 0;
        }

        // Upload Data To Table
        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsPeople.GetAllPeople();
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
                textBox1.Visible = true;
            else
                textBox1.Visible = false;

            textBox1.Text = "";
            LoadAllDataToDGV();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbSearchBy.Text == "Person ID" || cbSearchBy.Text == "Phone")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }

            DataTable dataTable = clsPeople.SearchData((clsPeople.PeopleColumn)Enum.Parse(typeof(clsPeople.PeopleColumn), cbSearchBy.Text), textBox1.Text);

            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            LoadAllDataToDGV();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            LoadAllDataToDGV();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());


            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


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

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


            frmPersonDetails Frm = new frmPersonDetails(PersonID);
            
            Frm.ShowDialog();
            LoadAllDataToDGV();

        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[0].Value;


            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


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

            LoadAllDataToDGV();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {

            cbSearchBy.SelectedIndex = 0;
            textBox1.Text = "";


            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
