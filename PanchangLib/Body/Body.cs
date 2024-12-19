using System.ComponentModel;
using System.Diagnostics;

namespace org.transliteral.panchang
{

    /// <summary>
    ///  A compile-time list of every body we will use in this program
    /// </summary>
    public class Body
    {
       
        public static int ToInt(BodyName b)
        {
            return ((int)b);
        }
        public static Longitude ExaltationDegree(BodyName b)
        {
            int _b = (int)b;
            Debug.Assert(_b >= (int)BodyName.Sun && _b <= (int)BodyName.Saturn);
            double d = 0;
            switch (b)
            {
                case BodyName.Sun: d = 10; break;
                case BodyName.Moon: d = 33; break;
                case BodyName.Mars: d = 298; break;
                case BodyName.Mercury: d = 165; break;
                case BodyName.Jupiter: d = 95; break;
                case BodyName.Venus: d = 357; break;
                case BodyName.Saturn: d = 200; break;
            }
            return new Longitude(d);
        }
        public static Longitude DebilitationDegree(BodyName b)
        {
            return ExaltationDegree(b).Add(180);
        }
        public static string ToString(BodyName b)
        {
            switch (b)
            {
                case BodyName.Lagna: return "Lagna";
                case BodyName.Sun: return "Sun";
                case BodyName.Moon: return "Moon";
                case BodyName.Mars: return "Mars";
                case BodyName.Mercury: return "Mercury";
                case BodyName.Jupiter: return "Jupiter";
                case BodyName.Venus: return "Venus";
                case BodyName.Saturn: return "Saturn";
                case BodyName.Rahu: return "Rahu";
                case BodyName.Ketu: return "Ketu";
            }
            return "";
        }
        public static string ToShortString(BodyName b)
        {
            switch (b)
            {
                case BodyName.Lagna: return "As";
                case BodyName.Sun: return "Su";
                case BodyName.Moon: return "Mo";
                case BodyName.Mars: return "Ma";
                case BodyName.Mercury: return "Me";
                case BodyName.Jupiter: return "Ju";
                case BodyName.Venus: return "Ve";
                case BodyName.Saturn: return "Sa";
                case BodyName.Rahu: return "Ra";
                case BodyName.Ketu: return "Ke";
                case BodyName.AL: return "AL";
                case BodyName.A2: return "A2";
                case BodyName.A3: return "A3";
                case BodyName.A4: return "A4";
                case BodyName.A5: return "A5";
                case BodyName.A6: return "A6";
                case BodyName.A7: return "A7";
                case BodyName.A8: return "A8";
                case BodyName.A9: return "A9";
                case BodyName.A10: return "A10";
                case BodyName.A11: return "A11";
                case BodyName.UL: return "UL";
                case BodyName.GhatiLagna: return "GL";
                case BodyName.BhavaLagna: return "BL";
                case BodyName.HoraLagna: return "HL";
                case BodyName.VighatiLagna: return "ViL";
                case BodyName.SreeLagna: return "SL";
                case BodyName.Pranapada: return "PL";
            }
            Trace.Assert(false, "Basics.Body.toShortString");
            return "   ";
        }
    }

}
