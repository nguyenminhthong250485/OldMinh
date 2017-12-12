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
        private string message = "";
        string mainHost, mainLink;
        string sportLink;
        string key, ip, username, password;
        HttpHelper http;
        Database db;
        List<objMatch> lst = new List<objMatch>();

        public IbetFunction(string key, string ip, string username, string password)
        {
            this.key = key;
            this.ip = ip;
            this.username = username;
            this.password = password;
            http = new HttpHelper();
            db = new Database();
        }

        public List<objMatch> getMatchOddNonLive()
        {
            try
            {
                lst.Clear();
                string data = http.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);

                int ik = data.IndexOf("name=\"k");
                int iv = data.IndexOf("value=\"v");
                string kIbet = data.Substring(ik + 6, iv - ik - 8);
                string vIbet = data.Substring(iv + 7, iv - ik - 8);
                string CT = Util.EscapeDataString(DateTime.Now.AddHours(-11).ToString());
                data = http.Fetch(mainHost + "/UnderOver_data.aspx?Market=t&Sport=1&DT=&RT=W&CT=" + CT + "&Game=0&OrderBy=0&OddsType=4&MainLeague=0&DispRang=0&" + kIbet + "=" + vIbet + "&key=dodds&_=1502509515441", HttpHelper.HttpMethod.Get, mainHost + "/UnderOver.aspx?Market=t&DispVer=new", null);
                string LeaugeName = "", HomeName = "", AwayName = "", Time = "";
                if (data == "")
                {
                    lst.Clear();
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
                            lst.Add(o);
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
                            lst.Add(o);
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
                            lst.Add(o);
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
                            lst.Add(o);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception)
            {
                lst.Clear();
                return lst;
            }
            return lst;
        }

        public string getMessage()
        {
            return message;
        }

        public void login()
        {
            string loginurl = http.FetchResponseUri(link, HttpHelper.HttpMethod.Get, null, null, ip);
            string data = http.Fetch(loginurl, HttpHelper.HttpMethod.Get, null, null, ip);
            string code = Util.HtmlGetAttributeValue(data, "value", "//input[@id='txtCode']");
            string tk = Util.HtmlGetAttributeValue(data, "value", "//input[@id='__tk']");
            data = http.Fetch(link + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, link, "selLang=en&txtID=" + username + "&txtPW=" + mylib.MD5(mylib.CFS(password) + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=", ip);
            if (data.Contains("Login too often,please wait 5 minutes"))
            {
                message = "IBET: [" + username + "] Login too often, please wait 5 minutes";
            }
            else
            {
                string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='.", "';</script>");
                sportLink = http.FetchResponseUri(link + VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                mainHost = sportLink.Replace("/sports", "");
                data = http.Fetch(sportLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                if (data != "")
                {
                    message = "IBET: [" + username + "] Login successfully";
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
                    message = "IBET: LOGIN FAILED [" + username + "] Data Empty";
                }
            }
        }
    }
}
