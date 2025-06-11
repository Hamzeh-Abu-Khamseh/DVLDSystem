using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmIssueDrivingLicenseApplication : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;

        public frmIssueDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        public frmIssueDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtNotes.Text, clsGlobalSettings.CurrentUser.UserID);
            if(LicenseID!=-1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Error Issuing License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmIssueDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if( _LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("No Application with Application "+_LocalDrivingLicenseApplicationID.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if(!_LocalDrivingLicenseApplication.DidPassAllTests())
            {
                MessageBox.Show("Applicant did not pass all tests.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if(LicenseID!=-1)
            {
                MessageBox.Show("Applicant already have a license, License ID = " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
        }
    }
}
