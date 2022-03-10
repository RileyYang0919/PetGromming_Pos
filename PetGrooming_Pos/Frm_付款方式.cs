using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetGrooming_Pos
{
    public partial class Frm_付款方式 : Form
    {
        public Frm_付款方式()
        {
            InitializeComponent();
        }

        private void btn_Cash_Click(object sender, EventArgs e)
        {
            Frm_Home.isCash = true;
            this.Close();
        }

        private void btn_Credit_Click(object sender, EventArgs e)
        {
            Frm_Home.isCredit = true;
            this.Close();
        }
    }
}
