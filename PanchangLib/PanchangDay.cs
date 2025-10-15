using System;
using System.Collections.Generic;

namespace org.transliteral.panchang.data
{
    public struct PanchangDay
    {
        public Weekday LocalWeekday { get; set; }
        public Moment Moment { get; set; }
        public List<CelestialBody> Celestials { get; set; }

        public List<KeyValuePair<Tithi, string>> Tithi { get; set; }
        public List<KeyValuePair<Karana, string>> Karana { get; set; }
        public List<KeyValuePair<SunMoonYoga, string>> Yoga { get; set; }
        public List<KeyValuePair<Nakshatra,string>> Nakshatra { get; set; }
        public List<KeyValuePair<ZodiacHouse, string>> Lagna { get; set; }
        public List<KeyValuePair<BodyName, string>> Hora { get; set; }

        public List<KeyValuePair<BodyName, string>> Kala { get; set; }
        public List<KeyValuePair<string, string>> SpecialKala { get; set; }

        public Dictionary<string,string> Texts { get; set; }

        public static PanchangDay New()
        {
            return new PanchangDay()
            {
                Celestials = new List<CelestialBody>(),
                Hora = new List<KeyValuePair<BodyName, string>>(),
                Kala = new List<KeyValuePair<BodyName, string>>(),
                Nakshatra = new List<KeyValuePair<Nakshatra, string>>(),
                Lagna = new List<KeyValuePair<ZodiacHouse, string>>(),
                Tithi = new List<KeyValuePair<Tithi, string>>(),
                Karana = new List<KeyValuePair<Karana, string>>(),
                Yoga = new List<KeyValuePair<SunMoonYoga, string>>(),
                SpecialKala = new List<KeyValuePair<string, string>>(),
                Texts = new Dictionary<string, string>()

            };
        }
    }
    public struct CelestialBody
    {
        public BodyName Name { get; set; }
        public TimeSpan Rise { get; set; }
        public TimeSpan Set { get; set; }
    }
    public struct DaySpan
    {
        public string FROM { get; set; }
        public string TO { get; set; }
        public bool FULL { get; set; }
        public string UNTIL { get; set; }

        public override string ToString()
        {
            if (FULL)
                return "full day";
            else if (TO != FROM)
                return String.Format("from {0} to {1}", FROM, TO);
            else
                return String.Format("Until {0}", UNTIL);                 
        }
    }
}
