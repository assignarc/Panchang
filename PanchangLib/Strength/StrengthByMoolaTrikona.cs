

namespace org.transliteral.panchang
{
    // Stronger rasi has more planets in moola trikona
    // Stronger planet is in moola trikona rasi
    public class StrengthByMoolaTrikona : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByMoolaTrikona (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public int value (ZodiacHouseName zn) 
		{
			int ret = 0;
			foreach (DivisionPosition dp in std_div_pos)
			{
				if (dp.type != BodyType.Name.Graha) continue;
				if (dp.zodiac_house.value != zn)continue;
				ret += value (dp.name);
			}
			return ret;
		}
		public int value (Body.Name b)
		{
			if (h.getPosition(b).ToDivisionPosition(dtype).isInMoolaTrikona()) return 1;
			return 0;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int vala = value (za);
			int valb = value (zb);

			if (vala > valb) return true;
			if (valb > vala) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			int valm = value (m);
			int valn = value (n);

			if (valm > valn) return true;
			if (valn > valm) return false;
			throw new EqualStrength();
		}
	}



}
	
