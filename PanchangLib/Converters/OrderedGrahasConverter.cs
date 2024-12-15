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
                    case "as": al.Add(Body.Name.Lagna); break;
                    case "su": al.Add(Body.Name.Sun); break;
                    case "mo": al.Add(Body.Name.Moon); break;
                    case "ma": al.Add(Body.Name.Mars); break;
                    case "me": al.Add(Body.Name.Mercury); break;
                    case "ju": al.Add(Body.Name.Jupiter); break;
                    case "ve": al.Add(Body.Name.Venus); break;
                    case "sa": al.Add(Body.Name.Saturn); break;
                    case "ra": al.Add(Body.Name.Rahu); break;
                    case "ke": al.Add(Body.Name.Ketu); break;
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
