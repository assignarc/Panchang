

namespace org.transliteral.panchang
{
    public class KutaRajju
	{
		public enum EType
		{
			IKantha, IKati, IPada, ISiro, IKukshi
		};
		public static int getScore (Nakshatra m, Nakshatra n)
		{
			if (getType(m) != getType(n)) return 1;
			return 0;
		}
		public static int getMaxScore ()
		{
			return 1;
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.value)
			{
				case NakshatraName.Rohini:
				case NakshatraName.Aridra:
				case NakshatraName.Hasta:
				case NakshatraName.Swati:
				case NakshatraName.Sravana:
				case NakshatraName.Satabisha:
					return EType.IKantha;
				case NakshatraName.Bharani:
				case NakshatraName.Pushya:
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.Anuradha:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraBhadra:
					return EType.IKati;
				case NakshatraName.Aswini:
				case NakshatraName.Aslesha:
				case NakshatraName.Makha:
				case NakshatraName.Jyestha:
				case NakshatraName.Moola:
				case NakshatraName.Revati:
					return EType.IPada;
				case NakshatraName.Mrigarirsa:
				case NakshatraName.Dhanishta:
				case NakshatraName.Chittra:
					return EType.ISiro;
			}
			return EType.IKukshi;
		}
		
	}


}
