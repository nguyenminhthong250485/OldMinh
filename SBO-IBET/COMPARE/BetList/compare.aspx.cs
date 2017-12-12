using BetList.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetList
{
    public partial class compare : System.Web.UI.Page
    {
        Database db = new Database();
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string filter_minus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ss_username"] == null)
                Response.Redirect("index.aspx");
            else
            {
                string outNonLive = "<table id='tbl_data' class='table' style='width: 100%; '><tr><th>#</th>";
                outNonLive += "<th><h3 style='color: red'><b>NonLive</b></h3></th><th><h3 style='color: blue'><b>Live</b></h3></th></tr>";
                DataTable dt = db.getCompare();
                DataTable dtUnder = db.getUnder();
                foreach (DataRow dr in dt.Rows)
                {
                    outNonLive += "<tr><td>" + dr["id"] + "</td>";
                    outNonLive += "<td>";
                    int stt = 0;
                    foreach (objCompare o in mylib.convertStringListCompare(dr["CompareNonLive"].ToString()))
                    {
                        stt++;
                        outNonLive += stt + ". " + o.ToHtml() + "<br />";
                    }
                    outNonLive += "</td>";
                    stt = 0;
                    outNonLive += "<td>";
                    foreach (objCompare o in mylib.convertStringListCompare(dr["CompareLive"].ToString()))
                    {
                        stt++;
                        outNonLive += stt + ". " + o.ToHtml() + "<br />";
                    }
                    outNonLive += "</td>";
                    outNonLive += "</tr>";
                }
                foreach (DataRow drUnder in dtUnder.Rows)
                {
                    outNonLive += "<tr style='background-color: yellow'><td>UNDER</td>";
                    outNonLive += "<td>";
                    outNonLive += "</td>";
                    int stt = 0;
                    outNonLive += "<td>";
                    foreach (objCompare o in mylib.convertStringListCompare(drUnder["CompareLive"].ToString()))
                    {
                        stt++;
                        outNonLive += stt + ". " + o.ToHtml() + "<br />";
                    }
                    outNonLive += "</td>";
                    outNonLive += "</tr>";
                }
                outNonLive += "</table>";
                lblNonLive.Text = outNonLive;


                string outMinus = "<table id='tbl_data_minus' class='table' style='width: 100%; '><tr><th>#</th>";
                outMinus += "<th><h3 style='color: red'><b>Data1</b></h3></th><th><h3 style='color: blue'><b>Data2</b></h3></th></tr>";

                if(Request.QueryString["id"]!=null)
                    filter_minus = Request.QueryString["id"];              
                DataTable dtMinus = db.getMinus(filter_minus);
                int number = 0;
                foreach (DataRow dr in dtMinus.Rows)
                {
                    string mode = "";
                    if (!String.IsNullOrEmpty(dr["data2"].ToString()))
                    {
                        mode = dr["data2"].ToString().Split(',')[dr["data2"].ToString().Split(',').Length - 1].Trim();
                    }
                    objCompare o1 = mylib.convertStringToCompare(dr["data1"].ToString());
                    objCompare o2 = mylib.convertStringToCompare(dr["data2"].ToString());

                    number++;
                    outMinus += "<tr><td>" + dr["id"] + "</td>";
                    outMinus += "<td>";
                    outMinus += o1 != null ? o1.ToHtml() : "";
                    outMinus += "</td>";
                    outMinus += "<td>";
                    outMinus += o2 != null ? o2.ToHtml(mode) : "";
                    outMinus += "</td>";
                    outMinus += "</tr>";
                }
                outMinus += "</table>";
                lblMinus.Text = outMinus;
            }
        }
    }
}