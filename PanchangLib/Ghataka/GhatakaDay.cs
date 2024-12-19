

namespace org.transliteral.panchang
{
    public class GhatakaDay
	{
        public static bool CheckDay(ZodiacHouse janmaRasi, Weekday wd)
		{
			ZodiacHouseName ja = janmaRasi.Value;
			Weekday gh = Weekday.Sunday;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = Weekday.Sunday; break;
				case ZodiacHouseName.Tau: gh = Weekday.Saturday; break;
				case ZodiacHouseName.Gem: gh = Weekday.Monday; break;
				case ZodiacHouseName.Can: gh = Weekday.Wednesday; break;
				case ZodiacHouseName.Leo: gh = Weekday.Saturday; break;
				case ZodiacHouseName.Vir: gh = Weekday.Saturday; break;
				case ZodiacHouseName.Lib: gh = Weekday.Thursday; break;
				case ZodiacHouseName.Sco: gh = Weekday.Friday; break;
				case ZodiacHouseName.Sag: gh = Weekday.Friday; break;
				case ZodiacHouseName.Cap: gh = Weekday.Tuesday; break;
				case ZodiacHouseName.Aqu: gh = Weekday.Thursday; break;
				case ZodiacHouseName.Pis: gh = Weekday.Friday; break;
			}
			return wd == gh;
		}
	}


}
