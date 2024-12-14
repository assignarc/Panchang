

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class SudarshanaChakraDasa : Dasa, IDasa
	{
		private Horoscope h;
		public SudarshanaChakraDasa (Horoscope _h)
		{
			h = _h;
		}
		public double paramAyus ()
		{
			return 12;
		}
		public string Description ()
		{
			return "Sudarshana Chakra Dasa";
		}
		public object GetOptions ()
		{
			return new Object();
		}
		public object SetOptions (object o)
		{
			return o;
		}
		public void recalculateOptions ()
		{
		}
		public ArrayList Dasa (int cycle)
		{
			ArrayList al = new ArrayList(12);
			double start = cycle * paramAyus();
			ZodiacHouse lzh = h.getPosition(Body.Name.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse czh = lzh.add(i);
				al.Add (new DasaEntry(czh.value, start, 1, 1, czh.value.ToString()));
				start += 1;
			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry de)
		{
			ArrayList al = new ArrayList(12);
			double start = de.StartUT;
			double length = de.DasaLength / 12.0;
			ZodiacHouse zh = new ZodiacHouse(de.zodiacHouse);
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse czh = zh.add(i);
				al.Add (new DasaEntry(czh.value, start, length, de.level+1, 
					de.shortDesc + " " + czh.value.ToString()));
				start += length;
			}
			return al;
		}
	}
}
