using DVLDSystem.Properties;
using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;
using static PeopleBusinessLayer.clsTestTypes;

namespace DVLDSystem
{
    public partial class frmListTestAppointments : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private DataTable _dtTestAppointments;


        
        public frmListTestAppointments()
        {
            InitializeComponent();
        }
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;          
        }
        private void _LoadTestImageAndTitle()
        {
            switch(_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }
                case clsTestTypes.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestImageAndTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            _dtTestAppointments = clsTestAppointments.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestTypeID);
            dataGridView1.DataSource = _dtTestAppointments;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (_dtTestAppointments.Rows.Count > 0)
            {
                dataGridView1.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dataGridView1.Columns["TestAppointmentID"].Width = 150;

                dataGridView1.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dataGridView1.Columns["AppointmentDate"].Width = 200;

                dataGridView1.Columns["PaidFees"].HeaderText = "Paid Fees";
                dataGridView1.Columns["PaidFees"].Width = 150;

                dataGridView1.Columns["IsLocked"].HeaderText = "Is Locked";
                dataGridView1.Columns["IsLocked"].Width = 100;


            }
            lblAppointmentsCount.Text= dataGridView1.Rows.Count.ToString();
            
            
            
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplications localDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //---
            clsManageTests LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                frm1.ShowDialog();
                frmTestAppointments_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestTypeID);
            frm2.ShowDialog();
            frmTestAppointments_Load(null, null);
            //---
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsLocked =(bool) dataGridView1.CurrentRow.Cells[3].Value;
            if (IsLocked)
            {
                MessageBox.Show("Test is already Evaluated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frm = new frmTakeTest((int)dataGridView1.CurrentRow.Cells[0].Value, _TestTypeID);
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);
            

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
