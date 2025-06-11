using DVLDSystem.People;
using DVLDSystem.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            // Set the form to fill the whole screen
            this.WindowState = FormWindowState.Maximized;

            // Optional: Remove the title bar and borders

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            applicationsToolStripMenuItem.Image = new Bitmap(Properties.Resources.GoodNotes, new Size(64, 64));
            peopleToolStripMenuItem.Image = new Bitmap(Properties.Resources.People, new Size(64, 64));
            usersToolStripMenuItem.Image = new Bitmap(Properties.Resources.Select_Users, new Size(64, 64));
            AccountSettingstoolStripMenuItem2.Image = new Bitmap(Properties.Resources.account_settings_64, new Size(64, 64));
            driversToolStripMenuItem.Image = new Bitmap(Properties.Resources.Vanpool, new Size(64, 64));
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
            
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            clsGlobalSettings.CurrentUser=null;
            frmLogin loginForm = new frmLogin();
            loginForm.FormClosed += (s, args) => this.Close(); // close Form1 when login form is closed
            loginForm.Show();
        }

        private void manageApplicationsTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewDrivingLicenseApplication frm = new frmNewDrivingLicenseApplication();
            frm.ShowDialog();

        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers frm = new frmManageDrivers();
            frm.ShowDialog();
        }

        private void internationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplication frm = new frmInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicenses frm = new frmManageInternationalLicenses();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplications frm = new frmRenewLicenseApplications();
            frm.ShowDialog();
        }

        private void replacmentForDamagedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicenseApplication frm = new frmReplaceLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm= new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInfo frm = new frmAddUpdatePersonInfo();
            frm.ShowDialog();
        }
    }
}
