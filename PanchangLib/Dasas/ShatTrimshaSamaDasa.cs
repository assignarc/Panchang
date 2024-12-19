

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShatTrimshaSamaDasa : NakshatraDasa, INakshatraDasa
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
		public ArrayList AntarDasa (DasaEntry di)
		{
			return base.AntarDasa (di);
		}
		public String Description ()
		{
			return ("ShatTrimsha Sama Dasa");
		}
		public ShatTrimshaSamaDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double ParamAyus ()
		{
			return 36.0;
		}
		public int NumberOfDasaItems ()
		{
			return 8;
		}
		public DasaEntry NextDasaLord (DasaEntry di) 
		{
			return new DasaEntry (NextDasaLordHelper(di.graha), 0, 0, di.level, "");
		}
        private BodyName NextDasaLordHelper(BodyName b)
		{
			switch (b)
			{
				case BodyName.Moon: return BodyName.Sun;
				case BodyName.Sun: return BodyName.Jupiter;
				case BodyName.Jupiter : return BodyName.Mars;
				case BodyName.Mars : return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Saturn;
				case BodyName.Saturn: return BodyName.Venus;
				case BodyName.Venus : return BodyName.Rahu;
				case BodyName.Rahu: return BodyName.Moon;
			}
			Trace.Assert (false, "ShatTrimshaSamaDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa(BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Moon: return 1;
				case BodyName.Sun: return 2;
				case BodyName.Jupiter: return 3;
				case BodyName.Mars: return 4;
				case BodyName.Mercury: return 5;
				case BodyName.Saturn: return 6;
				case BodyName.Venus: return 7;
				case BodyName.Rahu: return 8;
			}
			Trace.Assert (false, "ShatTrimshaSamaDasa::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[8] 
			{
				BodyName.Moon, BodyName.Sun, BodyName.Jupiter,
				BodyName.Mars, BodyName.Mercury, BodyName.Saturn,
				BodyName.Venus, BodyName.Rahu
			};				
			int nak_val = ((int)n.Value);
			int shr_val = (int)NakshatraName.Sravana;
			int diff_val = Basics.Normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - shr_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
