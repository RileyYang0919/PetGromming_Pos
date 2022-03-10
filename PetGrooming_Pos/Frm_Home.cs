using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PetGrooming_Pos
{
    public partial class Frm_Home : Form
    {
        public Frm_Home()
        {
            InitializeComponent();
        }
        string myDBConnectionString = "";
        List<int> searchIDs = new List<int>();
        List<int> listPid = new List<int>();
        List<string> listPName = new List<string>();
        List<string> listOName = new List<string>();
        List<string> list_服務內容 = new List<string>();
        List<int> list_價錢 = new List<int>();
        public static bool isAddOrder = false;
        private void Frm_Home_Load(object sender, EventArgs e)
        {
            //時間
            DateTime time = DateTime.Now;
            lbl_time.Text = time.ToString("yyyy-MM-dd HH:mm:ss dddd");

            //連線資料庫
            myDBConnectionString = ConfigurationManager.ConnectionStrings
                ["PetGrooming_Pos.Properties.Settings.mydbConnectionString"].ConnectionString;

            //搜尋欄位checkbox添加item
            cbx_欄位.Items.Add("編號");
            cbx_欄位.Items.Add("寵名");
            cbx_欄位.Items.Add("主人");
            cbx_欄位.Items.Add("手機");
            cbx_欄位.Items.Add("地址");


            //先添加ListView服物的欄位
            listView_服務.View = View.Details;

            listView_服務.Columns.Add("訂單編號", 80);
            listView_服務.Columns.Add("會員編號", 80);
            listView_服務.Columns.Add("寵名", 80);
            listView_服務.Columns.Add("主人", 80);
            listView_服務.Columns.Add("時間", 80);
            listView_服務.Columns.Add("服務項目", 200);
            listView_服務.Columns.Add("小計", 100);
            listView_服務.Columns.Add("付款方式", 80);
            listView_服務.Columns.Add("付款狀態", 80);
            listView_服務.FullRowSelect = true;

        }

        private void btn_搜尋_Click(object sender, EventArgs e)
        {
            string msg;
            if (cbx_欄位.SelectedItem == null)
            {
                lbl_搜尋結果.Text = "請選取搜尋欄位";
            }
            else
            {
                //每次搜尋清空
                listView_搜尋結果.Items.Clear();
                listView_搜尋結果.Columns.Clear();
                listPid.Clear();
                listPName.Clear();
                listOName.Clear();
                searchIDs.Clear();

                string str欄位名稱 = cbx_欄位.SelectedItem.ToString();
                string str搜尋字串 = txt_搜尋關鍵字.Text;

                if (str搜尋字串 != "")
                {
                    int i = 0;

                    SqlConnection con = new SqlConnection(myDBConnectionString);
                    con.Open();
                    string strSQL = $"select * from member where {str欄位名稱} like @SearchString";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchString", "%" + str搜尋字串 + "%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        searchIDs.Add(Convert.ToInt32(reader["編號"]));
                        listPid.Add(Convert.ToInt32(reader["編號"]));
                        listPName.Add(Convert.ToString(reader["寵名"]));
                        listOName.Add(Convert.ToString(reader["主人"]));
                        i++;
                    }
                    listView_搜尋結果.View = View.Details;
                    listView_搜尋結果.Columns.Add("編號", 100);
                    listView_搜尋結果.Columns.Add("寵名", 100);
                    listView_搜尋結果.Columns.Add("主人", 100);
                    listView_搜尋結果.FullRowSelect = true;

                    for (int k = 0; k < listPid.Count; k++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                        item.Tag = listPid[k];
                        item.Text = listPid[k].ToString();
                        item.SubItems.Add(listPName[k]);
                        item.SubItems.Add(listOName[k]);

                        listView_搜尋結果.Items.Add(item);
                    }

                    if (i <= 0)
                        msg = $"共{i}筆數";
                    else
                        msg = $"共{i}筆數";
                    reader.Close();
                    con.Close();
                }
                else
                    msg = "請輸入搜尋關鍵字";

                lbl_搜尋結果.Text = msg;
            }
        }

        private void listView_搜尋結果_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_搜尋結果.SelectedItems != null)
            {
                int intId = searchIDs[listView_搜尋結果.FocusedItem.Index];

                SqlConnection con = new SqlConnection(myDBConnectionString);
                con.Open();
                string strSQL = $"select * from member where 編號 = @SearchId";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchId", intId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_編號.Text = $"{reader["編號"]}";
                    txt_主人.Text = $"{reader["主人"]}";
                    txt_寵名.Text = $"{reader["寵名"]}";
                    txt_手機.Text = $"{reader["手機"]}";
                    txt_家電.Text = $"{reader["家電"]}";
                    txt_地址.Text = $"{reader["地址"]}";
                    txt_洗澡.Text = $"{reader["洗澡"]}";
                    txt_剪毛.Text = $"{reader["剪毛"]}";
                    cbx_品種.SelectedItem = $"{reader["品種"]}";
                    cbx_公母.SelectedItem = $"{reader["公母"]}";
                }
                reader.Close();
                con.Close();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            lbl_time.Text = time.ToString("yyyy-MM-dd HH:mm:ss dddd");
        }

        private void btn_刪除會員_Click(object sender, EventArgs e)
        {
            if (txt_編號.Text != "")
            {
                DialogResult dialog_del = MessageBox.Show("您確定要將此筆會員資料刪除？", "會員刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog_del == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(myDBConnectionString);
                    con.Open();
                    string strSQL = $"delete from member where 編號 = @SearchId";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@SearchId", txt_編號.Text);

                    int rows = cmd.ExecuteNonQuery();
                    con.Close();

                    txt_編號.Text = "";
                    txt_寵名.Text = "";
                    txt_家電.Text = "";
                    txt_主人.Text = "";
                    txt_手機.Text = "";
                    txt_地址.Text = "";
                    txt_剪毛.Text = "";
                    txt_洗澡.Text = "";
                    cbx_公母.SelectedItem = null;
                    cbx_品種.SelectedItem = null;
                    listView_搜尋結果.Items.Clear();

                    MessageBox.Show($"{rows}筆資料刪除成功！");
                }
            }
            else
                MessageBox.Show("請先使用搜尋功能並選取會員！", "未選取會員", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btn_新增會員_Click(object sender, EventArgs e)
        {
            Frm_member frm_member = new Frm_member();
            frm_member.ShowDialog();
        }


        private void listView_搜尋結果_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Frm_member frm_member = new Frm_member();
            frm_member.pid = Convert.ToInt32(listView_搜尋結果.SelectedItems[0].Tag);
            frm_member.ShowDialog();
        }

        private void btn_顯示全部會員列表_Click(object sender, EventArgs e)
        {
            Frm_ALLMember frm_all = new Frm_ALLMember();
            frm_all.ShowDialog();
        }

        private void btn_單次美容_Click(object sender, EventArgs e)
        {
            if (txt_編號.Text != "")
            {
                Frm_單次美容 frm_單次美容 = new Frm_單次美容();
                frm_單次美容.pid2 = Convert.ToInt32(listView_搜尋結果.SelectedItems[0].Tag);
                frm_單次美容.ShowDialog();

            }
            else
                MessageBox.Show("請先使用搜尋功能並選取會員！", "未選取會員", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        int subPrice = 0;
        string subService = "";
        public void 訂單狀態更新()
        {
            myDBConnectionString = ConfigurationManager.ConnectionStrings
                ["PetGrooming_Pos.Properties.Settings.mydbConnectionString"].ConnectionString;

            subPrice = 0;
            subService = "";
            list_服務內容.Clear();
            list_價錢.Clear();

            SqlDataReader reader1 = executeCmd_read_主服務();
            SqlDataReader reader2 = executeCmd_read_附服務();

            ListViewItem item = new ListViewItem();

            while (reader1.Read())
            {
                item.Font = new Font("微軟正黑體", 12, FontStyle.Regular);
                item.Text = $"{ reader1["訂單編號"]}".ToString();
                item.SubItems.Add($"{reader1["會員編號"]}".ToString());
                item.SubItems.Add($"{reader1["寵名"]}");
                item.SubItems.Add($"{reader1["主人"]}");
                item.SubItems.Add(Convert.ToDateTime($"{reader1["訂單時間"]}").ToString("HH:mm"));

                //主服務內容價錢讀取
                if (Convert.ToBoolean(reader1["洗澡"]))
                    list_服務內容.Add("洗澡");
                if (Convert.ToBoolean(reader1["剪毛"]))
                    list_服務內容.Add("剪毛");
                list_價錢.Add(Convert.ToInt32(reader1["價錢"]));

                //附服務內容價錢讀取
                while (reader2.Read())
                {
                    list_服務內容.Add(reader2["服務內容"].ToString());
                    list_價錢.Add(Convert.ToInt32(reader2["價錢"]));
                }

                foreach (string service in list_服務內容)
                {
                    subService += service + "/";
                }
                item.SubItems.Add(subService);

                foreach (int price in list_價錢)
                {
                    subPrice += price;
                }
                item.SubItems.Add(subPrice.ToString());
                item.SubItems.Add(""); //付款方式
                item.SubItems.Add("未付");
            }

            listView_服務.Items.Add(item);
            reader1.Close();
            reader2.Close();

        }

        private SqlDataReader executeCmd_read_主服務()
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.Connection = con;
            SqlCommand.CommandText =
                "select top 1 [order].*,[once_service].[洗澡],[once_service].[剪毛],once_service.[價錢],member.[寵名],member.[主人] " +
                "from [order] " +
                "join [once_service] on [order].[單次服務編號] = [once_service].[單次美容編號] " +
                "join member on member.編號 = [order].[會員編號]" +
                "order by [order].[訂單編號] desc;";

            SqlDataReader reader = SqlCommand.ExecuteReader();
            return reader;
        }

        private SqlDataReader executeCmd_read_附服務()
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.Connection = con;
            SqlCommand.CommandText =
                "select [服務內容],[價錢] " +
                "from addi_service " +
                "join [order] " +
                "on [order].[訂單編號] = addi_service.[訂單編號] " +
                "where [order].[訂單編號] = (select top 1 [order].[訂單編號] from [order] order by [order].[訂單編號] desc) ";
            SqlDataReader reader = SqlCommand.ExecuteReader();
            return reader;
        }



        private void Frm_Home_Activated(object sender, EventArgs e)
        {
            if (isAddOrder)
            {
                訂單狀態更新();
                isAddOrder = false;
            }
            if (isCash || isCredit)
            {
                btn_付款.Enabled = true;
            }
        }
        private void btn_刪除訂單_Click(object sender, EventArgs e)
        {
            if (listView_服務.SelectedItems.Count != 0)
            {
                if(listView_服務.SelectedItems[0].SubItems[8].Text == "未付")
                {
                    //先找單次服務的編號
                    int once_number = 0;
                    SqlConnection con = new SqlConnection(myDBConnectionString);
                    con.Open();
                    string strSQL = "select [單次服務編號] from [order] where [訂單編號] = @which訂單編號";
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    cmd.Parameters.AddWithValue("@which訂單編號", listView_服務.SelectedItems[0].SubItems[0].Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        once_number = Convert.ToInt32($"{ reader["單次服務編號"]}");
                    }
                    con.Close();
                    //才能一次刪除散佈在三個資料表的資料
                    DialogResult dialog_del = MessageBox.Show("您確定要將此筆訂單資料刪除？", "訂單刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog_del == DialogResult.Yes)
                    {
                        con.Open();
                        strSQL = $"delete from addi_service where [訂單編號] = @which訂單編號;" +
                            "delete from [order] where [訂單編號] = @which訂單編號;" +
                            "delete from once_service where [單次美容編號] = @which單次服務編號;";
                        cmd = new SqlCommand(strSQL, con);
                        cmd.Parameters.AddWithValue("@which訂單編號", listView_服務.SelectedItems[0].SubItems[0].Text);
                        cmd.Parameters.AddWithValue("@which單次服務編號", once_number);
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show($"1筆訂單刪除成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        listView_服務.SelectedItems[0].Remove();
                    }
                }
                else
                    MessageBox.Show("已結清之訂單無法被刪除", "刪除失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("請先選取預刪除的訂單", "訂單刪除失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool isCash = false;
        public static bool isCredit = false;
        private void btn_付款方式_Click_1(object sender, EventArgs e)         //付款方式
        {
            isCash = false;
            isCredit = false;
            Frm_付款方式 pay = new Frm_付款方式();
            pay.ShowDialog();
        }
        private void btn_付款方式_Click(object sender, EventArgs e) //付款按鈕
        {
            if(listView_服務.SelectedItems.Count > 0 && listView_服務.SelectedItems.Count < 2)
            {
                if (listView_服務.SelectedItems[0].SubItems[8].Text == "未付")
                {
                    SqlConnection con = new SqlConnection(myDBConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update [order] set [付款狀態] = 'true' where [訂單編號] = @which訂單編號", con);
                    cmd.Parameters.AddWithValue("@which訂單編號", listView_服務.SelectedItems[0].Text);
                    cmd.ExecuteNonQuery();
                    listView_服務.SelectedItems[0].SubItems[8].Text = "已結清";
                    if (isCash)
                        listView_服務.SelectedItems[0].SubItems[7].Text = "現金";
                    else if (isCredit)
                        listView_服務.SelectedItems[0].SubItems[7].Text = "信用卡";
                    MessageBox.Show("結清成功！");
                    
                }
                else
                    MessageBox.Show("此筆訂單已結清。");
            }
            else
                MessageBox.Show("請選擇預付款的訂單項目,\r\n" +
                    "一次僅能選擇一筆。");

            //付款完按鈕要關閉
            btn_付款.Enabled = false;
            isCash = false;
            isCredit = false;
        }

        private void btn_系統結帳_Click(object sender, EventArgs e)
        {
            DialogResult done = MessageBox.Show("是否進行系統結帳\r\n系統結帳後將移除所有訂單", "系統結帳確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (listView_服務.Items.Count != 0)
            {
                if (done == DialogResult.Yes)
                {
                    string strResult = "";
                    int amt = 0;
                    int count_未付 = 0;
                    string str_未付項目 = "寵名\t主人\t小計\r\n==================\r\n";
                    int total_未付 = 0;
                    for (int i = 0; i < listView_服務.Items.Count; i++)
                    {
                        if (listView_服務.Items[i].SubItems[8].Text == "未付")
                        {
                            count_未付++;
                            str_未付項目 += $"{listView_服務.Items[i].SubItems[2].Text}\t{listView_服務.Items[i].SubItems[3].Text}\t{listView_服務.Items[i].SubItems[6].Text}\r\n";
                            total_未付 += int.Parse(listView_服務.Items[i].SubItems[6].Text);
                        }
                        amt += int.Parse(listView_服務.Items[i].SubItems[6].Text);
                    }
                    strResult += $"今日訂單總數為{listView_服務.Items.Count}筆\t總金額為{amt.ToString("n0")}元\r\n\r\n未結清之訂單如下:\r\n{str_未付項目}" +
                        $"\r\n未結清訂單總數為{count_未付}筆\t總金額為{total_未付.ToString("n0")}元";

                    MessageBox.Show(strResult);
                    listView_服務.Items.Clear();
                }
            }
            else
                MessageBox.Show("目前尚未有任何訂單", "系統結帳失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btn_退出程式_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("是否退出程式\r\n退出程式後將遺失所有訂單\r\n而無法進行系統結帳", "退出程式確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(exit == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_服務設定_Click(object sender, EventArgs e)
        {
            Frm_服務設定 Priceset = new Frm_服務設定();
            Priceset.ShowDialog();
        }
    }
}
