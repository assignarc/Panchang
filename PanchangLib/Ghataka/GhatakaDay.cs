

namespace org.transliteral.panchang
{
    public class GhatakaDay
	{
		static public bool checkDay (ZodiacHouse janmaRasi, Basics.Weekday wd)
		{
			ZodiacHouseName ja = janmaRasi.value;
			Basics.Weekday gh = Basics.Weekday.Sunday;
			switch (ja)
			{
				case ZodiacHouseName.Ari: gh = Basics.Weekday.Sunday; break;
				case ZodiacHouseName.Tau: gh = Basics.Weekday.Saturday; break;
				case ZodiacHouseName.Gem: gh = Basics.Weekday.Monday; break;
				case ZodiacHouseName.Can: gh = Basics.Weekday.Wednesday; break;
				case ZodiacHouseName.Leo: gh = Basics.Weekday.Saturday; break;
				case ZodiacHouseName.Vir: gh = Basics.Weekday.Saturday; break;
				case ZodiacHouseName.Lib: gh = Basics.Weekday.Thursday; break;
				case ZodiacHouseName.Sco: gh = Basics.Weekday.Friday; break;
				case ZodiacHouseName.Sag: gh = Basics.Weekday.Friday; break;
				case ZodiacHouseName.Cap: gh = Basics.Weekday.Tuesday; break;
				case ZodiacHouseName.Aqu: gh = Basics.Weekday.Thursday; break;
				case ZodiacHouseName.Pis: gh = Basics.Weekday.Friday; break;
			}
			return wd == gh;
		}
	}


}
