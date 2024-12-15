

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShatTrimshaSamaDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        override public Object Options => new Object();
        override public object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			return Dasa (h.GetPosition(Body.Name.Moon).Longitude, 1, cycle );
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
        private Body.Name NextDasaLordHelper(Body.Name b)
		{
			switch (b)
			{
				case Body.Name.Moon: return Body.Name.Sun;
				case Body.Name.Sun: return Body.Name.Jupiter;
				case Body.Name.Jupiter : return Body.Name.Mars;
				case Body.Name.Mars : return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Saturn;
				case Body.Name.Saturn: return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Rahu;
				case Body.Name.Rahu: return Body.Name.Moon;
			}
			Trace.Assert (false, "ShatTrimshaSamaDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double LengthOfDasa(Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Moon: return 1;
				case Body.Name.Sun: return 2;
				case Body.Name.Jupiter: return 3;
				case Body.Name.Mars: return 4;
				case Body.Name.Mercury: return 5;
				case Body.Name.Saturn: return 6;
				case Body.Name.Venus: return 7;
				case Body.Name.Rahu: return 8;
			}
			Trace.Assert (false, "ShatTrimshaSamaDasa::lengthOfDasa");
			return 0;
		}
		public Body.Name LordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[8] 
			{
				Body.Name.Moon, Body.Name.Sun, Body.Name.Jupiter,
				Body.Name.Mars, Body.Name.Mercury, Body.Name.Saturn,
				Body.Name.Venus, Body.Name.Rahu
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
