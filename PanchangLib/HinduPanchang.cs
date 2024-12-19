using org.transliteral.panchang.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static org.transliteral.panchang.Dasa;

namespace org.transliteral.panchang
{
    public class HinduPanchang
    {


        List<PanchangaLocalMoments> locals = new List<PanchangaLocalMoments>();
        PanchangaGlobalMoments globals = new PanchangaGlobalMoments();
        protected Horoscope horoscope;


        public int numDays =3;
        public bool bCalculateLagnaCusps = false;
        public bool bCalculateTithiCusps = true;
        public bool bCalculateKaranaCusps = true;
        public bool bCalculateNakshatraCusps = true;
        public bool bCalculateHoraCusps = true;
        public bool bCalculateSMYogaCusps = true;
        public bool bCalculateKalaCusps = true;
        public bool bCalcSpecialKalas = true;
        public bool bShowSunriset = true;

        public bool bLargeHours = false;
        public bool bShowUpdates = true;
        public bool bOneEntryPerLine = false;


        int[] rahu_kalas = new int[] { 7, 1, 6, 4, 5, 3, 2 };
        int[] gulika_kalas = new int[] { 6, 5, 4, 3, 2, 1, 0 };
        int[] yama_kalas = new int[] { 4, 3, 2, 1, 0, 6, 5 };

        public HinduPanchang()
        {
            GlobalOptions globalOptions = new GlobalOptions();
            GlobalOptions.Instance = globalOptions;
            globalOptions.HOptions.EphemerisPath = "D:\\KHAPRE_DATA\\Code\\CalendarCode\\SwissEphemeris\\eph";
            HoroscopeOptions horaOptions = new HoroscopeOptions()
            {
                Ayanamsa = AyanamsaType.Lahiri,
                SunrisePosition = SunrisePositionType.ApparentDiscCenter,
                EphemerisPath = globalOptions.HOptions.EphemerisPath

            };
            StrengthOptions strengthOptions = new StrengthOptions();

           /* 
            * Roanoke
                HMSInfo mLat = new HMSInfo(33, 0, 18, Direction.NS);
                HMSInfo mLon = new HMSInfo(-97, 13, 35, Direction.EW);
                HMSInfo mTz = new HMSInfo(-5, 0, 0, Direction.EW);
           */
            /*   
             *   Seattle
               mLat = new HMSInfo(47, 40, 27, dir_type.NS);
               mLon = new HMSInfo(-122, 7, 13, dir_type.EW);
               mTz = new HMSInfo(-7, 0, 0, dir_type.EW);
           */


           
            HoraInfo horaInfo = new HoraInfo(
                                        atob: new Moment(), 
                                        alat: new HMSInfo(33, 0, 18, Direction.NorthSouth), 
                                        alon: new HMSInfo(-97, 13, 35, Direction.EastWest), 
                                        atz: new HMSInfo(-5, 0, 0, Direction.EastWest)
                                        );
            horoscope = new Horoscope(horaInfo, horaOptions);
            horoscope.StrengthOptions = strengthOptions;

        }

