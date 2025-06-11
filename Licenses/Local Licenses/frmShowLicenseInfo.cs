using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmShowLicenseInfo : Form
    {
        
        private int _LicenseID;
       

       
        public frmShowLicenseInfo()
        {
            InitializeComponent();
        }
        public frmShowLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            InitializeComponent();


        }
        
        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {

            ctrlDriverLicenseInfo1.LoadData(_LicenseID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
