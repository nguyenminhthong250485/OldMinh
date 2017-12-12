using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DS_IPVN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtb_Ip.Location = new Point(0, ClientRectangle.Height - 250);
            rtb_Ip.Width = ClientRectangle.Width;
            rtb_Ip.Height = 200;
        }

        private void bt_FPT1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "1.";
            string ip2 = r.Next(52, 55).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "42.";
            string ip2 = r.Next(112, 119).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "58.";
            string ip2 = r.Next(186, 187).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT4_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "113.";
            string ip2 = r.Next(22, 22).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "113.";
            string ip2 = r.Next(23, 23).ToString() + ".";
            string ip3 = r.Next(0, 127).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        private void bt_FPT6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "118.";
            string ip2 = r.Next(68, 71).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT7_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "183.";
            string ip2 = r.Next(80, 80).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT8_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "183.";
            string ip2 = r.Next(81, 81).ToString() + ".";
            string ip3 = r.Next(0, 127).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_FPT9_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "210.";
            string ip2 = r.Next(245, 245).ToString() + ".";
            string ip3 = r.Next(64, 127).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VNPT1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "14.";
            string ip2 = r.Next(160, 191).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        private void bt_VNPT2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "113.";
            string ip2 = r.Next(160, 191).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VNPT3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "14.";
            string ip2 = r.Next(244, 255).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
            
        }
        private void bt_VNPT4_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "123.";
            string ip2 = r.Next(16, 31).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VNPT5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "203.";
            string ip2 = r.Next(210, 210).ToString() + ".";
            string ip3 = r.Next(192, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VNPT6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "222.";
            string ip2 = r.Next(252, 255).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "115.";
            string ip2 = r.Next(72, 79).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        private void bt_VIETTEL2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "27.";
            string ip2 = r.Next(64, 79).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "116.";
            string ip2 = r.Next(96, 111).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL4_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "116.";
            string ip2 = r.Next(118, 118).ToString() + ".";
            string ip3 = r.Next(0, 127).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "117.";
            string ip2 = r.Next(0, 7).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "125.";
            string ip2 = r.Next(212, 212).ToString() + ".";
            string ip3 = r.Next(128, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL7_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "125.";
            string ip2 = r.Next(234, 235).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_VIETTEL8_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "171.";
            string ip2 = r.Next(224, 255).ToString() + ".";
            string ip3 = r.Next(0, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }
        private void bt_NETNAM1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "101.";
            string ip2 = r.Next(53, 53).ToString() + ".";
            string ip3 = r.Next(0, 63).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        private void bt_NETNAM2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "119.";
            string ip2 = r.Next(17, 17).ToString() + ".";
            string ip3 = r.Next(192, 255).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        private void bt_NETNAM3_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string ip1 = "101.";
            string ip2 = r.Next(96, 96).ToString() + ".";
            string ip3 = r.Next(64, 127).ToString() + ".";
            string ip4 = r.Next(0, 255).ToString() + ",";
            rtb_Ip.Text += ip1 + ip2 + ip3 + ip4;
        }

        
    }
}
