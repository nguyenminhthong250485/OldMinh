using System;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
namespace HTTP_AGENT
{
    public partial class AgentIbet : UserControl
    {
        public AgentIbet()
        {
            InitializeComponent();
            Button.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            db = new Database();
            LoadDataAgent();
        }
        Database db;
        HttpHelper http;
        Thread tIbetLogin,tIbetWL;
        string MemberList = "";
        private void LoadDataAgent()
        {
            ComboBox_Pass.DataSource = db.GetParnerData("Ut Q3");
            ComboBox_Pass.Name = "Pass";
            ComboBox_Pass.ValueMember = "Pass";

            ComboBox_Usd.DataSource = db.GetParnerData("Ut Q3");
            ComboBox_Usd.Name = "Usd";
            ComboBox_Usd.ValueMember = "Usd";

            ComboBox_Com.DataSource = db.GetParnerData("Ut Q3");
            ComboBox_Com.Name = "Com";
            ComboBox_Com.ValueMember = "Com";

            ComboBox_Code.DataSource = db.GetParnerData("Ut Q3");
            ComboBox_Code.Name = "Code";
            ComboBox_Code.ValueMember = "Code";

            ComboBox_LoginName.DataSource = db.GetParnerData("Ut Q3");
            ComboBox_LoginName.Name = "LoginName";
            ComboBox_LoginName.ValueMember = "LoginName";
        }
        private void bt_Login_Click(object sender, EventArgs e)
        {
            tIbetLogin = new Thread(delegate ()
            {
                http = new HttpHelper();
                string UrlIbet = "https://www.b8ag.com";
                string LoginName = ComboBox_LoginName.Text;
                string Pass = ComboBox_Pass.Text;
                string Code = ComboBox_Code.Text;
                string detecas_token = "";
                string data = http.Fetch(UrlIbet + "/Customer/SignIn?ReturnUrl=%2f", HttpHelper.HttpMethod.Get, null, null);
                string code = Util.HtmlGetAttributeValue(data, "value", "//input[@id='code']");
                string password = mylib.hc(Pass, code);

                data = http.Fetch(UrlIbet + "/", HttpHelper.HttpMethod.Post, UrlIbet, "hidLanguage=en-US&txtUserName=" + LoginName + " &txtPassWord=" + password + "&code=" + code);
                string errorMessage = Util.HtmlGetInnerText(data, "//div[@id='errmsg']");
                if (!errorMessage.Contains("Wrong username/nickname or password"))
                {
                    if (!errorMessage.Contains("Account closed. Please contact your administrator"))
                    {
                        lb_Status.Text = "Loading...";
                        data = http.Fetch(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Get, UrlIbet, null);
                        Thread.Sleep(5000);
                        detecas_token = Util.HtmlGetAttributeValue(data, "value", "//input[@id='detecas-token']");
                        detecas_token = Util.EscapeDataString(detecas_token);
                        string UrlIbetMain = http.FetchResponseUri(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Post, UrlIbet + "/Customer/SignIn/Flow", "detecas-token=" + detecas_token);
                        UrlIbetMain = UrlIbetMain.Replace("/site-main/", "");
                        string data_site_main = http.Fetch(UrlIbetMain + "/site-main/", HttpHelper.HttpMethod.Get, UrlIbetMain + "/Customer/SignIn/Flow", null);
                        string data_site_MemberList = http.Fetch(UrlIbetMain + "/ex-main/_MemberInfo/CustomerList/Agent/MemberList.aspx", HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);
                        HtmlNodeCollection DivMemberList = Util.HtmlGetNodeCollection(data_site_MemberList, "//div[@class='text']");
                        foreach(HtmlNode Div in DivMemberList)
                        {
                            if (!MemberList.Contains(Div.InnerHtml))
                            {
                                MemberList += Div.InnerHtml + ",";
                            }
                        }
                        rtb_Member.Text = MemberList;
                        if (data_site_MemberList.Contains(LoginName.ToUpper()))
                        {
                            lb_Status.Text = "Successful";
                        }
                        else
                        {
                            lb_Status.Text = "Error";
                        }
                    }
                    else
                    {
                        lb_Status.Text = "Account Closed";
                    }
                }
                else
                {
                    lb_Status.Text = "Wrong username/nickname or password";
                }
            });
            tIbetLogin.Start();
        }

        private void ComboBox_LoginName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = ComboBox_LoginName.SelectedIndex;
            ComboBox_Pass.SelectedIndex = i;
            ComboBox_Usd.SelectedIndex = i;
            ComboBox_Com.SelectedIndex = i;
            ComboBox_Code.SelectedIndex = i;
        }

        private void bt_WL_Click(object sender, EventArgs e)
        {
            tIbetWL = new Thread(delegate ()
            {
                
            });
            tIbetWL.Start();
        }
    }
}
