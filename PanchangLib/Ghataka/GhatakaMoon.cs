namespace org.transliteral.panchang
{

    public class GhatakaMoon
	{
        public static bool CheckGhataka(ZodiacHouse janmaRasi, ZodiacHouse chandraRasi)
		{
			ZodiacHouseName ja = janmaRasi.Value;
			ZodiacHouseName ch = chandraRasi.Value;

			ZodiacHouseName gh = ZodiacHouseName.Ari;

			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = ZodiacHouseName.Ari; break;
				case ZodiacHouseName.Tau: gh = ZodiacHouseName.Vir; break;
				case ZodiacHouseName.Gem: gh = ZodiacHouseName.Aqu; break;
				case ZodiacHouseName.Can: gh = ZodiacHouseName.Leo; break;
				case ZodiacHouseName.Leo: gh = ZodiacHouseName.Cap; break;
				case ZodiacHouseName.Vir: gh = ZodiacHouseName.Gem; break;
				case ZodiacHouseName.Lib: gh = ZodiacHouseName.Sag; break;
				case ZodiacHouseName.Sco: gh = ZodiacHouseName.Tau; break;
				case ZodiacHouseName.Sag: gh = ZodiacHouseName.Pis; break;
				case ZodiacHouseName.Cap: gh = ZodiacHouseName.Leo; break;
				case ZodiacHouseName.Aqu: gh = ZodiacHouseName.Sag; break;
				case ZodiacHouseName.Pis: gh = ZodiacHouseName.Aqu; break;
			}

			return ch == gh;
		}
	}


}
