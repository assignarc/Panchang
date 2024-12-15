

namespace org.transliteral.panchang
{
    // Stronger rasi has a larger narayana dasa length
    // Stronger graha is in such a rasi
    public class StrengthByNarayanaDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByNarayanaDasaLength (Horoscope h, Division dtype, bool bSimpleLord) : base (h, dtype, bSimpleLord) {}
		protected int Value (ZodiacHouseName _zh)
		{
			Body.Name bl = this.GetStrengthLord(_zh);
			DivisionPosition pl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType);
			return NarayanaDasa.NarayanaDasaLength  (new ZodiacHouse(_zh), pl);
		}
        protected int Value(Body.Name bm)
		{
			ZodiacHouseName zm = horoscope.GetPosition(bm).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			return Value(zm);
		}
        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb)
        {
			int a = this.Value(za);
			int b = this.Value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
        public bool Stronger(Body.Name m, Body.Name n)
		{
			int a = this.Value(m);
			int b = this.Value(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();

		}

	}



}
	
