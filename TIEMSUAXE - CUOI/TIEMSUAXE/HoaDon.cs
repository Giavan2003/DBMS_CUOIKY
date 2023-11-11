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
using TIEMSUAXE.BSLayer;

namespace TIEMSUAXE
{
    public partial class HoaDon : Form
    {
        DataTable dtHoaDon = null;
        string err;
        BSHoaDon dbHoaDon = new BSHoaDon();
        public HoaDon()
        {
            InitializeComponent();
        }
        void LoadData() //xong
        {
            try
            {
                dtHoaDon = new DataTable();
                dtHoaDon.Clear();
                DataSet ds = dbHoaDon.GetHoaDon();
                dtHoaDon = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView
                dgv_HoaDon.DataSource = dtHoaDon;

                // Thay đổi độ rộng cột
                dgv_HoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dgv_NhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgv_HoaDon.AutoResizeColumns();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Không lấy được nội dung trong table HoaDon. Lỗi: " + e);
            }
        }

        private void lbl_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbl_HoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.Show();
        }

        private void lbl_NhanVien_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Show();
        }

        private void lbl_CongViec_Click(object sender, EventArgs e)
        {
            CongViec cv = new CongViec();
            cv.Show();
        }

        private void lbl_PhieuSC_Click(object sender, EventArgs e)
        {
            PhieuSC psc = new PhieuSC();
            psc.Show();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                string MaHD = txt_MaHD.Text;
                string HoTenKH = txt_TenKH.Text;
                string SDT_KH = txt_SDTKH.Text;
                string TongChiPhi = txt_TongChiPhi.Text;
                DateTime NgayNhan = dtp_Nhan.Value;
                DateTime NgayTra = dtp_Tra.Value;
                string TrangThaiHDSX = txt_TTHD.Text;
                string MaPhieuSC = txt_MaPhieuSC.Text;
                //txt_MaNV.Enabled = false;
                // Hiện hộp thoại hỏi đáp 
                DialogResult result = MessageBox.Show("Bạn có muốn cập nhật dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không? 
                if (result == DialogResult.Yes)
                {
                    dbHoaDon.UpdateHoaDon(MaHD, HoTenKH, SDT_KH, float.Parse(txt_TongChiPhi.Text), NgayNhan, NgayTra, TrangThaiHDSX, MaPhieuSC, ref err);
                    // Cập nhật lại DataGridView
                    LoadData();
                    // Thông báo 
                    MessageBox.Show("Hoàn thành!");
                }
                else
                {
                    // Thông báo 
                    MessageBox.Show("Không thể cập nhật");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                string MaHD = txt_MaHD.Text;
                string HoTenKH = txt_TenKH.Text;
                string SDT_KH = txt_SDTKH.Text;
                float TongChiPhi = 0;
                DateTime NgayNhan = dtp_Nhan.Value;
                DateTime NgayTra = dtp_Tra.Value;
                string TrangThaiHDSX = txt_TTHD.Text;
                string MaPhieuSC = txt_MaPhieuSC.Text;
                //txt_MaNV.Enabled = false;
                // Hiện hộp thoại hỏi đáp 
                DialogResult result = MessageBox.Show("Bạn có muốn thêm dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không? 
                if (result == DialogResult.Yes)
                {
                    dbHoaDon.AddHoaDon(MaHD, HoTenKH, SDT_KH, TongChiPhi, NgayNhan, NgayTra, TrangThaiHDSX, MaPhieuSC, ref err);
                    // Cập nhật lại DataGridView
                    LoadData();
                    // Thông báo 
                    MessageBox.Show("Hoàn thành!");
                }
                else
                {
                    // Thông báo 
                    MessageBox.Show("Không thể thêm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xoá dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (dbHoaDon.DeleteHoaDon(dgv_HoaDon.CurrentRow.Cells["MaHD"].Value.ToString(), ref err))
                {
                    LoadData();
                    MessageBox.Show("Xoá thành công!");
                }
                else
                {
                    MessageBox.Show("Xoá không thành công. Lỗi: '" + err + "'");
                }
            }
        }
        public void LoadGridByKeyword()
        {
            try
            {
                dtHoaDon = new DataTable();
                dtHoaDon.Clear();
                DataSet ds = dbHoaDon.SearchHoaDon(txt_TimKiem.Text.Trim());
                dtHoaDon = ds.Tables[0];
                // Đưa dữ liệu lên DataGridView 
                dgv_HoaDon.DataSource = dtHoaDon;
                // Thay đổi độ rộng cột 
                dgv_HoaDon.AutoResizeColumns();
                txt_TimKiem.ResetText();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Hóa Đơn. Lỗi rồi!!!");
            }
        }

        private void btn_Timkiem_Click(object sender, EventArgs e)
        {
            LoadGridByKeyword();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgv_HoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_HoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_HoaDon.SelectedRows[0];
                txt_MaHD.Text = selectedRow.Cells["MaHD"].Value.ToString();
                txt_TenKH.Text = selectedRow.Cells["HoTenKH"].Value.ToString();
                txt_SDTKH.Text = selectedRow.Cells["SDT_KH"].Value.ToString();
                txt_TongChiPhi.Text = selectedRow.Cells["TongChiPhi"].Value.ToString();
                dtp_Nhan.Text = selectedRow.Cells["NgayNhan"].Value.ToString();
                dtp_Tra.Text = selectedRow.Cells["NgayTra"].Value.ToString();
                txt_TTHD.Text = selectedRow.Cells["TrangThaiHDSX"].Value.ToString();
                txt_MaPhieuSC.Text = selectedRow.Cells["MaPhieuSC"].Value.ToString();
            }
        }
    }
}
