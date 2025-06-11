using DVLDSystem.Properties;
using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlScheduleTests : UserControl
    {
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID;

        private clsTestAppointments _TestAppointment;
        private int _TestAppointmentID;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode=enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        
        public clsTestTypes.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }

                }
            }
        }
        public ctrlScheduleTests()
        {
            InitializeComponent();
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointments.Find(_TestAppointmentID);
            if(_TestAppointment==null)
            {
                MessageBox.Show("Appointment is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointmentDate;

            dtpDate.Value=_TestAppointment.AppointmentDate;
            if (_CreationMode != enCreationMode.RetakeTestSchedule)
            {
                lblRetakeTestAppID.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeTestFees.Text = "2";
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }




            return true;
        }
        private bool _HandleActiveTestAppointment()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplications.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblUserMessage.Text = "Person Already have and active appointment for this test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleLockedAppointment()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "Applicant already had this test.";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePreviousTest()
        {
            // continue from here
            switch(TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    if(!_LocalDrivingLicenseApplication.DidPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Applicant did not pass the vision test yet, he/she must pass it.";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }
                    return true;

                case clsTestTypes.enTestType.StreetTest:
                    if(!_LocalDrivingLicenseApplication.DidPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Applicant did not pass the writtent test yet, he/she must pass it to schedule the street test.";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        lblUserMessage.Text = "";
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }
                    return true;

            }
            return true;
        }   
        public void LoadInfo(int LocalDrivingLicenseApplicationID,int TestAppointmentID=-1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID=TestAppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("No Application with ID = "+_LocalDrivingLicenseApplicationID,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;    
                return;

            }

            if (_LocalDrivingLicenseApplication.DidAttendTestType(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;

                lblRetakeTestFees.Text = "2";
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";

            }
            lblDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text=_LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                dtpDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestTypes.Find(TestTypeID).TestTypeFees.ToString();
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointments();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;

            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();

            if (!_HandleActiveTestAppointment())
                return;

            if (!_HandlePreviousTest())
                return;

            if (!_HandleLockedAppointment())
                return;
                


        }
        private void ctrlScheduleTests_Load(object sender, EventArgs e)
        {
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobalSettings.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpDate.Enabled = false;

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            btnSave.Enabled = false;

        }


    }
}
