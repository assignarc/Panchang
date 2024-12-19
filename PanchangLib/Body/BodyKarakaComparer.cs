using System;
using System.Diagnostics;

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
        public double GetOffset()
        {
            double off = bpa.Longitude.ToZodiacHouseOffset();
            if (bpa.name == BodyName.Rahu)
                off = 30.0 - off;
            return off;
        }
        public int CompareTo(object obj)
        {
            Debug.Assert(obj is BodyKarakaComparer);
            double offa = this.GetOffset();
            double offb = ((BodyKarakaComparer)obj).GetOffset();
            return offb.CompareTo(offa);
        }
    }

}
