using org.transliteral.panchang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang.data
{
    public struct PDay
    {
        public Basics.Weekday LocalWeekday { get; set; }
        public Moment Moment { get; set; }
        public List<CelestialBody> Celestials { get; set; }
        public string Rahu { get; set; }
        public string Gulika { get; set; }
        public string Yama { get; set; }
        public Tithi Tithi { get; set; }
        public string TithiText { get; set; }
        public Karana Karana { get; set; }
        public string KaranaText {  get; set; }
        public string Yoga { get; set; }
        public Nakshatra Nakshatra { get; set; }
        public Dictionary<string, string> Hora { get; set; }
        public Dictionary< string, string> Kala { get; set; }
    }
    public struct CelestialBody
    {
        public Body.Name Name { get; set; }
        public string Rise { get; set; }
        public string Set { get; set; }
    }
}
