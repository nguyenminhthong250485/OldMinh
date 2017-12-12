using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

namespace COMPARE
{
    public partial class Main : Form
    {
        Database db;

        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data";
        string str_LinkBong88 = "http://www.bong88.net/login888.aspx,http://www.88cado.com/login888.aspx,http://www.cuoc88.net/login888.aspx,http://www.bookie88.net/login888.aspx,";
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public object Matches { get; private set; }

        public Main()
        {
            InitializeComponent();
            foreach (string LinkBong88 in str_LinkBong88.Split(','))
            {
                dm_LinkBong88.Items.Add(LinkBong88);
            }
            dm_LinkBong88.SelectedIndex += 1;
            db = new Database();

            DataTable dt = db.getCompareAccount();
            //for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    //MessageBox.Show(dr["id"].ToString());
            //    ((CompareControl)flowLayoutPanel1.Controls[i]).Group = dr["id"].ToString();
            //    ((CompareControl)flowLayoutPanel1.Controls[i]).str_UserNameSbo = dr["sboname"].ToString();
            //    ((CompareControl)flowLayoutPanel1.Controls[i]).str_UserNameIbet = dr["ibetname"].ToString();
            //    ((CompareControl)flowLayoutPanel1.Controls[i]).str_Profit = dr["profit"].ToString();

            //}
        }

        private void Main_Load(object sender, EventArgs e)
        {
            int width = ClientRectangle.Width/2 - 20;
            int height = compareControl1.Height;
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                ((CompareControl)flowLayoutPanel1.Controls[i]).Size= new System.Drawing.Size(width, height);
                ((CompareControl)flowLayoutPanel1.Controls[i]).linkBong88 = dm_LinkBong88.Text;
            }

            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
        }

        private void bt_Quit_Click(object sender, EventArgs e)
        {
            Thread Quit = new Thread(delegate ()
            {
                //Process.Start("cmd", "/c taskkill /f /im COMPARE.exe");
                //Process.Start("cmd", "/c taskkill /f /im chrome.exe");
                //Process.Start("cmd", "/c taskkill /f /im chromedriver.exe");
                player.SoundLocation = "Quit.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                Thread.Sleep(1500);
                Process.Start(desktop_path.Replace("Data","") + "clear.bat");
            });
            Quit.Start();
            Quit.Join();
            Process.GetCurrentProcess().Kill();
        }

        private void bt_Auto_Click(object sender, EventArgs e)
        {
            //Process.Start("AUTO.exe");
            Thread t = new Thread(delegate ()
            {
                foreach (var obj in flowLayoutPanel1.Controls)
                {
                    if (((CompareControl)obj).dud_SboName.Text != "" && ((CompareControl)obj).dm_UserNameIbet.Text != "")
                    {
                        ((CompareControl)obj).bt_CompareOdd.PerformClick();
                        Thread.Sleep(30000);
                    }
                }
            });
            t.Start();
        }

