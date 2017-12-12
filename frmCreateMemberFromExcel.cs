using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;

namespace MiMi
{
    public partial class frmCreateMemberFromExcel : Form
    {
        IniParser parser;
        string url = "";
        string currentLoginName = "";
        string currentType = "";
        SeleniumHelper helper;
        Database db;
        List<objRow> lst = new List<objRow>();
        Microsoft.Office.Interop.Excel.Application _excelApp;        

        public frmCreateMemberFromExcel()
        {
            InitializeComponent();
            db = new Database();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog1.ShowDialog();            
            txtFilePath.Text = openFileDialog1.FileName;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            helper = new SeleniumHelper();
            Thread t = new Thread(delegate()
            {
                _excelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelOpenSpreadsheets(txtFilePath.Text);
                _excelApp.Quit();
            });
            t.Start();
        }

        public void ExcelOpenSpreadsheets(string thisFileName)
        {
            try
            {              
                Workbook workBook = _excelApp.Workbooks.Open(thisFileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                ExcelScanIntenal(workBook);                
                workBook.Close();          
            }
            catch
            {

            }
        }

        private void ExcelScanIntenal(Workbook workBookIn)
        {
            int numSheets = workBookIn.Sheets.Count;
            Worksheet sheet = (Worksheet)workBookIn.Sheets[1];
            Range excelRange = sheet.UsedRange;

            int rowCount = excelRange.Rows.Count;
            int colCount = excelRange.Columns.Count;

            for (int i = 2; i <= rowCount; i++)
            {
                string type = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 1]).Value2.ToString();
                string loginname = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 2]).Value2.ToString();
                string member = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 3]).Value2.ToString();
                string password = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 4]).Value2.ToString();
                string credit = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 5]).Value2.ToString();
                string group = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 6]).Value2.ToString();
                string status = "";
                try
                {
                    status = ((Microsoft.Office.Interop.Excel.Range)excelRange.Cells[i, 7]).Value2.ToString();
                }
                catch (Exception)
                {
                    status = "";
                }

                if (status.Trim() == "")
                {
                    objRow r = new objRow();
                    r.type = type;
                    r.loginname = loginname;
                    r.member = member;
                    r.password = password;
                    r.credit = credit;
                    r.group = group;

                    sheet.Cells[i, 7] = createSbobetMember(r);                    
                    workBookIn.Save();
                }
            }
            helper.Close();
        }

        private string createSbobetMember(objRow r)
        {
            Account o = db.getAccountByLoginName(r.loginname, r.type, frmLogin.user.username);
            if (o != null)
            {
                if (currentLoginName != r.loginname)
                {
                    currentLoginName = r.loginname;                    
                    if (File.Exists(@"config.ini"))
                        parser = new IniParser(@"config.ini");

                    url = parser.GetSetting("sbobet", "link");
                    if (url[url.Length - 1].ToString() != "/")
                        url += "/";

                    helper.GotoURL(url);
                    helper.FindElement(By.XPath("//input[@id='username']")).SendKeys(o.Loginname);
                    helper.FindElement(By.XPath("//input[@id='password']")).SendKeys(mylib.md5_Decrypt(o.Password));
                    helper.FindElement(By.XPath("//input[@id='btnSubmit']")).Click();

                    IWebElement firstposition = helper.FindElement(By.XPath("//span[@id='firstposition']"), 10);
                    if (firstposition != null)
                    {
                        IWebElement secondposition = helper.FindElement(By.XPath("//span[@id='secondposition']"), 5);

                        int index1 = Int16.Parse(firstposition.Text[0].ToString()) - 1;
                        int index2 = Int16.Parse(secondposition.Text[0].ToString()) - 1;

                        helper.FindElement(By.XPath("//input[@id='FirstChar']")).SendKeys(o.Code[index1].ToString());
                        helper.FindElement(By.XPath("//input[@id='SecondChar']")).SendKeys(o.Code[index2].ToString());
                        helper.FindElement(By.XPath("//input[@id='btnSubmit']")).Click();
                    }
                    helper.GoToFrame("//frame[@id='MenuFrame']");
                    helper.FindElement(By.XPath("//div[@class='Plus' and contains(text(),'3. Member Mgmt')]")).Click();
                    helper.FindElement(By.XPath("//td[@id='MemberMgmt_NewMember']")).Click();
                }

                helper.GoToFrame(SbobetPage.frame);
                IWebElement form = helper.FindElement(By.XPath("//form[@id='SecurityCodeForm']"), 10);
                if (form != null)
                {
                    IWebElement first = helper.FindElement(By.XPath("//form[@id='SecurityCodeForm']//span[@style='padding-left:5px']"), 5);
                    IWebElement second = helper.FindElement(By.XPath("//form[@id='SecurityCodeForm']//span[@style='padding-left:5px;padding-right:5px']"), 5);

                    int index1 = Int16.Parse(first.Text[0].ToString()) - 1;
                    int index2 = Int16.Parse(second.Text[0].ToString()) - 1;

                    helper.FindElement(By.XPath("//input[@id='digit1']")).SendKeys(o.Code[index1].ToString());
                    helper.FindElement(By.XPath("//input[@id='digit2']")).SendKeys(o.Code[index2].ToString());
                    helper.FindElement(By.XPath("//input[@type='submit']")).Click();
                }

                SelectElement selector0 = new SelectElement(helper.FindElement(By.XPath("//select[@id='account0']")));
                SelectElement selector1 = new SelectElement(helper.FindElement(By.XPath("//select[@id='account1']")));
                SelectElement selector2 = new SelectElement(helper.FindElement(By.XPath("//select[@id='account2']")));

                selector0.SelectByText(Util.Right(r.member, 3)[0].ToString());
                selector1.SelectByText(Util.Right(r.member, 2)[0].ToString());
                selector2.SelectByText(Util.Right(r.member, 1)[0].ToString());

                helper.FindElement(By.XPath("//input[@value='Check Availability']")).Click();
                Thread.Sleep(1000);
                string text = helper.driver.SwitchTo().Alert().Text;
                helper.driver.SwitchTo().Alert().Accept();
                if (text.Contains("The username selected is available"))
                {
                    helper.SendKeys(helper.FindElement(By.XPath("//input[@id='TextPassword']")), r.password);
                    helper.SendKeys(helper.FindElement(By.XPath("//input[@id='TextCredit']")), r.credit);

                    SelectElement selectGroup = new SelectElement(helper.FindElement(By.XPath("//select[@id='UGroup']")));
                    selectGroup.SelectByText(r.group);

                    string imgUrl = helper.FindElement(By.XPath("//img[@id='imgText']")).GetAttribute("src");
                    frmCaptcha frm = new frmCaptcha(imgUrl);
                    frm.ShowDialog();

                    helper.SendKeys(helper.FindElement(By.XPath("//input[@id='vcode']")), frm.captcha);
                    helper.FindElement(By.XPath("//input[@value='Create']")).Click();
                    Thread.Sleep(1000);
                    helper.driver.SwitchTo().Alert().Accept();
                    return "Passed";
                }                
            }
            return "Failed";
        }
    }
}
