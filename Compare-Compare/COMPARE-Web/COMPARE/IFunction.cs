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
        void login();
        List<objMatch> getMatchOddNonLive();

    }
}
