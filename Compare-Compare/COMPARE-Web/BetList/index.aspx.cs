using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetList
{
    public partial class index : System.Web.UI.Page
    {
        Database db;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            db = new Database();
            string username = db.Login(txtUsername.Text, txtPassword.Text);
            if (username != "")
            {
                Session["ss_username"] = username;
                Response.Redirect("main.aspx");
            }
            else
            {
                Session["ss_username"] = null;
                Response.Redirect("http://www.google.com");
            }
        }
    }
}