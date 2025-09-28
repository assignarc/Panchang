

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class SudarshanaChakraDasa : Dasa, IDasa
	{
		private Horoscope h;
        public new object Options => new Object();
        public SudarshanaChakraDasa (Horoscope _h)
		{
			h = _h;
		}
		public double ParamAyus ()
		{
			return 12;
		}
		public string Description ()
		{
			return "Sudarshana Chakra Dasa";
		}
        
        public object SetOptions (object o)
		{
			return o;
		}
		public void RecalculateOptions ()
		{
		}
		public ArrayList Dasa (int cycle)
		{
			ArrayList al = new ArrayList(12);
			double start = cycle * ParamAyus();
			ZodiacHouse lzh = h.GetPosition(BodyName.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse czh = lzh.Add(i);
				al.Add (new DasaEntry(czh.Value, start, 1, 1, czh.Value.ToString()));
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
				ZodiacHouse czh = zh.Add(i);
				al.Add (new DasaEntry(czh.Value, start, length, de.level+1, 
					de.shortDesc + " " + czh.Value.ToString()));
				start += length;
			}
			return al;
		}
	}
}
