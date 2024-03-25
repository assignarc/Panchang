

namespace org.transliteral.panchang
{
    // Stronger rasi has its lord in a house of different oddity
    // Stronger graha in such a rasi
    public class StrengthByLordInDifferentOddity : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordInDifferentOddity (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		protected int oddityValueForZodiacHouse (ZodiacHouseName zh)
		{
			Body.Name lname = this.GetStrengthLord(zh);
			BodyPosition lbpos = h.getPosition(lname);
			DivisionPosition ldpos = h.CalculateDivisionPosition(lbpos, dtype);
			ZodiacHouse zh_lor = ldpos.zodiac_house;

			//System.Console.WriteLine("   DiffOddity {0} {1} {2}", zh.ToString(), zh_lor.value.ToString(), (int)zh %2==(int)zh_lor.value%2);
			if ((int)zh % 2 == (int)zh_lor.value % 2)
				return 0;

			return 1;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int a = this.oddityValueForZodiacHouse(za);
			int b = this.oddityValueForZodiacHouse(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name ba, Body.Name bb)
		{
			ZodiacHouseName za = h.getPosition(ba).toDivisionPosition(dtype).zodiac_house.value;
			ZodiacHouseName zb = h.getPosition(bb).toDivisionPosition(dtype).zodiac_house.value;
			return stronger (za, zb);
		}
	}



}
	
