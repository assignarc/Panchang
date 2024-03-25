

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaVihanga
	{
		public enum EDominator
		{
			IEqual, IMale, IFemale
		};
		public enum EType
		{
			IBharandhaka, IPingala, ICrow, ICock, IPeacock
		};
		public static EDominator getDominator (Nakshatra m, Nakshatra n)
		{
			EType em = getType(m);
			EType en = getType(n);

			EType[] order = new EType[] 
			{
				EType.IPeacock, EType.ICock, EType.ICrow, EType.IPingala
			};
			if (em == en) return EDominator.IEqual;
			for (int i=0; i<order.Length; i++)
			{
				if (em == order[i]) return EDominator.IMale;
				if (en == order[i]) return EDominator.IFemale;
			}
			return EDominator.IEqual;
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Bharani:
				case NakshatraName.Krittika:
				case NakshatraName.Rohini:
				case NakshatraName.Mrigarirsa:
					return EType.IBharandhaka;
				case NakshatraName.Aridra:
				case NakshatraName.Punarvasu:
				case NakshatraName.Pushya:
				case NakshatraName.Aslesha:
				case NakshatraName.Makha:
				case NakshatraName.PoorvaPhalguni:
					return EType.IPingala;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Hasta:
				case NakshatraName.Chittra:
				case NakshatraName.Swati:
				case NakshatraName.Vishaka:
				case NakshatraName.Anuradha:
					return EType.ICrow;
				case NakshatraName.Jyestha:
				case NakshatraName.Moola:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraShada:
				case NakshatraName.Sravana:
					return EType.ICock;
				case NakshatraName.Dhanishta:
				case NakshatraName.Satabisha:
				case NakshatraName.PoorvaBhadra:
				case NakshatraName.UttaraBhadra:
				case NakshatraName.Revati:
					return EType.IPeacock;
			}
			Debug.Assert(false, "KutaVibhanga::getType");
			return EType.IBharandhaka;
		}
	}


}
