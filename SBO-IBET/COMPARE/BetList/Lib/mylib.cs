    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetList.Lib
{
    public class mylib
    {
        public static objCompare convertStringToCompare(string input)
        {
            try
            {
                if (input.Trim() != "")
                {
                    string[] arr = input.Split(',');
                    objCompare o = new objCompare();
                    o.home = arr[0];
                    o.away = arr[1];
                    o.type = arr[2];
                    o.hdp = arr[3];
                    o.odd1 = double.Parse(arr[4].Split('/')[0]);
                    o.odd2 = double.Parse(arr[4].Split('/')[1]);
                    o.keo1 = arr[5].Split('/')[0];
                    o.keo2 = arr[5].Split('/')[1];
                    o.profit = Math.Abs(double.Parse(arr[7]) * 100);
                    o.league = Math.Abs(double.Parse(arr[8]) * 100);
                    return o;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static List<objCompare> convertStringListCompare(string input)
        {
            List<objCompare> lst = new List<objCompare>();
            string[] arr = input.Split('\n');
            foreach(string s in arr)
            {
                if(convertStringToCompare(s)!=null)
                    lst.Add(convertStringToCompare(s));
            }
            return lst;
        }

        public static string updateKeo(string type, string keo)
        {
            if (type == "1" && keo == "h")
                return "[FT-Home]";
            else if (type == "1" && keo == "a")
                return "[FT-Away]";
            else if (type == "3" && keo == "h")
                return "[FT-Over]";
            else if (type == "3" && keo == "a")
                return "[FT-Under]";
            else if (type == "7" && keo == "h")
                return "[H1-Home]";
            else if (type == "7" && keo == "a")
                return "[H1-Away]";
            else if (type == "9" && keo == "h")
                return "[H1-Over]";
            else if (type == "9" && keo == "a")
                return "[H1-Under]";
            return "";
        }
    }
}