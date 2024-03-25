using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    /// <summary>
    /// Mutually exclusive classes of BodyTypes
    /// </summary>
    public class BodyType
    {
        public enum Name : int
        {
            Lagna, Graha, NonLunarNode,
            SpecialLagna, ChandraLagna,
            BhavaArudha, BhavaArudhaSecondary, GrahaArudha,
            Varnada, Upagraha, Sahama, Other
        }
    }
}
