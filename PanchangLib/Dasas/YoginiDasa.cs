

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class YoginiDasa : NakshatraDasa, INakshatraDasa
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
			return ("Yogini Dasa");
		}
		public YoginiDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

		public double paramAyus ()
		{
			return 36.0;
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
				case Body.Name.Moon: return Body.Name.Sun;
				case Body.Name.Sun: return Body.Name.Jupiter;
				case Body.Name.Jupiter : return Body.Name.Mars;
				case Body.Name.Mars : return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Saturn;
				case Body.Name.Saturn: return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Rahu;
				case Body.Name.Rahu : return Body.Name.Moon;
			}
			Trace.Assert (false, "YoginiDasa::nextDasaLord");
			return Body.Name.Sun;
		}
		public double lengthOfDasa (Body.Name plt)
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
			Trace.Assert (false, "YoginiDasa::lengthOfDasa");
			return 0;
		}
		public Body.Name lordOfNakshatra(Nakshatra n) 
		{
			Body.Name[] lords = new Body.Name[]
			{
				Body.Name.Moon, Body.Name.Sun, Body.Name.Jupiter, Body.Name.Mars,
				Body.Name.Mercury, Body.Name.Saturn, Body.Name.Venus, Body.Name.Rahu
			};

			int index = ((int)n.value+3)%8;
			if (index == 0) index =  8;
			index--;
			return (Body.Name)(lords[index]);
		}
	}
}
