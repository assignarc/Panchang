using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace org.transliteral.panchang
{
    /// <summary>
    /// A class containing all required input from the user for a given chart
    /// (e.g.) all the information contained in a .jhd file
    /// </summary>
    ///
    [Serializable]
    public class HoraInfo : MhoraSerializableOptions, ICloneable, ISerializable
    {
        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected HoraInfo(SerializationInfo info, StreamingContext context) :
        this()
        {
            this.Constructor(this.GetType(), info, context);
        }

        public object Clone()
        {
            HoraInfo hi = new HoraInfo((Moment)tob.Clone(), (HMSInfo)lat.Clone(), (HMSInfo)lon.Clone(), (HMSInfo)tz.Clone());
            hi.events = this.events;
            hi.name = this.name;
            hi.defaultYearCompression = this.defaultYearCompression;
            hi.defaultYearLength = this.defaultYearLength;
            hi.defaultYearType = this.defaultYearType;
            return hi;
        }
       
        public HoraType type;
        public Moment tob;
        //public double lon, lat, alt, tz;
        public double alt;
        public HMSInfo lon, lat, tz;
        public string name;
        public EFileType FileType;
        public double defaultYearLength = 0;
        public double defaultYearCompression = 0;
        public DateType defaultYearType = DateType.FixedYear;

        private UserEvent[] events = null;

        private const string CAT_TOB = "1: Birth Info";
        private const string CAT_EVT = "2: Events";

        [Category(CAT_TOB)]
        [PropertyOrder(1), PGDisplayName("Time of Birth")]
        [Description("Date of Birth. Format is 'dd Mmm yyyy hh:mm:ss'\n Example 23 Mar 1979 23:11:00")]
        public Moment DateOfBirth
        {
            get { return tob; }
            set { tob = value; }
        }

        [Category(CAT_TOB), PropertyOrder(2)]
        [Description("Latitude. Format is 'hh D mm:ss mm:ss'\n Example 23 N 24:00")]
        public HMSInfo Latitude
        {
            get { return lat; }
            set { lat = value; }
        }

        [Category(CAT_TOB), PropertyOrder(3)]
        [Description("Longitude. Format is 'hh D mm:ss mm:ss'\n Example 23 E 24:00")]
        public HMSInfo Longitude
        {
            get { return lon; }
            set { lon = value; }
        }

        [Category(CAT_TOB), PropertyOrder(4)]
        [PGDisplayName("Time zone")]
        [Description("Time Zone. Format is 'hh D mm:ss mm:ss'\n Example 3 E 00:00")]
        public HMSInfo TimeZone
        {
            get { return tz; }
            set { tz = value; }
        }

        [Category(CAT_TOB), PropertyOrder(5)]
        public double Altitude
        {
            get { return alt; }
            set { alt = value; }
        }

        [Category(CAT_EVT), PropertyOrder(1)]
        [Description("Events")]
        public UserEvent[] Events
        {
            get { return this.events; }
            set { this.events = value; }
        }

        public HoraInfo(Moment atob, HMSInfo alat, HMSInfo alon, HMSInfo atz)
        {
            tob = atob;
            lon = alon;
            lat = alat;
            tz = atz;
            alt = 0.0;
            this.type = HoraType.Birth;
            this.FileType = EFileType.MudgalaHora;
            this.events = new UserEvent[0];
        }
        public HoraInfo()
        {
            System.DateTime t = DateTime.Now;
            tob = new Moment(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
            lon = (HMSInfo) MhoraGlobalOptions.Instance.Longitude.Clone();
            lat = (HMSInfo) MhoraGlobalOptions.Instance.Latitude.Clone();
            tz = (HMSInfo) MhoraGlobalOptions.Instance.TimeZone.Clone();
            alt = 0.0;
            this.type = HoraType.Birth;
            this.FileType = EFileType.MudgalaHora;
            this.events = new UserEvent[0];
        }
    }

}
