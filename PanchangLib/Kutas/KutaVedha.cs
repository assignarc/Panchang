

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaVedha
	{
		public enum EType
		{
			IAswJye, 
			IBhaAnu, 
			IKriVis, 
			IRohSwa, 
			IAriSra,
			IPunUsh, 
			IPusPsh, 
			IAslMoo, 
			IMakRev, 
			IPphUbh,
			IUphPbh, 
			IHasSat, 
			IMriDha, 
			IChi
		}
        public static int GetScore(Nakshatra m, Nakshatra n)
		{
			if (GetType(m) == GetType(n)) return 0;
			return 1;
		}
        public static int GetMaxScore() => 1;
        public static EType GetType (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Ashwini:
				case NakshatraName.Jyestha:
					return EType.IAswJye;
				case NakshatraName.Bharani:
				case NakshatraName.Anuradha:
					return EType.IBhaAnu;
				case NakshatraName.Krittika:
				case NakshatraName.Vishakha:
					return EType.IKriVis;
				case NakshatraName.Rohini:
				case NakshatraName.Swati:
					return EType.IRohSwa;
				case NakshatraName.Ardra:
				case NakshatraName.Shravana:
					return EType.IAriSra;
				case NakshatraName.Punarvasu:
				case NakshatraName.UttaraShada:
					return EType.IPunUsh;
				case NakshatraName.Pushya:
				case NakshatraName.PoorvaShada:
					return EType.IPusPsh;
				case NakshatraName.Ashlesha:
				case NakshatraName.Moola:
					return EType.IAslMoo;
				case NakshatraName.Magha:
				case NakshatraName.Revati:
					return EType.IMakRev;
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.UttaraBhadra:
					return EType.IPphUbh;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.PoorvaBhadra:
					return EType.IUphPbh;
				case NakshatraName.Hasta:
				case NakshatraName.Shatabhisha:
					return EType.IHasSat;
				case NakshatraName.Mrigashira:
				case NakshatraName.Dhanishta:
					return EType.IMriDha;
				case NakshatraName.Chitra:
					return EType.IChi;
			}
			Debug.Assert(false, "KutaVedha::getType");
			return EType.IAriSra;
		}
	}


}
