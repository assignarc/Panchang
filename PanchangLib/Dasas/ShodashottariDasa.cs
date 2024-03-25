

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShodashottariDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
		override public Object GetOptions ()
		{
			return new Object();
		}
		override public object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			return _Dasa (h.getPosition(Body.Name.Moon).longitude, 1, cycle );
		}
		public ArrayList AntarDasa (DasaEntry di)
		{
			return _AntarDasa (di);
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

		public double paramAyus ()
		{
			return 116.0;
		}
		public int numberOfDasaItems ()
		{
			return 8;
		}
		public DasaEntry nextDasaLord (DasaEntry di) 
		{
			return new DasaEntry (nextDasaLordHelper(di.graha), 0, 0, di.level, "");
		}
		private Body.Name nextDasaLordHelper (Body.Name b)
		{
			switch (b)
			{
				case Body.Name.Sun: return Body.Name.Mars;
				case Body.Name.Mars: return Body.Name.Jupiter;
				case Body.Name.Jupiter : return Body.Name.Saturn;
				case Body.Name.Saturn : return Body.Name.Ketu;
				case Body.Name.Ketu : return Body.Name.Moon;
				case Body.Name.Moon: return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Sun;
			}
			Trace.Assert (false, "ShodashottariDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double lengthOfDasa (Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Sun: return 11;
				case Body.Name.Mars: return 12;
				case Body.Name.Jupiter: return 13;
				case Body.Name.Saturn: return 14;
				case Body.Name.Ketu: return 15;
				case Body.Name.Moon: return 16;
				case Body.Name.Mercury: return 17;
				case Body.Name.Venus: return 18;
			}
			Trace.Assert (false, "Shodashottari::lengthOfDasa");
			return 0;
		}
		public Body.Name lordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[8] 
			{
				Body.Name.Sun, Body.Name.Mars, Body.Name.Jupiter,
				Body.Name.Saturn, Body.Name.Ketu, Body.Name.Moon,
				Body.Name.Mercury, Body.Name.Venus
			};				
			int nak_val = ((int)n.value);
			int pus_val = (int)NakshatraName.Pushya;
			int diff_val = Basics.normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - pus_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
