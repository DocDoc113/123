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
    public partial class Quản_lý_sách : UserControl
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        DataView dv;
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        public Quản_lý_sách()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    da = new SqlDataAdapter("SELECT* FROM ViewThongTinSach ORDER BY Ma_Sach", conn);
                    dt.Clear();
                    da.Fill(dt);
                    dgvSach.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        // DoSQL
        void DoSQL(string sql)
        {
            using (conn = new SqlConnection(strCon))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Themsach themsach = new Themsach();
            themsach.Show();
        }

        private void Quản_lý_sách_Load(object sender, EventArgs e)
        {
            constr = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "select* from ViewThongTinSach ORDER BY Ma_Sach;";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);    
            dv  = new DataView(dt);
            dgvSach.DataSource = dv;
            dgvSach.Refresh();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            TacGia theloai = new TacGia();
            theloai.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Chude chude = new Chude();
            chude.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            NXB nXB = new NXB();
            nXB.Show();

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

            if (dgvSach.SelectedRows.Count > 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dgvSach.SelectedRows[0]; // Lấy hàng được chọn

                // Khởi tạo form chi tiết và truyền dữ liệu
                chitietsach cts = new chitietsach();
                cts.LoadBookDetails(selectedRow); // Gọi phương thức để hiển thị chi tiết

                cts.ShowDialog(); // Hiển thị form chi tiết dưới dạng dialog
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đầu sách để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void guna2Button8_Click(object sender, EventArgs e)
        {

            // Khởi tạo form chi tiết và truyền dữ liệu
            ThemChiTiet tct = new ThemChiTiet();

            tct.ShowDialog(); // Hiển thị form chi tiết dưới dạng dialog

            LoadData();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

            if (dgvSach.SelectedRows.Count > 0) // Kiểm tra xem có hàng nào được chọn không
            {
                DataGridViewRow selectedRow = dgvSach.SelectedRows[0]; // Lấy hàng được chọn

                // Khởi tạo form chi tiết và truyền dữ liệu
                SuaChiTiet sct = new SuaChiTiet();
                sct.LoadBookDetails(selectedRow); // Gọi phương thức để hiển thị chi tiết

                sct.ShowDialog(); // Hiển thị form chi tiết dưới dạng dialog
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đầu sách để Sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadData();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

            if (dgvSach.SelectedRows.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời?", "Xác nhận yêu cầu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int i = dgvSach.CurrentRow.Index;
                    string tMaSach = dgvSach.Rows[i].Cells["Ma_Sach"].Value.ToString();
                    string sql = $"Delete from Sach where Ma_Sach = '{tMaSach}'";
                   

                    
                    DoSQL(sql);


                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Chua chon ban ghi");
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox hoặc nơi nhập liệu
            string tuKhoa = txtTimKiem.Text;

            // Chuẩn bị câu lệnh SQL tìm kiếm
            string sql = "SELECT Ma_Sach,Ten_Dau_Sach,Nam_Xuat_Ban,Gia_Bia,Ten_NXB,Ten_Kho,Ten_Chu_De,Ten_The_Loai,Ten_Tac_Gia, Ten_Tinh_Trang FROM [dbo].[ViewThongTinSach] " +
                         "WHERE Ten_Dau_Sach LIKE @TuKhoa " +
                         "   OR Ma_Sach LIKE @TuKhoa " +
                          "   OR Nam_Xuat_Ban LIKE @TuKhoa " +
                           "   OR Gia_Bia LIKE @TuKhoa " +
                            "   OR Ten_Kho LIKE @TuKhoa " +
                             "   OR Ten_Chu_De LIKE @TuKhoa " +
                              "   OR Ten_The_Loai LIKE @TuKhoa " +
                               "   OR Ten_Tac_Gia LIKE @TuKhoa " +
                         "   OR Ten_NXB LIKE @TuKhoa" +
                         "   OR Ten_Tinh_Trang LIKE @TuKhoa";

            // Tạo và thiết lập kết nối
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Thêm tham số cho câu lệnh SQL
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                    // Sử dụng SqlDataAdapter để lấy dữ liệu
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    // Mở kết nối và điền dữ liệu vào DataTable
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();

                    // Gán dữ liệu vào DataGridView
                    dgvSach.DataSource = dt;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
