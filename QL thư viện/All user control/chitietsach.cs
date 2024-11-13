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

namespace QL_thư_viện.All_user_control
{
    public partial class chitietsach : Form
    {
        public chitietsach()
        {
            InitializeComponent();
        }

        private bool addnewFlag = false;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        
        public void LoadBookDetails(DataGridViewRow row)
        {
            txtMaSach.Text = row.Cells["Ma_Sach"].Value.ToString();
            txtTenDauSach.Text = row.Cells["Ten_Dau_Sach"].Value.ToString();
            txtTenTacGia.Text = row.Cells["Ten_Tac_Gia"].Value.ToString();
            txtNamXuatBan.Text = row.Cells["Nam_Xuat_Ban"].Value.ToString();
            txtGiaBia.Text = row.Cells["Gia_Bia"].Value.ToString();
            txtTinhTrang.Text = row.Cells["Ten_Tinh_Trang"].Value.ToString();
          
            txtTheLoai.Text = row.Cells["Ten_The_Loai"].Value.ToString();
            txtChuDe.Text = row.Cells["Ten_Chu_De"].Value.ToString();
            txtNXB.Text = row.Cells["Ten_NXB"].Value.ToString();
            txtKho.Text = row.Cells["Ten_Kho"].Value.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select Ten_Dau_Sach from View_DauSach_Details";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            comTenDauSach.DataSource = dt;
            comTenDauSach.DisplayMember = "Ten_Dau_Sach";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void conTenDauSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            


        }
    }
}
