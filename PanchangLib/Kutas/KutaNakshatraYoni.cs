

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaNakshatraYoni 
	{
		public enum EType
		{
			IHorse, 
			IElephant, 
			ISheep, 
			ISerpent, 
			IDog, 
			ICat, 
			IRat, 
			ICow,
			IBuffalo, 
			ITiger, 
			IHare, 
			IMonkey, 
			ILion, 
			IMongoose
		};
		public enum ESex
		{
			IMale, IFemale
		};

        public static ESex GetSex(Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Ashwini:
				case NakshatraName.Bharani:
				case NakshatraName.Pushya:
				case NakshatraName.Rohini:
				case NakshatraName.Moola:
				case NakshatraName.Ashlesha:
				case NakshatraName.Magha:
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.Swati:
				case NakshatraName.Vishakha:
				case NakshatraName.Jyestha:
				case NakshatraName.PoorvaShada:
				case NakshatraName.PoorvaBhadra:
				case NakshatraName.UttaraShada:
					return ESex.IMale;
				default:
					return ESex.IFemale;
			}
		
		}
		public static EType getType (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Ashwini:
				case NakshatraName.Shatabhisha:
					return EType.IHorse;
				case NakshatraName.Bharani:
				case NakshatraName.Revati:
					return EType.IElephant;
				case NakshatraName.Pushya:
				case NakshatraName.Krittika:
					return EType.ISheep;
				case NakshatraName.Rohini:
				case NakshatraName.Mrigashira:
					return EType.ISerpent;
				case NakshatraName.Moola:
				case NakshatraName.Ardra:
					return EType.IDog;
				case NakshatraName.Ashlesha:
				case NakshatraName.Punarvasu:
					return EType.ICat;
				case NakshatraName.Magha:
				case NakshatraName.PoorvaPhalguni:
					return EType.IRat;
				case NakshatraName.UttaraPhalguni:
				case NakshatraName.UttaraBhadra:
					return EType.ICow;
				case NakshatraName.Swati:
				case NakshatraName.Hasta:
					return EType.IBuffalo;
				case NakshatraName.Vishakha:
				case NakshatraName.Chitra:
					return EType.ITiger;
				case NakshatraName.Jyestha:
				case NakshatraName.Anuradha:
					return EType.IHare;
				case NakshatraName.PoorvaShada:
				case NakshatraName.Shravana:
					return EType.IMonkey;
				case NakshatraName.PoorvaBhadra:
				case NakshatraName.Dhanishta:
					return EType.ILion;
				case NakshatraName.UttaraShada:
					return EType.IMongoose;

			}
			Debug.Assert(false, "KutaNakshatraYoni::getType");
			return EType.IHorse;
		}
	}


}
