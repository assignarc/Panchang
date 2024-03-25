

using System.ComponentModel;

namespace org.transliteral.panchang
{
    [TypeConverter(typeof(DasaEntryConverter))]
	public class DasaEntry
	{
		public Body.Name graha;
		public ZodiacHouseName zodiacHouse;
		public double startUT;
		public double dasaLength; // 1 year = 360 days = 360 degrees is used internally!!!!
		public int level;
		public string shortDesc;

		private void Construct (double _startUT, double _dasaLength, int _level, string _shortDesc)
		{
			startUT = _startUT;
			dasaLength = _dasaLength;
			level = _level;
			shortDesc = _shortDesc;
		}
		public DasaEntry (Body.Name _graha, double _startUT, double _dasaLength, int _level, string _shortDesc)
		{
			graha = _graha;
			Construct (_startUT, _dasaLength, _level, _shortDesc);
		}
		public DasaEntry (ZodiacHouseName _zodiacHouse, double _startUT, double _dasaLength, int _level, string _shortDesc)
		{
			zodiacHouse = _zodiacHouse;
			Construct (_startUT, _dasaLength, _level, _shortDesc);
		}
		public DasaEntry ()
		{
			startUT = dasaLength = 0.0;
			level = 1;
			shortDesc = "Jup";
			graha = Body.Name.Jupiter;
			zodiacHouse = ZodiacHouseName.Ari;
		}
		public string DasaName 
		{
			get { return shortDesc; }
			set { shortDesc = value; }
		}
		public int DasaLevel
		{
			get { return level; }
			set { level = value; }
		}
		public double StartUT
		{
			get { return startUT; }
			set { startUT = value; }
		}
		public double DasaLength
		{
			get { return dasaLength; }
			set { dasaLength = value; }
		}
		public Body.Name Graha
		{
			get { return graha; }
			set { graha = value; }
		}
		public ZodiacHouseName ZHouse
		{
			get { return zodiacHouse; }
			set { zodiacHouse = value; }
		}
	}
}
