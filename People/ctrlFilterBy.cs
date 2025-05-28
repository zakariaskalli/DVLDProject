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
    public partial class ctrlFilterBy : UserControl
    {
        //int _PersonID = -1;

        public event Action<string, string> OnFilterBtn;

        protected virtual void FilterBtn(string cbSearchName, string Data)
        {
            Action<string, string> handler = OnFilterBtn;

            if (handler != null)
                handler(cbSearchName, Data);
        }

        public string GetFilterByName()
        {
            return cbSearchBy.Text;
        }

        public string GetData()
        {
            return tbData.Text;
        }


        public event Action<int?> OnNewPeronAdd;
        protected virtual void NewPeronAdd(int? PersonID)
        {
            Action<int?> handler = OnNewPeronAdd;

            if (handler != null)
                handler(PersonID);
        }

        public ctrlFilterBy()
        {
            InitializeComponent();
        }

        public void SelectCombobox(int NumCb)
        {
            cbSearchBy.SelectedIndex = NumCb;
        }

        public void textBoxData(string txtbData)
        {
            tbData.Text = txtbData;

            //cbSearchBy.SelectedIndex = NumCb;
            
        }


        private void LoadComboBox()
        {
            cbSearchBy.Items.Add("NationalNo");
            cbSearchBy.Items.Add("PersonID");

            cbSearchBy.SelectedIndex = 0;

        }

        private void ctrlFilterBy_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        //Delete


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnFilterBtn != null)
                FilterBtn(cbSearchBy.Text, tbData.Text);
        
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

            frmAddEditPersonInfo Form = new frmAddEditPersonInfo();
            Form.ShowDialog();

            int? PersonID = Form.GetPersonID();

            if (PersonID != null)
            {
                if (OnNewPeronAdd != null)
                    NewPeronAdd(PersonID);
                cbSearchBy.SelectedIndex = 1;
                tbData.Text = PersonID.ToString();
            }


            //int i = Form.GetPersonID();


        }

        private void tbData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter key
            {
                btnSearch.PerformClick();
            }
            
        }

        public void SelectedTextBox()
        {
            tbData.Focus();
        }
    }
}
