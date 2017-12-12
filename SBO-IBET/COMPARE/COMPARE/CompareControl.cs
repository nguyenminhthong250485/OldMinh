using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using OpenQA.Selenium;
using System.Threading;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace COMPARE
{
    public partial class CompareControl : UserControl
    {
        Random ran = new Random();
        public string linkBong88 = "";
        Hashtable hsLeagueLive = new Hashtable();
        Hashtable hsMatchLive = new Hashtable();
        Hashtable hsOddLive = new Hashtable();
        Hashtable hsLeagueNonLive = new Hashtable();
        Hashtable hsMatchNonLive = new Hashtable();
        Hashtable hsOddNonLive = new Hashtable();
        Hashtable hsIbetMinus = new Hashtable();
        public int IdChangeLive = 0, IdChangeNonLive = 0;
        public string str_CompareLive = "";
        public string str_CompareUnder = "";
        string Title = "--------------------------------------------------------------------";
        public string str_CompareNonLive = "";
        public string str_UserNameSbo = "mqaiq13016,mqtbcat789";        
        public string str_UserNameIbet = "BYCE0922A,BYCE0939A,BYCE09469,BYCE0959B,BYCE09112,byce09222,byce09333,byce09444,byce09555,byce09666,byce09ab1,byce09ab2,byce09ab3,byce09ab4,byce09ab5,byce09ac1,byce09ac2,byce09ac3,byce09ac4,byce09ac5,byce09ad0,byce09ad1,byce09ad2,byce09ad3,byce09ad4,byce09ad5,byce09ad6,byce09ad7,byce09ad8,byce09ad9";        
        public string str_Profit = "2,3,4";
        //public string str_File = "";
        public string str_LeaugeSbo = "", str_LeaugeIbet = "", str_TeamSbo = "", str_TeamIbet = "";
        public DateTime TimeLimit = DateTime.Now.AddHours(3);
        Database db;
        double profitmin = 0;

        public void LoadData()
        {            
            udProfit.Text = "";
            foreach (string Profit in str_Profit.Split(','))
            {
                udProfit.Items.Add(Profit);
            }
            udProfit.SelectedIndex += 3;
            txtTime.Text = TimeLimit.ToShortTimeString();
        }
       
        public CompareControl()
        {
            InitializeComponent();
            db = new Database();
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
            CheckBox.CheckForIllegalCrossThreadCalls = false;
            RadioButton.CheckForIllegalCrossThreadCalls = false;
            DomainUpDown.CheckForIllegalCrossThreadCalls = false;            
        }
        
        private void bt_CompareName_Click(object sender, EventArgs e)
        {
                /////////////////////////////Compare Leauge//////////////////////////////////////////////////////////
                str_LeaugeSbo = str_LeaugeSbo.Substring(0, str_LeaugeSbo.Length - 1);
                str_LeaugeIbet = str_LeaugeIbet.Substring(0, str_LeaugeIbet.Length - 1);
                string str_LeaugeSboCompare = "", str_LeaugeIbetCompare = "";
                foreach (string LeaugeNameSbo in str_LeaugeSbo.Split(','))
                {
                    if (str_LeaugeIbet.IndexOf(LeaugeNameSbo) == -1)
                    {
                        str_LeaugeSboCompare += LeaugeNameSbo + ",";
                    }
                }
                str_LeaugeSboCompare = str_LeaugeSboCompare.Substring(0, str_LeaugeSboCompare.Length - 1);
                foreach (string LeaugeNameIbet in str_LeaugeIbet.Split(','))
                {
                    if (str_LeaugeSbo.IndexOf(LeaugeNameIbet) == -1)
                    {
                        str_LeaugeIbetCompare += LeaugeNameIbet + ",";
                    }
                }
                str_LeaugeIbetCompare = str_LeaugeIbetCompare.Substring(0, str_LeaugeIbetCompare.Length - 1);
                string str_LeaugeCompare = "Sbo Leauge: " + str_LeaugeSbo + "\r\n\r\n" + "Ibet Leauge: " + str_LeaugeIbet + "\r\n\r\n";
                str_LeaugeCompare = "Sbo Leauge Compare: " + str_LeaugeSboCompare + "\r\n\r\n" + "Ibet Leauge Compare: " + str_LeaugeIbetCompare + "\r\n\r\n";
                /////////////////////////////Compare Leauge////////////////////////////////////////////////////////////////

                /////////////////////////////Compare Team////////////////////////////////////////////////////////////////
                str_TeamSbo = str_TeamSbo.Substring(0, str_TeamSbo.Length - 1);
                str_TeamIbet = str_TeamIbet.Substring(0, str_TeamIbet.Length - 1);
                string str_TeamSboCompare = "", str_TeamIbetCompare = "";
                foreach (string TeamNameSbo in str_TeamSbo.Split(','))
                {
                    if (str_TeamIbet.IndexOf(TeamNameSbo) == -1)
                    {
                        str_TeamSboCompare += TeamNameSbo + ",";
                    }
                }
                str_TeamSboCompare = str_TeamSboCompare.Substring(0, str_TeamSboCompare.Length - 1);
                foreach (string TeamNameIbet in str_TeamIbet.Split(','))
                {
                    if (str_TeamSbo.IndexOf(TeamNameIbet) == -1)
                    {
                        str_TeamIbetCompare += TeamNameIbet + ",";
                    }
                }
                str_TeamIbetCompare = str_TeamIbetCompare.Substring(0, str_TeamIbetCompare.Length - 1);
                string str_TeamCompare = "Sbo Team: " + str_TeamSbo + "\r\n\r\n" + "Ibet Team: " + str_TeamIbet + "\r\n\r\n";
                str_TeamCompare = "Sbo Team Compare: " + str_TeamSboCompare + "\r\n\r\n" + "Ibet Team Compare: " + str_TeamIbetCompare + "\r\n\r\n";
                /////////////////////////////Compare Team////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////Compare Total////////////////////////////////////////////////////////////////////////////////
                int nLeaugeSbo = str_LeaugeSbo.Split(',').Length;
                int nLeaugeIbet = str_LeaugeIbet.Split(',').Length;
                int nLeaugeCompare = nLeaugeSbo - str_LeaugeSboCompare.Split(',').Length;
                int nTeamSbo = str_TeamSbo.Split(',').Length;
                int nTeamIbet = str_TeamIbet.Split(',').Length;
                int nTeamCompare = nTeamSbo - str_TeamSboCompare.Split(',').Length;
                string str_TotalCompare = "Total Leauge Sbo: " + nLeaugeSbo + "\r\n\r\n" + "Total Leauge Ibet: " + nLeaugeIbet + "\r\n\r\n";
                str_TotalCompare += "Total Leauge Compare: " + nLeaugeCompare + "\r\n\r\n";
                str_TotalCompare += "Total Team Sbo: " + nTeamSbo + "\r\n\r\n" + "Total Team Ibet: " + nTeamIbet + "\r\n\r\n";
                str_TotalCompare += "Total Team Compare: " + nTeamCompare + "\r\n\r\n";
                File.WriteAllText(@"C:\Users\THONG\Desktop\NameCom.txt", str_LeaugeCompare + str_TeamCompare + str_TotalCompare);
                /////////////////////////////Compare Total////////////////////////////////////////////////////////////////////////////////
        }        
        private void bt_Login_Click(object sender, EventArgs e)
        {
            str_UserNameSbo = txtSBO.Text;
            str_UserNameIbet = txtIBET.Text;

            bt_Login.Enabled = false;
            btnCompareName.Enabled = false;
            btnCompareOddOldVersion.Enabled = false;
            radLive.Enabled = false;
            radNonLive.Enabled = false;
            int startSbo, startIbet, endSbo, endIbet;            
            if(radNonLive.Checked)
            {
                startSbo = 0;
                endSbo = 1;
                startIbet = 0;
                endIbet = 10;
            }
            else
            {
                startSbo = 1;
                endSbo = 2;
                startIbet = 10;
                endIbet = 30;
            }
            Thread Login = new Thread(delegate ()
            {                
                for (int i = startSbo; i < endSbo; i++)
                {
                    BetManager.createBet("sbo", "sbo" + i, "", str_UserNameSbo.Split(',')[i]);
                    BetManager.getBet("sbo" + i).login();
                    rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", BetManager.getBet("sbo" + i).getMessage()));
                    Thread.Sleep(100);
                }

                for (int i = startIbet; i < endIbet; i++)
                {
                    BetManager.createBet("ibet", "ibet" + i, "", str_UserNameIbet.Split(',')[i]);
                    BetManager.getBet("ibet" + i).login();
                    rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + i + ": " + BetManager.getBet("ibet" + i).getMessage()));
                    if(BetManager.getBet("ibet" + i).getMessage()== "Under Maintenance")
                    {         
                        return;
                    }
                    Thread.Sleep(100);
                }
                btnCompareName.Enabled = true;
                btnCompareOddOldVersion.Enabled = true;             
            });
            Login.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText("acc.data", str_UserNameSbo + "\n" + str_UserNameIbet);
        }

        private void udProfit_SelectedItemChanged(object sender, EventArgs e)
        {
            try
            {
                profitmin = double.Parse(udProfit.Text) / (-100);
            }
            catch
            { }
        }

        private void radLive_CheckedChanged(object sender, EventArgs e)
        {
            if (radLive.Checked)
                udProfit.Text = "2";
            else
                udProfit.Text = "4";
        }

        private void CompareControl_Load(object sender, EventArgs e)
        {
            try
            {
                var lines = File.ReadAllLines("acc.data");
                txtSBO.Text = lines[0];
                txtIBET.Text = lines[1];                
            }
            catch (Exception)
            {
                txtSBO.Text = str_UserNameSbo;
                txtIBET.Text = str_UserNameIbet;
            }
            LoadData();

        }
        private void btnCompareOddOldVersion_Click(object sender, EventArgs e)
        {            
            btnCompareOddOldVersion.Enabled = false;           
            if (radNonLive.Checked)
            {
                DataTable dtMinus = db.getMinus();
                foreach(DataRow dr in dtMinus.Rows)
                {
                    objCompare o = UtilSoccer.convertStringToCompare(dr["data1"].ToString());                    
                    string str_BetIbet = o.home + "," + o.away + "," + o.bettype + "," + o.hdp + "," + o.odd.Split('/')[1] + "," + o.keo.Split('/')[1] + "," + o.betid.Split('/')[1] + "," + o.score;
                    ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);                                                        
                    if (!hsIbetMinus.Contains(dr["keo"].ToString()))
                    {
                        hsIbetMinus.Add(dr["keo"].ToString(), oIbet);
                    }                                      
                }

                Thread tNonLive = new Thread(delegate ()
                {
                    string strCompareNonLiveTemp = "X";
                    int index = 0;
                    int maxuser = 10;
                    int minuser = 0;
                    int count = 0;
                    List<objMatch> lstIbet = new List<objMatch>();
                    List<objMatch> lstSbo = new List<objMatch>();
                    int indexlogout = -1;
                    int countLoop = 0;
                    #region GetOdd
                    while (true)
                    {
                        countLoop++;
                        lblCountNonLive.Text = countLoop.ToString();
                        //Get Odd Ibet
                        try
                        {
                            lstIbet = BetManager.getBet("ibet" + index).getMatchOddNonLive(TimeLimit);
                        }
                        catch
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "ibet" + index + ": IBET NONLIVE NOT FOUND \n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            continue;
                        }
                        if (lstIbet.Count == 0)
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "ibet" + index + ": IBET NONLIVE NOT FOUND \n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            continue;
                        }
                        else if (lstIbet[0].Score == "Error")
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " Error" + "\n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            count = 0;
                            continue;
                        }
                        else if (lstIbet[0].Score == "Logout")
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " Logout" + "\n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            indexlogout = index;
                            index++;
                            if (index == maxuser) index = minuser;
                            count = 0;
                            continue;
                        }
                        rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " ScanOdd" + "\n");

                        //Get Odd Sbo
                        lstSbo = BetManager.getBet("sbo0").getMatchOddNonLive(TimeLimit);
                        str_CompareNonLive = UtilSoccer.CompareByHash(lstIbet, lstSbo, profitmin);
                        List<objCompare> lstCompare = UtilSoccer.convertCompareToList(str_CompareNonLive);
                        #region Minus
                        //if (lstIbet.Count > 0)
                        //{
                        //    if (lstCompare.Count > 0)
                        //    {
                        //        foreach (objCompare oCompare in lstCompare)
                        //        {
                        //            try
                        //            {
                        //                string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                        //                string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;

                        //                ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                        //                ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);

                        //                if (oIbet.odd < 0)
                        //                {
                        //                    string key = string.Format("{0},{1},{2},{3}", oCompare.home, oCompare.away, oCompare.hdp, oCompare.bettype);
                        //                    if (!hsIbetMinus.Contains(key))
                        //                    {
                        //                        hsIbetMinus.Add(key, oIbet);
                        //                        db.doInsertMinus(key, oCompare.ToString());
                        //                    }
                        //                }
                        //            }
                        //            catch
                        //            {

                        //            }
                        //        }
                        //    }

                        //    foreach (objMatch match in lstIbet)
                        //    {
                        //        try
                        //        {
                        //            string key = string.Format("{0},{1},{2},{3}", match.HomeName, match.AwayName, match.hdp, match.BetType);
                        //            if (hsIbetMinus.Contains(key))
                        //            {
                        //                if ((((ticket)hsIbetMinus[key]).choose == "h" && UtilSoccer.profit_odd(((ticket)hsIbetMinus[key]).odd.ToString(), match.Odd2) >= -0.03) || (((ticket)hsIbetMinus[key]).choose == "a" && UtilSoccer.profit_odd(((ticket)hsIbetMinus[key]).odd.ToString(), match.Odd1) >= -0.03))
                        //                {
                        //                    string Odd1 = match.Odd1;
                        //                    string Odd2 = match.Odd2;
                        //                    double LeagueOdd = UtilSoccer.profit_odd(Odd1, Odd2);
                        //                    string IdKeoIbet = match.IdKeo;
                        //                    string HomeName = match.HomeName;
                        //                    string AwayName = match.AwayName;
                        //                    string Keo = match.hdp;
                        //                    string BetType = match.BetType;
                        //                    string Score = match.Score;
                        //                    string Time = match.TimeLive;
                        //                    string home_away = "h/a";
                        //                    string result = HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + Odd1 + "/" + Odd2 + "," + home_away + "," + IdKeoIbet + "/" + IdKeoIbet + "," + UtilSoccer.profit_odd(Odd1, Odd2) + "," + LeagueOdd + "," + Score + "," + Time + ",i\n";
                        //                    db.doUpdateMinus(key, result);
                        //                }
                        //                else
                        //                {
                        //                    db.doUpdateMinus(key, "");
                        //                }
                        //            }
                        //        }
                        //        catch
                        //        {

                        //        }
                        //    }

                        //    foreach (objMatch match in lstSbo)
                        //    {
                        //        try
                        //        {
                        //            string key = string.Format("{0},{1},{2},{3}", match.HomeName, match.AwayName, match.hdp, match.BetType);
                        //            if (hsIbetMinus.Contains(key))
                        //            {
                        //                if ((((ticket)hsIbetMinus[key]).choose == "h" && UtilSoccer.profit_odd(((ticket)hsIbetMinus[key]).odd.ToString(), match.Odd2) >= -0.03) || (((ticket)hsIbetMinus[key]).choose == "a" && UtilSoccer.profit_odd(((ticket)hsIbetMinus[key]).odd.ToString(), match.Odd1) >= -0.03))
                        //                {
                        //                    string Odd1 = match.Odd1;
                        //                    string Odd2 = match.Odd2;
                        //                    double LeagueOdd = UtilSoccer.profit_odd(Odd1, Odd2);
                        //                    string IdKeoSbo = match.IdKeo;
                        //                    string HomeName = match.HomeName;
                        //                    string AwayName = match.AwayName;
                        //                    string Keo = match.hdp;
                        //                    string BetType = match.BetType;
                        //                    string Score = match.Score;
                        //                    string Time = match.TimeLive;
                        //                    string home_away = "h/a";
                        //                    string result = HomeName + "," + AwayName + "," + BetType + "," + Keo + "," + Odd1 + "/" + Odd2 + "," + home_away + "," + IdKeoSbo + "/" + IdKeoSbo + "," + UtilSoccer.profit_odd(Odd1, Odd2) + "," + LeagueOdd + "," + Score + "," + Time + ",s\n";
                        //                    db.doUpdateMinus(key, result);
                        //                }                                       
                        //            }
                        //        }
                        //        catch
                        //        {

                        //        }
                        //    }
                        //}
                        #endregion
                        if (str_CompareNonLive != strCompareNonLiveTemp)
                        {
                            db.UpdateInfoCompare("a,a", str_CompareNonLive, false);
                            string timecomparenonlive = Title + DateTime.Now.ToLongTimeString() + Title;
                            rtb_CompareNonLive.Text = Title + "NONLIVE" + Title + "\n" + str_CompareNonLive + "\n" + timecomparenonlive;
                            strCompareNonLiveTemp = str_CompareNonLive;
                        }
                        //Get Match Compare
                        rtb_CompareMatch.Clear();
                        rtb_CompareMatch.Text += BetManager.getBet("ibet" + index).getTotalMatch() + "\n";
                        rtb_CompareMatch.Text += BetManager.getBet("sbo0").getTotalMatch() + "\n";

                        string strSboCompareNonLive = BetManager.getBet("sbo0").getMatchs();
                        string strIbetCompareNonLive = BetManager.getBet("ibet" + index).getMatchs();
                        string SboIbetCompareNonLive = Util.GetSplitStringIntoString(strSboCompareNonLive, strIbetCompareNonLive);
                        int MatchCompareNonIve = SboIbetCompareNonLive.Split(',').Length - 1;
                        rtb_CompareMatch.Text += "COMPARE MATCH: " + MatchCompareNonIve.ToString() + "\n";

                        int KeoCompareNonLive = str_CompareNonLive.Split('\n').Length - 1;
                        rtb_CompareMatch.Text += "COMPARE NONLIVE: " + KeoCompareNonLive.ToString() + "\n";
                        //Change
                        count++;
                        if (count == 5)
                        {
                            index++;
                            count = 0;
                        }
                        if (index == maxuser)
                        {
                            index = minuser;
                            count = 0;
                        }
                        Thread.Sleep(100);
                    }
                    #endregion
                });
                tNonLive.Start();
            }
            else if (radLive.Checked)
            {

                Thread tLive = new Thread(delegate ()
                {
                    string strCompareLiveTemp = "X";
                    string strCompareUnderTemp = "X";
                    int index = 10;
                    int maxuser = 30;
                    int minuser = 10;
                    int count = 0;
                    List<objMatch> listIbetLive = new List<objMatch>();
                    List<objMatch> listSboLive = new List<objMatch>();
                    int indexlogout = -1;
                    int countLoop = 0;
                    #region GetOdd
                    while (true)
                    {
                        countLoop++;
                        lblCountLive.Text = countLoop.ToString();

                        //Get Odd Ibet
                        try
                        {
                            listIbetLive = BetManager.getBet("ibet" + index).getMatchOddLive();
                        }
                        catch
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "ibet" + index + ": IBET LIVE NOT FOUND \n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            continue;
                        }
                        if (listIbetLive.Count == 0)
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "ibet" + index + ": IBET LIVE NOT FOUND \n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            continue;
                        }
                        else if (listIbetLive[0].Score == "Error")
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " Error" + "\n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            index++;
                            if (index == maxuser) index = minuser;
                            count = 0;
                            continue;
                        }//Error

                        else if (listIbetLive[0].Score == "Logout")
                        {
                            rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " Logout" + "\n");
                            rtb_Login.Text = rtb_Login.Text.Insert(0, string.Format("{0} \n", "IBET" + index + ": " + BetManager.getBet("ibet" + index).getMessage()));
                            indexlogout = index;
                            index++;
                            if (index == maxuser) index = minuser;
                            count = 0;
                            continue;
                        }

                        rtb_Login.Text = rtb_Login.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + "IBET" + index + " ScanOdd" + "\n");

                        //Get Odd Sbo
                        listSboLive = BetManager.getBet("sbo1").getMatchOddLive();
                        str_CompareLive = UtilSoccer.CompareByHashLive(listIbetLive, listSboLive, profitmin);
                        str_CompareUnder = UtilSoccer.under_compare;
                        if (str_CompareLive != strCompareLiveTemp)
                        {
                            db.UpdateInfoCompare("a,a", str_CompareLive, true);
                            string timecomparelive = Title + DateTime.Now.ToLongTimeString() + Title;
                            rtb_CompareLive.Text = Title + "LIVE" + Title + "\n" + str_CompareLive + "\n" + timecomparelive;
                            strCompareLiveTemp = str_CompareLive;
                        }
                        if (str_CompareUnder != strCompareUnderTemp)
                        {
                            db.UpdateInfoCompare("under", str_CompareUnder, true);
                            string timecomparelive = Title + DateTime.Now.ToLongTimeString() + Title;
                            rtb_CompareNonLive.Text = Title + "UNDER" + Title + "\n" + str_CompareUnder + "\n" + timecomparelive;
                            strCompareUnderTemp = str_CompareUnder;
                        }


                        count++;
                        if (count == 5)
                        {
                            index++;
                            count = 0;
                        }
                        if (index == maxuser)
                        {
                            index = minuser;
                            count = 0;
                        }
                        Thread.Sleep(100);
                    }
                    #endregion
                });
                tLive.Start();
            }
        }
        private void bt_Pre_Click(object sender, EventArgs e)
        {
            TimeLimit=TimeLimit.AddMinutes(-30);
            txtTime.Text = TimeLimit.ToShortTimeString();
        }

        private void bt_Next_Click(object sender, EventArgs e)
        {
            TimeLimit=TimeLimit.AddMinutes(30);
            txtTime.Text = TimeLimit.ToShortTimeString();
        }
    }
}
