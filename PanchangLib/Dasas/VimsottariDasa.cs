

using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class VimsottariDasa : NakshatraDasa, INakshatraDasa
    {
        override public Object Options => this.options.Clone();
        override public object SetOptions(Object a)
        {
            UserOptions uo = (UserOptions)a;
            bool bRecalculate = false;
            if (options.SeedBody != uo.SeedBody)
            {
                options.SeedBody = uo.SeedBody;
                bRecalculate = true;
            }
            if (options.div != uo.div)
            {
                options.div = uo.div;
                bRecalculate = true;
            }

            if (bRecalculate == true && RecalculateEvent != null)
                RecalculateEvent();

            return options.Clone();
        }

        public ArrayList Dasa(int cycle)
        {
            return Dasa(horoscope.GetPosition(options.start_graha).ExtrapolateLongitude(options.div), options.nakshatra_offset, cycle);
        }
        public ArrayList AntarDasa(DasaEntry di)
        {
            return base.AntarDasa(di);
        }
        public class UserOptions : ICloneable
        {
            public Division div = new Division(DivisionType.Rasi);
            public Body.Name start_graha;
            public int nakshatra_offset;
            public StartBodyType user_start_graha;
            public Object Clone()
            {
                return new UserOptions
                {
                    start_graha = start_graha,
                    nakshatra_offset = nakshatra_offset,
                    SeedBody = SeedBody,
                    div = (Division)div.Clone()
                };
            }

            [org.transliteral.panchang.DisplayName("Varga")]
            public DivisionType Varga
            {
                get { return this.div.MultipleDivisions[0].Varga; }
                set { this.div = new Division(value); }
            }

            [org.transliteral.panchang.DisplayName("Seed Nakshatra")]
            public StartBodyType SeedBody
            {
                get { return user_start_graha; }
                set
                {
                    user_start_graha = value;
                    switch (value)
                    {
                        case StartBodyType.Lagna:
                            start_graha = Body.Name.Lagna;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Jupiter:
                            start_graha = Body.Name.Jupiter;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Moon:
                            start_graha = Body.Name.Moon;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Utpanna:
                            start_graha = Body.Name.Moon;
                            nakshatra_offset = 5;
                            break;
                        case StartBodyType.Kshema:
                            start_graha = Body.Name.Moon;
                            nakshatra_offset = 4;
                            break;
                        case StartBodyType.Aadhaana:
                            start_graha = Body.Name.Moon;
                            nakshatra_offset = 8;
                            break;
                        case StartBodyType.Maandi:
                            start_graha = Body.Name.Maandi;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Gulika:
                            start_graha = Body.Name.Gulika;
                            nakshatra_offset = 1;
                            break;
                    }
                }
            }
        }
        public UserOptions options;
        public Horoscope horoscope;
        public String Description()
        {
            return ("Vimsottari Dasa Seeded from " + options.SeedBody.ToString());
        }
        public VimsottariDasa(Horoscope h)
        {
            common = this;
            options = new UserOptions();
            horoscope = h;

            FindStronger fs_graha = new FindStronger(h, new Division(DivisionType.BhavaPada), FindStronger.RulesVimsottariGraha(h));
            Body.Name stronger = fs_graha.StrongerGraha(Body.Name.Moon, Body.Name.Lagna, false);

            if (stronger == Body.Name.Lagna)
                options.SeedBody = StartBodyType.Lagna;
            else
                options.SeedBody = StartBodyType.Moon;
            h.Changed += new EvtChanged(ChangedHoroscope);
        }
        public void ChangedHoroscope(Object o)
        {
            Horoscope h = (Horoscope)o;
            OnChanged();
        }
        public double ParamAyus()
        {
            return 120.0;
        }
        public int NumberOfDasaItems()
        {
            return 9;
        }
        public DasaEntry NextDasaLord(DasaEntry di)
        {
            return new DasaEntry(NextDasaLordHelper(di.graha), 0, 0, di.level, "");
        }
        private Body.Name NextDasaLordHelper(Body.Name b)
        {
            switch (b)
            {
                case Body.Name.Sun: return Body.Name.Moon;
                case Body.Name.Moon: return Body.Name.Mars;
                case Body.Name.Mars: return Body.Name.Rahu;
                case Body.Name.Rahu: return Body.Name.Jupiter;
                case Body.Name.Jupiter: return Body.Name.Saturn;
                case Body.Name.Saturn: return Body.Name.Mercury;
                case Body.Name.Mercury: return Body.Name.Ketu;
                case Body.Name.Ketu: return Body.Name.Venus;
                case Body.Name.Venus: return Body.Name.Sun;
            }
            Trace.Assert(false, "VimsottariDasa::nextDasaLord");
            return Body.Name.Lagna;
        }
        public double LengthOfDasa(Body.Name plt)
        {
            return VimsottariDasa.LengthOfDasaS(plt);
        }
        public static double LengthOfDasaS(Body.Name plt)
        {
            switch (plt)
            {
                case Body.Name.Sun: return 6;
                case Body.Name.Moon: return 10;
                case Body.Name.Mars: return 7;
                case Body.Name.Rahu: return 18;
                case Body.Name.Jupiter: return 16;
                case Body.Name.Saturn: return 19;
                case Body.Name.Mercury: return 17;
                case Body.Name.Ketu: return 7;
                case Body.Name.Venus: return 20;
            }
            Trace.Assert(false, "Vimsottari::lengthOfDasa");
            return 0;
        }

        public Body.Name LordOfNakshatra(Nakshatra n)
        {
            return VimsottariDasa.LordOfNakshatraS(n);
        }
        public static Body.Name LordOfNakshatraS(Nakshatra n)
        {
            Body.Name[] lords = new Body.Name[9]
                {
                    Body.Name.Mercury,
                    Body.Name.Ketu, Body.Name.Venus, Body.Name.Sun,
                    Body.Name.Moon, Body.Name.Mars, Body.Name.Rahu,
                    Body.Name.Jupiter, Body.Name.Saturn
                };
            int nak_val = ((int)n.Value) % 9;
            return lords[nak_val];
        }
        new public void DivisionChanged(Division div)
        {
            UserOptions uoNew = (UserOptions)this.options.Clone();
            uoNew.div = (Division)div.Clone();
            this.SetOptions(uoNew);
        }
    }
}