        private void bt_PreAll_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate ()
            {
                foreach (var obj in flowLayoutPanel1.Controls)
                {
                    ((CompareControl)obj).bt_Pre.PerformClick();
                    Thread.Sleep(10);
                }
            });
            t.Start();
        }

        private void bt_NextAll_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate ()
            {
                foreach (var obj in flowLayoutPanel1.Controls)
                {
                    ((CompareControl)obj).bt_Next.PerformClick();
                    Thread.Sleep(10);
                }
            });
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate ()
            {
                string username = "byce09111";
                string password = "Vvvv6868@";
                string message = "";
                string url = "http://www.bong88.net";
                HttpHelper httpIbet = new HttpHelper();
                string data = httpIbet.Fetch(url + "/login888.aspx", HttpHelper.HttpMethod.Get, null, null);
                string code = Util.HtmlGetAttributeValue(data, "value", "//input[@id='txtCode']");
                string tk = Util.HtmlGetAttributeValue(data, "value", "//input[@id='__tk']");
                data = httpIbet.Fetch(url + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, url + "/login888.aspx", "selLang=en&txtID=" + username + "&txtPW=" + mylib.MD5(mylib.CFS(password) + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=");
                if (data == "")
                {
                    message = "error";
                }
                string verifyInfoLink = Util.GetSubstringByString(data, "window.location='.", "';</script>");
                string sportLink = httpIbet.FetchResponseUri(url + verifyInfoLink, HttpHelper.HttpMethod.Get, url + "/ProcessLogin.aspx", null);
                string mainHost = sportLink.Replace("/sports", "");
                string mainLink = httpIbet.FetchResponseUri(mainHost + "/SwitchPlatform/SwitchToOtherSite/", HttpHelper.HttpMethod.Get, sportLink, null);
                mainHost = mainLink.Replace("/main.aspx", "");
                httpIbet.Fetch(mainLink, HttpHelper.HttpMethod.Get, sportLink, null);
                httpIbet.Fetch(mainHost + "/topmenu.aspx", HttpHelper.HttpMethod.Get, mainLink, null);
                data = httpIbet.Fetch(mainHost + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, mainLink, null);
                int ik = data.IndexOf("name=\"k");
                int iv = data.IndexOf("value=\"v");
                string kIbet = data.Substring(ik + 6, iv - ik - 8);
                string vIbet = data.Substring(iv + 7, iv - ik - 8);
                /////NON-LIVE/////
                string test = "";
                int check = 0;
                List<objMatch> lstIbetMatch = new List<objMatch>();
                string checklogin = "check";
                //Thread tCheckLogin = new Thread(delegate ()
                //{
                //    while (checklogin != "")
                //    {
                //        check += 1;
                //        Thread.Sleep(60000);
                //        checklogin = httpIbet.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + username.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                //    }
                //    MessageBox.Show("CHECK LOGIN");
                //});
                //tCheckLogin.Start();
                while (data != "")
                {
                    check += 1;
                    lstIbetMatch.Clear();
                    data = httpIbet.Fetch(mainHost + "/UnderOver_data.aspx?Market=t&Sport=1&DT=&RT=W&CT=&Game=0&OrderBy=0&OddsType=4&MainLeague=0&DispRang=0&" + kIbet + "=" + vIbet + "&key=dodds&_=1502509515441", HttpHelper.HttpMethod.Get, mainHost + "/UnderOver.aspx?Market=t&DispVer=new", null);
                    foreach (Match m in Regex.Matches(data, @"Nt\[\d+\]=\[.*\];"))
                    {
                        string strIbetMatch = m.Value.ToString().Replace("];", "");
                        strIbetMatch = Regex.Replace(strIbetMatch, @"Nt\[\d+\]=\[", "");
                        richOutput.Text = strIbetMatch;
                        List<objMatch> lst = convertStringToObjMatch(strIbetMatch);
                        break;
                    }
                    Thread.Sleep(1000);
                    if(check==50)
                    {
                        Thread.Sleep(10000);
                        checklogin = httpIbet.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + username.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                        if(checklogin=="")
                        {
                            MessageBox.Show("CHECK LOGIN");
                        }
                    }
                    test = httpIbet.Fetch(mainHost + "/login_checkin.aspx", HttpHelper.HttpMethod.Post, mainHost + "/topmenu.aspx", "username=" + username.ToUpper() + "&key=login", "", mainHost, mainHost.Replace("http://", "").Replace("/", ""));
                }
                Console.Beep(1000, 5000);
                richOutput.Text = "Logout\n" + data;

            });
            t.Start();
        }

        private List<objMatch> convertStringToObjMatch(string input)
        {
            input = input.Replace("'", "");
            List<objMatch> lst = new List<objMatch>();
            string LeaugeName = "", HomeName = "", AwayName = "";            
            string[] dataMatch = input.Split(',');
            if (dataMatch[5] != "")
            {
                LeaugeName = UtilSoccer.ChuanTenLeauge_Ibet(dataMatch[5]);
            }
            if (LeaugeName.Contains("CORNERS") || LeaugeName.Contains("BOOKING") || LeaugeName.Contains("TEAM") || LeaugeName.Contains("MATCH"))
                return null;            
            if (dataMatch[6] != "")
            {
                HomeName = UtilSoccer.ChuanTenTeam_Ibet(dataMatch[6]);
                AwayName = UtilSoccer.ChuanTenTeam_Ibet(dataMatch[7]);
            }
            ///FT///
            objMatch o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;            
            o.IdKeo = dataMatch[31];
            o.BetType = "1";
            o.Keo = dataMatch[32];
            o.Odd1 = dataMatch[33];
            o.Odd2 = dataMatch[34];
            if (dataMatch[35] == "a") o.Keo = "-" + o.Keo;
            o.Keo = UtilSoccer.formatkeo(o.Keo);
            if (o.IdKeo != "")
                lst.Add(o);

            ///OUFT///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[36];
            o.BetType = "3";
            o.Keo = dataMatch[37];
            o.Odd1 = dataMatch[38];
            o.Odd2 = dataMatch[39];
            if (dataMatch[35] == "a") o.Keo = "-" + o.Keo;
            o.Keo = UtilSoccer.formatkeo(o.Keo);
            if (o.IdKeo != "")
                lst.Add(o);

            ///HT///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[44];
            o.BetType = "7";
            o.Keo = dataMatch[45];
            o.Odd1 = dataMatch[46];
            o.Odd2 = dataMatch[47];
            if (dataMatch[48] == "a") o.Keo = "-" + o.Keo;            
            o.Keo = UtilSoccer.formatkeo(o.Keo);
            if (o.IdKeo != "")
                lst.Add(o);

            ///HTOU///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[49];
            o.BetType = "9";
            o.Keo = dataMatch[50];
            o.Odd1 = dataMatch[51];
            o.Odd2 = dataMatch[52];
            if (dataMatch[48] == "a") o.Keo = "-" + o.Keo;
            o.Keo = UtilSoccer.formatkeo(o.Keo);
            if (o.IdKeo != "")
                lst.Add(o);

            return lst;
        }
    }
}
