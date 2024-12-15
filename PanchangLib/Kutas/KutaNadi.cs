

namespace org.transliteral.panchang
{
    public class KutaNadi
	{
		public enum EType
		{
			IVata, 
			IPitta, 
			ISleshma
		};
        public static int GetMaxScore() => 2;
        public static int GetScore(Nakshatra m, Nakshatra n)
		{
			EType ea = GetType(m);
			EType eb = GetType(n);
			if (ea != eb) return 2;
			if (ea == EType.IVata || ea == EType.ISleshma) return 1;
			return 0;
		}
		public static EType GetType (Nakshatra n)
		{
			switch (n.Value)
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
