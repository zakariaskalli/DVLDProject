using Business_Layer___DVLDProject;
using DVLD_BusinessLayer;
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
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAllDataToDGV()
        {


            DataTable dataTable = clsApplicationTypes.GetAllApplicationTypes();
            dataGridView1.DataSource = dataTable;
            TotalRecord.Text = $"# Record: {dataGridView1.RowCount}";

            dataGridView1.Columns[0].Width = 140;  
            dataGridView1.Columns[1].Width = 300; 
            dataGridView1.Columns[2].Width = 140;  


        }


        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            LoadAllDataToDGV();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());


            frmUpdateApplicationType frm = new frmUpdateApplicationType(ID);
            frm.ShowDialog();

            LoadAllDataToDGV();
        }

    }
}
