using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public class OrderedZodiacHousesConverter : ExpandableObjectConverter
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
            Trace.Assert(value is string, "OrderedZodiacHousesConverter::ConvertFrom 1");
            string s = (string)value;

            OrderedZodiacHouses oz = new OrderedZodiacHouses();
            ArrayList al = new ArrayList();
            string[] arr = s.Split(new Char[4] { '.', ' ', ':', ',' });
            foreach (string szh_mixed in arr)
            {
                string szh = szh_mixed.ToLower();
                switch (szh)
                {
                    case "ari": al.Add(ZodiacHouseName.Ari); break;
                    case "tau": al.Add(ZodiacHouseName.Tau); break;
                    case "gem": al.Add(ZodiacHouseName.Gem); break;
                    case "can": al.Add(ZodiacHouseName.Can); break;
                    case "leo": al.Add(ZodiacHouseName.Leo); break;
                    case "vir": al.Add(ZodiacHouseName.Vir); break;
                    case "lib": al.Add(ZodiacHouseName.Lib); break;
                    case "sco": al.Add(ZodiacHouseName.Sco); break;
                    case "sag": al.Add(ZodiacHouseName.Sag); break;
                    case "cap": al.Add(ZodiacHouseName.Cap); break;
                    case "aqu": al.Add(ZodiacHouseName.Aqu); break;
                    case "pis": al.Add(ZodiacHouseName.Pis); break;
                }
            }
            oz.houses = (ArrayList)al.Clone();
            return oz;
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            Trace.Assert(destType == typeof(string) && value is OrderedZodiacHouses, "HMSInfo::ConvertTo 1");
            OrderedZodiacHouses oz = (OrderedZodiacHouses)value;
            return oz.ToString();
        }
    }


}
