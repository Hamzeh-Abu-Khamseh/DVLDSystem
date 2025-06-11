using PeopleBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLDSystem
{
    public partial class frmFindPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        public frmFindPerson()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
        }


    }
}
