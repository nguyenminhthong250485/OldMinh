using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanOdds
{
    public class objMatch
    {
        public string LeaugeName = "";
        public string HomeName = "";
        public string AwayName = "";
        public string TimeLive = "";
        public string TimeNonLive = "";
        public string Score = "";

        public string IdKeo = "";
        public string Keo = "";
        public string BetType = "";
        public string Odd1 = "";
        public string Odd2 = "";
        public string scantime = "";
        public double afterseconds = 0;
        public void Clear()
        {
            LeaugeName = "";
            HomeName = "";
            AwayName = "";
            TimeLive = "";
            TimeNonLive = "";
            Score = "";

            IdKeo = "";
            Keo = "";
            BetType = "";
            Odd1 = "";
            Odd2 = "";
            scantime = "";
            afterseconds = 0;
        }

        public string toSQL(bool isLive)
        {
            return string.Format("({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", isLive == true ? 1 : 0, LeaugeName, HomeName, AwayName, TimeLive, TimeNonLive, Score, IdKeo, Keo, BetType, Odd1, Odd2, scantime, afterseconds);
        }

        public string Show()
        {
            string result = "";
            result += "Leauge: " + LeaugeName + "\n";
            result += HomeName + "-" + AwayName  + "\n";
            result += "Keo " + Keo + "(" + BetType + ")" + "," + Odd1 + "," + Odd2 + "\n";
            return result;
        }
        public objMatch Clone()
        {
            objMatch Cl = new objMatch();
            Cl.LeaugeName = LeaugeName;
            Cl.HomeName = HomeName;
            Cl.AwayName = AwayName;
            Cl.TimeLive = TimeLive;
            Cl.TimeNonLive = TimeNonLive;
            Cl.Score = Score;

            Cl.IdKeo = IdKeo;
            Cl.Keo = Keo;
            Cl.BetType = BetType;
            Cl.Odd1 = Odd1;
            Cl.Odd2 = Odd2;
            return Cl;
        }
        public List<objMatch> ListClone(List<objMatch> Input)
        {
            List<objMatch> Output = new List<objMatch>();
            foreach (objMatch Cl in Input)
            {
                Output.Add(Cl);
            }
            return Output;
        }
    }
}
