using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetList.Lib
{
    public class objCompare
    {
        public string home;
        public string away;
        public string type;
        public string hdp;
        public double odd1;
        public double odd2;
        public string keo1;
        public string keo2;
        public double profit;
        public double league;

        public string ToHtml(string mode = "")
        {
            if (mode == "")
                return string.Format("<span style='color:red'>{0}</span>-vs-<span style='color:blue'>{1}</span> <b>HDP: {2}</b> <span style='color:#207de8'>SBO: {3} <b>{5}</b></span> <span style='color:brown'>IBET: {4} <b>{6}</b></span>, {7}, {8}", home, away, hdp, mylib.updateKeo(type, keo1), mylib.updateKeo(type, keo2), odd1, odd2, profit, league);
            else if (mode == "i")
                return string.Format("<span style='color:red'>{0}</span>-vs-<span style='color:blue'>{1}</span> <b>HDP: {2}</b> <span style='color:#207de8'>IBET: {3} <b>{5}</b></span> <span style='color:brown'>IBET: {4} <b>{6}</b></span>, {7}, {8}", home, away, hdp, mylib.updateKeo(type, keo1), mylib.updateKeo(type, keo2), odd1, odd2, profit, league);
            else if (mode == "s")
                return string.Format("<span style='color:red'>{0}</span>-vs-<span style='color:blue'>{1}</span> <b>HDP: {2}</b> <span style='color:#207de8'>SBO: {3} <b>{5}</b></span> <span style='color:brown'>SBO: {4} <b>{6}</b></span>, {7}, {8}", home, away, hdp, mylib.updateKeo(type, keo1), mylib.updateKeo(type, keo2), odd1, odd2, profit, league);
            return "";
        }
    }
}