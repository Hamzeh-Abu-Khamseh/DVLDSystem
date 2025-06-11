using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _TestAppointmentID = -1;
        

        private clsTestTypes _TestType;
        public frmScheduleTest()
        {
            InitializeComponent();
        }
        
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID,int TestAppointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            _TestTypeID= TestTypeID;
            _TestAppointmentID= TestAppointmentID;


        }

        
        private void ctrlScheduleTests1_Load(object sender, EventArgs e)
        {

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTests1.TestTypeID = _TestTypeID;
            ctrlScheduleTests1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestAppointmentID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
