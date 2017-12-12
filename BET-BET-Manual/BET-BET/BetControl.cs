using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace BET_BET
{
    public partial class BetControl : UserControl
    {
        string str_CompareNonLive_AA = "";
        string str_CompareLive_AA = "";       
        string str_CompareLive_Under = "";

        DataTable dtSBO, dtIBET;
        Thread tSbo, tIbet, tBetNonLive, tBetLive, tSboClick, tIbetClick, tData2, tData1, tUnder;
        Database db;
        Random ran = new Random();

        public BetControl()
        {
            InitializeComponent();
            db = new Database();
        }
        private void BetControl_Load(object sender, EventArgs e)
        {
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;

            loadDataToComboBox();
        }
        private void loadDataToComboBox()
        {
            cboSheet.DataSource = db.getSheetName();
            cboSheet.Name = "name";
            cboSheet.ValueMember = "name";
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            BetManager.clearHashTable();
            createTableSBO(db.getAccountBet(cboSheet.Text, "sbo"));
            createTableIBET(db.getAccountBet(cboSheet.Text, "ibet"));
        }
        private string getCommonMoney(string money1, string money2)
        {
            string common = "";
            List<string> lst1 = money1.Split(',').ToList();
            List<string> lst2 = money2.Split(',').ToList();
            foreach (string str in lst2)
            {
                if (lst1.Contains(str))
                    common += str + ",";
            }
            if (common != "")
                common = common.Substring(0, common.Length - 1);
            return common;
        }
        private void createTableSBO(DataTable dt)
        {
            dataGridViewSBO.Columns.Clear();
            dtSBO = new DataTable();
            dtSBO.Columns.Add("key");
            dtSBO.PrimaryKey = new DataColumn[] { dtSBO.Columns["key"] };
            dtSBO.Columns.Add("betgroup");
            dtSBO.Columns.Add("ip");
            dtSBO.Columns.Add("username");
            dtSBO.Columns.Add("usd");
            dtSBO.Columns.Add("credit");
            dtSBO.Columns.Add("message");
            dtSBO.Columns.Add("profit");
            dtSBO.Columns.Add("league");
            dtSBO.Columns.Add("money");
            dtSBO.Columns.Add("betmode");

            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                index++;
                dtSBO.Rows.Add(new Object[] { "sbo" + index, dr["betgroup"], dr["ip"], dr["username"], dr["usd"], "", "", dr["profit"], dr["league"], dr["money"], dr["betmode"] });
            }

            dataGridViewSBO.DataSource = dtSBO;

            dataGridViewSBO.Columns["key"].HeaderText = "KEY";
            dataGridViewSBO.Columns["betgroup"].HeaderText = "GROUP";
            dataGridViewSBO.Columns["ip"].HeaderText = "IP";
            dataGridViewSBO.Columns["username"].HeaderText = "USERNAME";
            dataGridViewSBO.Columns["usd"].HeaderText = "USD";
            dataGridViewSBO.Columns["credit"].HeaderText = "CREDIT";
            dataGridViewSBO.Columns["message"].HeaderText = "MESSAGE";
            dataGridViewSBO.Columns["profit"].HeaderText = "PROFIT";
            dataGridViewSBO.Columns["league"].HeaderText = "LEAGUE";
            dataGridViewSBO.Columns["money"].HeaderText = "MONEY";
            dataGridViewSBO.Columns["betmode"].HeaderText = "MODE";

            dataGridViewSBO.Columns["key"].ReadOnly = true;
            dataGridViewSBO.Columns["betgroup"].ReadOnly = true;
            dataGridViewSBO.Columns["ip"].ReadOnly = true;
            dataGridViewSBO.Columns["username"].ReadOnly = true;
            dataGridViewSBO.Columns["usd"].ReadOnly = true;
            dataGridViewSBO.Columns["credit"].ReadOnly = true;
            dataGridViewSBO.Columns["message"].ReadOnly = true;
            dataGridViewSBO.Columns["betmode"].ReadOnly = true;

            dataGridViewSBO.Columns["key"].Width = 38;
            dataGridViewSBO.Columns["betgroup"].Width = 50;
            dataGridViewSBO.Columns["ip"].Width = 85;
            dataGridViewSBO.Columns["username"].Width = 80;
            dataGridViewSBO.Columns["usd"].Width = 30;
            dataGridViewSBO.Columns["credit"].Width = 80;
            dataGridViewSBO.Columns["message"].Width = 120;
            dataGridViewSBO.Columns["profit"].Width = 50;
            dataGridViewSBO.Columns["league"].Width = 50;
            dataGridViewSBO.Columns["money"].Width = 110;
            dataGridViewSBO.Columns["betmode"].Width = 70;

            DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
            colButton.HeaderText = "BET LIST";
            colButton.Name = "betlist";
            colButton.Text = "View";
            colButton.Width = 80;
            colButton.UseColumnTextForButtonValue = true;
            dataGridViewSBO.Columns.Add(colButton);

            DataGridViewButtonColumn colButtonLogin = new DataGridViewButtonColumn();
            colButtonLogin.HeaderText = "Login";
            colButtonLogin.Name = "dologin";
            colButtonLogin.Text = "Login";
            colButtonLogin.Width = 80;
            colButtonLogin.UseColumnTextForButtonValue = true;
            dataGridViewSBO.Columns.Add(colButtonLogin);

            foreach (DataGridViewColumn col in dataGridViewSBO.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            dataGridViewSBO.Columns["key"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSBO.Columns["betgroup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSBO.Columns["credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewSBO.Columns["usd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void createTableIBET(DataTable dt)
        {
            dataGridViewIBET.Columns.Clear();
            dtIBET = new DataTable();
            dtIBET.Columns.Add("key");
            dtIBET.PrimaryKey = new DataColumn[] { dtIBET.Columns["key"] };
            dtIBET.Columns.Add("betgroup");
            dtIBET.Columns.Add("ip");
            dtIBET.Columns.Add("username");
            dtIBET.Columns.Add("usd");
            dtIBET.Columns.Add("credit");
            dtIBET.Columns.Add("message");
            dtIBET.Columns.Add("profit");
            dtIBET.Columns.Add("league");
            dtIBET.Columns.Add("money");
            dtIBET.Columns.Add("betmode");

            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                index++;
                dtIBET.Rows.Add(new Object[] { "ibet" + index, dr["betgroup"], dr["ip"], dr["username"], dr["usd"], "", "", dr["profit"], dr["league"], dr["money"], dr["betmode"] });
            }

            dataGridViewIBET.DataSource = dtIBET;

            dataGridViewIBET.Columns["key"].HeaderText = "KEY";
            dataGridViewIBET.Columns["betgroup"].HeaderText = "GROUP";
            dataGridViewIBET.Columns["ip"].HeaderText = "IP";
            dataGridViewIBET.Columns["username"].HeaderText = "USERNAME";
            dataGridViewIBET.Columns["usd"].HeaderText = "USD";
            dataGridViewIBET.Columns["credit"].HeaderText = "CREDIT";
            dataGridViewIBET.Columns["message"].HeaderText = "MESSAGE";
            dataGridViewIBET.Columns["profit"].HeaderText = "PROFIT";
            dataGridViewIBET.Columns["league"].HeaderText = "LEAGUE";
            dataGridViewIBET.Columns["money"].HeaderText = "MONEY";
            dataGridViewIBET.Columns["betmode"].HeaderText = "MODE";

            dataGridViewIBET.Columns["key"].ReadOnly = true;
            dataGridViewIBET.Columns["betgroup"].ReadOnly = true;
            dataGridViewIBET.Columns["ip"].ReadOnly = true;
            dataGridViewIBET.Columns["username"].ReadOnly = true;
            dataGridViewIBET.Columns["usd"].ReadOnly = true;
            dataGridViewIBET.Columns["credit"].ReadOnly = true;
            dataGridViewIBET.Columns["message"].ReadOnly = true;

            dataGridViewIBET.Columns["key"].Width = 38;
            dataGridViewIBET.Columns["betgroup"].Width = 50;
            dataGridViewIBET.Columns["ip"].Width = 85;
            dataGridViewIBET.Columns["username"].Width = 80;
            dataGridViewIBET.Columns["usd"].Width = 30;
            dataGridViewIBET.Columns["credit"].Width = 80;
            dataGridViewIBET.Columns["message"].Width = 120;
            dataGridViewIBET.Columns["profit"].Width = 50;
            dataGridViewIBET.Columns["league"].Width = 50;
            dataGridViewIBET.Columns["money"].Width = 110;
            dataGridViewIBET.Columns["betmode"].Width = 70;

            DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
            colButton.HeaderText = "BET LIST";
            colButton.Name = "betlist";
            colButton.Text = "View";
            colButton.Width = 80;
            colButton.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(colButton);

            DataGridViewButtonColumn colButtonLogin = new DataGridViewButtonColumn();
            colButtonLogin.HeaderText = "Login";
            colButtonLogin.Name = "dologin";
            colButtonLogin.Text = "Login";
            colButtonLogin.Width = 80;
            colButtonLogin.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(colButtonLogin);

            foreach (DataGridViewColumn col in dataGridViewIBET.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            dataGridViewIBET.Columns["key"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewIBET.Columns["betgroup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewIBET.Columns["credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewIBET.Columns["usd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void dataGridViewSBO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tSboClick = new Thread(delegate ()
            {
                if (e.ColumnIndex == dataGridViewSBO.Columns["betlist"].Index)
                {
                    string title = dataGridViewSBO["username", e.RowIndex].Value.ToString();
                    string key = dataGridViewSBO["key", e.RowIndex].Value.ToString();

                    Browser web = new Browser(title, BetManager.getBet(key).getBetList());
                    web.Show();
                    System.Windows.Threading.Dispatcher.Run();
                }
                else if (e.ColumnIndex == dataGridViewSBO.Columns["dologin"].Index)
                {
                    string key = dataGridViewSBO["key", e.RowIndex].Value.ToString();
                    string ip = dataGridViewSBO["ip", e.RowIndex].Value.ToString();
                    string username = dataGridViewSBO["username", e.RowIndex].Value.ToString();
                    string usd = dataGridViewSBO["usd", e.RowIndex].Value.ToString();
                    BetManager.createBet("sbo", key, ip, username, usd);
                    BetManager.getBet(key).login();
                    dataGridViewSBO["credit", e.RowIndex].Value = BetManager.getBet(key).getCredit();
                    dataGridViewSBO["message", e.RowIndex].Value = BetManager.getBet(key).getMessage();
                }
            });
            tSboClick.SetApartmentState(ApartmentState.STA);
            tSboClick.IsBackground = true;
            tSboClick.Start();
        }
        private void dataGridViewIBET_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tIbetClick = new Thread(delegate ()
            {
                try
                {
                    if (e.ColumnIndex == dataGridViewIBET.Columns["betlist"].Index)
                    {
                        string title = dataGridViewIBET["username", e.RowIndex].Value.ToString();
                        string key = dataGridViewIBET["key", e.RowIndex].Value.ToString();

                        Browser web = new Browser(title, BetManager.getBet(key).getBetList());
                        web.Show();
                        System.Windows.Threading.Dispatcher.Run();
                    }
                    else if (e.ColumnIndex == dataGridViewIBET.Columns["dologin"].Index)
                    {
                        string key = dataGridViewIBET["key", e.RowIndex].Value.ToString();
                        string ip = dataGridViewIBET["ip", e.RowIndex].Value.ToString();
                        string username = dataGridViewIBET["username", e.RowIndex].Value.ToString();
                        string usd = dataGridViewIBET["usd", e.RowIndex].Value.ToString();
                        BetManager.createBet("ibet", key, ip, username, usd);
                        BetManager.getBet(key).login();
                        dataGridViewIBET["credit", e.RowIndex].Value = BetManager.getBet(key).getCredit();
                        dataGridViewIBET["message", e.RowIndex].Value = BetManager.getBet(key).getMessage();
                    }
                }
                catch (Exception)
                {
                }
            });
            tIbetClick.SetApartmentState(ApartmentState.STA);
            tIbetClick.IsBackground = true;
            tIbetClick.Start();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            tSbo = new Thread(delegate ()
            {
                picSBO.Enabled = true;
                foreach (DataRow dr in dtSBO.Rows)
                {
                    BetManager.createBet("sbo", dr["key"].ToString(), dr["ip"].ToString(), dr["username"].ToString(), dr["usd"].ToString());
                    BetManager.getBet(dr["key"].ToString()).login();
                    dr["credit"] = BetManager.getBet(dr["key"].ToString()).getCredit();
                    dr["message"] = BetManager.getBet(dr["key"].ToString()).getMessage();
                    Thread.Sleep(100);
                }
                picSBO.Enabled = false;
            });
            tSbo.Start();
        }
        private void updateMessage(string key, string message)
        {
            if (key.Contains("ibet"))
                dataGridViewIBET["message", findRowByKey(dataGridViewIBET, key)].Value = message;
            else
                dataGridViewSBO["message", findRowByKey(dataGridViewSBO, key)].Value = message;
        }
        private void updateCredit(string key, string credit)
        {
            if (key.Contains("ibet"))
                dataGridViewIBET["credit", findRowByKey(dataGridViewIBET, key)].Value = credit;
            else
                dataGridViewSBO["credit", findRowByKey(dataGridViewSBO, key)].Value = credit;
        }

        private void btnBetUnder_Click(object sender, EventArgs e)
        {
            if (btnBetUnder.Text == "BET UNDER")
            {
                tUnder = new Thread(delegate ()
                {
                    bool betsuccess = false;
                    picIBET.Enabled = true;
                    picSBO.Enabled = true;
                    btnBetUnder.Text = "STOP";
                    string str_CompareLive_Under_TEMP = "";
                    List<objCompare> lstCompare;
                    while (true)
                    {
                        updateCompare();
                        if (str_CompareLive_Under_TEMP != str_CompareLive_Under)
                        {
                            str_CompareLive_Under_TEMP = str_CompareLive_Under;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        string[] mangchienthuat = richChienThuat.Text.Split('\n');
                        foreach (string chienthuat in mangchienthuat)
                        {
                            if (chienthuat.Trim() != "")
                            {
                                richLog.Text = richLog.Text.Insert(0, "===========================\n");
                                bool betsameagent = false;
                                string keyIbet = "ibet" + chienthuat.Split(',')[0];
                                string keySbo = "sbo" + chienthuat.Split(',')[1];
                                if (chienthuat.Split(',').Length > 2) betsameagent = true;
                                updateMessage(keyIbet, "");
                                updateMessage(keySbo, "");
                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                info infoIbet = getInfoByKey(keyIbet);
                                info infoSbo = getInfoByKey(keySbo);                              

                                if (infoIbet != null && infoSbo != null)
                                {
                                    if (infoIbet.money == "" || infoSbo.money == "")
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check money setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.profit != infoSbo.profit)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check profit setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.league != infoSbo.league)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check league setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    string commonmoney = getCommonMoney(infoIbet.money, infoSbo.money);
                                    if (commonmoney != "")
                                    {
                                        string money = commonmoney.Split(',')[ran.Next(0, commonmoney.Split(',').Length)];
                                        if (double.Parse(money) / double.Parse(infoIbet.usd) > double.Parse(infoIbet.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        if (double.Parse(money) / double.Parse(infoSbo.usd) > double.Parse(infoSbo.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        string str_betgroup = infoSbo.betgroup + "," + infoIbet.betgroup;
                                        updateCompare();
                                        lstCompare = UtilSoccer.convertCompareToList(str_CompareLive_Under, 1);
                                        foreach (objCompare oCompare in lstCompare)
                                        {
                                            if (oCompare != null)
                                            {
                                                int profit = Convert.ToInt32(Math.Abs(oCompare.profit * 100));
                                                int league = Convert.ToInt32(Math.Abs(oCompare.league * 100));
                                                string ibetLeague = "," + infoIbet.league + ",";
                                                if (profit > Int32.Parse(infoIbet.profit))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, profit is {2} is great than {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, profit, infoIbet.profit));
                                                    continue;
                                                }
                                                else if (!ibetLeague.Contains(league.ToString()))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, league is {2} is not in {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, league, infoIbet.league));
                                                    continue;
                                                }
                                                else
                                                {
                                                    string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                                                    string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;
                                                    
                                                    ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                                                    ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);

                                                    if (chkDuoi.Checked && (oIbet.bettype == "3" || oIbet.bettype == "9"))
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, is not keo duoi\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                                        continue;
                                                    }

                                                    if (db.ticketExists(oIbet, infoIbet.username, betsameagent, 2) == true || db.ticketExists(oSbo, infoSbo.username, betsameagent, 2) == true)
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, bet list exists\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                                        continue;
                                                    }                                                  

                                                    #region Bet                                                      
                                                    if (oIbet.choose == "a")
                                                    {
                                                        BetManager.getBet(keyIbet).playBetUnder(oIbet, money, "1", "", str_betgroup);
                                                        if (BetManager.getBet(keyIbet).getPhieuchung() != "")
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("UNDER --- {0} {1}-vs-{2}, {3}, IBET ({6}): {4} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                            updateMessage(keyIbet, "Done");
                                                            updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                                            betsuccess = true;
                                                        }
                                                        else
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("UNDER --- {0} {1}-vs-{2}, {3}, IBET ({6}): {4} (Bet failed). Error: {8}\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username, BetManager.getBet(keyIbet).getMessage()));
                                                            updateMessage(keyIbet, "");
                                                        }
                                                    }
                                                    else if (oSbo.choose == "a")
                                                    {
                                                        string phieuchung = mylib.generateID("T");
                                                        BetManager.getBet(keySbo).playBetUnder(oSbo, money, "1", phieuchung, str_betgroup);
                                                        if (BetManager.getBet(keySbo).getPhieuchung() != "")
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("UNDER --- {0} {1}-vs-{2}, {3}, SBO ({7}): {5} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                            updateMessage(keySbo, "Done");
                                                            updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                                            betsuccess = true;
                                                        }
                                                        else
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("UNDER --- {0} {1}-vs-{2}, {3}, SBO ({6}): {4} (Bet failed). Error: {8}\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username, BetManager.getBet(keySbo).getMessage()));
                                                            updateMessage(keySbo, "");
                                                        }
                                                    }                                           
                                                    #endregion
                                                }
                                            }
                                            if (betsuccess)
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, no common money\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                    updateMessage(keyIbet, "");
                                    updateMessage(keySbo, "");
                                }
                            }
                            if (betsuccess)
                                break;
                        }     
                        Thread.Sleep(1000);
                    }                    
                });
                tUnder.Start();
            }
            else
            {
                btnBetUnder.Text = "BET UNDER";
                picIBET.Enabled = false;
                picSBO.Enabled = false;
                tUnder.Abort();
            }
        }     
        private info getInfoByKey(string key)
        {
            int index = -1;
            info o = new info();
            try
            {
                if (key.Contains("ibet"))
                {
                    index = findRowByKey(dataGridViewIBET, key);
                    if (index > -1)
                    {
                        o.key = key;
                        o.betgroup = dataGridViewIBET["betgroup", index].Value.ToString().Trim();
                        o.username = dataGridViewIBET["username", index].Value.ToString().Trim();
                        o.usd = dataGridViewIBET["usd", index].Value.ToString().Trim();
                        o.credit = dataGridViewIBET["credit", index].Value.ToString().Trim();
                        o.profit = dataGridViewIBET["profit", index].Value.ToString().Trim();
                        o.league = dataGridViewIBET["league", index].Value.ToString().Trim();
                        o.money = dataGridViewIBET["money", index].Value.ToString().Trim();
                        o.mode = dataGridViewIBET["betmode", index].Value.ToString().Trim();
                    }
                }
                else
                {
                    index = findRowByKey(dataGridViewSBO, key);
                    if (index > -1)
                    {
                        o.key = key;
                        o.betgroup = dataGridViewSBO["betgroup", index].Value.ToString().Trim();
                        o.username = dataGridViewSBO["username", index].Value.ToString().Trim();
                        o.usd = dataGridViewSBO["usd", index].Value.ToString().Trim();
                        o.credit = dataGridViewSBO["credit", index].Value.ToString().Trim();
                        o.profit = dataGridViewSBO["profit", index].Value.ToString().Trim();
                        o.league = dataGridViewSBO["league", index].Value.ToString().Trim();
                        o.money = dataGridViewSBO["money", index].Value.ToString().Trim();
                        o.mode = dataGridViewSBO["betmode", index].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                return null;
            }
            return o;
        }
        private int findRowByKey(DataGridView drv, string key)
        {
            int rowIndex = -1;
            try
            {
                DataGridViewRow row = drv.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["key"].Value.ToString().Equals(key))
                        .First();
                rowIndex = row.Index;
            }
            catch
            {
                return -1;
            }
            return rowIndex;
        }       
        private void btnLoginIBET_Click(object sender, EventArgs e)
        {
            tIbet = new Thread(delegate ()
            {
                picIBET.Enabled = true;
                foreach (DataRow dr in dtIBET.Rows)
                {
                    BetManager.createBet("ibet", dr["key"].ToString(), dr["ip"].ToString(), dr["username"].ToString(), dr["usd"].ToString());
                    BetManager.getBet(dr["key"].ToString()).login();
                    dr["credit"] = BetManager.getBet(dr["key"].ToString()).getCredit();
                    dr["message"] = BetManager.getBet(dr["key"].ToString()).getMessage();
                    Thread.Sleep(100);
                }
                picIBET.Enabled = false;
            });
            tIbet.Start();
        }
        private void updateCompare()
        {
            DataTable dtCompareAccount = db.getCompareAccount();
            foreach (DataRow dr in dtCompareAccount.Rows)
            {
                try
                {
                    if (dr["id"].ToString() == "a,a")
                    {
                        str_CompareNonLive_AA = dr["CompareNonLive"].ToString().Trim();
                        str_CompareLive_AA = dr["CompareLive"].ToString().Trim();
                    }
                    else if (dr["id"].ToString() == "under")
                    {
                        str_CompareLive_Under = dr["CompareLive"].ToString().Trim();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void btnBetLive_Click(object sender, EventArgs e)
        {
            if (btnBetLive.Text == "BET LIVE")
            {
                tBetLive = new Thread(delegate ()
                {
                    bool betsuccess = false;
                    picIBET.Enabled = true;
                    picSBO.Enabled = true;
                    btnBetLive.Text = "STOP";
                    string str_CompareLive_AA_TEMP = "";
                    List<objCompare> lstCompare;
                    while (true)
                    {                                              
                        updateCompare();
                        if (str_CompareLive_AA_TEMP != str_CompareLive_AA)
                        {
                            str_CompareLive_AA_TEMP = str_CompareLive_AA;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        string[] mangchienthuat = richChienThuat.Text.Split('\n');
                        foreach (string chienthuat in mangchienthuat)
                        {
                            if (chienthuat.Trim() != "")
                            {                               
                                richLog.Text = richLog.Text.Insert(0, "===========================\n");
                                bool betsameagent = false;
                                string keyIbet = "ibet" + chienthuat.Split(',')[0];
                                string keySbo = "sbo" + chienthuat.Split(',')[1];
                                if (chienthuat.Split(',').Length > 2) betsameagent = true;
                                updateMessage(keyIbet, "");
                                updateMessage(keySbo, "");
                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                info infoIbet = getInfoByKey(keyIbet);
                                info infoSbo = getInfoByKey(keySbo);

                                if (infoIbet != null && infoSbo != null)
                                {
                                    if (infoIbet.money == "" || infoSbo.money == "")
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check money setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.profit != infoSbo.profit)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check profit setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.league != infoSbo.league)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check league setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    string commonmoney = getCommonMoney(infoIbet.money, infoSbo.money);
                                    if (commonmoney != "")
                                    {
                                        string money = commonmoney.Split(',')[ran.Next(0, commonmoney.Split(',').Length)];
                                        if (double.Parse(money) / double.Parse(infoIbet.usd) > double.Parse(infoIbet.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        if (double.Parse(money) / double.Parse(infoSbo.usd) > double.Parse(infoSbo.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        string str_betgroup = infoSbo.betgroup + "," + infoIbet.betgroup;
                                        updateCompare();
                                        lstCompare = UtilSoccer.convertCompareToList(str_CompareLive_AA, 1);
                                        foreach (objCompare oCompare in lstCompare)
                                        {
                                            if (oCompare != null)
                                            {
                                                int profit = Convert.ToInt32(Math.Abs(oCompare.profit * 100));
                                                int league = Convert.ToInt32(Math.Abs(oCompare.league * 100));
                                                string ibetLeague = "," + infoIbet.league + ",";
                                                if (profit > Int32.Parse(infoIbet.profit))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, profit is {2} is great than {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, profit, infoIbet.profit));
                                                    continue;
                                                }
                                                else if (!ibetLeague.Contains(league.ToString()))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, league is {2} is not in {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, league, infoIbet.league));
                                                    continue;
                                                }
                                                else
                                                {
                                                    string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                                                    string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;

                                                    bool betIbet = true;
                                                    bool betSbo = true;

                                                    ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                                                    ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);

                                                    if (db.ticketExists(oIbet, infoIbet.username, betsameagent) == true || db.ticketExists(oSbo, infoSbo.username, betsameagent) == true)
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, bet list exists\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                                        continue;
                                                    }
                                                    #region Check Odd on Groups
                                                    if (infoIbet.betgroup == "c")
                                                    {
                                                        double ibetOddBet = 0;
                                                        try
                                                        {
                                                            ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "1"));
                                                            int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                                            if (deviationOddIbet > 1)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddIbet));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd));
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoIbet.betgroup == "d")
                                                    {
                                                        double ibetOddBet = 0;
                                                        try
                                                        {
                                                            ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "1"));
                                                            int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                                            if (deviationOddIbet > 2)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddIbet));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd));
                                                            continue;
                                                        }
                                                    }
                                                    if (infoSbo.betgroup == "c")
                                                    {
                                                        double sboOddBet = 0;
                                                        try
                                                        {
                                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "1"));
                                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                                            if (deviationOddSbo > 1)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddSbo));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd));
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoSbo.betgroup == "a")
                                                    {
                                                        double sboOddBet = 0;
                                                        try
                                                        {
                                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "1"));
                                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                                            if (deviationOddSbo > 0)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddSbo));
                                                                continue;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd));
                                                            continue;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Check Mode
                                                    if (infoIbet.mode.ToLower() == "sw")
                                                    {
                                                        if (oSbo.odd < 0)
                                                            continue;
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "iw")
                                                    {
                                                        if (oIbet.odd < 0)
                                                            continue;
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "w")
                                                    {
                                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                                        {
                                                            betIbet = true;
                                                            betSbo = false;
                                                        }
                                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                                        {
                                                            betIbet = false;
                                                            betSbo = true;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "l")
                                                    {
                                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                                        {
                                                            betIbet = false;
                                                            betSbo = true;
                                                        }
                                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                                        {
                                                            betIbet = true;
                                                            betSbo = false;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Bet
                                                    string phieuchung = "";
                                                    if (betIbet)
                                                    {
                                                        BetManager.getBet(keyIbet).playBetLive(oIbet, money, "1", "", str_betgroup);
                                                        if (betSbo == false)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, IBET ({6}): {4} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                            updateMessage(keyIbet, "Done");
                                                            updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                                            betsuccess = true;
                                                        }

                                                        if (betSbo == false)
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            phieuchung = BetManager.getBet(keyIbet).getPhieuchung();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        phieuchung = mylib.generateID("T");
                                                    }

                                                    if (betSbo)
                                                    {
                                                        if (phieuchung != "")
                                                        {
                                                            string realmoney = money;
                                                            if (betIbet)
                                                                realmoney = BetManager.getBet(keyIbet).getRealMoney();

                                                            Thread.Sleep(1000);
                                                            BetManager.getBet(keySbo).playBetLive(oSbo, realmoney, "1", phieuchung, str_betgroup);
                                                            if (betIbet == true)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, IBET ({6}): {4}, SBO ({7}): {5} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                                updateMessage(keyIbet, "Done");
                                                                updateMessage(keySbo, "Done");
                                                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                                                betsuccess = true;
                                                            }
                                                            else if (betIbet == false)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, SBO ({7}): {5} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                                updateMessage(keySbo, "Done");
                                                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                                                betsuccess = true;
                                                            }
                                                            break;
                                                        }
                                                    }

                                                    if (betIbet == true && phieuchung == "")
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, BetManager.getBet(keyIbet).getMessage()));
                                                        updateMessage(keyIbet, "");
                                                        updateMessage(keySbo, "");
                                                    }
                                                    #endregion
                                                }
                                            }
                                            if (betsuccess)
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, no common money\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                    updateMessage(keyIbet, "");
                                    updateMessage(keySbo, "");
                                }
                            }
                            if (betsuccess)
                                break;
                        }
                        if (betsuccess)
                            break;
                        Thread.Sleep(1000);
                    }
                    btnBetLive.Text = "BET LIVE";
                    picIBET.Enabled = false;
                    picSBO.Enabled = false;
                });
                tBetLive.Start();
            }
            else
            {
                btnBetLive.Text = "BET LIVE";
                picIBET.Enabled = false;
                picSBO.Enabled = false;
                tBetLive.Abort();
            }
        }
        private void btnBET_Click(object sender, EventArgs e)
        {            
            if (btnBet.Text == "BET NON LIVE")
            {
                tBetNonLive = new Thread(delegate ()
                {
                    bool betsuccess = false;
                    picIBET.Enabled = true;
                    picSBO.Enabled = true;
                    btnBet.Text = "STOP";
                    string str_CompareNonLive_AA_TEMP = "";
                    List<objCompare> lstCompare;
                    while (true)
                    {
                        updateCompare();
                        if (str_CompareNonLive_AA_TEMP != str_CompareNonLive_AA)
                        {
                            str_CompareNonLive_AA_TEMP = str_CompareNonLive_AA;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        string[] mangchienthuat = richChienThuat.Text.Split('\n');
                        foreach (string chienthuat in mangchienthuat)
                        {
                            if (chienthuat.Trim() != "")
                            {
                                richLog.Text = richLog.Text.Insert(0, "===========================\n");
                                bool betsameagent = false;
                                string keyIbet = "ibet" + chienthuat.Split(',')[0];
                                string keySbo = "sbo" + chienthuat.Split(',')[1];
                                double minGia = 0;
                                double maxGia = 10;
                                if (chienthuat.Split(',').Length > 2)
                                {
                                    if (chienthuat.Split(',')[2].Trim() != "")
                                        minGia = double.Parse(chienthuat.Split(',')[2].Trim());
                                    betsameagent = true;
                                }
                                if (chienthuat.Split(',').Length > 3)
                                {
                                    if (chienthuat.Split(',')[3].Trim() != "")
                                        maxGia = double.Parse(chienthuat.Split(',')[3].Trim());
                                }
                                updateMessage(keyIbet, "");
                                updateMessage(keySbo, "");
                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                info infoIbet = getInfoByKey(keyIbet);
                                info infoSbo = getInfoByKey(keySbo);

                                if (infoIbet != null && infoSbo != null)
                                {
                                    if (infoIbet.money == "" || infoSbo.money == "")
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check money setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.profit != infoSbo.profit)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check profit setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    if (infoIbet.league != infoSbo.league)
                                    {
                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, please check league setting\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                        continue;
                                    }

                                    string commonmoney = getCommonMoney(infoIbet.money, infoSbo.money);
                                    if (commonmoney != "")
                                    {
                                        string money = commonmoney.Split(',')[ran.Next(0, commonmoney.Split(',').Length)];
                                        if (double.Parse(money) / double.Parse(infoIbet.usd) > double.Parse(infoIbet.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        if (double.Parse(money) / double.Parse(infoSbo.usd) > double.Parse(infoSbo.credit.Replace(",", "")))
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is wrong, credit is not enough\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                            continue;
                                        }

                                        string str_betgroup = infoSbo.betgroup + "," + infoIbet.betgroup;
                                        updateCompare();
                                        lstCompare = UtilSoccer.convertCompareToList(str_CompareNonLive_AA);
                                        foreach (objCompare oCompare in lstCompare)
                                        {
                                            if (oCompare != null)
                                            {
                                                int profit = Convert.ToInt32(Math.Abs(oCompare.profit * 100));
                                                int league = Convert.ToInt32(Math.Abs(oCompare.league * 100));
                                                string ibetLeague = "," + infoIbet.league + ",";
                                                if (profit > Int32.Parse(infoIbet.profit))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, profit is {2} is great than {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, profit, infoIbet.profit));
                                                    continue;
                                                }
                                                else if (!ibetLeague.Contains(league.ToString()))
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, league is {2} is not in {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, league, infoIbet.league));
                                                    continue;
                                                }
                                                else
                                                {
                                                    string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                                                    string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;

                                                    bool betIbet = true;
                                                    bool betSbo = true;

                                                    ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                                                    ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);

                                                    if (minGia > 0 && maxGia > 0)
                                                    {
                                                        if (oIbet.odd > 0 && (oIbet.odd * 10 < minGia || oIbet.odd * 10 > maxGia))
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Min Odd is {2} and Max Odd is {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, minGia, maxGia));
                                                            continue;
                                                        }
                                                        else if (oSbo.odd > 0 && (oSbo.odd * 10 < minGia || oSbo.odd * 10 > maxGia))
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Min Odd is {2} and Max Odd is {3}\n", DateTime.Now.ToLongTimeString(), chienthuat, minGia, maxGia));
                                                            continue;
                                                        }
                                                    }

                                                    if (db.ticketExists(oIbet, infoIbet.username, betsameagent) == true || db.ticketExists(oSbo, infoSbo.username, betsameagent) == true)
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, bet list exists\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                                        continue;
                                                    }
                                                    #region Check Odd on Groups
                                                    if (infoIbet.betgroup == "c")
                                                    {
                                                        double ibetOddBet = 0;
                                                        try
                                                        {
                                                            ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "0"));
                                                            int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                                            if (deviationOddIbet > 1)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddIbet));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd));
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoIbet.betgroup == "d")
                                                    {
                                                        double ibetOddBet = 0;
                                                        try
                                                        {
                                                            ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "0"));
                                                            int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                                            if (deviationOddIbet > 2)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddIbet));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd IBET error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd));
                                                            continue;
                                                        }
                                                    }
                                                    if (infoSbo.betgroup == "c")
                                                    {
                                                        double sboOddBet = 0;
                                                        try
                                                        {
                                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "0"));
                                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                                            if (deviationOddSbo > 1)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddSbo));
                                                                continue;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd));
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoSbo.betgroup == "a")
                                                    {
                                                        double sboOddBet = 0;
                                                        try
                                                        {
                                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "0"));
                                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                                            if (deviationOddSbo > 0)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO down {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, deviationOddSbo));
                                                                continue;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, Odd SBO error {2}, {3}-{4}, HDP: {5}, BetType: {6}[{7}]\n", DateTime.Now.ToLongTimeString(), chienthuat, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd));
                                                            continue;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Check Mode
                                                    if (infoIbet.mode.ToLower() == "sw")
                                                    {
                                                        if (oSbo.odd < 0)
                                                            continue;
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "iw")
                                                    {
                                                        if (oIbet.odd < 0)
                                                            continue;
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "w")
                                                    {
                                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                                        {
                                                            betIbet = true;
                                                            betSbo = false;
                                                        }
                                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                                        {
                                                            betIbet = false;
                                                            betSbo = true;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else if (infoIbet.mode.ToLower() == "l")
                                                    {
                                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                                        {
                                                            betIbet = false;
                                                            betSbo = true;
                                                        }
                                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                                        {
                                                            betIbet = true;
                                                            betSbo = false;
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Bet
                                                    string phieuchung = "";
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, IBET ({6}): {4}, SBO ({7}): {5} (Betting)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                    if (betIbet)
                                                    {
                                                        BetManager.getBet(keyIbet).playBetNonLive(oIbet, money, "0", "", str_betgroup);
                                                        if (betSbo == false)
                                                        {
                                                            richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, IBET ({6}): {4} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                            updateMessage(keyIbet, "Done");
                                                            updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                                            betsuccess = true;
                                                            Thread.Sleep(ran.Next(4, 10) * 1000);
                                                        }
                                                        if (betSbo == false)
                                                        {
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            phieuchung = BetManager.getBet(keyIbet).getPhieuchung();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        phieuchung = mylib.generateID("T");
                                                    }

                                                    if (betSbo)
                                                    {
                                                        if (phieuchung != "")
                                                        {
                                                            string realmoney = money;
                                                            if (betIbet)
                                                                realmoney = BetManager.getBet(keyIbet).getRealMoney();

                                                            Thread.Sleep(ran.Next(4, 10) * 1000);
                                                            BetManager.getBet(keySbo).playBetNonLive(oSbo, realmoney, "0", phieuchung, str_betgroup);

                                                            if (betIbet == true)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, IBET ({6}): {4}, SBO ({7}): {5} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                                updateMessage(keyIbet, "Done");
                                                                updateMessage(keySbo, "Done");
                                                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit());
                                                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                                                betsuccess = true;
                                                                Thread.Sleep(ran.Next(4, 10) * 1000);
                                                            }
                                                            else if (betIbet == false)
                                                            {
                                                                richLog.Text = richLog.Text.Insert(0, string.Format("{0} {1}-vs-{2}, {3}, SBO ({7}): {5} (Bet Successful)\n", DateTime.Now.ToLongTimeString(), oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                                updateMessage(keySbo, "Done");
                                                                updateCredit(keySbo, BetManager.getBet(keySbo).getCredit());
                                                                betsuccess = true;
                                                                Thread.Sleep(ran.Next(4, 10) * 1000);
                                                            }
                                                            break;
                                                        }
                                                    }

                                                    if (betIbet == true && phieuchung == "")
                                                    {
                                                        richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, {2}\n", DateTime.Now.ToLongTimeString(), chienthuat, BetManager.getBet(keyIbet).getMessage()));
                                                        updateMessage(keyIbet, "");
                                                        updateMessage(keySbo, "");
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} Error - Strategy [{1}] is skipped, no common money\n", DateTime.Now.ToLongTimeString(), chienthuat));
                                    updateMessage(keyIbet, "");
                                    updateMessage(keySbo, "");
                                }
                            }
                        }

                        if (betsuccess)
                            break;
                        Thread.Sleep(1000);
                    }
                    btnBet.Text = "BET NON LIVE";
                    picIBET.Enabled = false;
                    picSBO.Enabled = false;
                });
                tBetNonLive.Start();
            }
            else
            {
                btnBet.Text = "BET NON LIVE";
                picIBET.Enabled = false;
                picSBO.Enabled = false;
                tBetNonLive.Abort();
            }
        }
    }
}
