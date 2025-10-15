using org.transliteral.panchang.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace org.transliteral.panchang
{
    public class HinduPanchang
    {

        private Dictionary<DateTime, PanchangDay> calendar = new Dictionary<DateTime,PanchangDay>();

        GlobalMoments globals = new GlobalMoments();
        protected Horoscope horoscope;
        private PanchangOptions options = new PanchangOptions();

        public Dictionary<DateTime, PanchangDay> Calendar { get => calendar; set => calendar = value; }

        public HinduPanchang(HoraInfo horaInfo, 
                                HoroscopeOptions horaOptions, 
                                StrengthOptions strengthOptions, 
                                GlobalOptions globalOptions,
                                PanchangOptions panchangOptions)
        {
            GlobalOptions.Instance = globalOptions;
            options = panchangOptions;

            horoscope = new Horoscope(horaInfo, horaOptions)
            {
                StrengthOptions = strengthOptions
            };

        }
        public HinduPanchang(Horoscope horoScope,
                             PanchangOptions panchangOptions)
        {
            options = panchangOptions;
            horoscope = horoScope;
        }

        public void Compute()
        {
            double ut_start = Math.Floor(horoscope.BaseUT);
            double[] geopos = new double[]{ horoscope.Info.lon.toDouble(), horoscope.Info.lat.toDouble(), horoscope.Info.alt };

            this.globals = new GlobalMoments();
            for (int i = 0; i < options.NumberOfDays; i++)
            {
                LocalMoments lm = this.Compute(ut_start, geopos);
                calendar.Add(lm.HoraInfo.tob.ToDateTime(), this.Format(lm));
                ut_start += 1;
            }
        }
        private LocalMoments Compute(double ut, double[] geopos)
        {
            int year = 0, month = 0, day = 0;
            double sunset = 0, hour = 0;
            double sunrise = 0;
            double ut_sr = 0;

            Sweph.Lock(horoscope);
            horoscope.PopulateSunrisetCacheHelper(ut - 0.5, ref sunrise, ref sunset, ref ut_sr);
            Sweph.Unlock(horoscope);

            Sweph.SWE_ReverseJulianDay(ut_sr, ref year, ref month, ref day, ref hour);
            Moment moment_sr = new Moment(year, month, day, hour);
            Moment moment_ut = new Moment(ut, horoscope);
            HoraInfo infoCurr = new HoraInfo(moment_ut, horoscope.Info.lat, horoscope.Info.lon, horoscope.Info.tz);
            Horoscope hCurr = new Horoscope(infoCurr, horoscope.Options);


            LocalMoments local = new LocalMoments
            {
                Sunrise = hCurr.Sunrise,
                Sunset = sunset,
                SunriseUT = ut_sr,
                HoraInfo = infoCurr,
            };
            Sweph.SWE_ReverseJulianDay(ut, ref year, ref month, ref day, ref hour);
            local.WeekDay = (Weekday)Sweph.SWE_DayOfWeek(ut);

            local.KalasUT = hCurr.GetKalaCuspsUt();
            if (options.CalculateSpecialKalas)
            {
                BodyName bStart = Basics.WeekdayRuler(hCurr.Weekday);
                if (hCurr.Options.KalaType == EHoraType.Lmt)
                    bStart = Basics.WeekdayRuler(hCurr.WeekdayLMT);

                local.RahuKalaIndex = options.RahuKalas[(int)bStart];
                local.GulikaKalaIndex = options.GulikaKalas[(int)bStart];
                local.YamaKalaIndex = options.YamaKalas[(int)bStart];
            }

            if (options.CalculateLagnaCusps)
            {
                Sweph.Lock(horoscope);
                BodyPosition bp_lagna_sr = Basics.CalculateSingleBodyPosition(ut_sr, Sweph.BodyNameToSweph(BodyName.Lagna), BodyName.Lagna, BodyType.Name.Lagna, horoscope);
                DivisionPosition dp_lagna_sr = bp_lagna_sr.ToDivisionPosition(new Division(DivisionType.Rasi));
                local.LagnaZodiacHouse = dp_lagna_sr.ZodiacHouse.Value;

                Longitude bp_lagna_base = new Longitude(bp_lagna_sr.Longitude.ToZodiacHouseBase());
                double ut_transit = ut_sr;
                for (int i = 1; i <= 12; i++)
                {
                    Retrogression r = new Retrogression(horoscope, BodyName.Lagna);
                    ut_transit = r.GetLagnaTransitForward(ut_transit, bp_lagna_base.Add(i * 30.0));

                    MomentInfo pmi = new MomentInfo(
                        ut_transit, (int)bp_lagna_sr.Longitude.ToZodiacHouse().Add(i + 1).Value);
                    local.LagnasUT.Add(pmi);
                }
                Sweph.Unlock(horoscope);
            }

            if (options.CalculateTithiCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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

                    globals.TithisUT.Add(new MomentInfo(ut_found, (int)tithi_curr.Value));
                    local.TithiIndexEnd++;
                }
                Sweph.Unlock(horoscope);
            }

            if (options.CalculateKaranaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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

                    globals.KaranasUT.Add(new MomentInfo(ut_found, (int)karana_curr.value));
                    local.KaranaIndexEnd++;
                }
                Sweph.Unlock(horoscope);
            }

            if (options.CalculateSunMoonYogaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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

                    globals.SunMoonYogasUT.Add(new MomentInfo(ut_found, (int)sm_curr.Value));
                    local.SunMoonYogaIndexEnd++;
                }
                Sweph.Unlock(horoscope);
            }

            if (options.CalculateNakshatraCusps)
            {
                bool bDiscard = true;
                Transit t = new Transit(horoscope, BodyName.Moon);
                Sweph.Lock(horoscope);
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

                    globals.NakshatrasUT.Add(new MomentInfo(ut_found, (int)nak_curr.Value));
                    Logger.Info(String.Format("Found nakshatra {0}", nak_curr.Value));
                    local.NakshatraIndexEnd++;
                }
                Sweph.Unlock(horoscope);
            }

            if (options.CalculateHoraCusps)
            {
                local.HorasUT = hCurr.GetHoraCuspsUt();
                hCurr.CalculateHora(ut_sr + 1.0 / 24.0, ref local.HoraBase);
            }

            if (options.CalculateKalaCusps)
            {
                hCurr.CalculateKala(ref local.KalaBase);
            }


            return local;
            
        }

        private PanchangDay Format(LocalMoments local)
        {
            //Dictionary<string, string> results = new Dictionary<string, string>();
            PanchangDay pDay = PanchangDay.New();
            int day = 0, month = 0, year = 0;
            double time = 0;

            Sweph.SWE_ReverseJulianDay(local.SunriseUT, ref year, ref month, ref day, ref time);
            Moment m = new Moment(year, month, day, time);

            pDay.LocalWeekday = local.WeekDay;
            pDay.Texts.Add("LocalWeekday", string.Format("{0}, {1}", local.WeekDay, m.ToDateString()));
            pDay.Moment = m;
            pDay.Texts.Add("#Moment", m.ToDateString());

            if (options.ShowSunriset)
            {
                Logger.Info(string.Format("Sunrise at {0}. Sunset at {1}", 
                    TimeUtils.TimeToString(local.Sunrise), 
                    TimeUtils.TimeToString(local.Sunset)));
                pDay.Celestials.Add(new CelestialBody()
                {
                    Name = BodyName.Sun,
                    Rise = TimeUtils.ToTimeSpan(local.Sunrise),
                    Set = TimeUtils.ToTimeSpan(local.Sunset)
                });
               

                pDay.Texts.Add("#Sunrise", TimeUtils.TimeToString(local.Sunrise));
                pDay.Texts.Add("#Sunset", TimeUtils.TimeToString(local.Sunset));
                pDay.Texts.Add("SunRiseSet", string.Format("Sunrise at {0}. Sunset at {1}",
                                TimeUtils.TimeToString(local.Sunrise),
                                TimeUtils.TimeToString(local.Sunset)));

            }

            if (options.CalculateSpecialKalas)
            {
                pDay.SpecialKala.Add(new KeyValuePair<string, string>("Rahu",
                         string.Format("from {0} to {1}",
                            new Moment(local.KalasUT[local.RahuKalaIndex], horoscope).ToTimeString(),
                            new Moment(local.KalasUT[local.RahuKalaIndex + 1], horoscope).ToTimeString()))
                    );
                pDay.Texts.Add("Rahu Kala", string.Format("Rahu Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.RahuKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.RahuKalaIndex + 1], horoscope).ToTimeString()));

                pDay.SpecialKala.Add(new KeyValuePair<string, string>("Gulika",
                         string.Format("from {0} to {1}",
                            new Moment(local.KalasUT[local.GulikaKalaIndex], horoscope).ToTimeString(),
                            new Moment(local.KalasUT[local.GulikaKalaIndex + 1], horoscope).ToTimeString()))
                    );
                pDay.Texts.Add("Gulika Kala", string.Format("Gulika Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.GulikaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.GulikaKalaIndex + 1], horoscope).ToTimeString()));


                pDay.SpecialKala.Add(new KeyValuePair<string, string>("Yama",
                         string.Format("from {0} to {1}",
                            new Moment(local.KalasUT[local.YamaKalaIndex], horoscope).ToTimeString(),
                            new Moment(local.KalasUT[local.YamaKalaIndex + 1], horoscope).ToTimeString()))
                    );
                pDay.Texts.Add("Yama Kala",string.Format("Yama Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.YamaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.YamaKalaIndex + 1], horoscope).ToTimeString()));

                //pDay.Texts.Add("Rahu Kala", pDay.SpecialKala.First(x => x.Key == "Rahu").Value);
                //pDay.Texts.Add("Gulika Kala", pDay.SpecialKala.First(x => x.Key == "Gulika").Value);
                //pDay.Texts.Add("Yama Kala", pDay.SpecialKala.First(x => x.Key == "Yama").Value);
           }

            if (options.CalculateTithiCusps)
            {
                string s_tithi = "";

                if (local.TithiIndexStart == local.TithiIndexEnd &&
                    local.TithiIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.TithisUT[local.TithiIndexStart];
                    Tithi t = new Tithi((TithiName)pmi.Info);

                    pDay.Texts.Add("Tithi", string.Format("{0} - full.", t.Value));
                    pDay.Tithi.Add(new KeyValuePair<Tithi, string>(t, "full"));
                }
                else
                {
                    for (int i = local.TithiIndexStart + 1; i <= local.TithiIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.TithisUT[i];
                        Tithi t = new Tithi((TithiName)pmi.Info).AddReverse(2);
                        s_tithi += string.Format("{0} until {1}",
                            t.Value,
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));
                        pDay.Tithi = new List<KeyValuePair<Tithi, string>>()
                        {
                            new KeyValuePair<Tithi, string>(t, "Until " +
                                TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours))
                        };

                        if (options.OneEntryPerLine)
                            s_tithi += "\n";
                        else
                            s_tithi += ". ";
                    }

                    pDay.Texts.Add("Tithi", s_tithi);

                }
            }

            if (options.CalculateKaranaCusps)
            {
                string s_karana = "";

                if (local.KaranaIndexStart == local.KaranaIndexEnd &&
                    local.KaranaIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.KaranasUT[local.KaranaIndexStart];
                    Karana karana = new Karana((KaranaName)pmi.Info);
                    pDay.Texts.Add("Karana", string.Format("{0} karana - full.", karana.value));
                    pDay.Karana  = new List<KeyValuePair<Karana, string>>()
                    {
                        new KeyValuePair<Karana, string>(karana, "full")
                    };  
                    
                }
                else
                {
                    for (int i = local.KaranaIndexStart + 1; i <= local.KaranaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.KaranasUT[i];
                        Karana karana = new Karana((KaranaName)pmi.Info).addReverse(2);

                        s_karana += string.Format("{0} karana until {1}",
                            karana.value,
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));
                        
                        pDay.Karana.Add(new KeyValuePair<Karana, string>(karana,
                                                    "Until " + TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));
                        if (options.OneEntryPerLine)
                            s_karana += "\n";
                        else
                            s_karana += ". ";
                    }

                    pDay.Texts.Add("Karana", s_karana);
                }
            }

            if (options.CalculateSunMoonYogaCusps)
            {
                string s_smyoga = "";

                if (local.SunMoonYogaIndexStart == local.SunMoonYogaIndexEnd &&
                    local.SunMoonYogaIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.SunMoonYogasUT[local.SunMoonYogaIndexStart];
                    SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info);
                    pDay.Texts.Add("Yoga", string.Format("{0} yoga - full.", sm.Value));
                    pDay.Yoga = new List<KeyValuePair<SunMoonYoga, string>>()
                    {
                        new KeyValuePair<SunMoonYoga, string>(sm, "full")
                    };
                }
                else
                {
                    for (int i = local.SunMoonYogaIndexStart + 1; i <= local.SunMoonYogaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.SunMoonYogasUT[i];
                        SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info).AddReverse(2);
                        s_smyoga += string.Format("{0} yoga until {1}",
                            sm.Value,
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));

                        pDay.Yoga.Add(new KeyValuePair<SunMoonYoga, string>(sm,
                                                    "Until " + TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));

                        if (options.OneEntryPerLine)
                             s_smyoga += "\n";
                        else
                            s_smyoga += ". ";
                    }

                    pDay.Texts.Add("Yoga", s_smyoga);
                }
            }

            if (options.CalculateNakshatraCusps)
            {
                string s_nak = "";

                if (local.NakshatraIndexStart == local.NakshatraIndexEnd &&
                    local.NakshatraIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.NakshatrasUT[local.NakshatraIndexStart];
                    Nakshatra n = new Nakshatra((NakshatraName)pmi.Info);
                    pDay.Texts.Add("Nakshatra", string.Format("{0} - full.", n.Value));
                    pDay.Nakshatra.Add(new KeyValuePair<Nakshatra, string>(n, "full") );
                }
                else
                {
                    for (int i = local.NakshatraIndexStart + 1; i <= local.NakshatraIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;

                        MomentInfo pmi = (MomentInfo)globals.NakshatrasUT[i];
                        Nakshatra nakshatra  = new Nakshatra((NakshatraName)pmi.Info).AddReverse(2);

                        pDay.Nakshatra.Add(
                            new KeyValuePair<Nakshatra, string>(nakshatra,
                                     "Until " + TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));

                        s_nak += string.Format("{0} until {1}",
                            nakshatra.Value,
                                TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));
                        if(options.OneEntryPerLine)
                            s_nak += "\n";
                        else
                            s_nak += ". ";
                    }

                    pDay.Texts.Add("Nakshatra", s_nak);
                }
            }

            if (options.CalculateLagnaCusps)
            {
                string sLagna = "";
                ZodiacHouse zBase = new ZodiacHouse(local.LagnaZodiacHouse);
                for (int i = 0; i < 12; i++)
                {
                    MomentInfo pmi = (MomentInfo)local.LagnasUT[i];
                    ZodiacHouse zCurr = new ZodiacHouse((ZodiacHouseName)pmi.Info);

                    zCurr = zCurr.Add(12);
                    sLagna = string.Format("{0}{1} Lagna until {2}. ", sLagna, zCurr.Value,
                        TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz,options.LargeHours));
                    pDay.Lagna.Add(
                        new KeyValuePair<ZodiacHouse, string>(zCurr,"Until " + 
                        TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));
                    
                    if (i % 4 == 3)
                    {
                        pDay.Texts.Add("Lagna" + i, sLagna);
                        sLagna = "";
                    }
                }
            }

            if (options.CalculateHoraCusps)
            {
                string sHora = "";
                for (int i = 0; i < 24; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 7, local.HoraBase + i);
                    BodyName bHora = horoscope.HoraOrder[ib];

                    sHora = string.Format("{0}{1} hora until {2}. ", sHora, bHora,
                        TimeUtils.UtTimeToString(local.HorasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));

                    pDay.Hora.Add(new KeyValuePair<BodyName, string>(bHora,
                                                "Until " + TimeUtils.UtTimeToString(local.HorasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));
                  
                    if (options.OneEntryPerLine || i % 4 == 3)
                    {
                        pDay.Texts.Add("Hora" +i, sHora);
                        sHora = "";
                    }
                }
            }

            if (options.CalculateKalaCusps)
            {
                string sKala = "";
                for (int i = 0; i < 16; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 8, local.KalaBase + i);
                    BodyName bKala = horoscope.KalaOrder[ib];
                    sKala = string.Format("{0}{1} kala until {2}. ", sKala, bKala,
                        TimeUtils.UtTimeToString(local.KalasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours));
                    
                    pDay.Kala.Add(new KeyValuePair<BodyName, string>(bKala,
                                                "Until " + TimeUtils.UtTimeToString(local.KalasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, options.LargeHours)));

                    if (options.OneEntryPerLine || i % 4 == 3)
                    {
                        pDay.Texts.Add("Kala" + i, sKala);
                        sKala = "";
                    }
                }
            }
            return pDay;
        }
    }
}
