using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickVN868
{
    public partial class frmMain : Form
    {
        //string username="msub7";
        //string password="Zzzz1111";
        //string username="sbsub12";
        //string password="Zzzz1111";
        string key1;
        string key2;
        Thread t;
        SeleniumHelper selenium;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (btnRun.Text == "Run")
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                pictureBox1.Enabled = true;
                btnRun.Text = "Stop";
                Hashtable hs = new Hashtable();
                List<string> lst = new List<string>();
                t = new Thread(delegate ()
                {                    
                    selenium = new SeleniumHelper();
                    if (chkOK368.Checked)
                    {
                        selenium.GotoURL("https://ag.ok368.biz/index.do");
                        selenium.driver.SwitchTo().DefaultContent();
                        selenium.driver.SwitchTo().Frame("frmBody");
                        selenium.FindElement(By.XPath("//div[@title='Standard Mode' and @class='d2']/div[@class='bm']")).Click();
                    }
                    else
                        selenium.GotoURL("https://ag.vn868.info/index.do");
                    selenium.driver.Manage().Window.Position = new System.Drawing.Point(-2000, 0);
                    selenium.GoToFrame("//frame[@name='frmBody']");
                    frmCaptcha captcha = new frmCaptcha(selenium.CaptureScreenShot(selenium.FindElement(By.XPath("//span[@id='randomSpan']"))));
                    captcha.TopMost = true;
                    captcha.ShowDialog();
                    captcha.Focus();
                    selenium.FindElement(By.XPath("//input[@name='loginName']")).Clear();
                    selenium.FindElement(By.XPath("//input[@name='loginName']")).SendKeys(txtUsername.Text);
                    selenium.FindElement(By.XPath("//input[@name='password']")).Clear();
                    selenium.FindElement(By.XPath("//input[@name='password']")).SendKeys(txtPassword.Text);                    
                    selenium.FindElement(By.XPath("//input[@name='randomCode']")).SendKeys(captcha.captcha);
                    selenium.FindElement(By.XPath("//input[@id='submitBtn']")).Click();
                    selenium.driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
                    selenium.driver.Manage().Window.Maximize();
                    Thread.Sleep(3000);
                    while (true)
                    {
                        lst.Clear();
                        selenium.driver.SwitchTo().DefaultContent();
                        selenium.driver.SwitchTo().Frame("container");
                        selenium.driver.SwitchTo().Frame(selenium.FindElement(By.XPath("//frame[contains(@src,'left')]")));
                        selenium.FindElement(By.XPath("//div[@class='menu']/ul/li[contains(.,'Report')]")).Click();
                        selenium.FindElement(By.XPath("//ul/li/a[contains(.,'Schedule')]")).Click();
                        Thread.Sleep(3000);
                        //selenium.FindElement(By.XPath("//ul/li/a[contains(.,'Download Transaction')]")).Click();
                        selenium.FindElement(By.XPath("//ul/li/a[contains(.,'Transaction')]")).Click();
                        Thread.Sleep(3000);
                        selenium.driver.SwitchTo().DefaultContent();
                        selenium.driver.SwitchTo().Frame("container");
                        selenium.driver.SwitchTo().Frame(selenium.FindElement(By.XPath("//frame[@name='main']")));                       
                        ReadOnlyCollection<IWebElement> elements = selenium.FindElements(By.XPath("//table[@id='tb_report']/tbody//a"));                        
                        foreach(IWebElement element in elements)
                        {
                            string att = element.GetAttribute("onclick").Replace("'","").Replace(")", "");
                            string[] arr = att.Split(',');
                            lst.Add(arr[1] + "," + arr[2] + "," + arr[3]);
                        }

                        if (lst.Count > 0)
                        {
                            foreach (string href in lst)
                            {                                                                
                                if (!hs.Contains(href))
                                {
                                    richLog.Text = richLog.Text.Insert(0, string.Format("{0} - {1}\n", DateTime.Now.ToLongTimeString(), href));
                                    if (href.Split(',')[2].Length > 2)
                                    {
                                        hs.Add(href, href);                                    
                                        selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody//a[contains(@onclick,'" + href.Split(',')[0] + "') and contains(@onclick,'" + href.Split(',')[1] + "') and contains(@onclick,'" + href.Split(',')[2] + "')]")).Click();
                                        Thread.Sleep(20000);
                                    }
                                }
                            }
                        }               

                        //int count = elements.Count();
                        //if (elements.Count > 0)
                        //{
                        //    for (int i = 1; i <= count; i++)
                        //    {
                        //        try
                        //        {
                        //            key1 = "";
                        //            key2 = "";                                    
                        //            if (selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[3]")).Text.Trim() != "")
                        //            {
                        //                string t1 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[1]")).Text;
                        //                string t2 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[2]")).Text;
                        //                string t3 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[3]")).Text;
                        //                key1 = t1 + t2 + t3;
                        //                if (key1 != "")
                        //                {                                            
                        //                    richLog.Text = richLog.Text.Insert(0, key1 + "\n");
                        //                    //hs.Add(key1, key1);
                        //                    selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[3]")).Click();
                        //                    Thread.Sleep(20000);
                        //                }
                        //            }
                        //            if (selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[4]")).Text.Trim() != "")
                        //            {
                        //                string t1 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[1]")).Text;
                        //                string t2 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[2]")).Text;
                        //                string t4 = selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[4]")).Text;
                        //                key2 = t1 + t2 + t4;
                        //                if (key2 != "")
                        //                {                                         
                        //                    richLog.Text = richLog.Text.Insert(0, key2 + "\n");
                        //                    //hs.Add(key2, key2);
                        //                    selenium.FindElement(By.XPath("//table[@id='tb_report']/tbody/tr[" + i + "]/td[4]")).Click();
                        //                    Thread.Sleep(20000);
                        //                }
                        //            }
                        //        }
                        //        catch
                        //        { }
                        //    }
                        //}
                        Thread.Sleep(10000);
                    }
                });
                t.Start();             
            }
            else
            {
                pictureBox1.Enabled = false;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                btnRun.Text = "Run";
                selenium.driver.Quit();
                t.Abort();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {          
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);

            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            PictureBox.CheckForIllegalCrossThreadCalls = false;

        }
    }
}
