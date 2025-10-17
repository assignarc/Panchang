

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
			IBharandhaka, 
			IPingala, 
			ICrow, 
			ICock, 
			IPeacock
		};
		public static EDominator GetDominator (Nakshatra m, Nakshatra n)
		{
			EType em = GetType(m);
			EType en = GetType(n);

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
		public static EType GetType (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Ashwini:
				case NakshatraName.Bharani:
				case NakshatraName.Krittika:
				case NakshatraName.Rohini:
				case NakshatraName.Mrigashira:
					return EType.IBharandhaka;
				case NakshatraName.Ardra:
				case NakshatraName.Punarvasu:
				case NakshatraName.Pushya:
				case NakshatraName.Ashlesha:
				case NakshatraName.Magha:
				case NakshatraName.PoorvaPhalguni:
					return EType.IPingala;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Hasta:
				case NakshatraName.Chitra:
				case NakshatraName.Swati:
				case NakshatraName.Vishakha:
				case NakshatraName.Anuradha:
					return EType.ICrow;
				case NakshatraName.Jyestha:
				case NakshatraName.Moola:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraShada:
				case NakshatraName.Shravana:
					return EType.ICock;
				case NakshatraName.Dhanishta:
				case NakshatraName.Shatabhisha:
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
