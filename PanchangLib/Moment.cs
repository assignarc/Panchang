using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    /// <summary>
    /// Specified a Moment. This can be used for charts, dasa times etc
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(MomentConverter))]
    public class Moment : HoraSerializableOptions, ICloneable, ISerializable
    {
        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected Moment(SerializationInfo info, StreamingContext context) :
            this()
        {
            this.Constructor(this.GetType(), info, context);
        }
        private int m_day, m_month, m_year, m_hour, m_minute, m_second;
        public static void doubleToHMS(double h, ref int hour, ref int minute, ref int second)
        {
            hour = (int)Math.Floor(h);
            h = (h - (double)hour) * 60.0;
            minute = (int)Math.Floor(h);
            h = (h - (double)minute) * 60.0;
            second = (int)Math.Floor(h);
        }
        public Object Clone()
        {
            return new Moment(m_year, m_month, m_day, m_hour, m_minute, m_second);
        }
        public double toUniversalTime()
        {
            return Sweph.swe_julday(m_year, m_month, m_day, time);
        }
        public double toUniversalTime(Horoscope h)
        {
            double local_ut = Sweph.swe_julday(year, month, day, time);
            return local_ut - (h.info.tz.toDouble()) / 24.0;
        }
        public double time
        {
            get
            {
                return (double)m_hour + (double)m_minute / 60.0 + (double)m_second / 3600.0;
            }
        }
        public int day { get { return m_day; } set { m_day = value; } }
        public int month { get { return m_month; } set { m_month = value; } }
        public int year { get { return m_year; } set { m_year = value; } }
        public int hour { get { return m_hour; } set { m_hour = value; } }
        public int minute { get { return m_minute; } set { m_minute = value; } }
        public int second { get { return m_second; } set { m_second = value; } }
        public Moment() => Setup(DateTime.Now);
        
        public Moment(DateTime t) => Setup(t);
        
        public void Setup(DateTime t)
        {
            this.day = t.Day;
            this.month = t.Month;
            this.year = t.Year;
            this.hour = t.Hour;
            this.minute = t.Minute;
            this.second = t.Second;
        }
        public Moment(int year, int month, int day, double time)
        {
            m_day = day;
            m_month = month;
            m_year = year;
            Moment.doubleToHMS(time, ref m_hour, ref m_minute, ref m_second);
        }
        public Moment(int year, int month, int day, int hour, int minute, int second)
        {
            m_day = day;
            m_month = month;
            m_year = year;
            m_hour = hour;
            m_minute = minute;
            m_second = second;
        }
        public Moment(double tjd_ut, Horoscope h)
        {
            double time = 0;
            tjd_ut += h.info.tz.toDouble() / 24.0;
            Sweph.swe_revjul(tjd_ut, ref m_year, ref m_month, ref m_day, ref time);
            Moment.doubleToHMS(time, ref m_hour, ref m_minute, ref m_second);
        }
        public static int FromStringMonth(string s)
        {
            switch (s)
            {
                case "Jan": return 1;
                case "Feb": return 2;
                case "Mar": return 3;
                case "Apr": return 4;
                case "May": return 5;
                case "Jun": return 6;
                case "Jul": return 7;
                case "Aug": return 8;
                case "Sep": return 9;
                case "Oct": return 10;
                case "Nov": return 11;
                case "Dec": return 12;
            }

            return 1;
        }
        public string ToStringMonth(int i)
        {
            switch (i)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
            }
            Trace.Assert(false, "Moment::ToStringMonth");
            return "";
        }
        override public string ToString()
        {
            return (m_day < 10 ? "0" : "") + m_day.ToString() +
                " " + ToStringMonth(m_month) + " " + m_year.ToString()
                + " " + (m_hour < 10 ? "0" : "") + m_hour.ToString()
                + ":" + (m_minute < 10 ? "0" : "") + m_minute.ToString()
                + ":" + (m_second < 10 ? "0" : "") + m_second.ToString();
        }
        public string ToShortDateString()
        {
            int year = m_year % 100;
            return string.Format("{0:00}-{1:00}-{2:00}", m_day, m_month, year);
        }
        public string ToDateString()
        {
            return string.Format("{0:00} {1} {2}", m_day, ToStringMonth(m_month), m_year);
        }

        public string ToTimeString()
        {
            return this.ToTimeString(false);
        }
        public string ToTimeString(bool bDisplaySeconds)
        {
            if (bDisplaySeconds)
                return string.Format("{0:00}:{1:00}:{2:00}", m_hour, m_minute, m_second);
            else
                return string.Format("{0:00}:{1:00}", m_hour, m_minute);
        }
    }


}
