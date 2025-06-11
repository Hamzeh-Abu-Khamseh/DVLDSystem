using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationTypes _ApplicationType;


        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;

        }


        private void EditApplicationType_Load(object sender, EventArgs e)
        {
            
            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);
            if (_ApplicationType != null)
            {
                _LoadData();
            }
        }
        private void _LoadData()
        {
            txtApplicationTypeTitle.Focus();
            lblApplicationID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtApplicationTypeTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtApplicationTypeFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle= txtApplicationTypeTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtApplicationTypeFees.Text);
            if(_ApplicationType.Save())
            {
                MessageBox.Show("Data updated successfully","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error updating Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtApplicationTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtApplicationTypeTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationTypeTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTypeTitle, "Title Connot be Empty");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeTitle, null);
            }
        }

        private void txtApplicationTypeFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtApplicationTypeFees.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtApplicationTypeFees, "Fees Must have a value");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, null);
            }
        }
    }
}
