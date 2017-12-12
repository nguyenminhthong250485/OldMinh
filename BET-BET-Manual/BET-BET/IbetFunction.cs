using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BET_BET
{
    public class IbetFunction : IFunction
    {
        string link = "http://www.bong88.com";
        private string message = "";
        private string phieuchung = "";
        private string realmoney = "";
        string mainHost;
        string sportLink;
        string key, ip, username, password, usd;
        HttpHelper http;
        Database db;

        public IbetFunction(string key, string ip, string username, string password, string usd)
        {
            this.key = key;
            this.ip = ip;
            this.username = username;
            this.password = password;
            this.usd = usd;
            http = new HttpHelper();
            db = new Database();
        }
        public string getBetList()
        {
            string betlist = http.Fetch(mainHost + "/Statement/BetList", HttpHelper.HttpMethod.Get, sportLink, null, ip);
            betlist = betlist.Replace("/template", mainHost + "/template").Replace("/bundles", mainHost + "/bundles").Replace("/Scripts", mainHost + "/Scripts").Replace("http://","https://");                                                            
            return betlist;
        }
        public string getCredit()
        {
            string credit = "";
            try
            {
                credit = http.Fetch(mainHost + "/Customer/Balance", HttpHelper.HttpMethod.Post, sportLink, null, ip, mainHost);
                credit = Util.GetSubstringByString(credit, "\"BCredit\":\"", ".");
                double.Parse(credit.Replace(",", ""));
                return credit;
            }
            catch (Exception)
            {
                return credit;
            }
        }
        public string getMessage()
        {
            return message;
        }
        public string getPhieuchung()
        {
            return phieuchung;
        }
        public string getRealMoney()
        {
            return realmoney;
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
                if (data.Contains("window.location='."))
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
                        data = http.Fetch(mainHost+ "/_Incapsula_Resource?SWKMTFSR=1&e=0.021669474353265494", HttpHelper.HttpMethod.Get, mainHost + "/ChangeAccountPassword/ChangePasswordPage", null, ip);
                        data = http.Fetch(mainHost + "/Preferences/ChangePassWord", HttpHelper.HttpMethod.Post, mainHost + "/ChangeAccountPassword/ChangePasswordPage", postChangePassData, ip);
                        data = http.Fetch(mainHost + "/", HttpHelper.HttpMethod.Get, mainHost + "/ChangeAccountPassword/ChangePasswordPage", null, ip);
                        sportLink = mainHost + "/";
                        data = http.Fetch(mainHost + "/Preferences/AccountAndStatememt", HttpHelper.HttpMethod.Get, sportLink, null, ip);
                        data = http.Fetch(mainHost + "/Preferences/ChangePassWord", HttpHelper.HttpMethod.Post, sportLink, "OldPassword=" + mylib.CFS(passNew) + "&OldLowerCasePassword=" + mylib.CFS(passNew.ToLower()) + "&NewPassword=" + mylib.CFS(passDefault) + "&ReTypePassword=" + mylib.CFS(passDefault), ip);
                        data = http.Fetch(mainHost + "/Preferences/AccountAndStatememt", HttpHelper.HttpMethod.Get, mainHost + "/Preferences/AccountAndStatememt", null, ip);
                        if (data != "")
                        {
                            message = "IBET: [" + username + "] Need to change password";
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
                    else
                    {
                        mainHost = sportLink.Replace("/sports", "");
                        data = http.Fetch(sportLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                        if (data != "")
                        {
                            message = "IBET: [" + username + "] Login successfully";
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
                }
                else
                {
                    string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='", "';</script>");
                    string mainLink = http.FetchResponseUri(VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    mainHost = mainLink.Replace("/main.aspx", "");
                    data = http.Fetch(mainLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip, "", mainHost.Replace("http://", "").Replace("/", ""));
                    if (data != "")
                    {
                        data = http.Fetch(mainHost + "/LeftAllInOne.aspx", HttpHelper.HttpMethod.Get, mainLink, null, ip);
                        data = http.Fetch(mainHost + "/GetLoginVerifyInfo.aspx", HttpHelper.HttpMethod.Get, mainHost + "/LeftAllInOne.aspx", null, ip);
                        LoginVerifyInfo o = JsonConvert.DeserializeObject<LoginVerifyInfo>(data);
                        sportLink = http.FetchResponseUri(o.Host + "/ValidateToken/Index?t=" + o.TicketID + "&c=" + o.CustID + "&l=" + o.Lan + "&f=" + o.CountryName + "&v=1&s=" + o.Ssl, HttpHelper.HttpMethod.Get, mainHost + "/LeftAllInOne.aspx", null, ip);
                        mainHost = o.Host;
                        data = http.Fetch(sportLink, HttpHelper.HttpMethod.Get, mainHost + "/LeftAllInOne.aspx", null, ip);
                        if (data != "")
                        {
                            message = "IBET: [" + username + "] Login successfully";
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
                }

            }
        }
        public void playBetNonLive(ticket o, string money, string IsLive, string phieuchung, string group)
        {
            bool doBet = true;
            string betGroup = group.Split(',')[1];
            this.phieuchung = phieuchung;
            int usdbet = int.Parse(money) / int.Parse(usd);
            double hdp = o.hdp;
            string keoid = o.keoid;
            double odd = o.odd;
            string bettype = o.bettype;
            string choose = o.choose;
            string homescore = "0";
            string awayscore = "0";

            string PostData = "ItemList[0][sportname]=Soccer&ItemList[0][Hscore]=" + homescore + "&ItemList[0][Ascore]=" + awayscore + "&ItemList[0][type]=OU&ItemList[0][bettype]=" + bettype + "&ItemList[0][oddsid]=" + keoid.Trim() + "&ItemList[0][betteam]=" + choose + "&ItemList[0][stake]=" + usdbet;
            string GetTicket = http.Fetch(mainHost + "/Betting/GetTickets", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
            if (GetTicket.Replace("\"", "").IndexOf("ErrorCode:0") == -1)
            {
                message = "IBET: [" + username + "] LOG OUT";
            }
            else
            {
                double oddBet = double.Parse(Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ","));
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, oddBet);
                if (betGroup == "d" && deviationOdd <= 2)
                {
                    doBet = true;
                }
                else if (betGroup == "c" && deviationOdd <= 1)
                {
                    doBet = true;
                }
                else if (UtilSoccer.CheckOddChange(odd, oddBet) == "Down")
                {
                    doBet = false;
                    message = "IBET: [" + username + "] Odd Down " + odd + "/" + oddBet;
                    Thread.Sleep(2000);
                }
                if (doBet)
                {
                    if (UtilSoccer.CheckOddChange(odd, oddBet) == "Up")
                    {
                        message = "IBET: [" + username + "] Odd Up " + odd + "/" + oddBet;
                    }
                    string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
                    string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
                    PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
                    if (double.Parse(MaxBet) < usdbet)
                    {
                        PostData = PostData.Replace("ItemList[0][stake]=" + usdbet, "ItemList[0][stake]=" + MaxBet);
                        money = (int.Parse(MaxBet.Replace(",", "")) * int.Parse(usd)).ToString();
                    }
                    string ProcessBet = http.Fetch(mainHost + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
                    if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0)
                    {
                        message = "IBET: [" + username + "] BET FAILED - INFO: " + o.ToString();
                    }
                    else
                    {
                        if (phieuchung == "")
                            this.phieuchung = mylib.generateID("T");
                        this.realmoney = money;
                        db.doInsertTicket("IBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddBet.ToString(), money, usd, group, this.phieuchung);
                        message = "IBET: [" + username + "] BET SUCCESS - INFO: " + o.ToString();
                    }
                }
            }
        }
        public void playBetLive(ticket o, string money, string IsLive, string phieuchung, string group)
        {
            bool doBet = true;
            string betGroup = group.Split(',')[1];
            this.phieuchung = phieuchung;
            int usdbet = int.Parse(money) / int.Parse(usd);
            double hdp = o.hdp;
            string keoid = o.keoid;
            double odd = o.odd;
            string bettype = o.bettype;
            string choose = o.choose;
            string homescore = o.score.Split('-')[0];
            string awayscore = o.score.Split('-')[1];

            string PostData = "ItemList[0][sportname]=Soccer&ItemList[0][Hscore]=" + homescore + "&ItemList[0][Ascore]=" + awayscore + "&ItemList[0][type]=OU&ItemList[0][bettype]=" + bettype + "&ItemList[0][oddsid]=" + keoid.Trim() + "&ItemList[0][betteam]=" + choose + "&ItemList[0][stake]=" + usdbet;
            string GetTicket = http.Fetch(mainHost + "/Betting/GetTickets", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
            if (GetTicket.Replace("\"", "").IndexOf("ErrorCode:0") == -1)
            {
                message = "IBET: [" + username + "] LOG OUT";
            }
            else
            {
                double oddBet = double.Parse(Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ","));
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, oddBet);
                if (betGroup == "d" && deviationOdd <= 2)
                {
                    doBet = true;
                }
                else if (betGroup == "c" && deviationOdd <= 1)
                {
                    doBet = true;
                }
                else if (UtilSoccer.CheckOddChange(odd, oddBet) == "Down")
                {
                    doBet = false;
                    message = "IBET: [" + username + "] Odd Down " + odd + "/" + oddBet;
                    Thread.Sleep(2000);
                }
                if (doBet)
                {
                    if (UtilSoccer.CheckOddChange(odd, oddBet) == "Up")
                    {
                        message = "IBET: [" + username + "] Odd Up " + odd + "/" + oddBet;
                    }
                    string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
                    string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
                    PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
                    if (double.Parse(MaxBet) < usdbet)
                    {
                        PostData = PostData.Replace("ItemList[0][stake]=" + usdbet, "ItemList[0][stake]=" + MaxBet);
                        money = (int.Parse(MaxBet.Replace(",", "")) * int.Parse(usd)).ToString();
                    }
                    string ProcessBet = http.Fetch(mainHost + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
                    if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0 && ProcessBet.IndexOf("đang được xử lý") < 0)
                    {
                        message = "IBET: [" + username + "] BET FAILED - INFO: " + o.ToString();
                    }
                    else
                    {
                        this.phieuchung = mylib.generateID("T");
                        this.realmoney = money;
                        db.doInsertTicket("IBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddBet.ToString(), money, usd, "* " + group, this.phieuchung);
                        message = "IBET: [" + username + "] BET SUCCESS - INFO: " + o.ToString();
                    }
                }
            }
        }
        public void playBetUnder(ticket o, string money, string IsLive, string phieuchung, string group)
        {          
            string betGroup = group.Split(',')[1];
            this.phieuchung = phieuchung;
            int usdbet = int.Parse(money) / int.Parse(usd);
            double hdp = o.hdp;
            string keoid = o.keoid;
            double odd = o.odd;
            string bettype = o.bettype;
            string choose = o.choose;
            string homescore = o.score.Split('-')[0];
            string awayscore = o.score.Split('-')[1];

            string PostData = "ItemList[0][sportname]=Soccer&ItemList[0][Hscore]=" + homescore + "&ItemList[0][Ascore]=" + awayscore + "&ItemList[0][type]=OU&ItemList[0][bettype]=" + bettype + "&ItemList[0][oddsid]=" + keoid.Trim() + "&ItemList[0][betteam]=" + choose + "&ItemList[0][stake]=" + usdbet;
            string GetTicket = http.Fetch(mainHost + "/Betting/GetTickets", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
            if (GetTicket.Replace("\"", "").IndexOf("ErrorCode:0") == -1)
            {
                message = "IBET: [" + username + "] LOG OUT";
            }
            else
            {
                double oddBet = double.Parse(Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ","));                
                string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
                string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
                PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
                if (double.Parse(MaxBet) < usdbet)
                {
                    PostData = PostData.Replace("ItemList[0][stake]=" + usdbet, "ItemList[0][stake]=" + MaxBet);
                    money = (int.Parse(MaxBet.Replace(",", "")) * int.Parse(usd)).ToString();
                }
                string ProcessBet = http.Fetch(mainHost + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
                if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0 && ProcessBet.IndexOf("đang được xử lý") < 0)
                {
                    message = "IBET: [" + username + "] BET FAILED - INFO: " + o.ToString();
                }
                else
                {
                    this.phieuchung = mylib.generateID("T");
                    this.realmoney = money;
                    db.doInsertTicket("IBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddBet.ToString(), money, usd, "DX", this.phieuchung);
                    message = "IBET: [" + username + "] BET SUCCESS - INFO: " + o.ToString();
                }
            }
        }
        public string getOddChange(ticket o, string money, string IsLive)
        {
            int usdbet = int.Parse(money) / int.Parse(usd);
            double hdp = o.hdp;
            string keoid = o.keoid;
            double odd = o.odd;
            string bettype = o.bettype;
            string choose = o.choose;
            string homescore = "0";
            string awayscore = "0";
            string PostData = "ItemList[0][sportname]=Soccer&ItemList[0][Hscore]=" + homescore + "&ItemList[0][Ascore]=" + awayscore + "&ItemList[0][type]=OU&ItemList[0][bettype]=" + bettype + "&ItemList[0][oddsid]=" + keoid.Trim() + "&ItemList[0][betteam]=" + choose + "&ItemList[0][stake]=" + usdbet;
            string GetTicket = http.Fetch(mainHost + "/Betting/GetTickets", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
            if (GetTicket.Replace("\"", "").IndexOf("ErrorCode:0") == -1)
            {
                message = "IBET: [" + username + "] LOG OUT";
                return "";
            }
            else
            {
                string oddBet = Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ",");
                return oddBet;
            }
        }
    }
}
