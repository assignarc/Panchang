namespace org.transliteral.panchang
{
    /// <summary>
    /// Represents a Sun-Moon Yoga, a concept in Vedic astrology, which is associated with a specific name and its
    /// corresponding properties and behaviors.
    /// </summary>
    /// <remarks>A Sun-Moon Yoga is identified by its <see cref="Value"/>, which corresponds to a specific
    /// enumeration value of <see cref="SunMoonYogaName"/>.  This class provides methods to normalize the yoga value,
    /// perform arithmetic operations on it, and retrieve its associated lord.</remarks>
    public class SunMoonYoga
    {
       
        private SunMoonYogaName mValue;
        public SunMoonYoga(SunMoonYogaName _mvalue)
        {
            mValue = _mvalue;
        }
        public SunMoonYogaName Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        public int Normalize()
        {
            return Basics.NormalizeInclusive(1, 27, (int)this.Value);
        }
        public SunMoonYoga Add(int i)
        {
            int snum = Basics.NormalizeInclusive(1, 27, (int)this.Value + i - 1);
            return new SunMoonYoga((SunMoonYogaName)snum);
        }
        public SunMoonYoga AddReverse(int i)
        {
            int snum = Basics.NormalizeInclusive(1, 27, (int)this.Value - i + 1);
            return new SunMoonYoga((SunMoonYogaName)snum);
        }
        public BodyName GetLord()
        {
            switch ((int)this.Value % 9)
            {
                case 1: return BodyName.Saturn;
                case 2: return BodyName.Mercury;
                case 3: return BodyName.Ketu;
                case 4: return BodyName.Venus;
                case 5: return BodyName.Sun;
                case 6: return BodyName.Moon;
                case 7: return BodyName.Mars;
                case 8: return BodyName.Rahu;
                default: return BodyName.Jupiter;
            }
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }

    }


}
