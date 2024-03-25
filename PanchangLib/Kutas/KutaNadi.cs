

namespace org.transliteral.panchang
{
    public class KutaNadi
	{
		public enum EType
		{
			IVata, IPitta, ISleshma
		};
		public static int getMaxScore ()
		{
			return 2;
		}
		public static int getScore (Nakshatra m, Nakshatra n)
		{
			EType ea = getType(m);
			EType eb = getType(n);
			if (ea != eb) return 2;
			if (ea == EType.IVata || ea == EType.ISleshma) return 1;
			return 0;
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Aridra:
				case NakshatraName.Punarvasu:
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Hasta:
				case NakshatraName.Jyestha:
				case NakshatraName.Moola:
				case NakshatraName.Satabisha:
				case NakshatraName.PoorvaBhadra:
					return EType.IVata;
				case NakshatraName.Bharani:
				case NakshatraName.Mrigarirsa:
				case NakshatraName.Pushya:
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.Chittra:
				case NakshatraName.Anuradha:
				case NakshatraName.PoorvaShada:
				case NakshatraName.Dhanishta:
				case NakshatraName.UttaraBhadra:
					return EType.IPitta;
			}
			return EType.ISleshma;
		}
	}


}
