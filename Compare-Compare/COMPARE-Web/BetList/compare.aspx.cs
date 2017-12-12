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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ss_username"] == null)
                Response.Redirect("index.aspx");
            else
            {
                string outNonLive = "<table id='tbl_data' class='table' style='width: 1024px; '><tr><th>#</th>";                
                outNonLive += "<th>info</th></tr>";
                DataTable dt = db.getCompare();
                foreach (DataRow dr in dt.Rows)
                {                    

                    outNonLive += "<tr><td>" + dr["id"] + "</td>";                    
                    outNonLive += "<td>";
                    int stt = 0;
                    foreach(objCompare o in mylib.convertStringListCompare(dr["CompareNonLive"].ToString()))
                    {
                        stt++;
                        outNonLive += stt+". " + o.ToHtml() + "<br />";
                    }
                    outNonLive += "</td>";
                    outNonLive += "</tr>";
                }
                outNonLive += "</table>";
                lblNonLive.Text = outNonLive;


                string outLive = "<table id='tbl_data' class='table' style='width: 1024px; '><tr><th>#</th>";                
                outLive += "<th>info</th></tr>";
                foreach (DataRow dr in dt.Rows)
                {

                    outLive += "<tr><td>" + dr["id"] + "</td>";
                    outLive += "<td>";
                    int stt = 0;
                    foreach (objCompare o in mylib.convertStringListCompare(dr["CompareLive"].ToString()))
                    {
                        stt++;
                        outLive += stt + ". " + o.ToHtml() + "<br />";
                    }
                    outLive += "</td>";
                    outLive += "</tr>";
                }
                outLive += "</table>";
                lblLive.Text = outLive;
            }
        }
    }
}