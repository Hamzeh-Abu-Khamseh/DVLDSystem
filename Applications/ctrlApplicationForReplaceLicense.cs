using System;
using System.Globalization;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlApplicationForReplaceLicense : UserControl
    {
        public int LicenseRenewApplicationID
        {
            get { return Convert.ToInt32(lblLicenseRenewAppID.Text); }
            set { lblLicenseRenewAppID.Text = value.ToString(); }
        }

        public DateTime ApplicationDate
        {
            get
            {
                DateTime result;
                return DateTime.TryParseExact(lblApplicationDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
                    ? result
                    : DateTime.MinValue; // Or throw a custom exception/log
            }
            set { lblApplicationDate.Text = value.ToString("dd/MM/yyyy"); }
        }

        public float ApplicationFees
        {
            get
            {
                float value;
                return float.TryParse(lblApplicationFees.Text, out value) ? value : 0f;
            }
            set { lblApplicationFees.Text = value.ToString("F2"); }
        }
        public int ReplacedLicenseID
        {
            get { return Convert.ToInt32(lblReplacedLicenseID.Text); }
            set { lblReplacedLicenseID.Text = value.ToString(); }
        }
        public int OldLicenseID
        {
            get { return Convert.ToInt32(lblOldLicenseID.Text); }
            set { lblOldLicenseID.Text = value.ToString(); }
        }

        public string CreatedBy
        {
            get { return lblCreatedBy.Text; }
            set { lblCreatedBy.Text = value; }
        }

        public ctrlApplicationForReplaceLicense()
        {
            InitializeComponent();
        }

        public void ResetValues()
        {
            lblApplicationDate.Text = "[------]";
            
            lblReplacedLicenseID.Text = "[------]";
            lblOldLicenseID.Text = "[------]";
            lblCreatedBy.Text = "[------]";
            lblLicenseRenewAppID.Text = "[------]";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationForReplaceLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
