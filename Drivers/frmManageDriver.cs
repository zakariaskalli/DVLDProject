using Business_Layer___DVLDProject;
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

namespace DVLDProject
{
    public partial class frmManageDriver : Form
    {
        public frmManageDriver()
        {
            InitializeComponent();
        }

        private void LoadComboBox()
        {
            cbSearchBy.Items.Add("None");
            cbSearchBy.Items.Add("Driver ID");
            cbSearchBy.Items.Add("Person ID");
            cbSearchBy.Items.Add("National No.");
            cbSearchBy.Items.Add("Full Name");

            cbSearchBy.SelectedIndex = 0;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsManageDriversBusiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
            // 915

            dataGridView1.Columns["DriverID"].Width = 110;
            dataGridView1.Columns["PersonID"].Width = 110;
            dataGridView1.Columns["NationalNo"].Width = 120;
            dataGridView1.Columns["FullName"].Width = 245;
            dataGridView1.Columns["Date"].Width = 140;
            dataGridView1.Columns["Active Licenses"].Width = 140;
        }


        private void frmManageDriver_Load(object sender, EventArgs e)
        {
            LoadComboBox();


            LoadAllDataToDGV();

        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSearchBy.Text == "Person ID" || cbSearchBy.Text == "Driver ID")
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
        }

        private string ColumnName()
        {
            switch (cbSearchBy.Text)
            {
                case "Driver ID":
                    return "DriverID";
                case "Person ID":
                    return "PersonID";
                case "National No.":
                    return "NationalNo";
                case "Full Name":
                    return "FullName";
                default:
                    return cbSearchBy.Text;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                LoadAllDataToDGV();
                return;
            }


            DataTable dataTable = clsManageDriversBusiness.SearchInTable(ColumnName(), textBox1.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
