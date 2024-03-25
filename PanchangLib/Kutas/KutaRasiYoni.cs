

namespace org.transliteral.panchang
{
    public class KutaRasiYoni
	{
		public enum EType
		{
			IPakshi, IReptile, IPasu, INara
		};
		public static EType getType (ZodiacHouse z)
		{
			switch (z.value)
			{
				case ZodiacHouseName.Cap:
				case ZodiacHouseName.Pis:
					return EType.IPakshi;
				case ZodiacHouseName.Can:
				case ZodiacHouseName.Sco:
					return EType.IReptile;
				case ZodiacHouseName.Ari:
				case ZodiacHouseName.Tau:
				case ZodiacHouseName.Leo:
					return EType.IPasu;
			}
			return EType.INara;
		}
	}


}
