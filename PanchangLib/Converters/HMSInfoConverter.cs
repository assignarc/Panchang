using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace org.transliteral.panchang
{
    internal class HMSInfoConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {
            if (t == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, t);
        }
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo info,
            object value)
        {
            Trace.Assert(value is string, "HMSInfoConverter::ConvertFrom 1");
            string s = (string)value;

            int hour = 1, min = 1, sec = 1;

            Direction dir = Direction.NorthSouth;
            string[] _arr = s.Split(new Char[3] { '.', ' ', ':' });
            ArrayList arr = new ArrayList(_arr);

            if (arr.Count >= 2)
            {
                if ((String)arr[arr.Count - 1] == "")
                    arr[arr.Count - 1] = "0";

                while (arr.Count < 4)
                    arr.Add("0");

                hour = int.Parse((String)arr[0]);
                String sdir = (String)arr[1];
                if (sdir == "W" || sdir == "w" || sdir == "S" || sdir == "s")
                    hour *= -1;
                if (sdir == "W" || sdir == "w" || sdir == "E" || sdir == "e")
                    dir = Direction.EastWest;
                else
                    dir = Direction.NorthSouth;

                min = int.Parse((String)arr[2]);
                sec = int.Parse((String)arr[3]);
            }

            if (hour < -180 || hour > 180) hour = 29;
            if (min < 0 || min > 60) min = 20;
            if (sec < 0 || sec > 60) sec = 30;
            HMSInfo hi = new HMSInfo(hour, min, sec, dir);
            return hi;
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            Trace.Assert(destType == typeof(string) && value is HMSInfo, "HMSInfo::ConvertTo 1");
            HMSInfo hi = (HMSInfo)value;
            return hi.ToString();
        }
    }
}
