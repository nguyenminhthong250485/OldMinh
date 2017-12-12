using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GenerateLoto
{
    public partial class frmMain : Form
    {
        Hashtable hsNumber = new Hashtable();
        Hashtable hsPhoi = new Hashtable();
        Hashtable hsNumberOut;
        List<int> lstFull = new List<int>();
        Random ran = new Random();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Generate2D();
            List<string> lstLoai = txtLoai.Text.Split(' ').OfType<string>().ToList();
            richOutput.Clear();
            foreach (DictionaryEntry entry in hsNumberOut)
            {
                Hashtable hs = (Hashtable)entry.Value;
                foreach (DictionaryEntry entry in hsNumberOut)
                {
                    if (!lstLoai.Contains(format2d((int)entry.Key)))
                    {
                        if ((int)entry.Value != 0)
                            richOutput.Text += string.Format("{0}\t{1}\n", format2d((int)entry.Key), (int)entry.Value * Int32.Parse(txtUnit.Text));
                    }
                }
            }

            foreach (DictionaryEntry entry in hsNumberOut)
            {
                if (!lstLoai.Contains(format2d((int)entry.Key)))
                {
                    if ((int)entry.Value != 0)
                        richOutput.Text += string.Format("{0}\t{1}\n", format2d((int)entry.Key), (int)entry.Value * Int32.Parse(txtUnit.Text));
                }
            }

            richOutput.Text += "------------------------------------\n";
        }

        private int randomMoney(int minMoney, int maxMoney)
        {
            return ran.Next(minMoney, maxMoney + 1);
        }

        private string format2d(int input)
        {
            if (input < 10)
                return "0" + input;
            return input.ToString();
        }

        private void Generate2D()
        {
            hsNumber.Clear();
            int phoinumber = Int32.Parse(txtSoPhoi.Text);
            int money = Int32.Parse(txtTien.Text);            
            for (int j = 0; j < phoinumber; j++)
            {
                hsNumberOut = new Hashtable();
                for (int i = 0; i < 10; i++)
                {
                    if (!hsNumber.Contains(i))
                    {
                        int temp = randomMoney(0, money);
                        if (temp <= money / 2)
                        {
                            hsNumber.Add(i, temp);
                            hsNumberOut.Add(i, temp);
                        }
                        else
                        {
                            hsNumber.Add(i, 0);
                            hsNumberOut.Add(i, 0);
                        }
                    }
                    else
                    {
                        if (j == phoinumber - 1)
                        {
                            hsNumberOut.Add(i, money - (int)hsNumber[i]);
                        }
                        else
                        {
                            int temp = randomMoney(0, money);
                            if (temp + (int)hsNumber[i] <= money)
                            {
                                hsNumberOut.Add(i, temp);
                                hsNumber[i] = temp + (int)hsNumber[i];
                            }
                            else
                            {
                                hsNumber[i] = money;
                                hsNumberOut.Add(i, money - (int)hsNumber[i]);
                            }
                        }
                    }
                }

                hsPhoi.Add(j, hsNumberOut);
            }
        }
    }
}
