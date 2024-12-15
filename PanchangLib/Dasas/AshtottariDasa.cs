

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class AshtottariDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        override public Object Options => new Object();
        override public object SetOptions(Object a) => new object();
        public ArrayList Dasa(int cycle) => Dasa(h.GetPosition(Body.Name.Moon).Longitude, 1, cycle);
        public ArrayList AntarDasa(DasaEntry di) => base.AntarDasa(di);
        public String Description() => ("Ashtottari Dasa");
        public AshtottariDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

        public double ParamAyus() => 108.0;
        public int NumberOfDasaItems() => 8;
        public DasaEntry NextDasaLord(DasaEntry di) => new DasaEntry(NextDasaLordHelper(di.graha), 0, 0, di.level, "");
        private Body.Name NextDasaLordHelper(Body.Name b)
		{
			switch (b)
			{
				case Body.Name.Sun: return Body.Name.Moon;
				case Body.Name.Moon: return Body.Name.Mars;
				case Body.Name.Mars : return Body.Name.Mercury;
				case Body.Name.Mercury : return Body.Name.Saturn;
				case Body.Name.Saturn : return Body.Name.Jupiter;
				case Body.Name.Jupiter: return Body.Name.Rahu;
				case Body.Name.Rahu : return Body.Name.Venus;
				case Body.Name.Venus : return Body.Name.Sun;
			}
			Trace.Assert (false, "AshtottariDasa::nextDasaLord");
			return Body.Name.Lagna;
		}
		public double LengthOfDasa (Body.Name plt)
		{
			switch (plt)
			{
				case Body.Name.Sun: return 6;
				case Body.Name.Moon: return 15;
				case Body.Name.Mars: return 8;
				case Body.Name.Mercury: return 17;
				case Body.Name.Saturn: return 10;
				case Body.Name.Jupiter: return 19;
				case Body.Name.Rahu: return 12;
				case Body.Name.Venus: return 21;
			}
			Trace.Assert (false, "Ashtottari::lengthOfDasa");
			return 0;
		}
		public Body.Name LordOfNakshatra(Nakshatra n) 
		{
			switch (n.Value)
			{
				case NakshatraName.Aswini : return Body.Name.Rahu ;
				case NakshatraName.Bharani : return Body.Name.Rahu ;
				case NakshatraName.Krittika : return Body.Name.Venus ;
				case NakshatraName.Rohini : return Body.Name.Venus ;
				case NakshatraName.Mrigarirsa : return Body.Name.Venus ;
				case NakshatraName.Aridra : return Body.Name.Sun ;
				case NakshatraName.Punarvasu : return Body.Name.Sun ;
				case NakshatraName.Pushya : return Body.Name.Sun ;
				case NakshatraName.Aslesha : return Body.Name.Sun ;
				case NakshatraName.Makha : return Body.Name.Moon ;
				case NakshatraName.PoorvaPhalguni : return Body.Name.Moon ;
				case NakshatraName.UttaraPhalguni : return Body.Name.Moon ;
				case NakshatraName.Hasta : return Body.Name.Mars ;
				case NakshatraName.Chittra : return Body.Name.Mars ;
				case NakshatraName.Swati : return Body.Name.Mars ;
				case NakshatraName.Vishaka : return Body.Name.Mars ;
				case NakshatraName.Anuradha : return Body.Name.Mercury ;
				case NakshatraName.Jyestha : return Body.Name.Mercury ;
				case NakshatraName.Moola : return Body.Name.Mercury ;
				case NakshatraName.PoorvaShada : return Body.Name.Saturn ;
				case NakshatraName.UttaraShada : return Body.Name.Saturn ;
				case NakshatraName.Sravana : return Body.Name.Saturn ;
				case NakshatraName.Dhanishta : return Body.Name.Jupiter ;
				case NakshatraName.Satabisha : return Body.Name.Jupiter ;
				case NakshatraName.PoorvaBhadra : return Body.Name.Jupiter ;
				case NakshatraName.UttaraBhadra : return Body.Name.Rahu ;
				case NakshatraName.Revati : return Body.Name.Rahu ;
			}
			Trace.Assert (false, "AshtottariDasa::LordOfNakshatra");
			return Body.Name.Lagna;
		}
	}
}
