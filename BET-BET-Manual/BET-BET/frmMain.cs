using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BET_BET
{
    public partial class frmMain : Form
    {
        Database db;      
        Random ran = new Random();
        public frmMain()
        {
            InitializeComponent();
            db = new Database();
        }      

        private void frmMain_Load(object sender, EventArgs e)
        {
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnUpdateSheet_Click(object sender, EventArgs e)
        {
            frmSheet frm = new frmSheet();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string d = DateTime.Now.ToString("yyMMddHHmmssfff");
            MessageBox.Show(d);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
