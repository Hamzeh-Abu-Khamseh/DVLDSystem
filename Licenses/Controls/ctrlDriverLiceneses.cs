using DVLDSystem.Licenses.International_Licenses;
using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlDriverLiceneses : UserControl
    {

        private clsManageDrivers _Driver;
        private int _DriverID;

        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;
        public ctrlDriverLiceneses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenses()
        {
            _dtLocalLicenses = clsManageLicenses.GetAllLicenesByDriverID(_DriverID);
            dgvLocalLicenes.DataSource = _dtLocalLicenses;
            dgvLocalLicenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvLocalLicenes.Rows.Count > 0)
            {

                dgvLocalLicenes.Columns["LicenseID"].Width = 100;
                dgvLocalLicenes.Columns["ApplicationID"].Width = 80;
                dgvLocalLicenes.Columns["IsActive"].Width = 60;
                dgvLocalLicenes.Columns["IssueDate"].Width = 125;
                dgvLocalLicenes.Columns["ExpirationDate"].Width = 125;
                dgvLocalLicenes.Columns["ClassName"].Width = 200;
                lblLocalLicensesRecordCount.Text = dgvLocalLicenes.Rows.Count.ToString();
            }

        }
        private void _LoadInternatinoalLicenses()
        {
            _dtInternationalLicenses = clsManageInternationalLicenses.GetAllInternationalLicensesBy(_DriverID);
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;

            lblInternationalLicenesRecordCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvInternationalLicenses.Columns["InternationalLicenseID"].Width = 100;
                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].Width = 120;
                dgvInternationalLicenses.Columns["DriverID"].Width = 80;
                dgvInternationalLicenses.Columns["ApplicationID"].Width = 100;
                dgvInternationalLicenses.Columns["IssueDate"].Width = 125;
                dgvInternationalLicenses.Columns["ExpirationDate"].Width = 125;
                dgvInternationalLicenses.Columns["IsActive"].Width = 90;
                dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "Int. License ID";
                dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].HeaderText = "Local License ID";
                dgvInternationalLicenses.Columns["DriverID"].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns["CreatedByUserID"].Visible = false;
               
            }

            
        }
        public void LoadDataByPersonID(int PersonID)
        {
            _Driver=clsManageDrivers.FindDriverByPersonID(PersonID);

            if(_Driver==null )
            {
                MessageBox.Show("There is not driver with Person ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;

            
            _LoadLocalLicenses();

        }
        public void LoadDataByDriverID(int DriverID)
        {
            _DriverID=DriverID;
            _Driver = clsManageDrivers.FindDriverByDriverID(_DriverID);

            if(_Driver == null )
            {
                MessageBox.Show("Driver Not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadInternatinoalLicenses();
            _LoadLocalLicenses();
        }       
        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocalLicenes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
        public void Clear()
        {
            _dtLocalLicenses.Clear();
            _dtInternationalLicenses.Clear();
        }

        private void dgvLocalLicenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==1)
            {
                _LoadInternatinoalLicenses();
            }
        }
    }
}
