

using System;
namespace org.transliteral.panchang
{
    // Stronger graha has longer length
    // Stronger rasi has a graha with longer length placed therein
    public class StrengthByKarakaKendradiGrahaDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByKarakaKendradiGrahaDasaLength (Horoscope h, Division dtype)
			: base (h, dtype, false) { }
        protected double Value(ZodiacHouseName zh)
		{
			double length = 0;
			foreach (BodyPosition bp in horoscope.PositionList)
			{
				if (bp.type == BodyType.Name.Graha)
				{
					DivisionPosition dp = bp.ToDivisionPosition(divisionType);
					length = Math.Max(length, KarakaKendradiGrahaDasa.LengthOfDasa(horoscope, divisionType, bp.name, dp));
				}
			}
			return length;
		}
        protected double Value(BodyName b)
		{
			DivisionPosition dp = horoscope.GetPosition(b).ToDivisionPosition(divisionType);
			return KarakaKendradiGrahaDasa.LengthOfDasa(horoscope, divisionType, b, dp);
		}
        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb)
		{
			double a = Value(za);
			double b = Value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
        public bool Stronger(BodyName m, BodyName n)
		{
			double a = Value (m);
			double b = Value (n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
	}



}
	
