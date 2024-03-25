

namespace org.transliteral.panchang
{
    // Stronger rasi's lord is AK
    public class StrengthByLordIsAtmaKaraka : BaseStrength, IStrengthRasi
	{
		public StrengthByLordIsAtmaKaraka (Horoscope h, Division dtype, bool bSimpleLord)
			: base (h, dtype, bSimpleLord) {}

		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			Body.Name lora = this.GetStrengthLord(za); 
			Body.Name lorb = this.GetStrengthLord(zb);
			Body.Name ak = findAtmaKaraka();
			if (lora == ak) return true;
			if (lorb == ak) return false;
			throw new EqualStrength();
		}
	}



}
	
