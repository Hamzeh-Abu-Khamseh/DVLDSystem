using PeopleBusinessLayer;
using System;
using System.Windows.Forms;
using System.ComponentModel;
namespace DVLDSystem
{
    public partial class frmEditTestTypeInformation : Form
    {
        private clsTestTypes _TestType;
        private clsTestTypes.enTestType _TestTypeID=clsTestTypes.enTestType.VisionTest;


        public frmEditTestTypeInformation(clsTestTypes.enTestType TestTypeID)
        {
            _TestTypeID = TestTypeID;


            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadData()
        {
            lblTestTypeID.Text = ((int)_TestTypeID).ToString();
            txtTestTypeTitle.Text = _TestType.TestTypeTitle.ToString();
            txtTestTypeDescription.Text = _TestType.TestTypeDescription.ToString();
            txtTestTypeFees.Text = _TestType.TestTypeFees.ToString();
        }
        private void frmEditTestTypeInformation_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find(_TestTypeID);
            if (_TestType != null)
            {
                _LoadData();
            }
            else  
            {
                MessageBox.Show("Test Type Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid, point your mouse to the red icon to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeDescription = txtTestTypeDescription.Text;
            _TestType.TestTypeFees = Convert.ToSingle(txtTestTypeFees.Text);
            _TestType.TestTypeTitle = txtTestTypeTitle.Text;

            if (_TestType.Save())
            {
                MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error Saving Data","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

        }

        private void txtTestTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeTitle, null);
            }
            ;
        }

        private void txtTestTypeDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeDescription, "Description cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeDescription, null);
            }
            
        }

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "Fees cannot be 0!");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);
            }
        }

        private void txtTestTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains("."))) 
            {
                e.Handled = true;
            }
        }
    }
}
