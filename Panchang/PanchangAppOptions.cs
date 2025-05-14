
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
namespace org.transliteral.panchang.app
{


    /// <summary>
    /// Summary description for GlobalAppOptions.
    /// </summary>
    [XmlRoot("HoraOptions")]
    [Serializable]
    public class PanchangAppOptions : GlobalOptions
    {
        //[NonSerialized]	public static object Reference = null;
        [NonSerialized] 
        public static object mainControl = null;


        // General
        protected bool mbShowSplashScreeen;
        protected bool mbSavePrefsOnExit;
        protected string msNotesExtension;

        // Dasa Control
        protected Color mcDasaBackColor;
        protected Color mcDasaDateColor;
        protected Color mcDasaPeriodColor;
        protected Color mcDasaHighlightColor;

        // Body Colors
        protected Color mcBodyLagna;
        protected Color mcBodySun;
        protected Color mcBodyMoon;
        protected Color mcBodyMars;
        protected Color mcBodyMercury;
        protected Color mcBodyJupiter;
        protected Color mcBodyVenus;
        protected Color mcBodySaturn;
        protected Color mcBodyRahu;
        protected Color mcBodyKetu;
        protected Color mcBodyOther;

        // General Font families
        protected Font mfGeneral;
        protected Font mfFixedWidth;

        // Varga charts
        protected Color mcVargaBackground;
        protected Color mcVargaSecondary;
        protected Color mcVargaGraha;
        protected Color mcVargaLagna;
        protected Color mcVargaSAV;
        protected Color mcVargaSpecialLagna;
        protected Font mfVarga;

        // Tabular Displays
        protected Color mcTableBackground;
        protected Color mcTableForeground;
        protected Color mcTableInterleaveFirst;
        protected Color mcTableInterleaveSecond;

        // Chakra Displays
        protected Color mcChakraBackground;

        // Form Widths
        public Size GrahaStrengthsFormSize = new Size(0, 0);
        public Size RasiStrengthsFormSize = new Size(0, 0);
        public Size VargaRectificationFormSize = new Size(0, 0);

        private static PanchangAppOptions PanchangAppOptionsInstance;
        public static new PanchangAppOptions Instance {
            get
            {
                return PanchangAppOptionsInstance;
            }
            
            set { 
                PanchangAppOptionsInstance = value;
                GlobalOptions.Instance = value;
            }
        }
        public static event EvtChanged DisplayPrefsChanged = null;
        public static new event EvtChanged CalculationPrefsChanged = null;

       
        public static void NotifyDisplayChange()
        {
            PanchangAppOptions.DisplayPrefsChanged(PanchangAppOptions.Instance);
        }

        public static void NotifyCalculationChange()
        {
            PanchangAppOptions.CalculationPrefsChanged(PanchangAppOptions.Instance.HOptions);
        }

        public PanchangAppOptions() : base()
        {

            this.mfFixedWidth = new Font("Courier New", 10);
            this.mfGeneral = new Font("Microsoft Sans Serif", 10);

            this.mcDasaBackColor = Color.Lavender;
            this.mcDasaDateColor = Color.DarkRed;
            this.mcDasaPeriodColor = Color.DarkBlue;
            this.mcDasaHighlightColor = Color.White;


            this.msNotesExtension = "txt";

            this.mcBodyLagna = Color.BlanchedAlmond;
            this.mcBodySun = Color.Orange;
            this.mcBodyMoon = Color.LightSkyBlue;
            this.mcBodyMars = Color.Red;
            this.mcBodyMercury = Color.Green;
            this.mcBodyJupiter = Color.Yellow;
            this.mcBodyVenus = Color.Violet;
            this.mcBodySaturn = Color.DarkBlue;
            this.mcBodyRahu = Color.LightBlue;
            this.mcBodyKetu = Color.LightPink;
            this.mcBodyOther = Color.Black;

            this.mcVargaBackground = Color.AliceBlue;
            this.mcVargaSecondary = Color.CadetBlue;
            this.mcVargaGraha = Color.DarkRed;
            this.mcVargaLagna = Color.DarkViolet;
            this.mcVargaSAV = Color.Gainsboro;
            this.mcVargaSpecialLagna = Color.Gray;
            this.mfVarga = new Font("Times New Roman", 7);
            
            this.mcTableBackground = Color.Lavender;
            this.mcTableForeground = Color.Black;
            this.mcTableInterleaveFirst = Color.AliceBlue;
            this.mcTableInterleaveSecond = Color.Lavender;

            this.mcChakraBackground = Color.AliceBlue;
        }


        [Category(CAT_GENERAL)]
        [PropertyOrder(1), Visible("Show splash screen")]
        public bool ShowSplashScreen
        {
            get { return this.mbShowSplashScreeen; }
            set { this.mbShowSplashScreeen = value; }
        }

        [Category(CAT_GENERAL)]
        [PropertyOrder(2), Visible("Save Preferences on Exit")]
        public bool SavePrefsOnExit
        {
            get { return this.mbSavePrefsOnExit; }
            set { this.mbSavePrefsOnExit = value; }
        }

