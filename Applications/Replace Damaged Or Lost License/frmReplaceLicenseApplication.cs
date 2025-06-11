using System;
using System.Windows.Forms;
using PeopleBusinessLayer;
using static PeopleBusinessLayer.clsManageLicenses;
namespace DVLDSystem
{
    public partial class frmReplaceLicenseApplication : Form
    {
        
        private int _NewLicenseID;

        public frmReplaceLicenseApplication()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

        }

        private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }
       
        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }      
       
        private void ctrlLocalDrivingLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            int SelectedLicenseID = LicenseID;
            lblOldLicenseID.Text= SelectedLicenseID.ToString();
            lnkShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if(SelectedLicenseID==-1)
            {
                return;
            }
            if(!ctrlLocalDrivingLicenseInfoWithFilter1.License.IsActive)
            {
                MessageBox.Show("License is Not active.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
     
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsManageLicenses NewLicense = ctrlLocalDrivingLicenseInfoWithFilter1.License.Replace(_GetIssueReason(), clsGlobalSettings.CurrentUser.UserID);

            if(NewLicense==null)
            {
                MessageBox.Show("Error Renewing License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLicenseRenewAppID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("License renewed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            gbReplaceReason.Enabled = false;
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverInfo.PersonInfo.PersonID);
            frm.ShowDialog();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypes.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
        }

        private void frmReplaceLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        
    }
}
