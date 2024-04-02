using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Specifies a DivisionPosition, i.e. a position in a varga chart like Rasi
    /// or Navamsa. It has no notion of "longitude".
    /// </summary>
    public class DivisionPosition
    {
        public Body.Name name;
        public BodyType.Name type;
        public ZodiacHouse zodiac_house;
        public double cusp_lower;
        public double cusp_higher;
        public int part;
        public int ruler_index;
        public string otherString;


        public DivisionPosition(Body.Name _name, BodyType.Name _type, ZodiacHouse _zodiac_house,
            double _cusp_lower, double _cusp_higher, int _part)
        {
            name = _name;
            type = _type;
            zodiac_house = _zodiac_house;
            cusp_lower = _cusp_lower;
            cusp_higher = _cusp_higher;
            part = _part;
            ruler_index = 0;
        }
        public bool isInMoolaTrikona()
        {
            switch (this.name)
            {
                case Body.Name.Sun: if (zodiac_house.value == ZodiacHouseName.Leo) return true; break;
                case Body.Name.Moon: if (zodiac_house.value == ZodiacHouseName.Tau) return true; break;
                case Body.Name.Mars: if (zodiac_house.value == ZodiacHouseName.Ari) return true; break;
                case Body.Name.Mercury: if (zodiac_house.value == ZodiacHouseName.Vir) return true; break;
                case Body.Name.Jupiter: if (zodiac_house.value == ZodiacHouseName.Sag) return true; break;
                case Body.Name.Venus: if (zodiac_house.value == ZodiacHouseName.Lib) return true; break;
                case Body.Name.Saturn: if (zodiac_house.value == ZodiacHouseName.Aqu) return true; break;
                case Body.Name.Rahu: if (zodiac_house.value == ZodiacHouseName.Vir) return true; break;
                case Body.Name.Ketu: if (zodiac_house.value == ZodiacHouseName.Pis) return true; break;
            }
            return false;
        }
        public bool isInOwnHouse()
        {
            ZodiacHouseName zh = zodiac_house.value;
            switch (this.name)
            {
                case Body.Name.Sun: if (zh == ZodiacHouseName.Leo) return true; break;
                case Body.Name.Moon: if (zh == ZodiacHouseName.Tau) return true; break;
                case Body.Name.Mars: if (zh == ZodiacHouseName.Ari || zh == ZodiacHouseName.Sco) return true; break;
                case Body.Name.Mercury: if (zh == ZodiacHouseName.Gem || zh == ZodiacHouseName.Vir) return true; break;
                case Body.Name.Jupiter: if (zh == ZodiacHouseName.Sag || zh == ZodiacHouseName.Pis) return true; break;
                case Body.Name.Venus: if (zh == ZodiacHouseName.Tau || zh == ZodiacHouseName.Lib) return true; break;
                case Body.Name.Saturn: if (zh == ZodiacHouseName.Cap || zh == ZodiacHouseName.Aqu) return true; break;
                case Body.Name.Rahu: if (zh == ZodiacHouseName.Aqu) return true; break;
                case Body.Name.Ketu: if (zh == ZodiacHouseName.Sco) return true; break;
            }
            return false;
        }
        public bool isExaltedPhalita()
        {
            switch (this.name)
            {
                case Body.Name.Sun: if (zodiac_house.value == ZodiacHouseName.Ari) return true; break;
                case Body.Name.Moon: if (zodiac_house.value == ZodiacHouseName.Tau) return true; break;
                case Body.Name.Mars: if (zodiac_house.value == ZodiacHouseName.Cap) return true; break;
                case Body.Name.Mercury: if (zodiac_house.value == ZodiacHouseName.Vir) return true; break;
                case Body.Name.Jupiter: if (zodiac_house.value == ZodiacHouseName.Can) return true; break;
                case Body.Name.Venus: if (zodiac_house.value == ZodiacHouseName.Pis) return true; break;
                case Body.Name.Saturn: if (zodiac_house.value == ZodiacHouseName.Lib) return true; break;
                case Body.Name.Rahu: if (zodiac_house.value == ZodiacHouseName.Gem) return true; break;
                case Body.Name.Ketu: if (zodiac_house.value == ZodiacHouseName.Sag) return true; break;
            }
            return false;
        }
        public bool isDebilitatedPhalita()
        {
            switch (this.name)
            {
                case Body.Name.Sun: if (zodiac_house.value == ZodiacHouseName.Lib) return true; break;
                case Body.Name.Moon: if (zodiac_house.value == ZodiacHouseName.Sco) return true; break;
                case Body.Name.Mars: if (zodiac_house.value == ZodiacHouseName.Can) return true; break;
                case Body.Name.Mercury: if (zodiac_house.value == ZodiacHouseName.Pis) return true; break;
                case Body.Name.Jupiter: if (zodiac_house.value == ZodiacHouseName.Cap) return true; break;
                case Body.Name.Venus: if (zodiac_house.value == ZodiacHouseName.Vir) return true; break;
                case Body.Name.Saturn: if (zodiac_house.value == ZodiacHouseName.Ari) return true; break;
                case Body.Name.Rahu: if (zodiac_house.value == ZodiacHouseName.Sag) return true; break;
                case Body.Name.Ketu: if (zodiac_house.value == ZodiacHouseName.Gem) return true; break;
            }
            return false;
        }
        public bool GrahaDristi(ZodiacHouse h)
        {
            int num = zodiac_house.numHousesBetween(h);
            if (num == 7) return true;

            if (name == Body.Name.Jupiter && (num == 5 || num == 9)) return true;
            if (name == Body.Name.Rahu && (num == 5 || num == 9 || num == 2)) return true;
            if (name == Body.Name.Mars && (num == 4 || num == 8)) return true;
            if (name == Body.Name.Saturn && (num == 3 || num == 10)) return true;

            return false;
        }
    }

}
