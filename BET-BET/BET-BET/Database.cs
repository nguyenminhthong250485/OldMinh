using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BET_BET
{
    public class Database
    {
        DataSet ds;
        SqlDataAdapter da;

        public SqlConnection conn;
        public Database()
        {
            string sqlserver = "mt-return.ddns.me,3030";

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "Server=" + sqlserver + ";Database=Humbuger;User ID=tt;Password=tt135135;Max Pool Size=250; Connection Timeout=300";
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
        public int doInsertTicket(string type, string member, string bettype, string hdp, string home, string away, string keo, string odd, string money, string usd, string group, string phieuchung)
        {
            string sql = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, {9}, '{10}', '{11}'", type, member, bettype, hdp, home, away, keo, odd, money, usd, group, phieuchung);
            return execNonQuery(@"INSERT INTO [DataBetList]
                           ([type]
                           ,[member]
                           ,[bettype]
                           ,[hdp]
                           ,[home]
                           ,[away]
                           ,[keo]
                           ,[odd],[money],[usd],[group],[phieuchung])
                     VALUES
                           (" + sql + ")");
        }
        public DataTable getCompareAccount(string str_group = "")
        {
            if (str_group == "")
            {
                return execQuery("Select * from CompareAccount").Tables[0];
            }
            else
            {
                return execQuery("Select * from CompareAccount Where id='" + str_group + "'").Tables[0];
            }
        }
        public string Login(string username, string password)
        {
            try
            {
                DataTable dt = execQuery("select top 1 username from [User] where username = '" + username.Replace("'", "''") + "' and password = '" + password.Replace("'", "''") + "' and status='1'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public DataTable getPartnerName()
        {
            return execQuery("select name from PartnerName").Tables[0];
        }
        public DataTable getgroup(string PartnerName, string type)
        {
            return execQuery("select distinct group from AccBet where PartnerName='" + PartnerName + "'").Tables[0];
        }
        public DataTable getBetMoney(string PartnerName)
        {
            return execQuery("select distinct money from AccBet where PartnerName='" + PartnerName + "'").Tables[0];
        }
        public DataTable getAccountBet(string PartnerName, string type)
        {
            return execQuery("select * from AccBet where PartnerName ='" + PartnerName + "' and type='" + type + "' and status='" + 1 + "'").Tables[0];
        }
        public DataSet getAccBet(string sheet)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                ds = new DataSet();
                da = new SqlDataAdapter("select * from AccBet where PartnerName = '" + sheet.Replace("'", "''") + "'", conn);
                da.Fill(ds, "AccBet");
                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }     
        public void UpdateAccBet()
        {
            try
            {
                da.Update(ds, "AccBet");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int doInsertPartnerName(string name)
        {
            return execNonQuery("insert into PartnerName values('" + name.Replace("'", "''") + "')");
        }
        public int InsertAccBet(string str_insert,int reset=0)
        {
            string type = str_insert.Split('/')[0];
            string group = str_insert.Split('/')[1];
            string username = str_insert.Split('/')[2];
            string usd = str_insert.Split('/')[3];
            string ip = str_insert.Split('/')[4];
            string money = str_insert.Split('/')[5];
            string mode = str_insert.Split('/')[6];
            string profit = str_insert.Split('/')[7];
            string leauge = str_insert.Split('/')[8];
            string status = str_insert.Split('/')[9];
            string PartnerName = str_insert.Split('/')[10];
            string MaxBet= str_insert.Split('/')[11];
            string sql = string.Format("'{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}','{11}'", type, group, username, usd, ip, money, mode, profit, leauge, status, PartnerName,MaxBet);
            if (reset == 1)
            {
                execNonQuery("DELETE FROM AccBet");
                execNonQuery("DBCC CHECKIDENT (AccBet, RESEED, 0)");
            }
            return execNonQuery(@"INSERT INTO [AccBet] ([type],[group],[username],[usd],[ip],[money],[mode],[profit],[league],[status],[PartnerName],[MaxBet]) 
                                VALUES (" + sql + ")");
        }
        public bool oddExists(ticket o)
        {
            DataTable dt;
            try
            {
                dt = execQuery("select home from DataBetList where home ='" + o.home + "' and away='" + o.away + "' and bettype='" + o.bettype + "' and hdp='" + o.hdp + "' and odd='" + o.odd + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }
        //execNonQuery("DELETE FROM AccBet");
        public bool ticketExists(ticket o, string player)
        {
            DataTable dt;
            string agent = player.Substring(0, player.Length - 3);
            try
            {
                dt = execQuery("select home from DataBetList where member = '" + player + "' and home ='" + o.home + "' and away='" + o.away + "' and bettype='" + o.bettype + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
            return false;                
        }
        public bool DeleteTicket()
        {
            execNonQuery("DELETE FROM DataBetList");
            execNonQuery("DBCC CHECKIDENT ('DataBetList', RESEED, 0);");
            //DBCC CHECKIDENT ('[MyTable]', RESEED, 0);
            return true;
        }
    }
}
