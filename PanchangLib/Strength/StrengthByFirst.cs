

namespace org.transliteral.panchang
{
    // Stronger rasi is the first one
    // Stronger graha is the first one
    public class StrengthByFirst: BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByFirst (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			return true;
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			return true;
		}
	}



}
	
