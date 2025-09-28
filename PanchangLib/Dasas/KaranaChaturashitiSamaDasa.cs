

using System;
using System.Collections;

namespace org.transliteral.panchang
{


    // Wrapper around ChaturashitiSamaDasa
    public class KaranaChaturashitiSamaDasa : NakshatraDasa, INakshatraDasa, INakshatraKaranaDasa
	{
		private Horoscope h;
		ChaturashitiSamaDasa cd;
        public override Object Options => new Object();
        public override object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			Longitude mMoon = h.GetPosition(BodyName.Moon).Longitude;
			Longitude mSun = h.GetPosition(BodyName.Sun).Longitude;
			return KaranaDasa (mMoon.Subtract(mSun), 1, cycle);
		}
		public new ArrayList AntarDasa (DasaEntry di)
		{
			return base.AntarDasa (di);
		}
		public String Description ()
		{
			return ("Karana Chaturashiti-Sama Dasa");
		}
		public KaranaChaturashitiSamaDasa (Horoscope _h)
		{
			common = this;
			karanaCommon = this;
			h = _h;
			cd = new ChaturashitiSamaDasa(h);
		}

		public double ParamAyus ()
		{
			return cd.ParamAyus();
		}
		public int NumberOfDasaItems ()
		{
			return cd.NumberOfDasaItems();
		}
		public DasaEntry NextDasaLord (DasaEntry di) 
		{
			return cd.NextDasaLord(di);
		}
		public double LengthOfDasa (BodyName plt)
		{
			return cd.LengthOfDasa(plt);

		}
		public BodyName LordOfNakshatra(Nakshatra n)
		{
			return cd.LordOfNakshatra(n);
		}
		public BodyName LordOfKarana(Longitude l) 
		{
			return l.ToKarana().GetLord();
		}
	}
}
