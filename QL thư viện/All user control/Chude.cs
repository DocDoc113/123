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
    public partial class Chude : Form
    {
        private bool addnewFlag = false;
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        DataTable comdt = new DataTable();
        string sql, constr;
        string strCon = @"Data Source=DESKTOP-9JJTF2A\SQLEXPRESS;Initial Catalog=""QLthuvien (1)"";Integrated Security=True;TrustServerCertificate=True";
        public Chude()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Chude_Load(object sender, EventArgs e)
        {

            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select* from ChuDe";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvChuDe.DataSource = dt;
            dgvChuDe.Refresh();
        }
        public void NapCT()
        {
            if (dgvChuDe.CurrentRow != null && dgvChuDe.CurrentRow.Index >= 0)
            {

                int i = dgvChuDe.CurrentRow.Index; // Lấy chỉ số dòng hiện tại
                txtMaCD.Text = dgvChuDe.Rows[i].Cells["Ma_Chu_De"].Value.ToString();
                txtTenCD.Text = dgvChuDe.Rows[i].Cells["Ten_Chu_De"].Value.ToString();
             
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaCD.Text = "";
            txtTenCD.Text = "";
           

            txtMaCD.Focus();
            addnewFlag = true;
        }

        // Phương thức LoadData để tải lại dữ liệu sau khi thêm mới hoặc cập nhật
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strCon))
                {
                    da = new SqlDataAdapter("SELECT * FROM ChuDe", conn);
                    dt.Clear();
                    da.Fill(dt);
                    dgvChuDe.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnCapNhat_Click(object sender, EventArgs e)
        {

            // Kiểm tra nếu đang trong chế độ thêm mới
            if (addnewFlag)
            {
                try
                {
                    // Lấy giá trị từ các TextBox
                    string tMaCD = txtMaCD.Text;
                    string tTenCD = txtTenCD.Text;

                    // Tạo câu lệnh SQL Insert để thêm mới
                    sql = "INSERT INTO ChuDe (Ma_Chu_De, Ten_Chu_De) VALUES (@Ma_Chu_De, @Ten_Chu_De)";

                    // Sử dụng SqlConnection và SqlCommand trong using để đảm bảo giải phóng tài nguyên
                    using (SqlConnection conn = new SqlConnection(strCon))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // Thêm tham số vào câu lệnh SQL
                            cmd.Parameters.AddWithValue("@Ma_Chu_De", tMaCD);
                            cmd.Parameters.AddWithValue("@Ten_Chu_De", tTenCD);
                            

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

        private void dgvChuDe_CellStyleContentChanged(object sender, DataGridViewCellStyleContentChangedEventArgs e)
        {

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
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (addnewFlag == false)
            {
                int i;
                int n = Convert.ToInt16(dgvChuDe.RowCount.ToString());
                for (i = 0; i < n - 1; i++)
                {
                    string tMaCD = dgvChuDe.Rows[i].Cells["Ma_Chu_De"].Value.ToString();
                    string tTenCD = dgvChuDe.Rows[i].Cells["Ten_Chu_De"].Value.ToString();

                    sql = $"UPDATE ChuDe set Ten_Chu_De= N'{tTenCD}' Where Ma_Chu_De = '{tMaCD}'";
                    DoSQL(sql);
                }
                MessageBox.Show("Đã cập nhật thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChuDe.SelectedRows.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi hiện thời?", "Xác nhận yêu cầu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    int i = dgvChuDe.CurrentRow.Index;
                    string tMaCD = dgvChuDe.Rows[i].Cells["Ma_Chu_De"].Value.ToString();
                    string sql = $"Delete from ChuDe where Ma_Chu_De = '{tMaCD}'";
                    DoSQL(sql);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Chua chon ban ghi");
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa tìm kiếm từ TextBox hoặc nơi nhập liệu
            string tuKhoa = txtTimKiem.Text;

            // Chuẩn bị câu lệnh SQL tìm kiếm
            string sql = "SELECT Ma_Chu_De,Ten_Chu_De FROM ChuDe " +
                         "WHERE Ma_Chu_De LIKE @TuKhoa " +

                         "   OR Ten_Chu_De LIKE @TuKhoa";

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
                    dgvChuDe.DataSource = dt;
                }
            }
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvChuDe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        
    }
}
