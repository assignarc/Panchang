using System.Collections;

namespace org.transliteral.panchang
{
    abstract public class BaseStrength
    {
        protected ArrayList standardGrahas;
        protected Division divisionType;
        protected ArrayList standardDivisionPositions;
        protected Horoscope horoscope;
        protected bool bUseSimpleLords;

        protected Body.Name GetStrengthLord(ZodiacHouseName zh)
        {
            if (bUseSimpleLords)
                return Basics.SimpleLordOfZodiacHouse(zh);
            else
                return horoscope.LordOfZodiacHouse(new ZodiacHouse(zh), divisionType);
        }
        protected Body.Name GetStrengthLord(ZodiacHouse zh)
        {
            return GetStrengthLord(zh.Value);
        }
        protected BaseStrength(Horoscope _h, Division _dtype, bool _bUseSimpleLords)
        {
            horoscope = _h;
            divisionType = _dtype;
            bUseSimpleLords = _bUseSimpleLords;
            standardDivisionPositions = horoscope.CalculateDivisionPositions(divisionType);
        }
        protected int NumGrahasInZodiacHouse(ZodiacHouseName zh)
        {
            int num = 0;
            foreach (DivisionPosition dp in standardDivisionPositions)
            {
                if (dp.Type != BodyType.Name.Graha)
                    continue;
                if (dp.ZodiacHouse.Value == zh)
                    num = num + 1;
            }
            return num;
        }
        protected double KarakaLongitude(Body.Name b)
        {
            double lon = horoscope.GetPosition(b).Longitude.ToZodiacHouseOffset();
            if (b == Body.Name.Rahu || b == Body.Name.Ketu)
                lon = 30.0 - lon;
            return lon;
        }
        protected Body.Name FindAtmaKaraka()
        {
            Body.Name[] karakaBodies =
            {
                Body.Name.Sun, Body.Name.Moon, Body.Name.Mars, Body.Name.Mercury,
                Body.Name.Jupiter, Body.Name.Venus, Body.Name.Saturn, Body.Name.Rahu
            };
            double lon = 0.0;
            Body.Name ret = Body.Name.Sun;
            foreach (Body.Name bn in karakaBodies)
            {
                double offset = KarakaLongitude(bn);
                if (offset > lon) lon = offset;
                ret = bn;
            }
            return ret;
        }
        public ArrayList FindGrahasInHouse(ZodiacHouseName zh)
        {
            ArrayList ret = new ArrayList();
            foreach (DivisionPosition dp in standardDivisionPositions)
            {
                if (dp.Type == BodyType.Name.Graha &&
                    dp.ZodiacHouse.Value == zh)
                {
                    ret.Add(dp.Name);
                }
            }
            return ret;
        }
    }

}
