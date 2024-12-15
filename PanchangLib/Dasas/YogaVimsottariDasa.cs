

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    // Wrapper around vimsottari dasa that starts the initial dasa
    // based on the yoga
    public class YogaVimsottariDasa : NakshatraDasa, INakshatraDasa, INakshatraYogaDasa
	{
		private Horoscope h;
		private VimsottariDasa vd;
		private UserOptions options;

		public class UserOptions : ICloneable
		{
			public bool bExpungeTravelled = true;
			public UserOptions ()
			{
				this.bExpungeTravelled = true;
			}
			public Object Clone ()
			{
                UserOptions options = new UserOptions
                {
                    bExpungeTravelled = this.bExpungeTravelled
                };
                return options;
			}
			public Object SetOptions (Object b)
			{
				if (b is UserOptions)
				{
					UserOptions uo = (UserOptions) b;
					this.bExpungeTravelled = uo.bExpungeTravelled;
				}
				return this.Clone();
			}

			[InVisible]
			public bool UseYogaRemainder
			{
				get { return this.bExpungeTravelled; }
				set { this.bExpungeTravelled = value; }
			}
		}

        override public Object Options => this.options.Clone();
        override public object SetOptions (Object a)
		{
			this.options = (UserOptions)this.options.SetOptions(a);
			if (this.RecalculateEvent != null)
                this.RecalculateEvent();
			return this.options.Clone();
		}
		public ArrayList Dasa(int cycle)
		{
			Transit t = new Transit(h);
			Longitude l = t.LongitudeOfSunMoonYoga(h.baseUT);
			return this.YogaDasa(l, 1, cycle);
		}
		public ArrayList AntarDasa (DasaEntry di)
		{
			return base.AntarDasa (di);
		}
		public String Description ()
		{
			return String.Format("Yoga Vimsottari Dasa");
			
		}
		public Body.Name LordOfYoga (Longitude l)
		{
			return l.ToSunMoonYoga().GetLord();
		}
		public YogaVimsottariDasa (Horoscope _h)
		{
			this.options = new UserOptions();
			common = this;
			yogaCommon = this;
			h = _h;
			vd = new VimsottariDasa(h);
		}

		public double ParamAyus ()
		{
			return vd.ParamAyus();
		}
		public int NumberOfDasaItems ()
		{
			return vd.NumberOfDasaItems();
		}
		public DasaEntry NextDasaLord (DasaEntry di) 
		{
			return vd.NextDasaLord(di);
		}

		public double LengthOfDasa(Body.Name plt)
		{
			return vd.LengthOfDasa(plt);

		}
		public Body.Name LordOfNakshatra(Nakshatra n)
		{
			throw new Exception();
			return Body.Name.Lagna;
		}
	}
}
