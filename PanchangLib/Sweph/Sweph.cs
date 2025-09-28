using SwissEphNet;
using System;
using System.IO;
using System.Text;
namespace org.transliteral.panchang
{



    /// <summary>
    /// A Simple wrapper around the swiss ephemeris DLL functions
    /// Many function arguments use sane defaults for Jyotish programs
    /// For documentation go to http://www.astro.ch and follow the
    /// Swiss Ephemeris (for programmers) link.
    /// </summary>
    public class Sweph
    {
        public static int SEFLG_SWIEPH = SwissEph.SEFLG_SWIEPH;
        public static int SEFLG_TRUEPOS = SwissEph.SEFLG_TRUEPOS;
        public static int SEFLG_SPEED = SwissEph.SEFLG_SPEED;
        public static int SEFLG_SIDEREAL = SwissEph.SEFLG_SIDEREAL;

        public static int iflag = SEFLG_SWIEPH | SEFLG_SPEED | SEFLG_SIDEREAL;

        public static int SE_AYANAMSA_LAHIRI = SwissEph.SE_SIDM_LAHIRI;
        public static int SE_AYANAMSA_RAMAN = SwissEph.SE_SIDM_RAMAN;
        public static int SE_AYANAMSA_SURYASIDDHANTA = SwissEph.SE_SIDM_SURYASIDDHANTA;

        public static int ayanamsa = SE_AYANAMSA_LAHIRI;

        public static int SE_SUN = SwissEph.SE_SUN;
        public static int SE_MOON = SwissEph.SE_MOON;
        public static int SE_MERCURY = SwissEph.SE_MERCURY;
        public static int SE_VENUS = SwissEph.SE_VENUS;
        public static int SE_MARS = SwissEph.SE_MARS;
        public static int SE_JUPITER = SwissEph.SE_JUPITER;
        public static int SE_SATURN = SwissEph.SE_SATURN;
        public static int SE_MEAN_NODE = SwissEph.SE_MEAN_NODE;
        public static int SE_TRUE_NODE = SwissEph.SE_TRUE_NODE;

        public static int SE_CALC_RISE = SwissEph.SE_CALC_RISE;
        public static int SE_CALC_SET = SwissEph.SE_CALC_SET;
        public static int SE_CALC_MTRANSIT = SwissEph.SE_CALC_MTRANSIT;
        public static int SE_CALC_ITRANSIT = SwissEph.SE_CALC_ITRANSIT;
        public static int SE_BIT_DISC_CENTER = SwissEph.SE_BIT_DISC_CENTER;
        public static int SE_BIT_NO_REFRACTION = SwissEph.SE_BIT_NO_REFRACTION;
        public static int SE_BIT_HINDU_RISING = SwissEph.SE_BIT_HINDU_RISING;

        public static int SE_WK_MONDAY = 0;
        public static int SE_WK_TUESDAY = 1;
        public static int SE_WK_WEDNESDAY = 2;
        public static int SE_WK_THURSDAY = 3;
        public static int SE_WK_FRIDAY = 4;
        public static int SE_WK_SATURDAY = 5;
        public static int SE_WK_SUNDAY = 6;


        public static int SE_GREG_CAL = SwissEph.SE_GREG_CAL;

        private static Horoscope mCurrentLockHolder = null;
        private static Object SwephLockObject = null;
        private static SwissEph swissEphemerides = new SwissEph();


        public static void Initialize()
        {
            if (swissEphemerides == null)
            {
                swissEphemerides = new SwissEph();
                swissEphemerides.OnLoadFile += SwissEph_OnLoadFile;
            }
            
        }

        private static void SwissEph_OnLoadFile(object sender, LoadFileEventArgs e)
        {
            if (File.Exists(e.FileName))
            {
                Logger.Debug(String.Format($"{e.FileName} - Loaded"));
                e.File = File.OpenRead(e.FileName);
            }
            else
                Logger.Debug(String.Format($"{e.FileName} - Does not exist"));

        }

