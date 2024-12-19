

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
		private void VerifyGraha (BodyName BodyName)
		{
			int _b = (int)BodyName;
			Debug.Assert(_b >= (int)BodyName.Sun && _b <= (int)BodyName.Saturn);
		}
		public double UcchaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			Longitude debLon = Body.DebilitationDegree (BodyName);
			Longitude posLon = horoscope.GetPosition(BodyName).Longitude;
			double diff = posLon.Subtract(debLon).Value;
			if (diff > 180) diff = 360 - diff;
			return ((diff / 180.0) * 60.0);
		}
		public bool GetsOjaBala (BodyName BodyName)
		{
			switch (BodyName)
			{
				case BodyName.Moon:
				case BodyName.Venus:
					return false;
				default:
					return true;
			}
		}
		public double OjaYugmaHelper (BodyName BodyName, ZodiacHouse zodiacHouse)
		{
			if (this.GetsOjaBala(BodyName))
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
		public double OjaYugmaRasyAmsaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			BodyPosition bp = horoscope.GetPosition(BodyName);
			ZodiacHouse zh_rasi = bp.ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			ZodiacHouse zh_amsa = bp.ToDivisionPosition(new Division(DivisionType.Navamsa)).ZodiacHouse;
			double s = 0;
			s += this.OjaYugmaHelper(BodyName, zh_rasi);
			s += this.OjaYugmaHelper(BodyName, zh_amsa);
			return s;
		}
		public double KendraBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			ZodiacHouse zh_b = horoscope.GetPosition(BodyName).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			ZodiacHouse zh_l = horoscope.GetPosition(BodyName.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
			int diff = zh_l.NumHousesBetween(zh_b);
			switch (diff % 3)
			{
				case 1: return 60;
				case 2: return 30.0;
				case 0: default:
						return 15.0;
			}
		}
		public double DrekkanaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			int part = horoscope.GetPosition(BodyName).PartOfZodiacHouse(3);
			if (part == 1 &&
				(BodyName == BodyName.Sun || BodyName == BodyName.Jupiter || BodyName == BodyName.Mars))
				return 15.0;

			if (part == 2 && 
				(BodyName == BodyName.Saturn || BodyName == BodyName.Mercury))
				return 15.0;

			if (part == 3 &&
				(BodyName == BodyName.Moon || BodyName == BodyName.Venus))
				return 15.0;

			return 0;
		}
		public double DigBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			int[] powerlessHouse = new int[] { 4, 10, 4, 7, 7, 10, 1 };
			Longitude lagLon = horoscope.GetPosition(BodyName.Lagna).Longitude;
			Longitude debLon = new Longitude(lagLon.ToZodiacHouseBase());
			debLon = debLon.Add(powerlessHouse[(int)BodyName]*30.0+15.0);
			Longitude posLon = horoscope.GetPosition(BodyName).Longitude;

			Logger.Debug(String.Format("digBala {0} {1} {2}", BodyName, posLon.Value, debLon.Value));

			double diff = posLon.Subtract(debLon).Value;
			if (diff > 180) diff = 360 - diff;
			return ((diff / 180.0) * 60.0);
		}
		public double NathonnathaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);

			if (BodyName == BodyName.Mercury) return 60;

			double lmt_midnight = horoscope.lmt_offset * 24.0;
			double lmt_noon = 12.0 + horoscope.lmt_offset * 24.0;
			double diff = 0;
			if (horoscope.Info.tob.Time  > lmt_noon)
				diff = lmt_midnight - horoscope.Info.tob.Time;
			else
				diff = horoscope.Info.tob.Time - lmt_midnight;

			while (diff < 0) diff += 12.0;
			diff = diff / 12.0 * 60.0;

			if (BodyName == BodyName.Moon || BodyName == BodyName.Mars ||
				BodyName == BodyName.Saturn)
				diff = 60-diff;

			return diff;
		}
		public double PakshaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);

			Longitude mlon = horoscope.GetPosition(BodyName.Moon).Longitude;
			Longitude slon = horoscope.GetPosition(BodyName.Sun).Longitude;

			double diff = mlon.Subtract(slon).Value;
			if (diff > 180) diff = 360.0 - diff;
			double shubha = diff / 3.0;
			double paapa = 60.0 - shubha;

			switch (BodyName)
			{
				case BodyName.Sun:
				case BodyName.Mars:
				case BodyName.Saturn:
					return paapa;
				case BodyName.Moon:
					return shubha *2.0;
				default:
				case BodyName.Mercury:
				case BodyName.Jupiter:
				case BodyName.Venus:
					return shubha;
			}
		}
		public double TribhaagaBala (BodyName BodyName)
		{
			BodyName ret = BodyName.Jupiter;
			this.VerifyGraha(BodyName);
			if (horoscope.IsDayBirth())
			{
				double length = (horoscope.Sunset - horoscope.Sunrise)/3;
				double offset = horoscope.Info.tob.Time - horoscope.Sunrise;
				int part = (int)(Math.Floor(offset/length));
				switch (part)
				{
					case 0: ret = BodyName.Mercury; break;
					case 1: ret = BodyName.Sun; break;
					case 2: ret = BodyName.Saturn; break;
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
					case 0: ret = BodyName.Moon; break;
					case 1: ret = BodyName.Venus; break;
					case 2: ret = BodyName.Mars; break;
				}
			}
			if (BodyName == BodyName.Jupiter || BodyName == ret)
				return 60;
			return 0;
		}
		public double naisargikaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			switch (BodyName)
			{
				case BodyName.Sun: return 60;
				case BodyName.Moon: return 51.43;
				case BodyName.Mars: return 17.14;
				case BodyName.Mercury: return 25.70;
				case BodyName.Jupiter: return 34.28;
				case BodyName.Venus: return 42.85;
				case BodyName.Saturn: return 8.57;
			}
			return 0;
		}
		public void kalaHelper (ref BodyName yearLord, ref BodyName monthLord)
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

			yearLord = Basics.WeekdayRuler((Weekday)Sweph.swe_day_of_week(ut_noon - diff_year));
			monthLord = Basics.WeekdayRuler((Weekday)Sweph.swe_day_of_week(ut_noon - diff_month));
		}
		public double abdaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			BodyName yearLord=BodyName.Sun, monthLord=BodyName.Sun;
			this.kalaHelper(ref yearLord, ref monthLord);
			if (yearLord == BodyName) return 15.0;
			return 0.0;
		}
		public double masaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			BodyName yearLord=BodyName.Sun, monthLord=BodyName.Sun;
			this.kalaHelper(ref yearLord, ref monthLord);
			if (monthLord == BodyName) return 30.0;
			return 0.0;
		}
		public double varaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			if (Basics.WeekdayRuler(horoscope.Weekday) == BodyName) return 45.0;
			return 0.0;
		}
		public double horaBala (BodyName BodyName)
		{
			this.VerifyGraha(BodyName);
			if (horoscope.CalculateHora() == BodyName) return 60.0;
			return 0.0;
		}
	}
}
