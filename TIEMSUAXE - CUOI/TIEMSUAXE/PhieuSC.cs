using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIEMSUAXE.BSLayers;

namespace TIEMSUAXE
{
    public partial class PhieuSC : Form
    {
        DataTable dtPhieuSC = null;
        string err;
        BSPhieuSC dbPhieuSC = new BSPhieuSC();
        public PhieuSC()
        {
            InitializeComponent();
        }
        void LoadData() //xong
        {
            try
            {
                dtPhieuSC = new DataTable();
                dtPhieuSC.Clear();
                DataSet ds = dbPhieuSC.GetPhieuSC();
                dtPhieuSC = ds.Tables[0];

                dgv_PhieuSC.DataSource = dtPhieuSC;

                // Thay đổi độ rộng cột
                dgv_PhieuSC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgv_PhieuSC.AutoResizeColumns();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Không lấy được nội dung trong table PhieuSC. Lỗi: " + e);
            }
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                string MaPhieuSC = txt_MaPhieuSC.Text;
                string BienSoXe = txt_BienSo.Text;
                string LoaiXe = txt_LoaiXe.Text;
                string MauXe = txt_MauXe.Text;
                string TrangThaiPhieuSC = txt_TrangThaiPhieuSC.Text;
                string MaNV = txt_MaNV.Text;

                DialogResult result = MessageBox.Show("Bạn có muốn thêm dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không? 
                if (result == DialogResult.Yes)
                {
                    dbPhieuSC.AddPhieuSC(MaPhieuSC, BienSoXe, LoaiXe, MauXe, TrangThaiPhieuSC, MaNV, ref err);
                    // Cập nhật lại DataGridView
                    LoadData();
                    // Thông báo 
                    MessageBox.Show("Hoàn thành");
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


        private void PhieuSC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgv_PhieuSC_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_PhieuSC.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_PhieuSC.SelectedRows[0];
                txt_MaPhieuSC.Text = selectedRow.Cells["MaPhieuSC"].Value.ToString();
                txt_BienSo.Text = selectedRow.Cells["BienSoXe"].Value.ToString();
                txt_LoaiXe.Text = selectedRow.Cells["LoaiXe"].Value.ToString();
                txt_MauXe.Text = selectedRow.Cells["MauXe"].Value.ToString();
                txt_TrangThaiPhieuSC.Text = selectedRow.Cells["TrangThaiPhieuSC"].Value.ToString();
                txt_MaNV.Text = selectedRow.Cells["MaNV"].Value.ToString();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xoá dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (dbPhieuSC.DeletePhieuSC(dgv_PhieuSC.CurrentRow.Cells["MaPhieuSC"].Value.ToString(), ref err))
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

        private void btn_Sua_Click(object sender, EventArgs e)
        {
                try
                {
                    string MaPhieuSC = txt_MaPhieuSC.Text;
                    string BienSoXe = txt_BienSo.Text;
                    string LoaiXe = txt_LoaiXe.Text;
                    string MauXe = txt_MauXe.Text;
                    string TrangThaiPhieuSC = txt_TrangThaiPhieuSC.Text;
                    string MaNV = txt_MaNV.Text;
                    DialogResult result = MessageBox.Show("Bạn có muốn cập nhật dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Kiểm tra có nhắp chọn nút Ok không? 
                    if (result == DialogResult.Yes)
                    {
                        dbPhieuSC.UpdatePhieuSC(MaPhieuSC, BienSoXe, LoaiXe, MauXe, TrangThaiPhieuSC, MaNV, ref err);
                        // Cập nhật lại DataGridView

                        // Thông báo 
                        MessageBox.Show("Hoàn thành!");
                        LoadData();
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
        public void LoadGridByKeyword()
        {
            try
            {
                dtPhieuSC = new DataTable();
                dtPhieuSC.Clear();
                DataSet ds = dbPhieuSC.SearchPhieuSC(txt_TimKiem.Text.Trim());
                dtPhieuSC = ds.Tables[0];
                dgv_PhieuSC.DataSource = dtPhieuSC;
                // Thay đổi độ rộng cột 
                dgv_PhieuSC.AutoResizeColumns();
                txt_TimKiem.ResetText();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Nhân Vien. Lỗi rồi!!!");
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


        private void lbl_CongViec_Click(object sender, EventArgs e)
        {
            CongViec cv = new CongViec();
            cv.Show();
        }


        private void lbl_NhanVien_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Show();
        }

        private void lbl_HoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.Show();
        }
        private void lbl_PhieuSC_Click(object sender, EventArgs e)
        {
            PhieuSC psc = new PhieuSC();
            psc.Show();
        }

        private void lbl_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
