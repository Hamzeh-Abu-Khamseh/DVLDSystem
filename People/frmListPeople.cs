using DVLDSystem.People;
using PeopleBusinessLayer;
using System;
using System.Windows.Forms;
using System.IO;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLDSystem
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllPeople =clsPeople.GetPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");
        public frmListPeople()
        {
            InitializeComponent();
        }
        private void _RefreshPeople()
        {
            _dtAllPeople=clsPeople.GetPeople();
            _dtPeople= _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvViewAllPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvViewAllPeople.DataSource = _dtPeople;
            lblRecordsNumber.Text = dgvViewAllPeople.Rows.Count.ToString();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvViewAllPeople.DataSource = _dtPeople;
            cbFilters.SelectedIndex = 0;
            lblRecordsNumber.Text = dgvViewAllPeople.Rows.Count.ToString();
            if (dgvViewAllPeople.Rows.Count > 0)
            {

                dgvViewAllPeople.Columns[0].HeaderText = "Person ID";
                dgvViewAllPeople.Columns[0].Width = 80;

                dgvViewAllPeople.Columns[1].HeaderText = "National No.";
                dgvViewAllPeople.Columns[1].Width = 90;


                dgvViewAllPeople.Columns[2].HeaderText = "First Name";
                dgvViewAllPeople.Columns[2].Width = 100;

                dgvViewAllPeople.Columns[3].HeaderText = "Second Name";
                dgvViewAllPeople.Columns[3].Width = 100;


                dgvViewAllPeople.Columns[4].HeaderText = "Third Name";
                dgvViewAllPeople.Columns[4].Width = 100;

                dgvViewAllPeople.Columns[5].HeaderText = "Last Name";
                dgvViewAllPeople.Columns[5].Width = 120;

                dgvViewAllPeople.Columns[6].HeaderText = "Gendor";
                dgvViewAllPeople.Columns[6].Width = 80;

                dgvViewAllPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvViewAllPeople.Columns[7].Width = 140;

                dgvViewAllPeople.Columns[8].HeaderText = "Nationality";
                dgvViewAllPeople.Columns[8].Width = 120;


                dgvViewAllPeople.Columns[9].HeaderText = "Phone";
                dgvViewAllPeople.Columns[9].Width = 120;


                dgvViewAllPeople.Columns[10].HeaderText = "Email";
                dgvViewAllPeople.Columns[10].Width = 170;
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInfo frm = new frmAddUpdatePersonInfo();
            frm.ShowDialog();
            _RefreshPeople();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInfo frm = new frmAddUpdatePersonInfo((int)dgvViewAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvViewAllPeople.CurrentRow.Cells[0].Value;
            string PersonImage = clsPeople.Find(PersonID).ImagePath;
            if (MessageBox.Show("Are You Sure You Want To Delete Person [" + PersonID + "]",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsPeople.DeletePerson(PersonID))
                {
                    if (PersonImage != null)
                    {
                        File.Delete(PersonImage);
                    }
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    _RefreshPeople();
                }
                else
                {
                    MessageBox.Show("Person Was Not Deleted Because It Has Data Linked To It",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilters.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtSearchBar.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvViewAllPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtSearchBar.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtSearchBar.Text.Trim());

            lblRecordsNumber.Text = dgvViewAllPeople.Rows.Count.ToString();
        }

        private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.Text == "PersonID" || cbFilters.Text == "Phone")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            txtSearchBar.Visible = (cbFilters.Text != "None");
            _RefreshPeople();

            if(txtSearchBar.Visible)
           
            {
                txtSearchBar.Text = "";
                txtSearchBar.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void viewPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvViewAllPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeople();
        }
    }
}
