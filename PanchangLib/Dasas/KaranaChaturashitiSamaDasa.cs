

using System;
using System.Collections;

namespace org.transliteral.panchang
{


    // Wrapper around ChaturashitiSamaDasa
    public class KaranaChaturashitiSamaDasa : NakshatraDasa, INakshatraDasa, INakshatraKaranaDasa
	{
		private Horoscope h;
		ChaturashitiSamaDasa cd;
        override public Object Options => new Object();
        override public object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			Longitude mMoon = h.GetPosition(Body.Name.Moon).Longitude;
			Longitude mSun = h.GetPosition(Body.Name.Sun).Longitude;
			return KaranaDasa (mMoon.Subtract(mSun), 1, cycle);
		}
		public ArrayList AntarDasa (DasaEntry di)
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
		public double LengthOfDasa (Body.Name plt)
		{
			return cd.LengthOfDasa(plt);

		}
		public Body.Name LordOfNakshatra(Nakshatra n)
		{
			return cd.LordOfNakshatra(n);
		}
		public Body.Name LordOfKarana(Longitude l) 
		{
			return l.ToKarana().GetLord();
		}
	}
}
