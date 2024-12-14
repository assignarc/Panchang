namespace org.transliteral.panchang
{

    // Stronger rasi has larger number of grahas
    // Stronger graha is in such a rasi
    public class StrengthByConjunction : BaseStrength, IStrengthRasi, IStrengthGraha
	{		
		public StrengthByConjunction (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int numa = this.numGrahasInZodiacHouse (za);
			int numb = this.numGrahasInZodiacHouse (zb);
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
	
