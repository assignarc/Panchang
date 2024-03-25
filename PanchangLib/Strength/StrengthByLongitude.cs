

namespace org.transliteral.panchang
{
    // Stronger rasi has a graha which has traversed larger longitude
    // Stronger graha has traversed larger longitude in its house
    public class StrengthByLongitude : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLongitude (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			Body.Name[] karakaBodies =
			{
				Body.Name.Sun, Body.Name.Moon, Body.Name.Mars, Body.Name.Mercury,
				Body.Name.Jupiter, Body.Name.Venus, Body.Name.Saturn, Body.Name.Rahu
			};

			double lona = 0.0, lonb = 0.0;
			foreach (Body.Name bn in karakaBodies) 
			{
				DivisionPosition div = h.getPosition(bn).toDivisionPosition(new Division(DivisionType.Rasi));
				double offset = karakaLongitude (bn);
				if (div.zodiac_house.value == za && offset > lona)
					lona = offset;
				else if (div.zodiac_house.value == zb && offset > lonb)
					lonb = offset;
			}
			if (lona > lonb) return true;
			if (lonb > lona) return false;
			throw new EqualStrength();
		}
		public bool stronger (Body.Name m, Body.Name n)
		{
			double lonm = karakaLongitude (m);
			double lonn = karakaLongitude (n);
			if (lonm > lonn) return true;
			if (lonn > lonm) return false;
			throw new EqualStrength();
		}
	}



}
	
