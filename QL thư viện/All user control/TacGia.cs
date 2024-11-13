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
    public partial class TacGia : Form
    {
        private bool addnewFlag = false;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        public TacGia()
        {
            InitializeComponent();
        }

        private void theloai_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select* from TacGia";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvTacGia.DataSource = dt;
            dgvTacGia.Refresh();


        }

        private void dgvTacGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }
        // Dosql 
        void DoSQL(string sql)
        {
            using (conn = new SqlConnection(strCon))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        // Phương thức LoadData để tải lại dữ liệu sau khi thêm mới hoặc cập nhật
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    da = new SqlDataAdapter("SELECT * FROM TacGia", conn);
                    dt.Clear();
                    da.Fill(dt);
                    dgvTacGia.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaTacGia.Text = "";
            txtTenTacGia.Text = "";
            txtNamSinh.Text = "";

            txtMaTacGia.Focus();
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
                    string tMaTG = txtMaTacGia.Text;
                    string tTenTG = txtTenTacGia.Text;
                    string tNamSinh = txtNamSinh.Text;

                    // Tạo câu lệnh SQL Insert để thêm mới
                    sql = "INSERT INTO TacGia (Ma_Tac_Gia, Ten_Tac_Gia, Nam_Sinh) VALUES (@Ma_TG, @Ten_TG, @NamSinh)";

                    // Sử dụng SqlConnection và SqlCommand trong using để đảm bảo giải phóng tài nguyên
                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Ma_TG", tMaTG);
                            cmd.Parameters.AddWithValue("@Ten_TG", tTenTG);
                            cmd.Parameters.AddWithValue("@NamSinh", tNamSinh);

                            // Mở kết nối và thực thi lệnh
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    MessageBox.Show("Thêm mới thành công!");

                    // Cập nhật lại DataGridView để hiển thị dữ liệu mới
                    LoadData();

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
                int i;
                int n = Convert.ToInt16(dgvTacGia.RowCount.ToString());
                for (i = 0; i < n - 1; i++)
                {
                    string tMaTG = dgvTacGia.Rows[i].Cells["Ma_Tac_Gia"].Value.ToString();
                    string tTenTG = dgvTacGia.Rows[i].Cells["Ten_Tac_Gia"].Value.ToString();
                    string tNamSinh = dgvTacGia.Rows[i].Cells["Nam_Sinh"].Value.ToString();

                    sql = $"UPDATE TacGia set Ten_Tac_Gia= N'{tTenTG}', Nam_Sinh='{tNamSinh}' Where Ma_Tac_Gia = '{tMaTG}'";
                    DoSQL(sql);
                }
                MessageBox.Show("Đã cập nhật thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời?", "Xác nhận yêu cầu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int i = dgvTacGia.CurrentRow.Index;
                    string tMaTG = dgvTacGia.Rows[i].Cells["Ma_Tac_Gia"].Value.ToString();
                    string sql = $"Delete from TacGia where Ma_Tac_Gia = '{tMaTG}'";
                    DoSQL(sql);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Chua chon ban ghi");
            }


        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox hoặc nơi nhập liệu
            string tuKhoa = txtTimKiem.Text;

            // Chuẩn bị câu lệnh SQL tìm kiếm
            string sql = "SELECT Ma_Tac_Gia,Ten_Tac_Gia, Nam_Sinh FROM TacGia " +
                         "WHERE Ma_Tac_Gia LIKE @TuKhoa " +
                         "   OR Ten_Tac_Gia LIKE @TuKhoa" +
                         "   OR Nam_Sinh LIKE @TuKhoa";

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
                    dgvTacGia.DataSource = dt;
                }
            }
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void NapCT()
        {
            if (dgvTacGia.CurrentRow != null && dgvTacGia.CurrentRow.Index >= 0)
            {

                int i = dgvTacGia.CurrentRow.Index; // Lấy chỉ số dòng hiện tại
                txtMaTacGia.Text = dgvTacGia.Rows[i].Cells["Ma_Tac_Gia"].Value.ToString();
                txtTenTacGia.Text = dgvTacGia.Rows[i].Cells["Ten_Tac_Gia"].Value.ToString();
                txtNamSinh.Text = dgvTacGia.Rows[i].Cells["Nam_Sinh"].Value.ToString();
            }
        }
    }
}
