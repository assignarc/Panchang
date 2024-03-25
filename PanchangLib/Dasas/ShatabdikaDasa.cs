

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShatabdikaDasa : NakshatraDasa, INakshatraDasa
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
			return ("Shatabdika Dasa");
		}
		public ShatabdikaDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double paramAyus ()
		{
			return 100.0;
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
				case Body.Name.Sun: return Body.Name.Moon;
				case Body.Name.Moon: return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Jupiter;
				case Body.Name.Jupiter : return Body.Name.Mars;
				case Body.Name.Mars: return Body.Name.Saturn;
				case Body.Name.Saturn : return Body.Name.Sun;
			}
			Trace.Assert (false, "ShatabdikaDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double lengthOfDasa (Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Sun: return 5;
				case Body.Name.Moon: return 5;
				case Body.Name.Venus: return 10;
				case Body.Name.Mercury: return 10;
				case Body.Name.Jupiter: return 20;
				case Body.Name.Mars: return 20;
				case Body.Name.Saturn: return 30;
			}
			Trace.Assert (false, "ShatabdikaDasa::lengthOfDasa");
			return 0;
		}
		public Body.Name lordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[7] 
			{
				Body.Name.Sun, Body.Name.Moon, Body.Name.Venus,
				Body.Name.Mercury, Body.Name.Jupiter, Body.Name.Mars,
				Body.Name.Saturn
			};				
			int nak_val = ((int)n.value);
			int rev_val = (int)NakshatraName.Revati;
			int diff_val = Basics.normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				nak_val - rev_val);
			int diff_off = diff_val % 7;
			return lords[diff_off];
		}
	}
}
