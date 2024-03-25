

namespace org.transliteral.panchang
{
    // Stronger rasi by nature (moveable, fixed, dual)
    // Stronger graha in such a rasi
    public class StrengthByRasisNature : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByRasisNature (Horoscope h, Division dtype)
			: base (h, dtype, true) {}
		public int naturalValueForRasi (ZodiacHouseName zha)
		{
			int[] vals = new int[] {3,1,2}; // dual, move, fix
			return vals[(int)zha % 3];
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
			ZodiacHouseName za = h.getPosition(m).toDivisionPosition(dtype).zodiac_house.value;
			ZodiacHouseName zb = h.getPosition(n).toDivisionPosition(dtype).zodiac_house.value;
			return stronger (za, zb);
		}
	}



}
	
