using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public class BodyKarakaComparer : IComparable
    {
        BodyPosition bpa;
        public BodyPosition GetPosition
        {
            get { return bpa; }
            set { this.bpa = value; }
        }
        public BodyKarakaComparer(BodyPosition _bp)
        {
            bpa = _bp;
        }
        public double getOffset()
        {
            double off = bpa.longitude.toZodiacHouseOffset();
            if (bpa.name == Body.Name.Rahu)
                off = 30.0 - off;
            return off;
        }
        public int CompareTo(object obj)
        {
            Debug.Assert(obj is BodyKarakaComparer);
            double offa = this.getOffset();
            double offb = ((BodyKarakaComparer)obj).getOffset();
            return offb.CompareTo(offa);
        }
    }

}
