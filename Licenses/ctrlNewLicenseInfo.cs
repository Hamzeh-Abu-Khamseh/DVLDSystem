using System;
using System.Globalization;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class ctrlNewLicenseInfo : UserControl
    {


        public int RenewLicenseApplicationID
        {
            get { return Convert.ToInt32(lblRenewLicenseAppID.Text); }
            set { lblRenewLicenseAppID.Text = value.ToString(); }
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


        public DateTime IssueDate
        {

            get
            {
                DateTime result;
                return DateTime.TryParseExact(lblIssueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
                    ? result
                    : DateTime.MinValue;
            }

            set { lblIssueDate.Text = value.ToString("dd/MM/yyyy"); }
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

        public float LicenseFees
        {
            get
            {
                float value;
                return float.TryParse(lblLicenseFees.Text, out value) ? value : 0f;
            }
            set { lblLicenseFees.Text = value.ToString("F2"); }
        }

        public float TotalFees
        {
            get
            {
                float value;
                return float.TryParse(lblTotalFees.Text, out value) ? value : 0f;
            }
            set { lblTotalFees.Text = value.ToString(); }
        }
        public int RenewedLicenseID
        {
            get { return Convert.ToInt32(lblRenewedLicenseID.Text); }
            set { lblRenewedLicenseID.Text = value.ToString(); }
        }
        public int OldLicenseID
        {
            get { return Convert.ToInt32(lblOldLicenseeID.Text); }
            set { lblOldLicenseeID.Text = value.ToString(); }
        }

        public string CreatedBy
        {
            get { return lblCreatedBy.Text; }
            set { lblCreatedBy.Text = value; }
        }
        public string Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        }


        public DateTime ExpirationDate
        {
            get
            {
                return DateTime.ParseExact(lblExpirationDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            set
            {
                lblExpirationDate.Text = value.ToString("dd/MM/yyyy");
            }
        }

        public ctrlNewLicenseInfo()
        {
            InitializeComponent();
        }
        public void ResetValues()
        {
            lblRenewLicenseAppID.Text = "[------]";
            lblApplicationFees.Text = "[------]";
            lblLicenseFees.Text = "[------]";
            lblTotalFees.Text = "[------]";
            lblRenewedLicenseID.Text = "[------]";
            lblOldLicenseeID.Text = "[------]";
            lblCreatedBy.Text = "[------]";
            lblExpirationDate.Text = "[------]";
            lblApplicationDate.Text = "[------]";
            lblIssueDate.Text = "[------]";
            lblApplicationDate.Text = "[------]";
            lblIssueDate.Text = "[------]";
            lblApplicationDate.Text = "[------]";
            lblCreatedBy.Text = "[------]";
        }
        private void ctrlNewLicenseInfo_Load(object sender, EventArgs e)
        {
            

            ApplicationDate = DateTime.Now;
            IssueDate = DateTime.Now;

        }
    }
}
