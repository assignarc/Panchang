

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NaisargikaRasiDasa: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			[DisplayName("Life Expectancy")]
			
			private ParamAyusType paramAyus;
			public UserOptions () 
			{
				ParamAyus = ParamAyusType.Middle;
			}
			[DisplayName("Total Param Ayus")]
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
		public void RecalculateOptions ()
		{
		}
		public double ParamAyus () 
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

			double cycle_start = ParamAyus() * (double)cycle;
			double curr = 0.0;
			double dasa_length;
			ZodiacHouse zlagna = h.GetPosition(Body.Name.Lagna).Longitude.ToZodiacHouse();
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh = zlagna.Add (order[i]);
				switch (this.options.ParamAyus) 
				{
					case ParamAyusType.Long: dasa_length = 10.0; break;
					case ParamAyusType.Middle: dasa_length = 9.0; break;
					default: 
						int mod = ((int)zh.Value) % 3;
						dasa_length = short_length[mod];
						break;
				}
				al.Add (new DasaEntry (zh.Value, cycle_start + curr, dasa_length, 1, zh.Value.ToString()));
				curr += dasa_length;

			}
			return al;
		}
        public ArrayList AntarDasa(DasaEntry pdi) => new ArrayList();
        public String Description() => "Naisargika Rasi Dasa";
        public Object Options => this.options.Clone();
        public object SetOptions (Object a)
		{
			UserOptions uo = (UserOptions)a;
			this.options.ParamAyus = uo.ParamAyus;
			RecalculateEvent();
			return options.Clone();
		}

	}
}
