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
        HttpHelper httpSbo, httpIbet;
        Hashtable hsLeagueLive = new Hashtable();
        Hashtable hsMatchLive = new Hashtable();
        Hashtable hsOddLive = new Hashtable();
        Hashtable hsLeagueNonLive = new Hashtable();
        Hashtable hsMatchNonLive = new Hashtable();
        Hashtable hsOddNonLive = new Hashtable();
        List<objMatch> SboLive = new List<objMatch>();
        List<objMatch> SboNonLive = new List<objMatch>();
        List<objMatch> IbetLive = new List<objMatch>();
        List<objMatch> IbetNonLive = new List<objMatch>();
        public int IdChangeLive = 0, IdChangeNonLive = 0;
        public string str_CompareLive = "";
        public string str_CompareNonLive = "";
        public string str_UserNameSbo = "";
        public string str_UserNameIbet = "";
        public string str_Profit = "";
        //public string str_File = "";
        public string str_LeaugeSbo = "", str_LeaugeIbet = "", str_TeamSbo = "", str_TeamIbet = "";
        public string Group = "";
        public DateTime TimeLimit = DateTime.Now.AddHours(3);
        bool SboScan = true, IbetScan = true;
        Database db;
        public void LoadData()
        {
            dud_SboName.Text = "";
            dm_UserNameIbet.Text = "";
            dm_Profit.Text = "";
            lb_Group.Text = Group;
            groupName.Text = Group;

            foreach (string UserNameSbo in str_UserNameSbo.Split(','))
            {
                dud_SboName.Items.Add(UserNameSbo);
            }
            dud_SboName.SelectedIndex += 1;
            foreach (string UserNameIbet in str_UserNameIbet.Split(','))
            {
                dm_UserNameIbet.Items.Add(UserNameIbet);
            }
            dm_UserNameIbet.SelectedIndex += 1;
            foreach (string Profit in str_Profit.Split(','))
            {
                dm_Profit.Items.Add(Profit);
            }
            dm_Profit.SelectedIndex += 1;
            tb_Time.Text = TimeLimit.ToShortTimeString();
        }

        private void CompareControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        bool BeepFoundCompareNonLive = false;
        bool BeepFoundCompareLive = false;
        private void lb_Group_Click(object sender, EventArgs e)
        {
            if (BeepFoundCompareNonLive)
            {
                BeepFoundCompareNonLive = false;
                lb_Group.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                BeepFoundCompareNonLive = true;
                lb_Group.ForeColor = System.Drawing.Color.Red;
            }
        }

        public CompareControl()
        {
            InitializeComponent();
            db = new Database();
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void bt_CompareName_Click(object sender, EventArgs e)
        {
            if (dud_SboName.Text != "" && dm_UserNameIbet.Text != "")
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
                File.WriteAllText(@"NameCom.txt", str_LeaugeCompare + str_TeamCompare + str_TotalCompare);
                /////////////////////////////Compare Total////////////////////////////////////////////////////////////////////////////////
            }
        }
        bool CheckLoginSbo = false;

        private void button1_Click(object sender, EventArgs e)
        {
            Thread tNonLive = new Thread(delegate ()
            {
                //BetManager.createBet("sbo", "sbo0", "", "mqaiq13016");
                //BetManager.getBet("sbo0").login();
                //List<objMatch> lstSbo = BetManager.getBet("sbo0").getMatchOddNonLive();


                string usernameIbets = "BYCE0922A,BYCE0939A,BYCE09469,BYCE0959B,BYCE09112,byce09222,byce09333,byce09444,byce09555,byce09666";
                for (int i = 0; i < usernameIbets.Split(',').Length; i++)
                {
                    BetManager.createBet("ibet", "ibet" + i, "", usernameIbets.Split(',')[i]);
                    BetManager.getBet("ibet" + i).login();
                    rtb_Info.Text = rtb_Info.Text.Insert(0, string.Format("User {0}, {1} \n", usernameIbets.Split(',')[i], BetManager.getBet("ibet" + i).getMessage()));
                    Thread.Sleep(1000);
                }

                int index = 0;
                int count = 0;
                List<objMatch> lstIbet = new List<objMatch>();
                while (true)
                {
                    lstIbet = BetManager.getBet("ibet" + index).getMatchOddNonLive();
                    if (lstIbet.Count == 0)
                    {
                        Console.Beep(1000, 5000);
                        Thread.Sleep(5000);
                        //break;
                    }
                    if (lstIbet.Count > 0)
                        rtb_Info.Text = rtb_Info.Text.Insert(0, DateTime.Now.ToLongTimeString() + " " + lstIbet[0].HomeName + "\n");

                    count++;
                    if (count == 5)
                    {
                        index++;
                        count = 0;
                    }
                    if (index == 10)
                    {
                        index = 0;
                        count = 0;
                    }
                    Thread.Sleep(1000);
                }
            });
            tNonLive.Start();
        }

        private void btnCompareOddOldVersion_Click(object sender, EventArgs e)
        {
            httpSbo = new HttpHelper();

            string dataIbet = "";
            if (dud_SboName.Text != "" && dm_UserNameIbet.Text != "")
            {
                tTCompare.SetToolTip(bt_CompareOdd, "Compare Odd");
                Thread ThreadSbo = new Thread(delegate ()
                {
                    /////////////////////////////////////////////////////////////Login/////////////////////////////////////////////
                    string mainLink = "", welcomeLink = "";
                    int nlink = 0;
                    string str_UrlSbo = "img.eaxybox.com,www.warungharta.com,www.beer777.com,www.currybread.com,www.pic5678.com,www.pasangsini.com,www.kampungemas.com";
                    while (true)
                    {
                        #region LOGIN SBO
                        if (CheckLoginSbo == false)
                        {
                            httpSbo.ClearCookies();
                            string link = "";
                            string data = "";

                            //Link
                            link = "http://" + str_UrlSbo.Split(',')[nlink];
                            data = httpSbo.Fetch(link + "/betting.aspx", HttpHelper.HttpMethod.Get, null, null);
                            //Thread.Sleep(2000);
                            if (data.Contains("SBOBET") == false)
                            {
                                nlink += 1;
                                rtb_Info.Text = "Error Link";
                                Console.Beep(2000, 3000);
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
                            welcomeLink = httpSbo.FetchResponseUri(link + "/web/public/process-sign-in.aspx", HttpHelper.HttpMethod.Post, link + "/betting.aspx", post);
                            if (welcomeLink.IndexOf("web-root") == -1)
                            {
                                lb_Info.Text = "SBO LOGIN ERROR";
                                Thread.Sleep(2000);
                                CheckLoginSbo = false;
                                break;
                            }
                            string defaultLink = httpSbo.FetchResponseUri(welcomeLink, HttpHelper.HttpMethod.Get, link + "/betting.aspx", null);
                            if (defaultLink.IndexOf("termandconditions") != -1)
                            {
                                string UrlTermAndCondition = defaultLink;
                                string data_UrlTermAndCondition = httpSbo.Fetch(UrlTermAndCondition, HttpHelper.HttpMethod.Get, null, null);
                                string UrlLoginSbo = httpSbo.FetchResponseUri(UrlTermAndCondition, HttpHelper.HttpMethod.Post, UrlTermAndCondition, "action=I Agree");
                                string data_Login = httpSbo.Fetch(UrlLoginSbo, HttpHelper.HttpMethod.Get, UrlTermAndCondition, null);
                            }

                            string mainName = Util.GetSubstringByString(welcomeLink, "http://", "/web-root");
                            mainLink = "http://" + mainName;

                            if (mainName != "")
                            {
                                lb_Info.Text = "LOGIN SBO...";
                                CheckLoginSbo = true;
                            }

                            else
                            {
                                rtb_Info.Text = "SBO LOGIN ERROR";
                                CheckLoginSbo = false;
                                continue;
                            }
                        }
                        #endregion
                        /////////////////////////////////////////////////////////////Login////////////////
                        //while (true)
                        //{
                        try
                        {
                            #region SboLive
                            if (cb_Live.Checked)
                            {
                                SboLive.Clear();
                                string DataOddSboLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                DataOddSboLive = DataOddSboLive.Replace("\\u200C", "");

                                string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "[[[", "]],[[") + "]";
                                if (leagueDataLive == "[null]")
                                {
                                    cb_Live.Checked = false;
                                    continue;
                                }
                                leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
                                hsLeagueLive.Clear();
                                foreach (string league in leagueDataLive.Split('\n'))
                                {
                                    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                                    if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 || nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 || nameleagueTemp.IndexOf("Injury") != -1) continue;
                                    hsLeagueLive.Add(leagueTemp.Split(',')[0], nameleagueTemp);
                                }

                                string matchDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "]],[[", "]],[[") + "]";
                                matchDataLive = matchDataLive.Replace("],[", "]\n[");
                                hsMatchLive.Clear();
                                foreach (string matchLive in matchDataLive.Split('\n'))
                                {
                                    string matchTempLive = matchLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_matchTempLive = matchTempLive.Split(',');
                                    string idmatchLive = Util.GetSubstringByStringLast(DataOddSboLive, "[", "," + arr_matchTempLive[0]);
                                    try
                                    {
                                        hsMatchLive.Add(idmatchLive, hsLeagueLive[arr_matchTempLive[2]].ToString() + "," + arr_matchTempLive[3] + "," + arr_matchTempLive[4] + "," + arr_matchTempLive[7]);
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                                string oddDataLive = "[[" + Util.GetSubstringByString(DataOddSboLive, ",,[[", "]]],,") + "]]]";
                                hsOddLive.Clear();

                                foreach (string OddTempLive in oddDataLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                                {
                                    objMatch OddLive = new objMatch();
                                    string OddTemp = OddTempLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_OddTemp = OddTemp.Split(',');
                                    string infomatch = "";
                                    try
                                    {
                                        infomatch = hsMatchLive[arr_OddTemp[1]].ToString();
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    OddLive.LeaugeName = infomatch.Split(',')[0];
                                    OddLive.HomeName = infomatch.Split(',')[1];
                                    OddLive.AwayName = infomatch.Split(',')[2];
                                    OddLive.TimeLive = infomatch.Split(',')[3];

                                    OddLive.IdKeo = arr_OddTemp[0];
                                    OddLive.Keo = arr_OddTemp[5];
                                    OddLive.BetType = arr_OddTemp[2];
                                    OddLive.Odd1 = arr_OddTemp[6];
                                    OddLive.Odd2 = arr_OddTemp[7];

                                    if (cb_HdpFT.Checked == false && OddLive.BetType == "1") continue;
                                    if (cb_OuFT.Checked == false && OddLive.BetType == "3") continue;
                                    if (cb_HdpH1.Checked == false && OddLive.BetType == "7") continue;
                                    if (cb_OuH1.Checked == false && OddLive.BetType == "9") continue;

                                    SboLive.Add(OddLive);
                                }
                            }
                            #endregion
                        }
                        catch
                        {
                            lb_Info.Text = "SBOLIVE SCAN ERROR";
                        }
                        try
                        {
                            #region SboNonLive
                            if (cb_NonLive.Checked)
                            {
                                SboNonLive.Clear();
                                string DataOddSboNonLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
                                leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");

                                if (str_LeaugeSbo == "")
                                {
                                    hsLeagueNonLive.Clear();
                                    foreach (string league in leagueDataNonLive.Split('\n'))
                                    {
                                        string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                        string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                                        if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 || nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 || nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1) continue;//INJURY
                                        hsLeagueNonLive.Add(leagueTemp.Split(',')[0], nameleagueTemp);
                                        if (str_LeaugeSbo.IndexOf(nameleagueTemp) == -1)
                                        {
                                            str_LeaugeSbo += nameleagueTemp + ",";
                                        }
                                    }
                                }

                                string matchDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "]],[[", "]],[[") + "]";
                                matchDataNonLive = matchDataNonLive.Replace("],[", "]\n[");
                                hsMatchNonLive.Clear();
                                foreach (string matchNonLive in matchDataNonLive.Split('\n'))
                                {
                                    string matchTempNonLive = matchNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_matchTempNonLive = matchTempNonLive.Split(',');
                                    string idmatchNonLive = Util.GetSubstringByStringLast(DataOddSboNonLive, "[", "," + arr_matchTempNonLive[0]);
                                    try
                                    {
                                        hsMatchNonLive.Add(idmatchNonLive, hsLeagueNonLive[arr_matchTempNonLive[2]].ToString() + "," + arr_matchTempNonLive[3] + "," + arr_matchTempNonLive[4] + "," + arr_matchTempNonLive[7]);
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    if (str_TeamSbo.IndexOf(UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[3])) == -1 || str_TeamSbo.IndexOf(UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[4])) == -1)
                                    {
                                        str_TeamSbo += UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[3]) + "-" + UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[4]) + ",";
                                    }
                                }
                                string oddDataNonLive = "[[" + Util.GetSubstringByString(DataOddSboNonLive, ",,[[", "]]],,") + "]]]";
                                hsOddNonLive.Clear();

                                foreach (string OddTempNonLive in oddDataNonLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                                {
                                    objMatch OddNonLive = new objMatch();
                                    string OddTemp = OddTempNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_OddTemp = OddTemp.Split(',');
                                    string infomatch = "";
                                    try
                                    {
                                        infomatch = hsMatchNonLive[arr_OddTemp[1]].ToString();
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    OddNonLive.LeaugeName = infomatch.Split(',')[0];
                                    OddNonLive.HomeName = infomatch.Split(',')[1];
                                    OddNonLive.AwayName = infomatch.Split(',')[2];
                                    OddNonLive.TimeNonLive = infomatch.Split(',')[3];
                                    if (OddNonLive.TimeNonLive.Split('/').Length == 1)
                                    {
                                        OddNonLive.TimeNonLive = OddNonLive.TimeLive;
                                    }

                                    OddNonLive.IdKeo = arr_OddTemp[0];
                                    OddNonLive.Keo = arr_OddTemp[5];
                                    OddNonLive.BetType = arr_OddTemp[2];
                                    OddNonLive.Odd1 = arr_OddTemp[6];
                                    OddNonLive.Odd2 = arr_OddTemp[7];

                                    if (cb_HdpFT.Checked == false && OddNonLive.BetType == "1") continue;
                                    if (cb_OuFT.Checked == false && OddNonLive.BetType == "3") continue;
                                    if (cb_HdpH1.Checked == false && OddNonLive.BetType == "7") continue;
                                    if (cb_OuH1.Checked == false && OddNonLive.BetType == "9") continue;

                                    if (cb_IdNonLive.Checked == false)
                                    {
                                        if (OddNonLive.CheckTimeNonLive(TimeLimit) == false) continue;
                                    }

                                    SboNonLive.Add(OddNonLive);
                                }
                            }
                            #endregion
                            SboScan = false;
                            while (SboScan == false)
                            {
                                Thread.Sleep(10);
                            }
                        }
                        catch
                        {
                            lb_Info.Text = "SBONONLIVE SCAN ERROR";
                            Thread.Sleep(3000);
                            CheckLoginSbo = false;
                            cb_IdNonLive.Checked = true;
                            lb_Info.Text = "";
                        }
                    }
                });
                //ThreadSbo.Start();
                Thread ThreadIbet = new Thread(delegate ()
                {
                    httpIbet = new HttpHelper();
                    string mainHost = "", mainLink = "";

                    #region LoginIbet
                    if (CheckLoginIbet == false)
                    {
                        string url = "http://www.bong88.net";
                        dataIbet = httpIbet.Fetch(url + "/login888.aspx", HttpHelper.HttpMethod.Get, null, null);
                        string code = Util.HtmlGetAttributeValue(dataIbet, "value", "//input[@id='txtCode']");
                        string tk = Util.HtmlGetAttributeValue(dataIbet, "value", "//input[@id='__tk']");
                        dataIbet = httpIbet.Fetch(url + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, url + "/login888.aspx", "selLang=en&txtID=" + dm_UserNameIbet.Text + "&txtPW=" + mylib.MD5(mylib.CFS("Vvvv6868@") + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=");
                        if (dataIbet == "")
                        {
                            lb_Info.Text = "IBET LOGIN ERROR";
                            Console.Beep(1000, 2000);
                            Thread.Sleep(3000);
                            lb_Info.Text = "";
                        }
                        string verifyInfoLink = Util.GetSubstringByString(dataIbet, "window.location='.", "';</script>");
                        string sportLink = httpIbet.FetchResponseUri(url + verifyInfoLink, HttpHelper.HttpMethod.Get, url + "/ProcessLogin.aspx", null);
                        mainHost = sportLink.Replace("/sports", "");
                        if (mainHost.Contains("ChangePasswordPage"))
                        {
                            MessageBox.Show("ChangeAccountPassword");
                        }
                        mainLink = httpIbet.FetchResponseUri(mainHost + "/SwitchPlatform/SwitchToOtherSite/", HttpHelper.HttpMethod.Get, sportLink, null);
                        mainHost = mainLink.Replace("/main.aspx", "");
                        dataIbet = httpIbet.Fetch(mainLink, HttpHelper.HttpMethod.Get, sportLink, null);
                        dataIbet = httpIbet.Fetch(mainHost + "/topmenu.aspx", HttpHelper.HttpMethod.Get, mainLink, null);

                        string checklogin = "check";
                        Thread tCheckLogin = new Thread(delegate ()
                        {
                            while (checklogin != "")
                            {
                                Thread.Sleep(60000);
                                checklogin = httpIbet.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + dm_UserNameIbet.Text.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                            }
                            MessageBox.Show("CHECK LOGIN");
                        });
                        tCheckLogin.Start();
                    }
                    #endregion
                    while (true)
                    {
                        #region IbetLive
                        try
                        {
                            if (cb_Live.Checked)
                            {
                                IbetLive.Clear();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("SCAN LIVE ERROR");
                        }
                        #endregion
                        #region IbetNonLive   
                        //try
                        //{
                            if (cb_NonLive.Checked)
                            {
                                IbetNonLive.Clear();
                                dataIbet = httpIbet.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);
                            //if(dataIbet=="")
                            //{
                            //Thread.Sleep(1000);
                            //    dataIbet = httpIbet.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);
                            //    MessageBox.Show(dataIbet);
                            //}
                                rtb_Info.Text = dataIbet;
                                int ik = dataIbet.IndexOf("name=\"k");
                                int iv = dataIbet.IndexOf("value=\"v");
                                string kIbet = dataIbet.Substring(ik + 6, iv - ik - 8);
                                string vIbet = dataIbet.Substring(iv + 7, iv - ik - 8);
                                dataIbet = httpIbet.Fetch(mainHost + "/UnderOver_data.aspx?Market=t&Sport=1&DT=&RT=W&CT=&Game=0&OrderBy=0&OddsType=4&MainLeague=0&DispRang=0&" + kIbet + "=" + vIbet + "&key=dodds&_=1502509515441", HttpHelper.HttpMethod.Get, mainHost + "/UnderOver.aspx?Market=t&DispVer=new", null);
                                string LeaugeName = "", HomeName = "", AwayName = "", Time = "";
                                foreach (Match m in Regex.Matches(dataIbet, @"Nt\[\d+\]=\[.*\];"))
                                {
                                    string strIbetMatch = m.Value.ToString().Replace("];", "");
                                    strIbetMatch = Regex.Replace(strIbetMatch, @"Nt\[\d+\]=\[", "");
                                    strIbetMatch = strIbetMatch.Replace("'", "");

                                    string[] dataIbetMatch = strIbetMatch.Split(',');
                                    if (dataIbetMatch[5] != "")
                                    {
                                        LeaugeName = UtilSoccer.ChuanTenLeauge_Ibet(dataIbetMatch[5]);
                                    }
                                    if (LeaugeName.Contains("CORNERS") || LeaugeName.Contains("BOOKING") || LeaugeName.Contains("TEAM") || LeaugeName.Contains("MATCH"))
                                        continue;

                                    if (dataIbetMatch[6] != "")
                                    {
                                        HomeName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[6]);
                                        AwayName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[7]);
                                    }
                                    objMatch o;
                                    if (dataIbetMatch[12] != "")
                                    {
                                        Time = dataIbetMatch[12].Split(' ')[dataIbetMatch[12].Split(' ').Length - 1];
                                        Time = UtilSoccer.convertSoccerTime(Time);
                                    }
                                    ///FT///
                                    try
                                    {
                                        o = new objMatch();
                                        o.TimeNonLive = Time;
                                        o.LeaugeName = LeaugeName;
                                        o.HomeName = HomeName;
                                        o.AwayName = AwayName;
                                        o.IdKeo = dataIbetMatch[31];
                                        o.BetType = "1";
                                        o.Keo = dataIbetMatch[32];
                                        o.Odd1 = dataIbetMatch[33];
                                        o.Odd2 = dataIbetMatch[34];
                                        if (dataIbetMatch[35] == "a") o.Keo = "-" + o.Keo;
                                        o.Keo = UtilSoccer.formatkeo(o.Keo);
                                        IbetNonLive.Add(o);
                                    }
                                    catch
                                    { }



                                    ///OUFT///
                                    try
                                    {
                                        o = new objMatch();
                                        o.TimeNonLive = Time;
                                        o.LeaugeName = LeaugeName;
                                        o.HomeName = HomeName;
                                        o.AwayName = AwayName;
                                        o.IdKeo = dataIbetMatch[36];
                                        o.BetType = "3";
                                        o.Keo = dataIbetMatch[37];
                                        o.Odd1 = dataIbetMatch[38];
                                        o.Odd2 = dataIbetMatch[39];
                                        if (dataIbetMatch[35] == "a") o.Keo = "-" + o.Keo;
                                        o.Keo = UtilSoccer.formatkeo(o.Keo);
                                        IbetNonLive.Add(o);
                                    }
                                    catch
                                    { }

                                    ///HT///
                                    try
                                    {
                                        o = new objMatch();
                                        o.TimeNonLive = Time;
                                        o.LeaugeName = LeaugeName;
                                        o.HomeName = HomeName;
                                        o.AwayName = AwayName;
                                        o.IdKeo = dataIbetMatch[44];
                                        o.BetType = "7";
                                        o.Keo = dataIbetMatch[45];
                                        o.Odd1 = dataIbetMatch[46];
                                        o.Odd2 = dataIbetMatch[47];
                                        if (dataIbetMatch[48] == "a") o.Keo = "-" + o.Keo;
                                        o.Keo = UtilSoccer.formatkeo(o.Keo);
                                        IbetNonLive.Add(o);
                                    }
                                    catch { }
                                    ///HTOU///
                                    try
                                    {
                                        o = new objMatch();
                                        o.TimeNonLive = Time;
                                        o.LeaugeName = LeaugeName;
                                        o.HomeName = HomeName;
                                        o.AwayName = AwayName;
                                        o.IdKeo = dataIbetMatch[49];
                                        o.BetType = "9";
                                        o.Keo = dataIbetMatch[50];
                                        o.Odd1 = dataIbetMatch[51];
                                        o.Odd2 = dataIbetMatch[52];
                                        if (dataIbetMatch[48] == "a") o.Keo = "-" + o.Keo;
                                        o.Keo = UtilSoccer.formatkeo(o.Keo);
                                        IbetNonLive.Add(o);
                                    }
                                    catch { }
                                }
                            }
                        //}
                        //catch(Exception Ex)
                        //{
                        //    Console.Beep(1000, 5000);
                        //    MessageBox.Show(Ex.ToString());
                        //}
                        #endregion
                        Thread.Sleep(100);
                    }
                });
                ThreadIbet.Start();
            }
        }

        bool CheckLoginIbet = false;
        private string common_password = "Vvvv6868@";
        private void bt_CompareOdd_Click(object sender, EventArgs e)
        {
            httpSbo = new HttpHelper();
            int j = 0;
            if (dud_SboName.Text == "" && dm_UserNameIbet.Text == "")
            {
                tTCompare.SetToolTip(bt_CompareOdd, "Compare Odd");
                Thread ThreadSbo = new Thread(delegate ()
                {
                /////////////////////////////////////////////////////////////Login/////////////////////////////////////////////
                string mainLink = "", welcomeLink = "";
                int nlink = 0;
                string str_UrlSbo = "img.eaxybox.com,www.warungharta.com,www.beer777.com,www.currybread.com,www.pic5678.com,www.pasangsini.com,www.kampungemas.com";
                while (true)
                {
                        #region LOGIN SBO
                        if (CheckLoginSbo == false)
                        {
                            httpSbo.ClearCookies();
                            j = 0;
                            string link = "";
                            string data = "";

                            //Link
                            link = "http://" + str_UrlSbo.Split(',')[nlink];
                            data = httpSbo.Fetch(link + "/betting.aspx", HttpHelper.HttpMethod.Get, null, null);
                            //Thread.Sleep(2000);
                            if (data.Contains("SBOBET") == false)
                            {
                                nlink += 1;
                                rtb_Info.Text = "Error Link";
                                Console.Beep(2000, 3000);
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
                            welcomeLink = httpSbo.FetchResponseUri(link + "/web/public/process-sign-in.aspx", HttpHelper.HttpMethod.Post, link + "/betting.aspx", post);
                            if (welcomeLink.IndexOf("web-root") == -1)
                            {
                                lb_Info.Text = "SBO LOGIN ERROR";
                                Thread.Sleep(2000);
                                CheckLoginSbo = false;
                                break;
                            }
                            string defaultLink = httpSbo.FetchResponseUri(welcomeLink, HttpHelper.HttpMethod.Get, link + "/betting.aspx", null);
                            if (defaultLink.IndexOf("termandconditions") != -1)
                            {
                                string UrlTermAndCondition = defaultLink;
                                string data_UrlTermAndCondition = httpSbo.Fetch(UrlTermAndCondition, HttpHelper.HttpMethod.Get, null, null);
                                string UrlLoginSbo = httpSbo.FetchResponseUri(UrlTermAndCondition, HttpHelper.HttpMethod.Post, UrlTermAndCondition, "action=I Agree");
                                string data_Login = httpSbo.Fetch(UrlLoginSbo, HttpHelper.HttpMethod.Get, UrlTermAndCondition, null);
                            }

                            string mainName = Util.GetSubstringByString(welcomeLink, "http://", "/web-root");
                            mainLink = "http://" + mainName;
                            
                            if (mainName != "")
                            {
                                lb_Info.Text = "LOGIN SBO...";
                                CheckLoginSbo = true;
                            }

                            else
                            {
                                rtb_Info.Text = "SBO LOGIN ERROR";
                                CheckLoginSbo = false;
                                continue;
                            }
                        }
                        #endregion
                        /////////////////////////////////////////////////////////////Login////////////////
                        //while (true)
                        //{
                        try
                        {
                            #region SboLive
                            if (cb_Live.Checked)
                            {
                                SboLive.Clear();
                                string DataOddSboLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                DataOddSboLive = DataOddSboLive.Replace("\\u200C", "");

                                string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "[[[", "]],[[") + "]";
                                if (leagueDataLive == "[null]")
                                {
                                    cb_Live.Checked = false;
                                    continue;
                                }
                                leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
                                hsLeagueLive.Clear();
                                foreach (string league in leagueDataLive.Split('\n'))
                                {
                                    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                                    if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 || nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 || nameleagueTemp.IndexOf("Injury") != -1) continue;
                                    hsLeagueLive.Add(leagueTemp.Split(',')[0], nameleagueTemp);
                                }

                                string matchDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "]],[[", "]],[[") + "]";
                                matchDataLive = matchDataLive.Replace("],[", "]\n[");
                                hsMatchLive.Clear();
                                foreach (string matchLive in matchDataLive.Split('\n'))
                                {
                                    string matchTempLive = matchLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_matchTempLive = matchTempLive.Split(',');
                                    string idmatchLive = Util.GetSubstringByStringLast(DataOddSboLive, "[", "," + arr_matchTempLive[0]);
                                    try
                                    {
                                        hsMatchLive.Add(idmatchLive, hsLeagueLive[arr_matchTempLive[2]].ToString() + "," + arr_matchTempLive[3] + "," + arr_matchTempLive[4] + "," + arr_matchTempLive[7]);
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                                string oddDataLive = "[[" + Util.GetSubstringByString(DataOddSboLive, ",,[[", "]]],,") + "]]]";
                                hsOddLive.Clear();

                                foreach (string OddTempLive in oddDataLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                                {
                                    objMatch OddLive = new objMatch();
                                    string OddTemp = OddTempLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_OddTemp = OddTemp.Split(',');
                                    string infomatch = "";
                                    try
                                    {
                                        infomatch = hsMatchLive[arr_OddTemp[1]].ToString();
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    OddLive.LeaugeName = infomatch.Split(',')[0];
                                    OddLive.HomeName = infomatch.Split(',')[1];
                                    OddLive.AwayName = infomatch.Split(',')[2];
                                    OddLive.TimeLive = infomatch.Split(',')[3];

                                    OddLive.IdKeo = arr_OddTemp[0];
                                    OddLive.Keo = arr_OddTemp[5];
                                    OddLive.BetType = arr_OddTemp[2];
                                    OddLive.Odd1 = arr_OddTemp[6];
                                    OddLive.Odd2 = arr_OddTemp[7];

                                    if (cb_HdpFT.Checked == false && OddLive.BetType == "1") continue;
                                    if (cb_OuFT.Checked == false && OddLive.BetType == "3") continue;
                                    if (cb_HdpH1.Checked == false && OddLive.BetType == "7") continue;
                                    if (cb_OuH1.Checked == false && OddLive.BetType == "9") continue;

                                    SboLive.Add(OddLive);
                                }
                            }
                            #endregion
                        }
                        catch
                        {
                            lb_Info.Text = "SBOLIVE SCAN ERROR";
                        }
                        try
                        { 
                            #region SboNonLive
                            if (cb_NonLive.Checked)
                            {
                                SboNonLive.Clear();
                                string DataOddSboNonLive = httpSbo.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
                                leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");

                                if (str_LeaugeSbo == "")
                                {
                                    hsLeagueNonLive.Clear();
                                    foreach (string league in leagueDataNonLive.Split('\n'))
                                    {
                                        string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                        string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                                        if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 || nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 || nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1) continue;//INJURY
                                        hsLeagueNonLive.Add(leagueTemp.Split(',')[0], nameleagueTemp);
                                        if (str_LeaugeSbo.IndexOf(nameleagueTemp) == -1)
                                        {
                                            str_LeaugeSbo += nameleagueTemp + ",";
                                        }
                                    }
                                }

                                string matchDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "]],[[", "]],[[") + "]";
                                matchDataNonLive = matchDataNonLive.Replace("],[", "]\n[");
                                hsMatchNonLive.Clear();
                                foreach (string matchNonLive in matchDataNonLive.Split('\n'))
                                {
                                    string matchTempNonLive = matchNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_matchTempNonLive = matchTempNonLive.Split(',');
                                    string idmatchNonLive = Util.GetSubstringByStringLast(DataOddSboNonLive, "[", "," + arr_matchTempNonLive[0]);
                                    try
                                    {
                                        hsMatchNonLive.Add(idmatchNonLive, hsLeagueNonLive[arr_matchTempNonLive[2]].ToString() + "," + arr_matchTempNonLive[3] + "," + arr_matchTempNonLive[4] + "," + arr_matchTempNonLive[7]);
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    if (str_TeamSbo.IndexOf(UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[3])) == -1 || str_TeamSbo.IndexOf(UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[4])) == -1)
                                    {
                                        str_TeamSbo += UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[3]) + "-" + UtilSoccer.ChuanTenTeam_Sbo(arr_matchTempNonLive[4]) + ",";
                                    }
                                }
                                string oddDataNonLive = "[[" + Util.GetSubstringByString(DataOddSboNonLive, ",,[[", "]]],,") + "]]]";
                                hsOddNonLive.Clear();

                                foreach (string OddTempNonLive in oddDataNonLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                                {
                                    objMatch OddNonLive = new objMatch();
                                    string OddTemp = OddTempNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                    string[] arr_OddTemp = OddTemp.Split(',');
                                    string infomatch = "";
                                    try
                                    {
                                        infomatch = hsMatchNonLive[arr_OddTemp[1]].ToString();
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    OddNonLive.LeaugeName = infomatch.Split(',')[0];
                                    OddNonLive.HomeName = infomatch.Split(',')[1];
                                    OddNonLive.AwayName = infomatch.Split(',')[2];
                                    OddNonLive.TimeNonLive = infomatch.Split(',')[3];
                                    if (OddNonLive.TimeNonLive.Split('/').Length == 1)
                                    {
                                        OddNonLive.TimeNonLive = OddNonLive.TimeLive;
                                    }

                                    OddNonLive.IdKeo = arr_OddTemp[0];
                                    OddNonLive.Keo = arr_OddTemp[5];
                                    OddNonLive.BetType = arr_OddTemp[2];
                                    OddNonLive.Odd1 = arr_OddTemp[6];
                                    OddNonLive.Odd2 = arr_OddTemp[7];

                                    if (cb_HdpFT.Checked == false && OddNonLive.BetType == "1") continue;
                                    if (cb_OuFT.Checked == false && OddNonLive.BetType == "3") continue;
                                    if (cb_HdpH1.Checked == false && OddNonLive.BetType == "7") continue;
                                    if (cb_OuH1.Checked == false && OddNonLive.BetType == "9") continue;

                                    if (cb_IdNonLive.Checked == false)
                                    {
                                        if (OddNonLive.CheckTimeNonLive(TimeLimit) == false) continue;
                                    }

                                    SboNonLive.Add(OddNonLive);
                                }
                            }
                            #endregion
                            SboScan = false;
                            while (SboScan == false)
                            {
                                Thread.Sleep(10);
                            }
                        }
                        catch
                        {
                            lb_Info.Text = "SBONONLIVE SCAN ERROR";
                            Thread.Sleep(3000);
                            CheckLoginSbo = false;
                            cb_IdNonLive.Checked = true;
                            lb_Info.Text = "";
                        }
                    }
                });
                //ThreadSbo.Start();

                Thread ThreadIbet = new Thread(delegate ()
                {
                    SeleniumHelper selenium = new SeleniumHelper();
                    while (true)
                    {
                        #region LoginIbet
                        if (CheckLoginIbet == false)
                        {
                            try
                            {
                                selenium.GotoURL(linkBong88);//http://www.88bong.com/login888.aspx,http://www.88cado.com
                                selenium.SendKeys(selenium.FindElement(By.XPath("//input[@id='txtID']")), dm_UserNameIbet.Text);
                                selenium.SendKeys(selenium.FindElement(By.XPath("//input[@id='txtPW']")), "Vvvv6868@");
                                selenium.FindElement(By.XPath("//a[@class='login_btn']")).Click();
                                DayOfWeek ThuHienTai = DateTime.Now.DayOfWeek;
                                if (ThuHienTai == DayOfWeek.Friday || ThuHienTai == DayOfWeek.Saturday || ThuHienTai == DayOfWeek.Sunday)
                                {
                                    bool all_listodd = false;
                                    while (all_listodd == false)
                                    {
                                        try
                                        {
                                            Thread.Sleep(1000);
                                            selenium.FindElement(By.XPath("//*[@id='mainArea']/div/div[1]/div[2]/div[2]/div[1]")).Click();
                                            Thread.Sleep(1000);////*[@id='mainArea']/div/div[1]/div[2]/div[2]/div[2]/div[1]
                                            selenium.FindElement(By.XPath("//*[@id='mainArea']/div/div[1]/div[2]/div[2]/div[2]/div[1]")).Click();
                                            all_listodd = true;
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                Thread.Sleep(3000);
                                CheckLoginIbet = true;
                            }
                            catch
                            {
                                selenium.Close();
                                Thread.Sleep(1000);
                                continue;
                            }
                        }
                        #endregion

                        try
                        {
                            #region IbetLive
                            if (cb_Live.Checked)
                            {
                                IbetLive.Clear();
                                IWebElement OddTableLive = selenium.FindElement(By.XPath("//div[@class='oddsTable hdpou-a sport1'][1]"));
                                string htmlCodeLive = OddTableLive.GetAttribute("innerHTML");
                                HtmlNodeCollection leagueGroupLives = Util.HtmlGetNodeCollection(htmlCodeLive, "//div[@class='leagueGroup']");
                                foreach (HtmlNode leagueGroupLive in leagueGroupLives)
                                {
                                    string htmlMatchLive = leagueGroupLive.InnerHtml;
                                    string leagueName = Util.HtmlGetInnerText(htmlMatchLive, "//div[@class='leagueName']/span");
                                    if (leagueName.IndexOf("SPECIFIC") != -1 || leagueName.IndexOf("CORNERS") != -1 || leagueName.IndexOf("BOOKING") != -1 || leagueName.IndexOf("FANTASY MATCH") != -1 || leagueName.IndexOf("WHICH TEAM") != -1 || leagueName.IndexOf("TOTAL GOALS") != -1 || leagueName.IndexOf("Injury") != -1) continue;
                                    HtmlNodeCollection matchAreas = Util.HtmlGetNodeCollection(htmlMatchLive, "//div[@class='matchArea']/div");
                                    foreach (HtmlNode matchArea in matchAreas)
                                    {
                                        string htmlmatchArea = matchArea.InnerHtml;
                                        HtmlNode time = matchArea.FirstChild;
                                        string score = time.FirstChild.InnerText;
                                        string timeInfo = time.FirstChild.NextSibling.InnerText;

                                        string home = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[1]");
                                        string away = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[2]");
                                        //if (timeInfo == "Hiệp một"|| timeInfo == "H.Time")//H.Time
                                        //{
                                        //    MessageBox.Show(home + "/" + away);
                                        //}
                                        HtmlNodeCollection multiOdds = Util.HtmlGetNodeCollection(htmlmatchArea, "//div[@class='multiOdds']");
                                        foreach (HtmlNode multiOdd in multiOdds)
                                        {
                                            string htmlmultiOdd = multiOdd.InnerHtml;
                                            HtmlNodeCollection odds = Util.HtmlGetNodeCollection(htmlmultiOdd, "//div[@class='odds subtxt']");
                                            int i = 0;
                                            string bettype = "0";

                                            foreach (HtmlNode odd in odds)
                                            {
                                                if (odd.FirstChild.ChildNodes.Count > 1 && odd.InnerHtml.IndexOf("data-moid") != -1)
                                                {
                                                    objMatch OddLive = new objMatch();
                                                    i += 1;
                                                    switch (i)
                                                    {
                                                        case 1:
                                                            bettype = "1";
                                                            break;
                                                        case 2:
                                                            bettype = "3";
                                                            break;
                                                        case 3:
                                                            bettype = "7";
                                                            break;
                                                        case 4:
                                                            bettype = "9";
                                                            break;
                                                    }
                                                    string Odd1 = odd.FirstChild.LastChild.InnerText;
                                                    string Odd2 = odd.LastChild.LastChild.InnerText;
                                                    string Keo = "";
                                                    string IdKeo = odd.FirstChild.LastChild.FirstChild.GetAttributeValue("data-moid", "").Split(new string[] { "__" }, StringSplitOptions.None)[1];
                                                    if (odd.FirstChild.FirstChild.InnerText != "")
                                                    {
                                                        Keo = odd.FirstChild.FirstChild.InnerText;
                                                    }
                                                    else
                                                    {
                                                        Keo = "-" + odd.LastChild.FirstChild.InnerText;
                                                    }

                                                    OddLive.LeaugeName = leagueName;
                                                    OddLive.HomeName = home;
                                                    OddLive.AwayName = away;
                                                    OddLive.TimeLive = timeInfo;
                                                    //OddLive.TimeNonLive = "";
                                                    OddLive.Score = score;

                                                    OddLive.IdKeo = IdKeo;
                                                    OddLive.Keo = Keo;
                                                    OddLive.BetType = bettype;
                                                    OddLive.Odd1 = Odd1;
                                                    OddLive.Odd2 = Odd2;

                                                    if (cb_HdpFT.Checked == false && OddLive.BetType == "1") continue;
                                                    if (cb_OuFT.Checked == false && OddLive.BetType == "3") continue;
                                                    if (cb_HdpH1.Checked == false && OddLive.BetType == "7") continue;
                                                    if (cb_OuH1.Checked == false && OddLive.BetType == "9") continue;

                                                    IbetLive.Add(OddLive);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        catch
                        {
                            lb_Info.Text = "IBETLIVE SCAN ERROR";
                            Thread.Sleep(3000);
                            lb_Info.Text = "";
                        }
                        try
                        { 

                            #region IbetNonLive
                            if (cb_NonLive.Checked)
                            {
                                IbetNonLive.Clear();
                                IWebElement OddTableNonLive;

                                OddTableNonLive = selenium.FindElement(By.XPath("//div[@class='oddsTable hdpou-a sport1'][2]"), 1);
                                if (OddTableNonLive == null)
                                {
                                    OddTableNonLive = selenium.FindElement(By.XPath("//div[@class='oddsTable hdpou-a sport1'][1]"));
                                }
                                string htmlCodeNonLive = OddTableNonLive.GetAttribute("innerHTML");
                                HtmlNodeCollection leagueGroupNonLives = Util.HtmlGetNodeCollection(htmlCodeNonLive, "//div[@class='leagueGroup']");
                                foreach (HtmlNode leagueGroupNonLive in leagueGroupNonLives)
                                {
                                    string htmlMatchNonLive = leagueGroupNonLive.InnerHtml;
                                    string leagueName = Util.HtmlGetInnerText(htmlMatchNonLive, "//div[@class='leagueName']/span");
                                    if (leagueName.IndexOf("SPECIFIC") != -1 || leagueName.IndexOf("CORNERS") != -1 || leagueName.IndexOf("BOOKING") != -1 || leagueName.IndexOf("FANTASY MATCH") != -1 || leagueName.IndexOf("WHICH TEAM") != -1 || leagueName.IndexOf("TOTAL GOALS") != -1 || leagueName.IndexOf("Injury") != -1) continue;// Injury
                                    if (str_LeaugeIbet.IndexOf(UtilSoccer.ChuanTenLeauge_Ibet(leagueName)) == -1)
                                    {
                                        str_LeaugeIbet += UtilSoccer.ChuanTenLeauge_Ibet(leagueName) + ",";
                                    }
                                    HtmlNodeCollection matchAreas = Util.HtmlGetNodeCollection(htmlMatchNonLive, "//div[@class='matchArea']/div");
                                    foreach (HtmlNode matchArea in matchAreas)
                                    {
                                        string htmlmatchArea = matchArea.InnerHtml;
                                        string time = matchArea.FirstChild.InnerText.Split(' ')[1];
                                        string home = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[1]");
                                        string away = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[2]");
                                        if (str_TeamIbet.IndexOf(UtilSoccer.ChuanTenTeam_Ibet(home)) == -1 || str_TeamIbet.IndexOf(UtilSoccer.ChuanTenTeam_Ibet(away)) == -1)
                                        {
                                            str_TeamIbet += UtilSoccer.ChuanTenTeam_Ibet(home) + "-" + UtilSoccer.ChuanTenTeam_Ibet(away) + ",";
                                        }
                                        HtmlNodeCollection multiOdds = Util.HtmlGetNodeCollection(htmlmatchArea, "//div[@class='multiOdds']");
                                        foreach (HtmlNode multiOdd in multiOdds)
                                        {
                                            string htmlmultiOdd = multiOdd.InnerHtml;
                                            HtmlNodeCollection odds = Util.HtmlGetNodeCollection(htmlmultiOdd, "//div[@class='odds subtxt']");
                                            int i = 0;
                                            string bettype = "0";

                                            foreach (HtmlNode odd in odds)
                                            {
                                                if (odd.FirstChild.ChildNodes.Count > 1)
                                                {
                                                    objMatch OddNonLive = new objMatch();
                                                    i += 1;
                                                    switch (i)
                                                    {
                                                        case 1:
                                                            bettype = "1";
                                                            break;
                                                        case 2:
                                                            bettype = "3";
                                                            break;
                                                        case 3:
                                                            bettype = "7";
                                                            break;
                                                        case 4:
                                                            bettype = "9";
                                                            break;
                                                    }
                                                    string Odd1 = odd.FirstChild.LastChild.InnerText;
                                                    string Odd2 = odd.LastChild.LastChild.InnerText;
                                                    string Keo = "";
                                                    string IdKeo = odd.FirstChild.LastChild.FirstChild.GetAttributeValue("data-moid", "").Split(new string[] { "__" }, StringSplitOptions.None)[1];
                                                    if (odd.FirstChild.FirstChild.InnerText != "")
                                                    {
                                                        Keo = odd.FirstChild.FirstChild.InnerText;
                                                    }
                                                    else
                                                    {
                                                        Keo = "-" + odd.LastChild.FirstChild.InnerText;
                                                    }

                                                    OddNonLive.LeaugeName = leagueName;
                                                    OddNonLive.HomeName = home;
                                                    OddNonLive.AwayName = away;

                                                    DateTime DayCompare = new DateTime();
                                                    if (DateTime.Now.ToShortTimeString().IndexOf("PM") != -1)
                                                    {
                                                        DayCompare = DateTime.Now;
                                                    }
                                                    else
                                                    {
                                                        DayCompare = DateTime.Now.AddDays(-1);
                                                    }
                                                    string timerun = "";
                                                    if (time.IndexOf("PM") != -1)
                                                    {
                                                        timerun = DayCompare.ToShortDateString() + " " + time;
                                                    }
                                                    else
                                                    {
                                                        timerun = DayCompare.AddDays(1).ToShortDateString() + " " + time;
                                                    }
                                                    OddNonLive.TimeNonLive = timerun;


                                                    OddNonLive.IdKeo = IdKeo;
                                                    OddNonLive.Keo = Keo;
                                                    OddNonLive.BetType = bettype;
                                                    OddNonLive.Odd1 = Odd1;
                                                    OddNonLive.Odd2 = Odd2;

                                                    if (cb_HdpFT.Checked == false && OddNonLive.BetType == "1") continue;
                                                    if (cb_OuFT.Checked == false && OddNonLive.BetType == "3") continue;
                                                    if (cb_HdpH1.Checked == false && OddNonLive.BetType == "7") continue;
                                                    if (cb_OuH1.Checked == false && OddNonLive.BetType == "9") continue;

                                                    if (cb_IdLive.Checked == false)
                                                    {
                                                        if (OddNonLive.CheckTimeNonLive(TimeLimit) == false) continue;
                                                    }

                                                    IbetNonLive.Add(OddNonLive);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            IbetScan = false;
                            while (IbetScan == false)
                            {
                                Thread.Sleep(10);
                            }
                        }
                        catch
                        {
                            lb_Info.Text = "IBETNONLIVE SCAN ERROR";
                            Thread.Sleep(3000);
                            lb_Info.Text = "";
                        }
                    }
                });
                ThreadIbet.Start();
            
                Thread ThreadCompare = new Thread(delegate ()
                {
                    Hashtable HashCompareLive = new Hashtable();
                    ICollection keysHashCompareLive = HashCompareLive.Keys;
                    string str_CompareLiveTemp = "";
                    Hashtable HashCompareNonLive = new Hashtable();
                    ICollection keysHashCompareNonLive = HashCompareNonLive.Keys;
                    string str_CompareNonLiveTemp = "";
                    int TimeLive = 0, TimeNonLive = 0;
                    while (true)
                    {
                        if (SboScan != false || IbetScan != false)
                        {
                            if (SboScan == false)
                            {
                                dud_SboName.ForeColor = System.Drawing.Color.Blue;
                            }
                            if (IbetScan == false)
                            {
                                dm_UserNameIbet.ForeColor = System.Drawing.Color.Blue;
                            }
                            Thread.Sleep(10);
                        }
                        else
                        {
                            #region CompareLive
                            if (cb_Live.Checked&&SboLive.Count!=0)
                            {
                                dm_UserNameIbet.ForeColor = System.Drawing.Color.Blue;
                                str_CompareLive = "";
                                Hashtable HashSboLive = objMatch.ParseObjToHashTable(SboLive);
                                Hashtable HashIbetLive = objMatch.ParseObjToHashTable(IbetLive);

                                string CompareLive_key = "", CompareLive_value = "";
                                if (cb_IdLive.Checked)
                                {
                                    cb_IdLive.Checked = false;
                                    TimeLive = 0;
                                    HashCompareLive = objMatch.GetHashTableIdMark(SboLive, IbetLive, true);
                                    keysHashCompareLive = HashCompareLive.Keys;
                                    foreach (string key in keysHashCompareLive)
                                    {
                                        CompareLive_key += key + ",";
                                        CompareLive_value += HashCompareLive[key].ToString() + ",";
                                    }
                                    CompareLive_key = CompareLive_key.Substring(0, CompareLive_key.Length - 1);
                                    CompareLive_value = CompareLive_value.Substring(0, CompareLive_value.Length - 1);
                                }

                                TimeLive += 1;
                                if (TimeLive==50)
                                {
                                    cb_IdLive.Checked = true;
                                }

                                keysHashCompareLive = HashCompareLive.Keys;
                                IdChangeLive = 0;
                                foreach (string key in keysHashCompareLive)
                                {
                                    if (HashSboLive.ContainsKey(key) && HashIbetLive.ContainsKey(HashCompareLive[key].ToString()))
                                    {
                                        objMatch Odd_Sbo = HashSboLive[key] as objMatch;
                                        objMatch Odd_Ibet = HashIbetLive[HashCompareLive[key].ToString()] as objMatch;
                                        if (UtilSoccer.formatkeo(Odd_Sbo.Keo) == UtilSoccer.formatkeo(Odd_Ibet.Keo))
                                        {
                                            double LeagueOdd = UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Sbo.Odd2);
                                            if (UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Ibet.Odd2) >= double.Parse(dm_Profit.Text) / -100)
                                            {
                                                str_CompareLive += Odd_Sbo.HomeName + "," + Odd_Sbo.AwayName + "," + Odd_Sbo.BetType + "," + Odd_Sbo.Keo + "," + Odd_Sbo.Odd1 + "/" + Odd_Ibet.Odd2 + ",h/a," + Odd_Sbo.IdKeo + "/" + Odd_Ibet.IdKeo + "," + UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Ibet.Odd2) + "," + LeagueOdd + "," + Odd_Ibet.Score + "\r\n";
                                            }
                                            else if (UtilSoccer.profit_odd(Odd_Sbo.Odd2, Odd_Ibet.Odd1) >= double.Parse(dm_Profit.Text) / -100)
                                            {
                                                str_CompareLive += Odd_Sbo.HomeName + "," + Odd_Sbo.AwayName + "," + Odd_Sbo.BetType + "," + Odd_Sbo.Keo + "," + Odd_Sbo.Odd2 + "/" + Odd_Ibet.Odd1 + ",a/h," + Odd_Sbo.IdKeo + "/" + Odd_Ibet.IdKeo + "," + UtilSoccer.profit_odd(Odd_Sbo.Odd2, Odd_Ibet.Odd1) + "," + LeagueOdd + "," + Odd_Ibet.Score + "\r\n";
                                            }
                                        }
                                        else
                                        {
                                            IdChangeLive += 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                str_CompareLive = "";
                            }
                            #endregion

                            #region CompareNonLive
                            if (cb_NonLive.Checked)
                            {
                                dm_UserNameIbet.ForeColor = System.Drawing.Color.Blue;
                                str_CompareNonLive = "";
                                Hashtable HashSboNonLive = objMatch.ParseObjToHashTable(SboNonLive);
                                Hashtable HashIbetNonLive = objMatch.ParseObjToHashTable(IbetNonLive);

                                string CompareNonLive_key = "", CompareNonLive_value = "";
                                if (cb_IdNonLive.Checked)
                                {
                                    cb_IdNonLive.Checked = false;
                                    TimeNonLive = 0;
                                    HashCompareNonLive = objMatch.GetHashTableIdMark(SboNonLive, IbetNonLive, false);
                                    if(HashCompareNonLive.Count==0)
                                    {
                                        lb_Info.Text = "IbetNonLive Compare0";
                                        continue;
                                    }
                                    keysHashCompareNonLive = HashCompareNonLive.Keys;
                                    foreach (string key in keysHashCompareNonLive)
                                    {
                                        CompareNonLive_key += key + ",";
                                        CompareNonLive_value += HashCompareNonLive[key].ToString() + ",";
                                    }
                                    CompareNonLive_key = CompareNonLive_key.Substring(0, CompareNonLive_key.Length - 1);
                                    CompareNonLive_value = CompareNonLive_value.Substring(0, CompareNonLive_value.Length - 1);
                                }

                                TimeNonLive += 1;
                                if (TimeNonLive == 500)
                                {
                                    cb_IdNonLive.Checked = true;
                                }

                                keysHashCompareNonLive = HashCompareNonLive.Keys;
                                IdChangeNonLive = 0;
                                foreach (string key in keysHashCompareNonLive)
                                {
                                    if (HashSboNonLive.ContainsKey(key) && HashIbetNonLive.ContainsKey(HashCompareNonLive[key].ToString()))
                                    {
                                        objMatch Odd_Sbo = HashSboNonLive[key] as objMatch;
                                        objMatch Odd_Ibet = HashIbetNonLive[HashCompareNonLive[key].ToString()] as objMatch;
                                        if (UtilSoccer.formatkeo(Odd_Sbo.Keo) == UtilSoccer.formatkeo(Odd_Ibet.Keo))
                                        {
                                            double LeagueOdd = UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Sbo.Odd2);
                                            if (UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Ibet.Odd2) >= double.Parse(dm_Profit.Text) / -100)
                                            {
                                                str_CompareNonLive += Odd_Sbo.HomeName + "," + Odd_Sbo.AwayName + "," + Odd_Sbo.BetType + "," + Odd_Sbo.Keo + "," + Odd_Sbo.Odd1 + "/" + Odd_Ibet.Odd2 + ",h/a," + Odd_Sbo.IdKeo + "/" + Odd_Ibet.IdKeo + "," + UtilSoccer.profit_odd(Odd_Sbo.Odd1, Odd_Ibet.Odd2) + "," + LeagueOdd + "\r\n";
                                            }
                                            else if (UtilSoccer.profit_odd(Odd_Sbo.Odd2, Odd_Ibet.Odd1) >= double.Parse(dm_Profit.Text) / -100)
                                            {
                                                str_CompareNonLive += Odd_Sbo.HomeName + "," + Odd_Sbo.AwayName + "," + Odd_Sbo.BetType + "," + Odd_Sbo.Keo + "," + Odd_Sbo.Odd2 + "/" + Odd_Ibet.Odd1 + ",a/h," + Odd_Sbo.IdKeo + "/" + Odd_Ibet.IdKeo + "," + UtilSoccer.profit_odd(Odd_Sbo.Odd2, Odd_Ibet.Odd1) + "," + LeagueOdd + "\r\n";
                                            }
                                        }
                                        else
                                        {
                                            IdChangeNonLive += 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                str_CompareNonLive = "";
                            }
                            #endregion

                            if (str_CompareNonLive != str_CompareNonLiveTemp || str_CompareLive != str_CompareLiveTemp)
                            {
                                try
                                {
                                    db.UpdateInfoCompare(Group, str_CompareLive, str_CompareNonLive);
                                    str_CompareLiveTemp = str_CompareLive;
                                    str_CompareNonLiveTemp = str_CompareNonLive;
                                    rtb_Info.Text = "------------------------------------------LIVE(" + IdChangeLive + ")------------------------------------------\n" + str_CompareLive + "\n" + "------------------------------------------NONLIVE(" + IdChangeNonLive + ")------------------------------------------\n" + str_CompareNonLive;
                                }
                                catch
                                {
                                    rtb_Info.Text = "\nPUSH DATA ERROR";
                                }
                            }
                            if (str_CompareNonLive != "" && BeepFoundCompareNonLive|| str_CompareLive != "" && BeepFoundCompareLive)
                            {
                                Console.Beep(2000, 3000);
                            }
                            
                            dud_SboName.ForeColor = System.Drawing.Color.Red;
                            dm_UserNameIbet.ForeColor = System.Drawing.Color.Red;
                            SboScan = true; IbetScan = true;
                        }
                    }
                });
                ThreadCompare.Start();
            }
        }
        private void bt_Pre_Click(object sender, EventArgs e)
        {
            TimeLimit=TimeLimit.AddMinutes(-30);
            tb_Time.Text = TimeLimit.ToShortTimeString();
        }

        private void bt_Next_Click(object sender, EventArgs e)
        {
            TimeLimit=TimeLimit.AddMinutes(30);
            tb_Time.Text = TimeLimit.ToShortTimeString();
        }
    }
}
