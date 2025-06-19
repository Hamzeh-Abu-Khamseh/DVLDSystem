using DVLDSystem.Global_Classes;
using PeopleBusinessLayer;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace DVLDSystem
{
    public partial class frmLogin : Form
    {
        private string filePath = Application.StartupPath + @"\rememberme.txt";
        private short _Trials = 0;

        private clsUsers _User = new clsUsers();

        public frmLogin()
        {
            InitializeComponent();
        }

       

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length >= 2)
                {

                    txtUserName.Text = lines[0];
                    txtPassword.Text = lines[1];
                    chkRememberMe.Checked = true;
                }
            }
        }

        private void SaveUserNameAndPassword(string UserName,string Password)
        {

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(UserName);
                writer.WriteLine(Password); 
            }
        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string HashedPassword = clsUtil.ComputeHash(txtPassword.Text);
            _User = clsUsers.FindByUsernameAndPassword(txtUserName.Text, HashedPassword);
            

            if (_Trials < 3)
            {
                if (_User == null)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    _Trials++;
                    return;
                }
                if (_User.IsActive == false)
                {
                    MessageBox.Show("User is Inactive, please contact your admin.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                    return;
                }

                clsGlobalSettings.CurrentUser = _User;

                if (chkRememberMe.Checked)
                {
                    SaveUserNameAndPassword(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    SaveUserNameAndPassword("", "");
                }
                this.Hide();
                frmMain mainForm = new frmMain();
                mainForm.FormClosed += (s, args) => this.Close(); // closes the app when main form is closed
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("You have exceeded the maximum number of login attempts.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobalSettings.LogError(  "User exceeded maximum login attempts", System.Diagnostics.EventLogEntryType.Warning);
                this.Close();
            }

            

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
