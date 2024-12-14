

namespace org.transliteral.panchang
{
    // Stronger rasi has larger number of exalted planets - debilitated planets
    // Stronger planet is exalted or not debilitated
    public class StrengthByExaltation : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByExaltation (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public int value (ZodiacHouseName zn) 
		{
			int ret = 0;
			foreach (DivisionPosition dp in std_div_pos)
			{
				if (dp.type != BodyType.Name.Graha) continue;
				if (dp.zodiac_house.value != zn)continue;

				if (dp.isExaltedPhalita()) ret++;
				else if (dp.isDebilitatedPhalita()) ret--;
			}
			return ret;
		}
		public int value (Body.Name b)
		{
			if (h.getPosition(b).ToDivisionPosition(dtype).isExaltedPhalita()) return 1;
			else if (h.getPosition(b).ToDivisionPosition(dtype).isDebilitatedPhalita()) return -1;
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
	
