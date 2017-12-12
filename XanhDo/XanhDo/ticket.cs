using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XanhDo
{
    public class ticket
    {
        public int id;
        public string memberid;
        public string name;
        public int matchid;
        public string companyid;
        public int game;
        public double money;

        public string toSql()
        {
            return string.Format("('{0}',N'{1}','{2}','{3}',{4},{5})", memberid, name, matchid, companyid, game, money);
        }
    }
}
