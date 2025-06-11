using DVLDSystem.People;
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

namespace DVLDSystem.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplications _Application;

        private int _ApplicationID;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        public void ResetDefaultValues()
        {
            lblApplicationID.Text = "[------]";
            lblApplicationStatus.Text = "[------]";
            lblApplicationFees.Text = "[------]";
            lblApplicantName.Text = "[------]";
            lblDate.Text = "[------]";
            lblStatusDate.Text = "[------]";
            lblCreatedBy.Text = "[------]";
            lnkPersonInfo.Visible = false;
        }
        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicationStatus.Text = _Application.StatusTest;
            lblApplicationFees.Text = _Application.PaidFees.ToString();
            lblApplicantName.Text = _Application.ApplicantPersonID.ToString();
            lblApplicationType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedBy.Text = _Application.CreatedByUserInfo.UserName;
            lnkPersonInfo.Enabled = true;
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            
            _Application = clsApplications.FindBaseApplication(ApplicationID);

            if (_Application == null)
            {
                ResetDefaultValues();
                MessageBox.Show("Application with ApplicationID = " + ApplicationID + " Does not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                _FillApplicationInfo();
            




        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void lnkPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_Application.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_ApplicationID);
        }
    }
}
