using Business_Layer___DVLDProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static DVLDProject.ctrlFilterDriverLicenseInfo;
using static DVLDProject.frmNewInternationalLicenseApplication;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DVLD_BusinessLayer;

namespace DVLDProject
{
    public partial class ctrlFilterDriverLicenseInfo : UserControl
    {
        public int _LicenseID = -1;
        public int _InternationalID = -1;
        public bool _IsActive = false;
        //public bool _IsPastExpirationDate = false;

        public event Action<int, bool> _OnClickToSearch;

        protected virtual void ClickToSave(int LicenseID, bool IsActive)
        {
            _OnClickToSearch?.Invoke(LicenseID, IsActive);
        }


        public enum enChose { enNewInternationalLicenseLicense = 1, enRenewLocalDrivingLicense = 2,
                                enReplacementForLostLicense = 3, enReplacementForDamagedLicense = 4,
                                enDetain = 5, enReleaseDetainedLicense = 6}
        public enChose _enChose;

        public ctrlFilterDriverLicenseInfo()
        {
            InitializeComponent();
            clsMethodsGeneralBusiness.UpdateDataFormAllLicenses();

        }

        public void ChoseTypeFilter(int Chose)
        {
            _enChose = (enChose)Chose;

        }

        private void tbLicenseIDSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        void SearchDataLicense()
        {
            if (tbLicenseIDSearch.Text == "" && !(clsLicenses.FindByLicenseID((int)_LicenseID) != null))
            {
                return;
            }

            int LicenseID = -1;

            if (clsLicenses.FindByLicenseID((int)_LicenseID) != null)
                LicenseID = _LicenseID;
            else
                LicenseID = Convert.ToInt16(tbLicenseIDSearch.Text);


            if (clsLicenses.FindByLicenseID((int)LicenseID) != null)
            {
                if (_enChose == enChose.enNewInternationalLicenseLicense)
                {

                    if (clsFilterDriverInfoBusiness.CanIAddInternationalLicenseByLicenseID(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = true;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, true);
                    }
                    else if (clsFilterDriverInfoBusiness.IHaveInternationalLicenseByLicenseID(LicenseID) != -1)
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;

                        _InternationalID = clsFilterDriverInfoBusiness.IHaveInternationalLicenseByLicenseID(LicenseID);

                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        int InternationalID = clsFilterDriverInfoBusiness.IHaveInternationalLicenseByLicenseID(LicenseID);

                        //_IsActive = false;
                        //_LicenseID = LicenseID;


                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);


                        MessageBox.Show($"Person already, have International License By ID ={InternationalID}", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);




                    }
                    else if (!clsFilterDriverInfoBusiness.IsAllLicensesActiveByLicenseID(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        MessageBox.Show($"Person Have A License But Is Not Active", "Not Actived", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                    else if (clsFilterDriverInfoBusiness.IsLicensePastExpirationDate(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        MessageBox.Show($"Person ExpirationDate Finale he Want to Renew", "Final Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (_enChose == enChose.enRenewLocalDrivingLicense)
                {
                    if (!clsFilterDriverInfoBusiness.IsAllLicensesActiveByLicenseID(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, true);

                    }
                    else
                    {
                        ctrlLicenseInfo1._LicenseID =  LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        string ExpirationDate = clsFilterDriverInfoBusiness.ExpirationDateLicenseByLicenseID(LicenseID);

                        MessageBox.Show($"Selected License is not yet expired, it will expire en {ExpirationDate} ",
                                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
                else if (_enChose == enChose.enReplacementForLostLicense)
                {
                    if (clsFilterDriverInfoBusiness.IsJustLicenseActiveByLicenseID(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, true);

                    }
                    else
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        MessageBox.Show($"Selected License is not Active",
                                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_enChose == enChose.enReplacementForDamagedLicense)
                {

                    if (clsFilterDriverInfoBusiness.IsJustLicenseActiveByLicenseID(LicenseID))
                    {

                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, true);

                    }
                    else
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        if (ctrlLicenseInfo1._LicenseID == -1)
                        {
                            MessageBox.Show($"Selected License is not Active and Isn't have a data relied",
                                            "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Selected License is not Active",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (_enChose == enChose.enDetain)
                {
                    if (!clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID)
                        &&
                        clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID))
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, true);

                    }
                    else
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        if (clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID)
                            && !clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID))
                        {

                            MessageBox.Show($"This License Doesn't have any Data and Not have any data",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if(clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID))
                        {
                            MessageBox.Show($"Selected License i already Detained, Chose Other one.",
                                            "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID))
                        {
                            MessageBox.Show($"This License Doesn't have any Data",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                         
                    }
                }
                else if (_enChose == enChose.enReleaseDetainedLicense)
                {
                    if (clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID)
                        &&
                        clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID)
                        )
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (!clsMethodsGeneralBusiness.IsDetainedCompleteByLicenseID(LicenseID))
                        {
                            if (_OnClickToSearch != null)
                                ClickToSave(LicenseID, true);
                        }
                        else
                        {
                            if (_OnClickToSearch != null)
                                ClickToSave(LicenseID, false);
                        }

                    }
                    else
                    {
                        ctrlLicenseInfo1._LicenseID = LicenseID;
                        ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();
                        ctrlLicenseInfo1.ctrlLicenseInfo_Load();

                        //_IsActive = false;
                        //_LicenseID = LicenseID;

                        if (_OnClickToSearch != null)
                            ClickToSave(LicenseID, false);

                        if (clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID)
                            && !clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID))
                        {

                            MessageBox.Show($"This License Doesn't have any Data and Not have any data",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!clsMethodsGeneralBusiness.IsLicenseDetained(LicenseID))
                        {
                            MessageBox.Show($"Selected License i Not Detained, Chose Other one.",
                                            "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!clsMethodsGeneralBusiness.IsHaveDataInLicense(LicenseID))
                        {
                            MessageBox.Show($"This License Doesn't have any Data",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            else
            {
                ctrlLicenseInfo1.ctrlLicenseInfo_LoadAutoData();

                if (_OnClickToSearch != null)
                    ClickToSave(-1, false);

                MessageBox.Show("This LicenseID Not Found, Enter Correct LicenseID", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbLicenseIDSearch.Focus();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDataLicense();

        }


        private void tbLicenseIDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchDataLicense();
            }
        }



        public void FocusTextBox()
        {
            tbLicenseIDSearch.Focus();
        }

        private void ctrlFilterDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        public void LicenseIDLoadData(int LicenseID)
        {
            _LicenseID = LicenseID;
            tbLicenseIDSearch.Text = _LicenseID.ToString();
            SearchDataLicense();
        }

        public void FilterDisabled()
        {
            gbFilter.Enabled = false;
        }
    }
}
