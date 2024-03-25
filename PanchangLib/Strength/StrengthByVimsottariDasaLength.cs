

using System;
namespace org.transliteral.panchang
{
    public class StrengthByVimsottariDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByVimsottariDasaLength (Horoscope h, Division dtype)
			: base (h, dtype, false) {}
		protected double value (ZodiacHouseName zh)
		{
			double length = 0;
			foreach (BodyPosition bp in h.positionList)
			{
				if (bp.type == BodyType.Name.Graha)
					length = Math.Max(length, VimsottariDasa.LengthOfDasa(bp.name));
			}
			return length;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb)
		{
			double a = value (za);
			double b = value (zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			double a = VimsottariDasa.LengthOfDasa(m);
			double b = VimsottariDasa.LengthOfDasa(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
	}



}
	
