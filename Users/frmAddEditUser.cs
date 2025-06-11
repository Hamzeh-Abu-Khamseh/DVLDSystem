using PeopleBusinessLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;


        private clsUsers _User;
        private int _UserID;

        public frmAddEditUser()
        {
            InitializeComponent();
        }
        public frmAddEditUser(int UserID)
        {
            _UserID = UserID;
            InitializeComponent();
            if (UserID == -1)
            {
                _Mode = enMode.AddNew;
                this.Text = "Add New User";
            }
            else
            {

                btnSave.Enabled = true;
                _Mode = enMode.Update;
                this.Text = "Update User";


            }
        }
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                label2.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUsers();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();


            }
            else
            {
                label2.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;


            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            checkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUsers.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            checkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!this.ValidateChildren())
            {
              
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if(txtConfirmPassword.Text=="" || txtPassword.Text==""||txtUserName.Text=="")
            {
                MessageBox.Show("Fill All the fields please, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.UserName=txtUserName.Text;
            
            _User.IsActive = checkIsActive.Checked;

            if (txtPassword.Text != txtConfirmPassword.Text)
            {

                MessageBox.Show("Passwords should be identical");
                return;
            }
            _User.Password = txtPassword.Text;
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully","Successfull",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtUserID.Text = _User.UserID.ToString();
                label2.Text = "Edit User";

            }
            else
            {
                MessageBox.Show("Error Saving Data, Fill All Fields");
            }
        }

        private void txtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            ;


            if (_Mode == enMode.AddNew)
            {

                if (clsUsers.DoesUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
                
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUsers.DoesUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    
                }
            }
        }

        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password Should have a Value!");

            }
            else
                errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password Should have a Value!");
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords should be identical");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void frmAddEditUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsUsers.DoesUserExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person is already a user.", "Declined", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }
        }
    }
}
