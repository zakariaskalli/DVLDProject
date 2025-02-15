using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;


namespace DVLDProject
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void LoadAllDataToDGV()
        {

            DataTable dataTable = clsTestTypes.GetAllTestTypes();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
            // with = 845
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 400;
            dataGridView1.Columns[3].Width = 100;

        }


        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            LoadAllDataToDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int ID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            frmUpdateTestType frm = new frmUpdateTestType(ID);
            frm.ShowDialog();

            LoadAllDataToDGV();

        }
    }
}
