

using System;
using System.Diagnostics;

namespace org.transliteral.panchang
{
	public delegate Longitude ReturnLon (double ut, ref bool dirForward);
	/// <summary>
	/// Summary description for Transits.
	/// </summary>
	/// 

	public class Transit
	{
		private Horoscope h;
		private Body.Name b;


		public Longitude LongitudeOfSun (double ut, ref bool bDirRetro)
		{
			BodyPosition bp = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_SUN, Body.Name.Sun, BodyType.Name.Graha, this.h);
			if (bp.SpeedLongitude >= 0) bDirRetro = false;
			else bDirRetro = true;
			return bp.Longitude;
		}
		public Longitude GenericLongitude (double ut, ref bool bDirRetro)
		{

			if (b == Body.Name.Lagna)
				return new Longitude(Sweph.swe_lagna(ut));

			BodyPosition bp = Basics.CalculateSingleBodyPosition (ut, 
				Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
			if (bp.SpeedLongitude >= 0) bDirRetro = false;
			else bDirRetro = true;
			return bp.Longitude;
		}		
		public Longitude LongitudeOfTithiDir (double ut, ref bool bDirRetro)
		{
			bDirRetro = false;
			return LongitudeOfTithi (ut);
		}
		public Longitude LongitudeOfTithi (double ut)
		{
			BodyPosition bp_sun = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_SUN, Body.Name.Sun, BodyType.Name.Graha, this.h);
			BodyPosition bp_moon = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_MOON, Body.Name.Moon, BodyType.Name.Graha, this.h);
			Longitude rel = bp_moon.Longitude.Subtract ( bp_sun.Longitude);
			return rel;
		}
		public Longitude LongitudeOfMoonDir (double ut, ref bool bDirRetro)
		{
			bDirRetro = false;
			return LongitudeOfMoon (ut);
		}
		public Longitude LongitudeOfMoon (double ut)
		{
			BodyPosition bp_moon = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_MOON, Body.Name.Moon, BodyType.Name.Graha, this.h);
			return bp_moon.Longitude.Add(0);
		}
		public Longitude LongitudeOfSunMoonYogaDir (double ut, ref bool bDirRetro)
		{
			bDirRetro = false;
			return LongitudeOfSunMoonYoga (ut);
		}
		public Longitude LongitudeOfSunMoonYoga (double ut)
		{
			BodyPosition bp_sun = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_SUN, Body.Name.Sun, BodyType.Name.Graha, this.h);
			BodyPosition bp_moon = Basics.CalculateSingleBodyPosition (ut, Sweph.SE_MOON, Body.Name.Moon, BodyType.Name.Graha, this.h);
			Longitude rel = bp_moon.Longitude.Add ( bp_sun.Longitude);
			return rel;
		}
		public bool CircularLonLessThan (Longitude a, Longitude b)
		{
			return Transit.CircLonLessThan(a, b);
		}
		public static bool CircLonLessThan (Longitude a, Longitude b)
		{
			double bounds = 40.0;

			if (a.Value > 360.0 - bounds && b.Value < bounds)
				return true;

			if (a.Value < bounds && b.Value > 360.0 - bounds)
				return false;

			return (a.Value < b.Value);
		}
		public double LinearSearch (double approx_ut, Longitude lon_to_find, ReturnLon func)
		{
			double day_start = LinearSearchApprox (approx_ut, lon_to_find, func);
			double day_found = LinearSearchBinary (day_start, day_start + 1.0, lon_to_find, func);
			return day_found;
		}
		public double LinearSearchBinary (double ut_start, double ut_end, Longitude lon_to_find, ReturnLon func)
		{
			bool bDiscard = true;
			if (Math.Abs (ut_end - ut_start) < (1.0 / (24.0 * 60.0 * 60.0 * 60.0)))
			{
				if (Transit.CircLonLessThan(func(ut_start, ref bDiscard), lon_to_find))
					return ut_end;
				return ut_start;
			}

			double ut_middle = (ut_start + ut_end) / 2.0;
			Longitude lon = func (ut_middle, ref bDiscard);
			if (this.CircularLonLessThan (lon, lon_to_find)) 
				return LinearSearchBinary (ut_middle, ut_end, lon_to_find, func);
			else
				return LinearSearchBinary (ut_start, ut_middle, lon_to_find, func);			
		}

		public double NonLinearSearch (double ut, Body.Name b, Longitude lon_to_find, ReturnLon func)
		{
			bool rDir_start = false;
			bool rDir_end = false;
			bool bDayFound = false;
			ut -= 1.0;
			do 
			{
				ut += 1.0;
				Longitude l_start = func (ut, ref rDir_start);
				Longitude l_end = func (ut+1.0, ref rDir_end);
				if (this.CircularLonLessThan(l_start, lon_to_find) &&
					this.CircularLonLessThan(lon_to_find, l_end)) 
				{
					bDayFound = true;
				}
			} while (bDayFound == false);

			if (rDir_start == false && rDir_end == false)
			{
				LinearSearchBinary (ut, ut+1.0, lon_to_find, new ReturnLon(this.LongitudeOfSun));
			}

			return ut;
		}
		public double LinearSearchApprox (double approx_ut, Longitude lon_to_find, ReturnLon func)
		{
			bool bDiscard = true;
			double ut = Math.Floor(approx_ut);
			Longitude lon = func (ut, ref bDiscard);
			
			if (this.CircularLonLessThan (lon, lon_to_find) )
			{
				while (this.CircularLonLessThan(lon, lon_to_find)) 
				{
					ut += 1.0;
					lon = func (ut, ref bDiscard);
				}
				ut -= 1.0;
			}
			else
			{
				while (! this.CircularLonLessThan (lon, lon_to_find))
				{
					ut -= 1.0;
					lon = func (ut, ref bDiscard);
				}
			}
			Longitude l = func (ut, ref bDiscard);
			return ut;
		}
		public Transit(Horoscope _h)
		{
			h = _h;
			b = Body.Name.Other;
		}
		public Transit(Horoscope _h, Body.Name _b)
		{
			h = _h;
			b = _b;
		}
	}


}
