using PeopleBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmChangePassword : Form
    {

        private clsUsers _User;
        private int _UserID;
        public frmChangePassword(int UserID)
        {
            _UserID = UserID;
            InitializeComponent();
            
        }

        private void _ResetDefualtValues()
        {
            txtCurrentPass.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
            txtCurrentPass.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUsers.FindByUserID(_UserID);
            if(_User==null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }
            ctrlUserCard1.LoadData(_UserID);          
        }

      

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid!, put the mouse over the red icon(s) to see the error","Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password = txtNewPass.Text;
            if(_User.Save())
            {
                MessageBox.Show("Password Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An Error has Occured, Password is NOT Updated","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPass, "Current password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPass, null);
            }
            ;

            if (_User.Password != txtCurrentPass.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPass, "Current password is wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPass, null);
            };
            
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPass, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtNewPass, null);
            };
            
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)  
        {
            if (txtConfirmPass.Text.Trim() != txtNewPass.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPass, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPass, null);
            };
            
        }



        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
