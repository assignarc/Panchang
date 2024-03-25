

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaVedha
	{
		public enum EType
		{
			IAswJye, IBhaAnu, IKriVis, IRohSwa, IAriSra,
			IPunUsh, IPusPsh, IAslMoo, IMakRev, IPphUbh,
			IUphPbh, IHasSat, IMriDha, IChi
		}
		public static int getScore (Nakshatra m, Nakshatra n)
		{
			if (getType(m) == getType(n)) return 0;
			return 1;
		}
		public static int getMaxScore ()
		{
			return 1;
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Jyestha:
					return EType.IAswJye;
				case NakshatraName.Bharani:
				case NakshatraName.Anuradha:
					return EType.IBhaAnu;
				case NakshatraName.Krittika:
				case NakshatraName.Vishaka:
					return EType.IKriVis;
				case NakshatraName.Rohini:
				case NakshatraName.Swati:
					return EType.IRohSwa;
				case NakshatraName.Aridra:
				case NakshatraName.Sravana:
					return EType.IAriSra;
				case NakshatraName.Punarvasu:
				case NakshatraName.UttaraShada:
					return EType.IPunUsh;
				case NakshatraName.Pushya:
				case NakshatraName.PoorvaShada:
					return EType.IPusPsh;
				case NakshatraName.Aslesha:
				case NakshatraName.Moola:
					return EType.IAslMoo;
				case NakshatraName.Makha:
				case NakshatraName.Revati:
					return EType.IMakRev;
				case NakshatraName.PoorvaPhalguni:
				case NakshatraName.UttaraBhadra:
					return EType.IPphUbh;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.PoorvaBhadra:
					return EType.IUphPbh;
				case NakshatraName.Hasta:
				case NakshatraName.Satabisha:
					return EType.IHasSat;
				case NakshatraName.Mrigarirsa:
				case NakshatraName.Dhanishta:
					return EType.IMriDha;
				case NakshatraName.Chittra:
					return EType.IChi;
			}
			Debug.Assert(false, "KutaVedha::getType");
			return EType.IAriSra;
		}
	}


}
