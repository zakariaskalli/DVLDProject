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
using System.Xml.Linq;

namespace DVLDProject
{
    public partial class ctrlFilterAndMakePersonInfo : UserControl
    {
        public int? _PersonID = null;
        public string _NationalNo = "";

        public ctrlFilterAndMakePersonInfo()
        {
            InitializeComponent();
        }

        public string GetFilterByName()
        {
            return ctrlFilterBy1.GetFilterByName();
        }

        public void DisabledFilterBy()
        {
            ctrlFilterBy1.Enabled = false;
        }

        public string GetData()
        {
            return ctrlFilterBy1.GetData();
        }

        public void SelectCombobox(int NumCb)
        {
            ctrlFilterBy1.SelectCombobox(NumCb);
        }

        public void textBoxData(string txtbData)
        {
            ctrlFilterBy1.textBoxData(txtbData);
        }


        public void ctrlShowPersonDetails_Load()
        {

            ctrlShowPersonDetails1._PersonID = _PersonID;
            ctrlShowPersonDetails1._NationalNo = _NationalNo;
            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();

        }





        private void ctrlFilterBy1_OnFilterBtn(string arg1, string arg2)
        {
            
            //DataTable dataTable = clsPeople.SearchData((clsPeople.PeopleColumn)Enum.Parse(typeof(clsPeople.PeopleColumn), arg1), arg2);


            if (clsPeople.SearchData((clsPeople.PeopleColumn)Enum.Parse(typeof(clsPeople.PeopleColumn), arg1), arg2).Rows.Count > 0)
            {
                _NationalNo = "";
                _PersonID = -1;

                ctrlShowPersonDetails1._NationalNo = "";
                ctrlShowPersonDetails1._PersonID = -1;

                if (arg1 == "NationalNo")
                {
                    ctrlShowPersonDetails1._NationalNo = arg2;
                    _NationalNo = arg2;
                }
                else if (arg1 == "PersonID")
                {
                    ctrlShowPersonDetails1._PersonID = int.Parse(arg2);
                    _PersonID = int.Parse(arg2);
                }



                ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();
            }
            else if (arg1 == "NationalNo" && arg2 != "")
                MessageBox.Show("No Person With National No = " + arg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (arg1 == "PersonID" && arg2 != "")
                MessageBox.Show("No Person With Person ID = " + arg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("No Person With This Value Enter Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ctrlFilterBy1_OnNewPeronAdd(int? obj)
        {

            ctrlShowPersonDetails1._PersonID = obj;

            ctrlShowPersonDetails1.ctrlShowPersonDetails_Load();

            _PersonID = obj;
        }

        private void ctrlFilterBy1_Load(object sender, EventArgs e)
        {

        }

        public void SelectedTextBox()
        {
            ctrlFilterBy1.SelectedTextBox();
        }

    }
}
