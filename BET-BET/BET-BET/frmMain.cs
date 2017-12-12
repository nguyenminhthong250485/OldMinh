using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill -f -im BET-BET.exe";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void bt_InserAccBet_Click(object sender, EventArgs e)
        {
            frmAddAcc frm = new frmAddAcc();
            frm.ShowDialog();
        }
    }
}
