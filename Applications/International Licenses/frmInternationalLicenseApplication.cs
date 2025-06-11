using Bunifu.UI.WinForms.Helpers.Transitions;
using DVLDSystem.Licenses.International_Licenses;
using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmInternationalLicenseApplication : Form
    {

        private int _InternationalLicenseID = -1;
        public frmInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsManageInternationalLicenses InternationalLicense=new clsManageInternationalLicenses();

            InternationalLicense.ApplicantPersonID = ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate= DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypes.Find((int)clsApplications.enApplicationType.NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            InternationalLicense.DriverID = ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlLocalDrivingLicenseInfoWithFilter1.License.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblILApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlLocalDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm =
              new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            lblFees.Text = clsApplicationTypes.Find((int)clsApplications.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
        }

        private void lnkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void ctrlLocalDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            lblLocalLicenseID.Text = LicenseID.ToString();
            lnkShowLicensesHistory.Enabled = LicenseID != -1;

            if (LicenseID == -1)
            {
                btnIssue.Enabled = false;
                return;
            }

            if(ctrlLocalDrivingLicenseInfoWithFilter1.License.LicenseClassInfo.LicenseClassID!=3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicenseID = clsManageInternationalLicenses.GetActiveInternationalLicenseID(ctrlLocalDrivingLicenseInfoWithFilter1.License.DriverID);

            if(ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lnkShowLicenseInfo.Enabled = false;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }

        private void frmInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
