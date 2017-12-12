using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
{
    public interface IFunction
    {
        string getPhieuchung();
        string getRealMoney();
        string getMessage();
        bool checklogin();
        void login();
        string getCredit();
        string getBetList();
        void playBetNonLive(ticket o, string money, string IsLive, string phieuchung, string group, bool AutoAccept = true);
        void playBetLive(ticket o, string money, string IsLive, string phieuchung, string group, bool AutoAccept = true);
        string getOddChange(ticket o, string money, string IsLive);
    }
}
