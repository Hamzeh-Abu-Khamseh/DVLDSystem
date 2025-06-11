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
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _LicenseID=-1;
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlLocalDrivingLicenseInfoWithFilter1.LoadData(LicenseID);
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
           

        }
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

        }
       
        private void ctrlLocalDrivingLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            _LicenseID= LicenseID;
            lblLicenseID.Text = _LicenseID.ToString();

            lnkShowLicenseHistory.Enabled = (_LicenseID != -1);

            if(_LicenseID==-1)
            {
                btnRelease.Enabled = false;
                return;
            }

            if(!ctrlLocalDrivingLicenseInfoWithFilter1.License.IsDetained)
            {
                MessageBox.Show("License is not detained.", "Not detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;

            lblDetainID.Text = ctrlLocalDrivingLicenseInfoWithFilter1.License.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlLocalDrivingLicenseInfoWithFilter1.LicenseID.ToString();

            lblCreatedBy.Text = ctrlLocalDrivingLicenseInfoWithFilter1.License.DetainedInfo.CreatedByUserInfo.UserName;
            lblDetainDate.Text = ctrlLocalDrivingLicenseInfoWithFilter1.License.DetainedInfo.DetainDate.ToString("dd/MM/yyyy");

            lblFineFees.Text = ctrlLocalDrivingLicenseInfoWithFilter1.License.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int ApplicationID = -1;

            bool IsReleased = ctrlLocalDrivingLicenseInfoWithFilter1.License.ReleaseDetainedLicense(clsGlobalSettings.CurrentUser.UserID,ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();

            if(!IsReleased)
            {
                MessageBox.Show("Error Releasing detained License.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License released successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlLocalDrivingLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

      
    }
}
