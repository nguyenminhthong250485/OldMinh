using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using HtmlAgilityPack;
using System.IO;
namespace BET_BET
{
    public partial class BetControl : UserControl
    {
        string str_CompareNonLive_AA = "";
        string str_CompareLive_AA = "";
        //string str_CompareNonLive_AB = "";
        //string str_CompareNonLive_AC = "";
        //string str_CompareNonLive_AD = "";
        //string str_CompareNonLive_CA = "";
        //string str_CompareNonLive_CB = "";
        //string str_CompareNonLive_CC = "";
        //string str_CompareNonLive_CD = "";
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        DataTable dtSBO, dtIBET;
        Thread tSbo, tIbet, tBetNonLive, tBetLive, ibetclick, sboclick;
        Database db;
        Random ran = new Random();
        string data_chienthuat = "";
        private void remove_chienthuat(string key)
        {
            string result = "";
            string str_chienthuat = richChienThuat.Text;
            string[] mangchienthuat = str_chienthuat.Split('\n');
            foreach (string chienthuat in mangchienthuat)
            {
                if(chienthuat.Contains("@"))
                {
                    result += chienthuat + "\n";
                    continue;
                }
                if (chienthuat.Trim() != "")
                {
                    if (key.Contains("ibet"))
                    {
                        string rowchienthuat = "";
                        string chienthuaibet = key.Replace("ibet", "");
                        foreach(string nibet in chienthuat.Split('/')[0].Split(','))
                        {
                            if(nibet!=chienthuaibet)
                            {
                                rowchienthuat += nibet + ",";
                            }
                        }
                        if (rowchienthuat != "")
                        {
                            rowchienthuat = rowchienthuat.Substring(0, rowchienthuat.Length - 1);
                            result += rowchienthuat + "/" + chienthuat.Split('/')[1] + "\n";
                        }
                    }
                    else
                    {
                        string rowchienthuat = "";
                        string chienthuasbo = key.Replace("sbo", "");
                        foreach (string nsbo in chienthuat.Split('/')[1].Split(','))
                        {
                            if (nsbo != chienthuasbo)
                            {
                                rowchienthuat += nsbo + ",";
                            }
                        }
                        if (rowchienthuat != "")
                        {
                            rowchienthuat = rowchienthuat.Substring(0, rowchienthuat.Length - 1) + "\n";
                            result += chienthuat.Split('/')[0] + "/" + rowchienthuat;
                        }
                    }
                }
            }
            richChienThuat.Text = result.Substring(0, result.Length - 1);
        }
        public BetControl()
        {
            InitializeComponent();
            db = new Database();
        }
        bool clickLive = false, clickNonLive = false;
        private void BetControl_Load(object sender, EventArgs e)
        {
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;

            data_chienthuat = System.IO.File.ReadAllText("../../../chienthuat.txt");
            richChienThuat.Text = data_chienthuat;
            loadDataToComboBox();
            createTableSBO(db.getAccountBet(cboSheet.Text, "sbo"));
            createTableIBET(db.getAccountBet(cboSheet.Text, "ibet"));
            Thread Auto;
            Auto = new Thread(delegate ()
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    if(clickNonLive)
                    {
                        player.SoundLocation = "../../../Sound/change_strategy.wav";
                        player.Play();
                        SendMessageToTelegram("change_strategy " + richChienThuat.Text.Split('\n')[0]);
                        Thread.Sleep(20000);
                        btnBet.PerformClick();
                        clickNonLive = false;
                    }
                }
            });
            Auto.Start();
        }
        public string ChangeCreditIbet(string MemberName, string AgentName, string Credit)
        {
            string result = "Error Request";
            HttpHelper http;
            
            http = new HttpHelper();
            string UrlIbet = "https://www.b8ag.com";
            string LoginName = AgentName;
            string Pass = "lucky6868@";
            //string Code = "790790";
            string detecas_token = "";
            string data = http.Fetch(UrlIbet, HttpHelper.HttpMethod.Get, null, null);
            string code = Util.HtmlGetAttributeValue(data, "value", "//input[@id='code']");
            if (code == "") return result;
            string password = mylib.hc(Pass, code);

            data = http.Fetch(UrlIbet + "/", HttpHelper.HttpMethod.Post, UrlIbet, "hidLanguage=en-US&txtUserName=" + LoginName + " &txtPassWord=" + password + "&code=" + code);
            string errorMessage = Util.HtmlGetInnerText(data, "//div[@id='errmsg']");
            if (!errorMessage.Contains("Wrong username/nickname or password"))
            {
                if (!errorMessage.Contains("Account closed. Please contact your administrator"))
                {
                    data = http.Fetch(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Get, UrlIbet, null);
                    //Thread.Sleep(5000);
                    detecas_token = Util.HtmlGetAttributeValue(data, "value", "//input[@id='detecas-token']");
                    detecas_token = Util.EscapeDataString(detecas_token);
                    string UrlIbetMain = http.FetchResponseUri(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Post, UrlIbet + "/Customer/SignIn/Flow", "detecas-token=" + detecas_token);
                    UrlIbetMain = UrlIbetMain.Replace("/site-main/", "");
                    string data_site_main = http.Fetch(UrlIbetMain + "/site-main/", HttpHelper.HttpMethod.Get, UrlIbetMain + "/Customer/SignIn/Flow", null);
                    //string data_Credit = http.Fetch(UrlIbetMain + "/ex-main/_MemberInfo/CreditBalance/CreditTransfer.aspx?custid=27029908&amt=9,000&username=DT7A0A2030&roleId={RoleId}", HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);
                    string data_CreditBalanceList = http.Fetch(UrlIbetMain + "/ex-main/_MemberInfo/CreditBalance/CreditBalanceList.aspx", HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);
                    HtmlNodeCollection TagAs = Util.HtmlGetNodeCollection(data_CreditBalanceList, "//a");
                    foreach (HtmlNode TagA in TagAs)
                    {
                        if (TagA.OuterHtml.Contains(MemberName.ToUpper()) && TagA.OuterHtml.Contains("openTransfer"))
                        {
                            string datalinkmember = Util.GetSubstringByString(TagA.OuterHtml, "openTransfer(", ")").Replace("'", "");
                            string id = datalinkmember.Split(',')[0];
                            string credit = "";
                            if (datalinkmember.Split(',').Length == 4)
                            {
                                credit = datalinkmember.Split(',')[1];
                            }
                            else
                            {
                                credit = datalinkmember.Split(',')[1] + "," + datalinkmember.Split(',')[2];
                            }
                            string UrlMemberBalance = "/ex-main/_MemberInfo/CreditBalance/CreditTransfer.aspx?custid=" + id + "&amt=" + credit + "&username=" + MemberName + "&roleId={RoleId}";
                            string data_CreditBalanceMember = http.Fetch(UrlIbetMain + UrlMemberBalance, HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);
                            string data_SecurityCode = http.Fetch(UrlIbetMain + "/site-main/SecurityCode/VerifySecurityCode?redirectUrl=" + UrlMemberBalance, HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);
                            string RequestVerificationToken = Util.HtmlGetAttributeValue(data_SecurityCode, "value", "//input[@name='__RequestVerificationToken']");
                            string securitycode_token = Util.HtmlGetAttributeValue(data_SecurityCode, "value", "//input[@id='securitycode-token']");//5j5kkk
                            string security_salt = Util.HtmlGetAttributeValue(data_SecurityCode, "value", "//input[@id='security-salt']");//
                            string hash_securitycode = mylib.hashSecurityCode(security_salt, securitycode_token, "790790");//2b3c1e3903672d4481c5bbab998275f5708785c0c3dd55dc9b3ca1f1024b8a85
                            string data_VerifySecurityCode = http.Fetch(UrlIbetMain + "/site-main/SecurityCode/VerifySecurityCode", HttpHelper.HttpMethod.Post, UrlIbetMain + "/site-main/SecurityCode/VerifySecurityCode?redirectUrl=" + UrlMemberBalance, "securityCode=" + hash_securitycode, "", "__RequestVerificationToken=" + RequestVerificationToken);
                            data_CreditBalanceMember = http.Fetch(UrlIbetMain + UrlMemberBalance, HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/SecurityCode/VerifySecurityCode?redirectUrl=" + UrlMemberBalance, null);
                            RequestVerificationToken = Util.HtmlGetAttributeValue(data_CreditBalanceMember, "value", "//input[@name='__RequestVerificationToken']");
                            string data_UpdateCreditMember = http.Fetch(UrlIbetMain + "/ex-main/_AccountInfo/Handlers/UpdateGivenCredit.ashx", HttpHelper.HttpMethod.Post, UrlIbetMain + UrlMemberBalance, "custid=" + id + "&custRoleId={RoleId}&amount=" + Credit, "", "__RequestVerificationToken=" + RequestVerificationToken);
                            if (data_UpdateCreditMember.Contains("Updated successfully"))
                            {
                                result = "UPDATE SUCCESS";
                            }
                            else
                            {
                                result = data_UpdateCreditMember;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    result = "Account closed. Please contact your administrator";
                }
            }
            else
            {
                result = "Wrong username/nickname or password";
            }
            
            return result;
        }
        public string ChangeCreditSbo(string MemberName, string AgentName, string Credit)
        {
            string result = "Error Request";
         
            HttpHelper http = new HttpHelper();
           
            string data = "";
                
            string password = "lucky6868@";
            string security_code = "790790";
            string url = http.FetchResponseUri("http://123.tek789.com", HttpHelper.HttpMethod.Get, null, null);
            data = http.Fetch(url, HttpHelper.HttpMethod.Get, null, null);
            string _requesttoken = Util.HtmlGetAttributeValue(data, "value", "//input[@name='__RequestVerificationToken']");
            string welcome = http.Fetch(url + "Captcha", HttpHelper.HttpMethod.Post, url, "__RequestVerificationToken=" + Util.EscapeDataString(_requesttoken) + "&username=" + Util.EscapeDataString(AgentName) + "&password=" + Util.EscapeDataString(password) + "&lang=EN&btnSubmit=" + Util.EscapeDataString("Sign In"));
            if (!welcome.Contains("Your login name and password is not valid"))
            {
                if (welcome.Contains("securityCodeForm"))
                {
                    string security_page = http.Fetch(url + "Security/SecurityCode", HttpHelper.HttpMethod.Get, url, null);
                    _requesttoken = Util.HtmlGetAttributeValue(security_page, "value", "//input[@name='__RequestVerificationToken']");
                    string security_message = Util.HtmlGetInnerText(security_page, "//p[contains(text(),'Security Code :')]");
                    char digit1 = security_code[Int16.Parse(security_message.Split('\n')[1].Trim().Substring(0, 1)) - 1];
                    char digit2 = security_code[Int16.Parse(security_message.Split('\n')[2].Trim().Substring(0, 1)) - 1];
                    welcome = http.Fetch(url + "Security/ValidateSecurityCode", HttpHelper.HttpMethod.Post, url + "Security/SecurityCode", "__RequestVerificationToken=" + Util.EscapeDataString(_requesttoken) + "&hiduseDesktop=&hidIsFromSecurityCode=1&FirstChar=" + digit1 + "&SecondChar=" + digit2 + "&btnSubmit=Submit");
                }

                if (!welcome.Contains("Please enter a valid security code"))
                {
                    url = "http://" + Util.GetSubstringByString(Util.HtmlGetAttributeValue(welcome, "action", "//form[@id='f']"), "http://", "welcome.aspx");
                    http.Fetch(url + "welcome.aspx?id=" + Util.HtmlGetAttributeValue(welcome, "value", "//input[@name='id']") + "&key=" + Util.HtmlGetAttributeValue(welcome, "value", "//input[@name='key']") + "&lang=en&useDesktop=no", HttpHelper.HttpMethod.Get, url + "processlogin.aspx", null);
                    http.Fetch(url + "webroot/restricted/tc/en/iom_tc_agent.aspx", HttpHelper.HttpMethod.Get, url + "webroot/restricted/tc/termsconditions.aspx?type=iom_audit", null);
                    http.Fetch(url + "webroot/restricted/tc/termsconditions.aspx", HttpHelper.HttpMethod.Post, url + "webroot/restricted/tc/termsconditions.aspx?type=iom_audit", "agree=1");
                    http.Fetch(url + "webroot/restricted/home.aspx", HttpHelper.HttpMethod.Get, url + "webroot/restricted/tc/termsconditions.aspx?type=iom_audit", null);
                    string hometop = http.Fetch(url + "webroot/restricted/HomeTop.aspx", HttpHelper.HttpMethod.Get, url + "webroot/restricted/home.aspx", null);
                }
            }
            data = http.Fetch(url + "/webroot/restricted/HomeLeft.aspx", HttpHelper.HttpMethod.Get, url + "webroot/restricted/home.aspx", null);
            data = http.Fetch(url + "/webroot/restricted/membermgmt/member_credit.aspx", HttpHelper.HttpMethod.Get, url + "/webroot/restricted/HomeLeft.aspx", null);
            string data_Security = http.Fetch(url + "/webroot/restricted/security/security-code-prompt.aspx", HttpHelper.HttpMethod.Get, url + " /webroot/restricted/HomeLeft.aspx", null);
            HtmlNodeCollection Spans = Util.HtmlGetNodeCollection(data_Security, "//span");
            string digit = "";
            foreach (HtmlNode Span in Spans)
            {
                digit += Span.InnerText[0];
            }
            string HidCK = Util.HtmlGetAttributeValue(data_Security, "value", "//input[@id='HidCK']");
            string digit11 = security_code[Int16.Parse(digit[0].ToString()) - 1].ToString();
            string digit22 = security_code[Int16.Parse(digit[1].ToString()) - 1].ToString();
            string post_data_Security = http.Fetch(url + "/webroot/restricted/security/security-code-prompt.aspx", HttpHelper.HttpMethod.Post, url + "/webroot/restricted/security/security-code-prompt.aspx", "cmd=securityCode&digit1=" + digit11 + "&digit2=" + digit22 + "&HidCK=" + HidCK);
            data = http.Fetch(url + "/webroot/restricted/membermgmt/member_credit.aspx", HttpHelper.HttpMethod.Get, url + "/webroot/restricted/security/security-code-prompt.aspx", null);
            string Filter = Util.HtmlGetAttributeValue(data, "value", "//input[@id='Filter']");
            string ek = Util.HtmlGetAttributeValue(data, "value", "//input[@id='ek']");
            string dataagent = Util.HtmlGetAttributeValue(data, "value", "//input[@id='data']");
            HidCK = Util.HtmlGetAttributeValue(data, "value", "//input[@id='HidCK']");
            string temp = Util.HtmlGetAttributeValue(data, "onclick", "//td[@class='TAC' and contains(.,'" + MemberName + "')]/../td/img");
            string idmember = Util.GetSubstringByString(temp, "this,'", "')");
            string UpdateCredit = http.Fetch(url + "/webroot/restricted/membermgmt/member_credit.aspx", HttpHelper.HttpMethod.Post, url + "/webroot/restricted/membermgmt/member_credit.aspx", "__VIEWSTATE=&TS_HidTSID=" + idmember + "%2CS&Filter=0&data=" + dataagent + "&ek=" + ek + "&tk=0&txtCredit=" + Credit + "&txtPlayableLimit=0&selTableLimit=1&HidCK=" + HidCK);

            if (UpdateCredit.Contains("successfully updated"))
            {
                result = "UPDATE SUCCESS";
            }

            return result;
        }
        private void loadDataToComboBox()
        {
            cboSheet.DataSource = db.getPartnerName();
            cboSheet.Name = "name";
            cboSheet.ValueMember = "name";
            cboSheet.SelectedIndex += 1;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            BetManager.clearHashTable();
            createTableSBO(db.getAccountBet(cboSheet.Text, "sbo"));
            createTableIBET(db.getAccountBet(cboSheet.Text, "ibet"));
        }

        private string getCommonMoney(string money1, string money2)
        {
            string common = "";
            if (money1 == "") return money2;
            if (money2 == "") return money1;
            List<string> lst1 = money1.Split(',').ToList();
            List<string> lst2 = money2.Split(',').ToList();
            foreach (string str in lst2)
            {
                if (lst1.Contains(str))
                    common += str + ",";
            }
            if(common!="")
                common = common.Substring(0, common.Length - 1);
            return common;
        }
        private string getMoneyBet(string str_CreditSbo,string str_CreditIbet,string str_MaxbetSbo, string str_MaxbetIbet, string str_MoneySbo, string str_MoneyIbet,string str_UsdSbo,string str_UsdIbet,bool Max=false)
        {
            string result = "";
            int HeSoIbet = ran.Next(1, int.Parse(str_MoneyIbet.Split(',')[1]));
            int HeSoSbo = ran.Next(1, int.Parse(str_MoneySbo.Split(',')[1]));
            double UsdSbo = double.Parse(str_UsdSbo);
            double UsdIbet = double.Parse(str_UsdIbet);
            double CreditSbo = Math.Floor(double.Parse(str_CreditSbo)) * UsdSbo;
            double CreditIbet = Math.Floor(double.Parse(str_CreditIbet)) * UsdIbet;
            double MaxbetSbo = double.Parse(str_MaxbetSbo) * UsdSbo;
            double MaxbetIbet = double.Parse(str_MaxbetIbet) * UsdIbet;
            double MoneySbo = double.Parse(str_MoneySbo.Split(',')[0]) * (double)HeSoSbo * UsdSbo;
            double MoneyIbet = double.Parse(str_MoneyIbet.Split(',')[0]) * (double)HeSoIbet * UsdIbet;
            if(CreditSbo< double.Parse(str_MoneySbo.Split(',')[0])/5||CreditSbo/ UsdSbo < 10)
            {
                result += "sbo";
            }
            if (CreditIbet < double.Parse(str_MoneyIbet.Split(',')[0]) / 5||CreditIbet/ UsdIbet < 10)
            {
                result += "ibet";
            }
            if (result != "") return result;
            double MaxBet = Math.Min(Math.Min(Math.Min(CreditSbo, CreditIbet), Math.Min(MaxbetSbo, MaxbetIbet)), Math.Min(MoneySbo, MoneyIbet));
            if(Max)
            {
                MaxBet = Math.Min(Math.Min(CreditSbo, CreditIbet), Math.Min(MaxbetSbo, MaxbetIbet));
            }
            result = MaxBet.ToString();
            return result;
        }
        private string getModeBet(string modeIbet,string modeSbo)
        {
            string result = "al";
            switch(modeIbet+modeSbo)
            {
                case "ww":
                    result = "w";
                    break;
                case "ll":
                    result = "l";
                    break;
                case "iwsw":
                    result = "al";
                    break;
                case "iwiw":
                    result = "iw";
                    break;
                case "swiw":
                    result = "al";
                    break;
                case "swsw":
                    result = "sw";
                    break;
                case "lsw":
                case "liw":
                    result = "sw";
                    break;
                case "swl":
                case "iwl":
                    result = "iw";
                    break;
            }
            return result;
        }
        private void createTableSBO(DataTable dt,bool status=false,string FilterGroup="")
        {
            dataGridViewSBO.Columns.Clear();
            dtSBO = new DataTable();
            dtSBO.Columns.Add("key");
            dtSBO.PrimaryKey = new DataColumn[] { dtSBO.Columns["key"] };
            dtSBO.Columns.Add("group");
            dtSBO.Columns.Add("ip");
            dtSBO.Columns.Add("username");
            dtSBO.Columns.Add("usd");
            dtSBO.Columns.Add("gcredit");
            dtSBO.Columns.Add("credit");
            dtSBO.Columns.Add("message");
            dtSBO.Columns.Add("profit");
            dtSBO.Columns.Add("league");
            dtSBO.Columns.Add("money");
            dtSBO.Columns.Add("mode");
            dtSBO.Columns.Add("maxbet");

            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["status"].ToString() == "0" && status == true)
                {
                    continue;
                }
                if (FilterGroup != "")
                {
                    if (dr["group"].ToString() == FilterGroup)
                    {

                        index++;
                        dtSBO.Rows.Add(new Object[] { "sbo" + index, dr["group"], dr["ip"], dr["username"], dr["usd"],"", "", "", dr["profit"], dr["league"], dr["money"], dr["mode"], dr["maxbet"] });
                    }
                }
                else
                {
                    index++;
                    dtSBO.Rows.Add(new Object[] { "sbo" + index, dr["group"], dr["ip"], dr["username"], dr["usd"],"", "", "", dr["profit"], dr["league"], dr["money"], dr["mode"], dr["maxbet"] });
                }
            }
            
            dataGridViewSBO.DataSource = dtSBO;
            int Width_dataGridView = dataGridViewSBO.Width;

            dataGridViewSBO.Columns["key"].HeaderText = "KEY";
            dataGridViewSBO.Columns["group"].HeaderText = "G";
            dataGridViewSBO.Columns["ip"].HeaderText = "IP";
            dataGridViewSBO.Columns["username"].HeaderText = "USERNAME";
            dataGridViewSBO.Columns["usd"].HeaderText = "U";
            dataGridViewSBO.Columns["gcredit"].HeaderText = "GCR";
            dataGridViewSBO.Columns["credit"].HeaderText = "CR";
            dataGridViewSBO.Columns["message"].HeaderText = "MESSAGE";
            dataGridViewSBO.Columns["profit"].HeaderText = "P";
            dataGridViewSBO.Columns["league"].HeaderText = "L";
            dataGridViewSBO.Columns["money"].HeaderText = "MONEY";
            dataGridViewSBO.Columns["mode"].HeaderText = "M";
            dataGridViewSBO.Columns["maxbet"].HeaderText = "MAX";

            dataGridViewSBO.Columns["key"].ReadOnly = true;
            dataGridViewSBO.Columns["group"].ReadOnly = true;
            dataGridViewSBO.Columns["ip"].ReadOnly = true;
            dataGridViewSBO.Columns["username"].ReadOnly = true;
            dataGridViewSBO.Columns["usd"].ReadOnly = true;
            dataGridViewSBO.Columns["gcredit"].ReadOnly = true;
            dataGridViewSBO.Columns["message"].ReadOnly = true;
            dataGridViewSBO.Columns["maxbet"].ReadOnly = true;

            dataGridViewSBO.Columns["key"].Width = Width_dataGridView * 5 / 100;
            dataGridViewSBO.Columns["group"].Width = Width_dataGridView * 3 / 100;
            dataGridViewSBO.Columns["ip"].Width = Width_dataGridView * 5 / 100;
            dataGridViewSBO.Columns["username"].Width = Width_dataGridView * 10 / 100;
            dataGridViewSBO.Columns["usd"].Width = Width_dataGridView * 3 / 100;
            dataGridViewSBO.Columns["gcredit"].Width = Width_dataGridView * 5 / 100;
            dataGridViewSBO.Columns["credit"].Width = Width_dataGridView * 5 / 100;
            dataGridViewSBO.Columns["message"].Width = Width_dataGridView * 40 / 100;
            dataGridViewSBO.Columns["profit"].Width = Width_dataGridView * 3 / 100;
            dataGridViewSBO.Columns["league"].Width = Width_dataGridView * 3 / 100;
            dataGridViewSBO.Columns["money"].Width = Width_dataGridView * 5 / 100;
            dataGridViewSBO.Columns["mode"].Width = Width_dataGridView * 3 / 100;
            dataGridViewSBO.Columns["maxbet"].Visible = false;

            DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
            colButton.HeaderText = "BET LIST";
            colButton.Name = "betlist";
            colButton.Text = "View";
            colButton.Width = Width_dataGridView * 5 / 100;
            colButton.UseColumnTextForButtonValue = true;
            dataGridViewSBO.Columns.Add(colButton);

            DataGridViewButtonColumn colButtonLogin = new DataGridViewButtonColumn();
            colButtonLogin.HeaderText = "Login";
            colButtonLogin.Name = "dologin";
            colButtonLogin.Text = "Login";
            colButtonLogin.Width = Width_dataGridView * 5 / 100;
            colButtonLogin.UseColumnTextForButtonValue = true;
            colButtonLogin.Visible = false;
            dataGridViewSBO.Columns.Add(colButtonLogin);

            DataGridViewButtonColumn colButtonCredit = new DataGridViewButtonColumn();
            colButtonCredit.HeaderText = "$";
            colButtonCredit.Name = "Balance";
            colButtonCredit.Text = "$";
            colButtonCredit.Width = Width_dataGridView * 5 / 100;
            colButtonCredit.UseColumnTextForButtonValue = true;
            dataGridViewSBO.Columns.Add(colButtonCredit);

            foreach (DataGridViewColumn col in dataGridViewSBO.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void createTableIBET(DataTable dt,bool status=false, string FilterGroup = "")
        {
            dataGridViewIBET.Columns.Clear();
            dtIBET = new DataTable();
            dtIBET.Columns.Add("key");
            dtIBET.PrimaryKey = new DataColumn[] { dtIBET.Columns["key"] };
            dtIBET.Columns.Add("group");
            dtIBET.Columns.Add("ip");
            dtIBET.Columns.Add("username");
            dtIBET.Columns.Add("usd");
            dtIBET.Columns.Add("gcredit");
            dtIBET.Columns.Add("credit");
            dtIBET.Columns.Add("message");
            dtIBET.Columns.Add("profit");
            dtIBET.Columns.Add("league");
            dtIBET.Columns.Add("money");
            dtIBET.Columns.Add("mode");
            dtIBET.Columns.Add("maxbet");

            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["status"].ToString() == "0" && status == true)
                {
                    continue;
                }
                
                if (FilterGroup != "")
                {
                    if (dr["group"].ToString() == FilterGroup)
                    {
                        index++;
                        dtIBET.Rows.Add(new Object[] { "ibet" + index, dr["group"], dr["ip"], dr["username"], dr["usd"],"", "", "", dr["profit"], dr["league"], dr["money"], dr["mode"], dr["maxbet"] });
                    }
                }
                else
                {
                    index++;
                    dtIBET.Rows.Add(new Object[] { "ibet" + index, dr["group"], dr["ip"], dr["username"], dr["usd"],"", "", "", dr["profit"], dr["league"], dr["money"], dr["mode"], dr["maxbet"] });
                }
            }

            dataGridViewIBET.DataSource = dtIBET;
            int Width_dataGridView = dataGridViewIBET.Width;

            dataGridViewIBET.Columns["key"].HeaderText = "KEY";
            dataGridViewIBET.Columns["group"].HeaderText = "G";
            dataGridViewIBET.Columns["ip"].HeaderText = "IP";
            dataGridViewIBET.Columns["username"].HeaderText = "USERNAME";
            dataGridViewIBET.Columns["usd"].HeaderText = "U";
            dataGridViewIBET.Columns["gcredit"].HeaderText = "GCR";
            dataGridViewIBET.Columns["credit"].HeaderText = "CR";
            dataGridViewIBET.Columns["message"].HeaderText = "MESSAGE";
            dataGridViewIBET.Columns["profit"].HeaderText = "P";
            dataGridViewIBET.Columns["league"].HeaderText = "L";
            dataGridViewIBET.Columns["money"].HeaderText = "MONEY";
            dataGridViewIBET.Columns["mode"].HeaderText = "M";
            dataGridViewIBET.Columns["maxbet"].HeaderText = "MAX";

            dataGridViewIBET.Columns["key"].ReadOnly = true;
            dataGridViewIBET.Columns["group"].ReadOnly = true;
            dataGridViewIBET.Columns["ip"].ReadOnly = true;
            dataGridViewIBET.Columns["username"].ReadOnly = true;
            dataGridViewIBET.Columns["usd"].ReadOnly = true;
            dataGridViewIBET.Columns["gcredit"].ReadOnly = true;
            dataGridViewIBET.Columns["message"].ReadOnly = true;
            dataGridViewIBET.Columns["maxbet"].ReadOnly = true;

            dataGridViewIBET.Columns["key"].Width = Width_dataGridView * 5 / 100;
            dataGridViewIBET.Columns["group"].Width = Width_dataGridView * 3 / 100;
            dataGridViewIBET.Columns["ip"].Width = Width_dataGridView * 5 / 100;
            dataGridViewIBET.Columns["username"].Width = Width_dataGridView * 10 / 100;
            dataGridViewIBET.Columns["usd"].Width = Width_dataGridView * 3 / 100;
            dataGridViewIBET.Columns["gcredit"].Width = Width_dataGridView * 5 / 100;
            dataGridViewIBET.Columns["credit"].Width = Width_dataGridView * 5 / 100;
            dataGridViewIBET.Columns["message"].Width = Width_dataGridView * 40 / 100;
            dataGridViewIBET.Columns["profit"].Width = Width_dataGridView * 3 / 100;
            dataGridViewIBET.Columns["league"].Width = Width_dataGridView * 3 / 100;
            dataGridViewIBET.Columns["money"].Width = Width_dataGridView * 5 / 100;
            dataGridViewIBET.Columns["mode"].Width = Width_dataGridView * 3 / 100;
            dataGridViewIBET.Columns["maxbet"].Visible = false;



            DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
            colButton.HeaderText = "BET LIST";
            colButton.Name = "betlist";
            colButton.Text = "View";
            colButton.Width = Width_dataGridView * 5 / 100;
            colButton.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(colButton);

            DataGridViewButtonColumn colButtonLogin = new DataGridViewButtonColumn();
            colButtonLogin.HeaderText = "Login";
            colButtonLogin.Name = "dologin";
            colButtonLogin.Text = "Login";
            colButtonLogin.Width = Width_dataGridView * 5 / 100;
            colButtonLogin.UseColumnTextForButtonValue = true;
            colButtonLogin.Visible = false;
            dataGridViewIBET.Columns.Add(colButtonLogin);

            DataGridViewButtonColumn colButtonCredit = new DataGridViewButtonColumn();
            colButtonCredit.HeaderText = "$";
            colButtonCredit.Name = "Balance";
            colButtonCredit.Text = "$";
            colButtonCredit.Width = Width_dataGridView * 5 / 100;
            colButtonCredit.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(colButtonCredit);

            foreach (DataGridViewColumn col in dataGridViewIBET.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridViewSBO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sboclick = new Thread(delegate ()
            {
                if (e.ColumnIndex == dataGridViewSBO.Columns["betlist"].Index)
                {
                    string title = dataGridViewSBO["username", e.RowIndex].Value.ToString();
                    string key = dataGridViewSBO["key", e.RowIndex].Value.ToString();

                    Browser web = new Browser(title, BetManager.getBet(key).getBetList());
                    web.Show();
                    System.Windows.Threading.Dispatcher.Run();
                }
                else if (e.ColumnIndex == dataGridViewSBO.Columns["dologin"].Index)
                {
                    string key = dataGridViewSBO["key", e.RowIndex].Value.ToString();
                    string ip = dataGridViewSBO["ip", e.RowIndex].Value.ToString();
                    string username = dataGridViewSBO["username", e.RowIndex].Value.ToString();
                    string usd = dataGridViewSBO["usd", e.RowIndex].Value.ToString();
                    BetManager.createBet("sbo", key, ip, username, usd);
                    BetManager.getBet(key).login();
                    dataGridViewSBO["gcredit", e.RowIndex].Value = BetManager.getBet(key).getCredit().Split('/')[0];
                    dataGridViewSBO["credit", e.RowIndex].Value = BetManager.getBet(key).getCredit().Split('/')[1];
                    dataGridViewSBO["message", e.RowIndex].Value = BetManager.getBet(key).getMessage();
                }
                else if (e.ColumnIndex == dataGridViewSBO.Columns["Balance"].Index)
                {
                    string key = dataGridViewSBO["key", e.RowIndex].Value.ToString();
                    string username = dataGridViewSBO["username", e.RowIndex].Value.ToString();
                    string credit = dataGridViewSBO["credit", e.RowIndex].Value.ToString();
                    string UpdateMessage = ChangeCreditSbo(username, username.Substring(0, username.Length - 3) + "Sub99", credit);
                    dataGridViewSBO["message", e.RowIndex].Value = UpdateMessage;
                    updateCredit(key, BetManager.getBet(key).getCredit().Split('/')[1]);
                }
            });
            sboclick.SetApartmentState(ApartmentState.STA);
            sboclick.IsBackground = true;            
            sboclick.Start();
        }

        private void dataGridViewIBET_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ibetclick = new Thread(delegate ()
            {
                if (e.ColumnIndex == dataGridViewIBET.Columns["betlist"].Index)
                {

                    string title = dataGridViewIBET["username", e.RowIndex].Value.ToString();
                    string key = dataGridViewIBET["key", e.RowIndex].Value.ToString();
                    Browser web = new Browser(title, BetManager.getBet(key).getBetList());
                    web.Show();
                    System.Windows.Threading.Dispatcher.Run();
                }
                else if (e.ColumnIndex == dataGridViewIBET.Columns["dologin"].Index)
                {
                    string key = dataGridViewIBET["key", e.RowIndex].Value.ToString();
                    string ip = dataGridViewIBET["ip", e.RowIndex].Value.ToString();
                    string username = dataGridViewIBET["username", e.RowIndex].Value.ToString();
                    string usd = dataGridViewIBET["usd", e.RowIndex].Value.ToString();
                    BetManager.createBet("ibet", key, ip, username, usd);
                    BetManager.getBet(key).login();
                    dataGridViewIBET["gcredit", e.RowIndex].Value = BetManager.getBet(key).getCredit().Split('/')[0];
                    dataGridViewIBET["credit", e.RowIndex].Value = BetManager.getBet(key).getCredit().Split('/')[1];
                    dataGridViewIBET["message", e.RowIndex].Value = BetManager.getBet(key).getMessage();
                }
                else if (e.ColumnIndex == dataGridViewIBET.Columns["Balance"].Index)
                {
                    string key = dataGridViewIBET["key", e.RowIndex].Value.ToString();
                    string username = dataGridViewIBET["username", e.RowIndex].Value.ToString();
                    string credit = dataGridViewIBET["credit", e.RowIndex].Value.ToString();
                    string UpdateMessage = ChangeCreditIbet(username, username.Substring(0, username.Length - 3) + "Sub99", credit);
                    dataGridViewIBET["message", e.RowIndex].Value = UpdateMessage;
                    updateCredit(key, BetManager.getBet(key).getCredit().Split('/')[1]);
                }
            });
            ibetclick.SetApartmentState(ApartmentState.STA);
            ibetclick.IsBackground = true;
            ibetclick.Start();
        }

        
        private void SendMessageToTelegram(string text)
        {
            //TelegramHelper tele = new TelegramHelper("472134659:AAGBdeNQki_tz5LxL-KW9904ckM78kQkNTM");
            //tele.sendMessage("252386501", text);
            TelegramHelper tele = new TelegramHelper("395167591:AAHXemyL-dIL6x1lFBkMQ035i8kDJ9XmJao");
            tele.sendMessage("-219293314", text);
        }
        private void updateMessage(string key, string message)
        {
            if (key.Contains("ibet"))
                dataGridViewIBET["message", findRowByKey(dataGridViewIBET, key)].Value = message;
            else
                dataGridViewSBO["message", findRowByKey(dataGridViewSBO, key)].Value = message;
        }

        private void updateCredit(string key, string credit)
        {
            if (key.Contains("ibet"))
                dataGridViewIBET["credit", findRowByKey(dataGridViewIBET, key)].Value = credit;
            else
                dataGridViewSBO["credit", findRowByKey(dataGridViewSBO, key)].Value = credit;
        }

        private info getInfoByKey(string key)
        {
            int index = -1;
            info o = new info();
            try
            {
                if (key.Contains("ibet"))
                {
                    index = findRowByKey(dataGridViewIBET, key);
                    if (index > -1)
                    {
                        o.key = key;
                        o.group = dataGridViewIBET["group", index].Value.ToString().Trim();
                        o.username = dataGridViewIBET["username", index].Value.ToString().Trim();
                        o.usd = dataGridViewIBET["usd", index].Value.ToString().Trim();
                        o.credit = dataGridViewIBET["credit", index].Value.ToString().Trim();
                        o.profit = dataGridViewIBET["profit", index].Value.ToString().Trim();
                        o.league = dataGridViewIBET["league", index].Value.ToString().Trim();
                        o.money = dataGridViewIBET["money", index].Value.ToString().Trim();
                        o.mode = dataGridViewIBET["mode", index].Value.ToString().Trim();
                        o.maxbet= dataGridViewIBET["maxbet", index].Value.ToString().Trim();
                    }
                }
                else
                {
                    index = findRowByKey(dataGridViewSBO, key);
                    if (index > -1)
                    {
                        o.key = key;
                        o.group = dataGridViewSBO["group", index].Value.ToString().Trim();
                        o.username = dataGridViewSBO["username", index].Value.ToString().Trim();
                        o.usd = dataGridViewSBO["usd", index].Value.ToString().Trim();
                        o.credit = dataGridViewSBO["credit", index].Value.ToString().Trim();
                        o.profit = dataGridViewSBO["profit", index].Value.ToString().Trim();
                        o.league = dataGridViewSBO["league", index].Value.ToString().Trim();
                        o.money = dataGridViewSBO["money", index].Value.ToString().Trim();
                        o.mode = dataGridViewSBO["mode", index].Value.ToString().Trim();
                        o.maxbet = dataGridViewSBO["maxbet", index].Value.ToString().Trim();
                    }
                }
            }
            catch
            {
                return null;
            }
            return o;
        }

        bool BetNow = false;
        private void bt_BetNow_Click(object sender, EventArgs e)
        {
            bt_BetNow.Visible = false;
            BetNow = true;
        }

        private int findRowByKey(DataGridView drv, string key)
        {
            int rowIndex = -1;
            try
            {
                DataGridViewRow row = drv.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["key"].Value.ToString().Equals(key))
                        .First();
                rowIndex = row.Index;
            }
            catch
            {
                return -1;
            }
            return rowIndex;
        }
        int nchienthuat = -1;
        private void richChienThuat_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                nchienthuat += 1;
                richChienThuat.Text = data_chienthuat.Split(new string[] { "\r\n\r\n" },StringSplitOptions.None) [nchienthuat];
            }
            catch
            {
                nchienthuat = 0;
                richChienThuat.Text = data_chienthuat.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None)[nchienthuat];
            }
        }

        private void bt_BetIbet_Click(object sender, EventArgs e)
        {
            string infobet_Ibet = tb_BetIbet.Text;
            string str_BetIbet = infobet_Ibet.Split('|')[0];
            string money = infobet_Ibet.Split('|')[1];
            string IsLive = infobet_Ibet.Split('|')[2];
            string phieuchung = infobet_Ibet.Split('|')[3];
            string keyibet = "ibet" + infobet_Ibet.Split('|')[4];
            string group = infobet_Ibet.Split('|')[5];
            ticket oibet = UtilSoccer.convertStringToTicket(str_BetIbet);
            if(IsLive=="0")
            {
                BetManager.getBet(keyibet).playBetNonLive(oibet, money, IsLive, phieuchung, group);
            }
            else
            {
                BetManager.getBet(keyibet).playBetLive(oibet, money, IsLive, phieuchung, group);
            }
            string Mes = BetManager.getBet(keyibet).getMessage();
            updateMessage(keyibet, Mes);
            if (Mes.Contains("(OK)"))
            {
                richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] Bet Success          [{1}]\n", keyibet, DateTime.Now.ToLongTimeString()));
                updateCredit(keyibet, BetManager.getBet(keyibet).getCredit().Split('/')[1]);
                player.SoundLocation = "../../../Sound/success_ibet.wav";
                player.Play();
            }
            else
            {
                updateMessage(keyibet, "BET FAIL");
            }
        }

        private void bt_BetSbo_Click(object sender, EventArgs e)
        {
            string infobet_Sbo = tb_BetSbo.Text;
            string str_BetSbo = infobet_Sbo.Split('|')[0];
            string money = infobet_Sbo.Split('|')[1];
            string IsLive = infobet_Sbo.Split('|')[2];
            string phieuchung = infobet_Sbo.Split('|')[3];
            string keysbo = "sbo" + infobet_Sbo.Split('|')[4];
            string group = infobet_Sbo.Split('|')[5];
            ticket osbo = UtilSoccer.convertStringToTicket(str_BetSbo);
            BetManager.getBet(keysbo).getOddChange(osbo, money, IsLive);
            if (IsLive == "0")
            {
                BetManager.getBet(keysbo).playBetNonLive(osbo, money, IsLive, phieuchung, group);
            }
            else
            {
                BetManager.getBet(keysbo).playBetLive(osbo, money, IsLive, phieuchung, group);
            }
            string Mes = BetManager.getBet(keysbo).getMessage();
            updateMessage(keysbo, Mes);
            if (Mes.Contains("(OK)"))
            {
                richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] Bet Success          [{1}]\n", keysbo, DateTime.Now.ToLongTimeString()));
                updateCredit(keysbo, BetManager.getBet(keysbo).getCredit().Split('/')[1]);
                player.SoundLocation = "../../../Sound/success_sbo.wav";
                player.Play();
            }
            else
            {
                updateMessage(keysbo, "BET FAIL");
            }
        }

        private void lb_CheckLogin_Click(object sender, EventArgs e)
        {
            int deviationOdd = UtilSoccer.getDeviationOdd(-0.98, 0.97);
            MessageBox.Show(deviationOdd.ToString());
        }

        private bool findkeyExits(string key)
        {
            bool result = false;
            string str_chienthuat = richChienThuat.Text;
            foreach(string chienthuat in str_chienthuat.Split('\n'))
            {
                if (chienthuat.Contains("@"))
                {
                    continue;
                }
                if (chienthuat!="")
                {
                    string str_keybet_ibet = chienthuat.Split('/')[0];
                    string str_keybet_sbo = chienthuat.Split('/')[1];
                    if (key.Contains("ibet"))
                    {
                        foreach(string keybet_ibet in str_keybet_ibet.Split(','))
                        {
                            if(keybet_ibet==key.Replace("ibet",""))
                            {
                                result = true;
                                return result;
                            }
                        }
                    }
                    else
                    {
                        foreach (string keybet_sbo in str_keybet_sbo.Split(','))
                        {
                            if (keybet_sbo == key.Replace("sbo", ""))
                            {
                                result = true;
                                return result;
                            }
                        }
                    }
                }
            }
            
            return result;
        }

        private void bt_delbetlist_Click(object sender, EventArgs e)
        {
            db.DeleteTicket();
            //change_strategy
            player.SoundLocation = "../../../Sound/delbetlist.wav";
            player.Play();
        }

        bool stop = false;
        private void btnLoginSBO_Click(object sender, EventArgs e)
        {
            btnLoginSBO.Enabled = false;
            tSbo = new Thread(delegate ()
            {
                picSBO.Enabled = true;
                int i = 0;
                foreach (DataRow dr in dtSBO.Rows)
                {
                    if (stop)
                    {
                        stop = false;
                        richNote.Text = "STOP";
                        picIBET.Enabled = false;
                        picSBO.Enabled = false;
                        btnLoginIBET.Enabled = true;
                        btnLoginSBO.Enabled = true;
                        btnBetLive.Text = "BET LIVE";
                        btnBet.Text = "BET NON LIVE";
                        SendMessageToTelegram("STOP");
                        return;
                    }
                    if (findkeyExits(dr["key"].ToString()))
                    {
                        BetManager.createBet("sbo", dr["key"].ToString(), dr["ip"].ToString(), dr["username"].ToString(), dr["usd"].ToString());
                        BetManager.getBet(dr["key"].ToString()).login();
                        dr["gcredit"] = BetManager.getBet(dr["key"].ToString()).getCredit().Split('/')[0];
                        dr["credit"] = BetManager.getBet(dr["key"].ToString()).getCredit().Split('/')[1];
                        dr["message"] = BetManager.getBet(dr["key"].ToString()).getMessage();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        try
                        {
                            dataGridViewSBO.Rows[i].Visible = false;
                        }
                        catch { };
                    }
                    i += 1;
                }
                picSBO.Enabled = false;
                richNote.Text = richNote.Text.Insert(0, "Login Sbo Success\n");
            });
            tSbo.Start();
        }
        private void btnLoginIBET_Click(object sender, EventArgs e)
        {
            btnLoginIBET.Enabled = false;
            tIbet = new Thread(delegate ()
            {
                picIBET.Enabled = true;
                int i = 0;
                foreach (DataRow dr in dtIBET.Rows)
                {
                    if (stop)
                    {
                        stop = false;
                        richNote.Text = "STOP";
                        picIBET.Enabled = false;
                        picSBO.Enabled = false;
                        btnLoginIBET.Enabled = true;
                        btnLoginSBO.Enabled = true;
                        btnBetLive.Text = "BET LIVE";
                        btnBet.Text = "BET NON LIVE";
                        SendMessageToTelegram("STOP");
                        return;
                    }
                    if (findkeyExits(dr["key"].ToString()))
                    {
                        BetManager.createBet("ibet", dr["key"].ToString(), dr["ip"].ToString(), dr["username"].ToString(), dr["usd"].ToString());
                        BetManager.getBet(dr["key"].ToString()).login();
                        dr["gcredit"] = BetManager.getBet(dr["key"].ToString()).getCredit().Split('/')[0];
                        dr["credit"] = BetManager.getBet(dr["key"].ToString()).getCredit().Split('/')[1];
                        dr["message"] = BetManager.getBet(dr["key"].ToString()).getMessage();
                        if(dr["message"].ToString()!= "LOGIN SUCCESS")
                        {
                            try
                            {
                                dataGridViewIBET.Rows[i].Visible = false;
                                remove_chienthuat(dr["key"].ToString());
                            }
                            catch { };
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        try
                        {
                            dataGridViewIBET.Rows[i].Visible = false;
                        }
                        catch { };
                    }
                    i += 1;
                }
                picIBET.Enabled = false;
                richNote.Text = richNote.Text.Insert(0,"Login Ibet Success\n");
                //Thread.Sleep(5000);
                //string[] mangchienthuat = richChienThuat.Text.Split('\n');
                //foreach (string chienthuat in mangchienthuat)
                //{
                //    if (chienthuat.Contains("/"))
                //    {
                //        foreach (string keyibet in chienthuat.Split('/')[0].Split(','))
                //        {
                //            string ibetlogin = "ibet" + keyibet;
                //            if (BetManager.getBet(ibetlogin).checklogin())
                //            {
                //                updateMessage(ibetlogin, "CHECK LOGIN SUCCESS");
                //            }
                //            else
                //            {
                //                updateMessage(ibetlogin, "LOG OUT");
                //                remove_chienthuat(ibetlogin);
                //            }
                //            Thread.Sleep(1000);
                //        }
                //    }
                //}
            });
            tIbet.Start();
        }

        private void updateCompare()
        {
            DataTable dtCompareAccount = db.getCompareAccount();
            foreach (DataRow dr in dtCompareAccount.Rows)
            {
                try
                {
                    if (dr["id"].ToString() == "a,a")
                    {
                        str_CompareNonLive_AA = dr["CompareNonLive"].ToString();
                        str_CompareLive_AA = dr["CompareLive"].ToString();
                    }

                }
                catch (Exception)
                {
                }
            }
        }
        private void btnBetLive_Click(object sender, EventArgs e)
        {
            int ncheck = 0;
            bt_BetNow.Visible = false;
            bool AutoAccept = false;
            tBetLive = new Thread(delegate ()
            {
                btnBetLive.Text = "STOP";
                bool wait = false;
                int count_delay = 0;
                if (btnLoginIBET.Enabled)
                {
                    btnLoginIBET.PerformClick();
                    Thread.Sleep(1000);
                    while (picIBET.Enabled)
                    {
                        if (stop)
                        {
                            stop = false;
                            richNote.Text = "STOP";
                            picIBET.Enabled = false;
                            picSBO.Enabled = false;
                            btnLoginIBET.Enabled = true;
                            btnLoginSBO.Enabled = true;
                            btnBetLive.Text = "BET LIVE";
                            btnBet.Text = "BET NON LIVE";
                            SendMessageToTelegram("STOP");
                            return;
                        }
                        Thread.Sleep(1000);
                        count_delay += 1;
                    }
                    btnLoginSBO.PerformClick();
                    Thread.Sleep(1000);
                    while (picSBO.Enabled)
                    {
                        if (stop)
                        {
                            stop = false;
                            richNote.Text = "STOP";
                            picIBET.Enabled = false;
                            picSBO.Enabled = false;
                            btnLoginIBET.Enabled = true;
                            btnLoginSBO.Enabled = true;
                            btnBetLive.Text = "BET LIVE";
                            btnBet.Text = "BET NON LIVE";
                            SendMessageToTelegram("STOP");
                            return;
                        }
                        Thread.Sleep(1000);
                        count_delay += 1;
                    }
                    if (richChienThuat.Text.Contains(" l") || richChienThuat.Text.Contains(" w"))
                    {
                        Thread.Sleep(60000);
                    }
                    player.SoundLocation = "../../../Sound/LoginSuccess.wav";
                    player.Play();
                    while (count_delay < 40)
                    {
                        if (stop)
                        {
                            stop = false;
                            richNote.Text = "STOP";
                            picIBET.Enabled = false;
                            picSBO.Enabled = false;
                            btnLoginIBET.Enabled = true;
                            btnLoginSBO.Enabled = true;
                            btnBetLive.Text = "BET LIVE";
                            btnBet.Text = "BET NON LIVE";
                            SendMessageToTelegram("STOP");
                            return;
                        }
                        Thread.Sleep(1000);
                        count_delay += 1;
                        richNote.Text = (120 - count_delay).ToString() + " ....";
                    }
                    richNote.Text = "START...";
                }
                while (true)
                {
                    if (stop)
                    {
                        stop = false;
                        richNote.Text = "STOP";
                        picIBET.Enabled = false;
                        picSBO.Enabled = false;
                        btnLoginIBET.Enabled = true;
                        btnLoginSBO.Enabled = true;
                        btnBetLive.Text = "BET LIVE";
                        btnBet.Text = "BET NON LIVE";
                        SendMessageToTelegram("STOP");
                        return;
                    }
                    updateCompare();
                    string[] mangchienthuat = richChienThuat.Text.Split('\n');
                    List<objCompare> lstCompare = UtilSoccer.convertCompareToList(str_CompareLive_AA);
                    #region checklogin
                    if (ncheck > 60)
                    {
                        ncheck = 0;
                        foreach (string ct in mangchienthuat)
                        {
                            if (ct.Contains("/"))
                            {
                                foreach (string keyibet in ct.Split('/')[0].Split(','))
                                {
                                    string ibetlogin = "ibet" + keyibet;
                                    if (BetManager.getBet(ibetlogin).checklogin())
                                    {
                                        updateMessage(ibetlogin, "LOGIN SUCCESS");
                                    }
                                    else
                                    {
                                        remove_chienthuat(ibetlogin);
                                    }
                                    Thread.Sleep(100);
                                }
                                foreach (string keysbo in ct.Split('/')[1].Split(','))
                                {
                                    string sbologin = "sbo" + keysbo;
                                    if (BetManager.getBet(sbologin).checklogin())
                                    {
                                        updateMessage(sbologin, "LOGIN SUCCESS");
                                    }
                                    else
                                    {
                                        remove_chienthuat(sbologin);
                                    }
                                    Thread.Sleep(100);
                                }
                            }
                        }
                    }
                    #endregion
                    if (lstCompare.Count == 0)
                    {
                        if (!wait)
                        {
                            richNote.Text = richNote.Text.Insert(0, "NO COMPARE LIVE\n");
                            wait = true;
                        }
                        ncheck += 1;
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        wait = false;
                    }

                    #region chienthuat
                    foreach (string chienthuat in mangchienthuat)
                    {
                        if (stop)
                        {
                            stop = false;
                            richNote.Text = "STOP";
                            picIBET.Enabled = false;
                            picSBO.Enabled = false;
                            btnLoginIBET.Enabled = true;
                            btnLoginSBO.Enabled = true;
                            btnBetLive.Text = "BET LIVE";
                            btnBet.Text = "BET NON LIVE";
                            SendMessageToTelegram("STOP");
                            return;
                        }
                        
                        if (chienthuat.Trim() == "" || chienthuat.Contains("@")) continue;
                        lb_CheckLogin.Text = ncheck.ToString();
                        bool betsameagent = false;
                        int nibet = chienthuat.Split('/')[0].Split(',').Length;
                        int nsbo = chienthuat.Split('/')[1].Split(',').Length;
                        string keyIbet = "ibet" + chienthuat.Split('/')[0].Split(',')[ran.Next(0, nibet)];
                        string keySbo = "sbo" + chienthuat.Split('/')[1].Split(',')[ran.Next(0, nsbo)];
                        string capbet = keyIbet + "," + keySbo;
                        if (chienthuat.Split('/').Length > 2) betsameagent = true;
                        string message_chienthuat = "";
                        info infoIbet = getInfoByKey(keyIbet);
                        info infoSbo = getInfoByKey(keySbo);

                        if (infoIbet.credit == null)
                        {
                            richNote.Text = richNote.Text.Insert(0, string.Format("[{0}] is null\n", keyIbet));
                            continue;
                        }
                        if (infoSbo.credit == null)
                        {
                            richNote.Text = richNote.Text.Insert(0, string.Format("[{0}] is null\n", keySbo));
                            continue;
                        }


                        string money = getMoneyBet(infoSbo.credit.Replace(",", ""), infoIbet.credit.Replace(",", ""), infoSbo.maxbet, infoIbet.maxbet, infoSbo.money, infoIbet.money, infoSbo.usd, infoIbet.usd);
                        if (money.Contains("ibet"))
                        {
                            remove_chienthuat(keyIbet);
                            updateMessage(keyIbet, "CREDIT...");
                        }
                        if (money.Contains("sbo"))
                        {
                            remove_chienthuat(keySbo);
                            updateMessage(keySbo, "CREDIT...");
                        }
                        if (money.Contains("sbo") || money.Contains("ibet")) continue;

                        string str_group = infoSbo.group + "," + infoIbet.group;
                        updateCompare();
                        foreach (objCompare oCompare in lstCompare)
                        {
                            int profit = Convert.ToInt32(Math.Abs(oCompare.profit * 100));
                            int league = Convert.ToInt32(Math.Abs(oCompare.league * 100));

                            if (profit > Int32.Parse(infoIbet.profit))
                            {
                                message_chienthuat += string.Format("[{0}] profit is {1} is great than {2}\n", capbet, profit, infoIbet.profit);
                                continue;
                            }
                            else if (int.Parse(infoIbet.league) < league)
                            {
                                message_chienthuat += string.Format("[{0}] league compare({1})/league bet({2})\n", capbet, league, infoIbet.league);
                                continue;
                            }
                            else
                            {
                                string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                                string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;

                                bool betIbet = true;
                                bool betSbo = true;

                                ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                                ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);
                                if (db.oddExists(oIbet))
                                {
                                    message_chienthuat += string.Format("[{0}] odd list exists\n", capbet);
                                    continue;
                                }
                                if (db.oddExists(oSbo))
                                {
                                    message_chienthuat += string.Format("[{0}] odd list exists\n", capbet);
                                    continue;
                                }
                                if (db.ticketExists(oIbet, infoIbet.username) == true || db.ticketExists(oSbo, infoSbo.username) == true)
                                {
                                    message_chienthuat += string.Format("[{0}] bet list exists\n", capbet);
                                    continue;
                                }
                                #region Check Mode
                                string modeBet = getModeBet(infoIbet.mode, infoSbo.mode);
                                if (modeBet.ToLower() == "sw")
                                {
                                    if (oSbo.odd < 0)
                                        continue;
                                }
                                else if (modeBet.ToLower() == "iw")
                                {
                                    if (oIbet.odd < 0)
                                        continue;
                                }
                                else if (modeBet.ToLower() == "w")
                                {
                                    if (oSbo.odd < 0 && oIbet.odd > 0)
                                    {
                                        betIbet = true;
                                        betSbo = false;
                                        money = getMoneyBet("10000", infoIbet.credit.Replace(",", ""), "10000", infoIbet.maxbet, "10000,10", infoIbet.money, infoSbo.usd, infoIbet.usd);
                                    }
                                    else if (oSbo.odd > 0 && oIbet.odd < 0)
                                    {
                                        betIbet = false;
                                        betSbo = true;
                                        getMoneyBet(infoSbo.credit.Replace(",", ""), "10000", infoSbo.maxbet, "10000", infoSbo.money, "10000,10", infoSbo.usd, infoIbet.usd);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else if (modeBet.ToLower() == "l")
                                {
                                    if (oSbo.odd < 0 && oIbet.odd > 0)
                                    {
                                        betIbet = false;
                                        betSbo = true;
                                        getMoneyBet(infoSbo.credit.Replace(",", ""), "10000", infoSbo.maxbet, "10000", infoSbo.money, "10000,10", infoSbo.usd, infoIbet.usd);
                                    }
                                    else if (oSbo.odd > 0 && oIbet.odd < 0)
                                    {
                                        betIbet = true;
                                        betSbo = false;
                                        money = getMoneyBet("10000", infoIbet.credit.Replace(",", ""), "10000", infoIbet.maxbet, "10000,10", infoIbet.money, infoSbo.usd, infoIbet.usd);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                #endregion

                                string oddbetibet = "", oddbetsbo = "";
                                if (db.oddExists(oIbet) == true && betsameagent == false)
                                {
                                    message_chienthuat += string.Format("[{0}] odd list exists\n", capbet);
                                    continue;
                                }
                                if (betIbet && (oIbet.bettype == "1" || oIbet.bettype == "7") && (oIbet.odd < 0 || oIbet.odd > 0.8) && (oIbet.hdp > 0.5 && oIbet.choose == "a" || oIbet.hdp < -0.5 && oIbet.choose == "h"))
                                {
                                    #region BetIbet
                                    string phieuchung = "";
                                    if (betIbet)
                                    {
                                        BetManager.getBet(keyIbet).playBetLive(oIbet, money, "1", "", str_group, AutoAccept);
                                        string Mes = BetManager.getBet(keyIbet).getMessage();
                                        updateMessage(keyIbet, Mes);
                                        if (Mes.Contains("(OK)"))
                                        {
                                            bt_BetNow.Visible = true;
                                            oddbetibet = oIbet.odd.ToString();
                                            richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keyIbet, infoIbet.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                            updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit().Split('/')[1]);
                                            richNote.Text = richNote.Text.Insert(0, string.Format("Waitting Bet {0}\n", keySbo));
                                            lb_Ibet.Text = string.Format("{1}", infoIbet.username, Mes.Replace("(OK)", ""));
                                            player.SoundLocation = "../../../Sound/success_ibet.wav";
                                            player.Play();
                                        }//Odd Down
                                        else if (Mes.Contains("Odd Down"))
                                        {
                                            updateMessage(keyIbet, Mes);
                                            break;
                                        }//ErrorCode
                                        else if (Mes.Contains("ErrorCode"))
                                        {
                                            remove_chienthuat(keyIbet);
                                            updateMessage(keyIbet, "ErrorCode");
                                            btnBetLive.Text = "BET LIVE";
                                            SendMessageToTelegram("ErrorCode");
                                            return;
                                        }
                                        if (betSbo == false)
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, IBET: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                            Thread.Sleep(2000);
                                            richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                            break;
                                        }
                                        else
                                        {
                                            phieuchung = mylib.generateID("T");
                                        }
                                    }
                                    else
                                    {
                                        phieuchung = mylib.generateID("T");
                                    }

                                    if (betSbo)
                                    {
                                        string realmoney = money;
                                        if (betIbet)
                                            realmoney = BetManager.getBet(keyIbet).getRealMoney();
                                        while (true)
                                        {
                                            oddbetsbo = BetManager.getBet(keySbo).getOddChange(oSbo, money, "1");
                                            if (oddbetsbo == "e")
                                            {
                                                tb_BetSbo.Text = str_BetSbo + "|" + money + "|" + "1" + "|" + phieuchung + "|";
                                                Console.Beep(5000, 5000);
                                                richNote.Text = richNote.Text.Insert(0, "Sbo Error\n");
                                                SendMessageToTelegram("Sbo Error");
                                                return;
                                            }
                                            lb_OddLive.Text = oddbetsbo;
                                            double profilive = UtilSoccer.profit_odd(oddbetibet, oddbetsbo);
                                            if (profilive >= -0.03 || BetNow)
                                            {
                                                BetManager.getBet(keySbo).playBetLive(oSbo, realmoney, "1", phieuchung, str_group);
                                                string Mes = BetManager.getBet(keySbo).getMessage();
                                                updateMessage(keySbo, Mes);
                                                if (Mes.Contains("(OK)"))
                                                {
                                                    bt_BetNow.Visible = false;
                                                    lb_OddLive.Text = "-";
                                                    BetNow = false;
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keySbo, infoSbo.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                                    updateCredit(keySbo, BetManager.getBet(keySbo).getCredit().Split('/')[1]);
                                                    lb_Sbo.Text = string.Format("{1}", infoSbo.username, Mes.Replace("(OK)", ""));
                                                    player.SoundLocation = "../../../Sound/success_sbo.wav";
                                                    player.Play();
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, IBET: {4}, SBO: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                    break;
                                                }
                                                else
                                                {
                                                    tb_BetSbo.Text = str_BetSbo + "|" + money + "|" + "1" + "|" + phieuchung + "|";
                                                    Console.Beep(5000, 5000);
                                                    richNote.Text = richNote.Text.Insert(0, "Sbo Bet Fail\n");
                                                    btnBetLive.Text = "BET LIVE";
                                                    SendMessageToTelegram("Sbo Bet Fail");
                                                    return;
                                                }
                                            }
                                            Thread.Sleep(10000);
                                            ncheck += 10;
                                            #region checklogin
                                            if (ncheck > 60)
                                            {
                                                ncheck = 0;
                                                foreach (string ct in mangchienthuat)
                                                {
                                                    if (ct.Contains("/"))
                                                    {
                                                        foreach (string keyibet in ct.Split('/')[0].Split(','))
                                                        {
                                                            string ibetlogin = "ibet" + keyibet;
                                                            if (BetManager.getBet(ibetlogin).checklogin())
                                                            {
                                                                updateMessage(ibetlogin, "LOGIN SUCCESS");
                                                            }
                                                            else
                                                            {
                                                                remove_chienthuat(ibetlogin);
                                                            }
                                                            Thread.Sleep(100);
                                                        }
                                                        foreach (string keysbo in ct.Split('/')[1].Split(','))
                                                        {
                                                            string sbologin = "sbo" + keysbo;
                                                            if (BetManager.getBet(sbologin).checklogin())
                                                            {
                                                                updateMessage(sbologin, "LOGIN SUCCESS");
                                                            }
                                                            else
                                                            {
                                                                remove_chienthuat(sbologin);
                                                            }
                                                            Thread.Sleep(100);
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                        if (bt_BetNow.Visible == false) break;
                                    }
                                    #endregion
                                }
                                else if (betSbo && (oIbet.bettype == "1" || oIbet.bettype == "7" || int.Parse(money) <= 3000) && (oSbo.odd < 0 || oSbo.odd > 0.8) && (oSbo.hdp > 0.5 && oSbo.choose == "a" || oSbo.hdp < -0.5 && oSbo.choose == "h"))
                                {
                                    #region BetSbo

                                    string phieuchung = "";
                                    if (betSbo)
                                    {
                                        oddbetsbo = BetManager.getBet(keySbo).getOddChange(oSbo, money, "1");
                                        //if (double.Parse(oddbetsbo) < oSbo.odd) continue;
                                        BetManager.getBet(keySbo).playBetLive(oSbo, money, "1", "", str_group,AutoAccept);
                                        string Mes = BetManager.getBet(keySbo).getMessage();
                                        updateMessage(keySbo, Mes);
                                        if (Mes.Contains("(OK)"))
                                        {
                                            bt_BetNow.Visible = true;
                                            oddbetsbo = BetManager.getBet(keySbo).getOddChange(oSbo, money, "1");
                                            richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keySbo, infoSbo.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                            updateCredit(keySbo, BetManager.getBet(keySbo).getCredit().Split('/')[1]);
                                            richNote.Text = richNote.Text.Insert(0, string.Format("Waitting Bet {0}\n", keyIbet));
                                            lb_Sbo.Text = string.Format("{1}", infoSbo.username, Mes.Replace("(OK)", ""));
                                            player.SoundLocation = "../../../Sound/success_sbo.wav";
                                            player.Play();
                                        }
                                        else
                                        {
                                            player.SoundLocation = "../../../Sound/bet_fall.wav";
                                            player.Play();
                                            richNote.Text = richNote.Text.Insert(0, "Sbo Bet Fail\n");
                                            btnBetLive.Text = "BET LIVE";
                                            break;
                                        }
                                        if (betIbet == false)
                                        {
                                            richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, SBO: {5}\n", capbet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoSbo.username, infoSbo.username));
                                            Thread.Sleep(2000);
                                            richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                            break;
                                        }
                                        else
                                        {
                                            phieuchung = mylib.generateID("T");
                                        }
                                    }
                                    else
                                    {
                                        phieuchung = mylib.generateID("T");
                                    }

                                    if (betIbet)
                                    {
                                        string realmoney = money;
                                        //if (betIbet)
                                        //    realmoney = BetManager.getBet(keySbo).getRealMoney();

                                        while (true)
                                        {
                                            oddbetibet = BetManager.getBet(keyIbet).getOddChange(oIbet, realmoney, "1");
                                            if (oddbetibet == "e")
                                            {
                                                tb_BetIbet.Text = str_BetIbet + "|" + money + "|" + "1" + "|" + phieuchung + "|";
                                                Console.Beep(5000, 5000);
                                                richNote.Text = richNote.Text.Insert(0, "Ibet Error\n");
                                                btnBetLive.Text = "BET LIVE";
                                                SendMessageToTelegram("Ibet Error");
                                                return;
                                            }
                                            lb_OddLive.Text = oddbetibet;
                                            double profilive = UtilSoccer.profit_odd(oddbetibet, oddbetsbo);
                                            if (profilive >= -0.03 || BetNow)
                                            {
                                                Thread.Sleep(3000);
                                                BetManager.getBet(keyIbet).playBetLive(oIbet, realmoney, "1", phieuchung, str_group);
                                                string Mes = BetManager.getBet(keyIbet).getMessage();
                                                updateMessage(keyIbet, Mes);
                                                if (Mes.Contains("(OK)"))
                                                {
                                                    bt_BetNow.Visible = false;
                                                    lb_OddLive.Text = "-";
                                                    BetNow = false;
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keyIbet, infoIbet.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                                    updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit().Split('/')[1]);
                                                    player.SoundLocation = "../../../Sound/success_ibet.wav";
                                                    lb_Ibet.Text = string.Format("{1}", infoIbet.username, Mes.Replace("(OK)", ""));
                                                    player.Play();
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, IBET: {4}, SBO: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                    break;
                                                }//Odd Down
                                                else if (Mes.Contains("Odd Down"))
                                                {
                                                    tb_BetIbet.Text = str_BetIbet + "|" + money + "|" + "1" + "|" + phieuchung + "|";
                                                    updateMessage(keyIbet, Mes);
                                                    Console.Beep(5000, 5000);
                                                    btnBetLive.Text = "BET LIVE";
                                                    SendMessageToTelegram("Ibet Error");
                                                    return;
                                                }//ErrorCode
                                                else if (Mes.Contains("ErrorCode"))
                                                {
                                                    tb_BetIbet.Text = str_BetIbet + "|" + money + "|" + "1" + "|" + phieuchung + "|";
                                                    remove_chienthuat(keyIbet);
                                                    Console.Beep(5000, 5000);
                                                    updateMessage(keyIbet, "ErrorCode");
                                                    btnBetLive.Text = "BET LIVE";
                                                    SendMessageToTelegram("ErrorCode");
                                                    return;
                                                }
                                            }
                                            Thread.Sleep(10000);
                                            ncheck += 10;
                                            #region checklogin
                                            if (ncheck > 60)
                                            {
                                                ncheck = 0;
                                                foreach (string ct in mangchienthuat)
                                                {
                                                    if (ct.Contains("/"))
                                                    {
                                                        foreach (string keyibet in ct.Split('/')[0].Split(','))
                                                        {
                                                            string ibetlogin = "ibet" + keyibet;
                                                            if (BetManager.getBet(ibetlogin).checklogin())
                                                            {
                                                                updateMessage(ibetlogin, "LOGIN SUCCESS");
                                                            }
                                                            else
                                                            {
                                                                remove_chienthuat(ibetlogin);
                                                            }
                                                            Thread.Sleep(100);
                                                        }
                                                        foreach (string keysbo in ct.Split('/')[1].Split(','))
                                                        {
                                                            string sbologin = "sbo" + keysbo;
                                                            if (BetManager.getBet(sbologin).checklogin())
                                                            {
                                                                updateMessage(sbologin, "LOGIN SUCCESS");
                                                            }
                                                            else
                                                            {
                                                                remove_chienthuat(sbologin);
                                                            }
                                                            Thread.Sleep(100);
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                        if (bt_BetNow.Visible == false) break;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    richNote.Text = richNote.Text.Insert(0, string.Format("[{0}] Not Fit\n", capbet));
                                }
                            }
                        }
                        richNote.Text = richNote.Text.Insert(0, message_chienthuat);
                        Thread.Sleep(1000);
                        ncheck += 1;

                    }
                    Thread.Sleep(1000);
                    ncheck += 1;
                    #endregion
                    if (!cb_Loop.Checked)
                    {
                        richLog.Text = richLog.Text.Insert(0, "BET LIVE COMPLETE\n");
                        btnBetLive.Text = "BET LIVE";
                        break;
                    }

                }
            });
            if (btnBetLive.Text == "STOP")
            {
                btnBetLive.Text = "BET LIVE";
                File.AppendAllText(@"../../../richLog.txt", richLog.Text);
                File.AppendAllText(@"../../../richNote.txt", richNote.Text);
                stop = true;
            }
            else
            {
                stop = false;
                tBetLive.Start();
                int i = 0;
                foreach (DataRow dr in dtIBET.Rows)
                {
                    dataGridViewIBET.Rows[i].Visible = true;
                    i += 1;
                }
                i = 0;
                foreach (DataRow dr in dtSBO.Rows)
                {
                    dataGridViewSBO.Rows[i].Visible = true;
                    i += 1;
                }
            }
        }

        private void btnBET_Click(object sender, EventArgs e)
        {
            int ncheck = 0;
            bool AutoAccept = true;
            SendMessageToTelegram("BET NONLIVE");
            tBetNonLive = new Thread(delegate ()
            {

                btnBet.Text = "STOP";
                bool wait = false;
                int count_delay = 0;
                try
                {
                    #region Login
                    if (btnLoginIBET.Enabled)
                    {
                        btnLoginIBET.PerformClick();
                        Thread.Sleep(1000);
                        while (picIBET.Enabled)
                        {
                            Thread.Sleep(1000);
                        }
                        count_delay = 60;
                        btnLoginSBO.PerformClick();
                        Thread.Sleep(1000);
                        while (picSBO.Enabled)
                        {
                            if (stop)
                            {
                                stop = false;
                                richNote.Text = "STOP";
                                picIBET.Enabled = false;
                                picSBO.Enabled = false;
                                btnLoginIBET.Enabled = true;
                                btnLoginSBO.Enabled = true;
                                btnBetLive.Text = "BET LIVE";
                                btnBet.Text = "BET NON LIVE";
                                SendMessageToTelegram("STOP");
                                return;
                            }
                            Thread.Sleep(1000);
                            count_delay -= 1;
                        }
                        if (richChienThuat.Text.Contains(" l") || richChienThuat.Text.Contains(" w"))
                        {
                            Thread.Sleep(60000);
                        }
                        player.SoundLocation = "../../../Sound/LoginSuccess.wav";
                        player.Play();

                        while (count_delay>0)
                        {
                            if (stop)
                            {
                                stop = false;
                                richNote.Text = "STOP";
                                picIBET.Enabled = false;
                                picSBO.Enabled = false;
                                btnLoginIBET.Enabled = true;
                                btnLoginSBO.Enabled = true;
                                btnBetLive.Text = "BET LIVE";
                                btnBet.Text = "BET NON LIVE";
                                SendMessageToTelegram("STOP");
                                return;
                            }
                            Thread.Sleep(1000);
                            count_delay -= 1;
                            richNote.Text = count_delay.ToString() + " ....";
                        }

                        richNote.Text = "START...";
                    }
                    #endregion
                }
                catch
                {
                    SendMessageToTelegram("LOGIN ERROR");
                    Thread.Sleep(2000);
                    Console.Beep(2000, 10000);
                }
                try
                {
                    #region Start
                    while (true)
                    {
                        if (stop)
                        {
                            stop = false;
                            richNote.Text = "STOP";
                            picIBET.Enabled = false;
                            picSBO.Enabled = false;
                            btnLoginIBET.Enabled = true;
                            btnLoginSBO.Enabled = true;
                            btnBetLive.Text = "BET LIVE";
                            btnBet.Text = "BET NON LIVE";
                            SendMessageToTelegram("STOP " + richChienThuat.Text.Split('\n')[0]);
                            return;
                        }
                        updateCompare();
                        string[] mangchienthuat = richChienThuat.Text.Split('\n');
                        List<objCompare> lstCompare = UtilSoccer.convertCompareToList(str_CompareNonLive_AA);
                        #region checklogin
                        if (ncheck > 30)
                        {
                            ncheck = 0;
                            foreach (string chienthuat in mangchienthuat)
                            {
                                if (chienthuat.Contains("/"))
                                {
                                    foreach (string keyibet in chienthuat.Split('/')[0].Split(','))
                                    {
                                        string ibetlogin = "ibet" + keyibet;
                                        if (BetManager.getBet(ibetlogin).checklogin())
                                        {
                                            updateMessage(ibetlogin, "LOGIN SUCCESS");
                                        }
                                        else
                                        {
                                            updateMessage(ibetlogin, "LOG OUT");
                                            remove_chienthuat(ibetlogin);
                                        }
                                        Thread.Sleep(100);
                                    }
                                    foreach (string keysbo in chienthuat.Split('/')[1].Split(','))
                                    {
                                        string sbologin = "sbo" + keysbo;
                                        if (BetManager.getBet(sbologin).checklogin())
                                        {
                                            updateMessage(sbologin, "LOGIN SUCCESS");
                                        }
                                        else
                                        {
                                            remove_chienthuat(sbologin);
                                        }
                                        Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                        #endregion
                        if (lstCompare.Count == 0)
                        {
                            if (!wait)
                            {
                                richNote.Text = richNote.Text.Insert(0, "NO COMPARE NONLIVE\n");
                                wait = true;
                            }
                            ncheck += 1;
                            Thread.Sleep(1000);
                            continue;
                        }
                        else
                        {
                            wait = false;
                        }

                        #region chienthuat
                        foreach (string chienthuat in mangchienthuat)
                        {
                            if (ncheck > 30) break;
                            if (mangchienthuat.Length <= 2)
                            {
                                SendMessageToTelegram("STOP " + richChienThuat.Text.Split('\n')[0]);
                                btnBet.PerformClick();
                                Thread.Sleep(2000);
                                try
                                {
                                    nchienthuat += 1;
                                    richChienThuat.Text = data_chienthuat.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None)[nchienthuat];
                                }
                                catch
                                {
                                    nchienthuat = 0;
                                    richChienThuat.Text = data_chienthuat.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None)[nchienthuat];
                                }
                                clickNonLive = true;
                            }

                            if (stop)
                            {
                                stop = false;
                                richNote.Text = "STOP";
                                picIBET.Enabled = false;
                                picSBO.Enabled = false;
                                btnLoginIBET.Enabled = true;
                                btnLoginSBO.Enabled = true;
                                btnBetLive.Text = "BET LIVE";
                                btnBet.Text = "BET NON LIVE";
                                return;
                            }

                            if (chienthuat.Trim() == "" || chienthuat.Contains("@")) continue;
                            lb_CheckLogin.Text = ncheck.ToString();
                            int nibet = chienthuat.Split('/')[0].Split(',').Length;
                            int nsbo = chienthuat.Split('/')[1].Split(',').Length;
                            string keyIbet = "ibet" + chienthuat.Split('/')[0].Split(',')[ran.Next(0, nibet)];
                            string keySbo = "sbo" + chienthuat.Split('/')[1].Split(',')[ran.Next(0, nsbo)];
                            string capbet = keyIbet + "," + keySbo;
                            string message_chienthuat = "";

                            info infoIbet = getInfoByKey(keyIbet);
                            info infoSbo = getInfoByKey(keySbo);
                            if (infoIbet.credit == null)
                            {
                                richNote.Text = richNote.Text.Insert(0, string.Format("[{0}] is null\n", keyIbet));
                                continue;
                            }
                            if (infoSbo.credit == null)
                            {
                                richNote.Text = richNote.Text.Insert(0, string.Format("[{0}] is null\n", keySbo));
                                continue;
                            }


                            string str_group = infoSbo.group + "," + infoIbet.group;
                            updateCompare();
                            foreach (objCompare oCompare in lstCompare)
                            {
                                double profit = oCompare.profit;
                                int league = Convert.ToInt32(Math.Abs(oCompare.league * 100));
                                string money = "10";
                                if (profit >= -0.03)
                                {
                                    money = getMoneyBet(infoSbo.credit.Replace(",", ""), infoIbet.credit.Replace(",", ""), infoSbo.maxbet, infoIbet.maxbet, infoSbo.money, infoIbet.money, infoSbo.usd, infoIbet.usd, true);
                                }
                                else
                                {
                                    money = getMoneyBet(infoSbo.credit.Replace(",", ""), infoIbet.credit.Replace(",", ""), infoSbo.maxbet, infoIbet.maxbet, infoSbo.money, infoIbet.money, infoSbo.usd, infoIbet.usd);
                                }
                                if (money.Contains("ibet"))
                                {
                                    remove_chienthuat(keyIbet);
                                    updateMessage(keyIbet, "CREDIT...");
                                }
                                if (money.Contains("sbo"))
                                {
                                    remove_chienthuat(keySbo);
                                    updateMessage(keySbo, "CREDIT...");
                                }
                                if (money.Contains("sbo") || money.Contains("ibet")) continue;

                                if (profit < double.Parse(infoIbet.profit) / -100)
                                {
                                    message_chienthuat += string.Format("[{0}] profit is {1} is great than {2}\n", capbet, profit, infoIbet.profit);
                                    continue;
                                }
                                else if (int.Parse(infoIbet.league) < league)
                                {
                                    message_chienthuat += string.Format("[{0}] league compare({1})/league bet({2})\n", capbet, league, infoIbet.league);
                                    continue;
                                }
                                else
                                {
                                    string str_BetSbo = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[0] + "," + oCompare.keo.Split('/')[0] + "," + oCompare.betid.Split('/')[0] + "," + oCompare.score;
                                    string str_BetIbet = oCompare.home + "," + oCompare.away + "," + oCompare.bettype + "," + oCompare.hdp + "," + oCompare.odd.Split('/')[1] + "," + oCompare.keo.Split('/')[1] + "," + oCompare.betid.Split('/')[1] + "," + oCompare.score;

                                    bool betIbet = true;
                                    bool betSbo = true;

                                    ticket oIbet = UtilSoccer.convertStringToTicket(str_BetIbet);
                                    ticket oSbo = UtilSoccer.convertStringToTicket(str_BetSbo);
                                    if (db.oddExists(oIbet) == true)
                                    {
                                        message_chienthuat += string.Format("[{0}] odd list exists\n", keyIbet);
                                        continue;
                                    }
                                    if (db.ticketExists(oIbet, infoIbet.username) == true || db.ticketExists(oSbo, infoSbo.username) == true)
                                    {
                                        message_chienthuat += string.Format("[{0}] bet list exists\n", capbet);
                                        continue;
                                    }
                                    #region Check Mode
                                    string modeBet = getModeBet(infoIbet.mode, infoSbo.mode);
                                    if (modeBet.ToLower() == "sw")
                                    {
                                        if (oSbo.odd < 0)
                                            continue;
                                    }
                                    else if (modeBet.ToLower() == "iw")
                                    {
                                        if (oIbet.odd < 0)
                                            continue;
                                    }
                                    else if (modeBet.ToLower() == "w")
                                    {
                                        AutoAccept = false;
                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                        {
                                            betIbet = true;
                                            betSbo = false;
                                            money = getMoneyBet("10000", infoIbet.credit.Replace(",", ""), "10000", infoIbet.maxbet, "10000,10", infoIbet.money, infoSbo.usd, infoIbet.usd);
                                        }
                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                        {
                                            betIbet = false;
                                            betSbo = true;
                                            getMoneyBet(infoSbo.credit.Replace(",", ""), "10000", infoSbo.maxbet, "10000", infoSbo.money, "10000,10", infoSbo.usd, infoIbet.usd);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else if (modeBet.ToLower() == "l")
                                    {
                                        AutoAccept = false;
                                        if (oSbo.odd < 0 && oIbet.odd > 0)
                                        {
                                            betIbet = false;
                                            betSbo = true;
                                            getMoneyBet(infoSbo.credit.Replace(",", ""), "10000", infoSbo.maxbet, "10000", infoSbo.money, "10000,10", infoSbo.usd, infoIbet.usd);
                                        }
                                        else if (oSbo.odd > 0 && oIbet.odd < 0)
                                        {
                                            betIbet = true;
                                            betSbo = false;
                                            money = getMoneyBet("10000", infoIbet.credit.Replace(",", ""), "10000", infoIbet.maxbet, "10000,10", infoIbet.money, infoSbo.usd, infoIbet.usd);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    #endregion
                                    #region Check Odd on Groups
                                    //if (infoIbet.group == "c")
                                    //{
                                    //    double ibetOddBet = 0;
                                    //    try
                                    //    {
                                    //        ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "0"));
                                    //        Thread.Sleep(1000);
                                    //        int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                    //        if (deviationOddIbet > 1)
                                    //        {
                                    //            message_chienthuat += string.Format("[{0}] Odd IBET down {1}\n", capbet, deviationOddIbet);
                                    //            continue;
                                    //        }
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        message_chienthuat += string.Format("[{0}] Odd IBET error {1}, {2}-{3}, HDP: {4}, BetType: {5}[{6}]\n", capbet, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd);
                                    //        continue;
                                    //    }
                                    //}
                                    //else if (infoIbet.group == "d")
                                    //{
                                    //    double ibetOddBet = 0;
                                    //    try
                                    //    {
                                    //        ibetOddBet = double.Parse(BetManager.getBet(keyIbet).getOddChange(oIbet, money, "0"));
                                    //        Thread.Sleep(1000);
                                    //        int deviationOddIbet = UtilSoccer.getDeviationOdd(oIbet.odd, ibetOddBet);
                                    //        if (deviationOddIbet > 2)
                                    //        {
                                    //            message_chienthuat += string.Format("[{0}] Odd IBET down {1}\n", capbet, deviationOddIbet);
                                    //            continue;
                                    //        }
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        message_chienthuat += string.Format("[{0}] Odd IBET error {1}, {2}-{3}, HDP: {4}, BetType: {5}[{6}]\n", capbet, ibetOddBet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), oIbet.odd);
                                    //        continue;
                                    //    }
                                    //}
                                    if (infoSbo.group == "c")
                                    {
                                        double sboOddBet = 0;
                                        try
                                        {
                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "0"));
                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                            if (deviationOddSbo < -1)
                                            {
                                                message_chienthuat += string.Format("[{0}] Odd SBO down {1}\n", capbet, deviationOddSbo);
                                                continue;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            message_chienthuat += string.Format("[{0}] Odd SBO error {1}, {2}-{3}, HDP: {4}, BetType: {5}[{6}]\n", capbet, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd);
                                            continue;
                                        }
                                    }
                                    else if (infoSbo.group == "a")
                                    {
                                        double sboOddBet = 0;
                                        try
                                        {
                                            sboOddBet = double.Parse(BetManager.getBet(keySbo).getOddChange(oSbo, money, "0"));
                                            int deviationOddSbo = UtilSoccer.getDeviationOdd(oSbo.odd, sboOddBet);
                                            if (deviationOddSbo < 0)
                                            {
                                                message_chienthuat += string.Format("[{0}] Odd SBO down {1}\n", capbet, deviationOddSbo);
                                                continue;
                                            }
                                        }
                                        catch
                                        {
                                            message_chienthuat += string.Format("[{0}] Odd SBO error {1}, {2}-{3}, HDP: {4}, BetType: {5}[{6}]\n", capbet, sboOddBet, oSbo.home, oSbo.away, oSbo.hdp, mylib.updateKeo(oSbo.bettype, oSbo.choose), oSbo.odd);
                                            continue;
                                        }
                                    }
                                    #endregion

                                    #region Bet
                                    if (db.ticketExists(oIbet, infoIbet.username) == false && db.ticketExists(oSbo, infoSbo.username) == false)
                                    {
                                        string phieuchung = "";
                                        if (betIbet)
                                        {
                                            BetManager.getBet(keyIbet).playBetNonLive(oIbet, money, "0", "", str_group);//
                                            string Mes = BetManager.getBet(keyIbet).getMessage();
                                            updateMessage(keyIbet, Mes);
                                            //richNote.Text = richNote.Text.Insert(0,string.Format( "[{0}] request     {1}\n",keyIbet, DateTime.Now.ToLongTimeString()));
                                            if (Mes.Contains("(OK)"))
                                            {
                                                tb_BetIbet.Text = str_BetIbet + "|" + money + "|" + "0" + "|" + phieuchung + "|";
                                                richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keyIbet, infoIbet.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                                updateCredit(keyIbet, BetManager.getBet(keyIbet).getCredit().Split('/')[1]);
                                                lb_Ibet.Text = string.Format("{1}", infoIbet.username, Mes.Replace("(OK)", ""));
                                                if (Mes.Contains("Odd Up"))
                                                {
                                                    player.SoundLocation = "../../../Sound/OddUp.wav";
                                                    player.Play();
                                                    Thread.Sleep(2000);
                                                    ncheck += 2;
                                                    lb_CheckLogin.Text = ncheck.ToString();
                                                }
                                                player.SoundLocation = "../../../Sound/success_ibet.wav";
                                                player.Play();
                                            }//Odd Down
                                            else if (Mes.Contains("Odd Down"))
                                            {
                                                break;
                                            }//ErrorCode
                                            else if (Mes.Contains("ErrorCode"))
                                            {
                                                remove_chienthuat(keyIbet);
                                                updateMessage(keyIbet, "ErrorCode");
                                                //btnBet.Text = "BET NON LIVE";
                                                SendMessageToTelegram("ErrorCode " + infoIbet.username);
                                                player.SoundLocation = "../../../Sound/errorcode.wav";
                                                player.Play();
                                                //return;
                                                break;
                                            }
                                            if (betSbo == false)
                                            {
                                                richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, IBET: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                Thread.Sleep(2000);
                                                ncheck += 2;
                                                lb_CheckLogin.Text = ncheck.ToString();
                                                richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                                break;
                                            }
                                            else
                                            {
                                                phieuchung = BetManager.getBet(keyIbet).getPhieuchung();
                                            }
                                        }
                                        else
                                        {
                                            phieuchung = mylib.generateID("T");
                                        }

                                        if (betSbo)
                                        {
                                            if (phieuchung != "")
                                            {
                                                string realmoney = money;
                                                if (betIbet)
                                                {
                                                    realmoney = BetManager.getBet(keyIbet).getRealMoney();
                                                }
                                                if (int.Parse(realmoney) > 10000)
                                                {
                                                    Thread.Sleep(ran.Next(1, 3) * 1000);
                                                    ncheck += 3;
                                                    lb_CheckLogin.Text = ncheck.ToString();
                                                }
                                                else if (int.Parse(realmoney) > 5000)
                                                {
                                                    Thread.Sleep(ran.Next(4, 10) * 1000);
                                                    ncheck += 10;
                                                    lb_CheckLogin.Text = ncheck.ToString();
                                                }
                                                else
                                                {
                                                    Thread.Sleep(ran.Next(10, 20) * 1000);
                                                    ncheck += 20;
                                                    lb_CheckLogin.Text = ncheck.ToString();
                                                }
                                                BetManager.getBet(keySbo).playBetNonLive(oSbo, realmoney, "0", phieuchung, str_group, AutoAccept);
                                                string Mes = BetManager.getBet(keySbo).getMessage();
                                                updateMessage(keySbo, Mes);
                                                if (Mes.Contains("(OK)"))
                                                {
                                                    tb_BetSbo.Text = str_BetSbo + "|" + money + "|" + "0" + "|" + phieuchung + "|";
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}-{1}]      {2}       [{3}]\n", keySbo, infoSbo.username, Mes.Replace("(OK)", ""), DateTime.Now.ToLongTimeString()));
                                                    updateCredit(keySbo, BetManager.getBet(keySbo).getCredit().Split('/')[1]);
                                                    lb_Sbo.Text = string.Format("{1}", infoSbo.username, Mes.Replace("(OK)", ""));
                                                    if (Mes.Contains("Odd Down"))
                                                    {
                                                        player.SoundLocation = "../../../Sound/OddDown.wav";
                                                        player.Play();
                                                        if (Mes.Contains("Odd Down:-1") == false)
                                                        {
                                                            SendMessageToTelegram(Mes.Replace("(OK)", "") + "----" + infoSbo.username);
                                                        }
                                                        Thread.Sleep(2000);
                                                        File.AppendAllText(@"../../../OddDown.txt", string.Format("[{0}]    {1}\n", keySbo, Mes));
                                                    }
                                                    player.SoundLocation = "../../../Sound/success_sbo.wav";
                                                    player.Play();
                                                }
                                                else if (Mes.Contains("Odd Down"))
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    tb_BetSbo.Text = str_BetSbo + "|" + money + "|" + "0" + "|" + phieuchung + "|";
                                                    Console.Beep(5000, 5000);
                                                    updateMessage(keySbo, "Sbo Bet Fail");
                                                    richNote.Text = richNote.Text.Insert(0, "Sbo Bet Fail\n");
                                                    SendMessageToTelegram("Sbo Bet Fail" + "----" + infoSbo.username);
                                                    return;
                                                }
                                                if (betIbet == true)
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, IBET: {4}, SBO: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                else if (betIbet == false)
                                                {
                                                    richLog.Text = richLog.Text.Insert(0, string.Format("[{0}] {1}-vs-{2}, {3}, SBO: {5}\n", capbet, oIbet.home, oIbet.away, oIbet.hdp, mylib.updateKeo(oIbet.bettype, oIbet.choose), mylib.updateKeo(oSbo.bettype, oSbo.choose), infoIbet.username, infoSbo.username));
                                                    Thread.Sleep(2000);
                                                    ncheck += 2;
                                                    lb_CheckLogin.Text = ncheck.ToString();
                                                }
                                                richLog.Text = richLog.Text.Insert(0, "====================================================================\n");
                                                break;
                                            }
                                        }

                                        if (betIbet == true && phieuchung == "")
                                        {
                                            message_chienthuat += string.Format("[{0}] {1}\n", capbet, BetManager.getBet(keyIbet).getMessage());
                                        }
                                    }
                                    #endregion
                                }
                            }
                            richNote.Text = richNote.Text.Insert(0, message_chienthuat);
                            ncheck += 2;
                            lb_CheckLogin.Text = ncheck.ToString();
                            Thread.Sleep(2000);
                        }
                        #endregion
                        Thread.Sleep(5000);
                        ncheck += 5;
                        lb_CheckLogin.Text = ncheck.ToString();
                        if (!cb_Loop.Checked)
                        {
                            richNote.Text = richNote.Text.Insert(0, "BET NONLIVE COMPLETE\n");
                            btnBet.Text = "BET NON LIVE";
                            break;
                        }
                    }
                    #endregion
                }
                catch
                {
                    SendMessageToTelegram("START ERROR");
                    Thread.Sleep(2000);
                    Console.Beep(2000, 10000);
                }
            });
            #region Main
            if (btnBet.Text == "STOP")
            {
                btnBet.Text = "BET NON LIVE";
                File.AppendAllText(@"../../../richLog.txt", "BET NON LIVE----------[" + DateTime.Now.ToLongTimeString() + "]\n" + richLog.Text);
                File.AppendAllText(@"../../../richNote.txt", "BET NON LIVE----------[" + DateTime.Now.ToLongTimeString() + "]                                                                                                                                                                   \n" + richNote.Text);
                stop = true;
                richLog.Clear();
                richNote.Clear();
            }
            else
            {
                int i = 0;
                foreach (DataRow dr in dtIBET.Rows)
                {
                    dataGridViewIBET.Rows[i].Visible = true;
                    i += 1;
                }
                i = 0;
                foreach (DataRow dr in dtSBO.Rows)
                {
                    dataGridViewSBO.Rows[i].Visible = true;
                    i += 1;
                }
                stop = false;

                tBetNonLive.Start();

            }
            #endregion
        }
        private void bt_CheckIp_Click(object sender, EventArgs e)
        {
            string str_ip = "";
            string str_key = "";
            string mes = "";
            foreach (DataGridViewRow row in dataGridViewSBO.Rows)
            {
                str_ip += row.Cells["ip"].Value.ToString() + ",";
                str_key += row.Cells["key"].Value.ToString() + ",";
            }
            foreach (DataGridViewRow row in dataGridViewIBET.Rows)
            {
                str_ip += row.Cells["ip"].Value.ToString() + ",";
                str_key += row.Cells["key"].Value.ToString() + ",";
            }
            str_ip = str_ip.Substring(0, str_ip.Length - 1);
            str_key = str_key.Substring(0, str_key.Length - 1);
            int i = 0;
            foreach (string ip in str_ip.Split(','))
            {
                if (str_ip.IndexOf(ip).ToString() != str_ip.LastIndexOf(ip).ToString())
                {
                    mes = "CHECK IP";
                    MessageBox.Show(str_key.Split(',')[i] + "/" + ip);
                }
                i += 1;
            }
            if (mes != "CHECK IP")
            {
                MessageBox.Show("IP OK!");
            }
        }
    }
}
