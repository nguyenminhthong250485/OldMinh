using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using HtmlAgilityPack;
using System.IO;
using System.Diagnostics;

namespace BET
{
    public partial class BetControl : UserControl
    {
        string phieuchung = "";
        Thread ThreadBet;
        Thread ThreadSbo;
        Thread ThreadIbet;
        Database db;
        private int numberControl;
        [Category("NumberControl")]
        [DefaultValue(0)]
        public int NumberControl
        {
            get { return numberControl; }
            set { numberControl = value; BetControlNumber.Text = value.ToString(); }
        }

        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data";
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public string str_BetListSbo = "";
        public string str_BetListIbet = "";

        HttpHelper httpSbo = new HttpHelper();
        HttpHelper httpIbet = new HttpHelper();
        Hashtable hsLeague = new Hashtable();
        Hashtable hsMatch = new Hashtable();
        Hashtable hsOdd = new Hashtable();

        public string str_UserNameSbo = "";
        public string str_UserNameIbet = "";
        
        public string str_IpSbo = "";
        public string str_IpIbet = "";
        public string GiaDoSbo = "";
        public string GiaDoIbet = "";
        public string str_GiaDoSbo = "";
        public string str_GiaDoIbet = "";
        public string GroupSbo = "";
        public string GroupIbet = "";
        public string str_Money = "";
        string money = "";//string moneylimit = "";
        public string str_Style = "W,ALL,SBO W,IBET W,L";
        public int RandomTimeMax = 13000;
        public int RandomTimeMin = 5000;

        public bool CheckLoginSbo = false, CheckLoginIbet = false;
        public bool StopAll = false;
        private bool CheckBetSbo = false, CheckBetIbet = false;
        public bool Complete = false;
        public bool PressBetOne = false, PressBetAuto = false;

        public string str_CompareLive = "";
        public string str_CompareNonLive = "";
        public string str_BetSbo = "";
        public string str_BetIbet = "";
        private int BetCountSbo = 0, BetCountIbet = 0;

        private string common_password = "Vvvv6868@";
        public BetControl()
        {
            InitializeComponent();
            db = new Database();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        private string RandomMoney(string str_money)
        {
            string result = "";
            Random r = new Random();
            int len = str_money.Split(',').Length;
            try
            {
                result = str_money.Split(',')[r.Next(0, len)];
            }
            catch (Exception)
            {
                result = str_money.Split(',')[0];
            }
            return result;
        }
        private string BetSbo(string str_Bet,string money,string DoSbo,string IsLive,string UrlLoginSbo1,string IpSboLogin)
        {
            string result = "Bet Success";
            int DoBet = int.Parse(money) / int.Parse(DoSbo);
            string Keo = str_Bet.Split(',')[3];
            string IdKeo = str_Bet.Split(',')[6];
            string Odd = str_Bet.Split(',')[4]; 
            string BetType = str_Bet.Split(',')[2];
            string op= str_Bet.Split(',')[5];
            string hdpType = "";
            if (BetType=="7"||BetType=="9")
            {
                hdpType = "2";
            }

            string UrlTicketSbo = "http://" + Util.GetSubstringByString(UrlLoginSbo1, "http://", "&").Replace("default", "ticket/ticket") + "&id=" + IdKeo + "&op=" + op + "&odds=" + Odd + "&hdpType="+hdpType+"&isor=0&isLive=" + IsLive + "&betpage=18&style=1";
            string UrlBetSbo = "http://" + Util.GetSubstringByString(UrlLoginSbo1, "http://", "&").Replace("default", "ticket/confirm") + "&sameticket=0&betcount=" + BetCountSbo + "&stake=" + DoBet.ToString() + "&ostyle=1&stakeInAuto=10&betpage=18&acceptIfAny=0&autoProcess=0&autoRefresh=1&oid=" + IdKeo + "&timeDiff=2100";

            string data1 = httpSbo.Fetch(UrlTicketSbo, HttpHelper.HttpMethod.Get, UrlLoginSbo1, null, IpSboLogin);

            string oddbet = Util.GetSubstringByString(data1.Replace("'", "").Replace("Over", "h").Replace("Under", "a").Replace("Tài", "h").Replace("Xỉu", "a"), "," + op + ",", ",");

            if (double.Parse(oddbet) != double.Parse(Odd))
            {
                result = "Odd Change " + Odd + "/" + oddbet;
            }
            //////////////////////////////////////////////////////////////////////////////////////
            string ProcessBet = httpSbo.Fetch(UrlBetSbo, HttpHelper.HttpMethod.Get, UrlLoginSbo1, null, IpSboLogin);

            if (ProcessBet.IndexOf("onOrderSubmitted") == -1)
            {
                player.SoundLocation = "bet_fall.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                mylib.AppendText(richOutput, ProcessBet, Color.Red, true);
                db.doInsertTicket("SBOBET", dud_SboName.Text, str_Bet.Split(',')[2], str_Bet.Split(',')[3], "", "", op, oddbet, money, DoSbo, GroupSbo + "," + GroupIbet, phieuchung);
                result = "BET FALL SBO";
                return result;
            }
            else
            {
                BetCountSbo += 1;
            }
            string BetSameAccSbo = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + dud_SboName.Text;
            string BetSameOddSbo = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + str_Bet.Split(',')[3] + "," + str_Bet.Split(',')[4];
            mylib.AppendText(richOutput,"SBO: "+ str_BetSbo.Split(',')[0] + " vs " + str_BetSbo.Split(',')[1] + "," + str_BetSbo.Split(',')[4] + "(" + str_BetSbo.Split(',')[5] + ")", Color.Blue, true);
            
            File.AppendAllText(desktop_path + "\\BetListSbo.txt", BetSameAccSbo + "@" + BetSameOddSbo + "\r\n");
            try
            {
                //mylib.AppendText(richOutput, "SBO: Push ticket to database", Color.Green, true);
                db.doInsertTicket("SBOBET", dud_SboName.Text, str_Bet.Split(',')[2], str_Bet.Split(',')[3], str_Bet.Split(',')[0], str_Bet.Split(',')[1], op, oddbet, money, DoSbo, GroupSbo + "," + GroupIbet, phieuchung);
            }
            catch (Exception)
            {
                mylib.AppendText(richOutput, "Error: Database error", Color.Green, true);
            }
            player.SoundLocation = "success_sbo.wav"; // Đường dẫn đến file cần chơi
            player.Play();
            return result;
        }
        private string BetIbet(string str_Bet,string moneybet,string DoIbet,string UrlSport, string IpIbetLogin)
        {
            string result = "Bet Success";
            int DoBet = int.Parse(moneybet) / int.Parse(DoIbet);
            string Keo = str_Bet.Split(',')[3];
            string IdKeo = str_Bet.Split(',')[6];
            string Odd = str_Bet.Split(',')[4];
            string BetType = str_Bet.Split(',')[2];
            string op = str_Bet.Split(',')[5];
            string HomeScore = "0";
            string AwayScore = "0";
            try
            {
                HomeScore = str_Bet.Split(',')[7].Split('-')[0];
                AwayScore = str_Bet.Split(',')[7].Split('-')[1].Replace("\r", "");
            }
            catch
            {}
            string UrlHostIbet = UrlSport.Replace("/sports", "");
            string PostData = "ItemList[0][sportname]=Soccer&ItemList[0][Hscore]=" + HomeScore + "&ItemList[0][Ascore]=" + AwayScore + "&ItemList[0][type]=OU&ItemList[0][bettype]=" + BetType + "&ItemList[0][oddsid]=" + IdKeo.Trim() + "&ItemList[0][betteam]=" + op + "&ItemList[0][stake]=" + DoBet;
            string GetTicket = httpIbet.Fetch(UrlHostIbet + "/Betting/GetTickets", HttpHelper.HttpMethod.Post, UrlSport, PostData, IpIbetLogin);
            if(GetTicket.Replace("\"","").IndexOf("ErrorCode:0")==-1)
            {
                mylib.AppendText(richOutput, GetTicket, Color.Red, true);
                result = "BET FALL IBET";
                return result;
            }

            string OddBet = Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ",");
            if (UtilSoccer.CheckOddChange(Odd, OddBet) == "Up")
            {
                result = "Odd Up " + Odd + "/" + OddBet;
                //return result;
            }
            if (UtilSoccer.CheckOddChange(Odd, OddBet) == "Down")
            {
                player.SoundLocation = "OddDown.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                result = "Odd Down " + Odd + "/" + OddBet;
                string BetSameOddIbetMax = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + str_Bet.Split(',')[3] + "," + str_Bet.Split(',')[4];
                File.AppendAllText(desktop_path + "\\BetListIbet.txt", BetSameOddIbetMax + "\r\n");
                return result;
            }
            string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
            string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
            PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
            if (double.Parse(MaxBet) < DoBet)
            {
                PostData = PostData.Replace("ItemList[0][stake]=" + DoBet, "ItemList[0][stake]=" + MaxBet);

                string BetSameAccIbetMax = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + dud_IbetName.Text;
                string BetSameOddIbetMax = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + str_Bet.Split(',')[3] + "," + str_Bet.Split(',')[4];
                File.AppendAllText(desktop_path + "\\BetListIbet.txt", BetSameAccIbetMax + "@" + BetSameOddIbetMax + "\r\n");
                player.SoundLocation = "MaxBet.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                money = (int.Parse(MaxBet.Replace(",","")) * int.Parse(DoIbet)).ToString();
            }
            /////////////////////////////////////////////////////////////////////////////////////
            string ProcessBet = httpIbet.Fetch(UrlHostIbet + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, UrlSport, PostData, IpIbetLogin);
            if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0)
            {
                player.SoundLocation = "bet_fall.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                if (ProcessBet.IndexOf("Chúng tôi đang cập nhật tỉ lệ cược") != -1)
                {
                    mylib.AppendText(richOutput, "Chúng tôi đang cập nhật tỉ lệ cược", Color.Red, true);
                    string BetOddIbetUpdate = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + str_Bet.Split(',')[3] + "," + str_Bet.Split(',')[4];
                    File.AppendAllText(desktop_path + "\\BetListIbet.txt", BetOddIbetUpdate + "\r\n");
                }

                mylib.AppendText(richOutput, ProcessBet, Color.Red, true);//Chúng tôi đang cập nhật tỉ lệ cược
                result = "BET FALL IBET";
                return result;
            }
            string BetSameAccIbet = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + dud_IbetName.Text;
            string BetSameOddIbet = str_Bet.Split(',')[0] + "," + str_Bet.Split(',')[1] + "," + str_Bet.Split(',')[2] + "," + str_Bet.Split(',')[3] + "," + str_Bet.Split(',')[4];
            mylib.AppendText(richOutput, "IBET: " + str_BetIbet.Split(',')[0] + " vs " + str_BetIbet.Split(',')[1] + "," + str_BetIbet.Split(',')[4] + "(" + str_BetIbet.Split(',')[5] + ")", Color.OrangeRed, true);

