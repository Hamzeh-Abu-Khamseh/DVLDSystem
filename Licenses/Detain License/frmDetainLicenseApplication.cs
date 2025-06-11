using PeopleBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace DVLDSystem
{
    public partial class frmDetainLicenseApplication : Form
    {
      
        private int _DetainID = -1;
        private int _LicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                MessageBox.Show("Please enter the fine fees.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            _DetainID = ctrlLocalDrivingLicenseInfoWithFilter1.License.DetainLicense(Convert.ToSingle(txtFineFees.Text), clsGlobalSettings.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Error in detaining License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }

        

       

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverInfo.PersonID);
            frm.ShowDialog();

        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm =new frmShowLicenseInfo(ctrlLocalDrivingLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLocalDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID=obj;
            lblLicenseID.Text = _LicenseID.ToString();

            lnkShowLicenseHistory.Enabled = _LicenseID != -1;

            if(_LicenseID==-1)
            {
                return;
            }
            if(ctrlLocalDrivingLicenseInfoWithFilter1.License.IsDetained)
            {
                MessageBox.Show("License is already detained!", "Detained Already", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFineFees.Enabled = false;
                return;
            }
            txtFineFees.Enabled = true;
            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void frmDetainLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text)) 
            {
               
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }

            
        }
    }
}
