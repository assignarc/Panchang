

namespace org.transliteral.panchang
{
    // Stronger rasi is the first one
    // Stronger graha is the first one
    public class StrengthByFirst: BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByFirst (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb) => true;
        public bool Stronger(Body.Name m, Body.Name n) => true;
    }



}
	
