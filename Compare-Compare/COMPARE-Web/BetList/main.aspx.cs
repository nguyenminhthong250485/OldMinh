using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetList
{
    public partial class main : System.Web.UI.Page
    {
        Database db = new Database();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ss_username"] == null)
                Response.Redirect("index.aspx");
            else
            {
                string output = "<table id='tbl_data' class='table' style='width: 1024px; '><tr><th>#</th>";
                output += "<th>id</th>";
                output += "<th>type</th>";
                output += "<th>member</th>";
                output += "<th>hdp</th>";
                output += "<th>home</th>";
                output += "<th>away</th>";
                output += "<th>choose</th>";
                output += "<th>money</th>";
                output += "<th>betgroup</th></tr>";
                DataTable dt = db.getBetList();
                int stt = 0;
                string temp = "";
                string row = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (temp != dr["phieuchung"].ToString())
                    {
                        temp = dr["phieuchung"].ToString();
                        row = "<tr style='background:#19D58C ;'><td colspan='10'></td></tr>";
                    }
                    else
                    {
                        row = "";
                    }

                    stt++;
                    output += row;
                    output += "<tr><td>" + stt + "</td>";
                    output += "<td>" + dr["phieuchung"] + "</td>";
                    output += "<td>" + dr["type"] + "</td>";
                    output += "<td>" + dr["member"] + "</td>";
                    output += "<td>" + dr["hdp"] + "</td>";
                    output += "<td>" + dr["home"] + "</td>";
                    output += "<td>" + dr["away"] + "</td>";
                    output += "<td>" + dr["choose"] + " (" + dr["odd"] + ")" + "</td>";
                    output += "<td>" + dr["money"] + " (" + dr["usd"] + ")" + "</td>";
                    output += "<td>" + dr["betgroup"] + " (" + dr["systemdate"] + ")" + "</td>";
                    output += "</tr>";                    
                }
                output += "</table>";
                lblOutput.Text = output;
            }
        }
    }
}