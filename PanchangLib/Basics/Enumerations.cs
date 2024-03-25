using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public enum ESavType { Normal, Rao }
    public enum EDisplayStyle { Chancha, NavaSav }
    public enum EKakshya { EKRegular, EKStandard }
    public enum Direction : int
    {
        NS = 1, EW = 2
    }

    public enum NakshatraGroupType
    {
        Savya, SavyaMirrored, Apasavya, ApasavyaMirrored
    }

    public enum ParamAyusType : int
    {
        Short, Middle, Long
    }

    public enum Tattwa : int
    { Bhoomi, Jala, Agni, Vayu, Akasha }

    public enum DateType : int
    {
        FixedYear, SolarYear, TithiYear, YogaYear,
        TithiPraveshYear, KaranaPraveshYear, YogaPraveshYear, NakshatraPraveshYear
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum StartBodyType : int
    {
        [Description("Lagna's sphuta")] Lagna,
        [Description("Moon's sphuta")] Moon,
        [Description("Jupiter's sphuta")] Jupiter,
        [Description("Utpanna - 5th tara from Moon's sphuta")] Utpanna,
        [Description("Kshema - 4th tara from Moon's sphuta")] Kshema,
        [Description("Adhana - 8th tara from Moon's sphuta")] Aadhaana,
        [Description("Mandi's sphuta")] Maandi,
        [Description("Gulika's sphuta")] Gulika
    }

    // Maintain numerical values for forward compatibility
    [TypeConverter(typeof(EnumDescConverter))]
    public enum ERasiStrength
    {
        [Description("Giving up: Arbitrarily choosing one")] First,
        [Description("Rasi has more grahas in it")] Conjunction,
        [Description("Rasi contains more exalted grahas")] Exaltation,
        [Description("Rasi has a graha with higher longitude offset")] Longitude,
        [Description("Rasi contains Atma Karaka")] AtmaKaraka,
        [Description("Rasi's lord is Atma Karaka")] LordIsAtmaKaraka,
        [Description("Rasi is stronger by nature")] RasisNature,
        [Description("Rasi has more rasi drishtis of lord, Mer, Jup")] AspectsRasi,
        [Description("Rasi has more graha drishtis of lord, Mer, Jup")] AspectsGraha,
        [Description("Rasi's lord is in a rasi of different oddity")] LordInDifferentOddity,
        [Description("Rasi's lord has a higher longitude offset")] LordsLongitude,
        [Description("Rasi has longer narayana dasa length")] NarayanaDasaLength,
        [Description("Rasi has a graha in moolatrikona")] MoolaTrikona,
        [Description("Rasi's lord is place there")] OwnHouse,
        [Description("Rasi has more grahas in kendras")] KendraConjunction,
        [Description("Rasi's dispositor is stronger by nature")] LordsNature,
        [Description("Rasi has a graha with longer karaka kendradi graha dasa length")] KarakaKendradiGrahaDasaLength,
        [Description("Rasi has a graha with longer vimsottari dasa length")] VimsottariDasaLength
    };

    // Maintain numerical values for forward compatibility
    [TypeConverter(typeof(EnumDescConverter))]
    public enum EGrahaStrength
    {
        [Description("Giving up: Arbitrarily choosing one")] First,
        [Description("Graha is conjunct more grahas")] Conjunction,
        [Description("Graha is exalted")] Exaltation,
        [Description("Graha has higher longitude offset")] Longitude,
        [Description("Graha is Atma Karaka")] AtmaKaraka,
        [Description("Graha is in a rasi with stronger nature")] RasisNature,
        [Description("Graha has more rasi drishti of dispositor, Jup, Mer")] AspectsRasi,
        [Description("Graha has more graha drishti of dispositor, Jup, Mer")] AspectsGraha,
        [Description("Graha has a larger narayana dasa length")] NarayanaDasaLength,
        [Description("Graha is in its moola trikona rasi")] MoolaTrikona,
        [Description("Graha is in own house")] OwnHouse,
        [Description("Graha is not in own house")] NotInOwnHouse,
        [Description("Graha's dispositor is in own house")] LordInOwnHouse,
        [Description("Graha has more grahas in kendras")] KendraConjunction,
        [Description("Graha's dispositor is in a rasi with stronger nature")] LordsNature,
        [Description("Graha's dispositor is in a rasi with different oddify")] LordInDifferentOddity,
        [Description("Graha has a larger Karaka Kendradi Graha Dasa length")] KarakaKendradiGrahaDasaLength,
        [Description("Graha has a larger Vimsottari Dasa length")] VimsottariDasaLength
    }

    public enum EFileType
    {
        JagannathaHora, MudgalaHora
    }

    public enum HoraType : int
    {
        Birth, Progression, TithiPravesh, Transit, Dasa
    }

    public enum AyanamsaType : int
    {
        Fagan = 0, Lahiri = 1, Raman = 3, Ushashashi = 4, Krishnamurti = 5
    }

    //HoroscopeOptions.cs
    [TypeConverter(typeof(EnumDescConverter))]
    public enum SunrisePositionType : int
    {
        [Description("Sun's center rises")]
        TrueDiscCenter,
        [Description("Sun's edge rises")]
        TrueDiscEdge,
        [Description("Sun's center appears to rise")]
        ApparentDiscCenter,
        [Description("Sun's edge apprears to rise")]
        ApparentDiscEdge,
        [Description("6am Local Mean Time")]
        Lmt
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EMaandiType
    {
        [Description("Rises at the beginning of Saturn's portion")] SaturnBegin,
        [Description("Rises in the middle of Saturn's portion")] SaturnMid,
        [Description("Rises at the end of Saturn's portion")] SaturnEnd,
        [Description("Rises at the beginning of the lordless portion")] LordlessBegin,
        [Description("Rises in the middle of the lordless portion")] LordlessMid,
        [Description("Rises at the end of the lordless portion")] LordlessEnd
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EUpagrahaType
    {
        [Description("Rises at the beginning of the grahas portion")] Begin,
        [Description("Rises in the middle of the grahas portion")] Mid,
        [Description("Rises at the end of the grahas portion")] End
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EHoraType
    {
        [Description("Day split into equal parts")] Sunriset,
        [Description("Daytime and Nighttime split into equal parts")] SunrisetEqual,
        [Description("Day (Local Mean Time) split into equal parts")] Lmt
    }

    public enum EGrahaPositionType : int
    {
        Apparent, True
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum ENodeType : int
    {
        [Description("Mean Positions of nodes")] Mean,
        [Description("True Positions of nodes")] True
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EBhavaType : int
    {
        [Description("Lagna is at the beginning of the bhava")] Start,
        [Description("Lagna is in the middle of the bhava")] Middle
    }



    public enum KaranaName : int
    {
        Kimstughna = 1,
        Bava1, Balava1, Kaulava1, Taitula1, Garija1, Vanija1, Vishti1,
        Bava2, Balava2, Kaulava2, Taitula2, Garija2, Vanija2, Vishti2,
        Bava3, Balava3, Kaulava3, Taitula3, Garija3, Vanija3, Vishti3,
        Bava4, Balava4, Kaulava4, Taitula4, Garija4, Vanija4, Vishti4,
        Bava5, Balava5, Kaulava5, Taitula5, Garija5, Vanija5, Vishti5,
        Bava6, Balava6, Kaulava6, Taitula6, Garija6, Vanija6, Vishti6,
        Bava7, Balava7, Kaulava7, Taitula7, Garija7, Vanija7, Vishti7,
        Bava8, Balava8, Kaulava8, Taitula8, Garija8, Vanija8, Vishti8,
        Sakuna, Chatushpada, Naga
    }

    // int values should not be changed. 
    // used in kalachakra dasa, and various other places.
    public enum NakshatraName : int
    {
        Aswini = 1, Bharani = 2, Krittika = 3, Rohini = 4, Mrigarirsa = 5,
        Aridra = 6, Punarvasu = 7, Pushya = 8, Aslesha = 9,
        Makha = 10, PoorvaPhalguni = 11, UttaraPhalguni = 12, Hasta = 13,
        Chittra = 14, Swati = 15, Vishaka = 16, Anuradha = 17, Jyestha = 18,
        Moola = 19, PoorvaShada = 20, UttaraShada = 21, Sravana = 22, Dhanishta = 23,
        Satabisha = 24, PoorvaBhadra = 25, UttaraBhadra = 26, Revati = 27
    }


    public enum Nakshatra28Name : int
    {
        Aswini = 1, Bharani = 2, Krittika = 3, Rohini = 4, Mrigarirsa = 5,
        Aridra = 6, Punarvasu = 7, Pushya = 8, Aslesha = 9,
        Makha = 10, PoorvaPhalguni = 11, UttaraPhalguni = 12, Hasta = 13,
        Chittra = 14, Swati = 15, Vishaka = 16, Anuradha = 17, Jyestha = 18,
        Moola = 19, PoorvaShada = 20, UttaraShada = 21, Abhijit = 22, Sravana = 23, Dhanishta = 24,
        Satabisha = 25, PoorvaBhadra = 26, UttaraBhadra = 27, Revati = 28
    }

    public enum SunMoonYogaName : int
    {
        Vishkambha = 1, Preeti, Aayushmaan, Saubhaagya, Sobhana, Atiganda, Sukarman, Dhriti, Shoola, Ganda,
        Vriddhi, Dhruva, Vyaaghaata, Harshana, Vajra, Siddhi, Vyatipaata, Variyan, Parigha, Shiva,
        Siddha, Saadhya, Subha, Sukla, Brahma, Indra, Vaidhriti
    }

    //Tithi.cs
    [TypeConverter(typeof(EnumDescConverter))]
    public enum TithiName : int
    {
        [Description("Shukla Pratipada")] ShuklaPratipada = 1,
        [Description("Shukla Dvitiya")] ShuklaDvitiya,
        [Description("Shukla Tritiya")] ShuklaTritiya,
        [Description("Shukla Chaturti")] ShuklaChaturti,
        [Description("Shukla Panchami")] ShuklaPanchami,
        [Description("Shukla Shashti")] ShuklaShashti,
        [Description("Shukla Saptami")] ShuklaSaptami,
        [Description("Shukla Ashtami")] ShuklaAshtami,
        [Description("Shukla Navami")] ShuklaNavami,
        [Description("Shukla Dashami")] ShuklaDasami,
        [Description("Shukla Ekadasi")] ShuklaEkadasi,
        [Description("Shukla Dwadasi")] ShuklaDvadasi,
        [Description("Shukla Trayodasi")] ShuklaTrayodasi,
        [Description("Shukla Chaturdasi")] ShuklaChaturdasi,
        [Description("Paurnami")] Paurnami,
        [Description("Krishna Pratipada")] KrishnaPratipada,
        [Description("Krishna Dvitiya")] KrishnaDvitiya,
        [Description("Krishna Tritiya")] KrishnaTritiya,
        [Description("Krishna Chaturti")] KrishnaChaturti,
        [Description("Krishna Panchami")] KrishnaPanchami,
        [Description("Krishna Shashti")] KrishnaShashti,
        [Description("Krishna Saptami")] KrishnaSaptami,
        [Description("Krishna Ashtami")] KrishnaAshtami,
        [Description("Krishna Navami")] KrishnaNavami,
        [Description("Krishna Dashami")] KrishnaDasami,
        [Description("Krishna Ekadasi")] KrishnaEkadasi,
        [Description("Krishna Dwadasi")] KrishnaDvadasi,
        [Description("Krishna Trayodasi")] KrishnaTrayodasi,
        [Description("Krishna Chaturdasi")] KrishnaChaturdasi,
        [Description("Amavasya")] Amavasya
    }

    public enum NandaType : int
    { Nanda, Bhadra, Jaya, Rikta, Purna }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EChartStyle
    {
        [Description("South Indian Square (Jupiter)")] SouthIndian,
        [Description("East Indian Square (Sun)")] EastIndian
    }
    public enum XmlYohaNodePos
    {
        SourceRef, SourceText, SourceItxText, MhoraRule, Result, Other
    }

    //ZodiacHouse.cs
    public enum ZodiacHouseName : int
    {
        Ari = 1, Tau = 2, Gem = 3, Can = 4, Leo = 5, Vir = 6,
        Lib = 7, Sco = 8, Sag = 9, Cap = 10, Aqu = 11, Pis = 12
    }
    public enum RiseType : int
    {
        RisesWithHead, RisesWithFoot, RisesWithBoth
    }

    public enum Muhurta : int
    {
        Rudra = 1, Ahi, Mitra, Pitri, Vasu, Ambu, Visvadeva, Abhijit, Vidhata, Puruhuta,
        Indragni, Nirriti, Varuna, Aryaman, Bhaga, Girisa, Ajapada, Ahirbudhnya, Pushan, Asvi,
        Yama, Agni, Vidhaatri, Chanda, Aditi, Jiiva, Vishnu, Arka, Tvashtri, Maruta
    }

    /// <summary>
    /// Enumeration of the various division types. Where a varga has multiple
    /// definitions, each of these should be specified separately below
    /// </summary>
    [TypeConverter(typeof(EnumDescConverter))]
    public enum DivisionType : int
    {
        [Description("D-1: Rasi")] Rasi = 0,
        [Description("D-9: Navamsa")] Navamsa,
        [Description("D-2: Hora (Parashara)")] HoraParasara,
        [Description("D-2: Hora (Jagannatha)")] HoraJagannath,
        [Description("D-2: Hora (Parivritti)")] HoraParivrittiDwaya,
        [Description("D-2: Hora (Kashinatha)")] HoraKashinath,
        [Description("D-3: Dreshkana (Parashara)")] DrekkanaParasara,
        [Description("D-3: Dreshkana (Jagannatha)")] DrekkanaJagannath,
        [Description("D-3: Dreshkana (Somnatha)")] DrekkanaSomnath,
        [Description("D-3: Dreshkana (Parivritti)")] DrekkanaParivrittitraya,
        [Description("D-4: Chaturthamsa")] Chaturthamsa,
        [Description("D-5: Panchamsa")] Panchamsa,
        [Description("D-6: Shashtamsa")] Shashthamsa,
        [Description("D-7: Saptamsa")] Saptamsa,
        [Description("D-8: Ashtamsa")] Ashtamsa,
        [Description("D-8: Ashtamsa (Raman)")] AshtamsaRaman,
        [Description("D-10: Dasamsa")] Dasamsa,
        [Description("D-11: Rudramsa (Rath)")] Rudramsa,
        [Description("D-11: Rudramsa (Raman)")] RudramsaRaman,
        [Description("D-12: Dwadasamsa")] Dwadasamsa,
        [Description("D-16: Shodasamsa")] Shodasamsa,
        [Description("D-20: Vimsamsa")] Vimsamsa,
        [Description("D-24: Chaturvimsamsa")] Chaturvimsamsa,
        [Description("D-27: Nakshatramsa")] Nakshatramsa,
        [Description("D-30: Trimsamsa (Parashara)")] Trimsamsa,
        [Description("D-30: Trimsamsa (Parivritti)")] TrimsamsaParivritti,
        [Description("D-30: Trimsamsa (Simple)")] TrimsamsaSimple,
        [Description("D-40: Khavedamsa")] Khavedamsa,
        [Description("D-45: Akshavedamsa")] Akshavedamsa,
        [Description("D-60: Shashtyamsa")] Shashtyamsa,
        [Description("D-108: Ashtottaramsa (Regular)")] Ashtottaramsa,
        [Description("D-150: Nadiamsa (Equal Division)")] Nadiamsa,
        [Description("D-150: Nadiamsa (Chandra Kala Nadi)")] NadiamsaCKN,
        [Description("D-9-12: Navamsa-Dwadasamsa (Composite)")] NavamsaDwadasamsa,
        [Description("D-12-12: Dwadasamsa-Dwadasamsa (Composite)")] DwadasamsaDwadasamsa,
        [Description("D-1: Bhava (9 Padas)")] BhavaPada,
        [Description("D-1: Bhava (Equal Length)")] BhavaEqual,
        [Description("D-1: Bhava (Alcabitus)")] BhavaAlcabitus,
        [Description("D-1: Bhava (Axial)")] BhavaAxial,
        [Description("D-1: Bhava (Campanus)")] BhavaCampanus,
        [Description("D-1: Bhava (Koch)")] BhavaKoch,
        [Description("D-1: Bhava (Placidus)")] BhavaPlacidus,
        [Description("D-1: Bhava (Regiomontanus)")] BhavaRegiomontanus,
        [Description("D-1: Bhava (Sripati)")] BhavaSripati,
        [Description("Regular: Parivritti")] GenericParivritti,
        [Description("Regular: Shashtamsa (d-6)")] GenericShashthamsa,
        [Description("Regular: Saptamsa (d-7)")] GenericSaptamsa,
        [Description("Regular: Dasamsa (d-10)")] GenericDasamsa,
        [Description("Regular: Dwadasamsa (d-12)")] GenericDwadasamsa,
        [Description("Regular: Chaturvimsamsa (d-24)")] GenericChaturvimsamsa,
        [Description("Trikona: Drekkana (d-3)")] GenericDrekkana,
        [Description("Trikona: Shodasamsa (d-16)")] GenericShodasamsa,
        [Description("Trikona: Vimsamsa (d-20)")] GenericVimsamsa,
        [Description("Kendra: Chaturthamsa (d-4)")] GenericChaturthamsa,
        [Description("Kendra: Nakshatramsa (d-27)")] GenericNakshatramsa
    }

}
