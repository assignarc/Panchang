using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace org.transliteral.panchang
{
    public class MomentConverter : ExpandableObjectConverter
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
            Trace.Assert(value is string, "MomentConverter::ConvertFrom 1");
            string s = (string)value;

            int day = 1, month = 1, year = 1, hour = 1, min = 1, sec = 1;

            string[] _arr = s.Split(new Char[2] { ' ', ':' });
            ArrayList arr = new ArrayList(_arr);

            if ((String)arr[arr.Count - 1] == "")
                arr[arr.Count - 1] = "0";

            if (arr.Count >= 3)
            {
                while (arr.Count < 6)
                {
                    arr.Add("0");
                }

                day = int.Parse((String)arr[0]);
                month = Moment.FromStringMonth((String)arr[1]);
                year = int.Parse((String)arr[2]);
                hour = int.Parse((String)arr[3]);
                min = int.Parse((String)arr[4]);
                sec = int.Parse((String)arr[5]);
            }

            //if (day < 1 || day > 31) day = 1;
            if (hour < 0 || hour > 23) hour = 12;
            if (min < 0 || min > 120) min = 30;
            if (sec < 0 || sec > 120) sec = 30;
            Moment m = new Moment(year, month, day, hour, min, sec);
            return m;
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            Logger.Info(String.Format("Foo: destType is {0}", destType));
            // Trace.Assert (destType == typeof(string) && value is Moment, "MomentConverter::ConvertTo 1");
            Moment m = (Moment)value;
            return m.ToString();
        }
    }


}
