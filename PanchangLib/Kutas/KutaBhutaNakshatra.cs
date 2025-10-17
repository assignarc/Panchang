

using System.Diagnostics;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Summary description for Kutas.
    /// </summary>

    public class KutaBhutaNakshatra
	{
		public static int GetMaxScore ()
		{
			return 1;
		}
        public static int GetScore(Nakshatra m, Nakshatra n)
		{
			EType a = GetType(m);
			EType b = GetType(n);
			if (a == b) return 1;
			
			if ((a == EType.IFire && b == EType.IAir) ||
				(a == EType.IAir && b == EType.IFire)) return 1;

			if (a == EType.IEarth || b == EType.IEarth) return 1;

			return 0;

		}
		public enum EType
		{
			IEarth, 
			IWater, 
			IFire, 
			IAir, 
			IEther
		};
		public static EType GetType (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Ashwini:
				case NakshatraName.Bharani:
				case NakshatraName.Krittika:
				case NakshatraName.Rohini:
				case NakshatraName.Mrigashira:
					return EType.IEarth;
				case NakshatraName.Ardra:
				case NakshatraName.Punarvasu:
				case NakshatraName.Pushya:
				case NakshatraName.Ashlesha:
				case NakshatraName.Magha:
				case NakshatraName.PoorvaPhalguni:
					return EType.IWater;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Hasta:
				case NakshatraName.Chitra:
				case NakshatraName.Swati:
				case NakshatraName.Vishakha:
					return EType.IFire;
				case NakshatraName.Anuradha:
				case NakshatraName.Jyestha:
				case NakshatraName.Moola:
				case NakshatraName.PoorvaShada:
				case NakshatraName.UttaraShada:
				case NakshatraName.Shravana:
					return EType.IAir;
				case NakshatraName.Dhanishta:
				case NakshatraName.Shatabhisha:
				case NakshatraName.PoorvaBhadra:
				case NakshatraName.UttaraBhadra:
				case NakshatraName.Revati:
					return EType.IEther;
			}
			Debug.Assert(false, "KutaBhutaNakshatra::getType");
			return EType.IAir;
		}
	}


}
