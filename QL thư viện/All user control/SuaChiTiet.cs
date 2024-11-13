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
    public partial class SuaChiTiet : Form
    {
        public SuaChiTiet()
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
            comTenDauSach.Text = row.Cells["Ten_Dau_Sach"].Value.ToString();
            comTinhTrang.Text = row.Cells["Ten_Tac_Gia"].Value.ToString();
            
        }

        private void SuaChiTiet_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = strCon;
            conn.Open();
            sql = "select Ten_Dau_Sach from DauSach";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            comTenDauSach.DataSource = dt;
            comTenDauSach.DisplayMember = "Ten_Dau_Sach";

            SqlDataAdapter daTT = new SqlDataAdapter();
            DataTable dtTT = new DataTable();
            string sqlTT = "select Ten_Tinh_Trang from TinhTrang";
            daTT = new SqlDataAdapter(sqlTT, conn);
            daTT.Fill(dtTT);
            comTinhTrang.DataSource = dtTT;
            comTinhTrang.DisplayMember = "Ten_Tinh_Trang";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            
                string tMaSach = txtMaSach.Text;
                string tTenDauSach = comTenDauSach.Text;
                string tTinhTrang = comTinhTrang.Text;


            // Tạo câu lệnh SQL Insert để thêm mới
            sql = "UPDATE S SET S.Ma_Dau_Sach = DS.Ma_Dau_Sach, S.Ma_Tinh_Trang = TT.Ma_Tinh_Trang FROM Sach S INNER JOIN DauSach DS ON DS.Ten_Dau_Sach = @Ten_Dau_Sach INNER JOIN TinhTrang TT ON TT.Ten_Tinh_Trang = @Tinh_Trang WHERE S.Ma_Sach = @Ma_Sach;"; 

            // Sử dụng SqlConnection và SqlCommand trong using để đảm bảo giải phóng tài nguyên
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@Ma_Sach", tMaSach);
                    cmd.Parameters.AddWithValue("@Ten_Dau_Sach", tTenDauSach);
                    cmd.Parameters.AddWithValue("@Tinh_Trang", tTinhTrang);



                    // Mở kết nối và thực thi lệnh
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }



            MessageBox.Show("Cập nhật thành công!");

            this.Close(); // Đóng form hiện tại
        }
    }
}
