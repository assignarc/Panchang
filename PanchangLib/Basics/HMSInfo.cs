using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    [Serializable]
    [TypeConverter(typeof(HMSInfoConverter))]
    public class HMSInfo : MhoraSerializableOptions, ICloneable, ISerializable
    {
        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected HMSInfo(SerializationInfo info, StreamingContext context) :
        this()
        {
            this.Constructor(this.GetType(), info, context);
        }
       
        public Direction direction;
        private int m_hour, m_minute, m_second;

        public Direction dir
        {
            get { return direction; }
            set { direction = value; }
        }

        public int degree
        {
            get { return m_hour; }
            set { m_hour = value; }
        }

        public int minute
        {
            get { return m_minute; }
            set { m_minute = value; }
        }

        public int second
        {
            get { return m_second; }
            set { m_second = value; }
        }
        public double toDouble()
        {
            if (m_hour >= 0)
                return (((double)m_hour) + (((double)m_minute) / 60.0) + (((double)m_second) / 3600.0));
            else
                return (((double)m_hour) - (((double)m_minute) / 60.0) - (((double)m_second) / 3600.0));
        }
        public HMSInfo()
        {
            m_hour = m_minute = m_second = 0;
            direction = Direction.NS;
        }
        public HMSInfo(int hour, int min, int sec, Direction dt)
        {
            m_hour = hour;
            m_minute = min;
            m_second = sec;
            direction = dt;
        }
        public HMSInfo(double hms)
        {
            double hour = Math.Floor(hms);
            hms = (hms - hour) * 60.0;
            double min = Math.Floor(hms);
            hms = (hms - min) * 60.0;
            double sec = Math.Floor(hms);
            m_hour = (int)hour;
            m_minute = (int)min;
            m_second = (int)sec;
            direction = Direction.NS;
        }
        override public string ToString()
        {
            string dirs;
            if (direction == Direction.EW && m_hour < 0) dirs = "W";
            else if (direction == Direction.EW) dirs = "E";
            else if (direction == Direction.NS && m_hour < 0) dirs = "S";
            else dirs = "N";


            int m_htemp = m_hour >= 0 ? m_hour : m_hour * -1;
            return (m_htemp < 10 ? "0" : "") + m_htemp.ToString()
                + " " + dirs
                + " " + (m_minute < 10 ? "0" : "") + m_minute.ToString()
                + ":" + (m_second < 10 ? "0" : "") + m_second.ToString();
        }
        public object Clone()
        {
            return new HMSInfo(m_hour, m_minute, m_second, direction);
        }
    }

}
