

namespace org.transliteral.panchang
{
    public class GhatakaStar
	{
        public static bool CheckStar(ZodiacHouse janmaRasi, Nakshatra nak)
		{
			ZodiacHouseName ja = janmaRasi.Value;
			NakshatraName gh = NakshatraName.Aswini;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = NakshatraName.Makha; break;
				case ZodiacHouseName.Tau: gh = NakshatraName.Hasta; break;
				case ZodiacHouseName.Gem: gh = NakshatraName.Swati; break;
				case ZodiacHouseName.Can: gh = NakshatraName.Anuradha; break;
				case ZodiacHouseName.Leo: gh = NakshatraName.Moola; break;
				case ZodiacHouseName.Vir: gh = NakshatraName.Sravana; break;
				case ZodiacHouseName.Lib: gh = NakshatraName.Satabisha; break;
				case ZodiacHouseName.Sco: gh = NakshatraName.Revati; break;
				// FIXME dveja nakshatra?????
				case ZodiacHouseName.Sag: gh = NakshatraName.Revati; break;
				case ZodiacHouseName.Cap: gh = NakshatraName.Rohini; break;
				case ZodiacHouseName.Aqu: gh = NakshatraName.Aridra; break;
				case ZodiacHouseName.Pis: gh = NakshatraName.Aslesha; break;
			}
			return nak.Value == gh;
		}
	}


}
