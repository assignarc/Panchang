using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class Nakshatra
    {
        
        public override string ToString()
        {
            switch (m_nak)
            {
                case NakshatraName.Aswini: return "Aswini";
                case NakshatraName.Bharani: return "Bharani";
                case NakshatraName.Krittika: return "Krittika";
                case NakshatraName.Rohini: return "Rohini";
                case NakshatraName.Mrigarirsa: return "Mrigasira";
                case NakshatraName.Aridra: return "Ardra";
                case NakshatraName.Punarvasu: return "Punarvasu";
                case NakshatraName.Pushya: return "Pushyami";
                case NakshatraName.Aslesha: return "Aslesha";
                case NakshatraName.Makha: return "Makha";
                case NakshatraName.PoorvaPhalguni: return "P.Phalguni";
                case NakshatraName.UttaraPhalguni: return "U.Phalguni";
                case NakshatraName.Hasta: return "Hasta";
                case NakshatraName.Chittra: return "Chitta";
                case NakshatraName.Swati: return "Swati";
                case NakshatraName.Vishaka: return "Visakha";
                case NakshatraName.Anuradha: return "Anuradha";
                case NakshatraName.Jyestha: return "Jyeshtha";
                case NakshatraName.Moola: return "Moola";
                case NakshatraName.PoorvaShada: return "P.Ashada";
                case NakshatraName.UttaraShada: return "U.Ashada";
                case NakshatraName.Sravana: return "Sravana";
                case NakshatraName.Dhanishta: return "Dhanishta";
                case NakshatraName.Satabisha: return "Shatabisha";
                case NakshatraName.PoorvaBhadra: return "P.Bhadra";
                case NakshatraName.UttaraBhadra: return "U.Bhadra";
                case NakshatraName.Revati: return "Revati";
                default: return "---";
            }
        }

        public string toShortString()
        {
            switch (m_nak)
            {
                case NakshatraName.Aswini: return "Asw";
                case NakshatraName.Bharani: return "Bha";
                case NakshatraName.Krittika: return "Kri";
                case NakshatraName.Rohini: return "Roh";
                case NakshatraName.Mrigarirsa: return "Mri";
                case NakshatraName.Aridra: return "Ari";
                case NakshatraName.Punarvasu: return "Pun";
                case NakshatraName.Pushya: return "Pus";
                case NakshatraName.Aslesha: return "Asl";
                case NakshatraName.Makha: return "Mak";
                case NakshatraName.PoorvaPhalguni: return "P.Ph";
                case NakshatraName.UttaraPhalguni: return "U.Ph";
                case NakshatraName.Hasta: return "Has";
                case NakshatraName.Chittra: return "Chi";
                case NakshatraName.Swati: return "Swa";
                case NakshatraName.Vishaka: return "Vis";
                case NakshatraName.Anuradha: return "Anu";
                case NakshatraName.Jyestha: return "Jye";
                case NakshatraName.Moola: return "Moo";
                case NakshatraName.PoorvaShada: return "P.Ash";
                case NakshatraName.UttaraShada: return "U.Ash";
                case NakshatraName.Sravana: return "Sra";
                case NakshatraName.Dhanishta: return "Dha";
                case NakshatraName.Satabisha: return "Sat";
                case NakshatraName.PoorvaBhadra: return "P.Bh";
                case NakshatraName.UttaraBhadra: return "U.Bh";
                case NakshatraName.Revati: return "Rev";
                default: return "---";
            }

        }
        private NakshatraName m_nak;
        public NakshatraName value
        {
            get { return m_nak; }
            set { m_nak = value; }
        }
        public Nakshatra(NakshatraName nak)
        {
            m_nak = (NakshatraName)Basics.normalize_inc(1, 27, (int)nak);
        }
        public int normalize()
        {
            return Basics.normalize_inc(1, 27, (int)this.value);
        }
        public Nakshatra add(int i)
        {
            int snum = Basics.normalize_inc(1, 27, (int)this.value + i - 1);
            return new Nakshatra((NakshatraName)snum);
        }
        public Nakshatra addReverse(int i)
        {
            int snum = Basics.normalize_inc(1, 27, (int)this.value - i + 1);
            return new Nakshatra((NakshatraName)snum);
        }
    }

}
