namespace org.transliteral.panchang
{

    // Stronger rasi has larger number of grahas
    // Stronger graha is in such a rasi
    public class StrengthByConjunction : BaseStrength, IStrengthRasi, IStrengthGraha
	{		
		public StrengthByConjunction (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			int numa = this.NumGrahasInZodiacHouse (za);
			int numb = this.NumGrahasInZodiacHouse (zb);
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
	
