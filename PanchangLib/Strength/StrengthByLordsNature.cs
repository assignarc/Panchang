

namespace org.transliteral.panchang
{
    // Stronger rasi's lord by nature (moveable, fixed, dual)
    // Stronger graha's dispositor in such a rasi
    public class StrengthByLordsNature : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordsNature (Horoscope h, Division dtype)
			: base (h, dtype, true) {}
        public int NaturalValueForRasi(ZodiacHouseName zha)
		{
			Body.Name bl = horoscope.LordOfZodiacHouse(zha, divisionType);
			ZodiacHouseName zhl = horoscope.GetPosition(bl).ToDivisionPosition(divisionType).ZodiacHouse.Value;

			int[] vals = new int[] {3,1,2}; // dual, move, fix
			return vals[(int)zhl % 3];
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int[] vals = new int[] {3,1,2}; // dual, move, fix
			int a = this.NaturalValueForRasi(za);
			int b = this.NaturalValueForRasi(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool Stronger (Body.Name m, Body.Name n)
		{
			ZodiacHouseName za = horoscope.GetPosition(m).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			ZodiacHouseName zb = horoscope.GetPosition(n).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			return Stronger (za, zb);
		}
	}



}
	
