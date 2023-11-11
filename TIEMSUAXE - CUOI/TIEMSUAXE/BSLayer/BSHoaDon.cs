using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEMSUAXE.DBLayer;

namespace TIEMSUAXE.BSLayer
{
    class BSHoaDon
    {
        DBMain db = null;
        string error = null;
        public BSHoaDon()
        {
            db = new DBMain();
        }

        public DataSet GetHoaDon()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM HOA_DON_SUA_XE", CommandType.Text);
        }

        public DataSet SearchHoaDon(String thongtin)
        {
            SqlParameter p1 = new SqlParameter("@string", SqlDbType.NVarChar);
            p1.Value = thongtin;

            return db.ExcuteQueryDataSetWithParam("SELECT * FROM func_SearchHoaDon(@string)", CommandType.Text, p1);
        }

        public bool AddHoaDon(string MaHD, string HoTenKH, string SDT_KH, float TongChiPhi, DateTime NgayNhan, DateTime NgayTra, String TrangThaiHDSX, String MaPhieuSC, ref string err)
        {
            string sqlString = "proc_AddHoaDon";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@HoTenKH", HoTenKH),
                new SqlParameter("@SDT_KH", SDT_KH),
                new SqlParameter("@TongChiPhi", TongChiPhi),
                new SqlParameter("@NgayNhan", NgayNhan),
                new SqlParameter("@NgayTra", NgayTra),
                new SqlParameter("@TrangThaiHDSX", TrangThaiHDSX),
                new SqlParameter("@MaPhieuSC", MaPhieuSC)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }

        public bool DeleteHoaDon(string MaHD, ref string error)
        {
            string sqlString = "proc_XoaHoaDon";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaHD", MaHD)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }


        public bool UpdateHoaDon(string MaHD, string HoTenKH, string SDT_KH, float TongChiPhi, DateTime NgayNhan, DateTime NgayTra, String TrangThaiHDSX, String MaPhieuSC, ref string err)
        {
            string sqlString = "proc_SuaThongTinHoaDon";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@HoTenKH", HoTenKH),
                new SqlParameter("@SDT_KH", SDT_KH),
                new SqlParameter("@TongChiPhi", TongChiPhi),
                new SqlParameter("@NgayNhan", NgayNhan),
                new SqlParameter("@NgayTra", NgayTra),
                new SqlParameter("@TrangThaiHDSX", TrangThaiHDSX),
                new SqlParameter("@MaPhieuSC", MaPhieuSC)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }

    }
}

