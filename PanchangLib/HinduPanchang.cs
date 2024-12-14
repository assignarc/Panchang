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
                sunrisePosition = SunrisePositionType.ApparentDiscCenter,
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
                                        alat: new HMSInfo(33, 0, 18, Direction.NS), 
                                        alon: new HMSInfo(-97, 13, 35, Direction.EW), 
                                        atz: new HMSInfo(-5, 0, 0, Direction.EW)
                                        );
            horoscope = new Horoscope(horaInfo, horaOptions);
            horoscope.strength_options = strengthOptions;

        }

        public void Compute()
        {
            double ut_start = Math.Floor(horoscope.baseUT);
            double[] geopos = new double[]{ horoscope.info.lon.toDouble(), horoscope.info.lat.toDouble(), horoscope.info.alt };

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



            Sweph.obtainLock(horoscope);
            horoscope.populateSunrisetCacheHelper(ut - 0.5, ref sunrise, ref sunset, ref ut_sr);
            Sweph.releaseLock(horoscope);

            Sweph.swe_revjul(ut_sr, ref year, ref month, ref day, ref hour);
            Moment moment_sr = new Moment(year, month, day, hour);
            Moment moment_ut = new Moment(ut, horoscope);
            HoraInfo infoCurr = new HoraInfo(moment_ut, horoscope.info.lat, horoscope.info.lon, horoscope.info.tz);
            Horoscope hCurr = new Horoscope(infoCurr, horoscope.options);

           
            PanchangaLocalMoments local = new PanchangaLocalMoments();
            local.sunrise = hCurr.sunrise;
            local.sunset = sunset;
            local.sunrise_ut = ut_sr;
            Sweph.swe_revjul(ut, ref year, ref month, ref day, ref hour);
            local.wday = (Basics.Weekday)Sweph.swe_day_of_week(ut);



            local.kalas_ut = hCurr.getKalaCuspsUt();
            if (bCalcSpecialKalas)
            {
                Body.Name bStart = Basics.WeekdayRuler(hCurr.wday);
                if (hCurr.options.KalaType == EHoraType.Lmt)
                    bStart = Basics.WeekdayRuler(hCurr.lmt_wday);

                local.rahu_kala_index = this.rahu_kalas[(int)bStart];
                local.gulika_kala_index = this.gulika_kalas[(int)bStart];
                local.yama_kala_index = this.yama_kalas[(int)bStart];
            }

            if (bCalculateLagnaCusps)
            {
                Sweph.obtainLock(horoscope);
                BodyPosition bp_lagna_sr = Basics.CalculateSingleBodyPosition(ut_sr, Sweph.BodyNameToSweph(Body.Name.Lagna), Body.Name.Lagna, BodyType.Name.Lagna, horoscope);
                DivisionPosition dp_lagna_sr = bp_lagna_sr.ToDivisionPosition(new Division(DivisionType.Rasi));
                local.lagna_zh = dp_lagna_sr.zodiac_house.value;

                Longitude bp_lagna_base = new Longitude(bp_lagna_sr.Longitude.toZodiacHouseBase());
                double ut_transit = ut_sr;
                for (int i = 1; i <= 12; i++)
                {
                    Retrogression r = new Retrogression(horoscope, Body.Name.Lagna);
                    ut_transit = r.GetLagnaTransitForward(ut_transit, bp_lagna_base.add(i * 30.0));

                    PanchangaMomentInfo pmi = new PanchangaMomentInfo(
                        ut_transit, (int)bp_lagna_sr.Longitude.toZodiacHouse().add(i + 1).value);
                    local.lagnas_ut.Add(pmi);
                }

                Sweph.releaseLock(horoscope);
            }

            if (bCalculateTithiCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.obtainLock(horoscope);
                Tithi tithi_start = t.LongitudeOfTithi(ut_sr).toTithi();
                Tithi tithi_end = t.LongitudeOfTithi(ut_sr + 1.0).toTithi();

                Tithi tithi_curr = tithi_start.add(1);
                local.tithi_index_start = globals.tithis_ut.Count - 1;
                local.tithi_index_end = globals.tithis_ut.Count - 1;

                while (tithi_start.value != tithi_end.value &&
                    tithi_curr.value != tithi_end.value)
                {
                    tithi_curr = tithi_curr.add(2);
                    double dLonToFind = ((double)(int)tithi_curr.value - 1) * (360.0 / 30.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.tithis_ut.Add(new PanchangaMomentInfo(ut_found, (int)tithi_curr.value));
                    local.tithi_index_end++;
                }
                Sweph.releaseLock(horoscope);
            }

            if (bCalculateKaranaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.obtainLock(horoscope);
                Karana karana_start = t.LongitudeOfTithi(ut_sr).toKarana();
                Karana karana_end = t.LongitudeOfTithi(ut_sr + 1.0).toKarana();

                Karana karana_curr = karana_start.add(1);
                local.karana_index_start = globals.karanas_ut.Count - 1;
                local.karana_index_end = globals.karanas_ut.Count - 1;

                while (karana_start.value != karana_end.value &&
                    karana_curr.value != karana_end.value)
                {
                    karana_curr = karana_curr.add(2);
                    double dLonToFind = ((double)(int)karana_curr.value - 1) * (360.0 / 60.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.karanas_ut.Add(new PanchangaMomentInfo(ut_found, (int)karana_curr.value));
                    local.karana_index_end++;
                }
                Sweph.releaseLock(horoscope);
            }

            if (bCalculateSMYogaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.obtainLock(horoscope);
                SunMoonYoga sm_start = t.LongitudeOfSunMoonYoga(ut_sr).toSunMoonYoga();
                SunMoonYoga sm_end = t.LongitudeOfSunMoonYoga(ut_sr + 1.0).toSunMoonYoga();

                SunMoonYoga sm_curr = sm_start.add(1);
                local.smyoga_index_start = globals.smyogas_ut.Count - 1;
                local.smyoga_index_end = globals.smyogas_ut.Count - 1;

                while (sm_start.value != sm_end.value &&
                    sm_curr.value != sm_end.value)
                {
                    sm_curr = sm_curr.add(2);
                    double dLonToFind = ((double)(int)sm_curr.value - 1) * (360.0 / 27);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfSunMoonYogaDir));

                    globals.smyogas_ut.Add(new PanchangaMomentInfo(ut_found, (int)sm_curr.value));
                    local.smyoga_index_end++;
                }

                Sweph.releaseLock(horoscope);
            }

            if (bCalculateNakshatraCusps)
            {
                bool bDiscard = true;
                Transit t = new Transit(horoscope, Body.Name.Moon);
                Sweph.obtainLock(horoscope);
                Nakshatra nak_start = t.GenericLongitude(ut_sr, ref bDiscard).toNakshatra();
                Nakshatra nak_end = t.GenericLongitude(ut_sr + 1.0, ref bDiscard).toNakshatra();

                local.nakshatra_index_start = globals.nakshatras_ut.Count - 1;
                local.nakshatra_index_end = globals.nakshatras_ut.Count - 1;

                Nakshatra nak_curr = nak_start.add(1);

                while (nak_start.value != nak_end.value &&
                    nak_curr.value != nak_end.value)
                {
                    nak_curr = nak_curr.add(2);
                    double dLonToFind = ((double)((int)nak_curr.value - 1)) * (360.0 / 27.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.GenericLongitude));

                    globals.nakshatras_ut.Add(new PanchangaMomentInfo(ut_found, (int)nak_curr.value));
                    Console.WriteLine("Found nakshatra {0}", nak_curr.value);
                    local.nakshatra_index_end++;
                }
                Sweph.releaseLock(horoscope);
            }

            if (bCalculateHoraCusps)
            {
                local.horas_ut = hCurr.getHoraCuspsUt();
                hCurr.calculateHora(ut_sr + 1.0 / 24.0, ref local.hora_base);
            }

            if (bCalculateKalaCusps)
            {
                hCurr.calculateKala(ref local.kala_base);
            }


            this.locals.Add(local);
            
        }

        private Dictionary<string, string> DisplayEntry(PanchangaLocalMoments local)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            PDay pDay = new PDay();
            string s;
            int day = 0, month = 0, year = 0;
            double time = 0;

            Sweph.swe_revjul(local.sunrise_ut, ref year, ref month, ref day, ref time);
            Moment m = new Moment(year, month, day, time);


            //this.mList.Items.Add(string.Format("{0}, {1}", local.wday, m.ToDateString()));
           /* results.Add("LocalWeekday", local.wday.ToString());
            results.Add("Moment", m.ToDateString());*/
            pDay.LocalWeekday = local.wday;
            pDay.Moment = m;
            if (this.bShowSunriset)
            {
                s = string.Format("Sunrise at {0}. Sunset at {1}",this.timeToString(local.sunrise),
                    this.timeToString(local.sunset));
                pDay.Celestials.Add(new CelestialBody() { Name = Body.Name.Sun, Rise = this.timeToString(local.sunrise), Set = this.timeToString(local.sunset) });
               /* results.Add("Sunrise", this.timeToString(local.sunrise));
                results.Add("Sunset", this.timeToString(local.sunset));*/
                //this.mList.Items.Add(s);
            }

            if (this.bCalcSpecialKalas)
            {

                pDay.Rahu = string.Format("Rahu Kala from {0} to {1}",
                    new Moment(local.kalas_ut[local.rahu_kala_index], horoscope).ToTimeString(),
                    new Moment(local.kalas_ut[local.rahu_kala_index + 1], horoscope).ToTimeString());
                pDay.Gulika = string.Format("Gulika Kala from {0} to {1}",
                    new Moment(local.kalas_ut[local.gulika_kala_index], horoscope).ToTimeString(),
                    new Moment(local.kalas_ut[local.gulika_kala_index + 1], horoscope).ToTimeString());
                pDay.Yama = string.Format("Yama Kala from {0} to {1}",
                    new Moment(local.kalas_ut[local.yama_kala_index], horoscope).ToTimeString(),
                    new Moment(local.kalas_ut[local.yama_kala_index + 1], horoscope).ToTimeString());

                
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

                if (local.tithi_index_start == local.tithi_index_end &&
                    local.tithi_index_start >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.tithis_ut[local.tithi_index_start];
                    Tithi t = new Tithi((TithiName)pmi.info);

                    //this.mList.Items.Add(string.Format("{0} - full.", t.value));
                    //results.Add("Tithi", string.Format("{0} - full.", t.value));
                    pDay.Tithi = t;
                    pDay.TithiText = string.Format("{0} - full.", t.value);
                }
                else
                {
                    for (int i = local.tithi_index_start + 1; i <= local.tithi_index_end; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.tithis_ut[i];
                        Tithi t = new Tithi((TithiName)pmi.info).addReverse(2);
                        s_tithi += string.Format("{0} until {1}",
                            t.value,
                            this.utTimeToString(pmi.ut, local.sunrise_ut, local.sunrise));
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

                if (local.karana_index_start == local.karana_index_end &&
                    local.karana_index_start >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.karanas_ut[local.karana_index_start];
                    Karana k = new Karana((KaranaName)pmi.info);
                    //this.mList.Items.Add(string.Format("{0} karana - full.", k.value));
                    //results.Add("Karana", string.Format("{0} karana - full.", k.value));
                    pDay.Karana = k;
                    pDay.KaranaText = string.Format("{0} karana - full.", k.value);
                }
                else
                {
                    for (int i = local.karana_index_start + 1; i <= local.karana_index_end; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.karanas_ut[i];
                        Karana k = new Karana((KaranaName)pmi.info).addReverse(2);
                        s_karana += string.Format("{0} karana until {1}",
                            k.value,
                            this.utTimeToString(pmi.ut, local.sunrise_ut, local.sunrise));
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

                if (local.smyoga_index_start == local.smyoga_index_end &&
                    local.smyoga_index_start >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.smyogas_ut[local.smyoga_index_start];
                    SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.info);
                    //this.mList.Items.Add();
                    results.Add("Yoga", string.Format("{0} yoga - full.", sm.value));
                }
                else
                {
                    for (int i = local.smyoga_index_start + 1; i <= local.smyoga_index_end; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.smyogas_ut[i];
                        SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.info).addReverse(2);
                        s_smyoga += string.Format("{0} yoga until {1}",
                            sm.value,
                            this.utTimeToString(pmi.ut, local.sunrise_ut, local.sunrise));

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

                if (local.nakshatra_index_start == local.nakshatra_index_end &&
                    local.nakshatra_index_start >= 0)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.nakshatras_ut[local.nakshatra_index_start];
                    Nakshatra n = new Nakshatra((NakshatraName)pmi.info);
                   // this.mList.Items.Add(string.Format("{0} - full.", n.value));
                    results.Add("Nakshatra", string.Format("{0} - full.", n.value));
                }
                else
                {
                    for (int i = local.nakshatra_index_start + 1; i <= local.nakshatra_index_end; i++)
                    {
                        if (i < 0)
                            continue;
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.nakshatras_ut[i];
                        Nakshatra n = new Nakshatra((NakshatraName)pmi.info).addReverse(2);
                        s_nak += string.Format("{0} until {1}",
                            n.value,
                            this.utTimeToString(pmi.ut, local.sunrise_ut, local.sunrise));
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
                ZodiacHouse zBase = new ZodiacHouse(local.lagna_zh);
                for (int i = 0; i < 12; i++)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)local.lagnas_ut[i];
                    ZodiacHouse zCurr = new ZodiacHouse((ZodiacHouseName)pmi.info);
                    zCurr = zCurr.add(12);
                    sLagna = string.Format("{0}{1} Lagna until {2}. ", sLagna, zCurr.value,
                        this.utTimeToString(pmi.ut, local.sunrise_ut, local.sunrise));
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
                    int ib = (int)Basics.Normalize_exc_lower(0, 7, local.hora_base + i);
                    Body.Name bHora = horoscope.horaOrder[ib];
                    sHora = string.Format("{0}{1} hora until {2}. ", sHora, bHora,
                        this.utTimeToString(local.horas_ut[i + 1], local.sunrise_ut, local.sunrise));
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
                    int ib = (int)Basics.Normalize_exc_lower(0, 8, local.kala_base + i);
                    Body.Name bKala = horoscope.kalaOrder[ib];
                    sKala = string.Format("{0}{1} kala until {2}. ", sKala, bKala,
                        this.utTimeToString(local.kalas_ut[i + 1], local.sunrise_ut, local.sunrise));
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
        private Moment utToMoment(double found_ut)
        {
            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += (horoscope.info.tz.toDouble() / 24.0);
            Sweph.swe_revjul(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            return m;
        }
        private string utToString(double ut)
        {
            int year = 0, month = 0, day = 0;
            double time = 0;

            ut += horoscope.info.tz.toDouble() / 24.0;
            Sweph.swe_revjul(ut, ref year, ref month, ref day, ref time);
            return this.timeToString(time);
        }
        private string utTimeToString(double ut_event, double ut_sr, double sunrise)
        {
            Moment m = this.utToMoment(ut_event);
            HMSInfo hms = new HMSInfo(m.time);

            if (ut_event >= (ut_sr - (sunrise / 24.0) + 1.0))
            {
                if (false == bLargeHours)
                    return string.Format("*{0:00}:{1:00}", hms.degree, hms.minute);
                else
                    return string.Format("{0:00}:{1:00}", hms.degree + 24, hms.minute);
            }
            return string.Format("{0:00}:{1:00}", hms.degree, hms.minute);
        }
        private string timeToString(double time)
        {
            HMSInfo hms = new HMSInfo(time);
            return string.Format("{0:00}:{1:00}",
                hms.degree, hms.minute, hms.second);
        }

    }
}
