using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIEMSUAXE.DBLayer;

namespace TIEMSUAXE.BSLayer
{
    class BSNhanVien
    {
        DBMain db = null;
        string error = null;
        public BSNhanVien()
        {
            db = new DBMain();
        }

        public DataSet GetNhanVien()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM NHAN_VIEN", CommandType.Text);
        }
        public DataSet SearchNhanVien(String thongtin)
        {
            SqlParameter p1 = new SqlParameter("@string", SqlDbType.NVarChar);
            p1.Value = thongtin;

            return db.ExcuteQueryDataSetWithParam("SELECT * FROM func_SearchNhanVien(@string)", CommandType.Text, p1);
        }
        public bool AddNhanVien(string MaNV, string HoTenNV, string DiaChi, String GioiTinh, string SDT_NV, DateTime NgaySinh, ref string err)
        {
            string sqlString = "proc_ThemNhanVien";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaNV", MaNV),
                new SqlParameter("@HoTenNV", HoTenNV),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@SDT_NV", SDT_NV),
                new SqlParameter("@NgaySinh", NgaySinh)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }

        public bool DeleteNhanVien(string MaNV, ref string error)
        {
            string sqlString = "proc_XoaNhanVien";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaNV", MaNV)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }


        public bool UpdateNhanVien(string MaNV, string HoTenNV, string DiaChi, string GioiTinh, string SDT_NV, DateTime NgaySinh, ref string err)
        {
            string sqlString = "proc_SuaThongTinNhanVien";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaNV", MaNV),
                new SqlParameter("@HoTenNV", HoTenNV),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@SDT_NV", SDT_NV),
                new SqlParameter("@NgaySinh", NgaySinh)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }

    }
}
