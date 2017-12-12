using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BET_BET
{
    public partial class Browser : Form
    {
        string document;
        string title;
        string url;
        public Browser(string title, string document, string url = "")
        {
            InitializeComponent();
            this.document = document;
            this.title = title;
            this.url = url;
        }

        private void Browser_Load(object sender, EventArgs e)
        {            
            this.Text = title;
            if (url == "")
            {                
                webBrowser1.DocumentText = document;
            }
            else
            {                                
                webBrowser1.Navigate(url);
            }
        }
    }
}
