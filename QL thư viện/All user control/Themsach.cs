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
using TheArtOfDevHtmlRenderer.Adapters;

namespace QL_thư_viện.All_user_control
{
    public partial class Themsach : Form
    {
        private bool addnewFlag = false;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        public Themsach()
        {
            InitializeComponent();
        }
        public void NapCT()
        {
            if (dgvDauSach.CurrentRow != null && dgvDauSach.CurrentRow.Index >= 0)
            {

                int i = dgvDauSach.CurrentRow.Index; // Lấy chỉ số dòng hiện tại
                txtMaDauSach.Text = dgvDauSach.Rows[i].Cells["Ma_Dau_Sach"].Value.ToString();
                txtTenDauSach.Text = dgvDauSach.Rows[i].Cells["Ten_Dau_Sach"].Value.ToString();
                txtNamXuatBan.Text = dgvDauSach.Rows[i].Cells["Nam_Xuat_Ban"].Value.ToString();
                txtGiaBia.Text = dgvDauSach.Rows[i].Cells["Gia_Bia"].Value.ToString();
                txtMaNXB.Text = dgvDauSach.Rows[i].Cells["Ma_NXB"].Value.ToString();
                txtMaKho.Text = dgvDauSach.Rows[i].Cells["Ma_Kho"].Value.ToString();
                txtMaChuDe.Text = dgvDauSach.Rows[i].Cells["Ma_Chu_De"].Value.ToString();
                txtMaTheLoai.Text = dgvDauSach.Rows[i].Cells["Ma_TL"].Value.ToString();

                txtMaTacGia.Text = dgvDauSach.Rows[i].Cells["Ma_Tac_Gia"].Value.ToString();

            }
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    da = new SqlDataAdapter("SELECT Ma_Dau_Sach,Ten_Dau_Sach,Nam_Xuat_Ban,Gia_Bia,Ma_NXB,Ma_Kho,Ma_Chu_De,Ma_TL,Ma_Tac_Gia FROM View_DauSach_Details", conn);
                    dt.Clear();
                    da.Fill(dt);
                    dgvDauSach.DataSource = dt;
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNXB.Visible = true;
            comMaNXB.Visible = false;
            txtMaKho.Visible = true;
            comMaKho.Visible = false;
            txtMaChuDe.Visible = true;
            comMaChuDe.Visible = false;
            txtMaTheLoai.Visible = true;
            comMaTheLoai.Visible = false;
            txtMaTacGia.Visible = true;
            comMaTacGia.Visible = false;
            NapCT();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
          
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaDauSach.Text = "";
            txtTenDauSach.Text = "";
            txtNamXuatBan.Text = "";
            txtGiaBia.Text = "";
            txtMaNXB.Visible = false;
            comMaNXB.Visible = true;
            txtMaKho.Visible = false;
            comMaKho.Visible = true;
            txtMaChuDe.Visible = false;
            comMaChuDe.Visible = true;
            txtMaTheLoai.Visible = false;
            comMaTheLoai.Visible = true;
            txtMaTacGia.Visible = false;
            comMaTacGia.Visible = true;


            txtMaDauSach.Focus();
            addnewFlag = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu đang trong chế độ thêm mới
            if (addnewFlag)
            {
                try
                {
                    // Lấy giá trị từ các TextBox
                    string tMaDauSach = txtMaDauSach.Text;
                    string tTenDauSach = txtTenDauSach.Text;
                    string tNamXuatBan = txtNamXuatBan.Text;
                    string tGiaBia = txtGiaBia.Text;
                    string tMaNXB = comMaNXB.Text;
                    string tMaKho = comMaKho.Text;
                    string tMaChuDe = comMaChuDe.Text;
                    string tMaTheLoai = comMaTheLoai.Text;
                    string tMaTacGia = comMaTacGia.Text;

                    // Tạo câu lệnh SQL Insert để thêm mới
                    sql = "INSERT INTO DauSach (Ma_Dau_Sach, Ten_Dau_Sach,Ma_NXB, Nam_Xuat_Ban, Gia_Bia, Ma_Kho,Ma_Chu_De,Ma_TL ) " +
                        "VALUES (@Ma_Dau_Sach, @Ten_Dau_Sach,@Ma_NXB, @Nam_Xuat_Ban, @Gia_Bia, @Ma_Kho, @Ma_Chu_De, @Ma_The_Loai)";
                    string sqlTG = "INSERT INTO TacGia_DauSach (Ma_Dau_Sach, Ma_Tac_Gia) " +
                        "VALUES (@Ma_Dau_Sach,@Ma_Tac_Gia)";

                    // Sử dụng SqlConnection và SqlCommand trong using để đảm bảo giải phóng tài nguyên
                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Ma_Dau_Sach", tMaDauSach);
                            cmd.Parameters.AddWithValue("@Ten_Dau_Sach", tTenDauSach);
                            cmd.Parameters.AddWithValue("@Ma_NXB", tMaNXB);
                            cmd.Parameters.AddWithValue("@Nam_Xuat_Ban", tNamXuatBan);
                            cmd.Parameters.AddWithValue("@Gia_Bia", tGiaBia);
                            cmd.Parameters.AddWithValue("@Ma_Kho", tMaKho);
                            cmd.Parameters.AddWithValue("@Ma_Chu_De", tMaChuDe);
                            cmd.Parameters.AddWithValue("@Ma_The_Loai", tMaTheLoai);


                            // Mở kết nối và thực thi lệnh
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlTG, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Ma_Dau_Sach", tMaDauSach);
                            cmd.Parameters.AddWithValue("@Ma_Tac_Gia", tMaTacGia);


                            // Mở kết nối và thực thi lệnh
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    MessageBox.Show("Thêm mới thành công!");

                    // Cập nhật lại DataGridView để hiển thị dữ liệu mới
                    LoadData();
                    txtMaNXB.Visible = true;
                    comMaNXB.Visible = false;
                    txtMaKho.Visible = true;
                    comMaKho.Visible = false;
                    txtMaChuDe.Visible = true;
                    comMaChuDe.Visible = false;
                    txtMaTheLoai.Visible = true;
                    comMaTheLoai.Visible = false;
                    txtMaTacGia.Visible = true;
                    comMaTacGia.Visible = false;
                    // Đặt lại cờ sau khi thêm
                    addnewFlag = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bạn không ở chế độ thêm mới.");
            }
          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            

            if (addnewFlag == false)
            {
                
                {
                    string tMaDauSach = txtMaDauSach.Text;
                    string tTenDauSach = txtTenDauSach.Text;
                    string tNamXuatBan = txtNamXuatBan.Text;
                    string tGiaBia = txtGiaBia.Text;
                    string tMaNXB = comMaNXB.Text;
                    string tMaKho = comMaKho.Text;
                    string tMaChuDe = comMaChuDe.Text;
                    string tMaTheLoai = comMaTheLoai.Text;
                    string tMaTacGia = comMaTacGia.Text;
                 

                   string sql = $"UPDATE DauSach " +
                        $"set " +
                        $"Ma_Dau_Sach= N'{tMaDauSach}'," +
                        $"Ten_Dau_Sach= N'{tTenDauSach}'," +
                        $"Nam_Xuat_Ban= N'{tNamXuatBan}'," +                       
                        $"Gia_Bia= N'{tGiaBia}', " +
                        $"Ma_NXB= N'{tMaNXB}', " +                      
                        $"Ma_Kho= N'{tMaKho}', " +
                        $"Ma_Chu_De= N'{tMaChuDe}', " +
                        $"Ma_TL= N'{tMaTheLoai}' " +
                       $" Where Ma_Dau_Sach = '{tMaDauSach}'";

                    string sqlTG = $"UPDATE [dbo].[TacGia_DauSach] " +
                        $"set " +
                         $"Ma_tac_Gia= N'{tMaTacGia}', " +
                        $"Ma_Dau_Sach= N'{tMaDauSach}' " +
                       $" Where Ma_Dau_Sach = '{tMaDauSach}'";
                    
                    DoSQL(sqlTG);
                    DoSQL(sql);
                    
                }
                MessageBox.Show("Đã cập nhật thành công!");
                LoadData();
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvDauSach.SelectedRows.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời?", "Xác nhận yêu cầu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int i = dgvDauSach.CurrentRow.Index;
                    string tMaDauSach = dgvDauSach.Rows[i].Cells["Ma_Dau_Sach"].Value.ToString();
                    string sql = $"Delete from DauSach where Ma_Dau_Sach = '{tMaDauSach}'";
                    string sqlTG = $"Delete from TacGia_DauSach where Ma_Dau_Sach = '{tMaDauSach}'";
                    
                    DoSQL(sqlTG);
                    DoSQL(sql);
                    

                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Chua chon ban ghi");
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            txtMaNXB.Visible = false;
            comMaNXB.Visible = true;
            txtMaKho.Visible = false;
            comMaKho.Visible = true;
            txtMaChuDe.Visible = false;
            comMaChuDe.Visible = true;
            txtMaTheLoai.Visible = false;
            comMaTheLoai.Visible = true;
            txtMaTacGia.Visible = false;
            comMaTacGia.Visible = true;

            if (dgvDauSach.CurrentRow != null && dgvDauSach.CurrentRow.Index >= 0)
            {

                int i = dgvDauSach.CurrentRow.Index; // Lấy chỉ số dòng hiện tại
                txtMaDauSach.Text = dgvDauSach.Rows[i].Cells["Ma_Dau_Sach"].Value.ToString();
                txtTenDauSach.Text = dgvDauSach.Rows[i].Cells["Ten_Dau_Sach"].Value.ToString();
                txtNamXuatBan.Text = dgvDauSach.Rows[i].Cells["Nam_Xuat_Ban"].Value.ToString();
                txtGiaBia.Text = dgvDauSach.Rows[i].Cells["Gia_Bia"].Value.ToString();
                comMaNXB.Text = dgvDauSach.Rows[i].Cells["Ma_NXB"].Value.ToString();
                comMaKho.Text = dgvDauSach.Rows[i].Cells["Ma_Kho"].Value.ToString();
                comMaChuDe.Text = dgvDauSach.Rows[i].Cells["Ma_Chu_De"].Value.ToString();
                comMaTheLoai.Text = dgvDauSach.Rows[i].Cells["Ma_TL"].Value.ToString();

                comMaTacGia.Text = dgvDauSach.Rows[i].Cells["Ma_Tac_Gia"].Value.ToString();

            }
        }

       
        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox hoặc nơi nhập liệu
            string tuKhoa = txtTimKiem.Text;

            // Chuẩn bị câu lệnh SQL tìm kiếm
            string sql = "SELECT Ma_Dau_Sach,Ten_Dau_Sach,Nam_Xuat_Ban,Gia_Bia,Ma_NXB,Ma_Kho,Ma_Chu_De,Ma_TL,Ma_Tac_Gia FROM [dbo].[View_DauSach_Details] " +
                         "WHERE Ten_Dau_Sach LIKE @TuKhoa " +
                         "   OR Ma_Dau_Sach LIKE @TuKhoa " +
                          "   OR Nam_Xuat_Ban LIKE @TuKhoa " +
                           "   OR Gia_Bia LIKE @TuKhoa " +
                            "   OR Ma_Kho LIKE @TuKhoa " +
                             "   OR Ma_Chu_De LIKE @TuKhoa " +
                              "   OR Ma_TL LIKE @TuKhoa " +
                               "   OR Ma_Tac_Gia LIKE @TuKhoa " +
                         "   OR Ma_NXB LIKE @TuKhoa";

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
                    dgvDauSach.DataSource = dt;
                }
            }
        }

