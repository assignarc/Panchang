

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
			BodyName bl = this.GetStrengthLord(_zh);
			DivisionPosition dl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType);
			DivisionPosition dj = horoscope.GetPosition(BodyName.Jupiter).ToDivisionPosition(divisionType);
			DivisionPosition dm = horoscope.GetPosition(BodyName.Mercury).ToDivisionPosition(divisionType);

			ZodiacHouse zh = new ZodiacHouse(_zh);
			if (dl.GrahaDristi(zh) || dl.ZodiacHouse.Value == _zh) val++;
			if (dj.GrahaDristi(zh) || dj.ZodiacHouse.Value == _zh) val++;
			if (dm.GrahaDristi(zh) || dm.ZodiacHouse.Value == _zh) val++;

			return val;
		}
        protected int Value(BodyName bm)
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
		public bool Stronger (BodyName m, BodyName n)
		{
			int a = this.Value(m);
			int b = this.Value(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();

		}
	}



}
	