            phieuchung = mylib.generateID("T");
            try
            {
                //mylib.AppendText(richOutput, "IBET: Push ticket to database", Color.Green, true);
                db.doInsertTicket("IBET", dud_IbetName.Text, str_Bet.Split(',')[2], str_Bet.Split(',')[3], str_Bet.Split(',')[0], str_Bet.Split(',')[1], op, OddBet, money, DoIbet, GroupSbo + "," + GroupIbet, phieuchung);
            }
            catch (Exception)
            {
                mylib.AppendText(richOutput, "Error: Database error", Color.Red, true);
            }
            File.AppendAllText(desktop_path + "\\BetListIbet.txt", BetSameAccIbet + "@" + BetSameOddIbet + "\r\n");
            BetCountIbet += 1;
            return result;
        }
        private string GetKeoSbo(List<objMatch> Odd_SboList, string str_BetSbo)
        {
            string IdKeoOutput = "";
            string KeoOutput = "";
            string Keo = str_BetSbo.Split(',')[3];
            string IdKeo = str_BetSbo.Split(',')[6];
            string Odd = str_BetSbo.Split(',')[4];
            string BetType = str_BetSbo.Split(',')[2];
            string HomeName = str_BetSbo.Split(',')[0];
            string AwayName = str_BetSbo.Split(',')[1];
            foreach (objMatch Odd_Sbo in Odd_SboList)
            {
                if(Odd_Sbo.HomeName==HomeName&&Odd_Sbo.AwayName==AwayName&&Odd_Sbo.Keo==Keo&&Odd_Sbo.BetType==BetType)
                {
                    IdKeoOutput = Odd_Sbo.IdKeo;
                    KeoOutput = str_BetSbo.Replace(IdKeo, IdKeoOutput);
                    break;
                }
            }
            return KeoOutput;
        }
        private void BetControl_Load(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            lb_Money.Text = str_Money;
            string str_Leauge = "6,8,10,11,12,14,16";
            string str_Profit = "";
            str_Profit = "2,3,4,5,6,7,8,9";
            foreach (string Profit in str_Profit.Split(','))
            {
                this.dud_Profit.Items.Add(Profit);
            }
            try
            {
                dud_Profit.SelectedIndex += 2;
            }
            catch
            {

            }
            dud_SboName.Items.Clear();
            dud_IbetName.Items.Clear();
            dud_Style.Items.Clear();
            dud_SboName.Text = "";
            dud_IbetName.Text = "";
            dud_Style.Text = "";
            switch(GroupSbo)
            {
                case "a":
                    lb_GroupSbo.ForeColor = System.Drawing.Color.Blue;

                    str_Leauge = "6,8,10,11,12,14,16";
                    foreach (string Leauge in str_Leauge.Split(','))
                    {
                        this.dud_Leauge.Items.Add(Leauge);
                    }
                    try
                    {
                        dud_Leauge.SelectedIndex += 2;
                    }
                    catch
                    {
                        
                    }

                    try
                    {
                        dud_Profit.SelectedIndex += 1;
                    }
                    catch
                    {

                    }
                    break;
                case "c":
                    lb_GroupSbo.ForeColor = System.Drawing.Color.Orange;
                    str_Leauge = "8,10,12,13,14,16,18";
                    foreach (string Leauge in str_Leauge.Split(','))
                    {
                        this.dud_Leauge.Items.Add(Leauge);
                    }
                    try
                    {
                        dud_Leauge.SelectedIndex += 2;
                    }
                    catch
                    {
                        //dm_UserNameSbo.Text = "";
                    }

                    try
                    {
                        dud_Profit.SelectedIndex += 3;
                    }
                    catch
                    {

                    }
                    break;
            }
            switch (GroupIbet)
            {
                case "a":
                    lb_GroupIbet.ForeColor = System.Drawing.Color.Blue;
                    break;
                case "b":
                    lb_GroupIbet.ForeColor = System.Drawing.Color.Yellow;

                    try
                    {
                        dud_Profit.SelectedIndex += 1;
                    }
                    catch
                    {

                    }
                    break;
                case "c":
                    lb_GroupIbet.ForeColor = System.Drawing.Color.Orange;
                    try
                    {
                        dud_Profit.SelectedIndex += 2;
                    }
                    catch
                    {

                    }
                    break;
                case "d":
                    lb_GroupIbet.ForeColor = System.Drawing.Color.Red;
                    try
                    {
                        dud_Profit.SelectedIndex += 3;
                    }
                    catch
                    {

                    }
                    break;
            }
            lb_GroupSbo.Text = GroupSbo;
            lb_GroupIbet.Text = GroupIbet;

            foreach (string UserNameSbo in str_UserNameSbo.Split(','))
            {
                this.dud_SboName.Items.Add(UserNameSbo);
            }
            try
            {
                dud_SboName.SelectedIndex +=1;
            }
            catch
            {
                //dm_UserNameSbo.Text = "";
            }
            foreach (string UserNameIbet in str_UserNameIbet.Split(','))
            {
                dud_IbetName.Items.Add(UserNameIbet);
            }
            try
            {
                dud_IbetName.SelectedIndex += 1;
            }
            catch
            {
                //dm_UserNameIbet.Text = "";
            }
            foreach (string style in str_Style.Split(','))
            {
                dud_Style.Items.Add(style);
            }
            try
            {
                dud_Style.SelectedIndex += 1;
            }
            catch
            {
                //dm_Style.Text = "";
            }
        }
        private void dud_SboName_SelectedItemChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < str_UserNameSbo.Split(',').Length; i++)
            {
                if (dud_SboName.Text == str_UserNameSbo.Split(',')[i])
                {
                    tb_IpSbo.Text = str_IpSbo.Split(',')[i];
                    txtGiaDoSbo.Text = str_GiaDoSbo.Split(',')[i];
                    break;
                }
            }
        }
        private void dud_IbetName_SelectedItemChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < str_UserNameIbet.Split(',').Length; i++)
            {
                if (dud_IbetName.Text == str_UserNameIbet.Split(',')[i])
                {
                    txtIPIbet.Text = str_IpIbet.Split(',')[i];
                    txtGiaDoIbet.Text = str_GiaDoIbet.Split(',')[i];
                    break;
                }
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int j = 0;
            ThreadSbo = new Thread(delegate ()
            {
            /////////////////////////////////////////////////////////////Login/////////////////////////////////////////////
                string mainLink = "", welcomeLink = "";
                string Credit = "", IpSboLogin = "";
                double maxbet = 0;
                int nlink = 0;
                string str_UrlSbo = "img.eaxybox.com,www.warungharta.com,www.beer777.com,www.currybread.com,www.pic5678.com,www.pasangsini.com,www.kampungemas.com";
                while (true)
                {
                    #region LOGIN SBO
                    if (CheckLoginSbo == false)
                    {
                        httpSbo.ClearCookies();
                        BetCountSbo = 0;
                        j = 0;
                        IpSboLogin = tb_IpSbo.Text;
                        maxbet = double.Parse(str_Money.Split(',')[str_Money.Split(',').Length - 1]) / double.Parse(txtGiaDoSbo.Text);
                        string link = "";
                        string data = "";

                        //Link
                        link = "http://" + str_UrlSbo.Split(',')[nlink];
                        lblSboStatus.Text = link.Replace("http://", "").Replace(".com", "");
                        data = httpSbo.Fetch(link + "/betting.aspx", HttpHelper.HttpMethod.Get, null, null, IpSboLogin);
                        //Thread.Sleep(2000);
                        if (data.Contains("SBOBET") == false)
                        {
                            nlink += 1;
                            lblSboStatus.Text = "Error Link";
                            Console.Beep(2000,3000);
                            Thread.Sleep(2000);
                            CheckLoginSbo = false;
                            continue;
                        }
                        //Link
                        string HidCK = Util.HtmlGetAttributeValue(data, "value", "//input[@id='HidCK']");
                        string tag = Util.EscapeDataString(Util.HtmlGetAttributeValue(data, "value", "//input[@id='tag']"));
                        string tk = Util.EscapeDataString(Util.GetSubstringByString(data, "'ms','ps'],[", "]));"));
                        string fingerprint = "2cf5547e23492b793471c401989931ac";
                        string post = "id=" + dud_SboName.Text + "&password=" + common_password + "&lang=en&tk=" + tk + "&type=form&tzDiff=1&HidCK=" + HidCK + "&tag=" + tag + "&fingerprint=" + fingerprint;
                        welcomeLink = httpSbo.FetchResponseUri(link + "/web/public/process-sign-in.aspx", HttpHelper.HttpMethod.Post, link + "/betting.aspx", post, IpSboLogin);
                        if (welcomeLink.IndexOf("web-root") == -1)
                        {
                            mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] \n Wrong User And Pass", Color.Red, true);
                            lblSboStatus.Text = "Error Info";
                            Console.Beep(2000, 500);
                            Thread.Sleep(2000);
                            CheckLoginSbo = false;
                            break;
                        }
                        string defaultLink = httpSbo.FetchResponseUri(welcomeLink, HttpHelper.HttpMethod.Get, link + "/betting.aspx", null, IpSboLogin);
                        if (defaultLink.IndexOf("termandconditions") != -1)
                        {
                            string UrlTermAndCondition = defaultLink;
                            string data_UrlTermAndCondition = httpSbo.Fetch(UrlTermAndCondition, HttpHelper.HttpMethod.Get, null, null);
                            string UrlLoginSbo = httpSbo.FetchResponseUri(UrlTermAndCondition, HttpHelper.HttpMethod.Post, UrlTermAndCondition, "action=I Agree");
                            string data_Login = httpSbo.Fetch(UrlLoginSbo, HttpHelper.HttpMethod.Get, UrlTermAndCondition, null);
                        }

                        string mainName = Util.GetSubstringByString(welcomeLink, "http://", "/web-root");
                        mainLink = "http://" + mainName;
                        ttBet.SetToolTip(btnLogin, "Success");
                        if (mainName != "")
                            mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Login successfully", Color.DarkBlue, true);
                        
                        else
                        {
                            mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Login failed", Color.Red, true);
                            Console.Beep(2000, 500);
                            Thread.Sleep(2000);
                            CheckLoginSbo = false;
                            continue;
                        }
                        #endregion

                        #region CHECK SBO CREDIT
                        Credit = httpSbo.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, IpSboLogin);
                        Credit = Credit.Split('\'')[3];
                        lblSboStatus.Text = "Credit:" + Credit;

                        try
                        {
                            if (double.Parse(Credit) < maxbet)
                            {
                                mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Credit(" + Credit + ") is low, need to switch to other member", Color.Red, true);
                                dud_SboName.ForeColor = System.Drawing.Color.Red;
                                try
                                {
                                    mylib.AppendText(richOutput, "SBO: CHANGE ACC [" + dud_SboName.Text + "]", Color.Purple, true);
                                    dud_SboName.Items.RemoveAt(dud_SboName.SelectedIndex);
                                    dud_SboName.SelectedIndex += 1;
                                    Console.Beep(2000, 500);
                                    Thread.Sleep(2000);
                                    CheckLoginSbo = false;
                                    continue;
                                }
                                catch
                                {
                                    mylib.AppendText(richOutput, "SBO: All members cannot login", Color.Red, true);
                                    dud_SboName.ForeColor = System.Drawing.Color.Red;
                                    btnLogin.Enabled = true;
                                    btnBet.Enabled = false;
                                    Console.Beep(1000, 2000);
                                    Thread.Sleep(2000);
                                    Complete = true;
                                    StopThread(ThreadSbo);
                                    StopThread(ThreadIbet);
                                }
                            }
                            else
                            {
                                dud_SboName.ForeColor = System.Drawing.Color.Black;
                                mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Credit ok", Color.DarkBlue, true);
                                CheckLoginSbo = true;
                            }
                        }
                        catch
                        {
                            mylib.AppendText(richOutput, "SBO ERROR: [" + dud_SboName.Text + "] " + Credit, Color.Red, true);
                            dud_SboName.ForeColor = System.Drawing.Color.Red;
                            btnLogin.Enabled = true;
                            btnBet.Enabled = false;
                            Console.Beep(1000, 2000);
                            Thread.Sleep(2000);
                            Complete = true;
                            StopThread(ThreadSbo);
                            StopThread(ThreadIbet);
                        }
                    }

                    #endregion
                    /////////////////////////////////////////////////////////////Login////////////////

                    j += 1;
                    #region CHANGE ACC
                    if(j==3000)
                    {
                        j = 0;
                        if (str_UserNameSbo.Split(',').Length != 1)
                        {
                            mylib.AppendText(richOutput, "SBO: CHANGE ACC [" + dud_SboName.Text + "]", Color.Purple, true);
                            dud_SboName.ForeColor = System.Drawing.Color.Red;
                            try
                            {
                                dud_SboName.SelectedIndex += 1;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginSbo = false;
                                continue;
                            }
                            catch
                            {
                                dud_SboName.SelectedIndex = 0;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginSbo = false;
                                continue;
                            }
                        }
                    }
                    #endregion
                    #region CHECK SBO LOGIN
                    if (j % 50==0)
                    {
                        string DataOddSboNonLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                        if (DataOddSboNonLive.IndexOf("odds-display") == -1)
                        {
                            ttBet.SetToolTip(btnLogin, "Login");
                            ttBet.SetToolTip(btnBet, "Bet");
                            dud_SboName.ForeColor = System.Drawing.Color.Red;
                            mylib.AppendText(richOutput, "SBO: LOGOUT [" + dud_SboName.Text + "]", Color.Red, true);
                            try
                            {
                                dud_SboName.SelectedIndex += 1;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginSbo = false;
                                continue;
                            }
                            catch
                            {
                                mylib.AppendText(richOutput, "SBO: All members cannot login", Color.Red, true);
                                dud_SboName.ForeColor = System.Drawing.Color.Red;
                                btnLogin.Enabled = true;
                                btnBet.Enabled = false;
                                Console.Beep(1000, 2000);
                                Thread.Sleep(2000);
                                Complete = true;
                                StopThread(ThreadSbo);
                                StopThread(ThreadIbet);
                            }
                        }
                    }
                    #endregion

                    List<objMatch> lstSboNonLiveBet = new List<objMatch>();
                    List<objMatch> lstSboLiveBet = new List<objMatch>();
                    if (CheckBetSbo)
                    {
                        if (str_BetSbo.Split(',').Length == 8)
                        {
                            #region GetLiveSBO
                            //string ScoreSbo = str_BetSbo.Split(',')[7];
                            lblSboStatus.ForeColor = System.Drawing.Color.DarkGoldenrod;
                            lblSboStatus.Text = "Bet";
                            lstSboLiveBet.Clear();
                            string DataOddSboLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                            string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive.Replace("\u200C", ""), "[[[", "]],[[") + "]";
                            leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
                            hsLeague.Clear();
                            foreach (string league in leagueDataLive.Split('\n'))
                            {
                                string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                if (leagueTemp.Split(',')[1].IndexOf("SPECIFIC") != -1 || leagueTemp.Split(',')[1].IndexOf("CORNERS") != -1 || leagueTemp.Split(',')[1].IndexOf("BOOKING") != -1 || leagueTemp.Split(',')[1].IndexOf("FANTASY MATCH") != -1 || leagueTemp.Split(',')[1].IndexOf("WHICH TEAM") != -1 || leagueTemp.Split(',')[1].IndexOf("TOTAL GOALS") != -1 || leagueTemp.Split(',')[1].IndexOf("Injury") != -1) continue;
                                hsLeague.Add(leagueTemp.Split(',')[0], leagueTemp.Split(',')[1]);
                            }

                            string matchDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "]],[[", "]],[[") + "]";
                            matchDataLive = matchDataLive.Replace("],[", "]\n[");
                            hsMatch.Clear();
                            foreach (string matchLive in matchDataLive.Split('\n'))
                            {
                                string matchTempLive = matchLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                string[] arr_matchTempLive = matchTempLive.Split(',');
                                string idmatchLive = Util.GetSubstringByStringLast(DataOddSboLive, "[", "," + arr_matchTempLive[0]);
                                hsMatch.Add(idmatchLive, hsLeague[arr_matchTempLive[2]].ToString() + "," + arr_matchTempLive[3] + "," + arr_matchTempLive[4] + "," + arr_matchTempLive[7]);

                            }
                            string oddDataLive = "[[" + Util.GetSubstringByString(DataOddSboLive, ",,[[", "]]],,") + "]]]";
                            hsOdd.Clear();

                            foreach (string OddTempLive in oddDataLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                            {
                                objMatch OddLive = new objMatch();
                                string OddTemp = OddTempLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                string[] arr_OddTemp = OddTemp.Split(',');
                                string infomatch = hsMatch[arr_OddTemp[1]].ToString();
                                OddLive.LeaugeName = infomatch.Split(',')[0];
                                OddLive.HomeName = infomatch.Split(',')[1];
                                OddLive.AwayName = infomatch.Split(',')[2];
                                OddLive.TimeLive = infomatch.Split(',')[3];

                                OddLive.IdKeo = arr_OddTemp[0];
                                OddLive.Keo = arr_OddTemp[5];
                                OddLive.BetType = arr_OddTemp[2];
                                OddLive.Odd1 = arr_OddTemp[6];
                                OddLive.Odd2 = arr_OddTemp[7];

                                lstSboLiveBet.Add(OddLive);
                            }

                            str_BetSbo = GetKeoSbo(lstSboLiveBet, str_BetSbo);
                            #endregion
                        }
                        else
                        {
                            #region GetNonLiveSBO
                            lblSboStatus.ForeColor = System.Drawing.Color.DarkGoldenrod;
                            lblSboStatus.Text = "Bet";
                            lstSboNonLiveBet.Clear();
                            string DataOddSboNonLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                            string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
                            leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");
                            hsLeague.Clear();
                            foreach (string league in leagueDataNonLive.Split('\n'))
                            {
                                string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                if (leagueTemp.Split(',')[1].IndexOf("SPECIFIC") != -1 || leagueTemp.Split(',')[1].IndexOf("CORNERS") != -1 || leagueTemp.Split(',')[1].IndexOf("BOOKING") != -1 || leagueTemp.Split(',')[1].IndexOf("FANTASY MATCH") != -1 || leagueTemp.Split(',')[1].IndexOf("WHICH TEAM") != -1 || leagueTemp.Split(',')[1].IndexOf("TOTAL GOALS") != -1 || leagueTemp.Split(',')[1].IndexOf("Injury") != -1) continue;
                                hsLeague.Add(leagueTemp.Split(',')[0], leagueTemp.Split(',')[1]);
                            }

                            string matchDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "]],[[", "]],[[") + "]";
                            matchDataNonLive = matchDataNonLive.Replace("],[", "]\n[");
                            hsMatch.Clear();
                            foreach (string matchNonLive in matchDataNonLive.Split('\n'))
                            {
                                string matchTempNonLive = matchNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                string[] arr_matchTempNonLive = matchTempNonLive.Split(',');
                                string idmatchNonLive = Util.GetSubstringByStringLast(DataOddSboNonLive, "[", "," + arr_matchTempNonLive[0]);
                                hsMatch.Add(idmatchNonLive, hsLeague[arr_matchTempNonLive[2]].ToString() + "," + arr_matchTempNonLive[3] + "," + arr_matchTempNonLive[4] + "," + arr_matchTempNonLive[7]);

                            }
                            string oddDataNonLive = "[[" + Util.GetSubstringByString(DataOddSboNonLive, ",,[[", "]]],,") + "]]]";
                            hsOdd.Clear();

                            foreach (string OddTempNonLive in oddDataNonLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                            {
                                objMatch OddNonLive = new objMatch();
                                string OddTemp = OddTempNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                string[] arr_OddTemp = OddTemp.Split(',');
                                string infomatch = hsMatch[arr_OddTemp[1]].ToString();
                                OddNonLive.LeaugeName = infomatch.Split(',')[0];
                                OddNonLive.HomeName = infomatch.Split(',')[1];
                                OddNonLive.AwayName = infomatch.Split(',')[2];
                                OddNonLive.TimeNonLive = infomatch.Split(',')[3];

                                OddNonLive.IdKeo = arr_OddTemp[0];
                                OddNonLive.Keo = arr_OddTemp[5];
                                OddNonLive.BetType = arr_OddTemp[2];
                                OddNonLive.Odd1 = arr_OddTemp[6];
                                OddNonLive.Odd2 = arr_OddTemp[7];

                                lstSboNonLiveBet.Add(OddNonLive);
                            }

                            str_BetSbo = GetKeoSbo(lstSboNonLiveBet, str_BetSbo);
                            #endregion
                        }

                        if (str_BetSbo != "")
                        {
                            #region BET SBO
                            Random Rtime = new Random();
                            Thread.Sleep(Rtime.Next(RandomTimeMin * 1000, RandomTimeMax * 1000));
                            while (true)
                            {
                                Thread.Sleep(100);
                                if (CheckBetIbet == false) break;
                                //if(check)
                            }
                            if (CheckBetSbo == false)
                            {
                                lblSboStatus.Text = "Bet Cancel";
                                Thread.Sleep(3000);
                                lblSboStatus.ForeColor = System.Drawing.Color.Black;
                                lblSboStatus.Text = "Credit:" + Credit;
                                continue;
                            }

                            string SboBetResult = BetSbo(str_BetSbo, money, txtGiaDoSbo.Text, "0", welcomeLink, IpSboLogin);
                            Thread.Sleep(3000);

                            if (SboBetResult == "BET FALL SBO")
                            {
                                lblSboStatus.ForeColor = System.Drawing.Color.Red;
                                lblSboStatus.Text = SboBetResult;
                                Console.Beep(2000, 2000);
                                Thread.Sleep(3000);
                                lblSboStatus.ForeColor = System.Drawing.Color.Black;
                                lblSboStatus.Text = "Credit:" + Credit;

                                string error = str_BetListSbo + "," + money + "\r\n";
                                File.AppendAllText(desktop_path + "\\SboErrorList.txt", error);

                                StopAll = true;
                                StopThread(ThreadBet);
                                StopThread(ThreadIbet);
                                StopThread(ThreadSbo);
                            }
                            else
                            {
                                if (SboBetResult == "Bet Success")
                                {
                                    lblSboStatus.ForeColor = System.Drawing.Color.Brown;
                                    lblSboStatus.Text = SboBetResult;
                                }
                                else
                                {
                                    player.SoundLocation = "OddChangeSbo.wav"; // Đường dẫn đến file cần chơi
                                    player.Play();
                                    lblSboStatus.ForeColor = System.Drawing.Color.Blue;
                                    lblSboStatus.Text = SboBetResult;
                                    Console.Beep(1000, 1000);
                                }
                                CheckBetSbo = false;
                                Thread.Sleep(3000);
                                Credit = httpSbo.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, IpSboLogin);
                                Credit = Credit.Split('\'')[3];
                                lblSboStatus.ForeColor = System.Drawing.Color.Black;
                                lblSboStatus.Text = "Credit:" + Credit;

                                try
                                {
                                    if (double.Parse(Credit) < maxbet)
                                    {
                                        mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Credit(" + Credit + ") is low, need to switch to other member", Color.Red, true);
                                        dud_SboName.ForeColor = System.Drawing.Color.Red;
                                        try
                                        {
                                            mylib.AppendText(richOutput, "SBO: CHANGE ACC [" + dud_SboName.Text + "]", Color.Purple, true);
                                            dud_SboName.Items.RemoveAt(dud_SboName.SelectedIndex);
                                            dud_SboName.SelectedIndex += 1;
                                            Console.Beep(2000, 500);
                                            Thread.Sleep(2000);
                                            CheckLoginSbo = false;
                                            continue;
                                        }
                                        catch
                                        {
                                            mylib.AppendText(richOutput, "SBO: All members cannot login", Color.Red, true);
                                            dud_SboName.ForeColor = System.Drawing.Color.Red;
                                            btnLogin.Enabled = true;
                                            btnBet.Enabled = false;
                                            Console.Beep(1000, 2000);
                                            Thread.Sleep(2000);
                                            Complete = true;
                                            StopThread(ThreadSbo);
                                            StopThread(ThreadIbet);
                                        }
                                    }
                                    else
                                    {
                                        dud_SboName.ForeColor = System.Drawing.Color.Black;
                                        mylib.AppendText(richOutput, "SBO: [" + dud_SboName.Text + "] Credit ok", Color.DarkBlue, true);
                                        CheckLoginSbo = true;
                                    }
                                }
                                catch
                                {
                                    mylib.AppendText(richOutput, "SBO ERROR: [" + dud_SboName.Text + "] " + Credit, Color.Red, true);
                                    dud_SboName.ForeColor = System.Drawing.Color.Red;
                                    btnLogin.Enabled = true;
                                    btnBet.Enabled = false;
                                    Console.Beep(1000, 2000);
                                    Thread.Sleep(2000);
                                    Complete = true;
                                    StopThread(ThreadSbo);
                                    StopThread(ThreadIbet);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            lblSboStatus.ForeColor = System.Drawing.Color.Red;
                            lblSboStatus.Text = "Bet Not Found";
                            Console.Beep(2000, 2000);
                            lblSboStatus.ForeColor = System.Drawing.Color.Black;
                            lblSboStatus.Text = "Credit:" + Credit;
                            string error = str_BetListSbo + "," + money + "\r\n";
                            File.AppendAllText(desktop_path + "\\SboErrorList.txt", error);
                            player.SoundLocation = "Quit.wav"; // Đường dẫn đến file cần chơi
                            player.Play();
                            Thread.Sleep(1500);
                            Complete = true;
                            //Process.Start("cmd", "/c taskkill /f /im BET.exe");
                            StopThread(ThreadSbo);
                            StopThread(ThreadIbet);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            });
            ThreadIbet = new Thread(delegate ()
            {
                string UrlIbet = "http://www.bong88.com";
                string Credit = "", IpIbetLogin = "";
                string UrlSport = "", UrlHostIbet = "";
                double maxbet = 0;
                int i = 0;
                while (true)
                {
                    if (CheckLoginIbet == false)
                    {
                        BetCountIbet = 0;
                        i = 5000;
                        #region LOGIN IBET
                        IpIbetLogin = txtIPIbet.Text;
                        maxbet = double.Parse(str_Money.Split(',')[str_Money.Split(',').Length - 1]) / double.Parse(txtGiaDoIbet.Text);
                        string DataIbet = "";
                        httpIbet.ClearCookies();
                        string loginurl = httpIbet.FetchResponseUri(UrlIbet, HttpHelper.HttpMethod.Get, null, null, IpIbetLogin);
                        DataIbet = httpIbet.Fetch(loginurl, HttpHelper.HttpMethod.Get, null, null, IpIbetLogin);
                        string code = Util.HtmlGetAttributeValue(DataIbet, "value", "//input[@id='txtCode']");
                        string tk = Util.HtmlGetAttributeValue(DataIbet, "value", "//input[@id='__tk']");
                        DataIbet = httpIbet.Fetch(UrlIbet + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, UrlIbet, "selLang=en&txtID=" + dud_IbetName.Text + "&txtPW=" + mylib.MD5(mylib.CFS(common_password) + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=", IpIbetLogin);
                        if (DataIbet.Contains("Login too often,please wait 5 minutes"))
                        {
                            mylib.AppendText(richOutput, "IBET: [" + dud_IbetName.Text + "] Login too often,please wait 5 minutes", Color.Red, true);
                            dud_IbetName.ForeColor = System.Drawing.Color.Red;
                            try
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.SelectedIndex += 1;
                                Console.Beep(500, 500);
                                Thread.Sleep(1000);
                                continue;
                            }
                            catch
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                btnLogin.Enabled = true;
                                btnBet.Enabled = false;
                                btnBet.Text = "Bet";
                                Console.Beep(1000, 2000);
                                Thread.Sleep(2000);
                                Complete = true;
                                StopThread(ThreadSbo);
                                StopThread(ThreadIbet);
                            }
                        }
                        string VerifyInfo_url = Util.GetSubstringByString(DataIbet, "window.location='.", "';</script>");
                        UrlSport = httpIbet.FetchResponseUri(UrlIbet + VerifyInfo_url, HttpHelper.HttpMethod.Get, UrlIbet + "/ProcessLogin.aspx", null, IpIbetLogin);
                        UrlHostIbet = UrlSport.Replace("/sports", "");
                        DataIbet = httpIbet.Fetch(UrlSport, HttpHelper.HttpMethod.Get, UrlIbet + "/ProcessLogin.aspx", null, IpIbetLogin);
                        if (DataIbet != "")
                        {
                            mylib.AppendText(richOutput, "IBET: [" + dud_IbetName.Text + "] Login successfully", Color.DarkGoldenrod, true);
                            CheckLoginIbet = true;
                        }
                        else
                        {
                            mylib.AppendText(richOutput, "IBET: LOGIN FAILED [" + dud_IbetName.Text + "] Data Empty", Color.Red, true);
                            
                            try
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                dud_IbetName.SelectedIndex += 1;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginIbet = false;
                                continue;
                            }
                            catch
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                dud_IbetName.SelectedIndex = 0;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginIbet = false;
                                continue;
                            }
                            
                        }
                        #endregion

                        #region CHECK IBET CREDIT
                        Credit = Util.GetSubstringByString(DataIbet, "\"BCredit\":\"", ".");
                        lblIbetStatus.Text = "Credit:" + Credit;

                        try
                        {
                            if (double.Parse(Credit) < maxbet)
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                try
                                {
                                    dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                    dud_IbetName.Items.RemoveAt(dud_IbetName.SelectedIndex);
                                    dud_IbetName.SelectedIndex += 1;
                                    Console.Beep(500, 500);
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                catch
                                {
                                    mylib.AppendText(richOutput, "IBET: All members cannot login", Color.Red, true);
                                    dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                    btnLogin.Enabled = true;
                                    btnBet.Enabled = false;
                                    Console.Beep(1000, 2000);
                                    Thread.Sleep(2000);
                                    Complete = true;
                                    StopThread(ThreadSbo);
                                    StopThread(ThreadIbet);
                                }
                            }
                            else
                            {
                                dud_IbetName.ForeColor = System.Drawing.Color.Black;
                                mylib.AppendText(richOutput, "IBET: [" + dud_IbetName.Text + "] Credit ok", Color.DarkGoldenrod, true);
                                CheckLoginIbet = true;
                            }
                        }
                        catch
                        {
                            mylib.AppendText(richOutput, "IBET: ERROR [" + dud_IbetName.Text + "] " + Credit, Color.Red, true);
                            dud_IbetName.ForeColor = System.Drawing.Color.Red;
                            btnLogin.Enabled = true;
                            btnBet.Enabled = false;
                            Console.Beep(1000, 2000);
                            Thread.Sleep(2000);
                            Complete = true;
                            StopThread(ThreadSbo);
                            StopThread(ThreadIbet);
                        }
                        #endregion
                    }
                    i += 1;
                    #region CHANGE ACC IBET
                    if (i==10000)
                    {
                        i = 0;
                        if(str_UserNameIbet.Split(',').Length!=1)
                        {
                            try
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                dud_IbetName.SelectedIndex += 1;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginIbet = false;
                                continue;
                            }
                            catch
                            {
                                mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                dud_IbetName.SelectedIndex = 0;
                                Console.Beep(2000, 500);
                                Thread.Sleep(2000);
                                CheckLoginIbet = false;
                                continue;
                            }
                        }
                    }
                    #endregion
                    if (i % 200==0)
                    {
                        string DataCredit = httpIbet.Fetch(UrlHostIbet + "/Customer/Balance", HttpHelper.HttpMethod.Post, UrlSport, null, IpIbetLogin, UrlHostIbet);
                    }

                    if (CheckBetIbet)
                    {
                        if(i<3000)
                        {
                            CheckBetIbet = false;
                            CheckBetSbo = false;
                            Thread.Sleep(3000);
                            lblIbetStatus.ForeColor = System.Drawing.Color.White;
                            lblIbetStatus.Text = "Ibet Login(" + i.ToString() + ")";
                            continue;
                        }
                        #region BET IBET
                        lblIbetStatus.ForeColor = System.Drawing.Color.DarkGoldenrod;
                        lblIbetStatus.Text = "Bet";

                        string IbetBetResult = BetIbet(str_BetIbet, money, txtGiaDoIbet.Text, UrlSport, IpIbetLogin);
                        //Thread.Sleep(3000);
                        if (IbetBetResult == "BET FALL IBET")
                        {
                            lblIbetStatus.ForeColor = System.Drawing.Color.Red;
                            lblIbetStatus.Text = IbetBetResult;
                            CheckBetIbet = false;
                            CheckBetSbo = false;
                            Console.Beep(2000, 2000);
                            Thread.Sleep(3000);
                            lblIbetStatus.ForeColor = System.Drawing.Color.Black;
                            lblIbetStatus.Text = "Credit:" + Credit;
                            continue;
                        }
                        else if (IbetBetResult.IndexOf("Down") != -1)
                        {
                            lblIbetStatus.ForeColor = System.Drawing.Color.Blue;
                            lblIbetStatus.Text = IbetBetResult;
                            CheckBetIbet = false;
                            CheckBetSbo = false;
                            Console.Beep(2000, 2000);
                            Thread.Sleep(3000);
                            lblIbetStatus.ForeColor = System.Drawing.Color.Black;
                            lblIbetStatus.Text = "Credit:" + Credit;
                            continue;
                        }
                        else
                        {
                            if (IbetBetResult == "Bet Success")
                            {
                                lblIbetStatus.ForeColor = System.Drawing.Color.Brown;
                                lblIbetStatus.Text = IbetBetResult;
                                player.SoundLocation = "success_ibet.wav";
                                player.Play();
                                CheckBetIbet = false;
                                i = 9990;
                            }
                            else
                            {
                                lblIbetStatus.ForeColor = System.Drawing.Color.Purple;
                                lblIbetStatus.Text = IbetBetResult;
                                player.SoundLocation = "OddUp.wav";
                                player.Play();
                                CheckBetIbet = false;
                            }

                            string DataCredit = httpIbet.Fetch(UrlHostIbet + "/Customer/Balance", HttpHelper.HttpMethod.Post, UrlSport, null, IpIbetLogin, UrlHostIbet);
                            Credit = Util.GetSubstringByString(DataCredit, "\"BCredit\":\"", ".");
                            Thread.Sleep(3000);
                            lblIbetStatus.ForeColor = System.Drawing.Color.Black;
                            lblIbetStatus.Text = "Credit:" + Credit;

                            try
                            {
                                if (double.Parse(Credit) < maxbet)
                                {
                                    mylib.AppendText(richOutput, "IBET: CHANGE ACC[" + dud_IbetName.Text + "]", Color.Purple, true);
                                    dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                    try
                                    {
                                        dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                        dud_IbetName.Items.RemoveAt(dud_IbetName.SelectedIndex);
                                        dud_IbetName.SelectedIndex += 1;
                                        Console.Beep(500, 500);
                                        Thread.Sleep(1000);
                                        continue;
                                    }
                                    catch
                                    {
                                        mylib.AppendText(richOutput, "IBET: All members cannot login", Color.Red, true);
                                        dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                        btnLogin.Enabled = true;
                                        btnBet.Enabled = false;
                                        Console.Beep(1000, 2000);
                                        Thread.Sleep(2000);
                                        Complete = true;
                                        StopThread(ThreadSbo);
                                        StopThread(ThreadIbet);
                                    }
                                }
                                else
                                {
                                    dud_IbetName.ForeColor = System.Drawing.Color.Black;
                                    mylib.AppendText(richOutput, "IBET: [" + dud_IbetName.Text + "] Credit ok", Color.DarkGoldenrod, true);
                                    CheckLoginIbet = true;
                                }
                            }
                            catch
                            {
                                mylib.AppendText(richOutput, "IBET: ERROR [" + dud_IbetName.Text + "] " + Credit, Color.Red, true);
                                dud_IbetName.ForeColor = System.Drawing.Color.Red;
                                btnLogin.Enabled = true;
                                btnBet.Enabled = false;
                                Console.Beep(1000, 2000);
                                Thread.Sleep(2000);
                                Complete = true;
                                StopThread(ThreadSbo);
                                StopThread(ThreadIbet);
                            }

                            //CheckBetIbet = false;
                            Thread.Sleep(3000);
                        }
                            #endregion
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            });
            if (PressBetOne == false)
            {
                if (richOutput.Text != "Data Group Empty")
                {
                    //btnLogin.Enabled = false;
                    btnBet.Enabled = true;
                    bt_Stop.Enabled = true;
                    ThreadSbo.Start();
                    ThreadIbet.Start();
                }
                else
                {
                    //btnLogin.Enabled = false;
                    //btnBet.Enabled = false;
                    richOutput.Clear();
                    mylib.AppendText(richOutput, "NO ACC", Color.Red);
                }
            }
        }
        private void btnBet_Click(object sender, EventArgs e)
        {
            ttBet.SetToolTip(bt_Stop, str_Money);
            ThreadBet = new Thread(delegate ()
            {
                while (true)
                {
                    btnBet.Text = "Bet";
                    money = RandomMoney(str_Money);
                    string TypeBet = "";
                    if (cb_HdpFT.Checked) TypeBet += "1";
                    if (cb_OuFT.Checked) TypeBet += "3";
                    if (cb_HdpH1.Checked) TypeBet += "7";
                    if (cb_OuH1.Checked) TypeBet += "9";
                    if (cb_Live.Checked)
                    {
                        str_CompareLive = UtilSoccer.FilterBet(str_CompareLive, TypeBet, dud_Profit.Text, dud_Leauge.Text);
                    }
                    else
                    {
                        str_CompareLive = "";
                    }
                    if (cb_NonLive.Checked)
                    {
                        str_CompareNonLive = UtilSoccer.FilterBet(str_CompareNonLive, TypeBet, dud_Profit.Text, dud_Leauge.Text);
                    }
                    else
                    {
                        str_CompareNonLive = "";
                    }

                    if (str_CompareNonLive != "" && Complete == false)
                    {
                        #region NonLive
                        string StyleBet = dud_Style.Text;
                        for (int i = 0; i < str_CompareNonLive.Split('\n').Length - 1; i++)
                        {
                            str_BetSbo = str_CompareNonLive.Split('\n')[i].Split(',')[0] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[1] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[2] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[3] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[4].Split('/')[0] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[5].Split('/')[0] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[6].Split('/')[0];
                            str_BetIbet = str_CompareNonLive.Split('\n')[i].Split(',')[0] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[1] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[2] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[3] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[4].Split('/')[1] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[5].Split('/')[1] + "," + str_CompareNonLive.Split('\n')[i].Split(',')[6].Split('/')[1];
                            double OddSbo = double.Parse(str_BetSbo.Split(',')[4]);
                            double OddIbet = double.Parse(str_BetIbet.Split(',')[4]);

                            if (cb_HdpFT.Checked == false && str_CompareNonLive.Split('\n')[i].Split(',')[2] == "1") continue;
                            if (cb_OuFT.Checked == false && str_CompareNonLive.Split('\n')[i].Split(',')[2] == "3") continue;
                            if (cb_HdpH1.Checked == false && str_CompareNonLive.Split('\n')[i].Split(',')[2] == "7") continue;
                            if (cb_OuH1.Checked == false && str_CompareNonLive.Split('\n')[i].Split(',')[2] == "9") continue;

                            string BetSameAccSbo = str_BetSbo.Split(',')[0] + "," + str_BetSbo.Split(',')[1] + "," + str_BetSbo.Split(',')[2] + "," + dud_SboName.Text;
                            string BetSameOddSbo = str_BetSbo.Split(',')[0] + "," + str_BetSbo.Split(',')[1] + "," + str_BetSbo.Split(',')[2] + "," + str_BetSbo.Split(',')[3] + "," + str_BetSbo.Split(',')[4];
                            string BetSameAccIbet = str_BetIbet.Split(',')[0] + "," + str_BetIbet.Split(',')[1] + "," + str_BetIbet.Split(',')[2] + "," + dud_IbetName.Text;
                            string BetSameOddIbet = str_BetIbet.Split(',')[0] + "," + str_BetIbet.Split(',')[1] + "," + str_BetIbet.Split(',')[2] + "," + str_BetIbet.Split(',')[3] + "," + str_BetIbet.Split(',')[4];
                            if (str_BetListSbo.IndexOf(BetSameAccSbo) == -1 && str_BetListSbo.IndexOf(BetSameOddSbo) == -1 && str_BetListIbet.IndexOf(BetSameAccIbet) == -1 && str_BetListIbet.IndexOf(BetSameOddIbet) == -1)
                            {
                                btnBet.Text = "Betting";
                                switch (StyleBet)
                                {
                                    case "ALL":
                                        CheckBetSbo = true;
                                        CheckBetIbet = true;
                                        break;
                                    case "SBO W":
                                        if (OddSbo < 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = true;
                                        }
                                        break;
                                    case "IBET W":
                                        if (OddIbet < 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = true;
                                        }
                                        break;
                                    case "W":
                                        if (OddSbo < 0 && OddIbet > 0)
                                        {
                                            CheckBetSbo = false;
                                            CheckBetIbet = true;
                                        }
                                        else if (OddSbo > 0 && OddIbet < 0)
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        break;
                                    case "L":
                                        if (OddSbo < 0 && OddIbet > 0)
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = false;
                                        }
                                        else if (OddSbo > 0 && OddIbet < 0)
                                        {
                                            CheckBetSbo = false;
                                            CheckBetIbet = true;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        break;
                                }
                                break;
                            }
                        }
                        //if (CheckBetSbo == false && (CheckBetIbet == false && (StyleBet == "W" || StyleBet == "L")))
                        if (CheckBetSbo == false && CheckBetIbet == false)
                        {
                            ttBet.SetToolTip(btnBet, "No New Bet");
                            player.SoundLocation = "no_new_bet.wav"; // Đường dẫn đến file cần chơi
                            player.Play();
                            Thread.Sleep(3000);
                            Complete = true;
                            continue;
                        }
                        //while (CheckBetSbo || (CheckBetIbet && (StyleBet == "W" || StyleBet == "L")))
                        while (CheckBetSbo || CheckBetIbet )
                        {
                            Thread.Sleep(1000);
                        }
                        Complete = true;
                        #endregion
                    }
                    else if (str_CompareLive != "" && Complete == false)
                    {
                        #region Live
                        string StyleBet = dud_Style.Text;
                        for (int i = 0; i < str_CompareLive.Split('\n').Length - 1; i++)
                        {
                            str_BetSbo = str_CompareLive.Split('\n')[i].Split(',')[0] + "," + str_CompareLive.Split('\n')[i].Split(',')[1] + "," + str_CompareLive.Split('\n')[i].Split(',')[2] + "," + str_CompareLive.Split('\n')[i].Split(',')[3] + "," + str_CompareLive.Split('\n')[i].Split(',')[4].Split('/')[0] + "," + str_CompareLive.Split('\n')[i].Split(',')[5].Split('/')[0] + "," + str_CompareLive.Split('\n')[i].Split(',')[6].Split('/')[0] + "," + str_CompareLive.Split('\n')[i].Split(',')[9];
                            str_BetIbet = str_CompareLive.Split('\n')[i].Split(',')[0] + "," + str_CompareLive.Split('\n')[i].Split(',')[1] + "," + str_CompareLive.Split('\n')[i].Split(',')[2] + "," + str_CompareLive.Split('\n')[i].Split(',')[3] + "," + str_CompareLive.Split('\n')[i].Split(',')[4].Split('/')[1] + "," + str_CompareLive.Split('\n')[i].Split(',')[5].Split('/')[1] + "," + str_CompareLive.Split('\n')[i].Split(',')[6].Split('/')[1] + "," + str_CompareLive.Split('\n')[i].Split(',')[9];
                            double OddSbo = double.Parse(str_BetSbo.Split(',')[4]);
                            double OddIbet = double.Parse(str_BetIbet.Split(',')[4]);

                            if (cb_HdpFT.Checked == false && str_CompareLive.Split('\n')[i].Split(',')[2] == "1") continue;
                            if (cb_OuFT.Checked == false && str_CompareLive.Split('\n')[i].Split(',')[2] == "3") continue;
                            if (cb_HdpH1.Checked == false && str_CompareLive.Split('\n')[i].Split(',')[2] == "7") continue;
                            if (cb_OuH1.Checked == false && str_CompareLive.Split('\n')[i].Split(',')[2] == "9") continue;

                            string BetSameAccSbo = str_BetSbo.Split(',')[0] + "," + str_BetSbo.Split(',')[1] + "," + str_BetSbo.Split(',')[2] + "," + dud_SboName.Text;
                            string BetSameOddSbo = str_BetSbo.Split(',')[0] + "," + str_BetSbo.Split(',')[1] + "," + str_BetSbo.Split(',')[2] + "," + str_BetSbo.Split(',')[3] + "," + str_BetSbo.Split(',')[4];
                            string BetSameAccIbet = str_BetIbet.Split(',')[0] + "," + str_BetIbet.Split(',')[1] + "," + str_BetIbet.Split(',')[2] + "," + dud_IbetName.Text;
                            string BetSameOddIbet = str_BetIbet.Split(',')[0] + "," + str_BetIbet.Split(',')[1] + "," + str_BetIbet.Split(',')[2] + "," + str_BetIbet.Split(',')[3] + "," + str_BetIbet.Split(',')[4];
                            if (str_BetListSbo.IndexOf(BetSameAccSbo) == -1 && str_BetListSbo.IndexOf(BetSameOddSbo) == -1 && str_BetListIbet.IndexOf(BetSameAccIbet) == -1 && str_BetListIbet.IndexOf(BetSameOddIbet) == -1)
                            {
                                btnBet.Text = "Betting";
                                switch (StyleBet)
                                {
                                    case "ALL":
                                        CheckBetSbo = true;
                                        CheckBetIbet = true;
                                        break;
                                    case "SBO W":
                                        if (OddSbo < 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = true;
                                        }
                                        break;
                                    case "IBET W":
                                        if (OddIbet < 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = true;
                                        }
                                        break;
                                    case "W":
                                        if (OddSbo < 0 && OddIbet > 0)
                                        {
                                            CheckBetSbo = false;
                                            CheckBetIbet = true;
                                        }
                                        else if (OddSbo > 0 && OddIbet < 0)
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        break;
                                    case "L":
                                        if (OddSbo < 0 && OddIbet > 0)
                                        {
                                            CheckBetSbo = true;
                                            CheckBetIbet = false;
                                        }
                                        else if (OddSbo > 0 && OddIbet < 0)
                                        {
                                            CheckBetSbo = false;
                                            CheckBetIbet = true;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        break;
                                }
                                break;
                            }
                        }
                        //if (CheckBetSbo == false && (CheckBetIbet == false && (StyleBet == "W" || StyleBet == "L")))
                        if (CheckBetSbo == false && CheckBetIbet == false)
                        {
                            ttBet.SetToolTip(btnBet, "No New Bet");
                            player.SoundLocation = "no_new_bet.wav"; // Đường dẫn đến file cần chơi
                            player.Play();
                            Thread.Sleep(3000);
                            Complete = true;
                            continue;
                        }
                        //while (CheckBetSbo || (CheckBetIbet && (StyleBet == "W" || StyleBet == "L")))
                        while (CheckBetSbo || CheckBetIbet)
                        {
                            Thread.Sleep(1000);
                        }
                        Complete = true;
                        #endregion
                    }
                    else
                    {
                        ttBet.SetToolTip(btnBet, "Waitting Bet " + BetCountSbo + "/" + BetCountIbet);
                        Thread.Sleep(100);
                        if (str_CompareNonLive == ""&& str_CompareLive == "")
                        {
                            Complete = true;
                        }
                    }
                    Thread.Sleep(100);
                }
            });
            ThreadBet.Start();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopThread(ThreadSbo);
            StopThread(ThreadIbet);
            StopThread(ThreadBet);
            CheckLoginSbo = false;
            CheckLoginIbet = false;
            bt_Stop.Enabled = false;
            btnLogin.Enabled = true;
            btnBet.Enabled = false;
            btnBet.Text = "Bet";
            ttBet.SetToolTip(btnLogin, "Login");
            ttBet.SetToolTip(btnBet, "Bet");
            richOutput.Text = "STOP";
        }
        
        private void StopThread(Thread t)
        {
            try
            {
                t.Abort();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
