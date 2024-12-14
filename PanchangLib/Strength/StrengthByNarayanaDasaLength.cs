

namespace org.transliteral.panchang
{
    // Stronger rasi has a larger narayana dasa length
    // Stronger graha is in such a rasi
    public class StrengthByNarayanaDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByNarayanaDasaLength (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}
		protected int value (ZodiacHouseName _zh)
		{
			Body.Name bl = this.GetStrengthLord(_zh);
			DivisionPosition pl = h.getPosition(bl).ToDivisionPosition(dtype);
			return NarayanaDasa.NarayanaDasaLength  (new ZodiacHouse(_zh), pl);
		}
		protected int value (Body.Name bm)
		{
			ZodiacHouseName zm = h.getPosition(bm).ToDivisionPosition(dtype).zodiac_house.value;
			return value(zm);
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
	
