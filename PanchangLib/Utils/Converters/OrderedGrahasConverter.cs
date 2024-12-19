using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace org.transliteral.panchang
{

    internal class OrderedGrahasConverter : ExpandableObjectConverter
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
            Trace.Assert(value is string, "OrderedGrahasConverter::ConvertFrom 1");
            string s = (string)value;

            OrderedGrahas oz = new OrderedGrahas();
            ArrayList al = new ArrayList();
            string[] arr = s.Split(new Char[4] { '.', ' ', ':', ',' });
            foreach (string szh_mixed in arr)
            {
                string szh = szh_mixed.ToLower();
                switch (szh)
                {
                    case "as": al.Add(BodyName.Lagna); break;
                    case "su": al.Add(BodyName.Sun); break;
                    case "mo": al.Add(BodyName.Moon); break;
                    case "ma": al.Add(BodyName.Mars); break;
                    case "me": al.Add(BodyName.Mercury); break;
                    case "ju": al.Add(BodyName.Jupiter); break;
                    case "ve": al.Add(BodyName.Venus); break;
                    case "sa": al.Add(BodyName.Saturn); break;
                    case "ra": al.Add(BodyName.Rahu); break;
                    case "ke": al.Add(BodyName.Ketu); break;
                }
            }
            oz.grahas = (ArrayList)al.Clone();
            return oz;
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            Trace.Assert(destType == typeof(string) && value is OrderedGrahas, "OrderedGrahas::ConvertTo 1");
            OrderedGrahas oz = (OrderedGrahas)value;
            return oz.ToString();
        }
    }



}
