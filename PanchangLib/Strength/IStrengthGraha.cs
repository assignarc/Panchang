using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public interface IStrengthGraha
    {
        bool stronger(Body.Name m, Body.Name n);
    }

}
