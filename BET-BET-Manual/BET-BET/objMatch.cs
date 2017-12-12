using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
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
        public string hdp = "";
        public string BetType = "";
        public string Odd1 = "";
        public string Odd2 = "";
        public void Clear()
        {
            LeaugeName = "";
            HomeName = "";
            AwayName = "";
            TimeLive = "";
            TimeNonLive = "";
            Score = "";

            IdKeo = "";
            hdp = "";
            BetType = "";
            Odd1 = "";
            Odd2 = "";
        }
        public string Show()
        {
            string result = "";
            result += "Leauge: " + LeaugeName + "\n";
            result += HomeName + "-" + AwayName + "\n";
            result += "Keo " + hdp + "(" + BetType + ")" + "," + Odd1 + "," + Odd2 + "\n";
            return result;
        }
        public static Hashtable ParseObjToHashTable(List<objMatch> Input)
        {
            Hashtable Output = new Hashtable();
            foreach (objMatch Obj in Input)
            {
                Output.Add(Obj.IdKeo, Obj);
            }
            return Output;
        }
        public static Hashtable GetHashTableIdMark(List<objMatch> Odd_SboList, List<objMatch> Odd_IbetList)
        {
            Hashtable Output = new Hashtable();
            foreach (objMatch Odd_Sbo in Odd_SboList)
            {
                foreach (objMatch Odd_Ibet in Odd_IbetList)
                {
                    if (Convert.ToDateTime(Odd_Sbo.TimeNonLive) < Convert.ToDateTime(Odd_Ibet.TimeNonLive))
                    {
                        continue;
                    }
                    if (UtilSoccer.ChuanTenLeauge_Sbo(Odd_Sbo.LeaugeName) == UtilSoccer.ChuanTenLeauge_Ibet(Odd_Ibet.LeaugeName))
                    {
                        if (UtilSoccer.ChuanTenTeam_Sbo(Odd_Sbo.HomeName) == UtilSoccer.ChuanTenTeam_Ibet(Odd_Ibet.HomeName) && UtilSoccer.ChuanTenTeam_Sbo(Odd_Sbo.AwayName) == UtilSoccer.ChuanTenTeam_Ibet(Odd_Ibet.AwayName))
                        {
                            if (UtilSoccer.formatHdp(Odd_Sbo.hdp) == UtilSoccer.formatHdp(Odd_Ibet.hdp) && Odd_Sbo.BetType == Odd_Ibet.BetType)
                            {
                                Output.Add(Odd_Sbo.IdKeo, Odd_Ibet.IdKeo);
                                break;
                            }
                        }

                    }
                }
            }
            return Output;
        }

        public bool CheckLai(string Filter)
        {
            double oddsspreada = UtilSoccer.profit_odd(Odd1, Odd2);
            switch (Filter)
            {
                case "First_a":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.05) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.08) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.1) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.1) return false;
                            break;
                    }
                    break;
                case "First_b":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.06) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.09) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.11) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.11) return false;
                            break;
                    }
                    break;
                case "First_c":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.07) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.1) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.12) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.12) return false;
                            break;
                    }
                    break;
                case "First_d":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.07) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.1) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.12) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.12) return false;
                            break;
                    }
                    break;
                case "Second_a":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.08) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.1) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.12) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.12) return false;
                            break;
                    }
                    break;
                case "Second_b":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.09) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.11) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.13) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.13) return false;
                            break;
                    }
                    break;
                case "Second_c":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.1) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.12) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.14) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.14) return false;
                            break;
                    }
                    break;
                case "Second_d":
                    switch (BetType)
                    {
                        case "1":
                            if (oddsspreada < -0.11) return false;
                            break;
                        case "3":
                            if (oddsspreada < -0.13) return false;
                            break;
                        case "7":
                            if (oddsspreada < -0.15) return false;
                            break;
                        case "9":
                            if (oddsspreada < -0.15) return false;
                            break;
                    }
                    break;
            }
            return true;
        }
        public bool CheckTimeNonLive(DateTime TimeLimit)
        {
            DateTime TimeRun = Convert.ToDateTime(TimeNonLive);
            if (TimeRun < TimeLimit) return true;
            else return false;
        }
    }
}
