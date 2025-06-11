using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmTakeTest : Form
    {


        private clsTestAppointments _TestAppointment;
        
        private int _TestAppointmentID;
        private clsTestTypes.enTestType _TestTypeID;

        private int _TestID;
        private clsManageTests _Test;


        
        public frmTakeTest()
        {
            InitializeComponent();
        }

        public frmTakeTest(int TestAppointmentID, clsTestTypes.enTestType TestTypeID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

            InitializeComponent();

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;        
            else
                btnSave.Enabled = true;

            int TestID = ctrlScheduledTest1.TestID;
            if (TestID != -1)
            {
                _Test = clsManageTests.Find(TestID);
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;

            }
            else
                _Test = new clsManageTests();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to submit the result?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) 
            {
                return;
            }
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID=clsGlobalSettings.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Result saved successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error saving result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
