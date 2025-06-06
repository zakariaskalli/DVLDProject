using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
using DVLDProject.Applications.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            cbSearchBy.Items.Add("DriverID");
            cbSearchBy.Items.Add("PersonID");
            cbSearchBy.Items.Add("NationalNo");
            cbSearchBy.Items.Add("FullName");

            cbSearchBy.SelectedIndex = 0;
        }

        private void LoadAllDataToDGV()
        {
            DataTable dataTable = clsDrivers.GetAllDrivers();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

        }


        private void frmManageDriver_Load(object sender, EventArgs e)
        {
            LoadComboBox();


            LoadAllDataToDGV();

        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
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
            if (cbSearchBy.Text == "Person ID" || cbSearchBy.Text == "Driver ID")
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
            

            DataTable dataTable = clsDrivers.SearchData((clsDrivers.DriversColumn)Enum.Parse(typeof(clsDrivers.DriversColumn), cbSearchBy.Text), tbFilterByData.Text);
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
