

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NaisargikaRasiDasa: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			[PGDisplayName("Life Expectancy")]
			
			private ParamAyusType paramAyus;
			public UserOptions () 
			{
				ParamAyus = ParamAyusType.Middle;
			}
			[PGDisplayName("Total Param Ayus")]
			public ParamAyusType ParamAyus
			{
				get { return paramAyus; }
				set { paramAyus = value; }
			}
			public Object Clone () 
			{
				UserOptions uo = new UserOptions();
				uo.paramAyus = this.paramAyus;
				return uo;
			}
		}

		private Horoscope h;
		private UserOptions options;
		public NaisargikaRasiDasa (Horoscope _h)
		{
			h = _h;
			options = new UserOptions();
		}
		public void recalculateOptions ()
		{
		}
		public double paramAyus () 
		{
			switch (this.options.ParamAyus) 
			{
				case ParamAyusType.Long: return 120.0;
				case ParamAyusType.Middle: return 108.0;
				default: return 96.0;
			}
		}
		public ArrayList Dasa(int cycle)
		{
			int[] order = new int[] {4, 2, 8, 10, 12, 6, 5, 11, 1, 7, 9, 3};
			int[] short_length = new int[] { 9, 7, 8};
			ArrayList al = new ArrayList (9);

			double cycle_start = paramAyus() * (double)cycle;
			double curr = 0.0;
			double dasa_length;
			ZodiacHouse zlagna = h.getPosition(Body.Name.Lagna).Longitude.toZodiacHouse();
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh = zlagna.add (order[i]);
				switch (this.options.ParamAyus) 
				{
					case ParamAyusType.Long: dasa_length = 10.0; break;
					case ParamAyusType.Middle: dasa_length = 9.0; break;
					default: 
						int mod = ((int)zh.value) % 3;
						dasa_length = short_length[mod];
						break;
				}
				al.Add (new DasaEntry (zh.value, cycle_start + curr, dasa_length, 1, zh.value.ToString()));
				curr += dasa_length;

			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			return new ArrayList();
		}
		public String Description ()
		{
			return "Naisargika Rasi Dasa";
		}
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
		public object SetOptions (Object a)
		{
			UserOptions uo = (UserOptions)a;
			this.options.ParamAyus = uo.ParamAyus;
			RecalculateEvent();
			return options.Clone();
		}

	}
}
