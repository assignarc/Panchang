

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class PanchottariDasa : NakshatraDasa, INakshatraDasa
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
			return ("Panchottari Dasa");
		}
		public PanchottariDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double paramAyus ()
		{
			return 105.0;
		}
		public int numberOfDasaItems ()
		{
			return 7;
		}
		public DasaEntry nextDasaLord (DasaEntry di) 
		{
			return new DasaEntry (nextDasaLordHelper(di.graha), 0, 0, di.level, "");
		}
		private Body.Name nextDasaLordHelper (Body.Name b)
		{
			switch (b)
			{
				case Body.Name.Sun: return Body.Name.Mercury;
				case Body.Name.Mercury: return Body.Name.Saturn;
				case Body.Name.Saturn : return Body.Name.Mars;
				case Body.Name.Mars : return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Moon;
				case Body.Name.Moon: return Body.Name.Jupiter;
				case Body.Name.Jupiter : return Body.Name.Sun;
			}
			Trace.Assert (false, "DwadashottariDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double lengthOfDasa (Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Sun: return 12;
				case Body.Name.Mercury: return 13;
				case Body.Name.Saturn: return 14;
				case Body.Name.Mars: return 15;
				case Body.Name.Venus: return 16;
				case Body.Name.Moon: return 17;
				case Body.Name.Jupiter: return 18;
			}
			Trace.Assert (false, "Panchottari::lengthOfDasa");
			return 0;
		}
		public Body.Name lordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[7] 
			{
				Body.Name.Sun, Body.Name.Mercury, Body.Name.Saturn,
				Body.Name.Mars, Body.Name.Venus, Body.Name.Moon,
				Body.Name.Jupiter
			};				
			int nak_val = ((int)n.value);
			int anu_val = (int)NakshatraName.Anuradha;
			int diff_val = Basics.normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - anu_val);
			int diff_off = diff_val % 7;
			return lords[diff_off];
		}
	}
}
