using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARE
{
    public class objCompare
    {
        public string home;
        public string away;
        public string bettype;
        public double hdp;
        public string odd;
        public string keo;
        public string betid;
        public double profit;
        public double league;
        public string score;
        public string time;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", home, away, bettype, hdp, odd, keo, betid, profit, league, score, time);
        }
    }    
}
