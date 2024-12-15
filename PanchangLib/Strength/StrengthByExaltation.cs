

namespace org.transliteral.panchang
{
    // Stronger rasi has larger number of exalted planets - debilitated planets
    // Stronger planet is exalted or not debilitated
    public class StrengthByExaltation : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByExaltation (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public int Value (ZodiacHouseName zn) 
		{
			int ret = 0;
			foreach (DivisionPosition dp in standardDivisionPositions)
			{
				if (dp.Type != BodyType.Name.Graha) continue;
				if (dp.ZodiacHouse.Value != zn)continue;

				if (dp.IsExaltedPhalita()) ret++;
				else if (dp.IsDebilitatedPhalita()) ret--;
			}
			return ret;
		}
		public int Value (Body.Name b)
		{
			if (horoscope.GetPosition(b).ToDivisionPosition(divisionType).IsExaltedPhalita()) return 1;
			else if (horoscope.GetPosition(b).ToDivisionPosition(divisionType).IsDebilitatedPhalita()) return -1;
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
		public bool Stronger (Body.Name m, Body.Name n)
		{
			int valm = Value (m);
			int valn = Value (n);

			if (valm > valn) return true;
			if (valn > valm) return false;
			throw new EqualStrength();
		}
	}



}
	
