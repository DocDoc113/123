using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_thư_viện
{
    public partial class frmTrangchu : Form
    {
        public frmTrangchu()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            quản_lý_sách1.Visible = true;
            quản_lý_sách1.BringToFront();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
           uCthongke1.Visible = true;
           uCthongke1.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton2_Click_2(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmTrangchu_Load(object sender, EventArgs e)
        {
            quản_lý_sách1.Visible = false;
            Quanlysach.PerformClick();
            uC_quanlydocgia1.Visible=false;
            quanlydocgia.PerformClick();
            uCquanlymuontra1.Visible = false;
            quanlydocgia.PerformClick();
        }

        private void quản_lý_sách1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void quanlydocgia_Click(object sender, EventArgs e)
        {
            uC_quanlydocgia1.Visible = true;
            uC_quanlydocgia1.BringToFront();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            uCquanlymuontra1.Visible = true;
            uCquanlymuontra1.BringToFront();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            uC_trangchu1.Visible = true;
            uC_trangchu1.BringToFront();
        }

        private void uC_trangchu1_Load(object sender, EventArgs e)
        {
        }
    }
}
