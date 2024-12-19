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
        public HoraInfo Info;
        public double Sunrise;
        public double Sunset;
        public double SunriseLMT;
        public double SunsetLMT;
        public double NextSunrise;
        public double NextSunset;
        public double Ayanamsa;
        public double lmt_offset;
        public double baseUT;
        public Basics.Weekday Weekday;
        public Basics.Weekday WeekdayLMT;
        public ArrayList PositionList;
        public Longitude[] SwephHouseCusps;
        public int SwephHouseSystem;
        public StrengthOptions StrengthOptions;
        private string[] VarnadaStars = new string[]
          {
                "VL", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10", "V11", "V12"
          };
        public HoroscopeOptions Options;
        public Body.Name[] KalaOrder = new Body.Name[]
           {
                Body.Name.Sun, Body.Name.Mars, Body.Name.Jupiter, Body.Name.Mercury,
                Body.Name.Venus, Body.Name.Saturn, Body.Name.Moon, Body.Name.Rahu
           };
        public Body.Name[] HoraOrder = new Body.Name[]
           {
                Body.Name.Sun, Body.Name.Venus, Body.Name.Mercury, Body.Name.Moon,
                Body.Name.Saturn, Body.Name.Jupiter, Body.Name.Mars
           };
        public Horoscope(HoraInfo _info, HoroscopeOptions _options)
        {
            Options = _options;
            Info = _info;
            this.SwephHouseSystem = 'P';
            this.PopulateCache();
            GlobalOptions.CalculationPrefsChanged += new EvtChanged(this.OnGlobalCalcPrefsChanged);
        }

        public Body.Name LordOfZodiacHouse(ZodiacHouse zh, Division dtype)
        {
            return LordOfZodiacHouse(zh.Value, dtype);
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
            Horoscope h = new Horoscope((HoraInfo)this.Info.Clone(), (HoroscopeOptions)this.Options.Clone());
            if (this.StrengthOptions != null)
                h.StrengthOptions = (StrengthOptions)this.StrengthOptions.Clone();
            return h;
        }
        public void OnGlobalCalcPrefsChanged(object o)
        {
            HoroscopeOptions ho = (HoroscopeOptions)o;
            this.Options.Copy(ho);
            this.StrengthOptions = null;
            this.OnChanged();
        }
        public void OnlySignalChanged()
        {
            if (Changed != null)
                Changed(this);
        }
        public void OnChanged()
        {
            PopulateCache();
            if (Changed != null)
                Changed(this);
        }
       
        public DivisionPosition CalculateDivisionPosition(BodyPosition bp, Division d)
        {
            return bp.ToDivisionPosition(d);
        }
        public ArrayList CalculateDivisionPositions(Division d)
        {
            ArrayList al = new ArrayList();
            foreach (BodyPosition bp in this.PositionList)
            {
                al.Add(CalculateDivisionPosition(bp, d));
            }
            return al;
        }
        private DivisionPosition CalculateGrahaArudhaDivisionPosition(Body.Name bn, ZodiacHouse zh, Division dtype)
        {
            DivisionPosition dp = GetPosition(bn).ToDivisionPosition(dtype);
            DivisionPosition dpl = GetPosition(Body.Name.Lagna).ToDivisionPosition(dtype);
            int rel = dp.ZodiacHouse.NumHousesBetween(zh);
            int hse = dpl.ZodiacHouse.NumHousesBetween(zh);
            ZodiacHouse zhsum = zh.Add(rel);
            int rel2 = dp.ZodiacHouse.NumHousesBetween(zhsum);
            if (rel2 == 1 || rel2 == 7)
                zhsum = zhsum.Add(10);
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
      
        public ArrayList CalculateVarnadaDivisionPositions(Division dtype)
        {
            ArrayList al = new ArrayList(12);
            ZodiacHouse _zh_l = this.GetPosition(Body.Name.Lagna).ToDivisionPosition(dtype).ZodiacHouse;
            ZodiacHouse _zh_hl = this.GetPosition(Body.Name.HoraLagna).ToDivisionPosition(dtype).ZodiacHouse;

            ZodiacHouse zh_ari = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zh_pis = new ZodiacHouse(ZodiacHouseName.Pis);
            for (int i = 1; i <= 12; i++)
            {
                ZodiacHouse zh_l = _zh_l.Add(i);
                ZodiacHouse zh_hl = _zh_hl.Add(i);

                int i_l = 0, i_hl = 0;
                if (zh_l.IsOdd()) i_l = zh_ari.NumHousesBetween(zh_l);
                else i_l = zh_pis.NumHousesBetweenReverse(zh_l);

                if (zh_hl.IsOdd()) i_hl = zh_ari.NumHousesBetween(zh_hl);
                else i_hl = zh_pis.NumHousesBetweenReverse(zh_hl);

                int sum = 0;
                if (zh_l.IsOdd() == zh_hl.IsOdd()) sum = i_l + i_hl;
                else sum = Math.Max(i_l, i_hl) - Math.Min(i_l, i_hl);

                ZodiacHouse zh_v = null;
                if (zh_l.IsOdd()) zh_v = zh_ari.Add(sum);
                else zh_v = zh_pis.AddReverse(sum);

                DivisionPosition div_pos = new DivisionPosition(Body.Name.Other, BodyType.Name.Varnada, zh_v, 0, 0, 0);
                div_pos.otherString = VarnadaStars[i - 1];
                al.Add(div_pos);
            }
            return al;
        }
        private DivisionPosition CalculateArudhaDivisionPosition(ZodiacHouse zh, Body.Name bn,
            Body.Name aname, Division d, BodyType.Name btype)
        {
            BodyPosition bp = GetPosition(bn);
            ZodiacHouse zhb = CalculateDivisionPosition(bp, d).ZodiacHouse;
            int rel = zh.NumHousesBetween(zhb);
            ZodiacHouse zhsum = zhb.Add(rel);
            int rel2 = zh.NumHousesBetween(zhsum);
            if (rel2 == 1 || rel2 == 7)
                zhsum = zhsum.Add(10);
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
                ZodiacHouse zlagna = this.CalculateDivisionPosition(this.GetPosition(Body.Name.Lagna), d).ZodiacHouse;
                ZodiacHouse zh = zlagna.Add(j);
                Body.Name bn_stronger, bn_weaker = Body.Name.Other;
                bn_stronger = Basics.SimpleLordOfZodiacHouse(zh.Value);
                if (zh.Value == ZodiacHouseName.Aqu)
                {
                    bn_stronger = fs_colord.StrongerGraha(Body.Name.Rahu, Body.Name.Saturn, true);
                    bn_weaker = fs_colord.WeakerGraha(Body.Name.Rahu, Body.Name.Saturn, true);
                }
                else if (zh.Value == ZodiacHouseName.Sco)
                {
                    bn_stronger = fs_colord.StrongerGraha(Body.Name.Ketu, Body.Name.Mars, true);
                    bn_weaker = fs_colord.WeakerGraha(Body.Name.Ketu, Body.Name.Mars, true);
                }
                first = CalculateArudhaDivisionPosition(zh, bn_stronger, bnlist[j], d, BodyType.Name.BhavaArudha);
                arudha_div_list.Add(first);
                if (zh.Value == ZodiacHouseName.Aqu || zh.Value == ZodiacHouseName.Sco)
                {
                    second = CalculateArudhaDivisionPosition(zh, bn_weaker, bnlist[j], d, BodyType.Name.BhavaArudhaSecondary);
                    if (first.ZodiacHouse.Value != second.ZodiacHouse.Value)
                        arudha_div_list.Add(second);
                }
            }
            return arudha_div_list;
        }
        public object UpdateHoraInfo(Object o)
        {
            HoraInfo i = (HoraInfo)o;
            Info.DateOfBirth = i.DateOfBirth;
            Info.Altitude = i.Altitude;
            Info.Latitude = i.Latitude;
            Info.Longitude = i.Longitude;
            Info.tz = i.tz;
            Info.Events = (UserEvent[])i.Events.Clone();
            OnChanged();
            return Info.Clone();
        }
        private void PopulateLmt()
        {
            this.lmt_offset = GetLmtOffset(Info, baseUT);
            this.SunriseLMT = 6.0 + lmt_offset * 24.0;
            this.SunsetLMT = 18.0 + lmt_offset * 24.0;
        }
        public double GetLmtOffsetDays(HoraInfo info, double _baseUT)
        {
            double ut_lmt_noon = this.GetLmtOffset(info, _baseUT);
            double ut_noon = this.baseUT - info.tob.Time / 24.0 + 12.0 / 24.0;
            return ut_lmt_noon - ut_noon;
        }
        public double GetLmtOffset(HoraInfo _info, double _baseUT)
        {
            double[] geopos = new Double[3] { _info.lon.toDouble(), _info.lat.toDouble(), _info.alt };
            double[] tret = new Double[6] { 0, 0, 0, 0, 0, 0 };
            double midnight_ut = _baseUT - _info.tob.Time / 24.0;
            Sweph.swe_lmt(midnight_ut, Sweph.SE_SUN, Sweph.SE_CALC_MTRANSIT, geopos, 0.0, 0.0, tret);
            double lmt_noon_1 = tret[0];
            double lmt_offset_1 = lmt_noon_1 - (midnight_ut + 12.0 / 24.0);
            Sweph.swe_lmt(midnight_ut, Sweph.SE_SUN, Sweph.SE_CALC_MTRANSIT, geopos, 0.0, 0.0, tret);
            double lmt_noon_2 = tret[0];
            double lmt_offset_2 = lmt_noon_2 - (midnight_ut + 12.0 / 24.0);

            double ret_lmt_offset = (lmt_offset_1 + lmt_offset_2) / 2.0;
            Logger.Info(String.Format("LMT: {0}, {1}", lmt_offset_1, lmt_offset_2));

            return ret_lmt_offset;
            #if DND
			// determine offset from ephemeris time
			lmt_offset = 0;
			double tjd_et = baseUT + Sweph.swe_deltat(baseUT);
			System.Text.StringBuilder s = new System.Text.StringBuilder(256);
			int ret = Sweph.swe_time_equ(tjd_et, ref lmt_offset, s);
            #endif
        }
        private void PopulateSunrisetCache()
        {
            double sunrise_ut = 0.0;
            this.PopulateSunrisetCacheHelper(this.baseUT, ref this.NextSunrise, ref this.NextSunset, ref sunrise_ut);
            this.PopulateSunrisetCacheHelper(sunrise_ut - 1.0 - (1.0 / 24.0), ref this.Sunrise, ref this.Sunset, ref sunrise_ut);
          
            Logger.Info("Sunrise[t]: " + this.Sunrise.ToString() + " " + this.Sunrise.ToString() + "Basics");
        }
        public void PopulateSunrisetCacheHelper(double ut, ref double sr, ref double ss, ref double sr_ut)
        {
            int srflag = 0;
            switch (Options.SunrisePosition)
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

                    double[] geopos = new Double[3] { this.Info.lon.toDouble(), this.Info.lat.toDouble(), this.Info.alt };
                    double[] tret = new Double[6] { 0, 0, 0, 0, 0, 0 };

                    Sweph.swe_rise(ut, Sweph.SE_SUN, srflag, geopos, 0.0, 0.0, tret);
                    sr_ut = tret[0];
                    Sweph.swe_revjul(tret[0], ref year, ref month, ref day, ref hour);
                    sr = hour + this.Info.tz.toDouble();
                    Sweph.swe_set(tret[0], Sweph.SE_SUN, srflag, geopos, 0.0, 0.0, tret);
                    Sweph.swe_revjul(tret[0], ref year, ref month, ref day, ref hour);
                    ss = hour + this.Info.tz.toDouble();
                    sr = Basics.Normalize_exc(0.0, 24.0, sr);
                    ss = Basics.Normalize_exc(0.0, 24.0, ss);
                    break;
            }
        }
        public double[] GetHoraCuspsUt()
        {
            double[] cusps = null;
            switch (this.Options.HoraType)
            {
                case EHoraType.Sunriset:
                    cusps = this.GetSunrisetCuspsUt(12);
                    break;
                case EHoraType.SunrisetEqual:
                    cusps = this.GetSunrisetEqualCuspsUt(12);
                    break;
                case EHoraType.Lmt:
                    Sweph.ObtainLock(this);
                    cusps = this.GetLmtCuspsUt(12);
                    Sweph.ReleaseLock(this);
                    break;
            }
            return cusps;
        }
        public double[] GetKalaCuspsUt()
        {
            double[] cusps = null;
            switch (this.Options.KalaType)
            {
                case EHoraType.Sunriset:
                    cusps = this.GetSunrisetCuspsUt(8);
                    break;
                case EHoraType.SunrisetEqual:
                    cusps = this.GetSunrisetEqualCuspsUt(8);
                    break;
                case EHoraType.Lmt:
                    Sweph.ObtainLock(this);
                    cusps = this.GetLmtCuspsUt(8);
                    Sweph.ReleaseLock(this);
                    break;
            }
            return cusps;
        }
        public double[] GetSunrisetCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];

            double sr_ut = this.baseUT - this.HoursAfterSunrise() / 24.0;
            double ss_ut = sr_ut - this.Sunrise / 24.0 + this.Sunset / 24.0;
            double sr_next_ut = sr_ut - this.Sunrise / 24.0 + this.NextSunrise / 24.0 + 1.0;

            double day_span = (ss_ut - sr_ut) / dayParts;
            double night_span = (sr_next_ut - ss_ut) / dayParts;

            for (int i = 0; i < dayParts; i++)
                ret[i] = sr_ut + day_span * i;
            for (int i = 0; i <= dayParts; i++)
                ret[i + dayParts] = ss_ut + night_span * i;
            return ret;
        }
        public double[] GetSunrisetEqualCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];

            double sr_ut = this.baseUT - this.HoursAfterSunrise() / 24.0;
            double sr_next_ut = sr_ut - this.Sunrise / 24.0 + this.NextSunrise / 24.0 + 1.0;
            double span = (sr_next_ut - sr_ut) / (dayParts * 2);

            for (int i = 0; i <= (dayParts * 2); i++)
                ret[i] = sr_ut + span * i;
            return ret;
        }

        public double[] GetLmtCuspsUt(int dayParts)
        {
            double[] ret = new double[dayParts * 2 + 1];
            double sr_lmt_ut = this.baseUT - this.HoursAfterSunrise() / 24.0 - this.Sunrise / 24.0 + 6.0 / 24.0;
            double sr_lmt_next_ut = sr_lmt_ut + 1.0;
            //double sr_lmt_ut = this.baseUT - this.info.tob.time / 24.0 + 6.0 / 24.0;
            //double sr_lmt_next_ut = sr_lmt_ut + 1.0;

            double lmt_offset = this.GetLmtOffset(this.Info, this.baseUT);
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
        public Body.Name CalculateKala()
        {
            int iBase = 0;
            return CalculateKala(ref iBase);
        }
        public Body.Name CalculateKala(ref int iBase)
        {
            int[] offsets_day = new int[] { 0, 6, 1, 3, 2, 4, 5 };
            Body.Name b = Basics.WeekdayRuler(this.Weekday);
            bool bday_birth = this.IsDayBirth();

            double[] cusps = this.GetKalaCuspsUt();
            if (this.Options.KalaType == EHoraType.Lmt)
            {
                b = Basics.WeekdayRuler(this.WeekdayLMT);
                bday_birth =
                    this.Info.tob.Time > this.SunsetLMT ||
                    this.Info.tob.Time < this.SunriseLMT;
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
                return KalaOrder[i];
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
                return KalaOrder[i];
            }

        }

       
        public Body.Name CalculateHora()
        {
            int iBody = 0;
            return this.CalculateHora(this.baseUT, ref iBody);
        }
        public Body.Name CalculateHora(double _baseUT, ref int baseBody)
        {
            int[] offsets = new int[] { 0, 3, 6, 2, 5, 1, 4 };
            Body.Name b = Basics.WeekdayRuler(this.Weekday);
            double[] cusps = this.GetHoraCuspsUt();
            if (this.Options.HoraType == EHoraType.Lmt)
                b = Basics.WeekdayRuler(this.WeekdayLMT);

            int i = offsets[(int)b];
            baseBody = i;
            int j = 0;
            //for (j=0; j<23; j++)
            //{
            //	Moment m1 = new Moment(cusps[j], this);
            //	Moment m2 = new Moment(cusps[j+1], this);
            //	Logger.Info(String.Format(("Seeing if dob is between {0} and {1}", m1, m2));
            //}
            for (j = 0; j < 23; j++)
            {
                if (_baseUT >= cusps[j] && _baseUT < cusps[j + 1])
                    break;
            }
            Logger.Info(String.Format("Found hora in the {0}th hora", j));
            i += j;
            while (i >= 7) i -= 7;
            return HoraOrder[i];
        }
        private Body.Name CalculateUpagrahasStart()
        {
            if (this.IsDayBirth())
                return Basics.WeekdayRuler(this.Weekday);

            switch (this.Weekday)
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

        private void CalculateUpagrahasSingle(Body.Name b, double tjd)
        {
            Longitude lon = new Longitude(0);
            lon.Value = Sweph.swe_lagna(tjd);
            BodyPosition bp = new BodyPosition(this, b, BodyType.Name.Upagraha,
                lon, 0, 0, 0, 0, 0);
            PositionList.Add(bp);
        }

        private void CalculateMaandiHelper(Body.Name b, EMaandiType mty, double[] jds, double dOffset, int[] bodyOffsets)
        {
            switch (mty)
            {
                case EMaandiType.SaturnBegin:
                    this.CalculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]]);
                    break;
                case EMaandiType.SaturnMid:
                    this.CalculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + dOffset);
                    break;
                case EMaandiType.SaturnEnd:
                case EMaandiType.LordlessBegin:
                    int _off1 = bodyOffsets[(int)Body.Name.Saturn] + 1;
                    this.CalculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 2.0));
                    break;
                case EMaandiType.LordlessMid:
                    this.CalculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 3.0));
                    break;
                case EMaandiType.LordlessEnd:
                    this.CalculateUpagrahasSingle(b, jds[bodyOffsets[(int)Body.Name.Saturn]] + (dOffset * 4.0));
                    break;
            }
        }
        private void CalculateUpagrahas()
        {

            double dStart = 0, dEnd = 0;

            Moment m = this.Info.tob;
            dStart = dEnd = Sweph.swe_julday(m.Year, m.Month, m.Day, -this.Info.tz.toDouble());
            Body.Name bStart = this.CalculateUpagrahasStart();

            if (this.IsDayBirth())
            {
                dStart += this.Sunrise / 24.0;
                dEnd += this.Sunset / 24.0;
            }
            else
            {
                dStart += this.Sunset / 24.0;
                dEnd += 1.0 + this.Sunrise / 24.0;
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
            switch (Options.UpagrahaType)
            {
                case EUpagrahaType.Begin:
                    dUpagrahaOffset = 0; break;
                case EUpagrahaType.Mid:
                    dUpagrahaOffset = dOffset; break;
                case EUpagrahaType.End:
                    dUpagrahaOffset = dPeriod; break;
            }

            Sweph.ObtainLock(this);
            this.CalculateUpagrahasSingle(Body.Name.Kala, jds[bodyOffsets[(int)Body.Name.Sun]]);
            this.CalculateUpagrahasSingle(Body.Name.Mrityu, jds[bodyOffsets[(int)Body.Name.Mars]]);
            this.CalculateUpagrahasSingle(Body.Name.ArthaPraharaka, jds[bodyOffsets[(int)Body.Name.Mercury]]);
            this.CalculateUpagrahasSingle(Body.Name.YamaGhantaka, jds[bodyOffsets[(int)Body.Name.Jupiter]]);


            this.CalculateMaandiHelper(Body.Name.Maandi, Options.MaandiType, jds, dOffset, bodyOffsets);
            this.CalculateMaandiHelper(Body.Name.Gulika, Options.GulikaType, jds, dOffset, bodyOffsets);
            Sweph.ReleaseLock(this);
        }
        private void CalculateSunsUpagrahas()
        {
            Longitude slon = this.GetPosition(Body.Name.Sun).Longitude;

            BodyPosition bpDhuma = new BodyPosition(this, Body.Name.Dhuma, BodyType.Name.Upagraha,
                slon.Add(133.0 + 20.0 / 60.0), 0, 0, 0, 0, 0);

            BodyPosition bpVyatipata = new BodyPosition(this, Body.Name.Vyatipata, BodyType.Name.Upagraha,
                new Longitude(360.0).Subtract(bpDhuma.Longitude), 0, 0, 0, 0, 0);

            BodyPosition bpParivesha = new BodyPosition(this, Body.Name.Parivesha, BodyType.Name.Upagraha,
                bpVyatipata.Longitude.Add(180), 0, 0, 0, 0, 0);

            BodyPosition bpIndrachapa = new BodyPosition(this, Body.Name.Indrachapa, BodyType.Name.Upagraha,
                new Longitude(360.0).Subtract(bpParivesha.Longitude), 0, 0, 0, 0, 0);

            BodyPosition bpUpaketu = new BodyPosition(this, Body.Name.Upaketu, BodyType.Name.Upagraha,
                slon.Subtract(30), 0, 0, 0, 0, 0);

            PositionList.Add(bpDhuma);
            PositionList.Add(bpVyatipata);
            PositionList.Add(bpParivesha);
            PositionList.Add(bpIndrachapa);
            PositionList.Add(bpUpaketu);
        }
        private void CalculateWeekday()
        {
            Moment m = this.Info.tob;
            double jd = Sweph.swe_julday(m.Year, m.Month, m.Day, 12.0);
            if (Info.tob.Time < Sunrise) jd -= 1;
            this.Weekday = (Basics.Weekday)Sweph.swe_day_of_week(jd);

            jd = Sweph.swe_julday(m.Year, m.Month, m.Day, 12.0);
            if (Info.tob.Time < SunriseLMT) jd -= 1;
            this.WeekdayLMT = (Basics.Weekday)Sweph.swe_day_of_week(jd);
        }
        private void AddChandraLagna(string desc, Longitude lon)
        {
            BodyPosition bp = new BodyPosition(
                this, Body.Name.Other, BodyType.Name.ChandraLagna, lon, 0, 0, 0, 0, 0);
            bp.otherString = desc;
            this.PositionList.Add(bp);
        }
        private void CalculateChandraLagnas()
        {
            BodyPosition bp_moon = this.GetPosition(Body.Name.Moon);
            Longitude lon_base =
                new Longitude(bp_moon.ExtrapolateLongitude(
                new Division(DivisionType.Navamsa)).ToZodiacHouseBase());
            lon_base = lon_base.Add(bp_moon.Longitude.ToZodiacHouseOffset());

            Logger.Info(String.Format("Starting Chandra Ayur Lagna from {0}", lon_base));

            double ista_ghati = Basics.Normalize_exc(0.0, 24.0, Info.tob.Time - Sunrise) * 2.5;
            Longitude gl_lon = lon_base.Add(new Longitude(ista_ghati * 30.0));
            Longitude hl_lon = lon_base.Add(new Longitude(ista_ghati * 30.0 / 2.5));
            Longitude bl_lon = lon_base.Add(new Longitude(ista_ghati * 30.0 / 5.0));

            double vl = ista_ghati * 5.0;
            while (ista_ghati > 12.0) ista_ghati -= 12.0;
            Longitude vl_lon = lon_base.Add(new Longitude(vl * 30.0));

            this.AddChandraLagna("Chandra Lagna - GL", gl_lon);
            this.AddChandraLagna("Chandra Lagna - HL", hl_lon);
            this.AddChandraLagna("Chandra Ayur Lagna - BL", bl_lon);
            this.AddChandraLagna("Chandra Lagna - ViL", vl_lon);
        }

        private void CalculateSL()
        {
            Longitude mpos = this.GetPosition(Body.Name.Moon).Longitude;
            Longitude lpos = this.GetPosition(Body.Name.Lagna).Longitude;
            double sldeg = mpos.ToNakshatraOffset() / ((360.0) / 27.0) * 360.0;
            Longitude slLon = lpos.Add(sldeg);
            BodyPosition bp = new BodyPosition(this, Body.Name.SreeLagna, BodyType.Name.SpecialLagna,
                slLon, 0, 0, 0, 0, 0);
            this.PositionList.Add(bp);
        }
        private void CalculatePranapada()
        {
            Longitude spos = this.GetPosition(Body.Name.Sun).Longitude;
            double offset = this.Info.tob.Time - this.Sunrise;
            if (offset < 0) offset += 24.0;
            offset *= (60.0 * 60.0 / 6.0);
            Longitude ppos = null;
            switch ((int)spos.ToZodiacHouse().Value % 3)
            {
                case 1: ppos = spos.Add(offset); break;
                case 2: ppos = spos.Add(offset + 8.0 * 30.0); break;
                default:
                case 0: ppos = spos.Add(offset + 4.0 * 30.0); break;
            }
            BodyPosition bp = new BodyPosition(this, Body.Name.Pranapada, BodyType.Name.SpecialLagna,
                ppos, 0, 0, 0, 0, 0);
            this.PositionList.Add(bp);
        }
        private void AddOtherPoints()
        {
            Longitude lag_pos = this.GetPosition(Body.Name.Lagna).Longitude;
            Longitude sun_pos = this.GetPosition(Body.Name.Sun).Longitude;
            Longitude moon_pos = this.GetPosition(Body.Name.Moon).Longitude;
            Longitude mars_pos = this.GetPosition(Body.Name.Mars).Longitude;
            Longitude jup_pos = this.GetPosition(Body.Name.Jupiter).Longitude;
            Longitude ven_pos = this.GetPosition(Body.Name.Venus).Longitude;
            Longitude sat_pos = this.GetPosition(Body.Name.Saturn).Longitude;
            Longitude rah_pos = this.GetPosition(Body.Name.Rahu).Longitude;
            Longitude mandi_pos = this.GetPosition(Body.Name.Maandi).Longitude;
            Longitude gulika_pos = this.GetPosition(Body.Name.Gulika).Longitude;
            Longitude muhurta_pos = new Longitude(
                this.HoursAfterSunrise() / (this.NextSunrise + 24.0 - this.Sunrise) * 360.0);

            // add simple midpoints
            this.AddOtherPosition("User Specified", this.Options.CustomBodyLongitude);
            this.AddOtherPosition("Brighu Bindu", rah_pos.Add(moon_pos.Subtract(rah_pos).Value / 2.0));
            this.AddOtherPosition("Muhurta Point", muhurta_pos);
            this.AddOtherPosition("Ra-Ke m.p", rah_pos.Add(90));
            this.AddOtherPosition("Ke-Ra m.p", rah_pos.Add(270));

            Longitude l1pos = this.GetPosition(this.LordOfZodiacHouse(
                lag_pos.ToZodiacHouse(), new Division(DivisionType.Rasi))).Longitude;
            Longitude l6pos = this.GetPosition(this.LordOfZodiacHouse(
                lag_pos.ToZodiacHouse().Add(6), new Division(DivisionType.Rasi))).Longitude;
            Longitude l8pos = this.GetPosition(this.LordOfZodiacHouse(
                lag_pos.ToZodiacHouse().Add(6), new Division(DivisionType.Rasi))).Longitude;
            Longitude l12pos = this.GetPosition(this.LordOfZodiacHouse(
                lag_pos.ToZodiacHouse().Add(6), new Division(DivisionType.Rasi))).Longitude;

            Longitude mrit_sat_pos = new Longitude(mandi_pos.Value * 8.0 + sat_pos.Value * 8.0);
            Longitude mrit_jup2_pos = new Longitude(
                sat_pos.Value * 9.0 + mandi_pos.Value * 18.0 + jup_pos.Value * 18.0);
            Longitude mrit_sun2_pos = new Longitude(
                sat_pos.Value * 9.0 + mandi_pos.Value * 18.0 + sun_pos.Value * 18.0);
            Longitude mrit_moon2_pos = new Longitude(
                sat_pos.Value * 9.0 + mandi_pos.Value * 18.0 + moon_pos.Value * 18.0);

            if (this.IsDayBirth())
                this.AddOtherPosition("Niryana: Su-Sa sum", sun_pos.Add(sat_pos), Body.Name.MrityuPoint);
            else
                this.AddOtherPosition("Niryana: Mo-Ra sum", moon_pos.Add(rah_pos), Body.Name.MrityuPoint);

            this.AddOtherPosition("Mrityu Sun: La-Mn sum", lag_pos.Add(mandi_pos), Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Moon: Mo-Mn sum", moon_pos.Add(mandi_pos), Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Lagna: La-Mo-Mn sum", lag_pos.Add(moon_pos).Add(mandi_pos), Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Sat: Mn8-Sa8", mrit_sat_pos, Body.Name.MrityuPoint);
            this.AddOtherPosition("6-8-12 sum", l6pos.Add(l8pos).Add(l12pos), Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Jup: Sa9-Mn18-Ju18", mrit_jup2_pos, Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Sun: Sa9-Mn18-Su18", mrit_sun2_pos, Body.Name.MrityuPoint);
            this.AddOtherPosition("Mrityu Moon: Sa9-Mn18-Mo18", mrit_moon2_pos, Body.Name.MrityuPoint);

            this.AddOtherPosition("Su-Mo sum", sun_pos.Add(moon_pos));
            this.AddOtherPosition("Ju-Mo-Ma sum", jup_pos.Add(moon_pos).Add(mars_pos));
            this.AddOtherPosition("Su-Ve-Ju sum", sun_pos.Add(ven_pos).Add(jup_pos));
            this.AddOtherPosition("Sa-Mo-Ma sum", sat_pos.Add(moon_pos).Add(mars_pos));
            this.AddOtherPosition("La-Gu-Sa sum", lag_pos.Add(gulika_pos).Add(sat_pos));
            this.AddOtherPosition("L-MLBase sum", l1pos.Add(moon_pos.ToZodiacHouseBase()));
        }
        public void PopulateHouseCusps()
        {
            this.SwephHouseCusps = new Longitude[13];
            double[] dCusps = new double[13];
            double[] ascmc = new double[10];

            Sweph.ObtainLock(this);
            Sweph.swe_houses_ex(this.baseUT, Sweph.SEFLG_SIDEREAL,
                Info.lat.toDouble(), Info.lon.toDouble(), this.SwephHouseSystem,
                dCusps, ascmc);
            Sweph.ReleaseLock(this);
            for (int i = 0; i < 12; i++)
                this.SwephHouseCusps[i] = new Longitude(dCusps[i + 1]);

            if (this.Options.BhavaType == EBhavaType.Middle)
            {
                Longitude middle = new Longitude((dCusps[1] + dCusps[2]) / 2.0);
                double offset = middle.Subtract(SwephHouseCusps[0]).Value;
                for (int i = 0; i < 12; i++)
                    SwephHouseCusps[i] = SwephHouseCusps[i].Subtract(offset);
            }


            this.SwephHouseCusps[12] = this.SwephHouseCusps[0];
        }
        private void PopulateCache()
        {
            // The stuff here is largely order sensitive
            // Try to add new definitions to the end

            baseUT = Sweph.swe_julday(Info.tob.Year, Info.tob.Month, Info.tob.Day,
                Info.tob.Time - Info.tz.toDouble());


            Sweph.ObtainLock(this);
            Sweph.swe_set_ephe_path(GlobalOptions.Instance.HOptions.EphemerisPath);
            // Find LMT offset
            this.PopulateLmt();
            // Sunrise (depends on lmt)
            PopulateSunrisetCache();
            // Basic grahas + Special lagnas (depend on sunrise)
            PositionList = Basics.CalculateBodyPositions(this, this.Sunrise);
            Sweph.ReleaseLock(this);
            // Srilagna etc
            this.CalculateSL();
            this.CalculatePranapada();
            // Sun based Upagrahas (depends on sun)
            this.CalculateSunsUpagrahas();
            // Upagrahas (depends on sunrise)
            this.CalculateUpagrahas();
            // Weekday (depends on sunrise)
            this.CalculateWeekday();
            // Sahamas
            this.CalculateSahamas();
            // Prana sphuta etc. (depends on upagrahas)
            this.GetPrashnaMargaPositions();
            this.CalculateChandraLagnas();
            this.AddOtherPoints();
            // Add extrapolated special lagnas (depends on sunrise)
            this.AddSpecialLagnaPositions();
            // Hora (depends on weekday)
            this.CalculateHora();
            // Populate house cusps on options refresh
            this.PopulateHouseCusps();
        }

        public double LengthOfDay() => this.NextSunrise + 24.0 - this.Sunrise;

        public double HoursAfterSunrise()
        {
            double ret = this.Info.tob.Time - this.Sunrise;
            if (ret < 0) ret += 24.0;
            return ret;
        }
        public double HoursAfterSunRiseSet()
        {
            double ret = 0;
            if (this.IsDayBirth())
                ret = this.Info.tob.Time - this.Sunrise;
            else
                ret = this.Info.tob.Time - this.Sunset;
            if (ret < 0) ret += 24.0;
            return ret;
        }
        public bool IsDayBirth()
        {
            if (Info.tob.Time >= this.Sunrise &&
                Info.tob.Time < this.Sunset) return true;
            return false;
        }

        public void AddOtherPosition(string desc, Longitude lon, Body.Name name)
        {
            BodyPosition bp = new BodyPosition(this, name, BodyType.Name.Other, lon, 0, 0, 0, 0, 0);
            bp.otherString = desc;
            this.PositionList.Add(bp);
        }
        public void AddOtherPosition(string desc, Longitude lon)
        {
            AddOtherPosition(desc, lon, Body.Name.Other);
        }

        public void AddSpecialLagnaPositions()
        {
            double diff = this.Info.tob.Time - this.Sunrise;
            if (diff < 0) diff += 24.0;
            Sweph.ObtainLock(this);
            for (int i = 1; i <= 12; i++)
            {
                double specialDiff = diff * (double)(i - 1);
                double tjd = this.baseUT + specialDiff / 24.0;
                double asc = Sweph.swe_lagna(tjd);
                string desc = String.Format("Special Lagna ({0:00})", i);
                this.AddOtherPosition(desc, new Longitude(asc));
            }
            Sweph.ReleaseLock(this);
        }

        public void GetPrashnaMargaPositions()
        {
            Longitude sunLon = this.GetPosition(Body.Name.Sun).Longitude;
            Longitude moonLon = this.GetPosition(Body.Name.Moon).Longitude;
            Longitude lagnaLon = this.GetPosition(Body.Name.Lagna).Longitude;
            Longitude gulikaLon = this.GetPosition(Body.Name.Gulika).Longitude;
            Longitude rahuLon = this.GetPosition(Body.Name.Rahu).Longitude;

            Longitude trisLon = lagnaLon.Add(moonLon).Add(gulikaLon);
            Longitude chatusLon = trisLon.Add(sunLon);
            Longitude panchasLon = chatusLon.Add(rahuLon);
            Longitude pranaLon = new Longitude(lagnaLon.Value * 5.0).Add(gulikaLon);
            Longitude dehaLon = new Longitude(moonLon.Value * 8.0).Add(gulikaLon);
            Longitude mrityuLon = new Longitude(gulikaLon.Value * 7.0).Add(sunLon);

            this.AddOtherPosition("Trih Sphuta", trisLon);
            this.AddOtherPosition("Chatuh Sphuta", chatusLon);
            this.AddOtherPosition("Panchah Sphuta", panchasLon);
            this.AddOtherPosition("Pranah Sphuta", pranaLon);
            this.AddOtherPosition("Deha Sphuta", dehaLon);
            this.AddOtherPosition("Mrityu Sphuta", mrityuLon);

        }

        public BodyPosition GetPosition(Body.Name b)
        {
            int index = Body.ToInt(b);
            System.Type t = PositionList[index].GetType();
            String s = t.ToString();
            Trace.Assert(index >= 0 && index < PositionList.Count, "Horoscope::getPosition 1");
            Trace.Assert(PositionList[index].GetType() == typeof(BodyPosition), "Horoscope::getPosition 2");
            BodyPosition bp = (BodyPosition)PositionList[Body.ToInt(b)];
            if (bp.name == b)
                return bp;

            for (int i = (int)Body.Name.Lagna + 1; i < PositionList.Count; i++)
                if (b == ((BodyPosition)PositionList[i]).name)
                    return (BodyPosition)PositionList[i];

            Trace.Assert(false, "Basics::GetPosition. Unable to find body");
            return (BodyPosition)PositionList[0];

        }
        private BodyPosition SahamaHelper(string sahama, Body.Name b, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonB, lonC;
            lonA = this.GetPosition(a).Longitude;
            lonB = this.GetPosition(b).Longitude;
            lonC = this.GetPosition(c).Longitude;
            return this.SahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaHelper(string sahama, Body.Name b, Body.Name a, Longitude lonC)
        {
            Longitude lonA, lonB;
            lonA = this.GetPosition(a).Longitude;
            lonB = this.GetPosition(b).Longitude;
            return this.SahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaHelper(string sahama, Longitude lonB, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonC;
            lonA = this.GetPosition(a).Longitude;
            lonC = this.GetPosition(c).Longitude;
            return this.SahamaHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaHelper(string sahama, Longitude lonB, Longitude lonA, Longitude lonC)
        {
            // b-a+c
            bool bDay = this.IsDayBirth();

            Longitude lonR;
            lonR = lonB.Subtract(lonA).Add(lonC);
            if (lonB.Subtract(lonA).Value <= lonC.Subtract(lonA).Value)
                lonR = lonR.Add(new Longitude(30.0));

            BodyPosition bp = new BodyPosition(this, Body.Name.Other, BodyType.Name.Sahama, lonR,
                0.0, 0.0, 0.0, 0.0, 0.0);
            bp.otherString = sahama;
            return bp;
        }

        private BodyPosition SahamaDNHelper(string sahama, Longitude lonB, Longitude lonA, Longitude lonC)
        {
            // b-a+c
            bool bDay = this.IsDayBirth();
            Longitude lonR;
            if (bDay)
                lonR = lonB.Subtract(lonA).Add(lonC);
            else
                lonR = lonA.Subtract(lonB).Add(lonC);

            if (lonB.Subtract(lonA).Value <= lonC.Subtract(lonA).Value)
                lonR = lonR.Add(new Longitude(30.0));

            BodyPosition bp = new BodyPosition(this, Body.Name.Other, BodyType.Name.Sahama, lonR,
                0.0, 0.0, 0.0, 0.0, 0.0);
            bp.otherString = sahama;
            return bp;
        }
        private BodyPosition SahamaDNHelper(string sahama, Body.Name b, Longitude lonA, Body.Name c)
        {
            Longitude lonB, lonC;
            lonB = this.GetPosition(b).Longitude;
            lonC = this.GetPosition(c).Longitude;
            return SahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaDNHelper(string sahama, Longitude lonB, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonC;
            lonA = this.GetPosition(a).Longitude;
            lonC = this.GetPosition(c).Longitude;
            return SahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaDNHelper(string sahama, Longitude lonB, Longitude lonA, Body.Name c)
        {
            Longitude lonC;
            lonC = this.GetPosition(c).Longitude;
            return SahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaDNHelper(string sahama, Body.Name b, Body.Name a, Body.Name c)
        {
            Longitude lonA, lonB, lonC;
            lonA = this.GetPosition(a).Longitude;
            lonB = this.GetPosition(b).Longitude;
            lonC = this.GetPosition(c).Longitude;
            return SahamaDNHelper(sahama, lonB, lonA, lonC);
        }
        private BodyPosition SahamaHelperNormalize(BodyPosition b, Body.Name lower, Body.Name higher)
        {
            Longitude lonA = this.GetPosition(lower).Longitude;
            Longitude lonB = this.GetPosition(higher).Longitude;
            if (b.Longitude.Subtract(lonA).Value < lonB.Subtract(lonA).Value) return b;
            b.Longitude = b.Longitude.Add(new Longitude(30));
            return b;
        }
        public ArrayList CalculateSahamas()
        {
            bool bDay = this.IsDayBirth();
            ArrayList al = new ArrayList();
            Longitude lon_lagna = this.GetPosition(Body.Name.Lagna).Longitude;
            Longitude lon_base = new Longitude(lon_lagna.ToZodiacHouseBase());
            ZodiacHouse zh_lagna = lon_lagna.ToZodiacHouse();
            ZodiacHouse zh_moon = this.GetPosition(Body.Name.Moon).Longitude.ToZodiacHouse();
            ZodiacHouse zh_sun = this.GetPosition(Body.Name.Sun).Longitude.ToZodiacHouse();


            // Fixed positions. Relied on by other sahams
            al.Add(SahamaDNHelper("Punya", Body.Name.Moon, Body.Name.Sun, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Vidya", Body.Name.Sun, Body.Name.Moon, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Sastra", Body.Name.Jupiter, Body.Name.Saturn, Body.Name.Mercury));

            // Variable positions.
            al.Add(SahamaDNHelper("Yasas", Body.Name.Jupiter, ((BodyPosition)al[0]).Longitude, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Mitra", Body.Name.Jupiter, ((BodyPosition)al[0]).Longitude, Body.Name.Venus));
            al.Add(SahamaDNHelper("Mahatmya", ((BodyPosition)al[0]).Longitude, Body.Name.Mars, Body.Name.Lagna));

            Body.Name bLagnaLord = this.LordOfZodiacHouse(zh_lagna, new Division(DivisionType.Rasi));
            if (bLagnaLord != Body.Name.Mars)
                al.Add(SahamaDNHelper("Samartha", Body.Name.Mars, bLagnaLord, Body.Name.Lagna));
            else
                al.Add(SahamaDNHelper("Samartha", Body.Name.Jupiter, Body.Name.Mars, Body.Name.Lagna));

            al.Add(SahamaHelper("Bhratri", Body.Name.Jupiter, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Gaurava", Body.Name.Jupiter, Body.Name.Moon, Body.Name.Sun));
            al.Add(SahamaDNHelper("Pitri", Body.Name.Saturn, Body.Name.Sun, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Rajya", Body.Name.Saturn, Body.Name.Sun, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Matri", Body.Name.Moon, Body.Name.Venus, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Putra", Body.Name.Jupiter, Body.Name.Moon, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Jeeva", Body.Name.Saturn, Body.Name.Jupiter, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Karma", Body.Name.Mars, Body.Name.Mercury, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Roga", Body.Name.Lagna, Body.Name.Moon, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Kali", Body.Name.Jupiter, Body.Name.Mars, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Bandhu", Body.Name.Mercury, Body.Name.Moon, Body.Name.Lagna));
            al.Add(SahamaHelper("Mrityu", lon_base.Add(8.0 * 30.0), Body.Name.Moon, Body.Name.Lagna));
            al.Add(SahamaHelper("Paradesa", lon_base.Add(9.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.Add(9), new Division(DivisionType.Rasi)),
                Body.Name.Lagna));
            al.Add(SahamaHelper("Artha", lon_base.Add(2.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.Add(2), new Division(DivisionType.Rasi)),
                Body.Name.Lagna));
            al.Add(SahamaDNHelper("Paradara", Body.Name.Venus, Body.Name.Sun, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Vanik", Body.Name.Moon, Body.Name.Mercury, Body.Name.Lagna));

            if (bDay)
                al.Add(SahamaHelper("Karyasiddhi", Body.Name.Saturn, Body.Name.Sun,
                    this.LordOfZodiacHouse(zh_sun, new Division(DivisionType.Rasi))));
            else
                al.Add(SahamaHelper("Karyasiddhi", Body.Name.Saturn, Body.Name.Moon,
                    this.LordOfZodiacHouse(zh_moon, new Division(DivisionType.Rasi))));

            al.Add(SahamaDNHelper("Vivaha", Body.Name.Venus, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaHelper("Santapa", Body.Name.Saturn, Body.Name.Moon, lon_base.Add(6.0 * 30.0)));
            al.Add(SahamaDNHelper("Sraddha", Body.Name.Venus, Body.Name.Mars, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Preeti",
                ((BodyPosition)al[2]).Longitude, ((BodyPosition)al[0]).Longitude, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Jadya", Body.Name.Mars, Body.Name.Saturn, Body.Name.Mercury));
            al.Add(SahamaHelper("Vyapara", Body.Name.Mars, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Satru", Body.Name.Mars, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Jalapatana", new Longitude(105), Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Bandhana", ((BodyPosition)al[0]).Longitude, Body.Name.Saturn, Body.Name.Lagna));
            al.Add(SahamaDNHelper("Apamrityu", lon_base.Add(8.0 * 30.0), Body.Name.Mars, Body.Name.Lagna));
            al.Add(SahamaHelper("Labha", lon_base.Add(11.0 * 30.0),
                this.LordOfZodiacHouse(zh_lagna.Add(11), new Division(DivisionType.Rasi)), Body.Name.Lagna));

            this.PositionList.AddRange(al);
            return al;
        }
    }
}
