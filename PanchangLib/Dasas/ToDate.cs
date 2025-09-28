

using System;
using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class ToDate
    {

        private readonly double baseUT;
        private readonly double yearLength;
        private readonly double compression;
        private double offset;
        private readonly double spos;
        private readonly double mpos;
        private readonly DateType type;
        readonly Horoscope horoscope;

        public ToDate(double _baseUT, double _yearLength, double _compression, Horoscope _horoscope)
        {
            baseUT = _baseUT;
            yearLength = _yearLength;
            type = DateType.FixedYear;
            compression = _compression;
            horoscope = _horoscope;
        }
        public ToDate(double _baseUT, DateType _type, double _yearLength, double _compression, Horoscope _horoscope)
        {
            baseUT = _baseUT;
            type = _type;
            yearLength = _yearLength;
            compression = _compression;
            horoscope = _horoscope;
            spos = horoscope.GetPosition(BodyName.Sun).Longitude.Value;
            mpos = horoscope.GetPosition(BodyName.Moon).Longitude.Value;
        }
        public void SetOffset(double _offset)
        {
            this.offset = _offset;
        }
        public Moment AddPraveshYears(double years,ReturnLon returnLonFunc, int numMonths, int numDays)
        {
            double jd = 0.0;
            int year = 0, month = 0, day = 0;
            int hour = 0, minute = 0, second = 0;
            double dhour = 0, lon = 0;
            double soff = 0;
            double _years = 0;
            double tYears = 0;
            double tMonths = 0;
            double tDays = 0;
            double jd_st = 0;
            bool bDiscard = true;
            Transit t = null;
            Longitude l = null;

            Debug.Assert(years >= 0, "pravesh years only work in the future");
            t = new Transit(horoscope);
            soff = horoscope.GetPosition(BodyName.Sun).Longitude.ToZodiacHouseOffset();
            _years = years;
            tYears = 0;
            tMonths = 0;
            tDays = 0;
            tYears = Math.Floor(_years);
            _years = (_years - Math.Floor(_years)) * numMonths;
            tMonths = Math.Floor(_years);
            _years = (_years - Math.Floor(_years)) * numDays;
            tDays = _years;

            Logger.Info(String.Format("Searching for {0} {1} {2}", tYears, tMonths, tDays));
            lon = spos - soff;
            l = new Longitude(lon);
            jd = t.LinearSearch(horoscope.baseUT + (tYears * 365.2425), l, new ReturnLon(t.LongitudeOfSun));
            double yoga_start = returnLonFunc(jd, ref bDiscard).Value;
            double yoga_end = returnLonFunc(horoscope.baseUT, ref bDiscard).Value;
            jd_st = jd + (yoga_end - yoga_start) / 360.0 * 28.0;
            if (yoga_end < yoga_start) jd_st += 28.0;
            l = new Longitude(yoga_end);
            jd = t.LinearSearch(jd_st, new Longitude(yoga_end), returnLonFunc);
            for (int i = 1; (double)i <= tMonths; i++)
            {
                jd = t.LinearSearch(jd + 30.0, new Longitude(yoga_end), returnLonFunc);
            }

            l = l.Add(new Longitude(tDays * (360.0 / (double)numDays)));
            jd_st = jd + tDays; // * 25.0/30.0;
            jd = t.LinearSearch(jd_st, l, returnLonFunc);
            jd += (horoscope.Info.tz.toDouble() / 24.0);
            jd += offset;

            Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
            Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
            return new Moment(year, month, day, hour, minute, second);
        }
        public Moment AddYears(double years)
        {
            double jd = 0.0;
            int year = 0, month = 0, day = 0;
            int hour = 0, minute = 0, second = 0;
            double dhour = 0, lon = 0;
            double new_baseut = 0.0;
            Transit t = null;
            Longitude l = null;
            if (compression > 0.0)
                years *= compression;
            switch (type)
            {
                case DateType.FixedYear:
                    Logger.Info(String.Format("Finding {0} fixed years of length {1}", years, yearLength));
                    jd = baseUT + (years * yearLength);
                    Logger.Info(String.Format("tz = {0}", (horoscope.Info.tz.toDouble()) / 24.0));
                    jd += offset;
                    Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
                    Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
                    return new Moment(year, month, day, hour, minute, second);
                case DateType.SolarYear:
                    // Turn into years of 360 degrees, and then search
                    years = years * yearLength / 360.0;
                    t = new Transit(horoscope);
                    if (years >= 0) lon = (years - Math.Floor(years)) * 360.0;
                    else lon = (years - Math.Ceiling(years)) * 360.0;
                    l = new Longitude(lon + spos);
                    jd = t.LinearSearch(horoscope.baseUT + years * 365.2425, l, new ReturnLon(t.LongitudeOfSun));
                    jd += (horoscope.Info.tz.toDouble() / 24.0);
                    jd += offset;
                    Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
                    Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
                    return new Moment(year, month, day, hour, minute, second);
                case DateType.TithiPraveshYear:
                    t = new Transit(horoscope);
                    return this.AddPraveshYears(years, new ReturnLon(t.LongitudeOfTithiDir), 13, 30);
                case DateType.KaranaPraveshYear:
                    t = new Transit(horoscope);
                    return this.AddPraveshYears(years, new ReturnLon(t.LongitudeOfTithiDir), 13, 60);
                case DateType.YogaPraveshYear:
                    t = new Transit(horoscope);
                    return this.AddPraveshYears(years, new ReturnLon(t.LongitudeOfSunMoonYogaDir), 15, 27);
                case DateType.NakshatraPraveshYear:
                    t = new Transit(horoscope);
                    return this.AddPraveshYears(years, new ReturnLon(t.LongitudeOfMoonDir), 13, 27);
                case DateType.TithiYear:
                    jd -= (horoscope.Info.tz.toDouble() / 24.0);
                    t = new Transit(horoscope);
                    jd = horoscope.baseUT;
                    Longitude tithi_base = new Longitude(mpos - spos);
                    double days = years * yearLength;
                    Logger.Info(String.Format("Find {0} tithi days", days));
                    while (days >= 30 * 12.0)
                    {
                        jd = t.LinearSearch(jd + 29.52916 * 12.0, tithi_base, new ReturnLon(t.LongitudeOfTithiDir));
                        days -= 30 * 12.0;
                    }
                    tithi_base = tithi_base.Add(new Longitude(days * 12.0));
                    //Logger.Info(String.Format("Searching from {0} for {1}", t.LongitudeOfTithiDir(jd+days*28.0/30.0), tithi_base));
                    jd = t.LinearSearch(jd + days * 28.0 / 30.0, tithi_base, new ReturnLon(t.LongitudeOfTithiDir));
                    jd += (horoscope.Info.tz.toDouble() / 24.0);
                    jd += offset;
                    Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
                    Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
                    return new Moment(year, month, day, hour, minute, second);
                case DateType.YogaYear:
                    jd -= (horoscope.Info.tz.toDouble() / 24.0);
                    t = new Transit(horoscope);
                    jd = horoscope.baseUT;
                    Longitude yoga_base = new Longitude(mpos + spos);
                    double yogaDays = years * yearLength;
                    Logger.Info(String.Format("Find {0} yoga days", yogaDays));
                    while (yogaDays >= 27 * 12)
                    {
                        jd = t.LinearSearch(jd + 305, yoga_base, new ReturnLon(t.LongitudeOfSunMoonYogaDir));
                        yogaDays -= 27 * 12;
                    }
                    yoga_base = yoga_base.Add(new Longitude(yogaDays * (360.0 / 27.0)));
                    jd = t.LinearSearch(jd + yogaDays * 28.0 / 30.0, yoga_base, new ReturnLon(t.LongitudeOfSunMoonYogaDir));
                    jd += (horoscope.Info.tz.toDouble() / 24.0);
                    jd += offset;
                    Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
                    Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
                    return new Moment(year, month, day, hour, minute, second);
                default:
                    //years = years * yearLength;
                    t = new Transit(horoscope);
                    if (years >= 0) lon = (years - Math.Floor(years)) * 4320;
                    else lon = (years - Math.Ceiling(years)) * 4320;
                    lon *= (yearLength / 360.0);
                    new_baseut = horoscope.baseUT;
                    Longitude tithi = t.LongitudeOfTithi(new_baseut);
                    l = tithi.Add(new Longitude(lon));
                    Logger.Info(String.Format("{0} {1} {2}", 354.35, 354.35 * yearLength / 360.0, yearLength));
                    double tyear_approx = 354.35 * yearLength / 360.0; /*357.93765*/
                    double lapp = t.LongitudeOfTithi(new_baseut + (years * tyear_approx)).Value;
                    jd = t.LinearSearch(new_baseut + (years * tyear_approx), l, new ReturnLon(t.LongitudeOfTithiDir));
                    jd += offset;
                    //jd += (h.info.tz.toDouble() / 24.0);
                    Sweph.SWE_ReverseJulianDay(jd, ref year, ref month, ref day, ref dhour);
                    Moment.DoubleToHMS(dhour, ref hour, ref minute, ref second);
                    return new Moment(year, month, day, hour, minute, second);

            }
        }
    }
}
