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
    public partial class Frm_ALLMember : Form
    {
        public Frm_ALLMember()
        {
            InitializeComponent();

        }
        SqlConnectionStringBuilder scsb;
        string myDBConnectionString = "";
        private void Frm_ALLMember_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @".";
            scsb.InitialCatalog = "mydb";
            scsb.IntegratedSecurity = true;
            myDBConnectionString = scsb.ToString();

            SqlConnection con = new SqlConnection(myDBConnectionString);
            con.Open();
            string strSQL = "select 寵名,主人,手機,家電,品種,公母,地址,剪毛,洗澡 from member;";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) //Read是一筆一筆讀資料，讓你放在reader裡可以一筆一筆讀取，hasrow只是偵測有無資料
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 13, FontStyle.Bold);
           
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(235,214,214);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(0,0,0);
            dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 12);

            dataGridView1.GridColor = Color.Black;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;


            reader.Close();
            con.Close();
        }

    }
}
