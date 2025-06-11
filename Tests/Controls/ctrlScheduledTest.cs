using DVLDSystem.Properties;
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
using static PeopleBusinessLayer.clsTestTypes;

namespace DVLDSystem.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _TestID = -1;

        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        private clsTestAppointments _TestAppointment;
        private int _TestAppointmentID = -1;
        
        public clsTestTypes.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch(_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestInfo.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestInfo.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestInfo.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }
        public int TestID
        {
            get { return _TestID; }
        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;


            _TestAppointment = clsTestAppointments.Find(TestAppointmentID);

            if( _TestAppointment == null )
            {
                MessageBox.Show("There is no Appointment with ID = " + TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }
            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication=clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("There is no application with ID = "+_LocalDrivingLicenseApplication,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();
        }
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

    }
}
