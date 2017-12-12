using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace COMPARE
{
    public class IbetFunction : IFunction
    {
        string link = "http://www.bong88.com";
        private string message = "", TotalMatch = "", Matchs = "";
        string mainHost, mainLink;
        string sportLink;
        string key, ip, username, password;
        HttpHelper http;
        Database db;
        List<objMatch> lst = new List<objMatch>();
        List<objMatch> lstLive = new List<objMatch>();

        public IbetFunction(string key, string ip, string username, string password)
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
            string loginurl = http.FetchResponseUri(link, HttpHelper.HttpMethod.Get, null, null, ip);
            string data = http.Fetch(loginurl, HttpHelper.HttpMethod.Get, null, null, ip);
            if(data.Contains("Under Maintenance"))
            {
                message = "Under Maintenance";
                return;
            }
            string code = Util.HtmlGetAttributeValue(data, "value", "//input[@id='txtCode']");
            string tk = Util.HtmlGetAttributeValue(data, "value", "//input[@id='__tk']");
            data = http.Fetch(link + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, link, "selLang=en&txtID=" + username + "&txtPW=" + mylib.MD5(mylib.CFS(password) + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=", ip);
            if (data.Contains("Login too often"))
            {
                message = "[" + username + "] Login too often, please wait 5 minutes";
                Thread.Sleep(300 * 1000);
            }
            else
            {
                if (!data.Contains("window.location='."))
                {
                    string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='", "';</script>");
                    mainLink = http.FetchResponseUri(VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    mainHost = mainLink.Replace("/main.aspx", "");
                    data = http.Fetch(mainLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip, "", mainHost.Replace("http://", "").Replace("/", ""));
                    if (data != "")
                    {
                        message = "[" + username + "] Login successfully";
                        http.Fetch(mainHost + "/topmenu.aspx", HttpHelper.HttpMethod.Get, mainLink, null);

                        string checklogin = "check";
                        Thread tCheckLogin = new Thread(delegate ()
                        {
                            while (checklogin != "")
                            {
                                Thread.Sleep(60000);
                                checklogin = http.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + username.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                            }
                        });
                        tCheckLogin.Start();
                    }
                    else
                    {
                        message = "LOGIN FAILED [" + username + "] Data Empty";
                    }
                }
                else
                {
                    string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='.", "';</script>");
                    sportLink = http.FetchResponseUri(link + VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    if (sportLink.Contains("ChangePasswordPage"))
                    {
                        string passDefault = "Vvvv6868@";
                        string passNew = "Vvvv1111";
                        string postChangePassData = "OldPassword=" + mylib.CFS(passDefault) + "&OldLowerCasePassword=" + mylib.CFS(passDefault.ToLower()) + "&NewPassword=" + mylib.CFS(passNew) + "&ReTypePassword=" + mylib.CFS(passNew);
                        mainHost = sportLink.Replace("/ChangeAccountPassword/ChangePasswordPage", "");
                        data = http.Fetch(mainHost + "/ChangeAccountPassword/ChangePasswordPage", HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                        data = http.Fetch(mainHost + "/_Incapsula_Resource?SWKMTFSR=1&e=0.021669474353265494", HttpHelper.HttpMethod.Get, mainHost + "/ChangeAccountPassword/ChangePasswordPage", null, ip);
                        data = http.Fetch(mainHost + "/Preferences/ChangePassWord", HttpHelper.HttpMethod.Post, mainHost + "/ChangeAccountPassword/ChangePasswordPage", postChangePassData, ip);
                        data = http.Fetch(mainHost + "/", HttpHelper.HttpMethod.Get, mainHost + "/ChangeAccountPassword/ChangePasswordPage", null, ip);
                        sportLink = mainHost + "/";
                        data = http.Fetch(mainHost + "/Preferences/AccountAndStatememt", HttpHelper.HttpMethod.Get, sportLink, null, ip);
                        data = http.Fetch(mainHost + "/Preferences/ChangePassWord", HttpHelper.HttpMethod.Post, sportLink, "OldPassword=" + mylib.CFS(passNew) + "&OldLowerCasePassword=" + mylib.CFS(passNew.ToLower()) + "&NewPassword=" + mylib.CFS(passDefault) + "&ReTypePassword=" + mylib.CFS(passDefault), ip);
                        data = http.Fetch(mainHost + "/Preferences/AccountAndStatememt", HttpHelper.HttpMethod.Get, mainHost + "/Preferences/AccountAndStatememt", null, ip);
                        if (data != "")
                        {
                            message = "IBET: [" + username + "] Need to change password";
                            mainLink = http.FetchResponseUri(mainHost + "/SwitchPlatform/SwitchToOtherSite/", HttpHelper.HttpMethod.Get, sportLink, null);
                            mainHost = mainLink.Replace("/main.aspx", "");
                            http.Fetch(mainLink, HttpHelper.HttpMethod.Get, sportLink, null);
                            http.Fetch(mainHost + "/topmenu.aspx", HttpHelper.HttpMethod.Get, mainLink, null);

                            string checkLogin = "check";
                            Thread tCheckLogin = new Thread(delegate ()
                            {
                                while (checkLogin != "")
                                {
                                    Thread.Sleep(60000);
                                    checkLogin = http.Fetch(mainHost + "/LoginCheckin/Index", HttpHelper.HttpMethod.Post, sportLink, null, ip, mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                                }
                            });
                            tCheckLogin.Start();
                        }
                        else
                        {
                            message = "IBET: LOGIN FAILED [" + username + "] Data Empty";
                        }
                    }
                    else {
                        mainHost = sportLink.Replace("/sports", "");
                        data = http.Fetch(sportLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                        if (data != "")
                        {
                            message = "[" + username + "] Login successfully";
                            mainLink = http.FetchResponseUri(mainHost + "/SwitchPlatform/SwitchToOtherSite/", HttpHelper.HttpMethod.Get, sportLink, null);
                            mainHost = mainLink.Replace("/main.aspx", "");
                            http.Fetch(mainLink, HttpHelper.HttpMethod.Get, sportLink, null);
                            http.Fetch(mainHost + "/topmenu.aspx", HttpHelper.HttpMethod.Get, mainLink, null);

                            string checklogin = "check";
                            Thread tCheckLogin = new Thread(delegate ()
                            {
                                while (checklogin != "")
                                {
                                    Thread.Sleep(60000);
                                    checklogin = http.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + username.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                                }
                            });
                            tCheckLogin.Start();
                        }
                        else
                        {
                            message = "LOGIN FAILED [" + username + "] Data Empty";
                        }
                    }
                }
            }
        }
        public List<objMatch> getMatchOddNonLive(DateTime TimeLimit)
        {
            objMatch o;
            string data = "";
            try
            {
                lst.Clear();
                data = http.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);
                if (data == "")
                {
                    o = new objMatch();
                    lst.Clear();
                    o.Score = "Logout";
                    lst.Add(o);
                    login();
                    return lst;
                }
                int ik = data.IndexOf("name=\"k");
                int iv = data.IndexOf("value=\"v");
                string kIbet = data.Substring(ik + 6, iv - ik - 8);
                string vIbet = data.Substring(iv + 7, iv - ik - 8);
                string CT = Util.EscapeDataString(DateTime.Now.AddHours(-11).ToString());
                data = http.Fetch(mainHost + "/UnderOver_data.aspx?Market=t&Sport=1&DT=&RT=W&CT=" + CT + "&Game=0&OrderBy=0&OddsType=4&MainLeague=0&DispRang=0&" + kIbet + "=" + vIbet + "&key=dodds&_=1502509515441", HttpHelper.HttpMethod.Get, mainHost + "/UnderOver.aspx?Market=t&DispVer=new", null);
                string LeaugeName = "", HomeName = "", AwayName = "", Time = "";
                if (data == "")
                {
                    o = new objMatch();
                    lst.Clear();
                    o.Score = "Logout";
                    lst.Add(o);
                    login();
                    return lst;
                }
                else
                {
                    foreach (Match m in Regex.Matches(data, @"Nt\[\d+\]=\[.*\];"))
                    {
                        string strIbetMatch = m.Value.ToString().Replace("];", "");
                        strIbetMatch = Regex.Replace(strIbetMatch, @"Nt\[\d+\]=\[", "");
                        strIbetMatch = strIbetMatch.Replace("'", "");

                        string[] dataIbetMatch = strIbetMatch.Split(',');
                        if (dataIbetMatch[5] != "")
                        {
                            LeaugeName = UtilSoccer.ChuanTenLeauge_Ibet(dataIbetMatch[5]);
                        }
                        if (LeaugeName.Contains("CORNERS") || LeaugeName.Contains("BOOKING") || LeaugeName.Contains("TEAM") || LeaugeName.Contains("WINNER")
                            || LeaugeName.Contains("MATCH") || LeaugeName.Contains("SPECIFIC") || LeaugeName.Contains("SUBSTITUTION") ||
                            LeaugeName.Contains("GOAL KICK") || LeaugeName.Contains("OFFSIDE") || LeaugeName.Contains("THROW IN") ||
                            LeaugeName.Contains("FREE KICK") || LeaugeName.Contains("1st HALF vs 2nd HALF") || LeaugeName.Contains("RED CARD")
                            || LeaugeName.Contains("OWN GOAL") || LeaugeName.Contains("PENALTY") || LeaugeName.Contains("TOTAL GOALS MINUTES")
                            || LeaugeName.Contains("Injury"))
                            continue;

                        if (dataIbetMatch[6] != "")
                        {
                            HomeName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[6]);
                            AwayName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[7]);
                        }
                        if (dataIbetMatch[12] != "")
                        {
                            Time = dataIbetMatch[12].Split(' ')[dataIbetMatch[12].Split(' ').Length - 1];
                            Time = UtilSoccer.convertSoccerTime(Time);
                            if (DateTime.Parse(Time) > TimeLimit.AddHours(1)) continue;
                        }
                        ///FT///
                        try
                        {
                            o = new objMatch();
                            o.Score = "0-0";
                            o.TimeNonLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            if (Matchs.Contains(o.HomeName + "-" + o.AwayName) == false)
                            {
                                Matchs += o.HomeName + "-" + o.AwayName + ",";
                            }
                            o.IdKeo = dataIbetMatch[31];
                            o.BetType = "1";
                            o.hdp = dataIbetMatch[32];
                            o.Odd1 = dataIbetMatch[33];
                            o.Odd2 = dataIbetMatch[34];
                            if (dataIbetMatch[35] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lst.Add(o);
                        }
                        catch
                        { }



                        ///OUFT///
                        try
                        {
                            o = new objMatch();
                            o.Score = "0-0";
                            o.TimeNonLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[36];
                            o.BetType = "3";
                            o.hdp = dataIbetMatch[37];
                            o.Odd1 = dataIbetMatch[38];
                            o.Odd2 = dataIbetMatch[39];
                            if (dataIbetMatch[35] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lst.Add(o);
                        }
                        catch
                        { }

                        ///HT///
                        try
                        {
                            o = new objMatch();
                            o.Score = "0-0";
                            o.TimeNonLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[44];
                            o.BetType = "7";
                            o.hdp = dataIbetMatch[45];
                            o.Odd1 = dataIbetMatch[46];
                            o.Odd2 = dataIbetMatch[47];
                            if (dataIbetMatch[48] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lst.Add(o);
                        }
                        catch { }
                        ///HTOU///
                        try
                        {
                            o = new objMatch();
                            o.Score = "0-0";
                            o.TimeNonLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[49];
                            o.BetType = "9";
                            o.hdp = dataIbetMatch[50];
                            o.Odd1 = dataIbetMatch[51];
                            o.Odd2 = dataIbetMatch[52];
                            if (dataIbetMatch[48] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lst.Add(o);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception)
            {
                o = new objMatch();
                lst.Clear();
                o.Score = "Error";
                o.BetType = data;
                lst.Add(o);
                return lst;
            }
            TotalMatch = "IBET MATCH: " + (Matchs.Split(',').Length - 1).ToString();
            return lst;
        }
        public List<objMatch> getMatchOddLive()
        {
            objMatch o;
            string data = "";
            try
            {
                lstLive.Clear();
                data = http.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);
                if (data == "")
                {
                    o = new objMatch();
                    lstLive.Clear();
                    o.Score = "Logout";
                    lstLive.Add(o);
                    login();
                    return lstLive;
                }
                int ik = data.IndexOf("name=\"k");
                int iv = data.IndexOf("value=\"v");
                string kIbet = data.Substring(ik + 6, iv - ik - 8);
                string vIbet = data.Substring(iv + 7, iv - ik - 8);
                string CT = Util.EscapeDataString(DateTime.Now.AddHours(-11).ToString());
                data = http.Fetch(mainHost + "/UnderOver_data.aspx?Market=l&Sport=1&DT=&RT=W&CT=" + CT + "&Game=0&OrderBy=0&OddsType=4&MainLeague=0&DispRang=0&" + kIbet + "=" + vIbet + "&key=dodds&_=1502509515441", HttpHelper.HttpMethod.Get, mainHost + "/UnderOver.aspx?Market=t&DispVer=new", null);
                string LeaugeName = "", HomeName = "", AwayName = "", Time = "", Score = "";
                if (data == "")
                {
                    o = new objMatch();
                    lstLive.Clear();
                    o.Score = "Logout";
                    lstLive.Add(o);
                    login();
                    return lstLive;
                }
                else
                {
                    foreach (Match m in Regex.Matches(data, @"Nl\[\d+\]=\[.*\];"))
                    {
                        string strIbetMatch = m.Value.ToString().Replace("];", "");
                        strIbetMatch = Regex.Replace(strIbetMatch, @"Nl\[\d+\]=\[", "");
                        strIbetMatch = strIbetMatch.Replace("'", "");

                        string[] dataIbetMatch = strIbetMatch.Split(',');
                        if (dataIbetMatch[5] != "")
                        {
                            LeaugeName = UtilSoccer.ChuanTenLeauge_Ibet(dataIbetMatch[5]);
                        }
                        if (LeaugeName.Contains("CORNERS") || LeaugeName.Contains("BOOKING") || LeaugeName.Contains("TEAM") || LeaugeName.Contains("WINNER")
                            || LeaugeName.Contains("MATCH") || LeaugeName.Contains("SPECIFIC") || LeaugeName.Contains("SUBSTITUTION") ||
                            LeaugeName.Contains("GOAL KICK") || LeaugeName.Contains("OFFSIDE") || LeaugeName.Contains("THROW IN") ||
                            LeaugeName.Contains("FREE KICK") || LeaugeName.Contains("1st HALF vs 2nd HALF") || LeaugeName.Contains("RED CARD")
                            || LeaugeName.Contains("OWN GOAL") || LeaugeName.Contains("PENALTY") || LeaugeName.Contains("TOTAL GOALS MINUTES")
                            || LeaugeName.Contains("Injury"))
                            continue;

                        if (dataIbetMatch[6] != "")
                        {
                            HomeName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[6]);
                            AwayName = UtilSoccer.ChuanTenTeam_Ibet(dataIbetMatch[7]);
                        }
                        if (dataIbetMatch[12] != "")
                        {
                            Time = dataIbetMatch[12].Replace("\\","");                            
                        }
                        if (dataIbetMatch[21] != "" && dataIbetMatch[22] != "")
                        {
                            Score = dataIbetMatch[21] + "-" + dataIbetMatch[22];
                        }
                        ///FT///
                        try
                        {
                            o = new objMatch();
                            o.Score = Score;
                            o.TimeLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            if (Matchs.Contains(o.HomeName + "-" + o.AwayName) == false)
                            {
                                Matchs += o.HomeName + "-" + o.AwayName + ",";
                            }
                            o.IdKeo = dataIbetMatch[31];
                            o.BetType = "1";
                            o.hdp = dataIbetMatch[32];
                            o.Odd1 = dataIbetMatch[33];
                            o.Odd2 = dataIbetMatch[34];
                            if (dataIbetMatch[35] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lstLive.Add(o);
                        }
                        catch
                        { }



                        ///OUFT///
                        try
                        {
                            o = new objMatch();
                            o.Score = Score;
                            o.TimeLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[36];
                            o.BetType = "3";
                            o.hdp = dataIbetMatch[37];
                            o.Odd1 = dataIbetMatch[38];
                            o.Odd2 = dataIbetMatch[39];
                            if (dataIbetMatch[35] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lstLive.Add(o);
                        }
                        catch
                        { }

                        ///HT///
                        try
                        {
                            o = new objMatch();
                            o.Score = Score;
                            o.TimeLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[44];
                            o.BetType = "7";
                            o.hdp = dataIbetMatch[45];
                            o.Odd1 = dataIbetMatch[46];
                            o.Odd2 = dataIbetMatch[47];
                            if (dataIbetMatch[48] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lstLive.Add(o);
                        }
                        catch { }
                        ///HTOU///
                        try
                        {
                            o = new objMatch();
                            o.Score = Score;
                            o.TimeLive = Time;
                            o.LeaugeName = LeaugeName;
                            o.HomeName = HomeName;
                            o.AwayName = AwayName;
                            o.IdKeo = dataIbetMatch[49];
                            o.BetType = "9";
                            o.hdp = dataIbetMatch[50];
                            o.Odd1 = dataIbetMatch[51];
                            o.Odd2 = dataIbetMatch[52];
                            if (dataIbetMatch[48] == "a") o.hdp = "-" + o.hdp;
                            o.hdp = UtilSoccer.formatkeo(o.hdp);
                            lstLive.Add(o);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception)
            {
                o = new objMatch();
                lstLive.Clear();
                o.Score = "Error";
                o.BetType = data;
                lstLive.Add(o);
                return lstLive;
            }
            TotalMatch = "IBET MATCH: " + (Matchs.Split(',').Length - 1).ToString();
            return lstLive;
        }
    }
}
