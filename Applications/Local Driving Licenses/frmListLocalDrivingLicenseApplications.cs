using DVLDSystem.Applications.Local_Driving_Licenses;
using PeopleBusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        private int _PassedTestCount;
        private string _Status;
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private void _FormatGird()
        {
            if (dataGridView1.Rows.Count > 0)
            {
    
                dataGridView1.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "Application ID";
                dataGridView1.Columns["LocalDrivingLicenseApplicationID"].Width = 100;



                dataGridView1.Columns["ClassName"].HeaderText = "Class Name";
                dataGridView1.Columns["ClassName"].Width = 200;


                dataGridView1.Columns["FullName"].HeaderText = "Full Name";
                dataGridView1.Columns["FullName"].Width = 200;



                dataGridView1.Columns["NationalNo"].HeaderText = "National No";


                dataGridView1.Columns["PassedTestCount"].HeaderText = "Passed Tests";
                dataGridView1.Columns["PassedTestCount"].Width = 100;

                
            }
            cbFilters.SelectedIndex = 0;
            
        }
        private void _LoadData()
        {

            
            _dtAllLocalDrivingLicenseApplications= clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();
            dataGridView1.DataSource=_dtAllLocalDrivingLicenseApplications;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();

            _FormatGird();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            
            _LoadData();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchBar.Visible = (cbFilters.Text != "None");
            if (txtSearchBar.Visible)
            {

                txtSearchBar.Focus();
                txtSearchBar.Text = "";
            }
            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";

            switch(cbFilters.Text)
            {
                case "L.D.LAppID":
                    FilterBy = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterBy = "NationalNo";
                    break;

                case "Full Name":
                    FilterBy = "FullName";
                    break;

                case "Status":
                    FilterBy = "Status";
                    break;

                default:
                    FilterBy = "None";
                    break;

            }
            if (txtSearchBar.Text.Trim() == "" || FilterBy == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterBy == "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtSearchBar.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter=string.Format("[{0}] LIKE '{1}%'",FilterBy, txtSearchBar.Text.Trim());
            
            lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();
                
        }

        private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem == "L.D.LAppID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (cbFilters.SelectedItem == "FullName")
            {
                if (char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }


        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmNewDrivingLicenseApplication frm = new frmNewDrivingLicenseApplication();
            frm.ShowDialog();
            _LoadData();
        }

        private void showLicensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("No license found.", "No license", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to cancel this Application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int LocalDrivingLicenseApplicationID =(int) dataGridView1.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplications LocalDrivingLicenseApplication = 
                    clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

                if(LocalDrivingLicenseApplication!=null)
                {
                    if (LocalDrivingLicenseApplication.Cancel())
                    {
                        MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _LoadData();
                    }
                    else
                        MessageBox.Show("Error Cancelling Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _LoadData();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _PassedTestCount = Convert.ToInt32(dataGridView1.CurrentRow.Cells["PassedTestCount"].Value);
            _Status = (string)dataGridView1.CurrentRow.Cells["Status"].Value;
            if (_Status == "New")
            {
                if (_PassedTestCount == 0)
                {
                    cmtVisionTest.Enabled = true;
                    cmtWritingTest.Enabled = false;
                    cmtStreetTest.Enabled = false;
                    cmtScheduleTests.Enabled = true;
                    cmtIssueDrivingLicense.Enabled = false;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                }
                else if (_PassedTestCount == 1)
                {
                    cmtVisionTest.Enabled = false;
                    cmtWritingTest.Enabled = true;
                    cmtStreetTest.Enabled = false;
                    cmtScheduleTests.Enabled = true;
                    cmtIssueDrivingLicense.Enabled = false;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                }
                else if (_PassedTestCount == 2)
                {
                    cmtVisionTest.Enabled = false;
                    cmtWritingTest.Enabled = false;
                    cmtStreetTest.Enabled = true;
                    cmtScheduleTests.Enabled = true;
                    cmtIssueDrivingLicense.Enabled = false;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                }
                else if (_PassedTestCount == 3)
                {
                    cmtScheduleTests.Enabled = false;
                    cmtIssueDrivingLicense.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                }
                cmtShowLicense.Enabled = false;
            }

            else if (_Status == "Cancelled")
            {

                cmtScheduleTests.Enabled = false;
                cmtIssueDrivingLicense.Enabled = false;

                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;

                if (_PassedTestCount > 0)
                {
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = false;

                }
                else
                {
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;

                }
                cmtShowLicense.Enabled = false;
            }
            else if (_Status == "Completed")
            {

                cmtScheduleTests.Enabled = false;
                cmtIssueDrivingLicense.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                cmtShowLicense.Enabled = true;
                editApplicationToolStripMenuItem.Enabled = false;
            }




        }

        private void _ScheduleTest(clsTestTypes.enTestType TestType)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value,TestType);
            frm.ShowDialog();
            _LoadData();

        }

        private void visionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void cmtWritingTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void cmtStreetTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void cmtIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicenseApplication frm = new frmIssueDrivingLicenseApplication((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplications.ApplicantPersonID);
            frm.ShowDialog();
            _LoadData();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewDrivingLicenseApplication frm =new frmNewDrivingLicenseApplication((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
        }
    }
}
