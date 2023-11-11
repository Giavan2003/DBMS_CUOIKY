using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using TIEMSUAXE.DBLayer;

namespace TIEMSUAXE.BSLayers
{
    class BSPhieuSC
    {
        DBMain db = null;
        string error = null;
        public BSPhieuSC()
        {
            db = new DBMain();
        }
        public DataSet GetPhieuSC()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM PHIEU_SUA_CHUA", CommandType.Text);
        }
        public bool AddPhieuSC(string MaPhieuSC, string BienSoXe, string LoaiXe, string MauXe, string TrangThaiPhieuSC, string MaNV, ref string err)
        {
            string sqlString = "proc_themPhieuSuaChua";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaPhieuSC", MaPhieuSC),
                new SqlParameter("@BienSoXe", BienSoXe),
                new SqlParameter("@LoaiXe", LoaiXe),
                new SqlParameter("@MauXe", MauXe),
                new SqlParameter("@TrangThaiPhieuSC", TrangThaiPhieuSC),
                new SqlParameter("@MaNV", MaNV)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }
        public bool DeletePhieuSC(string MaPhieuSC, ref string err)
        {
            string sqlString = "proc_XoaPhieuSuaChua";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaPhieuSC", MaPhieuSC)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }
        public bool UpdatePhieuSC(string MaPhieuSC, string BienSoXe, string LoaiXe, string MauXe, string TrangThaiPhieuSC, string MaNV, ref string err)
        {
            string sqlString = "proc_CapNhatPhieuSuaChua";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaPhieuSC", MaPhieuSC),
                new SqlParameter("@BienSoXe", BienSoXe),
                new SqlParameter("@LoaiXe", LoaiXe),
                new SqlParameter("@MauXe", MauXe),
                new SqlParameter("@TrangThaiPhieuSC", TrangThaiPhieuSC),
                new SqlParameter("@MaNV", MaNV)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }
        public DataSet SearchPhieuSC(string BienSoXe)
        {
            SqlParameter p = new SqlParameter("@string", SqlDbType.NVarChar);
            p.Value = BienSoXe;

            return db.ExcuteQueryDataSetWithParam("SELECT * FROM func_SearchByBienSoXe(@string)", CommandType.Text, p);
        }

    }

}
