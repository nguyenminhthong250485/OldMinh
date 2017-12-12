using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace XanhDo
{
    public class Database
    {
        private SqlConnection conn;     
        string sqlserver = "kyuc.ddns.me";

        public Database()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = conn.ConnectionString = "Server=" + sqlserver + ";Database=DBXanhDo;User ID=xanhdo_client;Password=xanhdo13579135;Max Pool Size=250; Connection Timeout=300";
                conn.Open();
            }
            catch (Exception)
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
                da.SelectCommand.CommandTimeout = 5000;
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
                cmd.CommandTimeout = 5000;
                ret = cmd.ExecuteNonQuery();
                closeConnect();
            }
            catch (SqlException e)
            {
                if (e.Number != 2627)
                    throw;
            }
            return ret;
        }

        public member loginMember(string login, string password)
        {
            try
            {
                member o;
                DataTable dt = execQuery("select top 1 * from member where login = '" + login.Replace("'", "''") + "' and password='" + password.Replace("'", "''") + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    o = new member();
                    o.id = dt.Rows[0]["id"].ToString();
                    o.name = dt.Rows[0]["name"].ToString();
                    o.login = dt.Rows[0]["login"].ToString();
                    o.password = dt.Rows[0]["password"].ToString();
                    o.agentid = dt.Rows[0]["agentid"].ToString();
                    return o;
                }
                return null;    
            }
            catch (Exception)
            {
                return null;
            }
        }

        public agent loginAgent(string login, string password)
        {
            try
            {
                agent o;
                DataTable dt = execQuery("select top 1 * from agent where login = '" + login.Replace("'", "''") + "' and password='" + password.Replace("'", "''") + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    o = new agent();
                    o.id = dt.Rows[0]["id"].ToString();
                    o.name = dt.Rows[0]["name"].ToString();
                    o.login = dt.Rows[0]["login"].ToString();
                    o.password = dt.Rows[0]["password"].ToString();
                    o.superid = dt.Rows[0]["superid"].ToString();
                    return o;
                }
                return null;
               
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int doInsertMatch(string companyid, int number, string agentid)
        {
            return execNonQuery("Insert into match(companyid, no, agentid) values('" + companyid + "', " + number + ",'" + agentid.Replace("'", "''") + "')");
        }

        public match getCurrentMatch(string companyid, string agentid)
        {
            try
            {
                match o = new match();
                DataTable dt = execQuery("select top 1 * from vCurrentMatch where companyid = '" + companyid + "' and agentid='" + agentid.Replace("'", "''") + "' order by no desc").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    o.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    o.companyid = dt.Rows[0]["companyid"].ToString();
                    o.no = Int32.Parse(dt.Rows[0]["no"].ToString());
                    return o;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int doInsertTicket(List<ticket> lst)
        {
            string strValues = "";
            foreach (ticket o in lst)
                strValues += o.toSql() + ",";
            strValues = strValues.Substring(0, strValues.Length - 1);
            return execNonQuery("insert into ticket(memberid,matchid,game,money) values " + strValues);
        }

        public int doInsertTicket(ticket o)
        {
            DataTable dt = execQuery("select top 1 * from match where id=" + o.matchid + " and redodds is null").Tables[0];
            if (dt.Rows.Count > 0)
                return execNonQuery("insert into ticket(memberid,name,matchid,companyid,game,money) values " + o.toSql());
            else
                return -99;
        }

        public DataTable getCurrentWinLoss(string companyid, int match_no, string agentid, string date)
        {
            return execQuery(@"SELECT [memberid] as [Máy]
                                  ,[name] as [Tên]
                                  ,[companyid] as [Sân]
                                  ,[matchid] as [Trận số]
                                  ,[game] as [Chọn]
                                  ,[TotalMoney] as [Tổng tiền]
                                  ,[TotalWinLoss] as [WinLoss]                                  
                              FROM[DBXanhDo].[dbo].[vCurrentWinLoss] v
                              inner join match m on v.matchid = m.id
                              and m.companyid='" + companyid + "' and m.no = " + match_no + " and cast(m.systemdate as date) = '"+date+"' Where v.agentid='" + agentid + "'").Tables[0];
        }

        public int doUpdateOdd(string companyid, int match_no, double redodds, double blueodds, double greenodds)
        {
            return execNonQuery("update match set redodds=" + redodds + ", blueodds=" + blueodds + ", greenodds=" + greenodds + " where id = (select top 1 id from match where companyid='" + companyid + "' and no=" + match_no + " order by id desc)");
        }

        public int doUpdateResult(string companyid, int match_no, int result)
        {
            return execNonQuery("update match set result=" + result + ", status='1' where id = (select top 1 id from match where companyid='" + companyid + "' and no=" + match_no + " order by id desc)");
        }

        public int doExecuteSP_UpdateTicket(int matchid)
        {
            int ret = 1;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("SP_UpdateTicket", conn);
                cmd.CommandTimeout = 15000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("SP_UpdateTicket", SqlDbType.Int).Value = matchid;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                ret = -1;
            }
            finally { conn.Close(); }
            return ret;
        }
    }
}
