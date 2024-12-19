namespace org.transliteral.panchang
{
    /// <summary>
    /// Specifies a DivisionPosition, i.e. a position in a varga chart like Rasi
    /// or Navamsa. It has no notion of "longitude".
    /// </summary>
    public class DivisionPosition
    {
        public BodyName Name;
        public BodyType.Name Type;
        public ZodiacHouse ZodiacHouse;
        public double CuspLower;
        public double CuspHigher;
        public int Part;
        public int RulerIndex;
        public string otherString;

       
        public DivisionPosition(BodyName _name, BodyType.Name _type, ZodiacHouse _zodiac_house,
            double _cusp_lower, double _cusp_higher, int _part)
        {
            Name = _name;
            Type = _type;
            ZodiacHouse = _zodiac_house;
            CuspLower = _cusp_lower;
            CuspHigher = _cusp_higher;
            Part = _part;
            RulerIndex = 0;
        }
        public bool IsInMoolaTrikona()
        {
            switch (this.Name)
            {
                case BodyName.Sun: if (ZodiacHouse.Value == ZodiacHouseName.Leo) return true; break;
                case BodyName.Moon: if (ZodiacHouse.Value == ZodiacHouseName.Tau) return true; break;
                case BodyName.Mars: if (ZodiacHouse.Value == ZodiacHouseName.Ari) return true; break;
                case BodyName.Mercury: if (ZodiacHouse.Value == ZodiacHouseName.Vir) return true; break;
                case BodyName.Jupiter: if (ZodiacHouse.Value == ZodiacHouseName.Sag) return true; break;
                case BodyName.Venus: if (ZodiacHouse.Value == ZodiacHouseName.Lib) return true; break;
                case BodyName.Saturn: if (ZodiacHouse.Value == ZodiacHouseName.Aqu) return true; break;
                case BodyName.Rahu: if (ZodiacHouse.Value == ZodiacHouseName.Vir) return true; break;
                case BodyName.Ketu: if (ZodiacHouse.Value == ZodiacHouseName.Pis) return true; break;
            }
            return false;
        }
        public bool IsInOwnHouse()
        {
            ZodiacHouseName zh = ZodiacHouse.Value;
            switch (this.Name)
            {
                case BodyName.Sun: if (zh == ZodiacHouseName.Leo) return true; break;
                case BodyName.Moon: if (zh == ZodiacHouseName.Tau) return true; break;
                case BodyName.Mars: if (zh == ZodiacHouseName.Ari || zh == ZodiacHouseName.Sco) return true; break;
                case BodyName.Mercury: if (zh == ZodiacHouseName.Gem || zh == ZodiacHouseName.Vir) return true; break;
                case BodyName.Jupiter: if (zh == ZodiacHouseName.Sag || zh == ZodiacHouseName.Pis) return true; break;
                case BodyName.Venus: if (zh == ZodiacHouseName.Tau || zh == ZodiacHouseName.Lib) return true; break;
                case BodyName.Saturn: if (zh == ZodiacHouseName.Cap || zh == ZodiacHouseName.Aqu) return true; break;
                case BodyName.Rahu: if (zh == ZodiacHouseName.Aqu) return true; break;
                case BodyName.Ketu: if (zh == ZodiacHouseName.Sco) return true; break;
            }
            return false;
        }
        public bool IsExaltedPhalita()
        {
            switch (this.Name)
            {
                case BodyName.Sun: if (ZodiacHouse.Value == ZodiacHouseName.Ari) return true; break;
                case BodyName.Moon: if (ZodiacHouse.Value == ZodiacHouseName.Tau) return true; break;
                case BodyName.Mars: if (ZodiacHouse.Value == ZodiacHouseName.Cap) return true; break;
                case BodyName.Mercury: if (ZodiacHouse.Value == ZodiacHouseName.Vir) return true; break;
                case BodyName.Jupiter: if (ZodiacHouse.Value == ZodiacHouseName.Can) return true; break;
                case BodyName.Venus: if (ZodiacHouse.Value == ZodiacHouseName.Pis) return true; break;
                case BodyName.Saturn: if (ZodiacHouse.Value == ZodiacHouseName.Lib) return true; break;
                case BodyName.Rahu: if (ZodiacHouse.Value == ZodiacHouseName.Gem) return true; break;
                case BodyName.Ketu: if (ZodiacHouse.Value == ZodiacHouseName.Sag) return true; break;
            }
            return false;
        }
        public bool IsDebilitatedPhalita()
        {
            switch (this.Name)
            {
                case BodyName.Sun: if (ZodiacHouse.Value == ZodiacHouseName.Lib) return true; break;
                case BodyName.Moon: if (ZodiacHouse.Value == ZodiacHouseName.Sco) return true; break;
                case BodyName.Mars: if (ZodiacHouse.Value == ZodiacHouseName.Can) return true; break;
                case BodyName.Mercury: if (ZodiacHouse.Value == ZodiacHouseName.Pis) return true; break;
                case BodyName.Jupiter: if (ZodiacHouse.Value == ZodiacHouseName.Cap) return true; break;
                case BodyName.Venus: if (ZodiacHouse.Value == ZodiacHouseName.Vir) return true; break;
                case BodyName.Saturn: if (ZodiacHouse.Value == ZodiacHouseName.Ari) return true; break;
                case BodyName.Rahu: if (ZodiacHouse.Value == ZodiacHouseName.Sag) return true; break;
                case BodyName.Ketu: if (ZodiacHouse.Value == ZodiacHouseName.Gem) return true; break;
            }
            return false;
        }
        public bool GrahaDristi(ZodiacHouse h)
        {
            int num = ZodiacHouse.NumHousesBetween(h);
            if (num == 7) return true;

            if (Name == BodyName.Jupiter && (num == 5 || num == 9)) return true;
            if (Name == BodyName.Rahu && (num == 5 || num == 9 || num == 2)) return true;
            if (Name == BodyName.Mars && (num == 4 || num == 8)) return true;
            if (Name == BodyName.Saturn && (num == 3 || num == 10)) return true;

            return false;
        }
    }

}
