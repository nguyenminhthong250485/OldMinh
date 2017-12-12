using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

namespace COMPARE
{
    public partial class Main : Form
    {
        string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Data";
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public object Matches { get; private set; }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
        }
       
        private void bt_Quit_Click(object sender, EventArgs e)
        {
            Thread Quit = new Thread(delegate ()
            {
                player.SoundLocation = "Quit.wav"; // Đường dẫn đến file cần chơi
                player.Play();
                Thread.Sleep(1500);
            });
            Quit.Start();
            Quit.Join();
            Process.GetCurrentProcess().Kill();
        }

        private List<objMatch> convertStringToObjMatch(string input)
        {
            input = input.Replace("'", "");
            List<objMatch> lst = new List<objMatch>();
            string LeaugeName = "", HomeName = "", AwayName = "";            
            string[] dataMatch = input.Split(',');
            if (dataMatch[5] != "")
            {
                LeaugeName = UtilSoccer.ChuanTenLeauge_Ibet(dataMatch[5]);
            }
            if (LeaugeName.Contains("CORNERS") || LeaugeName.Contains("BOOKING") || LeaugeName.Contains("TEAM") || LeaugeName.Contains("MATCH"))
                return null;            
            if (dataMatch[6] != "")
            {
                HomeName = UtilSoccer.ChuanTenTeam_Ibet(dataMatch[6]);
                AwayName = UtilSoccer.ChuanTenTeam_Ibet(dataMatch[7]);
            }
            ///FT///
            objMatch o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;            
            o.IdKeo = dataMatch[31];
            o.BetType = "1";
            o.hdp = dataMatch[32];
            o.Odd1 = dataMatch[33];
            o.Odd2 = dataMatch[34];
            if (dataMatch[35] == "a") o.hdp = "-" + o.hdp;
            o.hdp = UtilSoccer.formatkeo(o.hdp);
            if (o.IdKeo != "")
                lst.Add(o);

            ///OUFT///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[36];
            o.BetType = "3";
            o.hdp = dataMatch[37];
            o.Odd1 = dataMatch[38];
            o.Odd2 = dataMatch[39];
            if (dataMatch[35] == "a") o.hdp = "-" + o.hdp;
            o.hdp = UtilSoccer.formatkeo(o.hdp);
            if (o.IdKeo != "")
                lst.Add(o);

            ///HT///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[44];
            o.BetType = "7";
            o.hdp = dataMatch[45];
            o.Odd1 = dataMatch[46];
            o.Odd2 = dataMatch[47];
            if (dataMatch[48] == "a") o.hdp = "-" + o.hdp;            
            o.hdp = UtilSoccer.formatkeo(o.hdp);
            if (o.IdKeo != "")
                lst.Add(o);

            ///HTOU///
            o = new objMatch();
            o.LeaugeName = LeaugeName;
            o.HomeName = HomeName;
            o.AwayName = AwayName;
            o.IdKeo = dataMatch[49];
            o.BetType = "9";
            o.hdp = dataMatch[50];
            o.Odd1 = dataMatch[51];
            o.Odd2 = dataMatch[52];
            if (dataMatch[48] == "a") o.hdp = "-" + o.hdp;
            o.hdp = UtilSoccer.formatkeo(o.hdp);
            if (o.IdKeo != "")
                lst.Add(o);

            return lst;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
