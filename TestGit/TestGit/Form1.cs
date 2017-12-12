using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Console.WriteLine(convertSoccerTime("13:00PM"));

        }

        public static string convertSoccerTime(string matchTime) {
            DateTime dtMatch = DateTime.Parse(matchTime);
            string strTimeMatch = dtMatch.ToString("HH:mm");
            DateTime dtCurrent = DateTime.Now.AddHours(1);
            string strTimeCurrent = dtCurrent.ToString("HH:mm");

            int intTimeMatch = Int16.Parse(strTimeMatch.Split(':')[0]) * 60 + Int16.Parse(strTimeMatch.Split(':')[1]);
            int intTimeCurrent = Int16.Parse(strTimeCurrent.Split(':')[0]) * 60 + Int16.Parse(strTimeCurrent.Split(':')[1]);

            if(intTimeMatch < intTimeCurrent) {
                return dtCurrent.AddDays(1).ToShortDateString() + " " + strTimeMatch;
            }
            else {
                return dtCurrent.ToShortDateString() + " " + strTimeMatch;
            }
        }
    }
}
