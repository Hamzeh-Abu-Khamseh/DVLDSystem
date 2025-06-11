using DVLDSystem.People;
using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;

        private int _LicenseID;
        
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
            set { _LocalDrivingLicenseApplicationID = value; }
        }

        private void _ResetLocalDrivingLicenseApplicationInfoValues()
        {
            ctrlApplicationBasicInfo1.ResetDefaultValues();
            _LocalDrivingLicenseApplicationID = -1;
            lblLicenseClassName.Text = "[-----]";
            lblPassedTests.Text = "[-----]";
        }

        private void _FillLocalDrivingLicenseApplicationInfoValues()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            lnkLicenseInfo.Enabled = (_LicenseID != -1);



            lblDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
            lblLicenseClassName.Text = clsLicenseClasses.FindLicenseClass(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";
        }

        private void _ResetValues()
        {
            _ResetLocalDrivingLicenseApplicationInfoValues();


            MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        public void LoadApplicationInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                _ResetValues();
                return;
            }
            _FillLocalDrivingLicenseApplicationInfoValues();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByApplicationID(ApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                _ResetValues();
                return;
            }
            _FillLocalDrivingLicenseApplicationInfoValues();

        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            //frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _LocalLicenseApplication.PassedTestCount);
            //frm.ShowDialog();
        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }
    }
}
