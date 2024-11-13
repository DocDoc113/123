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
    public partial class NXB : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        public NXB()
        {
            InitializeComponent();
        }

        private void NXB_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select* from NXB";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvNXB.DataSource = dt;
            dgvNXB.Refresh();

        }
        public void NapCT()
        {
            if (dgvNXB.CurrentRow != null && dgvNXB.CurrentRow.Index >= 0)
            {

                int i = dgvNXB.CurrentRow.Index; // Lấy chỉ số dòng hiện tại
                txtMaNXB.Text = dgvNXB.Rows[i].Cells["Ma_NXB"].Value.ToString();
                txtTenNXB.Text = dgvNXB.Rows[i].Cells["Ten_NXB"].Value.ToString();
                txtEmail.Text = dgvNXB.Rows[i].Cells["Email"].Value.ToString();
            }
        }
        private bool addnewFlag = false;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            txtMaNXB.Text = "";
            txtTenNXB.Text = "";
            txtEmail.Text = "";
          
            txtMaNXB.Focus();
            addnewFlag = true;
        }
    

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu đang trong chế độ thêm mới
            if (addnewFlag)
            {
                try
                {
                    // Lấy giá trị từ các TextBox
                    string tMaNXB = txtMaNXB.Text;
                    string tTenNXB = txtTenNXB.Text;
                    string tEmail = txtEmail.Text;

                    // Tạo câu lệnh SQL Insert để thêm mới
                    sql = "INSERT INTO NXB (Ma_NXB, Ten_NXB, Email) VALUES (@Ma_NXB, @Ten_NXB, @Email)";

                    // Sử dụng SqlConnection và SqlCommand trong using để đảm bảo giải phóng tài nguyên
                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Ma_NXB", tMaNXB);
                            cmd.Parameters.AddWithValue("@Ten_NXB", tTenNXB);
                            cmd.Parameters.AddWithValue("@Email", tEmail);

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

        // Phương thức LoadData để tải lại dữ liệu sau khi thêm mới hoặc cập nhật
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    da = new SqlDataAdapter("SELECT * FROM NXB", conn);
                    dt.Clear();
                    da.Fill(dt);
                    dgvNXB.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        void DoSQL(string sql)
        {
            using (conn = new SqlConnection(strCon))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (addnewFlag == false)
            {
                int i; 
                int n = Convert.ToInt16(dgvNXB.RowCount.ToString());
                for (i = 0; i < n - 1; i++)
                {
                   string  tMaNXB = dgvNXB.Rows[i].Cells["Ma_NXB"].Value.ToString();
                   string tTenNXB = dgvNXB.Rows[i].Cells["Ten_NXB"].Value.ToString();
                   string tEmail = dgvNXB.Rows[i].Cells["Email"].Value.ToString();

                    sql = $"UPDATE NXB set Ten_NXB= N'{tTenNXB}', Email='{tEmail}' Where Ma_NXB = '{tMaNXB}'";
                    DoSQL(sql);
                }
                MessageBox.Show("Đã cập nhật thành công!");
            }
        }

        private void dgvNXB_SelectionChanged(object sender, EventArgs e)
        {
            NapCT();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox hoặc nơi nhập liệu
            string tuKhoa = txtTimKiem.Text;

            // Chuẩn bị câu lệnh SQL tìm kiếm
            string sql = "SELECT Ma_NXB,Ten_NXB, Email FROM NXB " +
                         "WHERE Ma_NXB LIKE @TuKhoa " +
                         "   OR Ten_NXB LIKE @TuKhoa" +
                         "   OR Email LIKE @TuKhoa";

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
                    dgvNXB.DataSource = dt;
                }
            }
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dgvNXB.SelectedRows.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời?", "Xác nhận yêu cầu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int i = dgvNXB.CurrentRow.Index;
                    string tMaNXB = dgvNXB.Rows[i].Cells["Ma_NXB"].Value.ToString();
                    string sql = $"Delete from NXB where Ma_NXB = '{tMaNXB}'";
                    DoSQL(sql);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Chua chon ban ghi");
            }

        }

       
    }
}
