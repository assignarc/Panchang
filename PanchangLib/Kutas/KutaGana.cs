

namespace org.transliteral.panchang
{
    public class KutaGana
	{
		public enum EType
		{
			IDeva, INara, IRakshasa
		};
		public static int getScore (Nakshatra m, Nakshatra f)
		{
			EType em = KutaGana.getType(m);
			EType ef = KutaGana.getType(f);

			if (em == ef) return 5;
			if (em == EType.IDeva && ef == EType.INara) return 4;
			if (em == EType.IRakshasa && ef == EType.INara) return 3;
			if (em == EType.INara && ef == EType.IDeva) return 2;
			return 1;
		}
		public static int getMaxScore ()
		{
			return 5;
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Mrigarirsa:
				case NakshatraName.Punarvasu:
				case NakshatraName.Pushya:
				case NakshatraName.Hasta:
				case NakshatraName.Swati:
				case NakshatraName.Anuradha:
				case NakshatraName.Sravana:
				case NakshatraName.Revati:
					return EType.IDeva;
				case NakshatraName.Bharani:
				case NakshatraName.Rohini:
				case NakshatraName.Aridra:
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraShada:
				case NakshatraName.PoorvaBhadra:
				case NakshatraName.UttaraBhadra:
					return EType.INara;
			}
			return EType.IRakshasa;
		}
	}


}