        public static void CheckLock()
        {
            Sweph.Initialize();
            lock (Sweph.SwephLockObject)
            {
                if (mCurrentLockHolder == null)
                    throw new Exception("Sweph: Unable to run. Sweph lock not obtained");
            }
        }
        public static void Lock(Horoscope h)
        {
            Sweph.Initialize();
            if (Sweph.SwephLockObject == null)
                Sweph.SwephLockObject = new Object();

            lock (Sweph.SwephLockObject)
            {
                if (mCurrentLockHolder != null)
                    throw new Exception("Sweph: obtainLock failed. Sweph Lock still held");

                Logger.Info("Sweph Lock obtained");
                mCurrentLockHolder = h;
                Sweph.SWE_SetSiderealMode((int)h.Options.Ayanamsa, 0.0, 0.0);
            }
        }
        public static void Unlock(Horoscope h)
        {
            if (mCurrentLockHolder == null)
                throw new Exception("Sweph: releaseLock failed. Lock not held");
            else if (mCurrentLockHolder != h)
                throw new Exception("Sweph: releaseLock failed. Not lock owner");
            Logger.Info("Sweph Lock released");
            mCurrentLockHolder = null;
        }

        public static int BodyNameToSweph(BodyName b)
        {
            switch (b)
            {
                case BodyName.Sun: return Sweph.SE_SUN;
                case BodyName.Moon: return Sweph.SE_MOON;
                case BodyName.Mars: return Sweph.SE_MARS;
                case BodyName.Mercury: return Sweph.SE_MERCURY;
                case BodyName.Jupiter: return Sweph.SE_JUPITER;
                case BodyName.Venus: return Sweph.SE_VENUS;
                case BodyName.Saturn: return Sweph.SE_SATURN;
                case BodyName.Lagna: return Sweph.SE_BIT_NO_REFRACTION;
                default:
                    throw new Exception();
            }
        }

      
        public static void SWE_SetEphemerisPath(string path) => swissEphemerides.swe_set_ephe_path(path);
       

        public static void SWE_SetSiderealMode(int sid_mode, double t0, double ayan_t0)
        {
            Sweph.CheckLock();
            swissEphemerides.swe_set_sid_mode(sid_mode, 0.0, 0.0);
        }

        public static double SWE_JullianDay(int year, int month, int day, double hour) 
            => swissEphemerides.swe_julday(year, month, day, hour, SwissEph.SE_GREG_CAL);
        

        public static double SWE_ReverseJulianDay(double tjd, ref int year, ref int month, ref int day, ref double hour)
        {
            swissEphemerides.swe_revjul(tjd,1,ref year,ref month,ref day,ref hour);
            return hour;
         }

        public static void SWE_CalculateUniversalTime(double tjd_ut, int ipl, int addFlags, double[] xx)
        {
            Sweph.CheckLock();
            string serr = "";
            int ret = swissEphemerides.swe_calc_ut(tjd_ut, ipl, iflag | addFlags, xx, ref serr);
            if (ret < 0)
            {
                Logger.Error(String.Format("Sweph Error: {0}", serr));
                throw new SwephException(serr.ToString());
            }
            xx[0] += Sweph.mCurrentLockHolder.Options.AyanamsaOffset.toDouble();
        }

        public static void SWE_FindSolarEclipseGlobally(double tjd_ut, double[] tret, bool forward)
        {
            Sweph.CheckLock();
            string serr = "";
            int ret = swissEphemerides.swe_sol_eclipse_when_glob(tjd_ut, iflag, 0, tret, !forward, ref serr);
            if (ret < 0)
            {
                Logger.Error(String.Format("Sweph Error: {0}", serr));
                throw new SwephException(serr.ToString());
            }
        }

        public static void SWE_FindSolarEclipseLocally(HoraInfo hi, double tjd_ut, double[] tret, double[] attr, bool forward)
        {
            Sweph.CheckLock();
            string serr = "";
            double[] geopos = new Double[3] { hi.lon.toDouble(), hi.lat.toDouble(), hi.alt };
            int ret = swissEphemerides.swe_sol_eclipse_when_loc(tjd_ut, iflag, geopos, tret, attr, !forward, ref serr);
            if (ret < 0)
            {
                Logger.Error(String.Format("Sweph Error: {0}", serr));
                throw new SwephException(serr.ToString());
            }
        }
        public static void SWE_FindLunarEclipse(double tjd_ut, double[] tret, bool forward)
        {
            Sweph.CheckLock();
             string serr = "";
            int ret = swissEphemerides.swe_lun_eclipse_when(tjd_ut, iflag, 0, tret, !forward, ref serr);
            if (ret < 0)
            {
                Logger.Error(String.Format("Sweph Error: {0}", serr));
                throw new SwephException(serr.ToString());
            }
        }
      

