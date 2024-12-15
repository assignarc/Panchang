using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace org.transliteral.panchang
{/// <summary>
 /// A package of longitude related functions. These are useful enough that
 /// I have justified using an object instead of a simple double value type
 /// </summary>
 /// 
    internal class LongitudeConverter : ExpandableObjectConverter
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
            Trace.Assert(value is string, "LongitudeConverter::ConvertFrom 1");
            string s = (string)value;

            string[] arr = s.Split(new Char[3] { '.', ' ', ':' });

            double lonValue = 0;
            if (arr.Length >= 1) lonValue = int.Parse(arr[0]);
            if (arr.Length >= 2)
            {
                switch (arr[1].ToLower())
                {
                    case "ari": lonValue += 0.0; break;
                    case "tau": lonValue += 30.0; break;
                    case "gem": lonValue += 60.0; break;
                    case "can": lonValue += 90.0; break;
                    case "leo": lonValue += 120.0; break;
                    case "vir": lonValue += 150.0; break;
                    case "lib": lonValue += 180.0; break;
                    case "sco": lonValue += 210.0; break;
                    case "sag": lonValue += 240.0; break;
                    case "cap": lonValue += 270.0; break;
                    case "aqu": lonValue += 300.0; break;
                    case "pis": lonValue += 330.0; break;
                }
            }
            double divider = 60;
            for (int i = 2; i < arr.Length; i++)
            {
                lonValue += (double.Parse(arr[i]) / divider);
                divider *= 60.0;
            }

            return new Longitude(lonValue);
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            Trace.Assert(destType == typeof(string) && value is Longitude, "Longitude::ConvertTo 1");
            Longitude lon = (Longitude)value;
            return lon.ToString();
        }
    }


}