        public void Compute()
        {
            double ut_start = Math.Floor(horoscope.baseUT);
            double[] geopos = new double[]{ horoscope.Info.lon.toDouble(), horoscope.Info.lat.toDouble(), horoscope.Info.alt };

            this.globals = new PanchangaGlobalMoments();
            this.locals = new List<PanchangaLocalMoments>();

            for (int i = 0; i < numDays; i++)
            {
                ComputeEntry(ut_start, geopos);
                ut_start += 1;
            }
            Dictionary<string, string> results = this.DisplayEntry(locals[0]);

        }
        public void ComputeEntry(double ut, double[] geopos)
        {

            int year = 0, month = 0, day = 0;
            double sunset = 0, hour = 0;
            double sunrise = 0;
            double ut_sr = 0;



            Sweph.ObtainLock(horoscope);
            horoscope.PopulateSunrisetCacheHelper(ut - 0.5, ref sunrise, ref sunset, ref ut_sr);
            Sweph.ReleaseLock(horoscope);

            Sweph.swe_revjul(ut_sr, ref year, ref month, ref day, ref hour);
            Moment moment_sr = new Moment(year, month, day, hour);
            Moment moment_ut = new Moment(ut, horoscope);
            HoraInfo infoCurr = new HoraInfo(moment_ut, horoscope.Info.lat, horoscope.Info.lon, horoscope.Info.tz);
            Horoscope hCurr = new Horoscope(infoCurr, horoscope.Options);

           
            PanchangaLocalMoments local = new PanchangaLocalMoments();
            local.Sunrise = hCurr.Sunrise;
            local.Sunset = sunset;
            local.SunriseUT = ut_sr;
            Sweph.swe_revjul(ut, ref year, ref month, ref day, ref hour);
            local.WeekDay = (Basics.Weekday)Sweph.swe_day_of_week(ut);



            local.KalasUT = hCurr.GetKalaCuspsUt();
            if (bCalcSpecialKalas)
            {
                Body.Name bStart = Basics.WeekdayRuler(hCurr.Weekday);
                if (hCurr.Options.KalaType == EHoraType.Lmt)
                    bStart = Basics.WeekdayRuler(hCurr.WeekdayLMT);

                local.RahuKalaIndex = this.rahu_kalas[(int)bStart];
                local.GulikaKalaIndex = this.gulika_kalas[(int)bStart];
                local.YamaKalaIndex = this.yama_kalas[(int)bStart];
            }

            if (bCalculateLagnaCusps)
            {
                Sweph.ObtainLock(horoscope);
                BodyPosition bp_lagna_sr = Basics.CalculateSingleBodyPosition(ut_sr, Sweph.BodyNameToSweph(Body.Name.Lagna), Body.Name.Lagna, BodyType.Name.Lagna, horoscope);
                DivisionPosition dp_lagna_sr = bp_lagna_sr.ToDivisionPosition(new Division(DivisionType.Rasi));
                local.LagnaZodiacHouse = dp_lagna_sr.ZodiacHouse.Value;

                Longitude bp_lagna_base = new Longitude(bp_lagna_sr.Longitude.ToZodiacHouseBase());
                double ut_transit = ut_sr;
                for (int i = 1; i <= 12; i++)
                {
                    Retrogression r = new Retrogression(horoscope, Body.Name.Lagna);
                    ut_transit = r.GetLagnaTransitForward(ut_transit, bp_lagna_base.Add(i * 30.0));

                    PanchangaMomentInfo pmi = new PanchangaMomentInfo(
                        ut_transit, (int)bp_lagna_sr.Longitude.ToZodiacHouse().Add(i + 1).Value);
                    local.LagnasUT.Add(pmi);
                }

                Sweph.ReleaseLock(horoscope);
            }

            if (bCalculateTithiCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.ObtainLock(horoscope);
                Tithi tithi_start = t.LongitudeOfTithi(ut_sr).ToTithi();
                Tithi tithi_end = t.LongitudeOfTithi(ut_sr + 1.0).ToTithi();

                Tithi tithi_curr = tithi_start.Add(1);
                local.TithiIndexStart = globals.TithisUT.Count - 1;
                local.TithiIndexEnd = globals.TithisUT.Count - 1;

                while (tithi_start.Value != tithi_end.Value &&
                    tithi_curr.Value != tithi_end.Value)
                {
                    tithi_curr = tithi_curr.Add(2);
                    double dLonToFind = ((double)(int)tithi_curr.Value - 1) * (360.0 / 30.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.TithisUT.Add(new PanchangaMomentInfo(ut_found, (int)tithi_curr.Value));
                    local.TithiIndexEnd++;
                }
                Sweph.ReleaseLock(horoscope);
            }

            if (bCalculateKaranaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.ObtainLock(horoscope);
                Karana karana_start = t.LongitudeOfTithi(ut_sr).ToKarana();
                Karana karana_end = t.LongitudeOfTithi(ut_sr + 1.0).ToKarana();

                Karana karana_curr = karana_start.add(1);
                local.KaranaIndexStart = globals.KaranasUT.Count - 1;
                local.KaranaIndexEnd = globals.KaranasUT.Count - 1;

                while (karana_start.value != karana_end.value &&
                    karana_curr.value != karana_end.value)
                {
                    karana_curr = karana_curr.add(2);
                    double dLonToFind = ((double)(int)karana_curr.value - 1) * (360.0 / 60.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.KaranasUT.Add(new PanchangaMomentInfo(ut_found, (int)karana_curr.value));
                    local.KaranaIndexEnd++;
                }
                Sweph.ReleaseLock(horoscope);
            }

            if (bCalculateSMYogaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.ObtainLock(horoscope);
                SunMoonYoga sm_start = t.LongitudeOfSunMoonYoga(ut_sr).ToSunMoonYoga();
                SunMoonYoga sm_end = t.LongitudeOfSunMoonYoga(ut_sr + 1.0).ToSunMoonYoga();

                SunMoonYoga sm_curr = sm_start.Add(1);
                local.SunMoonYogaIndexStart = globals.SunMoonYogasUT.Count - 1;
                local.SunMoonYogaIndexEnd = globals.SunMoonYogasUT.Count - 1;

                while (sm_start.Value != sm_end.Value &&
                    sm_curr.Value != sm_end.Value)
                {
                    sm_curr = sm_curr.Add(2);
                    double dLonToFind = ((double)(int)sm_curr.Value - 1) * (360.0 / 27);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfSunMoonYogaDir));

                    globals.SunMoonYogasUT.Add(new PanchangaMomentInfo(ut_found, (int)sm_curr.Value));
                    local.SunMoonYogaIndexEnd++;
                }

                Sweph.ReleaseLock(horoscope);
            }

            if (bCalculateNakshatraCusps)
            {
                bool bDiscard = true;
                Transit t = new Transit(horoscope, Body.Name.Moon);
                Sweph.ObtainLock(horoscope);
                Nakshatra nak_start = t.GenericLongitude(ut_sr, ref bDiscard).ToNakshatra();
                Nakshatra nak_end = t.GenericLongitude(ut_sr + 1.0, ref bDiscard).ToNakshatra();

                local.NakshatraIndexStart = globals.NakshatrasUT.Count - 1;
                local.NakshatraIndexEnd = globals.NakshatrasUT.Count - 1;

                Nakshatra nak_curr = nak_start.Add(1);

                while (nak_start.Value != nak_end.Value &&
                    nak_curr.Value != nak_end.Value)
                {
                    nak_curr = nak_curr.Add(2);
                    double dLonToFind = ((double)((int)nak_curr.Value - 1)) * (360.0 / 27.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.GenericLongitude));

                    globals.NakshatrasUT.Add(new PanchangaMomentInfo(ut_found, (int)nak_curr.Value));
                    Logger.Info(String.Format("Found nakshatra {0}", nak_curr.Value));
                    local.NakshatraIndexEnd++;
                }
                Sweph.ReleaseLock(horoscope);
            }

