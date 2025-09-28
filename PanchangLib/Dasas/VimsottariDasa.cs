

using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class VimsottariDasa : NakshatraDasa, INakshatraDasa
    {
        public override Object Options => this.options.Clone();
        public override object SetOptions(Object a)
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
        public new ArrayList AntarDasa(DasaEntry di)
        {
            return base.AntarDasa(di);
        }
        public class UserOptions : ICloneable
        {
            public Division div = new Division(DivisionType.Rasi);
            public BodyName start_graha;
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

            [Visible("Varga")]
            public DivisionType Varga
            {
                get { return this.div.MultipleDivisions[0].Varga; }
                set { this.div = new Division(value); }
            }

            [Visible("Seed Nakshatra")]
            public StartBodyType SeedBody
            {
                get { return user_start_graha; }
                set
                {
                    user_start_graha = value;
                    switch (value)
                    {
                        case StartBodyType.Lagna:
                            start_graha = BodyName.Lagna;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Jupiter:
                            start_graha = BodyName.Jupiter;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Moon:
                            start_graha = BodyName.Moon;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Utpanna:
                            start_graha = BodyName.Moon;
                            nakshatra_offset = 5;
                            break;
                        case StartBodyType.Kshema:
                            start_graha = BodyName.Moon;
                            nakshatra_offset = 4;
                            break;
                        case StartBodyType.Aadhaana:
                            start_graha = BodyName.Moon;
                            nakshatra_offset = 8;
                            break;
                        case StartBodyType.Maandi:
                            start_graha = BodyName.Maandi;
                            nakshatra_offset = 1;
                            break;
                        case StartBodyType.Gulika:
                            start_graha = BodyName.Gulika;
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

            Strongest fs_graha = new Strongest(h, new Division(DivisionType.BhavaPada), Strongest.RulesVimsottariGraha(h));
            BodyName stronger = fs_graha.StrongerGraha(BodyName.Moon, BodyName.Lagna, false);

            if (stronger == BodyName.Lagna)
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
        private BodyName NextDasaLordHelper(BodyName b)
        {
            switch (b)
            {
                case BodyName.Sun: return BodyName.Moon;
                case BodyName.Moon: return BodyName.Mars;
                case BodyName.Mars: return BodyName.Rahu;
                case BodyName.Rahu: return BodyName.Jupiter;
                case BodyName.Jupiter: return BodyName.Saturn;
                case BodyName.Saturn: return BodyName.Mercury;
                case BodyName.Mercury: return BodyName.Ketu;
                case BodyName.Ketu: return BodyName.Venus;
                case BodyName.Venus: return BodyName.Sun;
            }
            Trace.Assert(false, "VimsottariDasa::nextDasaLord");
            return BodyName.Lagna;
        }
        public double LengthOfDasa(BodyName plt)
        {
            return VimsottariDasa.LengthOfDasaS(plt);
        }
        public static double LengthOfDasaS(BodyName plt)
        {
            switch (plt)
            {
                case BodyName.Sun: return 6;
                case BodyName.Moon: return 10;
                case BodyName.Mars: return 7;
                case BodyName.Rahu: return 18;
                case BodyName.Jupiter: return 16;
                case BodyName.Saturn: return 19;
                case BodyName.Mercury: return 17;
                case BodyName.Ketu: return 7;
                case BodyName.Venus: return 20;
            }
            Trace.Assert(false, "Vimsottari::lengthOfDasa");
            return 0;
        }

        public BodyName LordOfNakshatra(Nakshatra n)
        {
            return VimsottariDasa.LordOfNakshatraS(n);
        }
        public static BodyName LordOfNakshatraS(Nakshatra n)
        {
            BodyName[] lords = new BodyName[9]
                {
                    BodyName.Mercury,
                    BodyName.Ketu, BodyName.Venus, BodyName.Sun,
                    BodyName.Moon, BodyName.Mars, BodyName.Rahu,
                    BodyName.Jupiter, BodyName.Saturn
                };
            int nak_val = ((int)n.Value) % 9;
            return lords[nak_val];
        }
        public new void DivisionChanged(Division div)
        {
            UserOptions uoNew = (UserOptions)this.options.Clone();
            uoNew.div = (Division)div.Clone();
            this.SetOptions(uoNew);
        }
    }
}
