

namespace org.transliteral.panchang
{
    public class GhatakaTithi
	{
        static public bool CheckTithi(ZodiacHouse janmaRasi, Tithi t)
		{
			ZodiacHouseName ja = janmaRasi.Value;
			NandaType gh = NandaType.Nanda;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = NandaType.Nanda; break;
				case ZodiacHouseName.Tau: gh = NandaType.Purna; break;
				case ZodiacHouseName.Gem: gh = NandaType.Bhadra; break;
				case ZodiacHouseName.Can: gh = NandaType.Bhadra; break;
				case ZodiacHouseName.Leo: gh = NandaType.Jaya; break;
				case ZodiacHouseName.Vir: gh = NandaType.Purna; break;
				case ZodiacHouseName.Lib: gh = NandaType.Rikta; break;
				case ZodiacHouseName.Sco: gh = NandaType.Nanda; break;
				case ZodiacHouseName.Sag: gh = NandaType.Jaya; break;
				case ZodiacHouseName.Cap: gh = NandaType.Rikta; break;
				case ZodiacHouseName.Aqu: gh = NandaType.Jaya; break;
				case ZodiacHouseName.Pis: gh = NandaType.Purna; break;
			}
			return t.ToNandaType() == gh;
		}
	}


}
