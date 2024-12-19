

namespace org.transliteral.panchang
{
    // Stronger rasi has more conjunctions/rasi drishtis of Jupiter, Mercury and Lord
    // Stronger graha is in such a rasi
    public class StrengthByAspectsRasi : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAspectsRasi (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int Value(ZodiacHouse zj, ZodiacHouse zm, ZodiacHouseName zx)
		{
			int ret = 0;
			ZodiacHouse zh = new ZodiacHouse(zx);

			BodyName bl = this.GetStrengthLord(zx);
			ZodiacHouse zl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType).ZodiacHouse;

			if (zj.RasiDristi(zh) || zj.Value == zh.Value) ret++;
			if (zm.RasiDristi(zh) || zm.Value == zh.Value) ret++;
			if (zl.RasiDristi(zh) || zl.Value == zh.Value) ret++;
			return ret;
		}
		public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb) 
		{
			ZodiacHouse zj = horoscope.GetPosition(BodyName.Jupiter).ToDivisionPosition(divisionType).ZodiacHouse;
			ZodiacHouse zm = horoscope.GetPosition(BodyName.Mercury).ToDivisionPosition(divisionType).ZodiacHouse;

			int a = this.Value(zj, zm, za);
			int b = this.Value(zj, zm, zb);
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
	
