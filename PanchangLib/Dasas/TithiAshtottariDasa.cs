

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
				UserOptions options = new UserOptions ();
				options.mTithiOffset = this.mTithiOffset;
				options.bExpungeTravelled = this.bExpungeTravelled;
				return options;
			}
			public Object SetOptions (Object b)
			{
				if (b is UserOptions)
				{
					UserOptions uo = (UserOptions) b;
					this.mTithiOffset = uo.mTithiOffset;
					this.bExpungeTravelled = uo.bExpungeTravelled;
				}
				return this.Clone();
			}

			[PGNotVisible]
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

		override public Object GetOptions ()
		{
			return this.options.Clone();
		}
		override public object SetOptions (Object a)
		{
			this.options = (UserOptions)this.options.SetOptions(a);
			if (this.RecalculateEvent != null)
				this.RecalculateEvent();
			return this.options.Clone();
		}
		public ArrayList Dasa(int cycle)
		{
			
			Longitude mpos = h.getPosition(Body.Name.Moon).longitude;
			Longitude spos = h.getPosition(Body.Name.Sun).longitude;

			Longitude tithi = mpos.sub(spos);
			if (options.UseTithiRemainder == false)
			{
				double offset = tithi.value;
				while (offset >= 12.0) offset -= 12.0;
				tithi = tithi.sub (new Longitude(offset));
			}
			return _TithiDasa(tithi, options.TithiOffset, cycle);
		}
		public ArrayList AntarDasa (DasaEntry di)
		{
			return _AntarDasa (di);
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

		public double paramAyus ()
		{
			return ad.paramAyus();
		}
		public int numberOfDasaItems ()
		{
			return ad.numberOfDasaItems();
		}
		public DasaEntry nextDasaLord (DasaEntry di) 
		{
			return ad.nextDasaLord(di);
		}

		public double lengthOfDasa (Body.Name plt)
		{
			return ad.lengthOfDasa(plt);

		}
		public Body.Name lordOfNakshatra (Nakshatra n)
		{
			Debug.Assert(false, "TithiAshtottari::lordOfNakshatra");
			return Body.Name.Sun;
		}
		public Body.Name lordOfTithi (Longitude l)
		{
			return l.toTithi().getLord();
		}
	}
}
