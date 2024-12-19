

namespace org.transliteral.panchang
{
    public class GhatakaLagnaOpp
	{
        public static bool CheckLagna(ZodiacHouse janma, ZodiacHouse same)
		{
			ZodiacHouseName ja = janma.Value;
			ZodiacHouseName gh = ZodiacHouseName.Ari;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = ZodiacHouseName.Lib; break;
				case ZodiacHouseName.Tau: gh = ZodiacHouseName.Sco; break;
				case ZodiacHouseName.Gem: gh = ZodiacHouseName.Cap; break;
				case ZodiacHouseName.Can: gh = ZodiacHouseName.Ari; break;
				case ZodiacHouseName.Leo: gh = ZodiacHouseName.Can; break;
				case ZodiacHouseName.Vir: gh = ZodiacHouseName.Vir; break;
				case ZodiacHouseName.Lib: gh = ZodiacHouseName.Pis; break;
				case ZodiacHouseName.Sco: gh = ZodiacHouseName.Tau; break;
				case ZodiacHouseName.Sag: gh = ZodiacHouseName.Gem; break;
				case ZodiacHouseName.Cap: gh = ZodiacHouseName.Leo; break;
				case ZodiacHouseName.Aqu: gh = ZodiacHouseName.Sag; break;
				case ZodiacHouseName.Pis: gh = ZodiacHouseName.Aqu; break;
			}
			return same.Value == gh;
		}
	}


}
