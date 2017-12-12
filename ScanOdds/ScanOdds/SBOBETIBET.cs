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

namespace ScanOdds
{
    public partial class SBOBETIBET : UserControl
    {
        Database db;
        HttpHelper help;
        Hashtable hsLeague = new Hashtable();
        Hashtable hsMatch = new Hashtable();
        Hashtable hsOdd = new Hashtable();
        public List<objMatch> SboLive = new List<objMatch>();
        List<objMatch> SboNonLive = new List<objMatch>();
        List<objMatch> IbetLive = new List<objMatch>();
        List<objMatch> IbetNonLive = new List<objMatch>();
        Hashtable hsCheckOdd = new Hashtable();
        Thread ThreadSbo;
        bool sbochay = true, ibetchay = true, sboxong = false, ibetxong = false;

        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (sboxong == true && ibetxong == true)
            {
                count++;
                richCompareResult.Text += "compare: " + count + "\n";
                Thread.Sleep(5000);
                sboxong = false;
                ibetxong = false;
            }
            ibetchay = true;
            sbochay = true;
        }

        private void SBOBETIBET_Load(object sender, EventArgs e)
        {
            db = new Database();
        }

        public SBOBETIBET()
        {
            InitializeComponent();
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Label.CheckForIllegalCrossThreadCalls = false;
        }
        
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (btnScanSbobet.Text == "Scan")
            {
                btnScanSbobet.Text = "Stop";
                ThreadSbo = new Thread(delegate ()
                {
                    string linkName = "img.eaxybox.com";
                    string link = "http://" + linkName;
                    string data = "";

                    help = new HttpHelper();
                    data = help.Fetch(link + "/betting.aspx", HttpHelper.HttpMethod.Get, null, null);
                    string HidCK = Util.HtmlGetAttributeValue(data, "value", "//input[@id='HidCK']");
                    string tag = Util.EscapeDataString(Util.HtmlGetAttributeValue(data, "value", "//input[@id='tag']"));
                    string tk = Util.EscapeDataString(Util.GetSubstringByString(data, "'ms','ps'],[", "]));"));
                    string fingerprint = "2cf5547e23492b793471c401989931ac";

                    string post = "id=" + txtUsernameSbobet.Text + "&password=" + txtPasswordSbobet.Text + "&lang=en&tk=" + tk + "&type=form&tzDiff=1&HidCK=" + HidCK + "&tag=" + tag + "&fingerprint=" + fingerprint;
                    string welcomeLink = help.FetchResponseUri(link + "/web/public/process-sign-in.aspx", HttpHelper.HttpMethod.Post, link + "/betting.aspx", post);
                    string defaultLink = help.FetchResponseUri(welcomeLink, HttpHelper.HttpMethod.Get, link + "/betting.aspx", null);
                    string mainName = Util.GetSubstringByString(welcomeLink, "http://", "/web-root");
                    string mainLink = "http://" + mainName;
                    while (true && btnScanSbobet.Text == "Stop")
                    {
                        string scanTime = db.getServerStringDate();
                        if (sbochay)
                        {
                            try
                            {
                                sboxong = false;
                                lblSboStatus.Text = "Running";
                                ////////////////////////////////////////////////Live///////////////////////////////////////////////////////////
                                //string DataOddSboLive = help.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                //DataOddSboLive = DataOddSboLive.Replace("\\u200C", "");

                                //string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "[[[", "]],[[") + "]";
                                //leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
                                //hsLeague.Clear();
                                //foreach (string league in leagueDataLive.Split('\n'))
                                //{
                                //    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                                //    hsLeague.Add(leagueTemp.Split(',')[0], leagueTemp.Split(',')[1]);
                                //}

                                //string matchDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "]],[[", "]],[[") + "]";
                                //matchDataLive = matchDataLive.Replace("],[", "]\n[");
                                //hsMatch.Clear();
                                //foreach (string matchLive in matchDataLive.Split('\n'))
                                //{
                                //    string matchTempLive = matchLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                //    string[] arr_matchTempLive = matchTempLive.Split(',');
                                //    string idmatchLive = Util.GetSubstringByStringLast(DataOddSboLive, "[", "," + arr_matchTempLive[0]);
                                //    hsMatch.Add(idmatchLive, hsLeague[arr_matchTempLive[2]].ToString() + "," + arr_matchTempLive[3] + "," + arr_matchTempLive[4] + "," + arr_matchTempLive[7]);
                                //}
                                //string oddDataLive = "[[" + Util.GetSubstringByString(DataOddSboLive, ",,[[", "]]],,") + "]]]";
                                //hsOdd.Clear();
                                //SboLive.Clear();
                                //foreach (string OddTempLive in oddDataLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                                //{
                                //    try
                                //    {
                                //        objMatch OddLive = new objMatch();
                                //        string OddTemp = OddTempLive.Replace("[", "").Replace("]", "").Replace("'", "");
                                //        string[] arr_OddTemp = OddTemp.Split(',');
                                //        if (hsMatch[arr_OddTemp[1]] != null)
                                //        {
                                //            string infomatch = hsMatch[arr_OddTemp[1]].ToString();
                                //            OddLive.LeaugeName = infomatch.Split(',')[0];
                                //            OddLive.HomeName = infomatch.Split(',')[1];
                                //            OddLive.AwayName = infomatch.Split(',')[2];
                                //            OddLive.TimeLive = infomatch.Split(',')[3];

                                //            OddLive.IdKeo = arr_OddTemp[0];
                                //            OddLive.Keo = arr_OddTemp[5];
                                //            OddLive.BetType = arr_OddTemp[2];
                                //            OddLive.Odd1 = arr_OddTemp[6];
                                //            OddLive.Odd2 = arr_OddTemp[7];
                                //            //////////////////////////////////////////////Live///////////////////////////////////////////////////////////
                                //            SboLive.Add(OddLive);
                                //        }
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        MessageBox.Show(ex.Message);
                                //    }
                                //}

                                //////////////////////////////////////////////NonLive///////////////////////////////////////////////////////////
                                string DataOddSboNonLive = help.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                                string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
                                leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");
                                hsLeague.Clear();
                                foreach (string league in leagueDataNonLive.Split('\n'))
                                {
                                    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
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
                                SboNonLive.Clear();
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
                                    OddNonLive.scantime = scanTime;

                                    string key = OddNonLive.HomeName + OddNonLive.Keo + OddNonLive.BetType;
                                    if (!hsCheckOdd.Contains(key))
                                    {
                                        OddNonLive.afterseconds = 0;
                                        hsCheckOdd.Add(key, OddNonLive);
                                        SboNonLive.Add(OddNonLive);
                                    }
                                    else
                                    {
                                        if (((objMatch)hsCheckOdd[key]).Odd1 != OddNonLive.Odd1)
                                        {
                                            OddNonLive.afterseconds = DateTime.Parse(OddNonLive.scantime).Subtract(DateTime.Parse(((objMatch)hsCheckOdd[key]).scantime)).TotalSeconds;
                                            hsCheckOdd[key] = OddNonLive;
                                            SboNonLive.Add(OddNonLive);
                                        }
                                    }
                                    lblSboStatus.Text = OddNonLive.Show();

                                }
                                //////////////////////////////////////////////NonLive///////////////////////////////////////////////////////////                            

                                string sql_row = "";
                                List<string> lstSql = new List<string>();
                                int int_1000 = 0;
                                foreach (objMatch o in SboLive)
                                {
                                    int_1000++;
                                    sql_row += o.toSQL(true) + ",";

                                    if (int_1000 >= 1000)
                                    {
                                        lstSql.Add(sql_row);
                                        sql_row = "";
                                        int_1000 = 0;
                                    }
                                }
                                int_1000 = 0;
                                foreach (objMatch o in SboNonLive)
                                {
                                    int_1000++;
                                    sql_row += o.toSQL(false) + ",";

                                    if (int_1000 >= 1000)
                                    {
                                        lstSql.Add(sql_row);
                                        sql_row = "";
                                        int_1000 = 0;
                                    }
                                }
                                if (sql_row.Length > 0)
                                {
                                    lstSql.Add(sql_row);
                                }

                                foreach (string sql in lstSql)
                                {
                                    db.doInsertSboOdds(sql);
                                }
                            }
                            catch (Exception ex)
                            {
                                Thread.Sleep(5000);
                            }
                        }
                        Thread.Sleep(5000);
                    }
                });
                ThreadSbo.Start();
            }
            else {
                btnScanSbobet.Text = "Scan";
                ThreadSbo.Abort();
            }
        }

        private void btnScanIbet_Click(object sender, EventArgs e)
        {
            Thread ThreadIbet = new Thread(delegate ()
            {
                SeleniumHelper selenium = new SeleniumHelper();
                selenium.GotoURL("http://www.bong88.com");
                selenium.SendKeys(selenium.FindElement(By.XPath("//input[@id='txtID']")), txtUsernameIbet.Text);
                selenium.SendKeys(selenium.FindElement(By.XPath("//input[@id='txtPW']")), txtPasswordIbet.Text);
                selenium.FindElement(By.XPath("//a[@class='login_btn']")).Click();
                Thread.Sleep(20000);
                while (true)
                {
                    if (ibetchay)
                    {
                        try
                        {
                            ibetxong = false;
                            lblIbetStatus.Text = "Running";
                            IbetLive.Clear();
                            IbetNonLive.Clear();
                            IWebElement OddTableLive = selenium.FindElement(By.XPath("//div[@class='oddsTable hdpou-a sport1'][1]"));
                            IWebElement OddTableNonLive = selenium.FindElement(By.XPath("//div[@class='oddsTable hdpou-a sport1'][2]"));
                            string htmlCodeLive = OddTableLive.GetAttribute("innerHTML");
                            string htmlCodeNonLive = OddTableNonLive.GetAttribute("innerHTML");
                            HtmlNodeCollection leagueGroupLives = Util.HtmlGetNodeCollection(htmlCodeLive, "//div[@class='leagueGroup']");
                            HtmlNodeCollection leagueGroupNonLives = Util.HtmlGetNodeCollection(htmlCodeNonLive, "//div[@class='leagueGroup']");
                            //////////////////////////////////////////////Live///////////////////////////////////////////////////////////
                            foreach (HtmlNode leagueGroupLive in leagueGroupLives)
                            {
                                string htmlMatchLive = leagueGroupLive.InnerHtml;
                                string leagueName = Util.HtmlGetInnerText(htmlMatchLive, "//div[@class='leagueName']/span");
                                if (leagueName.IndexOf("SPECIFIC") != -1 || leagueName.IndexOf("CORNERS") != -1 || leagueName.IndexOf("BOOKING") != -1 || leagueName.IndexOf("FANTASY MATCH") != -1) continue;
                                HtmlNodeCollection matchAreas = Util.HtmlGetNodeCollection(htmlMatchLive, "//div[@class='matchArea']/div");
                                foreach (HtmlNode matchArea in matchAreas)
                                {
                                    string htmlmatchArea = matchArea.InnerHtml;
                                    HtmlNode time = matchArea.FirstChild;
                                    string score = time.FirstChild.InnerText;
                                    string timeInfo = time.FirstChild.NextSibling.InnerText;
                                    string home = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[1]");
                                    string away = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[2]");
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

                                                IbetLive.Add(OddLive);
                                            }
                                        }
                                    }
                                }
                            }
                            //////////////////////////////////////////////Live///////////////////////////////////////////////////////////

                            //////////////////////////////////////////////NonLive///////////////////////////////////////////////////////////
                            foreach (HtmlNode leagueGroupNonLive in leagueGroupNonLives)
                            {
                                string htmlMatchNonLive = leagueGroupNonLive.InnerHtml;
                                string leagueName = Util.HtmlGetInnerText(htmlMatchNonLive, "//div[@class='leagueName']/span");
                                if (leagueName.IndexOf("SPECIFIC") != -1 || leagueName.IndexOf("CORNERS") != -1 || leagueName.IndexOf("BOOKING") != -1 || leagueName.IndexOf("FANTASY MATCH") != -1) continue;
                                HtmlNodeCollection matchAreas = Util.HtmlGetNodeCollection(htmlMatchNonLive, "//div[@class='matchArea']/div");
                                foreach (HtmlNode matchArea in matchAreas)
                                {
                                    string htmlmatchArea = matchArea.InnerHtml;
                                    string time = matchArea.FirstChild.InnerText.Split(' ')[1];
                                    //string score = time.FirstChild.InnerText;
                                    //string timeInfo = time.FirstChild.NextSibling.InnerText;
                                    string home = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[1]");
                                    string away = Util.HtmlGetInnerText(htmlmatchArea, "//div[@class='event']/div[2]");
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
                                                //OddNonLive.TimeLive = timeInfo;
                                                OddNonLive.TimeNonLive = time;
                                                //OddNonLive.Score = score;

                                                OddNonLive.IdKeo = IdKeo;
                                                OddNonLive.Keo = Keo;
                                                OddNonLive.BetType = bettype;
                                                OddNonLive.Odd1 = Odd1;
                                                OddNonLive.Odd2 = Odd2;

                                                IbetNonLive.Add(OddNonLive);
                                            }
                                        }
                                    }
                                }
                            }
                            lblIbetStatus.Text = "Done";
                            ibetxong = true;
                            ibetchay = false;
                            while (ibetchay == false)
                            {
                                Thread.Sleep(1000);
                            }
                            //////////////////////////////////////////////NonLive///////////////////////////////////////////////////////////                        
                        }
                        catch (Exception ex)
                        {
                            lblIbetStatus.Text = "Error " + ex.Message;
                            Thread.Sleep(5000);
                            ibetxong = false;
                            ibetchay = true;
                        } 
                    }
                }
            });
            ThreadIbet.Start();
            
        }
    }
}
