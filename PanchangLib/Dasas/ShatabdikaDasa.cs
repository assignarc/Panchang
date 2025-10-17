

using System;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShatabdikaDasa : NakshatraDasa, INakshatraDasa
	{
		private Horoscope h;
        public override object Options => new object();
        public override object SetOptions(object a) => new object();
        public ArrayList Dasa(int cycle) => this.Dasa(h.GetPosition(BodyName.Moon).Longitude, 1, cycle);
        public new ArrayList AntarDasa(DasaEntry di) => base.AntarDasa(di);
        public String Description() => ("Shatabdika Dasa");
        public ShatabdikaDasa (Horoscope _h)
		{
			common = this;
			h = _h;
		}

        public double ParamAyus() => 100.0;
        public int NumberOfDasaItems() => 7;
        public DasaEntry NextDasaLord(DasaEntry di) => new DasaEntry(NextDasaLordHelper(di.graha), 0, 0, di.level, "");
        private BodyName NextDasaLordHelper(BodyName b)
		{
			switch (b)
			{
				case BodyName.Sun: return BodyName.Moon;
				case BodyName.Moon: return BodyName.Venus;
				case BodyName.Venus : return BodyName.Mercury;
				case BodyName.Mercury : return BodyName.Jupiter;
				case BodyName.Jupiter : return BodyName.Mars;
				case BodyName.Mars: return BodyName.Saturn;
				case BodyName.Saturn : return BodyName.Sun;
			}
			Trace.Assert (false, "ShatabdikaDasa::nextDasaLord");
			return BodyName.Lagna;
		}
		public double LengthOfDasa(BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 5;
				case BodyName.Moon: return 5;
				case BodyName.Venus: return 10;
				case BodyName.Mercury: return 10;
				case BodyName.Jupiter: return 20;
				case BodyName.Mars: return 20;
				case BodyName.Saturn: return 30;
			}
			Trace.Assert (false, "ShatabdikaDasa::lengthOfDasa");
			return 0;
		}
		public BodyName LordOfNakshatra(Nakshatra n) 
		{
			BodyName[] lords = new BodyName[7] 
			{
				BodyName.Sun, BodyName.Moon, BodyName.Venus,
				BodyName.Mercury, BodyName.Jupiter, BodyName.Mars,
				BodyName.Saturn
			};				
			int nak_val = ((int)n.Value);
			int rev_val = (int)NakshatraName.Revati;
			int diff_val = Basics.NormalizeInclusive(
				(int)NakshatraName.Ashwini, (int)NakshatraName.Revati, 
				nak_val - rev_val);
			int diff_off = diff_val % 7;
			return lords[diff_off];
		}
	}
}
