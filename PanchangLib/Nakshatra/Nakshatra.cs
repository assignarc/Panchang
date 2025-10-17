namespace org.transliteral.panchang
{

    public class Nakshatra
    {
        
        public override string ToString()
        {
            switch (m_nak)
            {
                case NakshatraName.Ashwini: return "Aswini";
                case NakshatraName.Bharani: return "Bharani";
                case NakshatraName.Krittika: return "Krittika";
                case NakshatraName.Rohini: return "Rohini";
                case NakshatraName.Mrigashira: return "Mrigasira";
                case NakshatraName.Ardra: return "Ardra";
                case NakshatraName.Punarvasu: return "Punarvasu";
                case NakshatraName.Pushya: return "Pushyami";
                case NakshatraName.Ashlesha: return "Aslesha";
                case NakshatraName.Magha: return "Makha";
                case NakshatraName.PoorvaPhalguni: return "P.Phalguni";
                case NakshatraName.UttaraPhalguni: return "U.Phalguni";
                case NakshatraName.Hasta: return "Hasta";
                case NakshatraName.Chitra: return "Chitta";
                case NakshatraName.Swati: return "Swati";
                case NakshatraName.Vishakha: return "Visakha";
                case NakshatraName.Anuradha: return "Anuradha";
                case NakshatraName.Jyestha: return "Jyeshtha";
                case NakshatraName.Moola: return "Moola";
                case NakshatraName.PoorvaShada: return "P.Ashada";
                case NakshatraName.UttaraShada: return "U.Ashada";
                case NakshatraName.Shravana: return "Sravana";
                case NakshatraName.Dhanishta: return "Dhanishta";
                case NakshatraName.Shatabhisha: return "Shatabisha";
                case NakshatraName.PoorvaBhadra: return "P.Bhadra";
                case NakshatraName.UttaraBhadra: return "U.Bhadra";
                case NakshatraName.Revati: return "Revati";
                default: return "---";
            }
        }

        public string ToShortString()
        {
            switch (m_nak)
            {
                case NakshatraName.Ashwini: return "Asw";
                case NakshatraName.Bharani: return "Bha";
                case NakshatraName.Krittika: return "Kri";
                case NakshatraName.Rohini: return "Roh";
                case NakshatraName.Mrigashira: return "Mri";
                case NakshatraName.Ardra: return "Ari";
                case NakshatraName.Punarvasu: return "Pun";
                case NakshatraName.Pushya: return "Pus";
                case NakshatraName.Ashlesha: return "Asl";
                case NakshatraName.Magha: return "Mak";
                case NakshatraName.PoorvaPhalguni: return "P.Ph";
                case NakshatraName.UttaraPhalguni: return "U.Ph";
                case NakshatraName.Hasta: return "Has";
                case NakshatraName.Chitra: return "Chi";
                case NakshatraName.Swati: return "Swa";
                case NakshatraName.Vishakha: return "Vis";
                case NakshatraName.Anuradha: return "Anu";
                case NakshatraName.Jyestha: return "Jye";
                case NakshatraName.Moola: return "Moo";
                case NakshatraName.PoorvaShada: return "P.Ash";
                case NakshatraName.UttaraShada: return "U.Ash";
                case NakshatraName.Shravana: return "Sra";
                case NakshatraName.Dhanishta: return "Dha";
                case NakshatraName.Shatabhisha: return "Sat";
                case NakshatraName.PoorvaBhadra: return "P.Bh";
                case NakshatraName.UttaraBhadra: return "U.Bh";
                case NakshatraName.Revati: return "Rev";
                default: return "---";
            }

        }
        private NakshatraName m_nak;
        public NakshatraName Value
        {
            get { return m_nak; }
            set { m_nak = value; }
        }
        public Nakshatra(NakshatraName nak)
        {
            m_nak = (NakshatraName)Basics.NormalizeInclusive(1, 27, (int)nak);
        }
        public int Normalize()
        {
            return Basics.NormalizeInclusive(1, 27, (int)this.Value);
        }
        public Nakshatra Add(int i)
        {
            int snum = Basics.NormalizeInclusive(1, 27, (int)this.Value + i - 1);
            return new Nakshatra((NakshatraName)snum);
        }
        public Nakshatra AddReverse(int i)
        {
            int snum = Basics.NormalizeInclusive(1, 27, (int)this.Value - i + 1);
            return new Nakshatra((NakshatraName)snum);
        }
    }

}
