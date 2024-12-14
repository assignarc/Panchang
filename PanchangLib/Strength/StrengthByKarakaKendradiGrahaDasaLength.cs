

using System;
namespace org.transliteral.panchang
{
    // Stronger graha has longer length
    // Stronger rasi has a graha with longer length placed therein
    public class StrengthByKarakaKendradiGrahaDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByKarakaKendradiGrahaDasaLength (Horoscope h, Division dtype)
			: base (h, dtype, false) { }
		protected double value (ZodiacHouseName zh)
		{
			double length = 0;
			foreach (BodyPosition bp in h.positionList)
			{
				if (bp.type == BodyType.Name.Graha)
				{
					DivisionPosition dp = bp.ToDivisionPosition(dtype);
					length = Math.Max(length, KarakaKendradiGrahaDasa.LengthOfDasa(h, dtype, bp.name, dp));
				}
			}
			return length;
		}
		protected double value (Body.Name b)
		{
			DivisionPosition dp = h.getPosition(b).ToDivisionPosition(dtype);
			return KarakaKendradiGrahaDasa.LengthOfDasa(h, dtype, b, dp);
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb)
		{
			double a = value(za);
			double b = value(zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			double a = value (m);
			double b = value (n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
	}



}
	
