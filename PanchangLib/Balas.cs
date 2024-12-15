

using System;
using System.Diagnostics;

namespace org.transliteral.panchang
{
	/// <summary>
	/// Summary description for Balas.
	/// </summary>
	public class ShadBalas
	{
        private Horoscope horoscope;
        public ShadBalas(Horoscope _horoscope)
		{
			horoscope = _horoscope;
		}
		private void VerifyGraha (Body.Name bodyName)
		{
			int _b = (int)bodyName;
			Debug.Assert(_b >= (int)Body.Name.Sun && _b <= (int)Body.Name.Saturn);
		}
		public double UcchaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			Longitude debLon = Body.DebilitationDegree (bodyName);
			Longitude posLon = horoscope.GetPosition(bodyName).Longitude;
			double diff = posLon.Subtract(debLon).Value;
			if (diff > 180) diff = 360 - diff;
			return ((diff / 180.0) * 60.0);
		}
		public bool GetsOjaBala (Body.Name bodyName)
		{
			switch (bodyName)
			{
				case Body.Name.Moon:
				case Body.Name.Venus:
					return false;
				default:
					return true;
			}
		}
		public double OjaYugmaHelper (Body.Name bodyName, ZodiacHouse zodiacHouse)
		{
			if (this.GetsOjaBala(bodyName))
			{
				if (zodiacHouse.IsOdd())	return 15.0;
				else return 0.0;
			}
			else 
			{
				if (zodiacHouse.IsOdd()) return 0.0;
				else return 15.0;
			}
		}
		public double OjaYugmaRasyAmsaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			BodyPosition bp = horoscope.GetPosition(bodyName);
			ZodiacHouse zh_rasi = bp.ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			ZodiacHouse zh_amsa = bp.ToDivisionPosition(new Division(DivisionType.Navamsa)).ZodiacHouse;
			double s = 0;
			s += this.OjaYugmaHelper(bodyName, zh_rasi);
			s += this.OjaYugmaHelper(bodyName, zh_amsa);
			return s;
		}
		public double KendraBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			ZodiacHouse zh_b = horoscope.GetPosition(bodyName).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			ZodiacHouse zh_l = horoscope.GetPosition(Body.Name.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			int diff = zh_l.NumHousesBetween(zh_b);
			switch (diff % 3)
			{
				case 1: return 60;
				case 2: return 30.0;
				case 0: default:
						return 15.0;
			}
		}
		public double DrekkanaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			int part = horoscope.GetPosition(bodyName).PartOfZodiacHouse(3);
			if (part == 1 &&
				(bodyName == Body.Name.Sun || bodyName == Body.Name.Jupiter || bodyName == Body.Name.Mars))
				return 15.0;

			if (part == 2 && 
				(bodyName == Body.Name.Saturn || bodyName == Body.Name.Mercury))
				return 15.0;

			if (part == 3 &&
				(bodyName == Body.Name.Moon || bodyName == Body.Name.Venus))
				return 15.0;

			return 0;
		}
		public double DigBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			int[] powerlessHouse = new int[] { 4, 10, 4, 7, 7, 10, 1 };
			Longitude lagLon = horoscope.GetPosition(Body.Name.Lagna).Longitude;
			Longitude debLon = new Longitude(lagLon.ToZodiacHouseBase());
			debLon = debLon.Add(powerlessHouse[(int)bodyName]*30.0+15.0);
			Longitude posLon = horoscope.GetPosition(bodyName).Longitude;

			Logger.Debug(String.Format("digBala {0} {1} {2}", bodyName, posLon.Value, debLon.Value));

			double diff = posLon.Subtract(debLon).Value;
			if (diff > 180) diff = 360 - diff;
			return ((diff / 180.0) * 60.0);
		}
		public double NathonnathaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);

			if (bodyName == Body.Name.Mercury) return 60;

			double lmt_midnight = horoscope.lmt_offset * 24.0;
			double lmt_noon = 12.0 + horoscope.lmt_offset * 24.0;
			double diff = 0;
			if (horoscope.Info.tob.Time  > lmt_noon)
				diff = lmt_midnight - horoscope.Info.tob.Time;
			else
				diff = horoscope.Info.tob.Time - lmt_midnight;

			while (diff < 0) diff += 12.0;
			diff = diff / 12.0 * 60.0;

			if (bodyName == Body.Name.Moon || bodyName == Body.Name.Mars ||
				bodyName == Body.Name.Saturn)
				diff = 60-diff;

			return diff;
		}
		public double PakshaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);

			Longitude mlon = horoscope.GetPosition(Body.Name.Moon).Longitude;
			Longitude slon = horoscope.GetPosition(Body.Name.Sun).Longitude;

			double diff = mlon.Subtract(slon).Value;
			if (diff > 180) diff = 360.0 - diff;
			double shubha = diff / 3.0;
			double paapa = 60.0 - shubha;

			switch (bodyName)
			{
				case Body.Name.Sun:
				case Body.Name.Mars:
				case Body.Name.Saturn:
					return paapa;
				case Body.Name.Moon:
					return shubha *2.0;
				default:
				case Body.Name.Mercury:
				case Body.Name.Jupiter:
				case Body.Name.Venus:
					return shubha;
			}
		}
		public double TribhaagaBala (Body.Name bodyName)
		{
			Body.Name ret = Body.Name.Jupiter;
			this.VerifyGraha(bodyName);
			if (horoscope.IsDayBirth())
			{
				double length = (horoscope.Sunset - horoscope.Sunrise)/3;
				double offset = horoscope.Info.tob.Time - horoscope.Sunrise;
				int part = (int)(Math.Floor(offset/length));
				switch (part)
				{
					case 0: ret = Body.Name.Mercury; break;
					case 1: ret = Body.Name.Sun; break;
					case 2: ret = Body.Name.Saturn; break;
				}
			}
			else
			{
				double length = (horoscope.NextSunrise + 24.0 - horoscope.Sunset)/3;
				double offset = horoscope.Info.tob.Time - horoscope.Sunset;
				if (offset < 0) offset += 24;
				int part = (int)(Math.Floor(offset/length));
				switch (part)
				{
					case 0: ret = Body.Name.Moon; break;
					case 1: ret = Body.Name.Venus; break;
					case 2: ret = Body.Name.Mars; break;
				}
			}
			if (bodyName == Body.Name.Jupiter || bodyName == ret)
				return 60;
			return 0;
		}
		public double naisargikaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			switch (bodyName)
			{
				case Body.Name.Sun: return 60;
				case Body.Name.Moon: return 51.43;
				case Body.Name.Mars: return 17.14;
				case Body.Name.Mercury: return 25.70;
				case Body.Name.Jupiter: return 34.28;
				case Body.Name.Venus: return 42.85;
				case Body.Name.Saturn: return 8.57;
			}
			return 0;
		}
		public void kalaHelper (ref Body.Name yearLord, ref Body.Name monthLord)
		{
			double ut_arghana = Sweph.swe_julday(1827, 5, 2, - horoscope.Info.tz.toDouble() + 12.0/24.0);
			double ut_noon = horoscope.baseUT - horoscope.Info.tob.Time/24.0 + 12.0/24.0;

			double diff = ut_noon - ut_arghana;
			if (diff >= 0)
			{
				double quo = Math.Floor(diff/360.0);
				diff -= quo * 360.0;
			}
			else 
			{
				double pdiff = -diff;
				double quo = Math.Ceiling(pdiff/360.0);
				diff += quo * 360.0;
			}

			double diff_year = diff;
			while (diff > 30.0) diff -= 30.0;
			double diff_month = diff;
			while (diff > 7) diff -= 7.0;

			yearLord = Basics.WeekdayRuler((Basics.Weekday)Sweph.swe_day_of_week(ut_noon - diff_year));
			monthLord = Basics.WeekdayRuler((Basics.Weekday)Sweph.swe_day_of_week(ut_noon - diff_month));
		}
		public double abdaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			Body.Name yearLord=Body.Name.Sun, monthLord=Body.Name.Sun;
			this.kalaHelper(ref yearLord, ref monthLord);
			if (yearLord == bodyName) return 15.0;
			return 0.0;
		}
		public double masaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			Body.Name yearLord=Body.Name.Sun, monthLord=Body.Name.Sun;
			this.kalaHelper(ref yearLord, ref monthLord);
			if (monthLord == bodyName) return 30.0;
			return 0.0;
		}
		public double varaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			if (Basics.WeekdayRuler(horoscope.Weekday) == bodyName) return 45.0;
			return 0.0;
		}
		public double horaBala (Body.Name bodyName)
		{
			this.VerifyGraha(bodyName);
			if (horoscope.CalculateHora() == bodyName) return 60.0;
			return 0.0;
		}
	}
}
