using System;
using System.Diagnostics;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Represents a Tithi, a lunar day in the Hindu calendar, and provides methods to manipulate and retrieve
    /// information about it.
    /// </summary>
    /// <remarks>A Tithi is determined by the angular relationship between the Sun and the Moon. This class
    /// provides functionality to retrieve the Tithi's name, its associated lord, and its classification into Nanda
    /// types. It also supports arithmetic operations to add or subtract days, and normalization to ensure valid Tithi
    /// values.</remarks>
    public class Tithi
    {
        public string ToUnqualifiedString()
        {
            switch (mValue)
            {
                case TithiName.KrishnaPratipada:
                case TithiName.ShuklaPratipada: return "Prathama";
                case TithiName.KrishnaDvitiya:
                case TithiName.ShuklaDvitiya: return "Dvitiya";
                case TithiName.KrishnaTritiya:
                case TithiName.ShuklaTritiya: return "Tritiya";
                case TithiName.KrishnaChaturti:
                case TithiName.ShuklaChaturti: return "Chaturthi";
                case TithiName.KrishnaPanchami:
                case TithiName.ShuklaPanchami: return "Panchami";
                case TithiName.KrishnaShashti:
                case TithiName.ShuklaShashti: return "Shashti";
                case TithiName.KrishnaSaptami:
                case TithiName.ShuklaSaptami: return "Saptami";
                case TithiName.KrishnaAshtami:
                case TithiName.ShuklaAshtami: return "Ashtami";
                case TithiName.KrishnaNavami:
                case TithiName.ShuklaNavami: return "Navami";
                case TithiName.KrishnaDasami:
                case TithiName.ShuklaDasami: return "Dashami";
                case TithiName.KrishnaEkadasi:
                case TithiName.ShuklaEkadasi: return "Ekadashi";
                case TithiName.KrishnaDvadasi:
                case TithiName.ShuklaDvadasi: return "Dwadashi";
                case TithiName.KrishnaTrayodasi:
                case TithiName.ShuklaTrayodasi: return "Trayodashi";
                case TithiName.KrishnaChaturdasi:
                case TithiName.ShuklaChaturdasi: return "Chaturdashi";
                case TithiName.Paurnami: return "Poornima";
                case TithiName.Amavasya: return "Amavasya";
            }
            return "";
        }
        public override string ToString()
        {
            return EnumDescConverter.GetEnumDescription(mValue);
        }


        private TithiName mValue;
        public Tithi(TithiName _mValue)
        {
            mValue = (TithiName)Basics.NormalizeInclusive(1, 30, (int)_mValue);
        }
        public TithiName Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        public Tithi Add(int i)
        {
            int tnum = Basics.NormalizeInclusive(1, 30, (int)this.Value + i - 1);
            return new Tithi((TithiName)tnum);
        }
        public Tithi AddReverse(int i)
        {
            int tnum = Basics.NormalizeInclusive(1, 30, (int)this.Value - i + 1);
            return new Tithi((TithiName)tnum);
        }
        public BodyName GetLord()
        {
            // 1 based index starting with prathama
            int i_tithi = (int) Value;

            Logger.Info(String.Format("Looking for lord of tithi {0}", i_tithi));
            // check for new moon and full moon 
            if (i_tithi == 30) return BodyName.Rahu;
            if (i_tithi == 15) return BodyName.Saturn;

            // coalesce pakshas
            if (i_tithi >= 16) i_tithi -= 15;
            switch (i_tithi)
            {
                case 1: case 9: return BodyName.Sun;
                case 2: case 10: return BodyName.Moon;
                case 3: case 11: return BodyName.Mars;
                case 4: case 12: return BodyName.Mercury;
                case 5: case 13: return BodyName.Jupiter;
                case 6: case 14: return BodyName.Venus;
                case 7: return BodyName.Saturn;
                case 8: return BodyName.Rahu;
            }
            Debug.Assert(false, "Tithi::getLord");
            return BodyName.Sun;
        }

        public NandaType ToNandaType()
        {
            // 1 based index starting with prathama
            int i_tithi = (int)Value;

            // check for new moon and full moon 

            if (i_tithi == 30 || i_tithi == 15) return NandaType.Purna;

            // coalesce pakshas
            if (i_tithi >= 16) i_tithi -= 15;
            switch (i_tithi)
            {
                case 1: case 6: case 11: return NandaType.Nanda;
                case 2: case 7: case 12: return NandaType.Bhadra;
                case 3: case 8: case 13: return NandaType.Jaya;
                case 4: case 9: case 14: return NandaType.Rikta;
                case 5: case 10: return NandaType.Purna;
            }
            Debug.Assert(false, "Tithi::toNandaType");
            return NandaType.Nanda;
        }
    }

}
