

namespace org.transliteral.panchang
{
    // Stronger rasi has its Lord in its house
    // Stronger graha is in its own house
    public class StrengthByLordInOwnHouse : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordInOwnHouse (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int value (ZodiacHouseName _zh)
		{
			int ret=0;

			ZodiacHouse zh = new ZodiacHouse(_zh);
			Body.Name bl = this.GetStrengthLord(zh);
			DivisionPosition pl = h.getPosition(bl).ToDivisionPosition(dtype);
			DivisionPosition pj = h.getPosition(Body.Name.Jupiter).ToDivisionPosition(dtype);
			DivisionPosition pm = h.getPosition(Body.Name.Mercury).ToDivisionPosition(dtype);

			if (pl.GrahaDristi(zh)) ret++;
			if (pj.GrahaDristi(zh)) ret++;
			if (pm.GrahaDristi(zh)) ret++;
			return ret;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int a = this.value(za);
			int b = this.value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			ZodiacHouseName zm = h.getPosition(m).ToDivisionPosition(dtype).zodiac_house.value;
			ZodiacHouseName zn = h.getPosition(n).ToDivisionPosition(dtype).zodiac_house.value;
			return stronger (zm, zn);
		}
	}



}
	
