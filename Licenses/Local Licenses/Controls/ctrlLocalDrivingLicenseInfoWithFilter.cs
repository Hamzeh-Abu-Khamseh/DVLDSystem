using PeopleBusinessLayer;
using System;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlLocalDrivingLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null)
            {
                handler(LicenseID);
            }

        }
        
        public ctrlLocalDrivingLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set 
            {
                _FilterEnabled = value;
                groupBox2.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

       
        public clsManageLicenses License
        {
            get { return ctrlDriverLicenseInfo1.License; }
        }

        public void LoadData(int LicenseID)
        {
            txtFilter.Text= LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadData(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(_LicenseID);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())

            {
                MessageBox.Show("Please enter the License ID.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtFilter.Focus();
                return;
            }
            _LicenseID = Convert.ToInt32(txtFilter.Text);
            LoadData(_LicenseID);

        }
        public void txtLicenseIDFocus()
        {
            txtFilter.Focus();  
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            
            
        }

        private void txtFilter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Please enter the License ID");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilter, null);
            }
        }
    }
}
