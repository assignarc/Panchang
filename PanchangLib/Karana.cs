using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class Karana
    {
        private KaranaName mValue;
        public Karana(KaranaName _mValue)
        {
            mValue = (KaranaName)Basics.normalize_inc(1, 60, (int)_mValue);
        }
        public KaranaName value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        public Karana add(int i)
        {
            int tnum = Basics.normalize_inc(1, 60, (int)this.value + i - 1);
            return new Karana((KaranaName)tnum);
        }
        public Karana addReverse(int i)
        {
            int tnum = Basics.normalize_inc(1, 60, (int)this.value - i + 1);
            return new Karana((KaranaName)tnum);
        }
        public override string ToString()
        {
            return this.value.ToString();
        }
        public Body.Name getLord()
        {
            switch (this.value)
            {
                case KaranaName.Kimstughna: return Body.Name.Moon;
                case KaranaName.Sakuna: return Body.Name.Mars;
                case KaranaName.Chatushpada: return Body.Name.Sun;
                case KaranaName.Naga: return Body.Name.Venus;
                default:
                    {
                        int vn = Basics.normalize_inc(1, 7, (int)this.value - 1);
                        switch (vn)
                        {
                            case 1: return Body.Name.Sun;
                            case 2: return Body.Name.Moon;
                            case 3: return Body.Name.Mars;
                            case 4: return Body.Name.Mercury;
                            case 5: return Body.Name.Jupiter;
                            case 6: return Body.Name.Venus;
                            default: return Body.Name.Saturn;
                        }
                    }
            }
        }

    }

}
