

namespace org.transliteral.panchang
{
    // Stronger rasi has more conjunctions/rasi drishtis of Jupiter, Mercury and Lord
    // Stronger graha is in such a rasi
    public class StrengthByAspectsRasi : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAspectsRasi (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int value (ZodiacHouse zj, ZodiacHouse zm, ZodiacHouseName zx)
		{
			int ret = 0;
			ZodiacHouse zh = new ZodiacHouse(zx);

			Body.Name bl = this.GetStrengthLord(zx);
			ZodiacHouse zl = h.getPosition(bl).ToDivisionPosition(dtype).zodiac_house;

			if (zj.RasiDristi(zh) || zj.value == zh.value) ret++;
			if (zm.RasiDristi(zh) || zm.value == zh.value) ret++;
			if (zl.RasiDristi(zh) || zl.value == zh.value) ret++;
			return ret;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			ZodiacHouse zj = h.getPosition(Body.Name.Jupiter).ToDivisionPosition(dtype).zodiac_house;
			ZodiacHouse zm = h.getPosition(Body.Name.Mercury).ToDivisionPosition(dtype).zodiac_house;

			int a = this.value(zj, zm, za);
			int b = this.value(zj, zm, zb);
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
	
