

namespace org.transliteral.panchang
{
    // Stronger rasi's lord by nature (moveable, fixed, dual)
    // Stronger graha's dispositor in such a rasi
    public class StrengthByLordsNature : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordsNature (Horoscope h, Division dtype)
			: base (h, dtype, true) {}
		public int naturalValueForRasi (ZodiacHouseName zha)
		{
			Body.Name bl = h.LordOfZodiacHouse(zha, dtype);
			ZodiacHouseName zhl = h.getPosition(bl).ToDivisionPosition(dtype).zodiac_house.value;

			int[] vals = new int[] {3,1,2}; // dual, move, fix
			return vals[(int)zhl % 3];
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int[] vals = new int[] {3,1,2}; // dual, move, fix
			int a = this.naturalValueForRasi(za);
			int b = this.naturalValueForRasi(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			ZodiacHouseName za = h.getPosition(m).ToDivisionPosition(dtype).zodiac_house.value;
			ZodiacHouseName zb = h.getPosition(n).ToDivisionPosition(dtype).zodiac_house.value;
			return stronger (za, zb);
		}
	}



}
	
