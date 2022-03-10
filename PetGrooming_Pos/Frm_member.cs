using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetGrooming_Pos
{
    public partial class Frm_member : Form
    {
        public Frm_member()
        {
            InitializeComponent();
        }
        bool is已修改圖檔 = false;
        public int pid = 0;
        string image_dir = @"images\";
        string image_name = "";

        SqlConnectionStringBuilder scsb;
        string myDBConnectionString = "";

        private void Frm_member_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "mydb";
            scsb.IntegratedSecurity = true;
            myDBConnectionString = scsb.ToString();

            if (pid <= 0)
            {//新增模式
                btn_清空欄位.Enabled = true;
                btn_選擇毛小孩照片_新增.Enabled = true;
                btn_儲存會員.Enabled = true;

                btn_回復欄位.Enabled = false;
                btn_選擇毛小孩照片_修改.Enabled = false;
                btn_儲存修改.Enabled = false;
            }
            else
            {//修改模式
                btn_清空欄位.Enabled = false;
                btn_選擇毛小孩照片_新增.Enabled = false;
                btn_儲存會員.Enabled = false;

                btn_回復欄位.Enabled = true;
                btn_選擇毛小孩照片_修改.Enabled = true;
                btn_儲存修改.Enabled = true;
                show_frmMember();
            }
        }

        void show_frmMember()
        {
            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            string strSQL = $"select * from member where 編號 = @SearchID";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@SearchID", pid);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
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
                image_name = $"{reader["照片"]}";
                if (image_name != "")
                {
                    pictureBox_照片.Image = Image.FromFile(image_dir + image_name); //還沒做完
                }
                else
                {
                    pictureBox_照片.Image = Image.FromFile(image_dir + "notFound404.jpg");
                }
                i++;
            }
            con.Close();
        }

        //關於新增的按紐
        private void btn_清空欄位_Click(object sender, EventArgs e)
        {
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
        }

        private void btn_選擇毛小孩照片_新增_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "圖檔類型 (*. jpg, *.jpeg, *.png) | *. jpg; *.jpeg; *.png"; // |的左邊是描述，右邊是規則。
            DialogResult R = f.ShowDialog();

            if (R == DialogResult.OK)
            {
                pictureBox_照片.Image = Image.FromFile(f.FileName);
                string fileExt = Path.GetExtension(f.SafeFileName);
                Random myRnd = new Random();
                image_name = DateTime.Now.ToString("yyyyMMddHHmmss") + myRnd.Next(10, 99).ToString() + fileExt;
                is已修改圖檔 = true;
            }
        }

        private void btn_儲存會員_Click(object sender, EventArgs e)
        {
            if (txt_主人.Text != "" && txt_寵名.Text != "" && txt_手機.Text != "" && txt_家電.Text != "" && txt_地址.Text != ""
                && txt_剪毛.Text != "" && txt_洗澡.Text != "" && cbx_公母.SelectedItem != null && cbx_品種.SelectedItem != null)
            {
                if (is已修改圖檔 == true)
                {
                    pictureBox_照片.Image.Save(image_dir + image_name);
                    is已修改圖檔 = false;
                }

                SqlConnection con = new SqlConnection(myDBConnectionString);

                con.Open();
                string strSQL;
                if (pictureBox_照片.Image != null)
                {//有放照片
                    strSQL = "insert into member(寵名,主人,手機,家電,品種,公母,地址,剪毛,洗澡,照片)" +
                        "values(@New寵名,@New主人,@New手機,@New家電,@New品種,@New公母,@New地址,@New剪毛,@New洗澡,@New照片);";

                }
                else
                {//沒放照片
                    strSQL = "insert into member(寵名,主人,手機,家電,品種,公母,地址,剪毛,洗澡) " +
                        "values(@New寵名,@New主人,@New手機,@New家電,@New品種,@New公母,@New地址,@New剪毛,@New洗澡);";
                }
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@New寵名", txt_寵名.Text);
                cmd.Parameters.AddWithValue("@New主人", txt_主人.Text);
                cmd.Parameters.AddWithValue("@New手機", txt_手機.Text);
                cmd.Parameters.AddWithValue("@New家電", txt_家電.Text);
                cmd.Parameters.AddWithValue("@New品種", cbx_品種.SelectedItem);
                cmd.Parameters.AddWithValue("@New公母", cbx_公母.SelectedItem);
                cmd.Parameters.AddWithValue("@New地址", txt_地址.Text);
                cmd.Parameters.AddWithValue("@New照片", image_name);

                int CutPrice;
                int BathPrice;
                Int32.TryParse(txt_剪毛.Text, out CutPrice);
                Int32.TryParse(txt_洗澡.Text, out BathPrice);
                cmd.Parameters.AddWithValue("@New剪毛", CutPrice);
                cmd.Parameters.AddWithValue("@New洗澡", BathPrice);

                int rows = cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show($"{rows}筆會員新增成功！");
            }
            else
            {
                MessageBox.Show("所有欄位必填");
            }
        }

        //關於修改的按鈕
        private void btn_回復欄位_Click(object sender, EventArgs e)
        {
            show_frmMember();
        }

        private void btn_儲存修改_Click(object sender, EventArgs e)
        {
            if (txt_主人.Text != "" && txt_寵名.Text != "" && txt_手機.Text != "" && txt_家電.Text != "" && txt_地址.Text != ""
                && txt_剪毛.Text != "" && txt_洗澡.Text != "" && cbx_公母.SelectedItem != null && cbx_品種.SelectedItem != null)
            {
                if (is已修改圖檔 == true)
                {
                    pictureBox_照片.Image.Save(image_dir + image_name);
                    is已修改圖檔 = false;
                }

                SqlConnection con = new SqlConnection(myDBConnectionString);
                con.Open();

                string strSQL;

                if (pictureBox_照片.Image != null)
                {//有放照片
                    strSQL = "update member set 寵名 = @New寵名, 主人 = @New主人, 手機 = @New手機, 家電 = @New家電, 品種 = @New品種," +
                        "公母 = @New公母, 地址 = @New地址, 剪毛 = @New剪毛, 洗澡 = @New洗澡, 照片 = @New照片 where 編號 = @SearchID";
                }
                else
                {//沒放照片
                    strSQL = "update member set 寵名 = @New寵名, 主人 = @New主人, 手機 = @New手機, 家電 = @New家電, 品種 = @New品種," +
                        "公母 = @New公母, 地址 = @New地址, 剪毛 = @New剪毛, 洗澡 = @New洗澡 where 編號 = @SearchID";
                }

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchID", pid);
                cmd.Parameters.AddWithValue("@New寵名", txt_寵名.Text);
                cmd.Parameters.AddWithValue("@New主人", txt_主人.Text);
                cmd.Parameters.AddWithValue("@New手機", txt_手機.Text);
                cmd.Parameters.AddWithValue("@New家電", txt_家電.Text);
                cmd.Parameters.AddWithValue("@New品種", cbx_品種.SelectedItem);
                cmd.Parameters.AddWithValue("@New公母", cbx_公母.SelectedItem);
                cmd.Parameters.AddWithValue("@New地址", txt_地址.Text);
                cmd.Parameters.AddWithValue("@New照片", image_name);

                int CutPrice;
                int BathPrice;
                Int32.TryParse(txt_剪毛.Text, out CutPrice);
                Int32.TryParse(txt_洗澡.Text, out BathPrice);
                cmd.Parameters.AddWithValue("@New剪毛", CutPrice);
                cmd.Parameters.AddWithValue("@New洗澡", BathPrice);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
               
                MessageBox.Show($"{rows}會員資料修改成功！");
            }
            else
            {
                MessageBox.Show("所有欄位必填");
            }
        }
    }
}
