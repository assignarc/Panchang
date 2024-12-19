using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace org.transliteral.panchang
{

    [Serializable]
    public class HoroscopeOptions : SerializableOptions, ICloneable, ISerializable
    {
        public HoroscopeOptions()
        {
            //sunrisePosition = SunrisePositionType.TrueDiscEdge;
            SunrisePosition = SunrisePositionType.ApparentDiscCenter;
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
            HoroscopeOptions o = new HoroscopeOptions
            {
                SunrisePosition = this.SunrisePosition,
                grahaPositionType = this.grahaPositionType,
                nodeType = this.nodeType,
                Ayanamsa = this.Ayanamsa,
                AyanamsaOffset = this.AyanamsaOffset,
                HoraType = this.HoraType,
                KalaType = this.KalaType,
                BhavaType = this.BhavaType,
                mUserLongitude = this.mUserLongitude.Add(0),
                MaandiType = this.MaandiType,
                GulikaType = this.GulikaType,
                UpagrahaType = this.UpagrahaType,
                EphemerisPath = this.EphemerisPath
            };
            return o;
        }
        public void Copy(HoroscopeOptions o)
        {
            this.SunrisePosition = o.SunrisePosition;
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
        [PropertyOrder(1), Visible("Full Ephemeris Path")]
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
        [Visible("Custom Longitude")]
        public Longitude CustomBodyLongitude
        {
            get { return mUserLongitude; }
            set { this.mUserLongitude = value; }
        }
        [Category(CATEGORY_GENERAL)]
        [PropertyOrder(3), Visible("Ayanamsa Offset")]
        public HMSInfo AyanamsaOffset
        {
            get { return mAyanamsaOffset; }
            set { mAyanamsaOffset = value; }
        }

        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(1), Visible("Upagraha")]
        public EUpagrahaType UpagrahaType
        {
            get { return mUpagrahaType; }
            // TODO: Check this again.
            set { mUpagrahaType = this.mUpagrahaType; }
        }
        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(2), Visible("Maandi")]
        public EMaandiType MaandiType
        {
            get { return mMaandiType; }
            set { mMaandiType = value; }
        }
        [Category(CATEGORY_UPAGRAHA)]
        [PropertyOrder(3), Visible("Gulika")]
        public EMaandiType GulikaType
        {
            get { return mGulikaType; }
            set { mGulikaType = value; }
        }

        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(1), Visible("Sunrise")]
        public SunrisePositionType SunrisePosition
        {
            get { return mSunrisePosition; }
            set { mSunrisePosition = value; }
        }
        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(2), Visible("Hora")]
        public EHoraType HoraType
        {
            get { return mHoraType; }
            set { mHoraType = value; }
        }
        [Category(CATEGORY_SUNRISE)]
        [PropertyOrder(3), Visible("Kala")]
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
        [PropertyOrder(1), Visible("Rahu / Ketu")]
        public ENodeType NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        [Category(CATEGORY_GRAHA)]
        [PropertyOrder(2), Visible("Bhava")]
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
