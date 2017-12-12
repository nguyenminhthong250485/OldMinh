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
        public int doInsertTicket(string type, string member, string bettype, string hdp, string home, string away, string keo, string odd, string money, string usd, string betgroup, string phieuchung)
        {
            string sql = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, {9}, '{10}', '{11}'", type, member, bettype, hdp, home, away, keo, odd, money, usd, betgroup, phieuchung);
            return execNonQuery(@"INSERT INTO [BetList]
                           ([type]
                           ,[member]
                           ,[bettype]
                           ,[hdp]
                           ,[home]
                           ,[away]
                           ,[keo]
                           ,[odd],[money],[usd],[betgroup],[phieuchung])
                     VALUES
                           (" + sql + ")");
        }
        public DataTable getCompareAccount(string str_group = "")
        {
            if (str_group == "")
            {
                return execQuery("Select * from CompareAccount where id = 'a,a' or id = 'under'").Tables[0];
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
        public DataTable getSheetName()
        {
            return execQuery("select name from SheetName").Tables[0];
        }
        public DataTable getBetGroup(string sheetname, string type)
        {
            return execQuery("select distinct betgroup from SheetBet where sheetname='" + sheetname + "'").Tables[0];
        }
        public DataTable getBetMoney(string sheetname)
        {
            return execQuery("select distinct money from SheetBet where sheetname='" + sheetname + "'").Tables[0];
        }
        public DataTable getAccountBet(string sheetname, string type)
        {
            return execQuery("select * from SheetBet where sheetname ='" + sheetname + "' and type='" + type + "' and status='" + 1 + "'").Tables[0];
        }
        public DataSet getSheetBet(string sheet)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                ds = new DataSet();
                da = new SqlDataAdapter("select * from SheetBet where sheetname = '" + sheet.Replace("'", "''") + "'", conn);
                da.Fill(ds, "SheetBet");
                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }     
        public void UpdateSheetBet()
        {
            try
            {
                da.Update(ds, "SheetBet");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int doInsertSheetName(string name)
        {
            return execNonQuery("insert into SheetName values('" + name.Replace("'", "''") + "')");
        }
        public bool ticketExists(ticket o, string player, bool allowbetsameagent,  int count = 1)
        {
            DataTable dt;
            string agent = player.Substring(0, player.Length - 3);
            try
            {
                if (allowbetsameagent == false)
                {
                    //dt = execQuery("select home, away, hdp, bettype from BetList where member like '" + agent + "%' and home ='" + o.home + "' and away='" + o.away + "' and hdp='" + o.hdp + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                    dt = execQuery("select home, away, hdp, bettype from BetList where member like '" + agent + "%' and home ='" + o.home + "' and away='" + o.away + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                }
                else
                {
                    //dt = execQuery("select home, away, hdp, bettype from BetList where member = '" + player + "' and home ='" + o.home + "' and away='" + o.away + "' and hdp='" + o.hdp + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                    dt = execQuery("select home, away, hdp, bettype from BetList where member = '" + player + "' and home ='" + o.home + "' and away='" + o.away + "' and DATEDIFF(s,'1970-1-1',GETDATE())-DATEDIFF(s,'1970-1-1',systemdate) <= 43200").Tables[0];
                }
                if (dt.Rows.Count >= count)
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
        public DataTable checkIP()
        {
            return execQuery(@"select ip, username
                    from [Humbuger].[dbo].[SheetBet] 
                    where ip in(
                      SELECT ip
                      FROM [Humbuger].[dbo].[SheetBet] 
                      group by ip                            
                      having count(ip) > 1)
                      order by ip").Tables[0];
                            }
        public DataTable getMinus(string ids)
        {
            if (ids.Length > 0 && ids.EndsWith(","))
                ids = ids.Substring(0, ids.Length - 1);
            return execQuery(@"SELECT [id]
                              ,[keo]
                              ,[data1]
                              ,[data2]
                              ,[currentdate]
                              ,[systemdate]
                          FROM [Humbuger].[dbo].[Minus] where id in (" + ids.Replace("'", "''") + ")").Tables[0];
        }
        public DataTable getMinus(int fromid, int toid)
        {
            return execQuery(@"SELECT [id]
                              ,[keo]
                              ,[data1]
                              ,[data2]
                              ,[currentdate]
                              ,[systemdate]
                          FROM[Humbuger].[dbo].[Minus] where id >= "+ fromid + " and id <= "+ toid).Tables[0];
        }
        public DataTable getMinusData2(string ids)
        {
            if (ids.Length > 0 && ids.EndsWith(","))
                ids = ids.Substring(0, ids.Length - 1);
            return execQuery(@"select id, [data2]                              
                          FROM[Humbuger].[dbo].[Minus] where id in (" + ids.Replace("'", "''") + ") and data2 is not null").Tables[0];
        }
        public string getMinusData2ById(int id)
        {
            DataTable dt = execQuery(@"select id, [data2] FROM [Humbuger].[dbo].[Minus] where id = " + id + " and data2 is not null").Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["data2"].ToString();
            return "";
        }
        public int doInsertBetListMinus(int id, string member, string data, string phieuchung)
        {
            return execNonQuery(@"INSERT INTO [Humbuger].[dbo].[BetListMinus]([id],[member1],[data1],[member2],[data2],[phieuchung]) VALUES(" + id + ",'" + member.Replace("'", "''") + "','" + data.Replace("'", "''") + "', '','','" + phieuchung.Replace("'", "''") + "')");
        }
        public int doUpdateBetListMinus(int id, string member, string data)
        {
            return execNonQuery(@"UPDATE [Humbuger].[dbo].[BetListMinus] set member2 = '" + member.Replace("'", "''") + "', data2 = '" + data.Replace("'", "''") + "' WHERE id = " + id);
        }
        public DataTable getBetListMinus(string ids)
        {
            if (ids.Length > 0 && ids.EndsWith(","))
                ids = ids.Substring(0, ids.Length - 1);
            return execQuery(@"SELECT [id]
                              ,[member1]
                              ,[data1]
                              ,[member2]
                              ,[data2]
                              ,[systemdate]
                              ,[phieuchung]
                          FROM [Humbuger].[dbo].[BetListMinus] where id in (" + ids.Replace("'", "''") + ") and member2 = ''").Tables[0];           
        }
        public objBetListMinus getBetListMinusById(int id)
        {
            DataTable dt = execQuery(@"SELECT [id]
                              ,[member1]
                              ,[data1]
                              ,[member2]
                              ,[data2]
                              ,DATEDIFF(s,'1970-1-1',[systemdate]) as [systemdate]
                              ,[phieuchung]
                          FROM [Humbuger].[dbo].[BetListMinus] Where id=" + id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                objBetListMinus o = new objBetListMinus();
                o.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                o.member1 = dt.Rows[0]["member1"].ToString();
                o.data1 = dt.Rows[0]["data1"].ToString();
                o.member2 = dt.Rows[0]["member2"].ToString();
                o.data2 = dt.Rows[0]["data2"].ToString();
                o.systemdate = dt.Rows[0]["systemdate"].ToString();
                o.phieuchung = dt.Rows[0]["phieuchung"].ToString();

                return o;
            }
            return null;
        }
        public bool ticketMinusExists(int id)
        {
            DataTable dt = execQuery("select id from BetListMinus where id = " + id).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            return false;
        }
    }
}