            if (bCalculateHoraCusps)
            {
                local.HorasUT = hCurr.GetHoraCuspsUt();
                hCurr.CalculateHora(ut_sr + 1.0 / 24.0, ref local.HoraBase);
            }

            if (bCalculateKalaCusps)
            {
                hCurr.CalculateKala(ref local.KalaBase);
            }


            this.locals.Add(local);
            
        }

        private Dictionary<string, string> DisplayEntry(PanchangaLocalMoments local)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            PanchangDay pDay = new PanchangDay();
            string s;
            int day = 0, month = 0, year = 0;
            double time = 0;

            Sweph.swe_revjul(local.SunriseUT, ref year, ref month, ref day, ref time);
            Moment m = new Moment(year, month, day, time);


            //this.mList.Items.Add(string.Format("{0}, {1}", local.wday, m.ToDateString()));
           /* results.Add("LocalWeekday", local.wday.ToString());
            results.Add("Moment", m.ToDateString());*/
            pDay.LocalWeekday = local.WeekDay;
            pDay.Moment = m;
            if (this.bShowSunriset)
            {
                s = string.Format("Sunrise at {0}. Sunset at {1}",this.TimeToString(local.Sunrise),
                    this.TimeToString(local.Sunset));
                pDay.Celestials.Add(new CelestialBody() { Name = Body.Name.Sun, Rise = this.TimeToString(local.Sunrise), Set = this.TimeToString(local.Sunset) });
               /* results.Add("Sunrise", this.timeToString(local.sunrise));
                results.Add("Sunset", this.timeToString(local.sunset));*/
                //this.mList.Items.Add(s);
            }

