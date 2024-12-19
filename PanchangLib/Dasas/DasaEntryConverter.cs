

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;

namespace org.transliteral.panchang
{
    internal class DasaEntryConverter: ExpandableObjectConverter 
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
			Trace.Assert (value is string, "DasaEntryConverter::ConvertFrom 1");
			string s = (string) value;

			DasaEntry de = new DasaEntry(BodyName.Lagna, 0.0, 0.0, 1, "None");
			string[] arr = s.Split (new Char[1] {','});
			if (arr.Length >= 1) de.shortDesc = arr[0];
			if (arr.Length >= 2) de.level = int.Parse(arr[1]);
			if (arr.Length >= 3) de.startUT = double.Parse(arr[2]);
			if (arr.Length >= 4) de.dasaLength = double.Parse(arr[3]);
			if (arr.Length >= 5) de.graha = (BodyName)int.Parse(arr[4]);
			if (arr.Length >= 6) de.zodiacHouse = (ZodiacHouseName)int.Parse(arr[5]);
			return de;
		}

		public override object ConvertTo(
			ITypeDescriptorContext context, 
			CultureInfo culture, 
			object value,    
			Type destType) 
		{
			Trace.Assert (destType == typeof(string) && value is DasaEntry, "DasaItem::ConvertTo 1");
			DasaEntry de = (DasaEntry)value;
			return ( de.shortDesc.ToString() + "," +
				de.level.ToString() + "," +
				de.startUT.ToString() + "," +
				de.dasaLength.ToString() + "," +
				(int)de.graha + "," +
				(int)de.zodiacHouse);
		}   
	}
}