        [Category(CAT_GENERAL)]
        [PropertyOrder(3), Visible("Notes file type")]
        public string ChartNotesFileExtension
        {
            get { return this.msNotesExtension; }
            set { this.msNotesExtension = value; }
        }
        [Category(CAT_GENERAL)]
        [PropertyOrder(4), Visible("Yogas file name")]
        public string YogasFileName
        {
            get { return PanchangAppOptions.GetExeDir() + "\\" + "yogas.mhr"; }
        }

       


        [Category(CAT_LF_GEN)]
        [Visible("Font")]
        public Font GeneralFont
        {
            get { return this.mfGeneral; }
            set { this.mfGeneral = value; }
        }
        [Category(CAT_LF_GEN)]
        [Visible("Fixed width font")]
        public Font FixedWidthFont
        {
            get { return this.mfFixedWidth; }
            set { this.mfFixedWidth = value; }
        }

        [PropertyOrder(1), Category(CAT_LF_DASA)]
        [Visible("Select by Mouse Hover")]
        public bool DasaHoverSelect
        {
            get { return this.bDasaHoverSelect; }
            set { this.bDasaHoverSelect = value; }
        }

        [PropertyOrder(1), Category(CAT_LF_DASA)]
        [Visible("Select by Mouse Move")]
        public bool DasaMoveSelect
        {
            get { return this.bDasaMoveSelect; }
            set { this.bDasaMoveSelect = value; }
        }

        [PropertyOrder(2), Category(CAT_LF_DASA)]
        [Visible("Show Events")]
        public bool DasaShowEvents
        {
            get { return this.bDasaShowEvents; }
            set { this.bDasaShowEvents = value; }
        }
        [PropertyOrder(3), Category(CAT_LF_DASA)]
        [Visible("Show Events Level")]
        public int DasaEventsLevel
        {
            get { return this.miDasaShowEventsLevel; }
            set { this.miDasaShowEventsLevel = value; }
        }

        [PropertyOrder(4), Category(CAT_LF_DASA)]
        [Visible("Period foreground color")]
        public Color DasaPeriodColor
        {
            get { return this.mcDasaPeriodColor; }
            set { this.mcDasaPeriodColor = value; }
        }
        [PropertyOrder(5), Category(CAT_LF_DASA)]
        [Visible("Date foreground color")]
        public Color DasaDateColor
        {
            get { return this.mcDasaDateColor; }
            set { this.mcDasaDateColor = value; }
        }
        [PropertyOrder(6), Category(CAT_LF_DASA)]
        [Visible("Background colour")]
        public Color DasaBackgroundColor
        {
            get { return mcDasaBackColor; }
            set { mcDasaBackColor = value; }
        }
        [PropertyOrder(7), Category(CAT_LF_DASA)]
        [Visible("Item highlight color")]
        public Color DasaHighlightColor
        {
            get { return this.mcDasaHighlightColor; }
            set { this.mcDasaHighlightColor = value; }
        }

