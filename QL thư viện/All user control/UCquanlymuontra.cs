using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_thư_viện.All_user_control
{
    public partial class UCquanlymuontra : UserControl
    {
        public UCquanlymuontra()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Phieumuon phieumuon = new Phieumuon();
            phieumuon.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Phieuphat phieuphat = new Phieuphat();
            phieuphat.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
