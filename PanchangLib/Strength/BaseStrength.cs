using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    abstract public class BaseStrength
    {
        protected ArrayList std_grahas;
        protected Division dtype;
        protected ArrayList std_div_pos;
        protected Horoscope h;
        protected bool bUseSimpleLords;

        protected Body.Name GetStrengthLord(ZodiacHouseName zh)
        {
            if (bUseSimpleLords)
                return Basics.SimpleLordOfZodiacHouse(zh);
            else
                return h.LordOfZodiacHouse(new ZodiacHouse(zh), dtype);
        }
        protected Body.Name GetStrengthLord(ZodiacHouse zh)
        {
            return GetStrengthLord(zh.value);
        }
        protected BaseStrength(Horoscope _h, Division _dtype, bool _bUseSimpleLords)
        {
            h = _h;
            dtype = _dtype;
            bUseSimpleLords = _bUseSimpleLords;
            std_div_pos = h.CalculateDivisionPositions(dtype);
        }
        protected int numGrahasInZodiacHouse(ZodiacHouseName zh)
        {
            int num = 0;
            foreach (DivisionPosition dp in std_div_pos)
            {
                if (dp.type != BodyType.Name.Graha)
                    continue;
                if (dp.zodiac_house.value == zh)
                    num = num + 1;
            }
            return num;
        }

        protected double karakaLongitude(Body.Name b)
        {
            double lon = h.getPosition(b).longitude.toZodiacHouseOffset();
            if (b == Body.Name.Rahu || b == Body.Name.Ketu)
                lon = 30.0 - lon;
            return lon;
        }
        protected Body.Name findAtmaKaraka()
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
                double offset = karakaLongitude(bn);
                if (offset > lon) lon = offset;
                ret = bn;
            }
            return ret;
        }

        public ArrayList findGrahasInHouse(ZodiacHouseName zh)
        {
            ArrayList ret = new ArrayList();
            foreach (DivisionPosition dp in std_div_pos)
            {
                if (dp.type == BodyType.Name.Graha &&
                    dp.zodiac_house.value == zh)
                {
                    ret.Add(dp.name);
                }
            }
            return ret;
        }
    }

}
