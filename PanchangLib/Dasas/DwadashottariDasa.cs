

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class DwadashottariDasa : NakshatraDasa, INakshatraDasa
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
			return _Dasa (h.getPosition(Body.Name.Moon).Longitude, 1, cycle );
		}
		public ArrayList AntarDasa (DasaEntry di)
		{
			return _AntarDasa (di);
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

		public double paramAyus ()
		{
			return 112.0;
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
				case Body.Name.Sun: return Body.Name.Jupiter;
				case Body.Name.Jupiter: return Body.Name.Ketu;
				case Body.Name.Ketu : return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Rahu;
				case Body.Name.Rahu : return Body.Name.Mars;
				case Body.Name.Mars: return Body.Name.Saturn;
				case Body.Name.Saturn : return Body.Name.Moon;
				case Body.Name.Moon : return Body.Name.Sun;
			}
			Trace.Assert (false, "DwadashottariDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double lengthOfDasa (Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Sun: return 7;
				case Body.Name.Jupiter: return 9;
				case Body.Name.Ketu: return 11;
				case Body.Name.Mercury: return 13;
				case Body.Name.Rahu: return 15;
				case Body.Name.Mars: return 17;
				case Body.Name.Saturn: return 19;
				case Body.Name.Moon: return 21;
			}
			Trace.Assert (false, "Dwadashottari::lengthOfDasa");
			return 0;
		}
		public Body.Name lordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[8] 
			{
				Body.Name.Sun, Body.Name.Jupiter, Body.Name.Ketu,
				Body.Name.Mercury, Body.Name.Rahu, Body.Name.Mars,
				Body.Name.Saturn, Body.Name.Moon
			};				
			int nak_val = ((int)n.value);
			int rev_val = (int)NakshatraName.Revati;
			int diff_val = Basics.Normalize_inc(
				(int)NakshatraName.Aswini, (int)NakshatraName.Revati, 
				rev_val - nak_val);
			int diff_off = diff_val % 8;
			return lords[diff_off];
		}
	}
}
