

namespace org.transliteral.panchang
{
    // Stronger rasi has more graha drishtis of Jupiter, Mercury and Lord
    // Stronger graha is in such a rasi
    public class StrengthByAspectsGraha : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAspectsGraha (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int Value (ZodiacHouseName _zh)
		{
			int val = 0;
			Body.Name bl = this.GetStrengthLord(_zh);
			DivisionPosition dl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType);
			DivisionPosition dj = horoscope.GetPosition(Body.Name.Jupiter).ToDivisionPosition(divisionType);
			DivisionPosition dm = horoscope.GetPosition(Body.Name.Mercury).ToDivisionPosition(divisionType);

			ZodiacHouse zh = new ZodiacHouse(_zh);
			if (dl.GrahaDristi(zh) || dl.ZodiacHouse.Value == _zh) val++;
			if (dj.GrahaDristi(zh) || dj.ZodiacHouse.Value == _zh) val++;
			if (dm.GrahaDristi(zh) || dm.ZodiacHouse.Value == _zh) val++;

			return val;
		}
        protected int Value(Body.Name bm)
		{
			return Value (horoscope.GetPosition(bm).ToDivisionPosition(divisionType).ZodiacHouse.Value);
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int a = this.Value(za);
			int b = this.Value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool Stronger (Body.Name m, Body.Name n)
		{
			int a = this.Value(m);
			int b = this.Value(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();

		}
	}



}
	
