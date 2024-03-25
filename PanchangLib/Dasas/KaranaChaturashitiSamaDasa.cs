

using System;
using System.Collections;

namespace org.transliteral.panchang
{


    // Wrapper around ChaturashitiSamaDasa
    public class KaranaChaturashitiSamaDasa : NakshatraDasa, INakshatraDasa, INakshatraKaranaDasa
	{
		private Horoscope h;
		ChaturashitiSamaDasa cd;
		override public Object GetOptions ()
		{
			return new Object();
		}
		override public object SetOptions (Object a)
		{
			return new object();
		}
		public ArrayList Dasa(int cycle)
		{
			Longitude mMoon = h.getPosition(Body.Name.Moon).longitude;
			Longitude mSun = h.getPosition(Body.Name.Sun).longitude;
			return _KaranaDasa (mMoon.sub(mSun), 1, cycle);
		}
		public ArrayList AntarDasa (DasaEntry di)
		{
			return _AntarDasa (di);
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

		public double paramAyus ()
		{
			return cd.paramAyus();
		}
		public int numberOfDasaItems ()
		{
			return cd.numberOfDasaItems();
		}
		public DasaEntry nextDasaLord (DasaEntry di) 
		{
			return cd.nextDasaLord(di);
		}
		public double lengthOfDasa (Body.Name plt)
		{
			return cd.lengthOfDasa(plt);

		}
		public Body.Name lordOfNakshatra (Nakshatra n)
		{
			return cd.lordOfNakshatra(n);
		}
		public Body.Name lordOfKarana(Longitude l) 
		{
			return l.toKarana().getLord();
		}
	}
}
