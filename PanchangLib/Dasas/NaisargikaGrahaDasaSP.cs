

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
			public Object Clone () 
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
		public double paramAyus () 
		{
			return 108.0;
		}
		public void recalculateOptions ()
		{
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (36);
			Body.Name[] order = new Body.Name[] 
				{
					Body.Name.Moon, Body.Name.Mercury, Body.Name.Mars,
					Body.Name.Venus, Body.Name.Jupiter,	Body.Name.Sun,
					Body.Name.Ketu,	Body.Name.Rahu,	Body.Name.Saturn };

			double cycle_start = paramAyus() * (double)cycle;
			double curr = 0.0;
			for (int i=0; i<3; i++) 
			{
				foreach (Body.Name bn in order) 
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
		public String Description ()
		{
			return "Naisargika Graha Dasa (SP)";
		}
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
		public object SetOptions (Object a)
		{
			UserOptions uo = (UserOptions)a;
			if (RecalculateEvent != null)
				RecalculateEvent();
			return options.Clone();
		}

	}
}
