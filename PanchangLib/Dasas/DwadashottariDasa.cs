

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class DwadashottariDasa : NakshatraDasa, INakshatraDasa
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
			return ("Dwadashottari Dasa");
		}
		public DwadashottariDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double ParamAyus ()
		{
			return 112.0;
		}
		public int NumberOfDasaItems ()
		{
			return 8;
		}
		public DasaEntry NextDasaLord (DasaEntry di) 
		{
			return new DasaEntry (NextDasaLordHelper(di.graha), 0, 0, di.level, "");
		}
		private BodyName NextDasaLordHelper (BodyName b)
		{
			switch (b)
			{
				case BodyName.Sun: return BodyName.Jupiter;
				case BodyName.Jupiter: return BodyName.Ketu;
				case BodyName.Ketu : return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Rahu;
				case BodyName.Rahu : return BodyName.Mars;
				case BodyName.Mars: return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Moon;
				case BodyName.Moon : return BodyName.Sun;
			}
			Trace.Assert (false, "DwadashottariDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa (BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 7;
				case BodyName.Jupiter: return 9;
				case BodyName.Ketu: return 11;
				case BodyName.Mercury: return 13;
				case BodyName.Rahu: return 15;
				case BodyName.Mars: return 17;
				case BodyName.Saturn: return 19;
				case BodyName.Moon: return 21;
			}
			Trace.Assert (false, "Dwadashottari::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[8] 
			{
				BodyName.Sun, BodyName.Jupiter, BodyName.Ketu,
				BodyName.Mercury, BodyName.Rahu, BodyName.Mars,
				BodyName.Saturn, BodyName.Moon
			};				
			int nak_val = ((int)n.Value);
			int rev_val = (int)NakshatraName.Revati;
			int diff_val = Basics.Normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				rev_val - nak_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
