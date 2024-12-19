

namespace org.transliteral.panchang
{
    // Stronger rasi has more planets in moola trikona
    // Stronger planet is in moola trikona rasi
    public class StrengthByMoolaTrikona : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByMoolaTrikona (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

        public int Value(ZodiacHouseName zn)
        {
			int ret = 0;
			foreach (DivisionPosition dp in standardDivisionPositions)
			{
				if (dp.Type != BodyType.Name.Graha) continue;
				if (dp.ZodiacHouse.Value != zn)continue;
				ret += Value (dp.Name);
			}
			return ret;
		}
        public int Value(BodyName b)
		{
			if (horoscope.GetPosition(b).ToDivisionPosition(divisionType).IsInMoolaTrikona()) return 1;
			return 0;
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int vala = Value (za);
			int valb = Value (zb);

			if (vala > valb) return true;
			if (valb > vala) return false;
			throw new EqualStrength();
		}
		public bool Stronger (BodyName m, BodyName n)
		{
			int valm = Value (m);
			int valn = Value (n);

			if (valm > valn) return true;
			if (valn > valm) return false;
			throw new EqualStrength();
		}
	}



}
	
