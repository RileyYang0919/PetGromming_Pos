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
    public partial class Frm_服務設定 : Form
    {
        public Frm_服務設定()
        {
            InitializeComponent();
        }
        int[] prices = { Frm_單次美容.price_香氛浴, Frm_單次美容.price_藥浴, Frm_單次美容.price_護毛, Frm_單次美容.price_刷牙, Frm_單次美容.price_剪指甲, Frm_單次美容.price_清潔肛門,
        Frm_單次美容.price_除蚤,Frm_單次美容.price_全臉小修,Frm_單次美容.price_貴賓腳,Frm_單次美容.price_貴賓臉,Frm_單次美容.price_清耳朵,Frm_單次美容.price_手工剪};
        private void Frm_服務設定_Load(object sender, EventArgs e)
        {
            txt_香氛浴.Text = prices[0].ToString();
            txt_藥浴.Text = prices[1].ToString();
            txt_護毛.Text = prices[2].ToString();
            txt_刷牙.Text = prices[3].ToString();
            txt_剪指甲.Text = prices[4].ToString(); ;
            txt_清潔肛門.Text = prices[5].ToString(); ;

            txt_除蚤.Text = prices[6].ToString();
            txt_全臉小修.Text = prices[7].ToString();
            txt_貴賓腳.Text = prices[8].ToString();
            txt_貴賓臉.Text = prices[9].ToString();
            txt_清耳朵.Text = prices[10].ToString();
            txt_手工剪.Text = prices[11].ToString();
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            string[] txt_prices =
                { txt_香氛浴.Text,txt_藥浴.Text, txt_護毛.Text, txt_刷牙.Text, txt_剪指甲.Text, txt_清潔肛門.Text,
            txt_除蚤.Text,txt_全臉小修.Text,txt_貴賓腳.Text,txt_貴賓臉.Text,txt_清耳朵.Text,txt_手工剪.Text};

            int i = 0;
            foreach (string item in txt_prices)
            {
                int.TryParse(item, out prices[i]);
                i++;
            }
            Frm_單次美容.price_香氛浴 = prices[0];
            Frm_單次美容.price_藥浴 = prices[1];
            Frm_單次美容.price_護毛 = prices[2];
            Frm_單次美容.price_刷牙 = prices[3];
            Frm_單次美容.price_剪指甲 = prices[4];
            Frm_單次美容.price_清潔肛門 = prices[5];

            Frm_單次美容.price_除蚤 = prices[6];
            Frm_單次美容.price_全臉小修 = prices[7];
            Frm_單次美容.price_貴賓腳 = prices[8];
            Frm_單次美容.price_貴賓臉 = prices[9];
            Frm_單次美容.price_清耳朵 = prices[10];
            Frm_單次美容.price_手工剪 = prices[11];
            this.Close();
        }
    }
}
