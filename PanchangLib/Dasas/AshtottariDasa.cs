

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class AshtottariDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        public override Object Options => new Object();
        public override object SetOptions(Object a) => new object();
        public ArrayList Dasa(int cycle) => Dasa(h.GetPosition(BodyName.Moon).Longitude, 1, cycle);
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
        private BodyName NextDasaLordHelper(BodyName b)
		{
			switch (b)
			{
				case BodyName.Sun: return BodyName.Moon;
				case BodyName.Moon: return BodyName.Mars;
				case BodyName.Mars : return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Jupiter;
				case BodyName.Jupiter: return BodyName.Rahu;
				case BodyName.Rahu : return BodyName.Venus;
				case BodyName.Venus : return BodyName.Sun;
			}
			Trace.Assert (false, "AshtottariDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa (BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 6;
				case BodyName.Moon: return 15;
				case BodyName.Mars: return 8;
				case BodyName.Mercury: return 17;
				case BodyName.Saturn: return 10;
				case BodyName.Jupiter: return 19;
				case BodyName.Rahu: return 12;
				case BodyName.Venus: return 21;
			}
			Trace.Assert (false, "Ashtottari::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			switch (n.Value)
			{
				case NakshatraName.Aswini : return BodyName.Rahu ;
				case NakshatraName.Bharani : return BodyName.Rahu ;
				case NakshatraName.Krittika : return BodyName.Venus ;
				case NakshatraName.Rohini : return BodyName.Venus ;
				case NakshatraName.Mrigarirsa : return BodyName.Venus ;
				case NakshatraName.Aridra : return BodyName.Sun ;
				case NakshatraName.Punarvasu : return BodyName.Sun ;
				case NakshatraName.Pushya : return BodyName.Sun ;
				case NakshatraName.Aslesha : return BodyName.Sun ;
				case NakshatraName.Makha : return BodyName.Moon ;
				case NakshatraName.PoorvaPhalguni : return BodyName.Moon ;
				case NakshatraName.UttaraPhalguni : return BodyName.Moon ;
				case NakshatraName.Hasta : return BodyName.Mars ;
				case NakshatraName.Chittra : return BodyName.Mars ;
				case NakshatraName.Swati : return BodyName.Mars ;
				case NakshatraName.Vishaka : return BodyName.Mars ;
				case NakshatraName.Anuradha : return BodyName.Mercury ;
				case NakshatraName.Jyestha : return BodyName.Mercury ;
				case NakshatraName.Moola : return BodyName.Mercury ;
				case NakshatraName.PoorvaShada : return BodyName.Saturn ;
				case NakshatraName.UttaraShada : return BodyName.Saturn ;
				case NakshatraName.Sravana : return BodyName.Saturn ;
				case NakshatraName.Dhanishta : return BodyName.Jupiter ;
				case NakshatraName.Satabisha : return BodyName.Jupiter ;
				case NakshatraName.PoorvaBhadra : return BodyName.Jupiter ;
				case NakshatraName.UttaraBhadra : return BodyName.Rahu ;
				case NakshatraName.Revati : return BodyName.Rahu ;
			}
			Trace.Assert (false, "AshtottariDasa::LordOfNakshatra");
			return BodyName.Lagna;
		}
	}
}
