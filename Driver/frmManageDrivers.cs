using DVLDSystem.People;
using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmManageDrivers : Form
    {
        private DataTable _dtDrivers;
        private void _LoadData()
        {
            _dtDrivers = clsManageDrivers.GetDrivers();
            dataGridView1.DataSource =_dtDrivers;
            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView1.Columns["NumberOfActiveLicenses"].HeaderText = "Active Liceneses";
                dataGridView1.Columns["NumberOfActiveLicenses"].Width = 140;

                dataGridView1.Columns["FullName"].HeaderText = "Full Name";
                dataGridView1.Columns["FullName"].Width = 220;

                dataGridView1.Columns["DriverID"].Width = 80;
                dataGridView1.Columns["DriverID"].HeaderText = "Driver ID";

                dataGridView1.Columns["PersonID"].Width = 60;
                dataGridView1.Columns["PersonID"].HeaderText = "Person ID";

                dataGridView1.Columns["NationalNo"].Width = 100;
                dataGridView1.Columns["NationalNo"].HeaderText = "National No.";

                dataGridView1.Columns["CreatedDate"].HeaderText = "Created Date";
                dataGridView1.Columns["CreatedDate"].Width = 120;

                lblNumberOfDrivers.Text = dataGridView1.Rows.Count.ToString();
            }

        }
        public frmManageDrivers()
        {
            InitializeComponent();
        }



        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 0;
            txtSearchBar.Enabled = false;
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchBar.Visible = (cbFilters.Text != "None");
            if (cbFilters.SelectedIndex == 0)
            {
                txtSearchBar.Enabled = false;
            }
            else
                txtSearchBar.Enabled = true;

            txtSearchBar.Clear();
            txtSearchBar.Focus();
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";

            switch(cbFilters.Text)
            {
                case "Driver ID":
                    FilterBy = "DriverID";
                    break;
                case "Person ID":
                    FilterBy = "PersonID";
                    break;
                case "National No.":
                    FilterBy = "NationalNo";
                    break;
                case "Full Name":
                    FilterBy = "FullName";
                    break;
                default:
                    FilterBy = "None";
                    break;
            }

            if (txtSearchBar.Text.Trim() == "" || FilterBy == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblNumberOfDrivers.Text = _dtDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterBy != "FullName" && FilterBy != "NationalNo")
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, txtSearchBar.Text.Trim());
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterBy, txtSearchBar.Text.Trim());

            lblNumberOfDrivers.Text=_dtDrivers.Rows.Count.ToString();


        }

        private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem == "Person ID" || cbFilters.SelectedItem == "Driver ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (cbFilters.SelectedItem == "Full Name")
            {
                if (char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dataGridView1.CurrentRow.Cells[1].Value); 
            frm.ShowDialog();
            _LoadData();
        }

        private void issueInternationalLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _LoadData();
        }
    }
}
