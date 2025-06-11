using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtApplicationTypes;
        private void _RefreshData()
        {
            dgvApplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dtApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;

            lblNumberOfRecords.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns["ApplicationTypeID"].HeaderText = "ID";
                dgvApplicationTypes.Columns["ApplicationTypeID"].Width = 50;

                dgvApplicationTypes.Columns["ApplicationTypeTitle"].HeaderText = "Title";
                dgvApplicationTypes.Columns["ApplicationTypeTitle"].Width = 200;
                
                dgvApplicationTypes.Columns["ApplicationFees"].HeaderText = "Fees";
                dgvApplicationTypes.Columns["ApplicationFees"].Width = 50;

            }
        }
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {

            _RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshData();
        }


    }
}
