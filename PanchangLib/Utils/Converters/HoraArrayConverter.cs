using System;
using System.ComponentModel;
using System.Globalization;

namespace org.transliteral.panchang
{
    public class HoraArrayConverter : ArrayConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            if (destType == typeof(string))
                return "Click Here To Modify";
            else
                return base.ConvertTo(context, culture, value, destType);
        }

    }

}
