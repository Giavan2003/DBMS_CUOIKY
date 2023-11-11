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
    public partial class CongViec : Form
    {
        DataTable dtCongViec = null;
        string err;
        BSCongViec dbCongViec = new BSCongViec();
        public CongViec()
        {
            InitializeComponent();
        }
        void LoadData() //xong
        {
            try
            {
                dtCongViec = new DataTable();
                dtCongViec.Clear();
                DataSet ds = dbCongViec.GetCongViec();
                dtCongViec = ds.Tables[0];
               
                dgv_CongViec.DataSource = dtCongViec;

                // Thay đổi độ rộng cột
                dgv_CongViec.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
               
                dgv_CongViec.AutoResizeColumns();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Không lấy được nội dung trong table CongViec. Lỗi: " + e);
            }
        }

        private void CongViec_Load(object sender, EventArgs e) //xong
        {
            LoadData();


        }

        private void dgv_CongViec_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_CongViec.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv_CongViec.SelectedRows[0];
                txt_MaCV.Text = selectedRow.Cells["MaCV"].Value.ToString();
                txt_TenCV.Text = selectedRow.Cells["TenCv"].Value.ToString();
                txt_GiaCV.Text = selectedRow.Cells["GiaCV"].Value.ToString();
                txt_CongTho.Text = selectedRow.Cells["CongTho"].Value.ToString();

            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {

            try
            {
                string MaCV = txt_MaCV.Text;
                string TenCV = txt_TenCV.Text;
                string GiacvText = txt_GiaCV.Text;
                float GiaCV = float.Parse(GiacvText);
                string congThoText = txt_CongTho.Text;
                float CongTho = float.Parse(congThoText);




                DialogResult result = MessageBox.Show("Bạn có muốn thêm dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không? 
                if (result == DialogResult.Yes)
                {
                    dbCongViec.AddCongViec(MaCV, TenCV, GiaCV, CongTho, ref err);
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
                if (dbCongViec.DeleteCongViec(dgv_CongViec.CurrentRow.Cells["MaCV"].Value.ToString(), ref err))
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
                string MaCV = txt_MaCV.Text;
                string TenCV = txt_TenCV.Text;
                string GiacvText = txt_GiaCV.Text;
                float GiaCV = float.Parse(GiacvText);
                string congThoText = txt_CongTho.Text;
                float CongTho = float.Parse(congThoText);
                DialogResult result = MessageBox.Show("Bạn có muốn cập nhật dòng này không?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Kiểm tra có nhắp chọn nút Ok không? 
                if (result == DialogResult.Yes)
                {
                    dbCongViec.UpdateCongViec(MaCV, TenCV, GiaCV, CongTho, ref err);
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
                dtCongViec = new DataTable();
                dtCongViec.Clear();
                DataSet ds = dbCongViec.SearchCongViec(txt_TimKiem.Text.Trim());
                dtCongViec = ds.Tables[0];
                dgv_CongViec.DataSource = dtCongViec;
                // Thay đổi độ rộng cột 
                dgv_CongViec.AutoResizeColumns();
                txt_TimKiem.ResetText();
            }
            catch (SqlException)
            {
                MessageBox.Show("Không lấy được nội dung trong table Công việc. Lỗi rồi!!!");
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

        private void lbl_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbl_PhieuSC_Click(object sender, EventArgs e)
        {
            PhieuSC psc = new PhieuSC();
            psc.Show();
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

    }
}
