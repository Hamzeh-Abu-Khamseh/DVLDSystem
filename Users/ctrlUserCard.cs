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

namespace DVLDSystem.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUsers _User;
        private int _UserID = -1;

        private void _ResetDefaultValues()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[-----]";
            lblUsername.Text= "[-----]";
            lblIsActive.Text= "[-----]"; 
        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.UserName;

            if (_User.IsActive == true)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

        }
        public void LoadData(int UserID)
        {
            _User = clsUsers.FindByUserID(UserID);
            if(_User==null)
            {
                _ResetDefaultValues();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }
    }
}
