using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
{
    public class ticket
    {
        public string home;
        public string away;
        public string bettype;
        public double hdp;
        public double odd;
        public string choose;
        public string keoid;
        public string group;
        public string score;

        public override string ToString()
        {
            return string.Format("{0} -vs- {1}, HDP: {2}, Choose: {3} ({4})", home, away, hdp, mylib.updateKeo(bettype, choose), odd);
        }
    }
}
