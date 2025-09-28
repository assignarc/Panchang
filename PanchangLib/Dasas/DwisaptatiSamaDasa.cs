

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class DwisaptatiSamaDasa : NakshatraDasa, INakshatraDasa
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
			return ("Dwisaptati Sama Dasa");
		}
		public DwisaptatiSamaDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double ParamAyus ()
		{
			return 72.0;
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
				case BodyName.Sun: return BodyName.Moon;
				case BodyName.Moon: return BodyName.Mars;
				case BodyName.Mars : return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Jupiter;
				case BodyName.Jupiter : return BodyName.Venus;
				case BodyName.Venus: return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Rahu;
				case BodyName.Rahu: return BodyName.Sun;
			}
			Trace.Assert (false, "DwisaptatiSamaDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa (BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 9;
				case BodyName.Moon: return 9;
				case BodyName.Mars: return 9;
				case BodyName.Mercury: return 9;
				case BodyName.Jupiter: return 9;
				case BodyName.Venus: return 9;
				case BodyName.Saturn: return 9;
				case BodyName.Rahu: return 9;
			}
			Trace.Assert (false, "DwisaptatiSamaDasa::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[8] 
			{
				BodyName.Sun, BodyName.Moon, BodyName.Mars,
				BodyName.Mercury, BodyName.Jupiter, BodyName.Venus,
				BodyName.Saturn, BodyName.Rahu
			};				
			int nak_val = ((int)n.Value);
			int moo_val = (int)NakshatraName.Moola;
			int diff_val = Basics.NormalizeInclusive(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - moo_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
