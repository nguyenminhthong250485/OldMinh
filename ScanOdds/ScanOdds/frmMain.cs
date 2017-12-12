using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ScanOdds
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(delegate ()
            {
                string dataIbet = "";
                string UrlIbet = "http://www.bookie88.net";
                string IpIbet = "183.80.79.165";
                string UserNameIbet = "SJK7402Z1901";
                string passwordIbet = "Lucky790";
                string UrlHostIbet = "";
                string UrlMainIbet = "";

                HttpHelper httpIbet = new HttpHelper();
                httpIbet.ClearCookies();
                string loginurl = httpIbet.FetchResponseUri(UrlIbet, HttpHelper.HttpMethod.Get, null, null, IpIbet);
                dataIbet = httpIbet.Fetch(loginurl, HttpHelper.HttpMethod.Get, null, null, IpIbet);
                string code = Util.HtmlGetAttributeValue(dataIbet, "value", "//input[@id='txtCode']");
                string tk = Util.HtmlGetAttributeValue(dataIbet, "value", "//input[@id='__tk']");
                dataIbet = httpIbet.Fetch(UrlIbet + "/ProcessLogin.aspx", HttpHelper.HttpMethod.Post, UrlIbet, "selLang=en&txtID=" + UserNameIbet + "&txtPW=" + mylib.MD5(mylib.CFS(passwordIbet) + code) + "&txtCode=" + code + "&hidubmit=&IEVerison=0&detecResTime=347&__tk=" + tk + "&IsSSL=0&PF=Default&RMME=on&__di=", IpIbet);
                string validateticket_url = Util.GetSubstringByString(dataIbet, "window.location='", "';</script>");
                UrlHostIbet = "http://" + Util.GetSubstringByString(validateticket_url, "http://", "/ValidateTicket.aspx");
                UrlMainIbet = httpIbet.FetchResponseUri(validateticket_url, HttpHelper.HttpMethod.Get, UrlIbet + "/ProcessLogin.aspx", null, IpIbet);
                dataIbet = httpIbet.Fetch(UrlHostIbet + "/UnderOver.aspx?Market=t&DispVer=new", HttpHelper.HttpMethod.Get, UrlMainIbet, null, IpIbet);
                while (true)
                {

                    dataIbet = httpIbet.Fetch(UrlHostIbet + "/UnderOver_data.aspx?Market=l&Sport=1&DT=&RT=U&CT=04%2F23%2F2017+10%3A05%3A14&Game=0&OrderBy=0&OddsType=4&MainLeague=0&k288383514=v288383808&key=lodds&_=1492956333530", HttpHelper.HttpMethod.Get, UrlHostIbet + "/UnderOver.aspx?Market=t&DispVer=new", null, IpIbet);
                    
                    Thread.Sleep(3000);
   
                    Thread.Sleep(1000);
                }
            });
            t.Start();
        }

        private void sbobetibet1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
