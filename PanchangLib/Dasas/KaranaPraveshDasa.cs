

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class KaranaPraveshDasa: Dasa, IDasa
	{
		private Horoscope h;
		public KaranaPraveshDasa (Horoscope _h)
		{
			h = _h;
		}
		public Object GetOptions ()
		{
			return new Object();
		}
		public object SetOptions (Object a)
		{
			return new Object();
		}
		public void recalculateOptions ()
		{
		}
		public double paramAyus () 
		{
			return 60.0;
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList(60);
			double cycle_start = (double)cycle * this.paramAyus();
			for (int i=0; i<60; i++)
			{
				double start = cycle_start + (double)i;
				DasaEntry di = new DasaEntry(Body.Name.Other, start, 1.0, 1, "Karana Pravesh Year");
				al.Add (di);
			}
			return al;
		}

		new public string EntryDescription (DasaEntry pdi, Moment start, Moment end)
		{

			if (pdi.level == 2)
			{
				Longitude l = Basics.CalculateBodyLongitude(start.toUniversalTime(), Sweph.BodyNameToSweph(Body.Name.Sun));
				ZodiacHouse zh = l.toZodiacHouse();
				return zh.ToString();
			}
			else if (pdi.level == 3)
			{
				Longitude lSun = Basics.CalculateBodyLongitude(start.toUniversalTime(), Sweph.BodyNameToSweph(Body.Name.Sun));
				Longitude lMoon = Basics.CalculateBodyLongitude(start.toUniversalTime(), Sweph.BodyNameToSweph(Body.Name.Moon));
				Longitude l = lMoon.sub(lSun);
				Karana k = l.toKarana();
				return k.ToString();
			}
			return "";
		}

		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			string[] desc = new String[] { "  Month: ", "    Tithi: " };
			if (pdi.level == 3)
				return new ArrayList();

			ArrayList al;
			double start = 0.0, length = 0.0;
			int level = 0;

			al = null;
			start = pdi.startUT;
			level = pdi.level+1;

			switch (pdi.level)
			{
				case 1:
					al = new ArrayList (13);
					length = pdi.dasaLength / 13.0;
					//Console.WriteLine("AD length is {0}", length);
					for (int i=0; i<13; i++)
					{
						DasaEntry di = new DasaEntry (Body.Name.Other, start, length, level, desc[level-2]);
						al.Add (di);
						start += length;
					}
					return al;
				case 2:
					al = new ArrayList (60);
					length = pdi.dasaLength / 60.0;
					//Console.WriteLine("PD length is {0}", length);
					for (int i=0; i<60; i++)
					{
						DasaEntry di = new DasaEntry (Body.Name.Other, start, length, level, desc[level-2]);
						//Console.WriteLine ("PD: Starg {0}, length {1}", start, length);
						al.Add (di);
						start += length;
					}
					return al;
			}
			return new ArrayList();;
		}
		public String Description ()
		{
			return "Karana Pravesh Chart Dasa";
		}
	}
}
