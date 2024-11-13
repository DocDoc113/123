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
    public partial class ThemChiTiet : Form
    {
        public ThemChiTiet()
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

       

        private void ThemChiTiet_Load(object sender, EventArgs e)
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

        private void btnTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
                    // Lấy giá trị từ các TextBox
                    string tMaSach = txtMaSach.Text;
                    string tTenDauSach = comTenDauSach.Text;
                    string tTinhTrang = comTinhTrang.Text;
                    

                    // Tạo câu lệnh SQL Insert để thêm mới
                    sql = "DECLARE @MaDauSach NVARCHAR(50);\r\nSELECT @MaDauSach = Ma_Dau_Sach\r\nFROM DauSach\r\nWHERE Ten_Dau_Sach = @Ten_Dau_Sach ;" +
                        "DECLARE @MaTinhTrang NVARCHAR(50);\r\nSELECT @MaTinhTrang = Ma_Tinh_Trang\r\nFROM TinhTrang\r\nWHERE Ten_Tinh_Trang = @Tinh_Trang ;\r\n" +
                        "INSERT INTO Sach (Ma_Sach, Ma_Dau_Sach, Ma_Tinh_Trang)\r\nVALUES ( @Ma_Sach, @MaDauSach, @MaTinhTrang);";

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



            MessageBox.Show("Thêm mới thành công!");

            this.Close(); // Đóng form hiện tại

        }

        private void comTenDauSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