        private void Themsach_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select Ma_Dau_Sach, Ten_Dau_Sach,Nam_Xuat_Ban,Gia_Bia,Ma_NXB,Ma_Kho," +
                "Ma_Chu_De,Ma_TL,Ma_Tac_Gia from View_DauSach_Details";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvDauSach.DataSource = dt;
            dgvDauSach.Refresh();

            SqlDataAdapter daNXB = new SqlDataAdapter();
            DataTable dtNXB = new DataTable();
            string sqlNXB = "select Ma_NXB from NXB";
            daNXB = new SqlDataAdapter(sqlNXB, conn);
            daNXB.Fill(dtNXB);
            comMaNXB.DataSource = dtNXB;
            comMaNXB.DisplayMember = "Ma_NXB";

            SqlDataAdapter daKho = new SqlDataAdapter();
            DataTable dtKho = new DataTable();
            string sqlKho = "select Ma_Kho from Kho";
            daKho = new SqlDataAdapter(sqlKho, conn);
            daKho.Fill(dtKho);
            comMaKho.DataSource = dtKho;
            comMaKho.DisplayMember = "Ma_Kho";

            SqlDataAdapter daCD = new SqlDataAdapter();
            DataTable dtCD = new DataTable();
            string sqlCD = "select Ma_Chu_De from ChuDe";
            daCD = new SqlDataAdapter(sqlCD, conn);
            daCD.Fill(dtCD);
            comMaChuDe.DataSource = dtCD;
            comMaChuDe.DisplayMember = "Ma_Chu_De";

            SqlDataAdapter daTL = new SqlDataAdapter();
            DataTable dtTL = new DataTable();
            string sqlTL = "select Ma_The_Loai from TheLoai";
            daTL = new SqlDataAdapter(sqlTL, conn);
            daTL.Fill(dtTL);
            comMaTheLoai.DataSource = dtTL;
            comMaTheLoai.DisplayMember = "Ma_The_Loai";

            SqlDataAdapter daTG = new SqlDataAdapter();
            DataTable dtTG = new DataTable();
            string sqlTG = "select Ma_Tac_Gia from TacGia";
            daTG = new SqlDataAdapter(sqlTG, conn);
            daTG.Fill(dtTG);
            comMaTacGia.DataSource = dtTG;
            comMaTacGia.DisplayMember = "Ma_Tac_Gia";
        }
    }
}
