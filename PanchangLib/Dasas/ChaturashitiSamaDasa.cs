

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ChaturashitiSamaDasa : NakshatraDasa, INakshatraDasa
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
			return ("Chaturashiti-Sama Dasa");
		}
		public ChaturashitiSamaDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double ParamAyus ()
		{
			return 84.0;
		}
		public int NumberOfDasaItems ()
		{
			return 7;
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
				case BodyName.Saturn : return BodyName.Sun;
			}
			Trace.Assert (false, "Chaturashiti Sama Dasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa (BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 12;
				case BodyName.Moon: return 12;
				case BodyName.Mars: return 12;
				case BodyName.Mercury: return 12;
				case BodyName.Jupiter: return 12;
				case BodyName.Venus: return 12;
				case BodyName.Saturn: return 12;
			}
			Trace.Assert (false, "ChaturashitiSama Dasa::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[7] 
			{
				BodyName.Sun, BodyName.Moon, BodyName.Mars,
				BodyName.Mercury, BodyName.Jupiter, BodyName.Venus,
				BodyName.Saturn
			};				
			int nak_val = ((int)n.Value);
			int sva_val = (int)NakshatraName.Swati;
			int diff_val = Basics.Normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - sva_val);
			int diff_off = diff_val % 7;
			return lords[diff_off];
		}
	}
}
