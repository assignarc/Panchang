

using System;

namespace org.transliteral.panchang
{
    abstract public class Dasa
	{
		static public int NarayanaDasaLength (ZodiacHouse zh, DivisionPosition dp) 
		{
			int length=0;

			if (zh.isOddFooted())
				length = zh.numHousesBetween(dp.zodiac_house);
			else
				length = zh.numHousesBetweenReverse(dp.zodiac_house);
		
			if (dp.isExaltedPhalita())
				length++;
			else if (dp.isDebilitatedPhalita())
				length--;

			length = Basics.normalize_inc(1, 12, length-1);
			return length;
		}



		public event EvtChanged Changed;
		public void DivisionChanged(Division d)
		{
		}
		public void OnChanged ()
		{
			if (Changed != null)
				Changed(this);
		}
		public Recalculate RecalculateEvent;

		public class Options : ICloneable
		{
			private DateType DateType;
			private double _YearLength;
			private double _Compression;
			private double _OffsetDays;
			private double _OffsetHours;
			private double _OffsetMins;
			public Options () 
			{
				DateType = DateType.SolarYear;
				_YearLength = 360.0;
				_Compression = 0.0;
			}
			[PGDisplayName("Year Type")]
			public DateType YearType
			{
				get { return DateType; }
				set { DateType = value; }
			}

			[PGDisplayName("Dasa Compression")]
			public double Compression
			{
				get { return _Compression; }
				set 
				{
					if (value >= 0.0)
						_Compression = value; 
				}
			}
			[PGDisplayName("Year Length")]
			public double YearLength
			{
				get { return _YearLength; }
				set 
				{ 
					if (value >= 0.0)
						_YearLength = value; 
				}
			}
			[PGDisplayName("Offset Dates by Days")]
			public double OffsetDays
			{
				get { return this._OffsetDays; }
				set { this._OffsetDays = value; }
			}
			[PGDisplayName("Offset Dates by Hours")]
			public double OffsetHours
			{
				get { return _OffsetHours; }
				set { _OffsetHours = value; }
			}			
			[PGDisplayName("Offset Dates by Minutes")]
			public double OffsetMinutes
			{
				get { return _OffsetMins; }
				set { _OffsetMins = value; }
			}
			public object Clone ()
			{
				Options o = new Options();
				o.YearLength = YearLength;
				o.DateType = DateType;
				o.Compression = Compression;
				o.OffsetDays = OffsetDays;
				o.OffsetHours = OffsetHours;
				o.OffsetMinutes = OffsetMinutes;
				return o;
			}
			public void Copy (Dasa.Options o)
			{
				this.YearLength = o.YearLength;
				this.DateType = o.DateType;
				this.Compression = o.Compression;
				this.OffsetDays = o.OffsetDays;
				this.OffsetHours = o.OffsetHours;
				this.OffsetMinutes = o.OffsetMinutes;
			}
		}

		public string EntryDescription (DasaEntry de, Moment start, Moment end)
		{
			return "";
		}
	}
}
