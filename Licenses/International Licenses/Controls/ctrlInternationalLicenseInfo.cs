using DVLDSystem.Global_Classes;
using DVLDSystem.Properties;
using PeopleBusinessLayer;
using System;
using System.Windows.Forms;
using System.IO;

namespace DVLDSystem
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID = -1;
        private clsManageInternationalLicenses _InternationalLicense;

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }
        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadData(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsManageInternationalLicenses.Find(_InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblIntLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDataOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");

            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToString("dd/MM/yyyy");
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToString("dd/MM/yyyy"); 

            _LoadPersonImage();



        }
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void ctrlInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            
        }
    }
}
