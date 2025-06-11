using PeopleBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLDSystem
{

    public partial class frmNewDrivingLicenseApplication : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode=enMode.AddNew;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _PersonID = -1;
        private clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;



        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        private void _LoadAllClasses()
        {
            DataTable dt = clsLicenseClasses.GetAllLicenseClasses();
            cbLicsenseClasses.DataSource = dt;
            cbLicsenseClasses.DisplayMember = "ClassName";
            cbLicsenseClasses.SelectedIndex = 2;

        }

        private void _ResetDefaultValues()
        {
            _LoadAllClasses();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplications();
                ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                lblFees.Text = clsApplicationTypes.Find((int)clsApplications.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedBy.Text = clsGlobalSettings.CurrentUser.UserName;
                lblApplicationDate.Text = DateTime.Now.ToString();

            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Local Driving License Application Is NOT Found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLDLApplicationID.Text=_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            cbLicsenseClasses.SelectedIndex=cbLicsenseClasses.FindString(clsLicenseClasses.FindLicenseClass(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = clsUsers.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;

        }
        public frmNewDrivingLicenseApplication()
        {
            _Mode = enMode.AddNew;
            InitializeComponent();
        }

        public frmNewDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmNewDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if(_Mode==enMode.Update)
            {
                _LoadData();
            }
            
            _LoadAllClasses();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingApplicationInfo.SelectedIndex++;
            }
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled=true;
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingApplicationInfo.SelectedIndex++;
            }
            else
            {
                MessageBox.Show("Please selected a person first to an Application","Select A Person",MessageBoxButtons.OK,MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }

        }

            

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClasses.FindLicenseClass(cbLicsenseClasses.Text).LicenseClassID;
            int ActiveApplicationID = clsApplications.GetActiveApplicationIDForLicenseClass(ctrlPersonCardWithFilter1.PersonID, clsApplications.enApplicationType.NewDrivingLicense, LicenseClassID);

            if(ActiveApplicationID!=-1)
            {
                MessageBox.Show("Person already has active Application with ApplicationID = " + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }
            if (clsManageLicenses.DoesLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person Already has Acitve license for this Class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplications.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplications.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID=clsGlobalSettings.CurrentUser.UserID;

            if(_LocalDrivingLicenseApplication.Save())
            {
                lblLDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";
                MessageBox.Show("Local Driving License Application saved successfully","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error Saving Data","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void cbLicsenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
