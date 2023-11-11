using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIEMSUAXE.DBLayer
{
    class DBMain
    {
        //string ConnStr = "Data Source=DESKTOP-298HLM4\SQLSERVER2022;Initial Catalog=TIEMSUAXE;Persist Security Info=True;User ID=sa;Password=Vanvan1610.";
        string ConnStr = "Data Source=DESKTOP-298HLM4\\SQLSERVER2022;Initial Catalog=TIEMSUAXE;Integrated Security=True";
        SqlConnection conn = null;
        public SqlCommand comm = null;
        SqlDataAdapter da = null;
        public DBMain()
        {
            conn = new SqlConnection(ConnStr);
            comm = conn.CreateCommand();
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct, List<SqlParameter> parameters = null)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    comm.Parameters.Add(parameter);
                }
            }
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        public DataSet ExcuteQueryDataSetWithParam(string strSQL, CommandType commType, params SqlParameter[] param)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = commType;
            comm.Parameters.Clear();
            foreach (SqlParameter p in param)
            {
                comm.Parameters.Add(p);
            }
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            return ds;
        }
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, List<SqlParameter> parameters, ref string error)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            comm.Parameters.Clear();


            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    comm.Parameters.Add(parameter);
                    //MessageBox.Show(parameter.Value.ToString());
                }
            }
            try
            {
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return f;
        }
    }
}
