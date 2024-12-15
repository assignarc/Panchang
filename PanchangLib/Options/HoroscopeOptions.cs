using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace org.transliteral.panchang
{

    [Serializable]
    public class HoroscopeOptions : HoraSerializableOptions, ICloneable, ISerializable
    {
        public HoroscopeOptions()
        {
            //sunrisePosition = SunrisePositionType.TrueDiscEdge;
            sunrisePosition = SunrisePositionType.ApparentDiscCenter;
            mHoraType = EHoraType.Lmt;
            mKalaType = EHoraType.Sunriset;
            mBhavaType = EBhavaType.Start;
            grahaPositionType = EGrahaPositionType.True;
            nodeType = ENodeType.Mean;
            Ayanamsa = AyanamsaType.Lahiri;
            AyanamsaOffset = new HMSInfo(0, 0, 0, Direction.EastWest);
            this.mUserLongitude = new Longitude(0);
            this.MaandiType = EMaandiType.SaturnBegin;
            this.GulikaType = EMaandiType.SaturnMid;
            this.UpagrahaType = EUpagrahaType.Mid;
            mEphemPath = GetExeDir() + "\\eph";
        }
        public object Clone()
        {
            HoroscopeOptions o = new HoroscopeOptions();
            o.sunrisePosition = this.sunrisePosition;
            o.grahaPositionType = this.grahaPositionType;
            o.nodeType = this.nodeType;
            o.Ayanamsa = this.Ayanamsa;
            o.AyanamsaOffset = this.AyanamsaOffset;
            o.HoraType = this.HoraType;
            o.KalaType = this.KalaType;
            o.BhavaType = this.BhavaType;
            o.mUserLongitude = this.mUserLongitude.Add(0);
            o.MaandiType = this.MaandiType;
            o.GulikaType = this.GulikaType;
            o.UpagrahaType = this.UpagrahaType;
            o.EphemerisPath = this.EphemerisPath;
            return o;
        }
        public void Copy(HoroscopeOptions o)
        {
            this.sunrisePosition = o.sunrisePosition;
            this.grahaPositionType = o.grahaPositionType;
            this.nodeType = o.nodeType;
            this.Ayanamsa = o.Ayanamsa;
            this.AyanamsaOffset = o.AyanamsaOffset;
            this.HoraType = o.HoraType;
            this.KalaType = o.KalaType;
            this.BhavaType = o.BhavaType;
            this.mUserLongitude = o.mUserLongitude.Add(0);
            this.MaandiType = o.MaandiType;
            this.GulikaType = o.GulikaType;
            this.UpagrahaType = o.UpagrahaType;
            this.EphemerisPath = o.EphemerisPath;
        }
        private SunrisePositionType mSunrisePosition;
        private EHoraType mHoraType;
        private EHoraType mKalaType;
        public EGrahaPositionType grahaPositionType;
        public ENodeType nodeType;
        private AyanamsaType mAyanamsa;
        private HMSInfo mAyanamsaOffset;
        private EBhavaType mBhavaType;
        private Longitude mUserLongitude;
        private EMaandiType mMaandiType;
        private EMaandiType mGulikaType;
        private EUpagrahaType mUpagrahaType;
        private string mEphemPath;

        protected const string CATEGORY_GENERAL = "1: General Settings";
        protected const string CATEGORY_GRAHA = "2: Graha Settings";
        protected const string CATEGORY_SUNRISE = "3: Sunrise Settings";
        protected const string CATEGORY_UPAGRAHA = "4: Upagraha Settings";


        [Category(CATEGORY_GENERAL)]
        [PropertyOrder(1), @DisplayName("Full Ephemeris Path")]
        public string EphemerisPath
        {
            get { return mEphemPath; }
            set { this.mEphemPath = value; }
        }
        [PropertyOrder(2), Category(CATEGORY_GENERAL)]
        public AyanamsaType Ayanamsa
        {
            get { return mAyanamsa; }
            set { mAyanamsa = value; }
        }
        [PropertyOrder(4), Category(CATEGORY_GENERAL)]
        [@DisplayName("Custom Longitude")]
        public Longitude CustomBodyLongitude
        {
            get { return mUserLongitude; }
            set { this.mUserLongitude = value; }
        }
        [Category(CATEGORY_GENERAL)]
        [PropertyOrder(3), @DisplayName("Ayanamsa Offset")]
        public HMSInfo AyanamsaOffset
        {
            get { return mAyanamsaOffset; }
            set { mAyanamsaOffset = value; }
        }

        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(1), @DisplayName("Upagraha")]
        public EUpagrahaType UpagrahaType
        {
            get { return mUpagrahaType; }
            // TODO: Check this again.
            set { mUpagrahaType = this.mUpagrahaType; }
        }
        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(2), @DisplayName("Maandi")]
        public EMaandiType MaandiType
        {
            get { return mMaandiType; }
            set { mMaandiType = value; }
        }
        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(3), @DisplayName("Gulika")]
        public EMaandiType GulikaType
        {
            get { return mGulikaType; }
            set { mGulikaType = value; }
        }

        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(1), @DisplayName("Sunrise")]
        public SunrisePositionType sunrisePosition
        {
            get { return mSunrisePosition; }
            set { mSunrisePosition = value; }
        }
        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(2), @DisplayName("Hora")]
        public EHoraType HoraType
        {
            get { return mHoraType; }
            set { mHoraType = value; }
        }
        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(3), @DisplayName("Kala")]
        public EHoraType KalaType
        {
            get { return mKalaType; }
            set { mKalaType = value; }
        }
        //public EGrahaPositionType GrahaPositionType
        //{
        //	get { return grahaPositionType; }
        //	set { grahaPositionType = value; }
        //}
        [Category(CATEGORY_GRAHA)]
        [PropertyOrder(1), @DisplayName("Rahu / Ketu")]
        public ENodeType NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        [Category(CATEGORY_GRAHA)]
        [PropertyOrder(2), @DisplayName("Bhava")]
        public EBhavaType BhavaType
        {
            get { return mBhavaType; }
            set { mBhavaType = value; }
        }
        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected HoroscopeOptions(SerializationInfo info, StreamingContext context) :this()
        {
            this.Constructor(this.GetType(), info, context);
        }
    }

}
