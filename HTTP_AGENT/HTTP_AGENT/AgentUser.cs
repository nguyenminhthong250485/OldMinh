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

namespace HTTP_AGENT
{
    public partial class AgentUser : UserControl
    {
        DataTable dtSBO, dtIBET;
        Thread tSbo, tIbet;
        Database db;

        private void btnLoginIBET_Click(object sender, EventArgs e)
        {
            
        }
        private void dataGridViewIBET_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex== dataGridViewIBET.Columns["LOGIN"].Index)
            {
                string LoginName= dataGridViewIBET["LoginName", e.RowIndex].Value.ToString().ToUpper();
                string Pass = dataGridViewIBET["Pass", e.RowIndex].Value.ToString();
                string Code = dataGridViewIBET["Code", e.RowIndex].Value.ToString();
                string Usd = dataGridViewIBET["Usd", e.RowIndex].Value.ToString();
                string ParnerName = dataGridViewIBET["ParnerName", e.RowIndex].Value.ToString();

                Thread tIbetLogin;
                HttpHelper http;

                tIbetLogin = new Thread(delegate ()
                {
                    http = new HttpHelper();
                    string UrlIbet = "https://www.b8ag.com";
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
                            dataGridViewIBET["Status", e.RowIndex].Value = "Loading...";
                            data = http.Fetch(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Get, UrlIbet, null);
                            Thread.Sleep(5000);
                            detecas_token = Util.HtmlGetAttributeValue(data, "value", "//input[@id='detecas-token']");
                            detecas_token = Util.EscapeDataString(detecas_token);
                            string UrlIbetMain = http.FetchResponseUri(UrlIbet + "/Customer/SignIn/Flow", HttpHelper.HttpMethod.Post, UrlIbet + "/Customer/SignIn/Flow", "detecas-token=" + detecas_token);
                            UrlIbetMain = UrlIbetMain.Replace("/site-main/", "");
                            string data_site_main = http.Fetch(UrlIbetMain + "/site-main/", HttpHelper.HttpMethod.Get, UrlIbetMain + "/Customer/SignIn/Flow", null);
                            string data_site_MemberList = http.Fetch(UrlIbetMain + "/ex-main/_MemberInfo/CustomerList/Agent/MemberList.aspx", HttpHelper.HttpMethod.Get, UrlIbetMain + "/site-main/", null);

                            if (data_site_MemberList.Contains(LoginName.ToUpper()))
                            {
                                dataGridViewIBET["Status", e.RowIndex].Value = "Successful";
                                HtmlNodeCollection DivMemberList = Util.HtmlGetNodeCollection(data_site_MemberList, "//div[@class='text']");
                                string Member = "";
                                foreach (HtmlNode Div in DivMemberList)
                                {
                                    if (Member!=Div.InnerHtml)
                                    {
                                        Member = Div.InnerHtml;
                                        ((DataGridViewComboBoxCell)dataGridViewIBET["MemberList", e.RowIndex]).Items.Add(Member.Substring(Member.Length-3,3));
                                    }
                                }
                            }
                            else
                            {
                                dataGridViewIBET["Status", e.RowIndex].Value = "Error";
                            }
                        }
                        else
                        {
                            dataGridViewIBET["Status", e.RowIndex].Value = "Account Closed";
                        }
                    }
                    else
                    {
                        dataGridViewIBET["Status", e.RowIndex].Value = "Wrong username/nickname or password";
                    }
                });
                tIbetLogin.Start();
                
            }
            else if(e.ColumnIndex == dataGridViewIBET.Columns["NEW"].Index)
            {

            }
        }

        public AgentUser()
        {
            InitializeComponent();
            db = new Database();
            createTable(db.GetData("Ibet"));
        }
        private void createTable(DataTable dt)
        {
            dataGridViewIBET.Columns.Clear();
            dtIBET = new DataTable();
            dtIBET.Columns.Add("Key");
            dtIBET.PrimaryKey = new DataColumn[] { dtIBET.Columns["Key"] };
            dtIBET.Columns.Add("LoginName");
            dtIBET.Columns.Add("Pass");
            dtIBET.Columns.Add("Code");
            dtIBET.Columns.Add("Usd");
            dtIBET.Columns.Add("Com");
            dtIBET.Columns.Add("ParnerName");
            dtIBET.Columns.Add("Status");
            

            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                index++;
                dtIBET.Rows.Add(new Object[] {index,dr["LoginName"], dr["Pass"], dr["Code"], dr["Usd"], dr["Com"], dr["ParnerName"],""});
            }

            dataGridViewIBET.DataSource = dtIBET;

            dataGridViewIBET.Columns["Key"].HeaderText = "Key";
            dataGridViewIBET.Columns["LoginName"].HeaderText = "LoginName";
            dataGridViewIBET.Columns["Pass"].HeaderText = "Pass";
            dataGridViewIBET.Columns["Code"].HeaderText = "Code";
            dataGridViewIBET.Columns["Usd"].HeaderText = "Usd";
            dataGridViewIBET.Columns["Com"].HeaderText = "Com";
            dataGridViewIBET.Columns["ParnerName"].HeaderText = "ParnerName";
            dataGridViewIBET.Columns["Status"].HeaderText = "Status";


            dataGridViewIBET.Columns["LoginName"].ReadOnly = true;
            dataGridViewIBET.Columns["Pass"].ReadOnly = true;
            dataGridViewIBET.Columns["Code"].ReadOnly = true;
            dataGridViewIBET.Columns["Usd"].ReadOnly = true;
            dataGridViewIBET.Columns["Com"].ReadOnly = true;
            dataGridViewIBET.Columns["ParnerName"].ReadOnly = true;
            dataGridViewIBET.Columns["Status"].ReadOnly = true;
            

            dataGridViewIBET.Columns["Key"].Width = 20;
            dataGridViewIBET.Columns["LoginName"].Width = 100;
            dataGridViewIBET.Columns["Pass"].Width = 60;
            dataGridViewIBET.Columns["Code"].Width = 50;
            dataGridViewIBET.Columns["Usd"].Width = 30;
            dataGridViewIBET.Columns["Com"].Width = 30;
            dataGridViewIBET.Columns["ParnerName"].Width = 80;
            dataGridViewIBET.Columns["Status"].Width = 80;
            

            DataGridViewComboBoxColumn ComboBoxMemBerList = new DataGridViewComboBoxColumn();
            ComboBoxMemBerList.HeaderText = "MemberList";
            ComboBoxMemBerList.Name = "MemberList";
            ComboBoxMemBerList.Width = 80;
            dataGridViewIBET.Columns.Add(ComboBoxMemBerList);

            DataGridViewButtonColumn ColumnButtonLogin = new DataGridViewButtonColumn();
            ColumnButtonLogin.HeaderText = "LOGIN";
            ColumnButtonLogin.Name = "LOGIN";
            ColumnButtonLogin.Text = "LOGIN";
            ColumnButtonLogin.Width = 80;
            ColumnButtonLogin.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(ColumnButtonLogin);

            DataGridViewButtonColumn ColumnButtonNew = new DataGridViewButtonColumn();
            ColumnButtonNew.HeaderText = "NEW";
            ColumnButtonNew.Name = "NEW";
            ColumnButtonNew.Text = "NEW";
            ColumnButtonNew.Width = 80;
            ColumnButtonNew.UseColumnTextForButtonValue = true;
            dataGridViewIBET.Columns.Add(ColumnButtonNew);

            
        }
    }
}