        [PropertyOrder(6), Category(CAT_LF_DIV)]
        [Visible("Background colour")]
        public Color VargaBackgroundColor
        {
            get { return this.mcVargaBackground; }
            set { this.mcVargaBackground = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(7), Visible("Graha foreground colour")]
        public Color VargaGrahaColor
        {
            get { return this.mcVargaGraha; ; }
            set { this.mcVargaGraha = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(8), Visible("Secondary foreground colour")]
        public Color VargaSecondaryColor
        {
            get { return this.mcVargaSecondary; }
            set { this.mcVargaSecondary = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(9), Visible("Lagna foreground colour")]
        public Color VargaLagnaColor
        {
            get { return this.mcVargaLagna; }
            set { this.mcVargaLagna = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(10), Visible("Special lagna foreground colour")]
        public Color VargaSpecialLagnaColor
        {
            get { return this.mcVargaSpecialLagna; }
            set { this.mcVargaSpecialLagna = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(11), Visible("SAV foreground colour")]
        public Color VargaSAVColor
        {
            get { return this.mcVargaSAV; }
            set { this.mcVargaSAV = value; }
        }
        [Category(CAT_LF_DIV)]
        [PropertyOrder(12), Visible("Font")]
        public Font VargaFont
        {
            get { return this.mfVarga; }
            set { this.mfVarga = value; }
        }

        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(1), Visible("Lagna")]
        public Color BindusLagnaColor
        {
            get { return this.mcBodyLagna; }
            set { this.mcBodyLagna = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(2), Visible("Sun")]
        public Color BindusSunColor
        {
            get { return this.mcBodySun; }
            set { this.mcBodySun = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(3), Visible("Moon")]
        public Color BindusMoonColor
        {
            get { return this.mcBodyMoon; }
            set { this.mcBodyMoon = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(4), Visible("Mars")]
        public Color BindusMarsColor
        {
            get { return this.mcBodyMars; }
            set { this.mcBodyMars = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(5), Visible("Mercury")]
        public Color BindusMercuryColor
        {
            get { return this.mcBodyMercury; }
            set { this.mcBodyMercury = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(6), Visible("Jupiter")]
        public Color BindusJupiterColor
        {
            get { return this.mcBodyJupiter; }
            set { this.mcBodyJupiter = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(7), Visible("Venus")]
        public Color BindusVenusColor
        {
            get { return this.mcBodyVenus; }
            set { this.mcBodyVenus = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(8), Visible("Saturn")]
        public Color BindusSaturnColor
        {
            get { return this.mcBodySaturn; }
            set { this.mcBodySaturn = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(9), Visible("Rahu")]
        public Color BindusRahuColor
        {
            get { return this.mcBodyRahu; }
            set { this.mcBodyRahu = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(10), Visible("Ketu")]
        public Color BindusKetuColor
        {
            get { return this.mcBodyKetu; }
            set { this.mcBodyKetu = value; }
        }
        [Category(CAT_LF_BINDUS)]
        [PropertyOrder(11), Visible("Other")]
        public Color BindusOtherColor
        {
            get { return this.mcBodyOther; }
            set { this.mcBodyOther = value; }
        }

        [Category(CAT_LF_TABLE)]
        [PropertyOrder(1), Visible("Background colour")]
        public Color TableBackgroundColor
        {
            get { return this.mcTableBackground; }
            set { this.mcTableBackground = value; }
        }
        [Category(CAT_LF_TABLE)]
        [PropertyOrder(2), Visible("Foreground colour")]
        public Color TableForegroundColor
        {
            get { return this.mcTableForeground; }
            set { this.mcTableForeground = value; }
        }
        [Category(CAT_LF_TABLE)]
        [PropertyOrder(3), Visible("Interleave colour (odd)")]
        public Color TableOddRowColor
        {
            get { return this.mcTableInterleaveFirst; }
            set { this.mcTableInterleaveFirst = value; }
        }
        [Category(CAT_LF_TABLE)]
        [PropertyOrder(4), Visible("Interleave colour (even)")]
        public Color TableEvenRowColor
        {
            get { return this.mcTableInterleaveSecond; }
            set { this.mcTableInterleaveSecond = value; }
        }

        [Category(CAT_LF_CHAKRA)]
        [Visible("Background colour")]
        public Color ChakraBackgroundColor
        {
            get { return this.mcChakraBackground; }
            set { this.mcChakraBackground = value; }
        }

        private Font addToFontSizesHelper(Font f, int i)
        {
            return new Font(f.FontFamily, f.SizeInPoints + i);
        }
        private void addToFontSizes(int i)
        {
            this.mfFixedWidth = this.addToFontSizesHelper(this.mfFixedWidth, i);
            this.mfGeneral = this.addToFontSizesHelper(this.mfGeneral, i);
            this.mfVarga = this.addToFontSizesHelper(this.mfVarga, i);
        }
        public void increaseFontSize()
        {
            this.addToFontSizes(1);
        }
        public void decreaseFontSize()
        {
            this.addToFontSizes(-1);
        }

       
        public Color getBinduColor(BodyName b)
        {
            switch (b)
            {
                case BodyName.Lagna: return this.mcBodyLagna;
                case BodyName.Sun: return this.mcBodySun;
                case BodyName.Moon: return this.mcBodyMoon;
                case BodyName.Mars: return this.mcBodyMars;
                case BodyName.Mercury: return this.mcBodyMercury;
                case BodyName.Jupiter: return this.mcBodyJupiter;
                case BodyName.Venus: return this.mcBodyVenus;
                case BodyName.Saturn: return this.mcBodySaturn;
                case BodyName.Rahu: return this.mcBodyRahu;
                case BodyName.Ketu: return this.mcBodyKetu;
                default: return this.mcBodyOther;
            }
        }


        public static new PanchangAppOptions readFromFile()
        {
            PanchangAppOptions gOpts = new PanchangAppOptions();
            try
            {
                FileStream sOut;
                sOut = new FileStream(PanchangAppOptions.GetOptsFilename(), FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                gOpts = (PanchangAppOptions)formatter.Deserialize(sOut);
                sOut.Close();
            }
            catch
            {
                Logger.Info(String.Format("Unable to read user preferences {0}", "GlobalAppOptions"));
            }

            PanchangAppOptions.Instance = gOpts;
            return gOpts;
        }

        public new void saveToFile()
        {
            Logger.Info(String.Format("Saving Preferences to {0}", PanchangAppOptions.GetOptsFilename()));
            FileStream sOut = new FileStream(PanchangAppOptions.GetOptsFilename(), FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(sOut, this);
            sOut.Close();
        }

       

        protected PanchangAppOptions(SerializationInfo info, StreamingContext context) :
            this()
        {
            this.Constructor(this.GetType(), info, context);
        }

    }
}
