namespace org.transliteral.panchang
{

    public class Karana
    {
        private KaranaName mValue;
        public Karana(KaranaName _mValue)
        {
            mValue = (KaranaName)Basics.NormalizeInclusive(1, 60, (int)_mValue);
        }
        public KaranaName Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        public Karana Add(int i)
        {
            int tnum = Basics.NormalizeInclusive(1, 60, (int)this.Value + i - 1);
            return new Karana((KaranaName)tnum);
        }
        public Karana AddReverse(int i)
        {
            int tnum = Basics.NormalizeInclusive(1, 60, (int)this.Value - i + 1);
            return new Karana((KaranaName)tnum);
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public BodyName GetLord()
        {
            switch (this.Value)
            {
                case KaranaName.Kimstughna: return BodyName.Moon;
                case KaranaName.Sakuna: return BodyName.Mars;
                case KaranaName.Chatushpada: return BodyName.Sun;
                case KaranaName.Naga: return BodyName.Venus;
                default:
                    {
                        int vn = Basics.NormalizeInclusive(1, 7, (int)this.Value - 1);
                        switch (vn)
                        {
                            case 1: return BodyName.Sun;
                            case 2: return BodyName.Moon;
                            case 3: return BodyName.Mars;
                            case 4: return BodyName.Mercury;
                            case 5: return BodyName.Jupiter;
                            case 6: return BodyName.Venus;
                            default: return BodyName.Saturn;
                        }
                    }
            }
        }

    }

}
