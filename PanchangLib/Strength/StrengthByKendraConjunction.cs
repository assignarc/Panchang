

namespace org.transliteral.panchang
{
    // Stronger rasi has larger number of grahas in kendras
    // Stronger graha is in such a rasi
    public class StrengthByKendraConjunction: BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByKendraConjunction (Horoscope h, Division dtype)
				: base (h, dtype, true) {}

		public int value (ZodiacHouseName _zh)
		{
			int[] kendras = new int[4] {1, 4, 7, 10};
			int numGrahas=0;
			ZodiacHouse zh = new ZodiacHouse(_zh);
			foreach (int i in kendras)
			{
				numGrahas += this.numGrahasInZodiacHouse(zh.add(i).value);
			}
			return numGrahas;
		}
		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int numa = value (za);
			int numb = value (zb);
			if (numa > numb) return true;
			if (numb > numa) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			return stronger (
				h.getPosition(m).ToDivisionPosition(dtype).zodiac_house.value,
				h.getPosition(n).ToDivisionPosition(dtype).zodiac_house.value
				);
		}
	}



}
	
