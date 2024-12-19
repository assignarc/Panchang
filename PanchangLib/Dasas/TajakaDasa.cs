

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class TajakaDasa: Dasa, IDasa
	{
		private Horoscope h;
		public TajakaDasa (Horoscope _h)
		{
			h = _h;
		}
        public Object Options => new Object();
        public object SetOptions (Object a)
		{
			return new Object();
		}
		public void RecalculateOptions ()
		{
		}
		public double ParamAyus () 
		{
			return 60.0;
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList(60);
			double cycle_start = (double)cycle * this.ParamAyus();
			for (int i=0; i<60; i++)
			{
				double start = cycle_start + (double)i;
				DasaEntry di = new DasaEntry(BodyName.Other, start, 1.0, 1, "Tajaka Year");
				al.Add (di);
			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			string[] desc = new String[] { "  Tajaka Month", "    Tajaka 60 hour", "      Tajaka 5 hour", "        Tajaka 25 minute", "          Tajaka 2 minute" };
			if (pdi.level == 6)
				return new ArrayList();

			ArrayList al;
			double start = 0.0, length = 0.0;
			int level = 0;

			al = new ArrayList (12);
			start = pdi.startUT;
			level = pdi.level+1;
			length = pdi.dasaLength / 12.0;
			for (int i=0; i<12; i++)
			{
				DasaEntry di = new DasaEntry (BodyName.Other, start, length, level, desc[level-2]);
				al.Add (di);
				start += length;
			}
			return al;
		}
		public String Description ()
		{
			return "Tajaka Chart Dasa";
		}
	}
}
