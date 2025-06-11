using DVLDSystem.Licenses.International_Licenses;
using DVLDSystem.People;
using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;
namespace DVLDSystem
{
    public partial class frmManageInternationalLicenses : Form
    {

        private DataTable _dtInternationalLicenses;
        public frmManageInternationalLicenses()
        {
            InitializeComponent();
        }
        private void _LoadData()
        {
            _dtInternationalLicenses = clsManageInternationalLicenses.GetInternationalLicenses();
            dataGridView1.DataSource = _dtInternationalLicenses;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Rows.Count > 0)
            {


                dataGridView1.Columns["InternationalLicenseID"].HeaderText = "Int. License ID";

                dataGridView1.Columns["ApplicationID"].HeaderText = "Application ID";

                dataGridView1.Columns["DriverID"].HeaderText = "Driver ID";

                dataGridView1.Columns["IssuedUsingLocalLicenseID"].HeaderText = "L.License ID";

                dataGridView1.Columns["IssueDate"].HeaderText = "Issue Date";

                dataGridView1.Columns["ExpirationDate"].HeaderText = "Expiration Date";

                dataGridView1.Columns["IsActive"].HeaderText = "Is Active";

                dataGridView1.Columns["CreatedByUserID"].Visible = false;

                lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
            }

            txtSearchBar.Visible = false;
            cbStatus.Visible = false;
            
        }


      


        private void frmManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            _LoadData();
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbStatus.Text = "All";
            txtSearchBar.Text = "";

            if (cbFilters.Text == "Is Active")
            {
                txtSearchBar.Visible = false;
                cbStatus.Visible = true;
                cbStatus.Focus();
                cbStatus.SelectedIndex = 0;
            }

            else

            {

                txtSearchBar.Visible = (cbFilters.Text != "None");
                cbStatus.Visible = false;

                if (cbFilters.Text == "None")
                {
                    txtSearchBar.Enabled = false;
                   

                }
                else
                    txtSearchBar.Enabled = true;

                txtSearchBar.Text = "";
                txtSearchBar.Focus();
            }


        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilters.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    }
                    ;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtSearchBar.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
                return;
            }



            _dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtSearchBar.Text.Trim());

            lblRecordCount.Text = _dtInternationalLicenses.Rows.Count.ToString();


        }

        private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbStatus.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (_dtInternationalLicenses == null)
                return;

            if (FilterValue == "All")
                _dtInternationalLicenses.DefaultView.RowFilter = "";
            else
                
                _dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordCount.Text = _dtInternationalLicenses.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = clsManageDrivers.FindDriverByDriverID(DriverID).PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = clsManageDrivers.FindDriverByDriverID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplication frm = new frmInternationalLicenseApplication();
            frm.ShowDialog();
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
