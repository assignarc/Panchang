

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaGotra
	{
		public enum EType
		{
			IMarichi, 
			IVasishtha, 
			IAngirasa, 
			IAtri, 
			IPulastya,
			IPulaha, 
			IKretu
		};
        public static int GetScore(Nakshatra m, Nakshatra n)
		{
			if (GetType(m) == GetType(n)) return 0;
			return 1;
		}
		public static int GetMaxScore ()
		{
			return 1;
		}
		public static EType GetType (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Pushya:
				case NakshatraName.Swati:
					return EType.IMarichi;
				case NakshatraName.Bharani:
				case NakshatraName.Aslesha:
				case NakshatraName.Vishaka:
				case NakshatraName.Sravana:
					return EType.IVasishtha;
				case NakshatraName.Krittika:
				case NakshatraName.Makha:
				case NakshatraName.Anuradha:
				case NakshatraName.Dhanishta:
					return EType.IAngirasa;
				case NakshatraName.Rohini:
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.Jyestha:
				case NakshatraName.Satabisha:
					return EType.IAtri;
				case NakshatraName.Mrigarirsa:
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Moola:
				case NakshatraName.PoorvaBhadra:
					return EType.IPulastya;
				case NakshatraName.Aridra:
				case NakshatraName.Hasta:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraBhadra:
					return EType.IPulaha;
				case NakshatraName.Punarvasu:
				case NakshatraName.Chittra:
				case NakshatraName.UttaraShada:
				case NakshatraName.Revati:
					return EType.IKretu;
			}
			Debug.Assert(false, "KutaGotra::getType");
			return EType.IAngirasa;
		}
	}


}
