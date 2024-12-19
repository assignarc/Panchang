

namespace org.transliteral.panchang
{
    // Stronger rasi has larger number of grahas in kendras
    // Stronger graha is in such a rasi
    public class StrengthByKendraConjunction: BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByKendraConjunction (Horoscope h, Division dtype)
				: base (h, dtype, true) {}

		public int Value (ZodiacHouseName _zh)
		{
			int[] kendras = new int[4] {1, 4, 7, 10};
			int numGrahas=0;
			ZodiacHouse zh = new ZodiacHouse(_zh);
			foreach (int i in kendras)
			{
				numGrahas += this.NumGrahasInZodiacHouse(zh.Add(i).Value);
			}
			return numGrahas;
		}
		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int numa = Value (za);
			int numb = Value (zb);
			if (numa > numb) return true;
			if (numb > numa) return false;
			throw new EqualStrength();
		}
		public bool Stronger (BodyName m, BodyName n)
		{
			return Stronger (
				horoscope.GetPosition(m).ToDivisionPosition(divisionType).ZodiacHouse.Value,
				horoscope.GetPosition(n).ToDivisionPosition(divisionType).ZodiacHouse.Value
				);
		}
	}



}
	
