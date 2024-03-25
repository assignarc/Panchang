

namespace org.transliteral.panchang
{
    public class GhatakaLagnaSame
	{
		static public bool checkLagna (ZodiacHouse janma, ZodiacHouse same)
		{
			ZodiacHouseName ja = janma.value;
			ZodiacHouseName gh = ZodiacHouseName.Ari;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = ZodiacHouseName.Ari; break;
				case ZodiacHouseName.Tau: gh = ZodiacHouseName.Tau; break;
				case ZodiacHouseName.Gem: gh = ZodiacHouseName.Can; break;
				case ZodiacHouseName.Can: gh = ZodiacHouseName.Lib; break;
				case ZodiacHouseName.Leo: gh = ZodiacHouseName.Cap; break;
				case ZodiacHouseName.Vir: gh = ZodiacHouseName.Pis; break;
				case ZodiacHouseName.Lib: gh = ZodiacHouseName.Vir; break;
				case ZodiacHouseName.Sco: gh = ZodiacHouseName.Sco; break;
				case ZodiacHouseName.Sag: gh = ZodiacHouseName.Sag; break;
				case ZodiacHouseName.Cap: gh = ZodiacHouseName.Aqu; break;
				case ZodiacHouseName.Aqu: gh = ZodiacHouseName.Gem; break;
				case ZodiacHouseName.Pis: gh = ZodiacHouseName.Leo; break;
			}
			return same.value == gh;
		}
	}


}
