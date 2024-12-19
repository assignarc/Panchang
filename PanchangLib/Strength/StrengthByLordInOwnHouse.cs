

namespace org.transliteral.panchang
{
    // Stronger rasi has its Lord in its house
    // Stronger graha is in its own house
    public class StrengthByLordInOwnHouse : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordInOwnHouse (Horoscope h, Division dtype, bool bSimpleLord) : base (h, dtype, bSimpleLord) {}

        protected int Value(ZodiacHouseName _zh)
		{
			int ret=0;

			ZodiacHouse zh = new ZodiacHouse(_zh);
			BodyName bl = this.GetStrengthLord(zh);
			DivisionPosition pl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType);
			DivisionPosition pj = horoscope.GetPosition(BodyName.Jupiter).ToDivisionPosition(divisionType);
			DivisionPosition pm = horoscope.GetPosition(BodyName.Mercury).ToDivisionPosition(divisionType);

			if (pl.GrahaDristi(zh)) ret++;
			if (pj.GrahaDristi(zh)) ret++;
			if (pm.GrahaDristi(zh)) ret++;
			return ret;
		}
        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb)
        {
			int a = this.Value(za);
			int b = this.Value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
        public bool Stronger(BodyName m, BodyName n)
		{
			ZodiacHouseName zm = horoscope.GetPosition(m).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			ZodiacHouseName zn = horoscope.GetPosition(n).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			return Stronger (zm, zn);
		}
	}



}
	
