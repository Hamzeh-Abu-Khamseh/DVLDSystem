using System;
using PeopleBusinessLayer;
using System.Windows.Forms;
using System.Data;
using System.Security.Policy;
using System.Xml.Linq;
using DVLDSystem.People;

namespace DVLDSystem
{
    public partial class frmListDetainedLicenses : Form
    {

        private DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        private void _LoadData()
        {
            _dtDetainedLicenses= clsManageDetainedLicenses.GetAllDetainedLicenses();
            dataGridView1.DataSource = _dtDetainedLicenses;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dataGridView1.Rows.Count > 0)
            {



                dataGridView1.Columns["CreatedByUserID"].Visible = false;
                dataGridView1.Columns["ReleasedByUserID"].Visible = false;

                dataGridView1.Columns["DetainID"].HeaderText = "Detain ID";
                dataGridView1.Columns["DetainID"].Width = 80;

                dataGridView1.Columns["LicenseID"].HeaderText = "License ID";
                dataGridView1.Columns["LicenseID"].Width = 85;

                dataGridView1.Columns["DetainDate"].HeaderText = "Detain Date";
                dataGridView1.Columns["DetainDate"].Width = 120;

                dataGridView1.Columns["FineFees"].HeaderText = "Fine Fees";
                dataGridView1.Columns["FineFees"].Width = 80;

                dataGridView1.Columns["ReleaseDate"].HeaderText = "Release Date";
                dataGridView1.Columns["ReleaseDate"].Width = 120;

                dataGridView1.Columns["NationalNo"].HeaderText = "National No.";
                dataGridView1.Columns["NationalNo"].Width = 80;

                dataGridView1.Columns["FullName"].HeaderText = "Full Name";
                dataGridView1.Columns["FullName"].Width = 150;

                dataGridView1.Columns["IsReleased"].HeaderText = "Is Released";
                dataGridView1.Columns["IsReleased"].Width = 90;

                dataGridView1.Columns["ReleaseApplicationID"].HeaderText = "R.Application ID";
                dataGridView1.Columns["ReleaseApplicationID"].Width = 100;
            }

        }
        
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 0;
            _LoadData();

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchBar.Text = "";
            cbIsReleased.Text = "All";

            if (cbFilters.SelectedItem.ToString() == "Is Released")
            {
                txtSearchBar.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;
                cbIsReleased.Focus();
            }

            else
            {
                txtSearchBar.Visible = cbFilters.Text != "None";
                cbIsReleased.Visible = false;


                if (cbFilters.Text == "None")
                {
                    txtSearchBar.Visible = false;
                    cbIsReleased.Text = "All";

                    

                }
                else
                    txtSearchBar.Visible = true;

                txtSearchBar.Text = "";
                txtSearchBar.Focus();
            }
        }

        private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "Detain ID" || cbFilters.Text == "License ID"|| cbFilters.Text == "Is Released")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "None";
            switch(cbFilters.Text)
            {
                case "Detain ID":
                    FilterBy = "DetainID";
                    break;

                case "License ID":
                    FilterBy = "LicenseID";
                    break;

                case "National No.":
                    FilterBy = "NationalNo";
                    break;
                case "Full Name":
                    FilterBy = "FullName";
                    break;
                case "Release Application ID":
                    FilterBy = "ReleaseApplicationID";
                    break;
                default:
                    FilterBy="None"; break;
            }

            if(txtSearchBar.Text.Trim()==""||FilterBy=="None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblNumberOfUsers.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterBy == "DetainID" || FilterBy == "ReleaseApplicationID"||FilterBy=="LicenseID")
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtSearchBar.Text.Trim());
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterBy, txtSearchBar.Text.Trim());

            lblNumberOfUsers.Text= dataGridView1.Rows.Count.ToString();

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterBy = cbIsReleased.Text;
            string Filter = "IsReleased";

            switch(FilterBy)
            {
                case "All":
                    break;
                case "Yes":
                    FilterBy = "1";
                    break;
                case "No":
                    FilterBy = "0";
                    break;
            }
            if(_dtDetainedLicenses==null)
            {
                return;
            }
            if (FilterBy == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";

            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}]={1}", Filter, FilterBy);

            lblNumberOfUsers.Text = dataGridView1.Rows.Count.ToString();
         
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            _LoadData();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            _LoadData();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            int PersonID = clsManageLicenses.FindLicenseByLicenseID(LicenseID).DriverInfo.PersonID;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            clsManageLicenses License = clsManageLicenses.FindLicenseByLicenseID(LicenseID);
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(License.DriverInfo.PersonID);
            frm.ShowDialog();
            
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool IsReleased= (bool)dataGridView1.CurrentRow.Cells["IsReleased"].Value;

            releaseDetainedLicenseToolStripMenuItem.Enabled = !IsReleased;
            
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _LoadData();
        }
    }
}
