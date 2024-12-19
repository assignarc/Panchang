

namespace org.transliteral.panchang
{
    // Stronger rasi's lord is AK
    public class StrengthByLordIsAtmaKaraka : BaseStrength, IStrengthRasi
	{
		public StrengthByLordIsAtmaKaraka (Horoscope h, Division dtype, bool bSimpleLord) : base (h, dtype, bSimpleLord) {}

		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			BodyName lora = this.GetStrengthLord(za); 
			BodyName lorb = this.GetStrengthLord(zb);
			BodyName ak = FindAtmaKaraka();
			if (lora == ak) return true;
			if (lorb == ak) return false;
			throw new EqualStrength();
		}
	}



}
	