        private static int SWE_LunarOccultationWhenLocal(
           double tjd_ut, int ipl, ref string starname, int iflag,
           double[] geopos, double[] tret, double[] attr, bool backward, ref string s)
                 => swissEphemerides.swe_lun_occult_when_loc(tjd_ut, ipl, starname, iflag, geopos,tret, attr, backward, ref s);


        public static double SWE_GetAyanamsaUniversalTime(double tjd_ut)
        {
            Sweph.CheckLock();
            return swissEphemerides.swe_get_ayanamsa_ut(tjd_ut);
        }


       public static void SWE_Rise(double tjd_ut, int ipl, int rsflag, double[] geopos, double atpress, double attemp, double[] tret)
        {
            Sweph.CheckLock();
            string serr = "";
            int ret = swissEphemerides.swe_rise_trans(tjd_ut, ipl, "", iflag, SwissEph.SE_CALC_RISE | rsflag, geopos, atpress, attemp,ref tret[0],ref serr);

            if (ret < 0)
            {
                Logger.Error("Sweph: " + serr.ToString());
                throw new SwephException(serr.ToString());
            }
        }
        public static void SWE_Set(double tjd_ut, int ipl, int rsflag, double[] geopos, double atpress, double attemp, double[] tret)
        {
            Sweph.CheckLock();
            string serr = "";
            int ret = swissEphemerides.swe_rise_trans(tjd_ut, ipl, "", iflag, SwissEph.SE_CALC_SET | rsflag, geopos, atpress, attemp,ref tret[0],ref serr);
            if (ret < 0)
            {
                Logger.Error("Sweph: " + serr.ToString());
                throw new SwephException(serr.ToString());
            }
        }
        public static void SWE_LocalMeanTime(double tjd_ut, int ipl, int rsflag, double[] geopos, double atpress, double attemp, double[] tret)
        {
            Sweph.CheckLock();
            string serr = "";
            int ret = swissEphemerides.swe_rise_trans(tjd_ut, ipl, "", iflag, rsflag,geopos, atpress, attemp, ref tret[0],ref serr);

            if (ret < 0)
            {
                Logger.Error("Sweph: " + serr.ToString());
                throw new SwephException(serr.ToString());
            }
        }


        public static int SWE_HousesEx(double tjd_ut, int iflag, double lat, double lon, int hsys, double[] cusps, double[] ascmc)
        {
            Sweph.CheckLock();
            int ret = swissEphemerides.swe_houses_ex(tjd_ut, iflag, lat, lon, Convert.ToChar(hsys), cusps, ascmc);

            Longitude lOffset = new Longitude(Sweph.mCurrentLockHolder.Options.AyanamsaOffset.toDouble());

            // House cusps defined from 1 to 12 inclusive as per sweph docs
            // Ascendants defined from 0 to 7 inclusive as per sweph docs
            for (int i = 1; i <= 12; i++)
                cusps[i] = new Longitude(cusps[i]).Add(lOffset).Value;
            for (int i = 0; i <= 7; i++)
                ascmc[i] = new Longitude(ascmc[i]).Add(lOffset).Value;
            return ret;
        }

        public static double SWE_Lagna(double tjd_ut)
        {
            Sweph.CheckLock();
            HoraInfo hi = Sweph.mCurrentLockHolder.Info;
            double[] cusps = new double[13];
            double[] ascmc = new double[10];
            int ret = SWE_HousesEx(tjd_ut, SwissEph.SEFLG_SIDEREAL, hi.lat.toDouble(), hi.lon.toDouble(), 'R', cusps, ascmc);
            return ascmc[0];
        }

        public static int SWE_DayOfWeek(double jd)
            => swissEphemerides.swe_day_of_week(jd);

        public static double SWE_DeltaTime(double tjd_et)
            => swissEphemerides.swe_deltat(tjd_et);


        private static void SWE_SetTidalAcceleration(double t_acc)
            => swissEphemerides.swe_set_tid_acc(t_acc);
       
        public static int SWE_TimeEquation(double tjd_et, ref double e, StringBuilder s)
        {
            string serr = "";
            int ret = swissEphemerides.swe_time_equ(tjd_et, out e, ref serr);
            if (ret < 0)
            {
                Logger.Error("Sweph: " + serr.ToString());
                throw new SwephException(serr.ToString());
            }
            return ret;
        }

    }
}
