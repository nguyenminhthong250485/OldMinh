using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BET_BET
{
    public class SboFunction : IFunction
    {
        Random ran = new Random();
        string link = "http://img.eaxybox.com";
        private string message = "";
        private string phieuchung = "";
        private string realmoney = "";
        string mainLink = "";
        string welcomeLink = "";
        string defaultLink = "";
        string key, ip, username, password, usd;
        int betcount = 0;
        HttpHelper http;
        Database db;
        Hashtable hsLeague = new Hashtable();
        Hashtable hsMatch = new Hashtable();
        Hashtable hsOdd = new Hashtable();
        List<objMatch> lstSboNonLiveBet = new List<objMatch>();
        List<objMatch> lstSboLiveBet = new List<objMatch>();

        public SboFunction(string key, string ip, string username, string password, string usd)
        {
            this.key = key;
            this.ip = ip;
            this.username = username;
            this.password = password;
            this.usd = usd;
            http = new HttpHelper();
            db = new Database();
        }
        public string getPhieuchung()
        {
            return phieuchung;
        }
        public string getRealMoney()
        {
            return realmoney;
        }
        public bool checklogin()
        {
            string credit = http.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
            try
            {
                double.Parse(credit.Split('\'')[3].Replace(",", ""));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void login()
        {
            string data = http.Fetch(link + "/betting.aspx", HttpHelper.HttpMethod.Get, null, null, ip);
            string HidCK = Util.HtmlGetAttributeValue(data, "value", "//input[@id='HidCK']");
            string tag = Util.EscapeDataString(Util.HtmlGetAttributeValue(data, "value", "//input[@id='tag']"));
            string tk = Util.EscapeDataString(Util.GetSubstringByString(data, "'ms','ps'],[", "]));"));
            string fingerprint = "2cf5547e23492b793471c401989931ac";
            string post = "id=" + username + "&password=" + password + "&lang=en&tk=" + tk + "&type=form&tzDiff=1&HidCK=" + HidCK + "&tag=" + tag + "&fingerprint=" + fingerprint;
            welcomeLink = http.FetchResponseUri(link + "/web/public/process-sign-in.aspx", HttpHelper.HttpMethod.Post, link + "/betting.aspx", post, ip);
            if (welcomeLink.IndexOf("web-root") == -1)
            {
                message = "Wrong User And Pass";
            }
            else
            {
                defaultLink = http.FetchResponseUri(welcomeLink, HttpHelper.HttpMethod.Get, link + "/betting.aspx", null, ip);
                if (defaultLink.IndexOf("termandconditions") != -1)
                {
                    string UrlTermAndCondition = defaultLink;
                    string data_UrlTermAndCondition = http.Fetch(UrlTermAndCondition, HttpHelper.HttpMethod.Get, null, null);
                    string UrlLoginSbo = http.FetchResponseUri(UrlTermAndCondition, HttpHelper.HttpMethod.Post, UrlTermAndCondition, "action=I Agree");
                    string data_Login = http.Fetch(UrlLoginSbo, HttpHelper.HttpMethod.Get, UrlTermAndCondition, null);
                }

                string mainName = Util.GetSubstringByString(welcomeLink, "http://", "/web-root");
                mainLink = "http://" + mainName;
                if (mainName != "")
                {
                    message = "LOGIN SUCCESS";
                    Thread tCheckLogin = new Thread(delegate () {
                        //while (true)
                        //{
                        //    Thread.Sleep(60000);
                        //    string credit = http.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
                        //    try
                        //    {
                        //        double.Parse(credit.Split('\'')[3].Replace(",", ""));
                        //    }
                        //    catch (Exception)
                        //    {
                        //        break;
                        //    }
                        //}
                    });
                    tCheckLogin.Start();
                }
                else
                {
                    message = "Login failed";
                }
            }
        }
        public string getCredit()
        {
            string credit = "";
            //credit = http.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
            credit = http.Fetch(mainLink + "/web-root/restricted/account/balance.aspx", HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
            string GiveCredit = Util.GetSubstringByString(credit, "<th>Given Credit</th>      <td>", "</td>");
            string BetCredit = Util.GetSubstringByString(credit, "<th>Bet Credit</th>      <td>", "</td>");
            credit = GiveCredit + "/" + BetCredit;
            return credit;
        }
        public string getMessage()
        {
            return message;
        }
        public string getBetList()
        {
            http.Fetch(defaultLink, HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
            return http.Fetch(mainLink + "/web-root/restricted/betlist/running-bet-list.aspx?popout=1", HttpHelper.HttpMethod.Get, defaultLink, null, ip);
        }
        public void playBetNonLive(ticket o, string money, string IsLive, string phieuchung, string group,bool AutoAccept=true)
        {
            o = GetKeoSbo(lstSboNonLiveBet, o);
            if (o != null)
            {
                int usdbet = int.Parse(money) / int.Parse(usd);
                double hdp = o.hdp;
                string keoid = o.keoid;
                double odd = o.odd;
                string bettype = o.bettype;
                string choose = o.choose;
                string hdpType = "";
               
                if (bettype == "7" || bettype == "9")
                {
                    hdpType = "2";
                }

                string UrlTicketSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/ticket") + "&id=" + keoid + "&op=" + choose + "&odds=" + odd + "&hdpType=" + hdpType + "&isor=0&isLive=" + IsLive + "&betpage=18&style=1";
                //string UrlBetSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/confirm") + "&sameticket=0&betcount=" + betcount + "&stake=" + usdbet.ToString() + "&ostyle=1&stakeInAuto=10&betpage=18&acceptIfAny=0&autoProcess=0&autoRefresh=1&oid=" + keoid + "&timeDiff=2100";
                string UrlBetSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/confirm") + "&sameticket=0&betcount=" + betcount + "&stake=" + usdbet.ToString() + "&oid=" + keoid + "&timeDiff=2100";
                string data1 = http.Fetch(UrlTicketSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip, "ContentLength=0");
                string oddbet = Util.GetSubstringByString(data1.Replace("'", "").Replace("Over", "h").Replace("Under", "a").Replace("Tài", "h").Replace("Xỉu", "a"), "," + choose + ",", ",");
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, double.Parse(oddbet));
                if (deviationOdd < 0 && AutoAccept == false)
                {
                    message = "Odd Down:" + deviationOdd;
                    return;
                }
                string infobet = (bettype + choose).Replace("1h", o.home + "(FT) ").Replace("1a", o.away + "(FT) ").Replace("7h", o.home + "(H1) ").Replace("7a", o.away + "(H1) ").Replace("3h", o.home + "(OFT) ").Replace("3a", o.home + "(UFT) ").Replace("9h", o.home + "(OH1) ").Replace("9a", o.home + "(UH1) ") + hdp + " " + oddbet + " [" + usdbet + "]";
                string ProcessBet = http.Fetch(UrlBetSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
                if (ProcessBet.IndexOf("onOrderSubmitted") == -1)
                {
                    db.doInsertTicket("SBOBET", username, o.bettype, o.hdp.ToString(), "", "", choose, oddbet, money, usd, group, phieuchung);
                    message = "BET FAILED" + infobet;
                }
                else
                {
                    db.doInsertTicket("SBOBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddbet, money, usd, group, phieuchung);
                    message = "(OK)" + infobet;
                    betcount++;
                }

                if (deviationOdd < 0)
                {
                    message += "Odd Down:" + deviationOdd;
                }
                if(deviationOdd>0)
                {
                    message += "Odd Up:" + deviationOdd;
                }
            }
        }
        public void playBetLive(ticket o, string money, string IsLive, string phieuchung, string group, bool AutoAccept = true)
        {
            o = GetKeoSbo(lstSboLiveBet, o);
            if (o != null)
            {
                int usdbet = int.Parse(money) / int.Parse(usd);
                double hdp = o.hdp;
                string keoid = o.keoid;
                double odd = o.odd;
                string bettype = o.bettype;
                string choose = o.choose;
                string hdpType = "";
                if (bettype == "7" || bettype == "9")
                {
                    hdpType = "2";
                }

                string UrlTicketSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/ticket") + "&id=" + keoid + "&op=" + choose + "&odds=" + odd + "&hdpType=" + hdpType + "&isor=0&isLive=" + IsLive + "&betpage=18&style=1";
                string UrlBetSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/confirm") + "&sameticket=0&betcount=" + betcount + "&stake=" + usdbet.ToString() + "&ostyle=1&stakeInAuto=10&betpage=18&acceptIfAny=0&autoProcess=0&autoRefresh=1&oid=" + keoid + "&timeDiff=2100";

                string data1 = http.Fetch(UrlTicketSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip, "ContentLength=0");
                string oddbet = Util.GetSubstringByString(data1.Replace("'", "").Replace("Over", "h").Replace("Under", "a").Replace("Tài", "h").Replace("Xỉu", "a"), "," + choose + ",", ",");
                string infobet = (bettype + choose).Replace("1h", o.home.Substring(0, 5) + "(FT) ").Replace("1a", o.away + "(FT) ").Replace("7h", o.home + "(H1) ").Replace("7a", o.away + "(H1) ").Replace("3h", o.home + "(OFT) ").Replace("3a", o.home + "(UFT) ").Replace("9h", o.home + "(OH1) ").Replace("9a", o.home + "(UH1) ") + hdp + " " + oddbet + " [" + usdbet + "]";
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, double.Parse(oddbet));
                if (deviationOdd < 0 && AutoAccept == false)
                {
                    message = "Odd Down:" + deviationOdd;
                    return;
                }
                string ProcessBet = http.Fetch(UrlBetSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
                if (ProcessBet.IndexOf("onOrderSubmitted") == -1)
                {
                    db.doInsertTicket("SBOBET", username, o.bettype, o.hdp.ToString(), "", "", choose, oddbet, money, usd, group, phieuchung);
                    message = "BET FAILED" + infobet;
                    db.doInsertTicket("SBO", "BET FAIL", o.bettype, o.hdp.ToString(), o.home, o.away, choose, odd.ToString(), money, usd, group, this.phieuchung);
                }
                else
                {
                    db.doInsertTicket("SBOBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddbet, money, usd, group, phieuchung);
                    message = "(OK)" + infobet;
                    betcount++;
                }

                if (deviationOdd < 0)
                {
                    message += "Odd Down:" + deviationOdd;
                }
                if (deviationOdd > 0)
                {
                    message += "Odd Up:" + deviationOdd;
                }
            }
        }
        private void UpdateStringNonLive()
        {
            lstSboNonLiveBet.Clear();
            string DataOddSboNonLive = http.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
            string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
            leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");
            string str_LeaugeSbo = "", str_TeamSbo = "";
            if (str_LeaugeSbo == "")
            {
                hsLeague.Clear();
                foreach (string league in leagueDataNonLive.Split('\n'))
                {
                    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                    string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]).ToUpper();
                    if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 ||
                        nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 ||
                        nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1)
                        continue;
                    hsLeague.Add(leagueTemp.Split(',')[0], nameleagueTemp);
                    if (str_LeaugeSbo.IndexOf(nameleagueTemp) == -1)
                    {
                        str_LeaugeSbo += nameleagueTemp + ",";
                    }
                }
            }

            string matchDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "]],[[", "]],[[") + "]";
            matchDataNonLive = matchDataNonLive.Replace("],[", "]\n[");
            hsMatch.Clear();
            foreach (string matchNonLive in matchDataNonLive.Split('\n'))
            {
                string matchTempNonLive = matchNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                string[] arr_matchTempNonLive = matchTempNonLive.Split(',');
                string idmatchNonLive = Util.GetSubstringByStringLast(DataOddSboNonLive, "[", "," + arr_matchTempNonLive[0]);
                try
                {
                    hsMatch.Add(idmatchNonLive, hsLeague[arr_matchTempNonLive[2]].ToString() + "," + arr_matchTempNonLive[3] + "," + arr_matchTempNonLive[4] + "," + arr_matchTempNonLive[7]);
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
            foreach (string OddTempNonLive in oddDataNonLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
            {
                objMatch o = new objMatch();
                string OddTemp = OddTempNonLive.Replace("[", "").Replace("]", "").Replace("'", "");
                string[] arr_OddTemp = OddTemp.Split(',');
                string infomatch = "";
                try
                {
                    infomatch = hsMatch[arr_OddTemp[1]].ToString();
                }
                catch
                {
                    continue;
                }
                o.LeaugeName = UtilSoccer.ChuanTenLeauge_Sbo(infomatch.Split(',')[0]);
                o.HomeName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[1]);
                o.AwayName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[2]);
                o.TimeNonLive = infomatch.Split(',')[3];
                if (o.TimeNonLive.Split('/').Length == 1)
                {
                    o.TimeNonLive = o.TimeLive;
                }

                o.IdKeo = arr_OddTemp[0];
                o.hdp = UtilSoccer.formatHdp(arr_OddTemp[5]);
                o.BetType = arr_OddTemp[2];
                o.Odd1 = arr_OddTemp[6];
                o.Odd2 = arr_OddTemp[7];
                lstSboNonLiveBet.Add(o);
            }
        }
        private void UpdateStringLive()
        {
            lstSboLiveBet.Clear();
            string DataOddSboLive = http.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
            DataOddSboLive = DataOddSboLive.Replace("\\u200C", "");
            string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "[[[", "]],[[") + "]";
            leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
            hsLeague.Clear();
            foreach (string league in leagueDataLive.Split('\n'))
            {
                string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 ||
                nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 ||
                nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1)
                    continue;
                hsLeague.Add(leagueTemp.Split(',')[0], nameleagueTemp);
            }

            string matchDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "]],[[", "]],[[") + "]";
            matchDataLive = matchDataLive.Replace("],[", "]\n[");
            hsMatch.Clear();
            foreach (string matchLive in matchDataLive.Split('\n'))
            {
                string matchTempLive = matchLive.Replace("[", "").Replace("]", "").Replace("'", "");
                string[] arr_matchTempLive = matchTempLive.Split(',');
                string idmatchLive = Util.GetSubstringByStringLast(DataOddSboLive, "[", "," + arr_matchTempLive[0]);
                try
                {
                    hsMatch.Add(idmatchLive, hsLeague[arr_matchTempLive[2]].ToString() + "," + arr_matchTempLive[3] + "," + arr_matchTempLive[4] + "," + arr_matchTempLive[7]);
                }
                catch
                {
                    continue;
                }
            }
            string oddDataLive = "[[" + Util.GetSubstringByString(DataOddSboLive, ",,[[", "]]],,") + "]]]";

            foreach (string OddTempLive in oddDataLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
            {
                objMatch OddLive = new objMatch();
                string OddTemp = OddTempLive.Replace("[", "").Replace("]", "").Replace("'", "");
                string[] arr_OddTemp = OddTemp.Split(',');
                string infomatch = "";
                try
                {
                    infomatch = hsMatch[arr_OddTemp[1]].ToString();
                }
                catch
                {
                    continue;
                }
                OddLive.LeaugeName = UtilSoccer.ChuanTenLeauge_Sbo(infomatch.Split(',')[0]);
                OddLive.HomeName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[1]);
                OddLive.AwayName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[2]);
                OddLive.TimeLive = infomatch.Split(',')[3];

                OddLive.IdKeo = arr_OddTemp[0];
                OddLive.hdp = UtilSoccer.formatHdp(arr_OddTemp[5]);
                OddLive.BetType = arr_OddTemp[2];
                OddLive.Odd1 = arr_OddTemp[6];
                OddLive.Odd2 = arr_OddTemp[7];

                lstSboLiveBet.Add(OddLive);
            }
        }
        private ticket GetKeoSbo(List<objMatch> Odd_SboList, ticket o)
        {
            try
            {
                foreach (objMatch Odd_Sbo in Odd_SboList)
                {
                    if (Odd_Sbo.HomeName == o.home && Odd_Sbo.AwayName == o.away && double.Parse(Odd_Sbo.hdp) == o.hdp && Odd_Sbo.BetType == o.bettype)
                    {
                        o.keoid = Odd_Sbo.IdKeo;
                        return o;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public string getOddChange(ticket o, string money, string IsLive)
        {
            if (IsLive == "1")
            {
                UpdateStringLive();
                o = GetKeoSbo(lstSboLiveBet, o);
            }
            else
            {
                UpdateStringNonLive();
                o = GetKeoSbo(lstSboNonLiveBet, o);
            }
            if (o != null)
            {
                double hdp = o.hdp;
                string keoid = o.keoid;
                double odd = o.odd;
                string bettype = o.bettype;
                string choose = o.choose;
                string hdpType = "";
                if (bettype == "7" || bettype == "9")
                {
                    hdpType = "2";
                }
                string UrlTicketSbo = "http://" + Util.GetSubstringByString(welcomeLink, "http://", "&").Replace("default", "ticket/ticket") + "&id=" + keoid + "&op=" + choose + "&odds=" + odd + "&hdpType=" + hdpType + "&isor=0&isLive=" + IsLive + "&betpage=18&style=1";
                //string data1 = http.Fetch(UrlTicketSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip, "", mainLink.Replace("http://", ""));
                string data1 = http.Fetch(UrlTicketSbo, HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
                string oddbet = Util.GetSubstringByString(data1.Replace("'", "").Replace("Over", "h").Replace("Under", "a").Replace("Tài", "h").Replace("Xỉu", "a"), "," + choose + ",", ",");
                return oddbet;
            }
            return "e";
        }
    }
}
