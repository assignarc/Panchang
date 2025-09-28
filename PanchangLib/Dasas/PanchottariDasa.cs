

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class PanchottariDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        public override object Options => new object();
        public override object SetOptions(Object a) => new object();
        public ArrayList Dasa(int cycle) => Dasa(h.GetPosition(BodyName.Moon).Longitude, 1, cycle);
        public new ArrayList AntarDasa(DasaEntry di) => base.AntarDasa(di);
        public String Description() => ("Panchottari Dasa");
        public PanchottariDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

        public double ParamAyus() => 105.0;
        public int NumberOfDasaItems() => 7;
        public DasaEntry NextDasaLord(DasaEntry di) => new DasaEntry(NextDasaLordHelper(di.graha), 0, 0, di.level, "");
        private BodyName NextDasaLordHelper(BodyName b)
		{
			switch (b)
			{
				case BodyName.Sun: return BodyName.Mercury;
				case BodyName.Mercury: return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Mars;
				case BodyName.Mars : return BodyName.Venus;
				case BodyName.Venus : return BodyName.Moon;
				case BodyName.Moon: return BodyName.Jupiter;
				case BodyName.Jupiter : return BodyName.Sun;
			}
			Trace.Assert (false, "DwadashottariDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa(BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 12;
				case BodyName.Mercury: return 13;
				case BodyName.Saturn: return 14;
				case BodyName.Mars: return 15;
				case BodyName.Venus: return 16;
				case BodyName.Moon: return 17;
				case BodyName.Jupiter: return 18;
			}
			Trace.Assert (false, "Panchottari::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[7] 
			{
				BodyName.Sun, BodyName.Mercury, BodyName.Saturn,
				BodyName.Mars, BodyName.Venus, BodyName.Moon,
				BodyName.Jupiter
			};				
			int nak_val = ((int)n.Value);
			int anu_val = (int)NakshatraName.Anuradha;
			int diff_val = Basics.NormalizeInclusive(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - anu_val);
			int diff_off = diff_val % 7;
			return lords[diff_off];
		}
	}
}
