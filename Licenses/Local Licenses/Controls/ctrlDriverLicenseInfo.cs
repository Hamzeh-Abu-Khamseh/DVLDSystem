using DVLDSystem.Properties;
using PeopleBusinessLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsManageLicenses _License;
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsManageLicenses License
        {
            get { return _License; }
        }

        private void _LoadDriverPhoto()
        {
            if (License.DriverInfo.PersonInfo.Gendor == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if(ImagePath!="")
            {
                if(File.Exists(ImagePath))
                    pictureBox1.Load(ImagePath);
                else
                    MessageBox.Show("Could not find Driver's Image.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        public void LoadData(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsManageLicenses.FindLicenseByLicenseID(_LicenseID);
            if(_License==null)
            {
                MessageBox.Show("License with ID = " + LicenseID.ToString() + " Is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            lblClassName.Text = License.LicenseClassInfo.ClassName;
            lblName.Text = License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = License.LicenseID.ToString();
            lblNationalNo.Text = License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = License.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = License.IssueReasonText;
            lblNotes.Text = License.Notes;
            lblIsActive.Text = License.IsActive? "Yes" : "No";
            lblDataOfBirth.Text = License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text = License.DriverInfo.DriverID.ToString();
            lblExpirationDate.Text = License.ExpirationDate.ToString("dd/MM/yyyy");
            lblIsDetained.Text = License.IsDetained ? "Yes" : "No";
            _LoadDriverPhoto();


        }
    }
}
