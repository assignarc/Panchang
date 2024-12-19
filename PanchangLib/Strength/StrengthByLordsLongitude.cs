

namespace org.transliteral.panchang
{
    // Stronger rasi's lord has traversed larger longitude
    public class StrengthByLordsLongitude : BaseStrength, IStrengthRasi
	{
		public StrengthByLordsLongitude (Horoscope h, Division dtype, bool bSimpleLord) : base (h, dtype, bSimpleLord) {}

        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb)
        {
			BodyName lora = this.GetStrengthLord(za);
			BodyName lorb = this.GetStrengthLord(zb);
			double offa = this.KarakaLongitude(lora);
			double offb = this.KarakaLongitude(lorb);
			if (offa > offb) return true;
			if (offb > offa) return false;
			throw new EqualStrength();
		}
	}



}
	
