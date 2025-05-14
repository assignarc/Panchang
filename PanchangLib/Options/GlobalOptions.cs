
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
namespace org.transliteral.panchang
{


    /// <summary>
    /// Summary description for GlobalOptions.
    /// </summary>
    [XmlRoot("HoraOptions")]
    [Serializable]
    public class GlobalOptions : SerializableOptions, ISerializable
    {
        //[NonSerialized]	
        //public static object Reference = null;

        public HoroscopeOptions HOptions = new HoroscopeOptions();
        public StrengthOptions SOptions = new StrengthOptions();

        protected HMSInfo mLat=  new HMSInfo(33, 0, 18, Direction.NorthSouth);
        protected HMSInfo mLon= new HMSInfo(-97,13, 35, Direction.EastWest);
        protected HMSInfo mTz = new HMSInfo(-5, 0, 0, Direction.EastWest);

        // Dasa Control
        protected bool bDasaHoverSelect = false;
        protected bool bDasaMoveSelect = true;
        protected bool bDasaShowEvents = true;
        protected int miDasaShowEventsLevel = 2;

        // Varga charts
        protected bool bVargaSquare = true;
        protected bool bVargaShowDob = true;
        protected bool bVargaShowSAVRasi = false;
        protected bool bVargaShowSAVVarga = true;
       
        protected EChartStyle mChartStyle = EChartStyle.SouthIndian;
       
        protected const string CAT_GENERAL = "1: General Settings";
        protected const string CAT_LOCATION = "2: Default Location";
        protected const string CAT_LF_GEN = "3: Look and Feel";
        protected const string CAT_LF_DASA = "3: Look and Feel: Dasa";
        protected const string CAT_LF_DIV = "4: Look and Feel: Vargas";
        protected const string CAT_LF_TABLE = "5: Look and Feel: Tabular Charts";
        protected const string CAT_LF_CHAKRA = "6: Look and Feel: Chakras";
        protected const string CAT_LF_BINDUS = "7: Look and Feel: Bindus";

        public static event EvtChanged CalculationPrefsChanged = null;
        public static GlobalOptions Instance
        {
            get;
            set;
        }
        public GlobalOptions()
        {
            //All options are set to default values
            
            //HOptions = new HoroscopeOptions();
            //SOptions = new StrengthOptions();

            ///*   
            //mLat = new HMSInfo(47, 40, 27, dir_type.NS);
            //mLon = new HMSInfo(-122, 7, 13, dir_type.EW);
            //mTz = new HMSInfo(-7, 0, 0, dir_type.EW);
            //*/
            //mLat = new HMSInfo(33, 0, 18, Direction.NorthSouth);
            //mLon = new HMSInfo(-97,13, 35, Direction.EastWest);
            //mTz = new HMSInfo(-5, 0, 0, Direction.EastWest);

         

            //this.bDasaHoverSelect = false;
            //this.bDasaMoveSelect = true;
            //this.bDasaShowEvents = true;
            //this.miDasaShowEventsLevel = 2;
         
           
            //this.mChartStyle = EChartStyle.SouthIndian;
            //this.bVargaSquare = true;
            //this.bVargaShowDob = true;
            //this.bVargaShowSAVVarga = true;
            //this.bVargaShowSAVRasi = false;
            
           
        }


        
        [PropertyOrder(1), Category(CAT_LOCATION)]
        public HMSInfo Latitude
        {
            get { return mLat; }
            set { mLat = value; }
        }
        [PropertyOrder(2), Category(CAT_LOCATION)]
        public HMSInfo Longitude
        {
            get { return mLon; }
            set { mLon = value; }
        }
        [PropertyOrder(3), Category(CAT_LOCATION)]
        [Visible("Time zone")]
        public HMSInfo TimeZone
        {
            get { return mTz; }
            set { mTz = value; }
        }


       

        [PropertyOrder(1), Category(CAT_LF_DIV)]
        [Visible("Display style")]
        public EChartStyle VargaStyle
        {
            get { return this.mChartStyle; }
            set { this.mChartStyle = value; }
        }
        [PropertyOrder(2), Category(CAT_LF_DIV)]
        [Visible("Maintain square proportions")]
        public bool VargaChartIsSquare
        {
            get { return this.bVargaSquare; }
            set { this.bVargaSquare = value; }
        }
        [PropertyOrder(3), Category(CAT_LF_DIV)]
        [Visible("Show time of birth")]
        public bool VargaShowDob
        {
            get { return this.bVargaShowDob; }
            set { this.bVargaShowDob = value; }
        }
        [PropertyOrder(4), Category(CAT_LF_DIV)]
        [Visible("Show rasi's SAV bindus")]
        public bool VargaShowSAVRasi
        {
            get { return this.bVargaShowSAVRasi; }
            set { this.bVargaShowSAVRasi = value; }
        }
        [PropertyOrder(5), Category(CAT_LF_DIV)]
        [Visible("Show varga's SAV bindus")]
        public bool VargaShowSAVVarga
        {
            get { return this.bVargaShowSAVVarga; }
            set { this.bVargaShowSAVVarga = value; }
        }





        public static GlobalOptions ReadFromFile()
        {
            GlobalOptions gOpts = new GlobalOptions();
            try
            {
                FileStream sOut;
                sOut = new FileStream(GlobalOptions.GetOptsFilename(), FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter
                {
                    AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                };
                gOpts = (GlobalOptions)formatter.Deserialize(sOut);
                sOut.Close();
            }
            catch
            {
                Logger.Info(String.Format("Unable to read user preferences {0}", "GlobalOptions"));
            }

            GlobalOptions.Instance = gOpts;
            return gOpts;
        }

        public void SaveToFile()
        {
            Logger.Info(String.Format("Saving Preferences to {0}", GlobalOptions.GetOptsFilename()));
            FileStream sOut = new FileStream(GlobalOptions.GetOptsFilename(), FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(sOut, this);
            sOut.Close();
        }

        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected GlobalOptions(SerializationInfo info, StreamingContext context) :
            this()
        {
            this.Constructor(this.GetType(), info, context);
        }

    }
}
