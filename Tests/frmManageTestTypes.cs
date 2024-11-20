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
            DataTable dataTable = clsManageTestTypesBusiness.LoadData();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";
            // with = 715
            dataGridView1.Columns["ID"].Width = 80;
            dataGridView1.Columns["Title"].Width = 160;
            dataGridView1.Columns["Description"].Width = 350;
            dataGridView1.Columns["Fees"].Width = 80;

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
