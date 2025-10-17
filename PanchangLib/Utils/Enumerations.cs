using System.ComponentModel;
using System.Reflection;
using System.Security.Policy;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Sarvashtakavarga is a Vedic astrology tool that creates a summary table of 
    /// a person's horoscope, combining the scores from the individual 
    /// Bhinnashtakavarga charts for seven planets and the ascendant. 
    /// It assigns "bindus" or points to each of the 12 houses to assess their 
    /// overall strength and potential for benefit, with a higher number of points 
    /// indicating a more favorable outcome for that house and its associated 
    /// areas of life. The total of all points across all 12 houses is 337, 
    /// meaning the average score for a house is about 28; therefore, houses 
    /// with more than 28 points are considered strong and likely to give auspicious results. 
    /// 
    /// K.N. Rao's approach to Sarvashtakavarga (SAV)
    /// K.N. Rao is a renowned Indian astrologer who emphasizes practical techniques.
    /// While the Ashtakavarga system existed long before him, his contributions 
    /// include simplifying its application and providing solved examples for 
    /// easier understanding. Rao's approach typically focuses on using the SAV to: 
    /// Assess overall house strength: A house with an SAV score above 28 
    /// is considered strong and brings more favorable results, while a 
    /// score below 28 is weaker and indicates challenges.
    /// Determine longevity: The SAV is used to predict a person's lifespan 
    /// by comparing the scores of the Lagna (1st house) and the 8th house.
    /// Interpret transits: The SAV score of a house indicates how beneficial a 
    /// transiting planet will be while in that house. A transiting planet in a 
    /// house with a high SAV score generally yields better results.
    /// </summary>
    public enum SarvashtakavargaType 
    { 
        Normal,
        Rao //As per B.V. Raman's book, KN Rao's method
    }
    /// <summary>
    /// The Sarva Chancha Chakra, also known as the Sarvashtakavarga chart, 
    /// is a comprehensive Vedic astrology chart that combines the information 
    /// from all individual Ashtakavarga charts into one. It is a graphical 
    /// representation used for a more in-depth analysis of a person's horoscope, 
    /// helping to determine the overall strength and weakness of each house by 
    /// calculating and displaying the combined points (or "bindus") for all seven 
    /// planets and the ascendant.  
    /// </summary>
    public enum DisplayStyle 
    { 
        SarvaChanchaChakra, 
        NavaSav 
    }
    public enum KakshyaType {
        Regular,
        Standard
    }
    public enum Direction : int
    {
        NorthSouth = 1,
        EastWest = 2
    }

    /// <summary>
    /// "Nakshatra group" refers to groups of constellations in Vedic 
    /// astrology that move in a direct or clockwise motion (Savya) through 
    /// the zodiac, contrasting with "Apasavya" groups which move in a 
    /// retrograde or counter-clockwise motion. The term is used in systems 
    /// like the Kāla Chakra Dasa (KCD) to categorize nakshatras and analyze 
    /// their influence and order. 
    /// Saavya = Direct motion (clockwise), Means "left" and refers to a direct 
    ///     or clockwise motion through the zodiac, such as from Aries to Pisces.
    /// Apasavya = Retrograde motion (counter-clockwise), Means "right" and refers Apasavya: 
    ///     Means "right" and refers to a retrograde or counter-clockwise motion, 
    ///     such as from Pisces to Aries. 
    /// 
    /// </summary>
    public enum NakshatraGroupType
    {
        Savya,
        SavyaMirrored,
        Apasavya,
        ApasavyaMirrored
    }
    /// <summary>
    /// "Param Ayush" refers to the Vedic concept of Paramāyuṣ 
    /// (also spelled Paramayu) which relates to long life, longevity, 
    /// or the maximum potential lifespan as viewed through the lens of 
    /// Vedic astrology and Ayurveda. In this context, "param" means 
    /// supreme or maximum, and "ayush" means life or longevity
    /// </summary>
    public enum ParamAyusType : int
    {
        Short, Middle, Long
    }

    /// <summary>
    /// In Vedic astrology, Tattva refers to the five elements—Earth (Prithvi), 
    /// Water (Jala), Fire (Agni), Air (Vayu), and Ether (Akasha)—which form 
    /// the basis of all material existence. These elements are used to 
    /// analyze the nature of a person, their health, and life events by examining the 
    /// predominance of a certain Tattva in a person's birth chart or at a 
    /// particular moment. The signs of the zodiac are associated with four of 
    /// these elements, while Ether is believed to pervade all of them. 
    /// 
    /// The five Tattvas
    /// Prithvi Tattva(Earth) : Represents the solid, material, and worldly aspects of life.
    /// Jala Tattva(Water): Symbolizes the liquid state, emotions, and adaptability.
    /// Agni Tattva(Fire): Denotes energy, transformation, and passion.
    /// Vayu Tattva(Air): Represents the gaseous state, movement, and intellect.
    /// Akasha Tattva(Ether/Space): Signifies the space or vacuum that supports all other 
    ///     elements and is considered to pervade everything.
    /// </summary>
    public enum TattwaType : int { 
        Prithvi, 
        Jala, 
        Agni, 
        Vayu, 
        Akasha 
    }

    public enum DateType : int
    {
        FixedYear,
        SolarYear,
        TithiYear,
        YogaYear,
        TithiPraveshYear,
        KaranaPraveshYear,
        YogaPraveshYear,
        NakshatraPraveshYear
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
    public enum RasiStrength
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
    public enum GrahaStrength
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

    public enum FileType
    {
        JagannathaHora,
        PanchangHora
    }

    public enum HoraType : int
    {
        Birth,
        Progression,
        TithiPravesh,
        Transit,
        Dasa
    }

    /// <summary>
    /// Vedic astrology uses various methods to calculate ayanamsa, 
    /// which is the angular difference between the tropical and 
    /// sidereal zodiacs. Common systems include the Lahiri, Raman, 
    /// and Krishnamurti Ayanamsas, each with a slightly different 
    /// calculation based on fixed stars or precessional rates. 
    /// The choice of method can lead to different planetary positions in a birth chart.
    /// 
    /// How ayanamsa is calculated?
    /// Ayanamsa: is the correction factor applied because of the Earth's "wobble," 
    /// or precession of the equinoxes. This means the start of the sidereal zodiac 
    /// (based on fixed stars) shifts relative to the tropical zodiac (based on the seasons) over time. 
    /// By subtracting the ayanamsa value, astrologers can ensure that the planet 
    /// positions in the sidereal zodiac are aligned with their current, actual positions 
    /// in the sky, making predictions more accurate.
    /// Some methods may use mathematical formulas based on astronomical texts 
    /// like the Surya Siddhanta, while others may use specific stars as reference points.
    /// The choice of ayanamsa can shift a planet from one house to another, 
    /// so using different methods can result in different chart interpretations.
    /// 
    /// Common ayanamsa calculation methods
    /// 
    /// Lahiri Ayanamsa: Named after astronomer Nirmal Chandra Lahiri, 
    /// this is the most widely used system in India, according to Wikipedia.
    /// Raman Ayanamsa: Developed by B.V.Raman, this method is very close to 
    ///     the traditional Surya Siddhanta calculation, notes YouTube. 
    /// Krishnamurti Ayanamsa: Used in the Krishnamurti Paddhati (KP) 
    ///     system of astrology, this method is often used in conjunction 
    ///     with specific sub-lord calculations for more detailed analysis.
    /// Pushya Paksha Ayanamsa: This is a method based on a specific 
    ///     star (Pushya nakshatra) as the anchor point and has references 
    ///     in ancient texts, according to Scribd and VedicAstrologer.org.
    /// Surya Siddhanta Ayanamsa: This is a traditional, linear formula 
    ///     for calculation, which other methods have been compared against.
    /// </summary>
    public enum AyanamsaType : int
    {
        Fagan = 0,
        Lahiri = 1,
        Raman = 3,
        Ushashashi = 4,
        Krishnamurti = 5
    }
    //Body

    [TypeConverter(typeof(EnumDescConverter))]
    public enum BodyName : int
    {
        // DO NOT CHANGE ORDER WITHOUT CHANGING NARAYANA DASA ETC
        // RELY ON EXPLICIT EQUAL CONVERSION FOR STRONGER CO_LORD ETC
        Sun = 0,
        Moon = 1,
        Mars = 2,
        Mercury = 3,
        Jupiter = 4,
        Venus = 5,
        Saturn = 6,
        Rahu = 7,
        Ketu = 8,
        Lagna = 9,

        // And now, we're no longer uptight about the ordering :-)
        [Description("Bhava Lagna")] BhavaLagna,
        [Description("Hora Lagna")] HoraLagna,
        [Description("Ghati Lagna")] GhatiLagna,
        [Description("Sree Lagna")] SreeLagna,
        Pranapada,
        [Description("Vighati Lagna")] VighatiLagna,
        Dhuma, Vyatipata, Parivesha, Indrachapa, Upaketu,
        Kala, Mrityu, ArthaPraharaka, YamaGhantaka, Gulika, Maandi,
        [Description("Chandra Ayur Lagna")] ChandraAyurLagna,
        MrityuPoint, Other,
        AL, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, UL,

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
    /// <summary>
    /// Maandi is a non-physical, calculated point in a horoscope, 
    /// considered an upagraha or "sub-planet" linked to Saturn's 
    /// energy. It represents challenges, negative karma, and obstacles 
    /// in a person's life. Its position is determined mathematically 
    /// by the day of the week and the specific time of birth, and 
    /// it is believed to have a strong inauspicious influence, especially 
    /// when placed in certain houses or in conjunction with other planets. 
    /// </summary>
    [TypeConverter(typeof(EnumDescConverter))]
    public enum MaandiType
    {
        [Description("Rises at the beginning of Saturn's portion")] SaturnBegin,
        [Description("Rises in the middle of Saturn's portion")] SaturnMid,
        [Description("Rises at the end of Saturn's portion")] SaturnEnd,
        [Description("Rises at the beginning of the lordless portion")] LordlessBegin,
        [Description("Rises in the middle of the lordless portion")] LordlessMid,
        [Description("Rises at the end of the lordless portion")] LordlessEnd
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum UpagrahaType
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

    public enum GrahaPositionType : int
    {
        Apparent,
        True
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum NodeType : int
    {
        [Description("Mean Positions of nodes")] Mean,
        [Description("True Positions of nodes")] True
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum BhavaType : int
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
        Ashwini = 1, 
        Bharani = 2, 
        Krittika = 3, 
        Rohini = 4, 
        Mrigashira = 5,
        Ardra = 6, 
        Punarvasu = 7, 
        Pushya = 8, 
        Ashlesha = 9,
        Magha = 10, 
        PoorvaPhalguni = 11, 
        UttaraPhalguni = 12, 
        Hasta = 13,
        Chitra = 14, 
        Swati = 15, 
        Vishakha = 16, 
        Anuradha = 17, 
        Jyestha = 18,
        Moola = 19, 
        PoorvaShada = 20, 
        UttaraShada = 21, 
        Shravana = 22, 
        Dhanishta = 23,
        Shatabhisha = 24,
        PoorvaBhadra = 25, 
        UttaraBhadra = 26, 
        Revati = 27
    }


    public enum Nakshatra28Name : int
    {
        Ashwini = 1, 
        Bharani = 2, 
        Krittika = 3, 
        Rohini = 4, 
        Mrigashira = 5,
        Ardra = 6, 
        Punarvasu = 7, 
        Pushya = 8, 
        Ashlesha = 9,
        Magha = 10, 
        PoorvaPhalguni = 11, 
        UttaraPhalguni = 12, 
        Hasta = 13,
        Chitra = 14, 
        Swati = 15, 
        Vishakha = 16, 
        Anuradha = 17, 
        Jyestha = 18,
        Moola = 19, 
        PoorvaShada = 20, 
        UttaraShada = 21, 
        Abhijit = 22, 
        Shravana = 23, 
        Dhanishta = 24,
        Shatabhisha = 25, 
        PoorvaBhadra = 26, 
        UttaraBhadra = 27, 
        Revati = 28
    }

    public enum SunMoonYogaName : int
    {
        Vishkambha = 1, 
        Preeti, 
        Aayushmaan, 
        Saubhaagya, 
        Shobhana, 
        Atiganda, 
        Sukarman, 
        Dhriti, 
        Shoola, 
        Ganda,
        Vriddhi, 
        Dhruva, 
        Vyaaghaata, 
        Harshana, 
        Vajra, 
        Siddhi, 
        Vyatipaata, 
        Variyan, 
        Parigha, 
        Shiva,
        Siddha, 
        Saadhya, 
        Shubha, 
        Shukla, 
        Brahma, 
        Indra, 
        Vaidhriti
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
    { 
        Nanda, 
        Bhadra, 
        Jaya, 
        Rikta, 
        Purna 
    }

    [TypeConverter(typeof(EnumDescConverter))]
    public enum EChartStyle
    {
        [Description("South Indian Square (Jupiter)")] SouthIndian,
        [Description("East Indian Square (Sun)")] EastIndian
    }
    public enum XmlYohaNodePos
    {
        SourceRef, 
        SourceText, 
        SourceItxText, 
        HoraRule, 
        Result, 
        Other
    }

    //BaseUserOptions

    public enum BaseUserOptionsViewType : int
    {
        DivisionalChart, 
        Ashtakavarga,
        KeyInfo, 
        BasicCalculations, 
        Balas,
        TransitSearch, 
        NavamsaCircle,
        VaraChakra, 
        KutaMatching,
        ChakraSarvatobhadra81,
        Panchanga,
        DasaAshtottari, 
        DasaVimsottari, 
        DasaMudda,
        DasaShodashottari, 
        DasaDwadashottari,
        DasaPanchottari, 
        DasaShatabdika,
        DasaTithiAshtottari,
        DasaYogaVimsottari,
        DasaKaranaChaturashitiSama,
        DasaTithiPraveshAshtottariCompressedFixed,
        DasaTithiPraveshAshtottariCompressedSolar,
        DasaTithiPraveshAshtottariCompressedTithi,
        DasaYogaPraveshVimsottariCompressedYoga,
        DasaChaturashitiSama, 
        DasaDwisaptatiSama,
        DasaShatTrimshaSama,
        DasaDrig, 
        DasaNarayana, 
        DasaNarayanaSama,
        DasaShoola, 
        DasaNiryaanaShoola,
        DasaSu, 
        DasaKalachakra,
        DasaTajaka,
        DasaTithiPravesh, 
        DasaYogaPravesh, 
        DasaNakshatraPravesh,
        DasaKaranaPravesh,
        DasaTattwa,
        NaisargikaRasiDasa,
        NaisargikaGrahaDasa,
        DasaSudarshanaChakra,
        DasaSudarshanaChakraCompressed,
        DasaYogini,
        DasaNavamsa,
        DasaMandooka,
        DasaChara, 
        DasaTrikona,
        DasaLagnaKendradiRasi, 
        DasaMoola,
        DasaKarakaKendradiGraha
    }
    //Basics
    // This matches the sweph definitions for easy conversion
    public enum Weekday : int
    {
        Monday = 0, 
        Tuesday = 1, 
        Wednesday = 2, 
        Thursday = 3, 
        Friday = 4, 
        Saturday = 5, 
        Sunday = 6
    }

    //BasicCalculationsControl
    public enum NakshatraLord
    {
        Vimsottari, 
        Ashtottari, 
        Yogini, 
        Shodashottari, 
        Dwadashottari, 
        Panchottari,
        Shatabdika, 
        ChaturashitiSama, 
        DwisaptatiSama, 
        ShatTrimshaSama
    };
    public enum ViewType
    {
        ViewBasicGrahas, 
        ViewOtherLongitudes, 
        ViewMrityuLongitudes,
        ViewSahamaLongitudes, 
        ViewAvasthas,
        ViewSpecialTithis, 
        ViewSpecialTaras, 
        ViewBhavaCusps,
        ViewAstronomicalInfo, 
        ViewNakshatraAspects,
        ViewCharaKarakas, 
        ViewCharaKarakas7, 
        View64Navamsa,
        ViewNonLonBodies
    };

    //ZodiacHouse.cs
    public enum ZodiacHouseName : int
    {
        Ari = 1, 
        Tau = 2, 
        Gem = 3, 
        Can = 4, 
        Leo = 5, 
        Vir = 6,
        Lib = 7, 
        Sco = 8, 
        Sag = 9, 
        Cap = 10, 
        Aqu = 11, 
        Pis = 12
    }
    public enum RiseType : int
    {
        RisesWithHead, RisesWithFoot, RisesWithBoth
    }

    public enum Muhurta : int
    {
        Rudra = 1, 
        Ahi, 
        Mitra, 
        Pitri, 
        Vasu, 
        Ambu, 
        Vishvadeva, 
        Abhijit, 
        Vidhata, 
        Puruhuta,
        Indragni, 
        Nirriti, 
        Varuna, 
        Aryaman, 
        Bhaga, 
        Girisa, 
        Ajapada, 
        Ahirbudhnya, 
        Pushan, 
        Asvi,
        Yama, 
        Agni, 
        Vidhaatri, 
        Chanda, 
        Aditi, 
        JivaAmruta, 
        Vishnu, 
        Arka, 
        Tvashtri, 
        Maruta
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



    [TypeConverter(typeof(EnumDescConverter))]
    public enum EViewStyle
    {
        [Description("Regular")] Normal,
        [Description("Dual Graha Arudhas")] DualGrahaArudha,
        [Description("8 Chara Karakas (regular)")] CharaKarakas8,
        [Description("7 Chara Karakas (mundane)")] CharaKarakas7,
        [Description("Varnada Lagna")] Varnada,
        [Description("Panchanga Print View")] Panchanga
    }



    /// <summary>
    /// Mutually exclusive classes of BodyTypes
    /// </summary>
    public class BodyType
    {
        public enum Name : int
        {
            Lagna, Graha, NonLunarNode,
            SpecialLagna, ChandraLagna,
            BhavaArudha, BhavaArudhaSecondary, GrahaArudha,
            Varnada, Upagraha, Sahama, Other
        }
    }


}
