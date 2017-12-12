using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
namespace BET_BET
{
    public class IbetFunction : IFunction
    {
        //string link = "http://www.bong88.com";
        string link = "http://www.88cado.net";
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
            //DBetlist.aspx?fdate=8/29/2017&type=SB&datatype=3
            string betlist = http.Fetch(mainHost + "/Statement/BetList", HttpHelper.HttpMethod.Get, sportLink, null, ip);
            betlist = betlist.Replace("/template", mainHost + "/template").Replace("/bundles", mainHost + "/bundles").Replace("/Scripts", mainHost + "/Scripts");
            return betlist;
        }

        public string getCredit()
        {
            string credit = "";
            credit = http.Fetch(mainHost + "/Customer/Balance", HttpHelper.HttpMethod.Post, sportLink, null, ip, "ContentLength=0");
            credit = Util.GetSubstringByString(credit, "\"GCredit\":\"", ".") + "/" + Util.GetSubstringByString(credit, "\"BCredit\":\"", ".");
            return credit;
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
        public bool checklogin()
        {
            bool result = true;
            string checkLogin = http.Fetch(mainHost + "/LoginCheckin/Index", HttpHelper.HttpMethod.Post, sportLink, null, ip, "ContentLength=0");
            if (checkLogin == "")
            {
                result = false;
            }
            return result;
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
                message = "Login too often, please wait 5 minutes";
            }
            else
            {
                if (data.Contains("window.location='."))
                {
                    string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='.", "';</script>");
                    sportLink = http.FetchResponseUri(link + VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    mainHost = sportLink.Replace("/sports", "");
                    data = http.Fetch(sportLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    if (data != "")
                    {
                        message = "LOGIN SUCCESS";
                        //string checkLogin = "check";
                        //Thread tCheckLogin = new Thread(delegate () {
                        //    while (true)
                        //    {
                        //        Thread.Sleep(60000);
                        //        checkLogin = http.Fetch(mainHost + "/LoginCheckin/Index", HttpHelper.HttpMethod.Post, sportLink, null, ip, "ContentLength=0");
                        //        if(checkLogin=="")
                        //        {
                        //            message = "LOG OUT";
                        //            break;
                        //        }
                        //    }
                        //});
                        //tCheckLogin.Start();
                    }
                    else
                    {
                        message = "Data Empty";
                    }
                }
                else
                {
                    string VerifyInfo_url = Util.GetSubstringByString(data, "window.location='", "';</script>");
                    string mainLink = http.FetchResponseUri(VerifyInfo_url, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
                    mainHost = mainLink.Replace("/main.aspx", "");
                    data = http.Fetch(mainLink, HttpHelper.HttpMethod.Get, link + "/ProcessLogin.aspx", null, ip);
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
                            message = "Login successfully";
                            string checkLogin = "check";
                            Thread tCheckLogin = new Thread(delegate ()
                            {
                                while (true)
                                {
                                    Thread.Sleep(60000);
                                    //checkLogin = http.Fetch(mainHost + "/LoginCheckin/Index", HttpHelper.HttpMethod.Post, sportLink, null, ip, mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                                    checkLogin = http.Fetch(mainHost + "/LoginCheckin/Index", HttpHelper.HttpMethod.Post, sportLink, null, ip, "ContentLength=0");
                                    if (checkLogin == "")
                                    {
                                        message = "LOG OUT";
                                        break;
                                    }
                                }
                            });
                            tCheckLogin.Start();
                        }
                        else
                        {
                            message = "Data Empty";
                        }
                    }
                }

            }
        }

        public void playBetNonLive(ticket o, string money, string IsLive, string phieuchung, string groupinput="",bool AutoAccept=true)
        {
            bool doBet = true;
            string group = "";
            if (groupinput != "")
            {
                group = groupinput.Split(',')[1];
            }
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
                message = GetTicket;
            }
            else
            {
                double oddbet = double.Parse(Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ","));
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, oddbet);
                if (groupinput != "")
                {
                    if (group == "d" && deviationOdd >= -2)
                    {
                        doBet = true;
                    }
                    else if (group == "c" && deviationOdd >= -1)
                    {
                        doBet = true;
                    }
                    else if (deviationOdd >= 0)
                    {
                        doBet = true;
                    }
                    else
                    {
                        doBet = false;
                        message = "Odd Down:" + deviationOdd;
                        db.doInsertTicket("IBET", "ODD DOWN", o.bettype, o.hdp.ToString(), o.home, o.away, choose, odd.ToString(), money, usd, group, this.phieuchung);
                    }
                }
                if (doBet)
                {
                    string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
                    string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
                    PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
                    if (double.Parse(MaxBet) < usdbet)
                    {
                        PostData = PostData.Replace("ItemList[0][stake]=" + usdbet, "ItemList[0][stake]=" + MaxBet);
                        money = (int.Parse(MaxBet.Replace(",", "")) * int.Parse(usd)).ToString();
                    }
                    usdbet = int.Parse(money) / int.Parse(usd);
                    string infobet = (bettype + choose).Replace("1h", o.home + "(FT) ").Replace("1a", o.away + "(FT) ").Replace("7h", o.home + "(H1) ").Replace("7a", o.away + "(H1) ").Replace("3h", o.home + "(OFT) ").Replace("3a", o.home + "(UFT) ").Replace("9h", o.home + "(OH1) ").Replace("9a", o.home + "(UH1) ") + hdp + " " + oddbet + " [" + usdbet + "]";
                    string ProcessBet = http.Fetch(mainHost + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
                    if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0)
                    {
                        message = "BET FAILED" + infobet;
                    }
                    else
                    {
                        this.phieuchung = mylib.generateID("T");
                        this.realmoney = money;
                        db.doInsertTicket("IBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddbet.ToString(), money, usd, group, this.phieuchung);
                        message = "(OK)" + infobet;
                        if (deviationOdd > 0)
                        {
                            message += "Odd Up:" + deviationOdd;
                        }
                    }
                }
            }
        }

        public void playBetLive(ticket o, string money, string IsLive, string phieuchung, string groupinput="", bool AutoAccept = true)
        {
            bool doBet = true;
            string group = "";
            if (groupinput != "")
            {
                group = groupinput.Split(',')[1];
            }
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
                message = GetTicket;
            }
            else
            {
                double oddbet = double.Parse(Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ","));
                
                int deviationOdd = UtilSoccer.getDeviationOdd(odd, oddbet);
                if (groupinput != "")
                {
                    if (group == "d" && deviationOdd >= -2)
                    {
                        doBet = true;
                    }
                    else if (group == "c" && deviationOdd >= -1)
                    {
                        doBet = true;
                    }
                    else if (deviationOdd >= 0)
                    {
                        doBet = true;
                    }
                    else
                    {
                        doBet = false;
                        message = "Odd Down:" + deviationOdd;
                        db.doInsertTicket("IBET", "ODD DOWN", o.bettype, o.hdp.ToString(), o.home, o.away, choose, odd.ToString(), money, usd, group, this.phieuchung);
                    }
                }
                else
                {
                    message = "group empty";
                    return;
                }
                if (doBet)
                {
                    string MinBet = Util.GetSubstringByString(GetTicket, "\"Minbet\":\"", "\",");
                    string MaxBet = Util.GetSubstringByString(GetTicket, "Maxbet\":\"", "\",");
                    PostData = PostData + "&ItemList[0][oddsStatus]=&ItemList[0][min]=" + MinBet + "&ItemList[0][max]=" + MaxBet;
                    if (double.Parse(MaxBet) < usdbet)
                    {
                        PostData = PostData.Replace("ItemList[0][stake]=" + usdbet, "ItemList[0][stake]=" + MaxBet);
                        money = (int.Parse(MaxBet.Replace(",", "")) * int.Parse(usd)).ToString();
                    }
                    usdbet = int.Parse(money) / int.Parse(usd);
                    string infobet = (bettype + choose).Replace("1h", o.home + "(FT) ").Replace("1a", o.away + "(FT) ").Replace("7h", o.home + "(H1) ").Replace("7a", o.away + "(H1) ").Replace("3h", o.home + "(OFT) ").Replace("3a", o.home + "(UFT) ").Replace("9h", o.home + "(OH1) ").Replace("9a", o.home + "(UH1) ") + hdp + " " + oddbet + " [" + usdbet + "]";
                    string ProcessBet = http.Fetch(mainHost + "/Betting/ProcessBet", HttpHelper.HttpMethod.Post, sportLink, PostData, ip);
                    if (ProcessBet.IndexOf("Đơn cược đã được chấp nhận") < 0 && ProcessBet.IndexOf("Your bet has been accepted") < 0 && ProcessBet.IndexOf("đang được xử lý") < 0)
                    {
                        message = "BET FAILED" + infobet;
                    }
                    else
                    {
                        this.phieuchung = mylib.generateID("T");
                        this.realmoney = money;
                        db.doInsertTicket("IBET", username, o.bettype, o.hdp.ToString(), o.home, o.away, choose, oddbet.ToString(), money, usd, group, this.phieuchung);
                        message = "(OK)" + infobet;
                        if (deviationOdd > 0)
                        {
                            message += "Odd Up:" + deviationOdd;
                        }
                    }
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
                return "e";
            }
            else
            {
                string oddBet = Util.GetSubstringByString(GetTicket.Replace("\"", ""), "DisplayOdds:", ",");
                return oddBet;
            }
        }
        
    }
}
