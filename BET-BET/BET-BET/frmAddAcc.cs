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
    public partial class frmAddAcc : Form
    {
        Database db;
        public frmAddAcc()
        {
            InitializeComponent();
            db = new Database();
        }

        private void bt_AddAcc_Click(object sender, EventArgs e)
        {
            string str_insert = rtb_Data.Text;
            int i = 0;
            string result = "";
            foreach (string insert in str_insert.Split('\n'))
            {
                i += 1;
                if (db.InsertAccBet(insert) > 0)
                {
                    result += "Acc" + i.ToString() + ": Add Successful\n";
                }
                else
                {
                    result += "Acc" + i.ToString() + ": Add Fail\n";
                }
                rtb_Data.Text = result;
            }
        }

        private void bt_AddAccNew_Click(object sender, EventArgs e)
        {
            string str_insert = rtb_Data.Text;
            int i = 0;
            string result = "";
            foreach (string insert in str_insert.Split('\n'))
            {
                if (insert == ""||insert.Contains("-")) continue;
                i += 1;
                if (db.InsertAccBet(insert, i) > 0)
                {
                    result += "Acc " + i.ToString() + ": Add Successful\n";
                }
                else
                {
                    result += "Acc " + i.ToString() + ": Add Fail\n";
                }
            }
            rtb_Data.Text = result;
        }

        private void bt_UpdateIp_Click(object sender, EventArgs e)
        {
            string str_insert = rtb_Data.Text;
            string str_ip = tb_Ip.Text;
            
            int i = 0;
            string output = "";
            foreach (string insert in str_insert.Split('\n'))
            {
                try
                {
                    if (insert == "")
                    {
                        output += "\n";
                        continue;
                    }
                    if(insert.Contains("-"))
                    {
                        output += "---------------------------------------------------------------------------------------\n";
                        continue;
                    }
                    output += insert.Replace(insert.Split('/')[4], str_ip.Split(',')[i]) + "\n";
                    i += 1;
                }
                catch
                {
                    MessageBox.Show("Total Ip:" + i.ToString());
                    return;
                }
            }
            rtb_Data.Text = output;
        }
    }
}
