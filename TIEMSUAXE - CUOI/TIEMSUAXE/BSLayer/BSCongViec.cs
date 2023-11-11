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
    class BSCongViec
    {
        DBMain db = null;
        string error = null;
        public BSCongViec()
        {
            db = new DBMain();
        }

        public DataSet GetCongViec()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM CONG_VIEC", CommandType.Text);
        }

        public bool AddCongViec(string MaCV, string TenCV, float GiaCV, float CongTho, ref string err)
        {
            string sqlString = "proc_ThemCongViec";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaCV", MaCV),
                new SqlParameter("@TenCV",TenCV),
                new SqlParameter("@GiaCV", GiaCV),
                new SqlParameter("@CongTho", CongTho),

            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }

        public bool DeleteCongViec(string MaCV, ref string error)
        {
            string sqlString = "proc_XoaCongViec";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaCV", MaCV)
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }


        public bool UpdateCongViec(string MaCV, string TenCV, float GiaCV, float CongTho, ref string err)
        {
            string sqlString = "proc_CapNhatCongViec";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaCV", MaCV),
                new SqlParameter("@TenCV",TenCV),
                new SqlParameter("@GiaMoi", GiaCV),
                new SqlParameter("@CongTho", CongTho),
            };
            return db.MyExecuteNonQuery(sqlString, CommandType.StoredProcedure, parameters, ref error);
        }
        public DataSet SearchCongViec(String TenCV)
        {
            SqlParameter p1 = new SqlParameter("@string", SqlDbType.NVarChar);
            p1.Value = TenCV;

            return db.ExcuteQueryDataSetWithParam("SELECT * FROM func_SearchByTenCV(@string)", CommandType.Text, p1);
        }
    }
}
