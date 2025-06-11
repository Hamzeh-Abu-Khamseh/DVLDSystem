using PeopleBusinessLayer;
using System;
using System.IO;
using System.Windows.Forms;


namespace DVLDSystem
{
    public partial class frmLogin : Form
    {
        private string filePath = Application.StartupPath + @"\rememberme.txt";


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
            _User = clsUsers.FindByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
  
            if(_User==null) 
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(_User.IsActive== false)
            {
                MessageBox.Show("User is Inactive, please contact your admin.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }         

            clsGlobalSettings.CurrentUser = _User;

            if (chkRememberMe.Checked)
            {
                SaveUserNameAndPassword(txtUserName.Text,txtPassword.Text);
            }
            else
            {
                SaveUserNameAndPassword("","");
            }
            this.Hide();
            frmMain mainForm = new frmMain();
            mainForm.FormClosed += (s, args) => this.Close(); // closes the app when main form is closed
            mainForm.Show();

            

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
