using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _LoadData()
        {
            _dtAllTestTypes = clsTestTypes.GetAllTestTypes();

            dataGridView1.DataSource= _dtAllTestTypes;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
            if(dataGridView1.Rows.Count>0)
            {
                dataGridView1.Columns["TestTypeID"].HeaderText = "ID";
                dataGridView1.Columns["TestTypeID"].Width = 40;

                dataGridView1.Columns["TestTypeTitle"].HeaderText = "Title";
                dataGridView1.Columns["TestTypeTitle"].Width = 80;

                dataGridView1.Columns["TestTypeDescription"].HeaderText = "Description";

                dataGridView1.Columns["TestTypeFees"].HeaderText = "Fees";
                dataGridView1.Columns["TestTypeFees"].Width = 70;

            }

        }


        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            
            frmEditTestTypeInformation frm = new frmEditTestTypeInformation((clsTestTypes.enTestType)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
        }
    }
}
