using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace BET
{
    public partial class Main : Form
    {
        Thread ThreadLoginAuto;
        Thread ThreadBetAuto;
        Thread ThreadBetOne;
        Thread ThreadMain;
        Database db;


        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data";       
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        public string str_BetListSbo = "";
        public string str_BetListIbet = "";
        public string str_Mode = "Normal,ComSbo,ComIbet_d,ComSboIbet_d";
        bool Run = false;
        bool Next = false;
        public bool PressBetAuto = false;
        public bool PressBetOne = false;

        public void loadSheetName()
        {
            DataTable dtSheet = db.getSheet();
            foreach (DataRow dr in dtSheet.Rows)
            {
                dm_Partner.Items.Add(dr[0].ToString());
            }
        }
        public void loadData()
        {
            string BetData = File.ReadAllText(desktop_path + "\\Bet.txt");            
            //BetControl
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                try
                {
                    if (BetData.Split('@')[i] != "")
                    {
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_UserNameSbo = BetData.Split('@')[i].Split('/')[0];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_IpSbo = BetData.Split('@')[i].Split('/')[1];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_GiaDoSbo = BetData.Split('@')[i].Split('/')[2];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_UserNameIbet = BetData.Split('@')[i].Split('/')[3];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_IpIbet = BetData.Split('@')[i].Split('/')[4];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_GiaDoIbet = BetData.Split('@')[i].Split('/')[5];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_Money = BetData.Split('@')[i].Split('/')[6];
                        ((BetControl)flowLayoutPanel1.Controls[i]).str_Style = BetData.Split('@')[i].Split('/')[7];
                        ((BetControl)flowLayoutPanel1.Controls[i]).GroupSbo = BetData.Split('@')[i].Split('/')[8].Split(',')[0];
                        ((BetControl)flowLayoutPanel1.Controls[i]).GroupIbet = BetData.Split('@')[i].Split('/')[8].Split(',')[1];
                        ((BetControl)flowLayoutPanel1.Controls[i]).LoadData();
                    }              
                }
                catch
                {                  
                    ((BetControl)flowLayoutPanel1.Controls[i]).richOutput.Text = "Data Group Empty";
                    continue;
                }
            }            
        }
        public Main()
        {
            InitializeComponent();
    
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                ((BetControl)flowLayoutPanel1.Controls[i]).Width = this.Width / 3;
                ((BetControl)flowLayoutPanel1.Controls[i]).Height = this.Height / 3;
            }
                db = new Database();
            //Min Time,Max Time
            for (int i = 3; i < 100; i++)
            {
                dudMinTime.Items.Add(i.ToString());
            }
            dudMinTime.SelectedIndex += 1;
            for (int i = 10; i < 100; i++)
            {
                dudMaxTime.Items.Add(i.ToString());
            }
            dudMaxTime.SelectedIndex += 1;
            
        }
        private void Main_Load(object sender, EventArgs e)
        {
            loadSheetName();
            loadData();
        }        
        private void btnBetAuto_Click(object sender, EventArgs e)
        {
            if (btnBetAuto.Text == "Bet Auto")
            {
                btnBetAuto.Text = "Stop Bet Auto";
                ThreadBetAuto = new Thread(delegate ()
                {
                    player.SoundLocation = "Ring.wav"; // Đường dẫn đến file cần chơi
                    player.Play();
                    if (PressBetAuto == false)
                    {
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                        {
                            if (((BetControl)flowLayoutPanel1.Controls[i]).dud_SboName.Text != "")
                            {
                                ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.PerformClick();
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Red;
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    PressBetAuto = true;
                    while (true)
                    {
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                        {
                            int number = i + 1;
                            if (((BetControl)flowLayoutPanel1.Controls[i]).CheckLoginSbo == true && ((BetControl)flowLayoutPanel1.Controls[i]).CheckLoginIbet == true)
                            {
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Blue;
                                tt_Main.SetToolTip(btnBetAuto, "Run BetControl " + number);
                                ((BetControl)flowLayoutPanel1.Controls[i]).Complete = false;
                                Run = true;
                                while (((BetControl)flowLayoutPanel1.Controls[i]).Complete == false)
                                {
                                    Thread.Sleep(1000);
                                    if (Next)
                                    {
                                        Next = false;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                                    }
                                }
                                if(((BetControl)flowLayoutPanel1.Controls[i]).StopAll)
                                {
                                    while(true)
                                    {
                                        Console.Beep(1000, 2000);
                                        Thread.Sleep(5000);
                                    }
                                }
                                switch (number)
                                {
                                    case 1: player.SoundLocation = "betcomplete_one.wav"; break;
                                    case 2: player.SoundLocation = "betcomplete_two.wav"; break;
                                    case 3: player.SoundLocation = "betcomplete_three.wav"; break;
                                    case 4: player.SoundLocation = "betcomplete_four.wav"; break;
                                    case 5: player.SoundLocation = "betcomplete_five.wav"; break;
                                    case 6: player.SoundLocation = "betcomplete_six.wav"; break;
                                    case 7: player.SoundLocation = "betcomplete_seven.wav"; break;
                                    case 8: player.SoundLocation = "betcomplete_eight.wav"; break;
                                    case 9: player.SoundLocation = "betcomplete_nine.wav"; break;
                                    case 10: player.SoundLocation = "betcomplete_ten.wav"; break;
                                    case 11: player.SoundLocation = "betcomplete_eleven.wav"; break;
                                    case 12: player.SoundLocation = "betcomplete_twelve.wav"; break;
                                }
                                player.Play();
                                Run = false;
                                Thread.Sleep(10000);
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                });
                ThreadBetAuto.Start();
            }
            else
            {
                StopThread(ThreadBetAuto);
                btnBetAuto.Text = "Bet Auto";
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    if (((BetControl)flowLayoutPanel1.Controls[i]).dud_SboName.Text != "")
                    {
                        ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.Text = "Bet";
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }        
        private void btnBetOne_Click(object sender, EventArgs e)
        {
            if (btnBetOne.Text == "Bet One")
            {
                btnBetOne.Text = "Stop Bet One";
                ThreadBetOne = new Thread(delegate ()
                {
                    player.SoundLocation = "Ring.wav"; // Đường dẫn đến file cần chơi
                    player.Play();
                    if (PressBetOne == false)
                    {
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                        {
                            if (((BetControl)flowLayoutPanel1.Controls[i]).dud_SboName.Text != "")
                            {
                                ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.PerformClick();
                                ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Red;
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    PressBetOne = true;
                    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                    {
                        int number = i + 1;
                        if (((BetControl)flowLayoutPanel1.Controls[i]).CheckLoginSbo == true && ((BetControl)flowLayoutPanel1.Controls[i]).CheckLoginIbet == true)
                        {
                            ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Blue;
                            tt_Main.SetToolTip(btnBetAuto, "Run BetControl " + number);
                            ((BetControl)flowLayoutPanel1.Controls[i]).Complete = false;
                            Run = true;
                            while (((BetControl)flowLayoutPanel1.Controls[i]).Complete == false)
                            {
                                Thread.Sleep(1000);
                                if (Next)
                                {
                                    Next = false;
                                    ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                                }
                            }
                            switch (number)
                            {
                                case 1: player.SoundLocation = "betcomplete_one.wav"; break;
                                case 2: player.SoundLocation = "betcomplete_two.wav"; break;
                                case 3: player.SoundLocation = "betcomplete_three.wav"; break;
                                case 4: player.SoundLocation = "betcomplete_four.wav"; break;
                                case 5: player.SoundLocation = "betcomplete_five.wav"; break;
                                case 6: player.SoundLocation = "betcomplete_six.wav"; break;
                                case 7: player.SoundLocation = "betcomplete_seven.wav"; break;
                                case 8: player.SoundLocation = "betcomplete_eight.wav"; break;
                                case 9: player.SoundLocation = "betcomplete_nine.wav"; break;
                                case 10: player.SoundLocation = "betcomplete_ten.wav"; break;
                                case 11: player.SoundLocation = "betcomplete_eleven.wav"; break;
                                case 12: player.SoundLocation = "betcomplete_twelve.wav"; break;
                            }

                            player.Play();
                            Run = false;
                            Thread.Sleep(10000);
                            ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    btnBetOne.Text = "Bet One";
                });
                ThreadBetOne.Start();
            }
            else
            {
                StopThread(ThreadBetOne);
                btnBetOne.Text = "Bet One";
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    if (((BetControl)flowLayoutPanel1.Controls[i]).dud_SboName.Text != "")
                    {
                        ((BetControl)flowLayoutPanel1.Controls[i]).Complete = true;
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.Text = "Bet";
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnBet.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Next = true;
        }
        private void btnDelBetList_Click(object sender, EventArgs e)
        {
            tt_Main.SetToolTip(bt_DelBetList, "Delete Complete");
            File.WriteAllText(desktop_path + "\\BetListSbo.txt", "");
            File.WriteAllText(desktop_path + "\\BetListIbet.txt", "");
            if (btnLoginAuto.Enabled)
            {
                File.WriteAllText(desktop_path + "\\SboErrorList.txt", "");
                File.WriteAllText(desktop_path + "\\IbetErrorList.txt", "");
            }
            str_BetListSbo = ""; str_BetListIbet = "";
            player.SoundLocation = "betlistempty.wav"; // Đường dẫn đến file cần chơi
            player.Play();
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (dm_Partner.Text != "")
            {
                DataTable dt = db.getBetAccountTable(dm_Partner.Text);
                string str = "";
                foreach (DataRow dr in dt.Rows)
                {
                    str += dr["sboname"] + "/";
                    str += dr["sboip"] + "/";
                    str += dr["sbousd"] + "/";
                    str += dr["ibetname"] + "/";
                    str += dr["ibetip"] + "/";
                    str += dr["ibetusd"] + "/";
                    str += dr["money"] + "/";
                    str += dr["type"] + "/";
                    str += dr["betgroup"] + "/";
                    str += "@";
                }
                File.WriteAllText(desktop_path + "\\Bet.txt", str);
                loadData();
                Application.Restart();             
            }
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Thread Quit = new Thread(delegate ()
            {
                player.SoundLocation = "Quit.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                Thread.Sleep(1500);
                //Process.Start(desktop_path.Replace("Data", "") + "clear.bat");
            });
            Quit.Start();
            Quit.Join();
            Process.GetCurrentProcess().Kill();
        }
        private void btnLoginAuto_Click(object sender, EventArgs e)
        {
            btnLoginAuto.Enabled = false;
            ThreadLoginAuto = new Thread(delegate ()
            {
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    if (((BetControl)flowLayoutPanel1.Controls[i]).dud_SboName.Text != "")
                    {
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnLogin.PerformClick();
                        ((BetControl)flowLayoutPanel1.Controls[i]).btnLogin.ForeColor = System.Drawing.Color.Red;
                        Thread.Sleep(2000);
                    }
                }
                btnBetOne.Enabled = true;
                btnBetAuto.Enabled = true;
            });
            ThreadLoginAuto.Start();
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopThread(ThreadLoginAuto);
            StopThread(ThreadBetAuto);
            StopThread(ThreadBetOne);
            StopThread(ThreadMain);
        }
        private void StopThread(Thread t)
        {
            try
            {
                t.Abort();
            }
            catch (Exception)
            {
            }
        }
        private void btnUpdateBetAccount_Click(object sender, EventArgs e)
        {
            BetAccount frm = new BetAccount();
            frm.ShowDialog();
            loadSheetName();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                pictureBox1.Enabled = true;
                groupBetAccountSetting.Enabled = false;
                groupControl.Enabled = true;
                btnStart.Text = "Stop";
                string str_CompareLive_AA = "";
                string str_CompareLive_AB = "";
                string str_CompareLive_AC = "";
                string str_CompareLive_AD = "";
                string str_CompareLive_CA = "";
                string str_CompareLive_CB = "";
                string str_CompareLive_CC = "";
                string str_CompareLive_CD = "";
                string str_CompareNonLive_AA = "";
                string str_CompareNonLive_AB = "";
                string str_CompareNonLive_AC = "";
                string str_CompareNonLive_AD = "";
                string str_CompareNonLive_CA = "";
                string str_CompareNonLive_CB = "";
                string str_CompareNonLive_CC = "";
                string str_CompareNonLive_CD = "";

                ThreadMain = new Thread(delegate ()
                {
                    while (true)
                    {
                        if (Run == false)
                        {
                            str_BetListSbo = File.ReadAllText(desktop_path + "\\BetListSbo.txt");
                            str_BetListIbet = File.ReadAllText(desktop_path + "\\BetListIbet.txt");
                        }

                        #region Load Data From Server
                        DataTable dtCompareAccount = db.getCompareAccount();
                        foreach (DataRow dr in dtCompareAccount.Rows)
                        {
                            try
                            {
                                if (dr["id"].ToString() == "a,a")
                                    str_CompareLive_AA = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "a,b")
                                    str_CompareLive_AB = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "a,c")
                                    str_CompareLive_AC = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "a,d")
                                    str_CompareLive_AD = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "c,a")
                                    str_CompareLive_CA = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "c,b")
                                    str_CompareLive_CB = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "c,c")
                                    str_CompareLive_CC = dr["CompareLive"].ToString();
                                else if (dr["id"].ToString() == "c,d")
                                    str_CompareLive_CD = dr["CompareLive"].ToString();
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (dr["id"].ToString() == "a,a")
                                    str_CompareNonLive_AA = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "a,b")
                                    str_CompareNonLive_AB = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "a,c")
                                    str_CompareNonLive_AC = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "a,d")
                                    str_CompareNonLive_AD = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "c,a")
                                    str_CompareNonLive_CA = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "c,b")
                                    str_CompareNonLive_CB = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "c,c")
                                    str_CompareNonLive_CC = dr["CompareNonLive"].ToString();
                                else if (dr["id"].ToString() == "c,d")
                                    str_CompareNonLive_CD = dr["CompareNonLive"].ToString();
                            }
                            catch (Exception)
                            {
                            }
                        }
                        #endregion

                        #region Trans Data To Bet Control
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                            {
                            //str_Compare
                            string str_group = ((BetControl)flowLayoutPanel1.Controls[i]).GroupSbo + "," + ((BetControl)flowLayoutPanel1.Controls[i]).GroupIbet;
                                switch (str_group)
                                {
                                    case "a,a":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_AA;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_AA;
                                        break;
                                    case "a,b":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_AB;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_AB;
                                        break;
                                    case "a,c":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_AC;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_AC;
                                        break;
                                    case "a,d":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_AD;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_AD;
                                        break;
                                    case "c,a":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_CA;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_CA;
                                        break;
                                    case "c,b":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_CB;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_CB;
                                        break;
                                    case "c,c":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareLive_CC;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_CC;
                                        break;
                                    case "c,d":
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareLive = str_CompareNonLive_CD;
                                        ((BetControl)flowLayoutPanel1.Controls[i]).str_CompareNonLive = str_CompareNonLive_CD;
                                        break;
                                }
                                //str_Comapre
                                //MinTime,MaxTime,BetList
                                ((BetControl)flowLayoutPanel1.Controls[i]).RandomTimeMin = int.Parse(dudMinTime.Text);
                                ((BetControl)flowLayoutPanel1.Controls[i]).RandomTimeMax = int.Parse(dudMinTime.Text);
                                ((BetControl)flowLayoutPanel1.Controls[i]).str_BetListSbo = str_BetListSbo;
                                ((BetControl)flowLayoutPanel1.Controls[i]).str_BetListIbet = str_BetListIbet;
                            }
                        #endregion
                        Thread.Sleep(100);
                    }
                });
                ThreadMain.Start();
            }
            else
            {
                pictureBox1.Enabled = false;
                groupBetAccountSetting.Enabled = true;
                groupControl.Enabled = false;
                btnStart.Text = "Start";
                ThreadMain.Abort();
            }
        }
    }
}
