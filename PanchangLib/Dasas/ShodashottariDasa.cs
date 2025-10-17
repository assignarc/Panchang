

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShodashottariDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        public override Object Options => new Object();
        public override object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			return Dasa (h.GetPosition(BodyName.Moon).Longitude, 1, cycle );
		}
		public new ArrayList AntarDasa (DasaEntry di)
		{
			return base.AntarDasa (di);
		}
		public String Description ()
		{
			return ("Shodashottari Dasa");
		}
		public ShodashottariDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double ParamAyus ()
		{
			return 116.0;
		}
		public int NumberOfDasaItems ()
		{
			return 8;
		}
		public DasaEntry NextDasaLord(DasaEntry di) 
		{
			return new DasaEntry (NextDasaLordHelper(di.graha), 0, 0, di.level, "");
		}
        private BodyName NextDasaLordHelper(BodyName b)
		{
			switch (b)
			{
				case BodyName.Sun: return BodyName.Mars;
				case BodyName.Mars: return BodyName.Jupiter;
				case BodyName.Jupiter : return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Ketu;
				case BodyName.Ketu : return BodyName.Moon;
				case BodyName.Moon: return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Venus;
				case BodyName.Venus : return BodyName.Sun;
			}
			Trace.Assert (false, "ShodashottariDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa(BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 11;
				case BodyName.Mars: return 12;
				case BodyName.Jupiter: return 13;
				case BodyName.Saturn: return 14;
				case BodyName.Ketu: return 15;
				case BodyName.Moon: return 16;
				case BodyName.Mercury: return 17;
				case BodyName.Venus: return 18;
			}
			Trace.Assert (false, "Shodashottari::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[8] 
			{
				BodyName.Sun, BodyName.Mars, BodyName.Jupiter,
				BodyName.Saturn, BodyName.Ketu, BodyName.Moon,
				BodyName.Mercury, BodyName.Venus
			};				
			int nak_val = ((int)n.Value);
			int pus_val = (int)NakshatraName.Pushya;
			int diff_val = Basics.NormalizeInclusive(
				(int)NakshatraName.Ashwini, (int)NakshatraName.Revati, 
				nak_val - pus_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
