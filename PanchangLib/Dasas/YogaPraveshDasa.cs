

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class YogaPraveshDasa: Dasa, IDasa
	{
		private Horoscope h;
		public YogaPraveshDasa (Horoscope _h)
		{
			h = _h;
		}
        public new Object Options => new Object();
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
				DasaEntry di = new DasaEntry(BodyName.Other, start, 1.0, 1, "Yoga Pravesh Year");
				al.Add (di);
			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			string[] desc = new String[] { "  Month: ", "    Yoga: " };
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
					al = new ArrayList (15);
					length = pdi.dasaLength / 15.0;
                    Logger.Info(String.Format("AD length is {0}", length));
					for (int i=0; i<15; i++)
					{
						DasaEntry di = new DasaEntry (BodyName.Other, start, length, level, desc[level-2]);
						al.Add (di);
						start += length;
					}
					return al;
				case 2:
					al = new ArrayList (27);
					length = pdi.dasaLength / 27.0;
                    Logger.Info(String.Format("PD length is {0}", length));
					for (int i=0; i<27; i++)
					{
						DasaEntry di = new DasaEntry (BodyName.Other, start, length, level, desc[level-2]);
                        Logger.Info(String.Format("PD: Starg {0}, length {1}", start, length));
						al.Add (di);
						start += length;
					}
					return al;
			}
			return new ArrayList();;
		}
		public new string EntryDescription (DasaEntry pdi, Moment start, Moment end)
		{
			if (pdi.level == 2)
			{
				Longitude l = Basics.CalculateBodyLongitude(start.ToUniversalTime(), Sweph.BodyNameToSweph(BodyName.Sun));
				ZodiacHouse zh = l.ToZodiacHouse();
				return zh.ToString();
			}
			else if (pdi.level == 3)
			{
				Longitude lSun = Basics.CalculateBodyLongitude(start.ToUniversalTime(), Sweph.BodyNameToSweph(BodyName.Sun));
				Longitude lMoon = Basics.CalculateBodyLongitude(start.ToUniversalTime(), Sweph.BodyNameToSweph(BodyName.Moon));
				Longitude l = lMoon.Add(lSun);

				// this seems wrong. Why should we need to go to the next yoga here?
				SunMoonYoga y = l.ToSunMoonYoga().Add(2);
				return y.ToString();
			}
			return "";
		}

		public String Description ()
		{
			return "Yoga Pravesh Chart Dasa";
		}
	}
}
