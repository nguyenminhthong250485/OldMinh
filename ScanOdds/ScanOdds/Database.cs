using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ScanOdds
{
    public class Database
    {
        public SqlConnection conn;
        public Database()
        {
            string sqlserver = "mt-return.ddns.me,3030";

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "Server=" + sqlserver + ";Database=ODD;User ID=tt;Password=tt135135;Max Pool Size=250; Connection Timeout=300";
                conn.Open();
            }
            catch
            {
                throw;
            }
        }
        private void openConnect()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }
        private void closeConnect()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        private DataSet execQuery(string sql)
        {
            DataSet ds = null;
            try
            {
                openConnect();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.CommandTimeout = 1000;
                ds = new DataSet();
                da.Fill(ds);
                closeConnect();
            }
            catch (Exception)
            {
                throw;
            }
            finally { conn.Close(); }
            return ds;
        }
        private int execNonQuery(string sql)
        {
            int ret = 0;
            try
            {
                openConnect();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 500;
                ret = cmd.ExecuteNonQuery();
                closeConnect();
            }
            catch
            {
                //throw;
            }
            return ret;
        }
        public long getServerDate()
        {
            DataTable dt = execQuery("select datediff(s,'1970-1-1', getdate())").Tables[0];
            return long.Parse(dt.Rows[0][0].ToString());
        }
        public string getServerStringDate()
        {
            DataTable dt = execQuery("select GETDATE() AS [DateTime]").Tables[0];
            return dt.Rows[0][0].ToString();
        }

        public int doInsertSboOdds(string sql)
        {
            return execNonQuery(@"INSERT INTO [ODD].[dbo].[SboOdd]
                                ([IsLive]
                                ,[LeaugeName]
                                ,[HomeName]
                                ,[AwayName]
                                ,[TimeLive]
                                ,[TimeNonLive]
                                ,[Score]
                                ,[IdKeo]
                                ,[Keo]
                                ,[BetType]
                                ,[Odd1]
                                ,[Odd2]
                                ,[ScanTime]
                                ,[AfterSeconds])
                            VALUES " + sql.Substring(0, sql.Length - 1));
        }
    }
}
