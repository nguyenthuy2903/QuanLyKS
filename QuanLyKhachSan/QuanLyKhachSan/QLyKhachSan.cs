using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QuanLyKhachSan
{
    public partial class QLyKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public QLyKhachSan()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string connecionString = "Data  Source = localhost\\ SQLEXPRESS; Intial Catalog = Quan_Ly_Khach_San;Integrated Security=True";
            con.ConnectionString = connecionString;
            con.Open();

        }
        private void loadDataoGridview()
        {
            string sql = "select *from QuanLyKS";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tableQuanLyS = new DataTable();
            adp.Fill(tableQuanLyS);

            dataGridView1.DataSource = tableQuanLyS;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView1.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtDonGia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Enabled = false;
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete From Phong Where MaPhong; = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loadDataoGridview();
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Enabled = true;
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaPhong.ReadOnly = true;
            string sql = "update QuanLyKS set TenPhong = '" + txtTenPhong.Text.ToString() + "',"
              + "DonGia = '" + txtDonGia.Text.ToString() + "'where MaPhong = '" + txtMaPhong.Text + "'";
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            loadDataoGridview();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn Cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên phòng");
                txtTenPhong.Focus();
            }
            else
            {
                string sql = "insert into QuanLyKS values('" + txtMaPhong.Text + "', '" + txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                    sql = sql + " , " + txtDonGia.Text.Trim();
                sql = sql + " ) ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                loadDataoGridview();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            loadDataoGridview();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
               (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
    }
}
    
