using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COMPARE
{
    public class SboFunction : IFunction
    {
        string key, ip, username, password;
        string link = "http://img.eaxybox.com";
        private string message = "", TotalMatch = "", Matchs = "";
        string mainLink = "";
        string welcomeLink = "";
        string defaultLink = "";
        HttpHelper http;
        Database db;
        Hashtable hsLeagueNonLive = new Hashtable();
        Hashtable hsMatchNonLive = new Hashtable();
        Hashtable hsLeagueLive = new Hashtable();
        Hashtable hsMatchLive = new Hashtable();
        List<objMatch> lst = new List<objMatch>();
        List<objMatch> lstLive = new List<objMatch>();

        public SboFunction(string key, string ip, string username, string password)
        {
            this.key = key;
            this.ip = ip;
            this.username = username;
            this.password = password;        
            http = new HttpHelper();
            db = new Database();
        }
        public string getMessage()
        {
            if (message == null)
                return "null";
            return message;
        }
        public string getTotalMatch()
        {
            return TotalMatch;
        }
        public string getMatchs()
        {
            return Matchs;
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
                message = "SBO: [" + username + "] Wrong User And Pass";
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
                    message = "SBO: [" + username + "] Login successfully";
                    Thread tCheckLogin = new Thread(delegate () {
                        while (true)
                        {
                            Thread.Sleep(60000);
                            string credit = http.Fetch(mainLink + "/web-root/restricted/top-module/action-data.aspx?action=bet-credit", HttpHelper.HttpMethod.Get, welcomeLink, null, ip);
                            try
                            {
                                double.Parse(credit.Split('\'')[3].Replace(",", ""));
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                    });
                    tCheckLogin.Start();
                }
                else
                {
                    message = "SBO: [" + username + "] Login failed";
                }
            }
        }
        public List<objMatch> getMatchOddNonLive(DateTime TimeLimit)
        {
            try
            {
                lst.Clear();
                string DataOddSboNonLive = http.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=1&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                string leagueDataNonLive = "[" + Util.GetSubstringByString(DataOddSboNonLive, "[[[", "]],[[") + "]";
                leagueDataNonLive = leagueDataNonLive.Replace("],[", "]\n[");
                string str_LeaugeSbo = "", str_TeamSbo = "";
                if (str_LeaugeSbo == "")
                {
                    hsLeagueNonLive.Clear();
                    foreach (string league in leagueDataNonLive.Split('\n'))
                    {
                        string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                        string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]).ToUpper();
                        if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 || 
                            nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 || 
                            nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1)
                            continue;
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
                foreach (string OddTempNonLive in oddDataNonLive.Split(new string[] { "]],[" }, StringSplitOptions.None))
                {
                    objMatch o = new objMatch();
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
                    o.LeaugeName = UtilSoccer.ChuanTenLeauge_Sbo(infomatch.Split(',')[0]);
                    o.HomeName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[1]);
                    o.AwayName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[2]);
                    if (Matchs.Contains(o.HomeName + "-" + o.AwayName) == false)
                    {
                        Matchs += o.HomeName + "-" + o.AwayName + ",";
                    }
                    o.TimeNonLive = infomatch.Split(',')[3];
                    if (DateTime.Parse(o.TimeNonLive) > TimeLimit.AddHours(1)) continue;
                    if (o.TimeNonLive.Split('/').Length == 1)
                    {
                        o.TimeNonLive = o.TimeLive;
                    }

                    o.IdKeo = arr_OddTemp[0];
                    o.hdp = UtilSoccer.formatkeo(arr_OddTemp[5]);
                    o.BetType = arr_OddTemp[2];
                    o.Odd1 = arr_OddTemp[6];
                    o.Odd2 = arr_OddTemp[7];

                    lst.Add(o);
                }
            }
            catch (Exception)
            {
                lst.Clear();
                return lst;
            }
            TotalMatch = "SBO MATCH: " + (Matchs.Split(',').Length - 1).ToString();
            return lst;
        }
        public List<objMatch> getMatchOddLive()
        {
            try
            {
                lstLive.Clear();
                string DataOddSboLive = http.Fetch(mainLink + "/web-root/restricted/odds-display/today-data.aspx?od-param=2,1,1,1,2,2,2,2,3,1&fi=0&v=0&dl=3", HttpHelper.HttpMethod.Get, null, null);
                DataOddSboLive = DataOddSboLive.Replace("\\u200C", "");
                string leagueDataLive = "[" + Util.GetSubstringByString(DataOddSboLive, "[[[", "]],[[") + "]";
                leagueDataLive = leagueDataLive.Replace("],[", "]\n[");
                hsLeagueLive.Clear();
                foreach (string league in leagueDataLive.Split('\n'))
                {
                    string leagueTemp = league.Replace("[", "").Replace("]", "").Replace("'", "");
                    string nameleagueTemp = UtilSoccer.ChuanTenLeauge_Sbo(leagueTemp.Split(',')[1]);
                    if (nameleagueTemp.IndexOf("SPECIFIC") != -1 || nameleagueTemp.IndexOf("CORNERS") != -1 || nameleagueTemp.IndexOf("BOOKING") != -1 ||
                    nameleagueTemp.IndexOf("FANTASY MATCH") != -1 || nameleagueTemp.IndexOf("WHICH TEAM") != -1 || nameleagueTemp.IndexOf("TOTAL GOALS") != -1 ||
                    nameleagueTemp.IndexOf("INJURY") != -1 || nameleagueTemp.IndexOf("WINNER") != -1)
                        continue;
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
                    OddLive.LeaugeName = UtilSoccer.ChuanTenLeauge_Sbo(infomatch.Split(',')[0]);
                    OddLive.HomeName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[1]);
                    OddLive.AwayName = UtilSoccer.ChuanTenTeam_Sbo(infomatch.Split(',')[2]);
                    OddLive.TimeLive = infomatch.Split(',')[3];

                    OddLive.IdKeo = arr_OddTemp[0];
                    OddLive.hdp = UtilSoccer.formatkeo(arr_OddTemp[5]);
                    OddLive.BetType = arr_OddTemp[2];
                    OddLive.Odd1 = arr_OddTemp[6];
                    OddLive.Odd2 = arr_OddTemp[7];

                    lstLive.Add(OddLive);
                }
            }
            catch
            {
                lstLive.Clear();
                return lstLive;
            }
            return lstLive;
        }
    }
}
