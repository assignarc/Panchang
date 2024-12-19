

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class TithiPraveshDasa: Dasa, IDasa
	{
		private Horoscope h;
		public TithiPraveshDasa (Horoscope _h)
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
				DasaEntry di = new DasaEntry(BodyName.Other, start, 1.0, 1, "Tithi Pravesh Year");
				al.Add (di);
			}
			return al;
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
				Longitude l = lMoon.Subtract(lSun);
				Tithi t = l.ToTithi();
				return t.ToString();
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
                    Logger.Info(String.Format("AD length is {0}", length));
					for (int i=0; i<13; i++)
					{
						DasaEntry di = new DasaEntry (BodyName.Other, start, length, level, desc[level-2]);
						al.Add (di);
						start += length;
					}
					return al;
				case 2:
					al = new ArrayList (30);
					length = pdi.dasaLength / 30.0;
                    Logger.Info(String.Format("PD length is {0}", length));
					for (int i=0; i<30; i++)
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
		public String Description ()
		{
			return "Tithi Pravesh Chart Dasa";
		}
	}
}
