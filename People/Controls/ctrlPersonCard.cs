using DVLDSystem.Properties;
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
using System.IO;

namespace DVLDSystem.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPeople _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeople Person
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[-----]";
            lblFullName.Text = "[-----]";
            lblNationalNo.Text = "[-----]";
            lblGendor.Text = "[-----]";
            lblEmail.Text = "[-----]";
            lblAddress.Text = "[-----]";
            lblDateOfBirth.Text = "[-----]";
            lblPhone.Text = "[-----]";
            lblCountry.Text = "[-----]";
            pbPersonImage.Image = Resources.Male_512;
        }

        private void _FillPersonInfo()
        {
            lnkEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FullName;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private  void _LoadPersonInfo()
        {
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPeople.Find(PersonID);
            _LoadPersonInfo();
            
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPeople.Find(NationalNo);
            _LoadPersonInfo();
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {
            
        }


        private void lnkEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePersonInfo frm= new frmAddUpdatePersonInfo(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
