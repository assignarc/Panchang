

namespace org.transliteral.panchang
{
    // Stronger rasi has a graha which has traversed larger longitude
    // Stronger graha has traversed larger longitude in its house
    public class StrengthByLongitude : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByLongitude (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool Stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			BodyName[] karakaBodies =
			{
				BodyName.Sun, BodyName.Moon, BodyName.Mars, BodyName.Mercury,
				BodyName.Jupiter, BodyName.Venus, BodyName.Saturn, BodyName.Rahu
			};

			double lona = 0.0, lonb = 0.0;
			foreach (BodyName bn in karakaBodies) 
			{
				DivisionPosition div = horoscope.GetPosition(bn).ToDivisionPosition(new Division(DivisionType.Rasi));
				double offset = KarakaLongitude (bn);
				if (div.ZodiacHouse.Value == za && offset > lona)
					lona = offset;
				else if (div.ZodiacHouse.Value == zb && offset > lonb)
					lonb = offset;
			}
			if (lona > lonb) return true;
			if (lonb > lona) return false;
			throw new EqualStrength();
		}
		public bool Stronger (BodyName m, BodyName n)
		{
			double lonm = KarakaLongitude (m);
			double lonn = KarakaLongitude (n);
			if (lonm > lonn) return true;
			if (lonn > lonm) return false;
			throw new EqualStrength();
		}
	}



}
	
