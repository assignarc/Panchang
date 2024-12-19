

using System;
namespace org.transliteral.panchang
{
    public class StrengthByVimsottariDasaLength : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByVimsottariDasaLength (Horoscope h, Division dtype)
			: base (h, dtype, false) {}
		protected double Value (ZodiacHouseName zh)
		{
			double length = 0;
			foreach (BodyPosition bp in horoscope.PositionList)
			{
				if (bp.type == BodyType.Name.Graha)
					length = Math.Max(length, VimsottariDasa.LengthOfDasaS(bp.name));
			}
			return length;
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb)
		{
			double a = Value (za);
			double b = Value (zb);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
		public bool Stronger (BodyName m, BodyName n)
		{
			double a = VimsottariDasa.LengthOfDasaS(m);
			double b = VimsottariDasa.LengthOfDasaS(n);
			if (a > b) return true;
			if (a < b) return false;
			throw new EqualStrength();
		}
	}



}
	
