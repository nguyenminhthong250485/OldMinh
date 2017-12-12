using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARE
{
    public interface IFunction
    {      
        string getMessage();
        string getTotalMatch();
        string getMatchs();
        void login();
        List<objMatch> getMatchOddNonLive(DateTime TimeLimit);
        List<objMatch> getMatchOddLive();

    }
}
