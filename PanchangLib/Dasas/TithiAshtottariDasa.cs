

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    // Wrapper around ashtottari dasa that starts the initial dasa
    // based on the tithi. We do not reimplement ashtottari dasa 
    // semantics here.
    public class TithiAshtottariDasa : NakshatraDasa, INakshatraDasa, INakshatraTithiDasa
	{
		private Horoscope h;
		private AshtottariDasa ad;
		private UserOptions options;

		public class UserOptions : ICloneable
		{
			public int mTithiOffset = 1;
			public bool bExpungeTravelled = true;
			public UserOptions ()
			{
				this.mTithiOffset = 1;
				this.bExpungeTravelled = true;
			}
			public Object Clone ()
			{
                UserOptions options = new UserOptions
                {
                    mTithiOffset = this.mTithiOffset,
                    bExpungeTravelled = this.bExpungeTravelled
                };
                return options;
			}
			public Object SetOptions (Object b)
			{
                if (b is UserOptions uo)
                {
                    this.mTithiOffset = uo.mTithiOffset;
                    this.bExpungeTravelled = uo.bExpungeTravelled;
                }
                return this.Clone();
			}

			[InVisible]
			public bool UseTithiRemainder 
			{
				get { return this.bExpungeTravelled; }
				set { this.bExpungeTravelled = value; }
			}
			public int TithiOffset
			{
				get { return this.mTithiOffset; }
				set 
				{
					if (value >= 1 && value <= 30)
						this.mTithiOffset = value;
				}
			}
		}

        public override Object Options => this.options.Clone();
        public override object SetOptions (Object a)
		{
			this.options = (UserOptions)this.options.SetOptions(a);
			if (this.RecalculateEvent != null)
                this.RecalculateEvent();
			return this.options.Clone();
		}
		public ArrayList Dasa(int cycle)
		{
			
			Longitude mpos = h.GetPosition(BodyName.Moon).Longitude;
			Longitude spos = h.GetPosition(BodyName.Sun).Longitude;

			Longitude tithi = mpos.Subtract(spos);
			if (options.UseTithiRemainder == false)
			{
				double offset = tithi.Value;
				while (offset >= 12.0) offset -= 12.0;
				tithi = tithi.Subtract (new Longitude(offset));
			}
			return TithiDasa(tithi, options.TithiOffset, cycle);
		}
        public new ArrayList AntarDasa(DasaEntry di)
		{
			return base.AntarDasa (di);
		}
		public String Description ()
		{
			return String.Format("({0}) Tithi Ashtottari Dasa", this.options.TithiOffset);
			
		}
		public TithiAshtottariDasa (Horoscope _h)
		{
			this.options = new UserOptions();
			tithiCommon = this;
			common = this;
			h = _h;
			ad = new AshtottariDasa(h);
		}

		public double ParamAyus ()
		{
			return ad.ParamAyus();
		}
		public int NumberOfDasaItems ()
		{
			return ad.NumberOfDasaItems();
		}
		public DasaEntry NextDasaLord (DasaEntry di) 
		{
			return ad.NextDasaLord(di);
		}

		public double LengthOfDasa(BodyName plt)
		{
			return ad.LengthOfDasa(plt);

		}
		public BodyName LordOfNakshatra(Nakshatra n)
		{
			Debug.Assert(false, "TithiAshtottari::lordOfNakshatra");
			return BodyName.Sun;
		}
		public BodyName LordOfTithi (Longitude l)
		{
			return l.ToTithi().GetLord();
		}
	}
}
