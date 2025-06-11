using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDSystem.People
{
    public partial class frmPersonDetails : Form
    {
        
        public frmPersonDetails()
        {
            InitializeComponent();
        }
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();           
            ctrlPersonCard1.LoadPersonInfo(PersonID);

        }
        public frmPersonDetails(string NationalNo)
        {
            ctrlPersonCard1.LoadPersonInfo(NationalNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
