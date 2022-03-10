using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetGrooming_Pos
{
    public partial class Frm_單次美容 : Form
    {
        public Frm_單次美容()
        {
            InitializeComponent();
        }
        SqlConnectionStringBuilder scsb;
        string myDBConnectionString = "";

        public int pid2;
        //判斷按鈕
        bool isBath = false;
        bool isCut = false;
        bool isAddi = false;

        //主項目價格
        int price_洗澡;
        int price_剪毛;

        //附項目價格
        public static int price_香氛浴 = 150;
        public static int price_藥浴 = 200;
        public static int price_護毛 = 100;
        public static int price_刷牙 = 150;
        public static int price_剪指甲 = 50;
        public static int price_清潔肛門 = 50;

        public static int price_除蚤 = 50;
        public static int price_全臉小修 = 150;
        public static int price_貴賓腳 = 150;
        public static int price_貴賓臉 = 150;
        public static int price_清耳朵 = 50;
        public static int price_手工剪 = 300;

        int price; //單個項目的暫存位置
        int total_price;
        int k = 0; //次數超過7打開附項目的捲軸
        void show_frmMember()
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            string strSQL = $"select * from member where 編號 = @SearchID";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@SearchID", pid2);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                txt_編號.Text = $"{reader["編號"]}";
                txt_寵名.Text = $"{reader["寵名"]}";
                txt_品種.Text = $"{reader["品種"]}";
                price_洗澡 = int.Parse($"{reader["洗澡"]}");
                price_剪毛 = int.Parse($"{reader["剪毛"]}");
                i++;
            }
        }
        private void Frm_單次美容_Load(object sender, EventArgs e)
        {
            //連線資料庫
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "mydb";
            scsb.IntegratedSecurity = true;
            myDBConnectionString = scsb.ToString();

            show_frmMember();
        }

        private void btn_洗澡_Click(object sender, EventArgs e)
        {
            isBath = true;
            if (isCut)
            {
                txt_主項目.Text += $"洗澡\t{price_洗澡}";
                btn_洗澡.Enabled = false;
            }
            else if (!isCut)
            {
                txt_主項目.Text = $"主項目\r\n==============\r\n洗澡\t{price_洗澡}\r\n";
                btn_洗澡.Enabled = false;
            }
            total_price += price_洗澡;
            lbl_total.Text = $"總金額:　　{total_price}";
        }

        private void btn_剪毛_Click(object sender, EventArgs e)
        {
            isCut = true;
            if (isBath)
            {
                txt_主項目.Text += $"剪毛\t{price_剪毛}";
                btn_剪毛.Enabled = false;
            }
            else if (!isBath)
            {
                txt_主項目.Text = $"主項目\r\n==============\r\n剪毛\t{price_剪毛}\r\n";
                btn_剪毛.Enabled = false;
            }
            total_price += price_剪毛;
            lbl_total.Text = $"總金額:　　{total_price}";
        }

        private void btn_香氛浴_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            string BtnName = btn.Text;
            if (!isAddi)
            {
                isAddi = true;
                switch (BtnName)
                {
                    case "香氛浴":
                        price = price_香氛浴;
                        break;
                    case "藥浴":
                        price = price_藥浴;
                        break;
                    case "護毛":
                        price = price_護毛;
                        break;
                    case "刷牙":
                        price = price_刷牙;
                        break;
                    case "剪指甲":
                        price = price_剪指甲;
                        break;
                    case "清潔肛門":
                        price = price_清潔肛門;
                        break;
                    case "除蚤":
                        price = price_除蚤;
                        break;
                    case "全臉小修":
                        price = price_全臉小修;
                        break;
                    case "貴賓腳":
                        price = price_貴賓腳;
                        break;
                    case "貴賓臉":
                        price = price_貴賓臉;
                        break;
                    case "清耳朵":
                        price = price_清耳朵;
                        break;
                    case "手工剪":
                        price = price_手工剪;
                        break;
                }
                total_price += price;
                txt_附項目.Text += $"附項目\r\n==============\r\n{btn.Text}\t{price}\r\n";
                lbl_total.Text = $"總金額:　　{total_price}";

            }
            else
            {
                switch (BtnName)
                {
                    case "香氛浴":
                        price = price_香氛浴;
                        break;
                    case "藥浴":
                        price = price_藥浴;
                        break;
                    case "護毛":
                        price = price_護毛;
                        break;
                    case "刷牙":
                        price = price_刷牙;
                        break;
                    case "剪指甲":
                        price = price_剪指甲;
                        break;
                    case "清潔肛門":
                        price = price_清潔肛門;
                        break;
                    case "除蚤":
                        price = price_除蚤;
                        break;
                    case "全臉小修":
                        price = price_全臉小修;
                        break;
                    case "貴賓腳":
                        price = price_貴賓腳;
                        break;
                    case "貴賓臉":
                        price = price_貴賓臉;
                        break;
                    case "清耳朵":
                        price = price_清耳朵;
                        break;
                    case "手工剪":
                        price = price_手工剪;
                        break;
                }
                total_price += price;
                txt_附項目.Text += $"{btn.Text}\t{price}\r\n";
                lbl_total.Text = $"總金額:　　{total_price}";
            }
            k++;
            if (k > 7)
            {
                txt_附項目.ScrollBars = ScrollBars.Vertical;
            }
        }

        private void btn_清除_Click(object sender, EventArgs e)
        {
            //清單清空
            txt_主項目.Text = "";
            txt_附項目.Text = "";

            lbl_total.Text = "總金額:";

            isBath = false;
            isCut = false;
            isAddi = false;
            total_price = 0;
            k = 0;

            //按鈕打開
            btn_洗澡.Enabled = true;
            btn_剪毛.Enabled = true;

            btn_香氛浴.Enabled = true;
            btn_藥浴.Enabled = true;
            btn_護毛.Enabled = true;
            btn_刷牙.Enabled = true; 
            btn_剪指甲.Enabled = true;
            btn_清潔肛門.Enabled = true; 
            btn_除蚤.Enabled = true;
            btn_全臉小修.Enabled = true; 
            btn_貴賓腳.Enabled = true;
            btn_貴賓臉.Enabled = true; 
            btn_清耳朵.Enabled = true;
            btn_手工剪.Enabled = true;
        }

        int top1_單次美容編號 = 0;
        int top1_訂單編號 = 0;
        private void btn_確認_Click(object sender, EventArgs e)
        {
            if(btn_洗澡.Enabled && btn_剪毛.Enabled &&
                btn_香氛浴.Enabled && btn_藥浴.Enabled && btn_護毛.Enabled && btn_刷牙.Enabled && btn_剪指甲.Enabled && btn_清潔肛門.Enabled && 
                btn_除蚤.Enabled && btn_全臉小修.Enabled && btn_貴賓腳.Enabled && btn_貴賓臉.Enabled && btn_清耳朵.Enabled && btn_手工剪.Enabled)
            {
                MessageBox.Show("請先新增服務項目","訂單新增失敗",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                單次美容資料表新增();
                尋找單次美容編號();
                訂單資料表新增();
                尋找訂單編號();
                附項目資料表新增();
                MessageBox.Show("訂單已新增成功！請至主頁確認。", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Frm_Home.isAddOrder = true;
                this.Close();
            }
        }

        private void executeCmd(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private SqlDataReader executeCmd_read(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        private void 單次美容資料表新增() 
        {
            int price_主 = 0;
            if (isBath && isCut)
                price_主 = price_洗澡 + price_剪毛;
            else if (!isBath && isCut)
                price_主 = price_剪毛;
            else if (isBath && !isCut)
                price_主 = price_洗澡;

            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = $"insert into once_service(剪毛,洗澡,價錢) values(@New剪毛bit,@New洗澡bit,@New主項價格)";
            SqlCommand.Parameters.AddWithValue("@New剪毛bit", isCut);
            SqlCommand.Parameters.AddWithValue("@New洗澡bit", isBath);
            SqlCommand.Parameters.AddWithValue("@New主項價格", price_主);
            executeCmd(SqlCommand);
        }
        private void 尋找單次美容編號()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "select top 1 [單次美容編號] from once_service order by [單次美容編號] desc";
            SqlDataReader reader = executeCmd_read(SqlCommand);
            while (reader.Read())
            {
                top1_單次美容編號 = int.Parse($"{reader["單次美容編號"]}");
            }
        }
        private void 訂單資料表新增()
        {
            DateTime time = DateTime.Now;
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "insert into [order](會員編號,訂單時間,單次服務編號,付款狀態) values(@New會員編號,@New訂單時間,@New單次服務編號,@New付款狀態)";
            SqlCommand.Parameters.AddWithValue("@New會員編號", pid2);
            SqlCommand.Parameters.AddWithValue("@New訂單時間", time.ToString("yyyy-MM-dd HH:mm:ss"));
            SqlCommand.Parameters.AddWithValue("@New單次服務編號", top1_單次美容編號);
            SqlCommand.Parameters.AddWithValue("@New付款狀態", false);
            executeCmd(SqlCommand);
        }
        private void 尋找訂單編號()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "select top 1 [訂單編號] from [order] order by [訂單編號] desc";
            SqlDataReader reader = executeCmd_read(SqlCommand);
            while (reader.Read())
            {
                top1_訂單編號 = int.Parse($"{reader["訂單編號"]}");
            }
            reader.Close();
        }
        private void 附項目資料表新增()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = $"insert into addi_service values(@New訂單編號,@New價錢,@New服務內容)";

            Button[] btn = { btn_香氛浴, btn_藥浴, btn_護毛, btn_刷牙, btn_剪指甲, btn_清潔肛門, btn_除蚤, btn_全臉小修, btn_貴賓腳, btn_貴賓臉, btn_清耳朵, btn_手工剪 };
            int[] price_單個 = { price_香氛浴, price_藥浴, price_護毛, price_刷牙, price_剪指甲, price_清潔肛門, price_除蚤, price_全臉小修, price_貴賓腳, price_貴賓臉, price_清耳朵, price_手工剪 };
            int j = 0;
            foreach (Button item in btn)
            {
                if (item.Enabled == false)
                {
                    SqlCommand.Parameters.AddWithValue("@New訂單編號", top1_訂單編號);
                    SqlCommand.Parameters.AddWithValue("@New價錢", price_單個[j]);
                    SqlCommand.Parameters.AddWithValue("@New服務內容", item.Text);
                    executeCmd(SqlCommand); 
                    SqlCommand.Parameters.Clear();
                }
                j++;
            }
        }
    }
}
