

using System;

namespace org.transliteral.panchang
{
    // Stronger rasi has its lord in a house of different oddity
    // Stronger graha in such a rasi
    public class StrengthByLordInDifferentOddity : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLordInDifferentOddity (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

        protected int OddityValueForZodiacHouse(ZodiacHouseName zh)
		{
			BodyName lname = this.GetStrengthLord(zh);
			BodyPosition lbpos = horoscope.GetPosition(lname);
			DivisionPosition ldpos = horoscope.CalculateDivisionPosition(lbpos, divisionType);
			ZodiacHouse zh_lor = ldpos.ZodiacHouse;

            Logger.Info(String.Format("DiffOddity {0} {1} {2}", zh.ToString(), zh_lor.Value.ToString(), (int)zh %2==(int)zh_lor.Value%2));
			if ((int)zh % 2 == (int)zh_lor.Value % 2)
				return 0;

			return 1;
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int a = this.OddityValueForZodiacHouse(za);
			int b = this.OddityValueForZodiacHouse(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool Stronger (BodyName ba, BodyName bb)
		{
			ZodiacHouseName za = horoscope.GetPosition(ba).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			ZodiacHouseName zb = horoscope.GetPosition(bb).ToDivisionPosition(divisionType).ZodiacHouse.Value;
			return Stronger (za, zb);
		}
	}



}
	
