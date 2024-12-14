using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    /// <summary>
    /// Contains all the information for a horoscope. i.e. All ephemeris lookups
    /// have been completed, sunrise/sunset has been calculated etc.
    /// </summary>
    public class Horoscope : ICloneable
    {
        public event EvtChanged Changed;

        public Body.Name LordOfZodiacHouse(ZodiacHouse zh, Division dtype)
        {
            return LordOfZodiacHouse(zh.value, dtype);
        }
        public Body.Name LordOfZodiacHouse(ZodiacHouseName zh, Division dtype)
        {
            FindStronger fs_colord = new FindStronger(this, dtype, FindStronger.RulesStrongerCoLord(this));

            switch (zh)
            {
                case ZodiacHouseName.Aqu:
                    return fs_colord.StrongerGraha(Body.Name.Rahu, Body.Name.Saturn, true);
                case ZodiacHouseName.Sco:
                    return fs_colord.StrongerGraha(Body.Name.Ketu, Body.Name.Mars, true);
                default:
                    return Basics.SimpleLordOfZodiacHouse(zh);
            }
        }
        public object Clone()
        {
            Horoscope h = new Horoscope((HoraInfo)this.info.Clone(), (HoroscopeOptions)this.options.Clone());
            if (this.strength_options != null)
                h.strength_options = (StrengthOptions)this.strength_options.Clone();
            return h;
        }

        public void OnGlobalCalcPrefsChanged(object o)
        {
            HoroscopeOptions ho = (HoroscopeOptions)o;
            this.options.Copy(ho);
            this.strength_options = null;
            this.OnChanged();
        }

        public void OnlySignalChanged()
        {
            if (Changed != null)
                Changed(this);
        }
        public void OnChanged()
        {
            populateCache();
            if (Changed != null)
                Changed(this);
        }
        public HoroscopeOptions options;

        public DivisionPosition CalculateDivisionPosition(BodyPosition bp, Division d)
        {
            return bp.ToDivisionPosition(d);
        }

        public ArrayList CalculateDivisionPositions(Division d)
        {
            ArrayList al = new ArrayList();
            foreach (BodyPosition bp in this.positionList)
            {
                al.Add(CalculateDivisionPosition(bp, d));
            }
            return al;
        }
        private DivisionPosition CalculateGrahaArudhaDivisionPosition(Body.Name bn, ZodiacHouse zh, Division dtype)
        {
            DivisionPosition dp = getPosition(bn).ToDivisionPosition(dtype);
            DivisionPosition dpl = getPosition(Body.Name.Lagna).ToDivisionPosition(dtype);
            int rel = dp.zodiac_house.numHousesBetween(zh);
            int hse = dpl.zodiac_house.numHousesBetween(zh);
            ZodiacHouse zhsum = zh.add(rel);
            int rel2 = dp.zodiac_house.numHousesBetween(zhsum);
            if (rel2 == 1 || rel2 == 7)
                zhsum = zhsum.add(10);
            DivisionPosition dp2 = new DivisionPosition(Body.Name.Other, BodyType.Name.GrahaArudha, zhsum, 0, 0, 0);
            dp2.otherString = String.Format("{0}{1}", Body.ToShortString(bn), hse);
            return dp2;
        }
        public ArrayList CalculateGrahaArudhaDivisionPositions(Division dtype)
        {

            object[][] parameters = new object[][]
            {
                new object[] { ZodiacHouseName.Ari, Body.Name.Mars },
                new object[] { ZodiacHouseName.Tau, Body.Name.Venus },
                new object[] { ZodiacHouseName.Gem, Body.Name.Mercury },
                new object[] { ZodiacHouseName.Can, Body.Name.Moon },
                new object[] { ZodiacHouseName.Leo, Body.Name.Sun },
                new object[] { ZodiacHouseName.Vir, Body.Name.Mercury },
                new object[] { ZodiacHouseName.Lib, Body.Name.Venus },
                new object[] { ZodiacHouseName.Sco, Body.Name.Mars },
                new object[] { ZodiacHouseName.Sag, Body.Name.Jupiter },
                new object[] { ZodiacHouseName.Cap, Body.Name.Saturn },
                new object[] { ZodiacHouseName.Aqu, Body.Name.Saturn },
                new object[] { ZodiacHouseName.Pis, Body.Name.Jupiter },
                new object[] { ZodiacHouseName.Sco, Body.Name.Ketu },
                new object[] { ZodiacHouseName.Aqu, Body.Name.Rahu }
            };
            ArrayList al = new ArrayList(14);

            for (int i = 0; i < parameters.Length; i++)
                al.Add(this.CalculateGrahaArudhaDivisionPosition(
                    (Body.Name)parameters[i][1],
                    new ZodiacHouse((ZodiacHouseName)parameters[i][0]),
                    dtype));
            return al;
        }
        private string[] varnada_strs = new string[]
            {
                "VL", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10", "V11", "V12"
            };
        public ArrayList CalculateVarnadaDivisionPositions(Division dtype)
        {
            ArrayList al = new ArrayList(12);
            ZodiacHouse _zh_l = this.getPosition(Body.Name.Lagna).ToDivisionPosition(dtype).zodiac_house;
            ZodiacHouse _zh_hl = this.getPosition(Body.Name.HoraLagna).ToDivisionPosition(dtype).zodiac_house;

            ZodiacHouse zh_ari = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zh_pis = new ZodiacHouse(ZodiacHouseName.Pis);
            for (int i = 1; i <= 12; i++)
            {
                ZodiacHouse zh_l = _zh_l.add(i);
                ZodiacHouse zh_hl = _zh_hl.add(i);

                int i_l = 0, i_hl = 0;
                if (zh_l.isOdd()) i_l = zh_ari.numHousesBetween(zh_l);
                else i_l = zh_pis.numHousesBetweenReverse(zh_l);

                if (zh_hl.isOdd()) i_hl = zh_ari.numHousesBetween(zh_hl);
                else i_hl = zh_pis.numHousesBetweenReverse(zh_hl);

                int sum = 0;
                if (zh_l.isOdd() == zh_hl.isOdd()) sum = i_l + i_hl;
                else sum = Math.Max(i_l, i_hl) - Math.Min(i_l, i_hl);

                ZodiacHouse zh_v = null;
                if (zh_l.isOdd()) zh_v = zh_ari.add(sum);
                else zh_v = zh_pis.addReverse(sum);

                DivisionPosition div_pos = new DivisionPosition(Body.Name.Other, BodyType.Name.Varnada, zh_v, 0, 0, 0);
                div_pos.otherString = varnada_strs[i - 1];
                al.Add(div_pos);
            }
            return al;
        }
        private DivisionPosition CalculateArudhaDivisionPosition(ZodiacHouse zh, Body.Name bn,
            Body.Name aname, Division d, BodyType.Name btype)
        {
            BodyPosition bp = getPosition(bn);
            ZodiacHouse zhb = CalculateDivisionPosition(bp, d).zodiac_house;
            int rel = zh.numHousesBetween(zhb);
            ZodiacHouse zhsum = zhb.add(rel);
            int rel2 = zh.numHousesBetween(zhsum);
            if (rel2 == 1 || rel2 == 7)
                zhsum = zhsum.add(10);
            return new DivisionPosition(aname, btype, zhsum, 0, 0, 0);
        }
        public ArrayList CalculateArudhaDivisionPositions(Division d)
        {
            Body.Name[] bnlist = new Body.Name[]
                {
                    Body.Name.Other,
                    Body.Name.AL, Body.Name.A2, Body.Name.A3, Body.Name.A4,
                    Body.Name.A5, Body.Name.A6, Body.Name.A7, Body.Name.A8,
                    Body.Name.A9, Body.Name.A10, Body.Name.A11, Body.Name.UL
                };

            FindStronger fs_colord = new FindStronger(this, d, FindStronger.RulesStrongerCoLord(this));
            ArrayList arudha_div_list = new ArrayList(14);
            DivisionPosition first, second;
            for (int j = 1; j <= 12; j++)
            {
                ZodiacHouse zlagna = this.CalculateDivisionPosition(this.getPosition(Body.Name.Lagna), d).zodiac_house;
                ZodiacHouse zh = zlagna.add(j);
                Body.Name bn_stronger, bn_weaker = Body.Name.Other;
                bn_stronger = Basics.SimpleLordOfZodiacHouse(zh.value);
                if (zh.value == ZodiacHouseName.Aqu)
                {
                    bn_stronger = fs_colord.StrongerGraha(Body.Name.Rahu, Body.Name.Saturn, true);
                    bn_weaker = fs_colord.WeakerGraha(Body.Name.Rahu, Body.Name.Saturn, true);
                }
                else if (zh.value == ZodiacHouseName.Sco)
                {
                    bn_stronger = fs_colord.StrongerGraha(Body.Name.Ketu, Body.Name.Mars, true);
                    bn_weaker = fs_colord.WeakerGraha(Body.Name.Ketu, Body.Name.Mars, true);
                }
                first = CalculateArudhaDivisionPosition(zh, bn_stronger, bnlist[j], d, BodyType.Name.BhavaArudha);
                arudha_div_list.Add(first);
                if (zh.value == ZodiacHouseName.Aqu || zh.value == ZodiacHouseName.Sco)
                {
                    second = CalculateArudhaDivisionPosition(zh, bn_weaker, bnlist[j], d, BodyType.Name.BhavaArudhaSecondary);
                    if (first.zodiac_house.value != second.zodiac_house.value)
                        arudha_div_list.Add(second);
                }
            }
            return arudha_div_list;
        }

        public object UpdateHoraInfo(Object o)
        {
            HoraInfo i = (HoraInfo)o;
            info.DateOfBirth = i.DateOfBirth;
            info.Altitude = i.Altitude;
            info.Latitude = i.Latitude;
            info.Longitude = i.Longitude;
            info.tz = i.tz;
            info.Events = (UserEvent[])i.Events.Clone();
            OnChanged();
            return info.Clone();
        }
        public HoraInfo info;
        public double sunrise;
        public double sunset;
        public double lmt_sunrise;
        public double lmt_sunset;
        public double next_sunrise;
        public double next_sunset;
        public double ayanamsa;
        public double lmt_offset;
        public double baseUT;
        public Basics.Weekday wday;
        public Basics.Weekday lmt_wday;
        public ArrayList positionList;
        public Longitude[] swephHouseCusps;
        public int swephHouseSystem;
        public StrengthOptions strength_options;
        private void populateLmt()
        {
            this.lmt_offset = getLmtOffset(info, baseUT);
            this.lmt_sunrise = 6.0 + lmt_offset * 24.0;
            this.lmt_sunset = 18.0 + lmt_offset * 24.0;
        }
        public double getLmtOffsetDays(HoraInfo info, double _baseUT)
        {
            double ut_lmt_noon = this.getLmtOffset(info, _baseUT);
            double ut_noon = this.baseUT - info.tob.time / 24.0 + 12.0 / 24.0;
            return ut_lmt_noon - ut_noon;
        }
        public double getLmtOffset(HoraInfo _info, double _baseUT)
        {
            double[] geopos = new Double[3] { _info.lon.toDouble(), _info.lat.toDouble(), _info.alt };
            double[] tret = new Double[6] { 0, 0, 0, 0, 0, 0 };
            double midnight_ut = _baseUT - _info.tob.time / 24.0;
            Sweph.swe_lmt(midnight_ut, Sweph.SE_SUN, Sweph.SE_CALC_MTRANSIT, geopos, 0.0, 0.0, tret);
            double lmt_noon_1 = tret[0];
            double lmt_offset_1 = lmt_noon_1 - (midnight_ut + 12.0 / 24.0);
            Sweph.swe_lmt(midnight_ut, Sweph.SE_SUN, Sweph.SE_CALC_MTRANSIT, geopos, 0.0, 0.0, tret);
            double lmt_noon_2 = tret[0];
            double lmt_offset_2 = lmt_noon_2 - (midnight_ut + 12.0 / 24.0);

            double ret_lmt_offset = (lmt_offset_1 + lmt_offset_2) / 2.0;
            //Console.WriteLine("LMT: {0}, {1}", lmt_offset_1, lmt_offset_2);

            return ret_lmt_offset;
            #if DND
			// determine offset from ephemeris time
			lmt_offset = 0;
			double tjd_et = baseUT + Sweph.swe_deltat(baseUT);
			System.Text.StringBuilder s = new System.Text.StringBuilder(256);
			int ret = Sweph.swe_time_equ(tjd_et, ref lmt_offset, s);
            #endif
        }
        private void populateSunrisetCache()
        {
            double sunrise_ut = 0.0;
            this.populateSunrisetCacheHelper(this.baseUT, ref this.next_sunrise, ref this.next_sunset, ref sunrise_ut);
            this.populateSunrisetCacheHelper(sunrise_ut - 1.0 - (1.0 / 24.0), ref this.sunrise, ref this.sunset, ref sunrise_ut);
            //Debug.WriteLine("Sunrise[t]: " + this.sunrise.ToString() + " " + this.sunrise.ToString(), "Basics");
        }
        public void populateSunrisetCacheHelper(double ut, ref double sr, ref double ss, ref double sr_ut)
        {
            int srflag = 0;
            switch (options.sunrisePosition)
            {
                case SunrisePositionType.Lmt:
                    sr = 6.0 + lmt_offset * 24.0;
                    ss = 18.0 + lmt_offset * 24.0;
                    break;
                case SunrisePositionType.TrueDiscEdge:
                    srflag = Sweph.SE_BIT_NO_REFRACTION; goto default;
                case SunrisePositionType.TrueDiscCenter:
                    srflag = Sweph.SE_BIT_NO_REFRACTION | Sweph.SE_BIT_DISC_CENTER; goto default;
                case SunrisePositionType.ApparentDiscCenter:
                    srflag = Sweph.SE_BIT_DISC_CENTER; goto default;
                case SunrisePositionType.ApparentDiscEdge:
                default:
                    //int sflag = 0;
                    //if (options.sunrisePosition == SunrisePositionType.DiscCenter)
                    //	sflag += 256;
                    int year = 0, month = 0, day = 0;
                    double hour = 0.0;

                    double[] geopos = new Double[3] { this.info.lon.toDouble(), this.info.lat.toDouble(), this.info.alt };
                    double[] tret = new Double[6] { 0, 0, 0, 0, 0, 0 };

                    Sweph.swe_rise(ut, Sweph.SE_SUN, srflag, geopos, 0.0, 0.0, tret);
                    sr_ut = tret[0];
                    Sweph.swe_revjul(tret[0], ref year, ref month, ref day, ref hour);
                    sr = hour + this.info.tz.toDouble();
                    Sweph.swe_set(tret[0], Sweph.SE_SUN, srflag, geopos, 0.0, 0.0, tret);
                    Sweph.swe_revjul(tret[0], ref year, ref month, ref day, ref hour);
                    ss = hour + this.info.tz.toDouble();
                    sr = Basics.Normalize_exc(0.0, 24.0, sr);
                    ss = Basics.Normalize_exc(0.0, 24.0, ss);
                    break;
            }
        }


        public double[] getHoraCuspsUt()
        {
            double[] cusps = null;
            switch (this.options.HoraType)
            {
                case EHoraType.Sunriset:
                    cusps = this.getSunrisetCuspsUt(12);
                    break;
                case EHoraType.SunrisetEqual:
                    cusps = this.getSunrisetEqualCuspsUt(12);
                    break;
                case EHoraType.Lmt:
                    Sweph.obtainLock(this);
                    cusps = this.getLmtCuspsUt(12);
                    Sweph.releaseLock(this);
                    break;
            }
            return cusps;
        }

        public double[] getKalaCuspsUt()
        {
            double[] cusps = null;
            switch (this.options.KalaType)
            {
                case EHoraType.Sunriset:
                    cusps = this.getSunrisetCuspsUt(8);
                    break;
                case EHoraType.SunrisetEqual:
                    cusps = this.getSunrisetEqualCuspsUt(8);
                    break;
                case EHoraType.Lmt:
                    Sweph.obtainLock(this);
                    cusps = this.getLmtCuspsUt(8);
                    Sweph.releaseLock(this);
                    break;
            }
            return cusps;
        }

        public double[] getSunrisetCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];

            double sr_ut = this.baseUT - this.hoursAfterSunrise() / 24.0;
            double ss_ut = sr_ut - this.sunrise / 24.0 + this.sunset / 24.0;
            double sr_next_ut = sr_ut - this.sunrise / 24.0 + this.next_sunrise / 24.0 + 1.0;

            double day_span = (ss_ut - sr_ut) / dayParts;
            double night_span = (sr_next_ut - ss_ut) / dayParts;

            for (int i = 0; i < dayParts; i++)
                ret[i] = sr_ut + day_span * i;
            for (int i = 0; i <= dayParts; i++)
                ret[i + dayParts] = ss_ut + night_span * i;
            return ret;
        }
        public double[] getSunrisetEqualCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];

            double sr_ut = this.baseUT - this.hoursAfterSunrise() / 24.0;
            double sr_next_ut = sr_ut - this.sunrise / 24.0 + this.next_sunrise / 24.0 + 1.0;
            double span = (sr_next_ut - sr_ut) / (dayParts * 2);

            for (int i = 0; i <= (dayParts * 2); i++)
                ret[i] = sr_ut + span * i;
            return ret;
        }

        public double[] getLmtCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];
            double sr_lmt_ut = this.baseUT - this.hoursAfterSunrise() / 24.0 - this.sunrise / 24.0 + 6.0 / 24.0;
            double sr_lmt_next_ut = sr_lmt_ut + 1.0;
            //double sr_lmt_ut = this.baseUT - this.info.tob.time / 24.0 + 6.0 / 24.0;
            //double sr_lmt_next_ut = sr_lmt_ut + 1.0;

            double lmt_offset = this.getLmtOffset(this.info, this.baseUT);
            sr_lmt_ut += lmt_offset;
            sr_lmt_next_ut += lmt_offset;

            if (sr_lmt_ut > this.baseUT)
            {
                sr_lmt_ut--;
                sr_lmt_next_ut--;
            }


            double span = (sr_lmt_next_ut - sr_lmt_ut) / (dayParts * 2);

            for (int i = 0; i <= (dayParts * 2); i++)
                ret[i] = sr_lmt_ut + span * i;
            return ret;

        }

        public Body.Name[] kalaOrder = new Body.Name[]
            {
                Body.Name.Sun, Body.Name.Mars, Body.Name.Jupiter, Body.Name.Mercury,
                Body.Name.Venus, Body.Name.Saturn, Body.Name.Moon, Body.Name.Rahu
            };

        public Body.Name calculateKala()
        {
            int iBase = 0;
            return calculateKala(ref iBase);
        }
        public Body.Name calculateKala(ref int iBase)
        {
            int[] offsets_day = new int[] { 0, 6, 1, 3, 2, 4, 5 };
            Body.Name b = Basics.WeekdayRuler(this.wday);
            bool bday_birth = this.isDayBirth();

            double[] cusps = this.getKalaCuspsUt();
            if (this.options.KalaType == EHoraType.Lmt)
            {
                b = Basics.WeekdayRuler(this.lmt_wday);
                bday_birth =
                    this.info.tob.time > this.lmt_sunset ||
                    this.info.tob.time < this.lmt_sunrise;
            }
            int i = offsets_day[(int)b];
            iBase = i;
            int j = 0;

            if (bday_birth)
            {
                for (j = 0; j < 8; j++)
                {
                    if (this.baseUT >= cusps[j] && this.baseUT < cusps[j + 1])
                        break;
                }
                i += j;
                while (i >= 8) i -= 8;
                return kalaOrder[i];
            }
            else
            {
                //i+=4;
                for (j = 8; j < 16; j++)
                {
                    if (this.baseUT >= cusps[j] && this.baseUT < cusps[j + 1])
                        break;
                }
                i += j;
                while (i >= 8) i -= 8;
                return kalaOrder[i];
            }

        }


        public Body.Name[] horaOrder = new Body.Name[]
            {
                Body.Name.Sun, Body.Name.Venus, Body.Name.Mercury, Body.Name.Moon,
                Body.Name.Saturn, Body.Name.Jupiter, Body.Name.Mars
            };
        public Body.Name calculateHora()
        {
            int iBody = 0;
            return this.calculateHora(this.baseUT, ref iBody);
        }
        public Body.Name calculateHora(double _baseUT, ref int baseBody)
        {
            int[] offsets = new int[] { 0, 3, 6, 2, 5, 1, 4 };
            Body.Name b = Basics.WeekdayRuler(this.wday);
            double[] cusps = this.getHoraCuspsUt();
            if (this.options.HoraType == EHoraType.Lmt)
                b = Basics.WeekdayRuler(this.lmt_wday);

            int i = offsets[(int)b];
            baseBody = i;
            int j = 0;
            //for (j=0; j<23; j++)
            //{
            //	Moment m1 = new Moment(cusps[j], this);
            //	Moment m2 = new Moment(cusps[j+1], this);
            //	Console.WriteLine ("Seeing if dob is between {0} and {1}", m1, m2);
            //}
            for (j = 0; j < 23; j++)
            {
                if (_baseUT >= cusps[j] && _baseUT < cusps[j + 1])
                    break;
            }
            //Console.WriteLine ("Found hora in the {0}th hora", j);
            i += j;
            while (i >= 7) i -= 7;
            return horaOrder[i];
        }
        private Body.Name calculateUpagrahasStart()
        {
            if (this.isDayBirth())
                return Basics.WeekdayRuler(this.wday);

            switch (this.wday)
            {
                default:
                case Basics.Weekday.Sunday: return Body.Name.Jupiter;
                case Basics.Weekday.Monday: return Body.Name.Venus;
                case Basics.Weekday.Tuesday: return Body.Name.Saturn;
                case Basics.Weekday.Wednesday: return Body.Name.Sun;
                case Basics.Weekday.Thursday: return Body.Name.Moon;
                case Basics.Weekday.Friday: return Body.Name.Mars;
                case Basics.Weekday.Saturday: return Body.Name.Mercury;
            }
        }

        private void calculateUpagrahasSingle(Body.Name b, double tjd)
        {
            Longitude lon = new Longitude(0);
            lon.value = Sweph.swe_lagna(tjd);
            BodyPosition bp = new BodyPosition(this, b, BodyType.Name.Upagraha,
                lon, 0, 0, 0, 0, 0);
            positionList.Add(bp);
        }

        private void calculateMaandiHelper(Body.Name b, EMaandiType mty, double[] jds, double dOffset, int[] bodyOffsets)
        {
            switch (mty)
            {
                case EMaandiType.SaturnBegin:
                    this.calculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]]);
                    break;
                case EMaandiType.SaturnMid:
                    this.calculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + dOffset);
                    break;
                case EMaandiType.SaturnEnd:
                case EMaandiType.LordlessBegin:
                    int _off1 = bodyOffsets[(int)Body.Name.Saturn] + 1;
                    this.calculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 2.0));
                    break;
                case EMaandiType.LordlessMid:
                    this.calculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 3.0));
                    break;
                case EMaandiType.LordlessEnd:
                    this.calculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 4.0));
                    break;
            }
        }
        private void calculateUpagrahas()
        {

            double dStart = 0, dEnd = 0;

            Moment m = this.info.tob;
            dStart = dEnd = Sweph.swe_julday(m.year, m.month, m.day, -this.info.tz.toDouble());
            Body.Name bStart = this.calculateUpagrahasStart();

            if (this.isDayBirth())
            {
                dStart += this.sunrise / 24.0;
                dEnd += this.sunset / 24.0;
            }
            else
            {
                dStart += this.sunset / 24.0;
                dEnd += 1.0 + this.sunrise / 24.0;
            }
            double dPeriod = (dEnd - dStart) / 8.0;
            double dOffset = dPeriod / 2.0;

            double[] jds = new double[8];
            for (int i = 0; i < 8; i++)
                jds[i] = dStart + ((double)i * dPeriod) + dOffset;

            int[] bodyOffsets = new int[8];
            for (int i = 0; i < 8; i++)
            {
                int _ib = (int)bStart + i;
                while (_ib >= 8) _ib -= 8;
                bodyOffsets[_ib] = i;
            }

            double dUpagrahaOffset = 0;
            switch (options.UpagrahaType)
            {
                case EUpagrahaType.Begin:
                    dUpagrahaOffset = 0; break;
                case EUpagrahaType.Mid:
                    dUpagrahaOffset = dOffset; break;
                case EUpagrahaType.End:
                    dUpagrahaOffset = dPeriod; break;
            }

            Sweph.obtainLock(this);
            this.calculateUpagrahasSingle(Body.Name.Kala, jds[bodyOffsets[(int)Body.Name.Sun]]);
            this.calculateUpagrahasSingle(Body.Name.Mrityu, jds[bodyOffsets[(int)Body.Name.Mars]]);
            this.calculateUpagrahasSingle(Body.Name.ArthaPraharaka, jds[bodyOffsets[(int)Body.Name.Mercury]]);
            this.calculateUpagrahasSingle(Body.Name.YamaGhantaka, jds[bodyOffsets[(int)Body.Name.Jupiter]]);


            this.calculateMaandiHelper(Body.Name.Maandi, options.MaandiType, jds, dOffset, bodyOffsets);
            this.calculateMaandiHelper(Body.Name.Gulika, options.GulikaType, jds, dOffset, bodyOffsets);
            Sweph.releaseLock(this);
        }
        private void calculateSunsUpagrahas()
        {
            Longitude slon = this.getPosition(Body.Name.Sun).Longitude;

            BodyPosition bpDhuma = new BodyPosition(this, Body.Name.Dhuma, BodyType.Name.Upagraha,
                slon.add(133.0 + 20.0 / 60.0), 0, 0, 0, 0, 0);

            BodyPosition bpVyatipata = new BodyPosition(this, Body.Name.Vyatipata, BodyType.Name.Upagraha,
                new Longitude(360.0).sub(bpDhuma.Longitude), 0, 0, 0, 0, 0);

            BodyPosition bpParivesha = new BodyPosition(this, Body.Name.Parivesha, BodyType.Name.Upagraha,
                bpVyatipata.Longitude.add(180), 0, 0, 0, 0, 0);

            BodyPosition bpIndrachapa = new BodyPosition(this, Body.Name.Indrachapa, BodyType.Name.Upagraha,
                new Longitude(360.0).sub(bpParivesha.Longitude), 0, 0, 0, 0, 0);

            BodyPosition bpUpaketu = new BodyPosition(this, Body.Name.Upaketu, BodyType.Name.Upagraha,
                slon.sub(30), 0, 0, 0, 0, 0);

            positionList.Add(bpDhuma);
            positionList.Add(bpVyatipata);
            positionList.Add(bpParivesha);
            positionList.Add(bpIndrachapa);
            positionList.Add(bpUpaketu);
        }
        private void calculateWeekday()
        {
            Moment m = this.info.tob;
            double jd = Sweph.swe_julday(m.year, m.month, m.day, 12.0);
            if (info.tob.time < sunrise) jd -= 1;
            this.wday = (Basics.Weekday)Sweph.swe_day_of_week(jd);

            jd = Sweph.swe_julday(m.year, m.month, m.day, 12.0);
            if (info.tob.time < lmt_sunrise) jd -= 1;
            this.lmt_wday = (Basics.Weekday)Sweph.swe_day_of_week(jd);
        }

        private void addChandraLagna(string desc, Longitude lon)
        {
            BodyPosition bp = new BodyPosition(
                this, Body.Name.Other, BodyType.Name.ChandraLagna, lon, 0, 0, 0, 0, 0);
            bp.otherString = desc;
            this.positionList.Add(bp);
        }
        private void calculateChandraLagnas()
        {
            BodyPosition bp_moon = this.getPosition(Body.Name.Moon);
            Longitude lon_base =
                new Longitude(bp_moon.ExtrapolateLongitude(
                new Division(DivisionType.Navamsa)).toZodiacHouseBase());
            lon_base = lon_base.add(bp_moon.Longitude.toZodiacHouseOffset());

            //Console.WriteLine ("Starting Chandra Ayur Lagna from {0}", lon_base);

            double ista_ghati = Basics.Normalize_exc(0.0, 24.0, info.tob.time - sunrise) * 2.5;
            Longitude gl_lon = lon_base.add(new Longitude(ista_ghati * 30.0));
            Longitude hl_lon = lon_base.add(new Longitude(ista_ghati * 30.0 / 2.5));
            Longitude bl_lon = lon_base.add(new Longitude(ista_ghati * 30.0 / 5.0));

            double vl = ista_ghati * 5.0;
            while (ista_ghati > 12.0) ista_ghati -= 12.0;
            Longitude vl_lon = lon_base.add(new Longitude(vl * 30.0));

            this.addChandraLagna("Chandra Lagna - GL", gl_lon);
            this.addChandraLagna("Chandra Lagna - HL", hl_lon);
            this.addChandraLagna("Chandra Ayur Lagna - BL", bl_lon);
            this.addChandraLagna("Chandra Lagna - ViL", vl_lon);
        }

        private void calculateSL()
        {
            Longitude mpos = this.getPosition(Body.Name.Moon).Longitude;
            Longitude lpos = this.getPosition(Body.Name.Lagna).Longitude;
            double sldeg = mpos.toNakshatraOffset() / ((360.0) / 27.0) * 360.0;
            Longitude slLon = lpos.add(sldeg);
            BodyPosition bp = new BodyPosition(this, Body.Name.SreeLagna, BodyType.Name.SpecialLagna,
                slLon, 0, 0, 0, 0, 0);
            this.positionList.Add(bp);
        }
        private void calculatePranapada()
        {
            Longitude spos = this.getPosition(Body.Name.Sun).Longitude;
            double offset = this.info.tob.time - this.sunrise;
            if (offset < 0) offset += 24.0;
            offset *= (60.0 * 60.0 / 6.0);
            Longitude ppos = null;
            switch ((int)spos.toZodiacHouse().value % 3)
            {
                case 1: ppos = spos.add(offset); break;
                case 2: ppos = spos.add(offset + 8.0 * 30.0); break;
                default:
                case 0: ppos = spos.add(offset + 4.0 * 30.0); break;
            }
            BodyPosition bp = new BodyPosition(this, Body.Name.Pranapada, BodyType.Name.SpecialLagna,
                ppos, 0, 0, 0, 0, 0);
            this.positionList.Add(bp);
        }
        private void addOtherPoints()
        {
            Longitude lag_pos = this.getPosition(Body.Name.Lagna).Longitude;
            Longitude sun_pos = this.getPosition(Body.Name.Sun).Longitude;
            Longitude moon_pos = this.getPosition(Body.Name.Moon).Longitude;
            Longitude mars_pos = this.getPosition(Body.Name.Mars).Longitude;
            Longitude jup_pos = this.getPosition(Body.Name.Jupiter).Longitude;
            Longitude ven_pos = this.getPosition(Body.Name.Venus).Longitude;
            Longitude sat_pos = this.getPosition(Body.Name.Saturn).Longitude;
            Longitude rah_pos = this.getPosition(Body.Name.Rahu).Longitude;
            Longitude mandi_pos = this.getPosition(Body.Name.Maandi).Longitude;
            Longitude gulika_pos = this.getPosition(Body.Name.Gulika).Longitude;
            Longitude muhurta_pos = new Longitude(
                this.hoursAfterSunrise() / (this.next_sunrise + 24.0 - this.sunrise) * 360.0);

            // add simple midpoints
            this.addOtherPosition("User Specified", this.options.CustomBodyLongitude);
            this.addOtherPosition("Brighu Bindu", rah_pos.add(moon_pos.sub(rah_pos).value / 2.0));
            this.addOtherPosition("Muhurta Point", muhurta_pos);
            this.addOtherPosition("Ra-Ke m.p", rah_pos.add(90));
            this.addOtherPosition("Ke-Ra m.p", rah_pos.add(270));

            Longitude l1pos = this.getPosition(this.LordOfZodiacHouse(
                lag_pos.toZodiacHouse(), new Division(DivisionType.Rasi))).Longitude;
            Longitude l6pos = this.getPosition(this.LordOfZodiacHouse(
                lag_pos.toZodiacHouse().add(6), new Division(DivisionType.Rasi))).Longitude;
            Longitude l8pos = this.getPosition(this.LordOfZodiacHouse(
                lag_pos.toZodiacHouse().add(6), new Division(DivisionType.Rasi))).Longitude;
            Longitude l12pos = this.getPosition(this.LordOfZodiacHouse(
                lag_pos.toZodiacHouse().add(6), new Division(DivisionType.Rasi))).Longitude;

            Longitude mrit_sat_pos = new Longitude(mandi_pos.value * 8.0 + sat_pos.value * 8.0);
            Longitude mrit_jup2_pos = new Longitude(
                sat_pos.value * 9.0 + mandi_pos.value * 18.0 + jup_pos.value * 18.0);
            Longitude mrit_sun2_pos = new Longitude(
                sat_pos.value * 9.0 + mandi_pos.value * 18.0 + sun_pos.value * 18.0);
            Longitude mrit_moon2_pos = new Longitude(
                sat_pos.value * 9.0 + mandi_pos.value * 18.0 + moon_pos.value * 18.0);

            if (this.isDayBirth())
                this.addOtherPosition("Niryana: Su-Sa sum", sun_pos.add(sat_pos), Body.Name.MrityuPoint);
            else
                this.addOtherPosition("Niryana: Mo-Ra sum", moon_pos.add(rah_pos), Body.Name.MrityuPoint);

            this.addOtherPosition("Mrityu Sun: La-Mn sum", lag_pos.add(mandi_pos), Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Moon: Mo-Mn sum", moon_pos.add(mandi_pos), Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Lagna: La-Mo-Mn sum", lag_pos.add(moon_pos).add(mandi_pos), Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Sat: Mn8-Sa8", mrit_sat_pos, Body.Name.MrityuPoint);
            this.addOtherPosition("6-8-12 sum", l6pos.add(l8pos).add(l12pos), Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Jup: Sa9-Mn18-Ju18", mrit_jup2_pos, Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Sun: Sa9-Mn18-Su18", mrit_sun2_pos, Body.Name.MrityuPoint);
            this.addOtherPosition("Mrityu Moon: Sa9-Mn18-Mo18", mrit_moon2_pos, Body.Name.MrityuPoint);

            this.addOtherPosition("Su-Mo sum", sun_pos.add(moon_pos));
            this.addOtherPosition("Ju-Mo-Ma sum", jup_pos.add(moon_pos).add(mars_pos));
            this.addOtherPosition("Su-Ve-Ju sum", sun_pos.add(ven_pos).add(jup_pos));
            this.addOtherPosition("Sa-Mo-Ma sum", sat_pos.add(moon_pos).add(mars_pos));
            this.addOtherPosition("La-Gu-Sa sum", lag_pos.add(gulika_pos).add(sat_pos));
            this.addOtherPosition("L-MLBase sum", l1pos.add(moon_pos.toZodiacHouseBase()));
        }
        public void populateHouseCusps()
        {
            this.swephHouseCusps = new Longitude[13];
            double[] dCusps = new double[13];
            double[] ascmc = new double[10];

            Sweph.obtainLock(this);
            Sweph.swe_houses_ex(this.baseUT, Sweph.SEFLG_SIDEREAL,
                info.lat.toDouble(), info.lon.toDouble(), this.swephHouseSystem,
                dCusps, ascmc);
            Sweph.releaseLock(this);
            for (int i = 0; i < 12; i++)
                this.swephHouseCusps[i] = new Longitude(dCusps[i + 1]);

            if (this.options.BhavaType == EBhavaType.Middle)
            {
                Longitude middle = new Longitude((dCusps[1] + dCusps[2]) / 2.0);
                double offset = middle.sub(swephHouseCusps[0]).value;
                for (int i = 0; i < 12; i++)
                    swephHouseCusps[i] = swephHouseCusps[i].sub(offset);
            }


            this.swephHouseCusps[12] = this.swephHouseCusps[0];
        }
        private void populateCache()
        {
            // The stuff here is largely order sensitive
            // Try to add new definitions to the end

            baseUT = Sweph.swe_julday(info.tob.year, info.tob.month, info.tob.day,
                info.tob.time - info.tz.toDouble());


            Sweph.obtainLock(this);
            Sweph.swe_set_ephe_path(GlobalOptions.Instance.HOptions.EphemerisPath);
            // Find LMT offset
            this.populateLmt();
            // Sunrise (depends on lmt)
            populateSunrisetCache();
            // Basic grahas + Special lagnas (depend on sunrise)
            positionList = Basics.CalculateBodyPositions(this, this.sunrise);
            Sweph.releaseLock(this);
            // Srilagna etc
            this.calculateSL();
            this.calculatePranapada();
            // Sun based Upagrahas (depends on sun)
            this.calculateSunsUpagrahas();
            // Upagrahas (depends on sunrise)
            this.calculateUpagrahas();
            // Weekday (depends on sunrise)
            this.calculateWeekday();
            // Sahamas
            this.calculateSahamas();
            // Prana sphuta etc. (depends on upagrahas)
            this.getPrashnaMargaPositions();
            this.calculateChandraLagnas();
            this.addOtherPoints();
            // Add extrapolated special lagnas (depends on sunrise)
            this.addSpecialLagnaPositions();
            // Hora (depends on weekday)
            this.calculateHora();
            // Populate house cusps on options refresh
            this.populateHouseCusps();
        }
        public Horoscope(HoraInfo _info, HoroscopeOptions _options)
        {
            options = _options;
            info = _info;
            this.swephHouseSystem = 'P';
            this.populateCache();
            GlobalOptions.CalculationPrefsChanged += new EvtChanged(this.OnGlobalCalcPrefsChanged);
        }
        public double lengthOfDay()
        {
            return (this.next_sunrise + 24.0 - this.sunrise);

        }

        public double hoursAfterSunrise()
        {
            double ret = this.info.tob.time - this.sunrise;
            if (ret < 0) ret += 24.0;
            return ret;
        }
        public double hoursAfterSunRiseSet()
        {
            double ret = 0;
            if (this.isDayBirth())
                ret = this.info.tob.time - this.sunrise;
            else
                ret = this.info.tob.time - this.sunset;
            if (ret < 0) ret += 24.0;
            return ret;
        }
        public bool isDayBirth()
        {
            if (info.tob.time >= this.sunrise &&
                info.tob.time < this.sunset) return true;
            return false;
        }

        public void addOtherPosition(string desc, Longitude lon, Body.Name name)
        {
            BodyPosition bp = new BodyPosition(this, name, BodyType.Name.Other, lon, 0, 0, 0, 0, 0);
            bp.otherString = desc;
            this.positionList.Add(bp);
        }
        public void addOtherPosition(string desc, Longitude lon)
        {
            addOtherPosition(desc, lon, Body.Name.Other);
        }

        public void addSpecialLagnaPositions()
        {
            double diff = this.info.tob.time - this.sunrise;
            if (diff < 0) diff += 24.0;
            Sweph.obtainLock(this);
            for (int i = 1; i <= 12; i++)
            {
                double specialDiff = diff * (double)(i - 1);
                double tjd = this.baseUT + specialDiff / 24.0;
                double asc = Sweph.swe_lagna(tjd);
                string desc = String.Format("Special Lagna ({0:00})", i);
                this.addOtherPosition(desc, new Longitude(asc));
            }
            Sweph.releaseLock(this);
        }

        public void getPrashnaMargaPositions()
        {
            Longitude sunLon = this.getPosition(Body.Name.Sun).Longitude;
            Longitude moonLon = this.getPosition(Body.Name.Moon).Longitude;
            Longitude lagnaLon = this.getPosition(Body.Name.Lagna).Longitude;
            Longitude gulikaLon = this.getPosition(Body.Name.Gulika).Longitude;
            Longitude rahuLon = this.getPosition(Body.Name.Rahu).Longitude;

            Longitude trisLon = lagnaLon.add(moonLon).add(gulikaLon);
            Longitude chatusLon = trisLon.add(sunLon);
            Longitude panchasLon = chatusLon.add(rahuLon);
            Longitude pranaLon = new Longitude(lagnaLon.value * 5.0).add(gulikaLon);
            Longitude dehaLon = new Longitude(moonLon.value * 8.0).add(gulikaLon);
            Longitude mrityuLon = new Longitude(gulikaLon.value * 7.0).add(sunLon);

            this.addOtherPosition("Trih Sphuta", trisLon);
            this.addOtherPosition("Chatuh Sphuta", chatusLon);
            this.addOtherPosition("Panchah Sphuta", panchasLon);
            this.addOtherPosition("Pranah Sphuta", pranaLon);
            this.addOtherPosition("Deha Sphuta", dehaLon);
            this.addOtherPosition("Mrityu Sphuta", mrityuLon);

        }

        public BodyPosition getPosition(Body.Name b)
        {
            int index = Body.ToInt(b);
            System.Type t = positionList[index].GetType();
            String s = t.ToString();
            Trace.Assert(index >= 0 && index < positionList.Count, "Horoscope::getPosition 1");
            Trace.Assert(positionList[index].GetType() == typeof(BodyPosition), "Horoscope::getPosition 2");
            BodyPosition bp = (BodyPosition)positionList[Body.ToInt(b)];
            if (bp.name == b)
                return bp;

            for (int i = (int)Body.Name.Lagna + 1; i < positionList.Count; i++)
                if (b == ((BodyPosition)positionList[i]).name)
                    return (BodyPosition)positionList[i];

            Trace.Assert(false, "Basics::GetPosition. Unable to find body");
            return (BodyPosition)positionList[0];

        }
        private BodyPosition sahamaHelper(string sahama, Body.Name b, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonB, lonC;
            lonA = this.getPosition(a).Longitude;
            lonB = this.getPosition(b).Longitude;
            lonC = this.getPosition(c).Longitude;
            return this.sahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaHelper(string sahama, Body.Name b, Body.Name a, Longitude lonC)
        {
            Longitude lonA, lonB;
            lonA = this.getPosition(a).Longitude;
            lonB = this.getPosition(b).Longitude;
            return this.sahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaHelper(string sahama, Longitude lonB, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonC;
            lonA = this.getPosition(a).Longitude;
            lonC = this.getPosition(c).Longitude;
            return this.sahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaHelper(string sahama, Longitude lonB, Longitude lonA, Longitude lonC)
        {
            // b-a+c
            bool bDay = this.isDayBirth();

            Longitude lonR;
            lonR = lonB.sub(lonA).add(lonC);
            if (lonB.sub(lonA).value <= lonC.sub(lonA).value)
                lonR = lonR.add(new Longitude(30.0));

            BodyPosition bp = new BodyPosition(this, Body.Name.Other, BodyType.Name.Sahama, lonR,
                0.0, 0.0, 0.0, 0.0, 0.0);
            bp.otherString = sahama;
            return bp;
        }

        private BodyPosition sahamaDNHelper(string sahama, Longitude lonB, Longitude lonA, Longitude lonC)
        {
            // b-a+c
            bool bDay = this.isDayBirth();
            Longitude lonR;
            if (bDay)
                lonR = lonB.sub(lonA).add(lonC);
            else
                lonR = lonA.sub(lonB).add(lonC);

            if (lonB.sub(lonA).value <= lonC.sub(lonA).value)
                lonR = lonR.add(new Longitude(30.0));

            BodyPosition bp = new BodyPosition(this, Body.Name.Other, BodyType.Name.Sahama, lonR,
                0.0, 0.0, 0.0, 0.0, 0.0);
            bp.otherString = sahama;
            return bp;
        }
        private BodyPosition sahamaDNHelper(string sahama, Body.Name b, Longitude lonA, Body.Name c)
        {
            Longitude lonB, lonC;
            lonB = this.getPosition(b).Longitude;
            lonC = this.getPosition(c).Longitude;
            return sahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaDNHelper(string sahama, Longitude lonB, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonC;
            lonA = this.getPosition(a).Longitude;
            lonC = this.getPosition(c).Longitude;
            return sahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaDNHelper(string sahama, Longitude lonB, Longitude lonA, Body.Name c)
        {
            Longitude lonC;
            lonC = this.getPosition(c).Longitude;
            return sahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaDNHelper(string sahama, Body.Name b, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonB, lonC;
            lonA = this.getPosition(a).Longitude;
            lonB = this.getPosition(b).Longitude;
            lonC = this.getPosition(c).Longitude;
            return sahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition sahamaHelperNormalize(BodyPosition b, Body.Name lower, Body.Name higher)
        {
            Longitude lonA = this.getPosition(lower).Longitude;
            Longitude lonB = this.getPosition(higher).Longitude;
            if (b.Longitude.sub(lonA).value < lonB.sub(lonA).value) return b;
            b.Longitude = b.Longitude.add(new Longitude(30));
            return b;
        }
        public ArrayList calculateSahamas()
        {
            bool bDay = this.isDayBirth();
            ArrayList al = new ArrayList();
            Longitude lon_lagna = this.getPosition(Body.Name.Lagna).Longitude;
            Longitude lon_base = new Longitude(lon_lagna.toZodiacHouseBase());
            ZodiacHouse zh_lagna = lon_lagna.toZodiacHouse();
            ZodiacHouse zh_moon = this.getPosition(Body.Name.Moon).Longitude.toZodiacHouse();
            ZodiacHouse zh_sun = this.getPosition(Body.Name.Sun).Longitude.toZodiacHouse();


            // Fixed positions. Relied on by other sahams
            al.Add(sahamaDNHelper("Punya", Body.Name.Moon, Body.Name.Sun, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Vidya", Body.Name.Sun, Body.Name.Moon, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Sastra", Body.Name.Jupiter, Body.Name.Saturn, Body.Name.Mercury));

            // Variable positions.
            al.Add(sahamaDNHelper("Yasas", Body.Name.Jupiter, ((BodyPosition)al[0]).Longitude, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Mitra", Body.Name.Jupiter, ((BodyPosition)al[0]).Longitude, Body.Name.Venus));
            al.Add(sahamaDNHelper("Mahatmya", ((BodyPosition)al[0]).Longitude, Body.Name.Mars, Body.Name.Lagna));

            Body.Name bLagnaLord = this.LordOfZodiacHouse(zh_lagna, new Division(DivisionType.Rasi));
            if (bLagnaLord != Body.Name.Mars)
                al.Add(sahamaDNHelper("Samartha", Body.Name.Mars, bLagnaLord, Body.Name.Lagna));
            else
                al.Add(sahamaDNHelper("Samartha", Body.Name.Jupiter, Body.Name.Mars, Body.Name.Lagna));

            al.Add(sahamaHelper("Bhratri", Body.Name.Jupiter, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Gaurava", Body.Name.Jupiter, Body.Name.Moon, Body.Name.Sun));
            al.Add(sahamaDNHelper("Pitri", Body.Name.Saturn, Body.Name.Sun, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Rajya", Body.Name.Saturn, Body.Name.Sun, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Matri", Body.Name.Moon, Body.Name.Venus, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Putra", Body.Name.Jupiter, Body.Name.Moon, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Jeeva", Body.Name.Saturn, Body.Name.Jupiter, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Karma", Body.Name.Mars, Body.Name.Mercury, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Roga", Body.Name.Lagna, Body.Name.Moon, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Kali", Body.Name.Jupiter, Body.Name.Mars, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Bandhu", Body.Name.Mercury, Body.Name.Moon, Body.Name.Lagna));
            al.Add(sahamaHelper("Mrityu", lon_base.add(8.0 * 30.0), Body.Name.Moon, Body.Name.Lagna));
            al.Add(sahamaHelper("Paradesa", lon_base.add(9.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.add(9), new Division(DivisionType.Rasi)),
                Body.Name.Lagna));
            al.Add(sahamaHelper("Artha", lon_base.add(2.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.add(2), new Division(DivisionType.Rasi)),
                Body.Name.Lagna));
            al.Add(sahamaDNHelper("Paradara", Body.Name.Venus, Body.Name.Sun, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Vanik", Body.Name.Moon, Body.Name.Mercury, Body.Name.Lagna));

            if (bDay)
                al.Add(sahamaHelper("Karyasiddhi", Body.Name.Saturn, Body.Name.Sun,
                    this.LordOfZodiacHouse(zh_sun, new Division(DivisionType.Rasi))));
            else
                al.Add(sahamaHelper("Karyasiddhi", Body.Name.Saturn, Body.Name.Moon,
                    this.LordOfZodiacHouse(zh_moon, new Division(DivisionType.Rasi))));

            al.Add(sahamaDNHelper("Vivaha", Body.Name.Venus, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaHelper("Santapa", Body.Name.Saturn, Body.Name.Moon, lon_base.add(6.0 * 30.0)));
            al.Add(sahamaDNHelper("Sraddha", Body.Name.Venus, Body.Name.Mars, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Preeti",
                ((BodyPosition)al[2]).Longitude, ((BodyPosition)al[0]).Longitude, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Jadya", Body.Name.Mars, Body.Name.Saturn, Body.Name.Mercury));
            al.Add(sahamaHelper("Vyapara", Body.Name.Mars, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Satru", Body.Name.Mars, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Jalapatana", new Longitude(105), Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Bandhana", ((BodyPosition)al[0]).Longitude, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(sahamaDNHelper("Apamrityu", lon_base.add(8.0 * 30.0), Body.Name.Mars, Body.Name.Lagna));
            al.Add(sahamaHelper("Labha", lon_base.add(11.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.add(11), new Division(DivisionType.Rasi)), Body.Name.Lagna));

            this.positionList.AddRange(al);
            return al;
        }
    }
}
