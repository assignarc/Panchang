

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class TattwaDasa : Dasa, IDasa
	{
		public class UserOptions 
		{
			

			public Tattwa _startTattwa;

			[Visible("Seed Tattwa")]
			public Tattwa StartTattwa 
			{
				get { return _startTattwa; }
				set { _startTattwa = value; }
			}

		}
		private Horoscope h;
		public TattwaDasa (Horoscope _h)
		{
			h = _h;
		}
		public double ParamAyus()
		{
			return ((1.0 / 24.0) / 60.0);
		}
		public void RecalculateOptions()
		{
		}
		public ArrayList Dasa (int cycle)
		{
			ArrayList al = new ArrayList();
			
			double day_length = h.NextSunrise + 24.0 - h.Sunrise;
			double day_sr = Math.Floor(h.BaseUT) + (h.Sunrise / 24.0);

			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi)
		{
			return new ArrayList();
		}
		public string Description ()
		{
			return "Tattwa Dasa";
		}
        public new object Options => new Object();
        public object SetOptions (object o)
		{
			return o;
		}
	}
}
