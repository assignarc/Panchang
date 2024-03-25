

namespace org.transliteral.panchang
{
    // Stronger rasi has more graha drishtis of Jupiter, Mercury and Lord
    // Stronger graha is in such a rasi
    public class StrengthByAspectsGraha : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAspectsGraha (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int value (ZodiacHouseName _zh)
		{
			int val = 0;
			Body.Name bl = this.GetStrengthLord(_zh);
			DivisionPosition dl = h.getPosition(bl).toDivisionPosition(dtype);
			DivisionPosition dj = h.getPosition(Body.Name.Jupiter).toDivisionPosition(dtype);
			DivisionPosition dm = h.getPosition(Body.Name.Mercury).toDivisionPosition(dtype);

			ZodiacHouse zh = new ZodiacHouse(_zh);
			if (dl.GrahaDristi(zh) || dl.zodiac_house.value == _zh) val++;
			if (dj.GrahaDristi(zh) || dj.zodiac_house.value == _zh) val++;
			if (dm.GrahaDristi(zh) || dm.zodiac_house.value == _zh) val++;

			return val;
		}
		protected int value (Body.Name bm)
		{
			return value (h.getPosition(bm).toDivisionPosition(dtype).zodiac_house.value);
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
			int a = this.value(m);
			int b = this.value(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();

		}
	}



}
	
