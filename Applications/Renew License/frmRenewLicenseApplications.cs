using DVLDSystem.Global_Classes;
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

namespace DVLDSystem
{
    public partial class frmRenewLicenseApplications : Form
    {
      
        private clsManageLicenses _NewLicense;
        private int _NewLicenseID;
        public frmRenewLicenseApplications()
        {
            InitializeComponent();
        }
       
        

        private void frmRenewLicenseApplications_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = lblApplicationDate.Text;

            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlLocalDrivingLicenseInfoWithFilter1.NationalNo);
            //frm.ShowDialog();
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void ctrlLocalDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicense = obj;
            lblOldLicenseeID.Text = SelectedLicense.ToString();
            lnkShowLicenseHistory.Enabled = (SelectedLicense != -1);
            
            if(SelectedLicense==-1)
            {
                return;
            }
            int DefaultValidityLength = ctrlLocalDrivingLicenseInfoWithFilter1.License.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToString("dd/MM/yyyy");
            lblLicenseFees.Text=ctrlLocalDrivingLicenseInfoWithFilter1.License.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlLocalDrivingLicenseInfoWithFilter1.License.Notes;

            if(!ctrlLocalDrivingLicenseInfoWithFilter1.License.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlLocalDrivingLicenseInfoWithFilter1.License.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if(!ctrlLocalDrivingLicenseInfoWithFilter1.License.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsManageLicenses NewLicense = ctrlLocalDrivingLicenseInfoWithFilter1.License.RenewLicense(txtNotes.Text.Trim(), clsGlobalSettings.CurrentUser.UserID);

            if(NewLicense==null)
            {
                MessageBox.Show("Error Renewing the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblRenewLicenseAppID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();

            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;

        }

        private void frmRenewLicenseApplications_Activated(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
