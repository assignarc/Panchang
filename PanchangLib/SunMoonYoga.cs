namespace org.transliteral.panchang
{

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
            return Basics.Normalize_inc(1, 27, (int)this.Value);
        }
        public SunMoonYoga Add(int i)
        {
            int snum = Basics.Normalize_inc(1, 27, (int)this.Value + i - 1);
            return new SunMoonYoga((SunMoonYogaName)snum);
        }
        public SunMoonYoga AddReverse(int i)
        {
            int snum = Basics.Normalize_inc(1, 27, (int)this.Value - i + 1);
            return new SunMoonYoga((SunMoonYogaName)snum);
        }
        public Body.Name GetLord()
        {
            switch ((int)this.Value % 9)
            {
                case 1: return Body.Name.Saturn;
                case 2: return Body.Name.Mercury;
                case 3: return Body.Name.Ketu;
                case 4: return Body.Name.Venus;
                case 5: return Body.Name.Sun;
                case 6: return Body.Name.Moon;
                case 7: return Body.Name.Mars;
                case 8: return Body.Name.Rahu;
                default: return Body.Name.Jupiter;
            }
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }

    }


}