            if (this.bCalcSpecialKalas)
            {

                pDay.Rahu = string.Format("Rahu Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.RahuKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.RahuKalaIndex + 1], horoscope).ToTimeString());
                pDay.Gulika = string.Format("Gulika Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.GulikaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.GulikaKalaIndex + 1], horoscope).ToTimeString());
                pDay.Yama = string.Format("Yama Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.YamaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.YamaKalaIndex + 1], horoscope).ToTimeString());

                
               /* results.Add("Rahu", s_rahu);
                results.Add("Gulika", s_gulika);
                results.Add("Yama", s_yama);*/

             /*   if (opts.OneEntryPerLine)
                {
                    this.mList.Items.Add(s_rahu);
                    this.mList.Items.Add(s_gulika);
                    this.mList.Items.Add(s_yama);
                }
                else
                    this.mList.Items.Add(string.Format("{0}. {1}. {2}.", s_rahu, s_gulika, s_yama));*/
            }

            if (this.bCalculateTithiCusps)
            {
                string s_tithi = "";

                if (local.TithiIndexStart == local.TithiIndexEnd &&
                    local.TithiIndexStart >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.TithisUT[local.TithiIndexStart];
                    Tithi t = new Tithi((TithiName)pmi.Info);

                    //this.mList.Items.Add(string.Format("{0} - full.", t.value));
                    //results.Add("Tithi", string.Format("{0} - full.", t.value));
                    pDay.Tithi = t;
                    pDay.TithiText = string.Format("{0} - full.", t.Value);
                }
                else
                {
                    for (int i = local.TithiIndexStart + 1; i <= local.TithiIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.TithisUT[i];
                        Tithi t = new Tithi((TithiName)pmi.Info).AddReverse(2);
                        s_tithi += string.Format("{0} until {1}",
                            t.Value,
                            this.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
                        pDay.Tithi = t;
                        /*if (this.opts.OneEntryPerLine)
                        {
                            this.mList.Items.Add(s_tithi);
                            s_tithi = "";
                        }
                        else*/
                        s_tithi += ". ";
                    }
                    /* if (false == opts.OneEntryPerLine)
                         this.mList.Items.Add(s_tithi);
                    results.Add("Tithi", s_tithi);*/
                    pDay.TithiText = s_tithi;
                }
            }


            if (this.bCalculateKaranaCusps)
            {
                string s_karana = "";

                if (local.KaranaIndexStart == local.KaranaIndexEnd &&
                    local.KaranaIndexStart >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.KaranasUT[local.KaranaIndexStart];
                    Karana k = new Karana((KaranaName)pmi.Info);
                    //this.mList.Items.Add(string.Format("{0} karana - full.", k.value));
                    //results.Add("Karana", string.Format("{0} karana - full.", k.value));
                    pDay.Karana = k;
                    pDay.KaranaText = string.Format("{0} karana - full.", k.value);
                }
                else
                {
                    for (int i = local.KaranaIndexStart + 1; i <= local.KaranaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.KaranasUT[i];
                        Karana k = new Karana((KaranaName)pmi.Info).addReverse(2);
                        s_karana += string.Format("{0} karana until {1}",
                            k.value,
                            this.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
/*
                        if (this.opts.OneEntryPerLine)
                        {
                            this.mList.Items.Add(s_karana);
                            s_karana = "";
                        }
                        else*/
                            s_karana += ". ";
                    }
                    /*if (false == opts.OneEntryPerLine)
                        this.mList.Items.Add(s_karana);
                    results.Add("Karana", s_karana);*/
                    pDay.KaranaText = s_karana;
                }
            }



            if (this.bCalculateSMYogaCusps)
            {
                string s_smyoga = "";

                if (local.SunMoonYogaIndexStart == local.SunMoonYogaIndexEnd &&
                    local.SunMoonYogaIndexStart >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.SunMoonYogasUT[local.SunMoonYogaIndexStart];
                    SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info);
                    //this.mList.Items.Add();
                    results.Add("Yoga", string.Format("{0} yoga - full.", sm.Value));
                }
                else
                {
                    for (int i = local.SunMoonYogaIndexStart + 1; i <= local.SunMoonYogaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.SunMoonYogasUT[i];
                        SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info).AddReverse(2);
                        s_smyoga += string.Format("{0} yoga until {1}",
                            sm.Value,
                            this.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));

                       /* if (this.opts.OneEntryPerLine)
                        {
                            this.mList.Items.Add(s_smyoga);
                            s_smyoga = "";
                        }
                        else*/
                            s_smyoga += ". ";
                    }
                    /* if (false == opts.OneEntryPerLine)
                         this.mList.Items.Add(s_smyoga);*/
                    results.Add("Yoga", s_smyoga);
                }
            }



            if (this.bCalculateNakshatraCusps)
            {
                string s_nak = "";

                if (local.NakshatraIndexStart == local.NakshatraIndexEnd &&
                    local.NakshatraIndexStart >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.NakshatrasUT[local.NakshatraIndexStart];
                    Nakshatra n = new Nakshatra((NakshatraName)pmi.Info);
                   // this.mList.Items.Add(string.Format("{0} - full.", n.value));
                    results.Add("Nakshatra", string.Format("{0} - full.", n.Value));
                }
                else
                {
                    for (int i = local.NakshatraIndexStart + 1; i <= local.NakshatraIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.NakshatrasUT[i];
                        Nakshatra n = new Nakshatra((NakshatraName)pmi.Info).AddReverse(2);
                        s_nak += string.Format("{0} until {1}",
                            n.Value,
                            this.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
                       /* if (this.opts.OneEntryPerLine)
                        {
                            this.mList.Items.Add(s_nak);
                            s_nak = "";
                        }
                        else*/
                            s_nak += ". ";
                    }
                    /*  if (false == opts.OneEntryPerLine)
                          this.mList.Items.Add(s_nak);*/
                    results.Add("Nakshatra", s_nak);
                }
            }

            if (this.bCalculateLagnaCusps)
            {
                string sLagna = "    ";
                ZodiacHouse zBase = new ZodiacHouse(local.LagnaZodiacHouse);
                for (int i = 0; i < 12; i++)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)local.LagnasUT[i];
                    ZodiacHouse zCurr = new ZodiacHouse((ZodiacHouseName)pmi.Info);
                    zCurr = zCurr.Add(12);
                    sLagna = string.Format("{0}{1} Lagna until {2}. ", sLagna, zCurr.Value,
                        this.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
                    // if (opts.OneEntryPerLine || i % 4 == 3)
                    if (i % 4 == 3)
                    {
                        //TODO : Check this for logical display again, does it make sense to show?
                       // this.mList.Items.Add(sLagna);
                        results.Add("Lagna" + i, sLagna);
                        sLagna = "";
                    }
                }
            }

            if (this.bCalculateHoraCusps)
            {
                string sHora = "    ";
                for (int i = 0; i < 24; i++)
                {
                    int ib = (int)Basics.Normalize_exc_lower(0, 7, local.HoraBase + i);
                    Body.Name bHora = horoscope.HoraOrder[ib];
                    sHora = string.Format("{0}{1} hora until {2}. ", sHora, bHora,
                        this.UtTimeToString(local.HorasUT[i + 1], local.SunriseUT, local.Sunrise));
                    //if (opts.OneEntryPerLine || i % 4 == 3)
                    if (i % 4 == 3)
                    {
                        //TODO : Check this for logical display again, does it make sense to show?
                        //this.mList.Items.Add(sHora);
                        results.Add("Hora" +i, sHora);
                        sHora = "";
                    }
                }
            }

            if (this.bCalculateKalaCusps)
            {
                string sKala = "    ";
                for (int i = 0; i < 16; i++)
                {
                    int ib = (int)Basics.Normalize_exc_lower(0, 8, local.KalaBase + i);
                    Body.Name bKala = horoscope.KalaOrder[ib];
                    sKala = string.Format("{0}{1} kala until {2}. ", sKala, bKala,
                        this.UtTimeToString(local.KalasUT[i + 1], local.SunriseUT, local.Sunrise));
                    //if (opts.OneEntryPerLine || i % 4 == 3)
                    if (i % 4 == 3)
                    {
                        //TODO : Check this for logical display again, does it make sense to show?
                        //this.mList.Items.Add(sKala);
                        results.Add("Kala" + i, sKala);
                        sKala = "";
                    }
                }
            }
            return results;
            //this.mList.Items.Add("");
        }
        private Moment UtToMoment(double found_ut)
        {
            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += (horoscope.Info.tz.toDouble() / 24.0);
            Sweph.swe_revjul(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            return m;
        }
        private string UtToString(double ut)
        {
            int year = 0, month = 0, day = 0;
            double time = 0;

            ut += horoscope.Info.tz.toDouble() / 24.0;
            Sweph.swe_revjul(ut, ref year, ref month, ref day, ref time);
            return this.TimeToString(time);
        }
        private string UtTimeToString(double ut_event, double ut_sr, double sunrise)
        {
            Moment m = this.UtToMoment(ut_event);
            HMSInfo hms = new HMSInfo(m.Time);

            if (ut_event >= (ut_sr - (sunrise / 24.0) + 1.0))
            {
                if (false == bLargeHours)
                    return string.Format("*{0:00}:{1:00}", hms.degree, hms.minute);
                else
                    return string.Format("{0:00}:{1:00}", hms.degree + 24, hms.minute);
            }
            return string.Format("{0:00}:{1:00}", hms.degree, hms.minute);
        }
        private string TimeToString(double time)
        {
            HMSInfo hms = new HMSInfo(time);
            return string.Format("{0:00}:{1:00}",
                hms.degree, hms.minute, hms.second);
        }

    }
}
