using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class SingleDivisionConverter : ExpandableObjectConverter
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
            return new Division.SingleDivision(DivisionType.Rasi);
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            //Trace.Assert (destType == typeof(string) && value is Division, "DivisionConverter::ConvertTo 1");
            Division.SingleDivision dSingle = value as Division.SingleDivision;
            if (dSingle == null) return "Single Varga";
            return dSingle.ToString();
        }
    }


}
