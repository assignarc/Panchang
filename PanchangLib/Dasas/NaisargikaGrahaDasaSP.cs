

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NaisargikaGrahaDasaSP: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			public UserOptions () 
			{
			}
			public object Clone () 
			{
				UserOptions uo = new UserOptions();
				return uo;
			}
		}

		private Horoscope h;
		private UserOptions options;
		public NaisargikaGrahaDasaSP (Horoscope _h)
		{
			h = _h;
			options = new UserOptions();
		}
		public double ParamAyus () 
		{
			return 108.0;
		}
		public void RecalculateOptions ()
		{
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (36);
			BodyName[] order = new BodyName[] 
				{
					BodyName.Moon, BodyName.Mercury, BodyName.Mars,
					BodyName.Venus, BodyName.Jupiter,	BodyName.Sun,
					BodyName.Ketu,	BodyName.Rahu,	BodyName.Saturn };

			double cycle_start = ParamAyus() * (double)cycle;
			double curr = 0.0;
			for (int i=0; i<3; i++) 
			{
				foreach (BodyName bn in order) 
				{
					al.Add (new DasaEntry (bn, cycle_start + curr, 4.0, 1, bn.ToString()));
					curr += 4.0;
				}
			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			return new ArrayList();
		}
		public string Description ()
		{
			return "Naisargika Graha Dasa (SP)";
		}
        public object Options => this.options.Clone();
        public object SetOptions (object a)
		{
			UserOptions uo = (UserOptions)a;
			if (RecalculateEvent != null)
				RecalculateEvent();
			return options.Clone();
		}

	}
}
