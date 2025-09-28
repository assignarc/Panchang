

using System;

namespace org.transliteral.panchang
{
    public abstract class Dasa
	{
		public static int NarayanaDasaLength (ZodiacHouse zh, DivisionPosition dp) 
		{
			int length=0;

			if (zh.IsOddFooted())
				length = zh.NumHousesBetween(dp.ZodiacHouse);
			else
				length = zh.NumHousesBetweenReverse(dp.ZodiacHouse);
		
			if (dp.IsExaltedPhalita())
				length++;
			else if (dp.IsDebilitatedPhalita())
				length--;

			length = Basics.NormalizeInclusive(1, 12, length-1);
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
			[Visible("Year Type")]
			public DateType YearType
			{
				get { return DateType; }
				set { DateType = value; }
			}

			[Visible("Dasa Compression")]
			public double Compression
			{
				get { return _Compression; }
				set 
				{
					if (value >= 0.0)
						_Compression = value; 
				}
			}
			[Visible("Year Length")]
			public double YearLength
			{
				get { return _YearLength; }
				set 
				{ 
					if (value >= 0.0)
						_YearLength = value; 
				}
			}
			[Visible("Offset Dates by Days")]
			public double OffsetDays
			{
				get { return this._OffsetDays; }
				set { this._OffsetDays = value; }
			}
			[Visible("Offset Dates by Hours")]
			public double OffsetHours
			{
				get { return _OffsetHours; }
				set { _OffsetHours = value; }
			}			
			[Visible("Offset Dates by Minutes")]
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
			public void Copy (Options o)
			{
				this.YearLength = o.YearLength;
				this.DateType = o.DateType;
				this.Compression = o.Compression;
				this.OffsetDays = o.OffsetDays;
				this.OffsetHours = o.OffsetHours;
				this.OffsetMinutes = o.OffsetMinutes;
			}
		}

        public string EntryDescription(DasaEntry de, Moment start, Moment end) => "";
    }
}
