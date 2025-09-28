using System;
using System.ComponentModel;
using System.Diagnostics;

namespace org.transliteral.panchang
{
    [Serializable]
    [TypeConverter(typeof(LongitudeConverter))]
    public class Longitude
    {
        private double m_lon;
        public Longitude(double lon)
        {
            while (lon > 360.0) lon -= 360.0;
            while (lon < 0) lon += 360.0;
            m_lon = lon;
            //m_lon = Basics.normalize_exc (0, 360, lon);

        }
        public double Value
        {
            get { return m_lon; }
            set
            {
                Trace.Assert(value >= 0 && value <= 360);
                m_lon = value;
            }
        }
        public Longitude Add(Longitude b)
        {
            return new Longitude(Basics.NormalizeLower(0, 360, Value + b.Value));
        }
        public Longitude Add(double b)
        {
            return this.Add(new Longitude(b));
        }
        public Longitude Subtract(Longitude b)
        {
            return new Longitude(Basics.NormalizeLower(0, 360, Value - b.Value));
        }
        public Longitude Subtract(double b)
        {
            return this.Subtract(new Longitude(b));
        }
        public double Normalize()
        {
            return Basics.NormalizeLower(0, 360, this.Value);
        }
        public bool IsBetween(Longitude cusp_lower, Longitude cusp_higher)
        {
            double diff1 = this.Subtract(cusp_lower).Value;
            double diff2 = this.Subtract(cusp_higher).Value;

            bool bRet = (cusp_higher.Subtract(cusp_lower).Value <= 180) && diff1 <= diff2;

            Logger.Info(String.Format("Is it true that {0} < {1} < {2}? {3}", this, cusp_lower, cusp_higher, bRet));
            return bRet;
        }
        public SunMoonYoga ToSunMoonYoga()
        {
            int smIndex = (int)(Math.Floor(this.Value / (360.0 / 27.0)) + 1);
            SunMoonYoga smYoga = new SunMoonYoga((SunMoonYogaName)smIndex);
            return smYoga;
        }
        public double ToSunMoonYogaBase()
        {
            int num = (int)(ToSunMoonYoga().Value);
            double cusp = ((double)(num - 1)) * (360.0 / 27.0);
            return cusp;
        }
        public double ToSunMoonYogaOffset()
        {
            return (this.Value - this.ToSunMoonYogaBase());
        }
        public Tithi ToTithi()
        {
            int tIndex = (int)(Math.Floor(this.Value / (360.0 / 30.0)) + 1);
            Tithi t = new Tithi((TithiName)tIndex);
            return t;
        }
        public Karana ToKarana()
        {
            int kIndex = (int)(Math.Floor(this.Value / (360.0 / 60.0)) + 1);
            Karana k = new Karana((KaranaName)kIndex);
            return k;
        }
        public double ToKaranaBase()
        {
            int num = (int)(ToKarana().value);
            double cusp = ((double)(num - 1)) * (360.0 / 60.0);
            return cusp;
        }
        public double ToKaranaOffset()
        {
            return (this.Value - this.ToKaranaBase());
        }
        public double ToTithiBase()
        {
            int num = (int)(ToTithi().Value);
            double cusp = ((double)(num - 1)) * (360.0 / 30.0);
            return cusp;
        }
        public double ToTithiOffset()
        {
            return (this.Value - this.ToTithiBase());
        }
        public Nakshatra ToNakshatra()
        {
            int snum = (int)((System.Math.Floor(this.Value / (360.0 / 27.0))) + 1.0);
            return new Nakshatra((NakshatraName)snum);
        }
        public double ToNakshatraBase()
        {
            int num = (int)(ToNakshatra().Value);
            double cusp = ((double)(num - 1)) * (360.0 / 27.0);
            return cusp;
        }
        public Nakshatra28 ToNakshatra28() 
        {
            int snum = (int)((System.Math.Floor(this.Value / (360.0 / 27.0))) + 1.0);

            Nakshatra28 ret = new Nakshatra28((Nakshatra28Name)snum);
            if (snum >= (int)Nakshatra28Name.Abhijit)
                ret = ret.Add(2);
            if (this.Value >= 270 + (6.0 + 40.0 / 60.0) &&
                this.Value <= 270 + (10.0 + 53.0 / 60.0 + 20.0 / 3600.0))
                ret.Value = Nakshatra28Name.Abhijit;

            return ret;
        }
        public ZodiacHouse ToZodiacHouse()
        {
            int znum = (int)(System.Math.Floor(this.Value / 30.0) + 1.0);
            return new ZodiacHouse((ZodiacHouseName)znum);
        }
        public double ToZodiacHouseBase()
        {
            int znum = (int)(ToZodiacHouse().Value);
            double cusp = ((double)(znum - 1)) * 30.0;
            return cusp;
        }
        public double ToZodiacHouseOffset()
        {
            int znum = (int)(ToZodiacHouse().Value);
            double cusp = ((double)(znum - 1)) * 30.0;
            double ret = this.Value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= 30.0);
            return ret;
        }
        public double PercentageOfZodiacHouse() 
        {
            double offset = ToZodiacHouseOffset();
            double perc = offset / 30.0 * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public double ToNakshatraOffset()
        {
            int znum = (int)(ToNakshatra().Value);
            double cusp = ((double)(znum - 1)) * (360.0 / 27.0);
            double ret = this.Value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= (360.0 / 27.0));
            return ret;
        }
        public double PercentageOfNakshatra()
        {
            double offset = ToNakshatraOffset();
            double perc = offset / (360.0 / 27.0) * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public int ToNakshatraPada()
        {
            double offset = ToNakshatraOffset();
            int val = (int)Math.Floor(offset / (360.0 / (27.0 * 4.0))) + 1;
            Trace.Assert(val >= 1 && val <= 4);
            return val;
        }
        public int ToAbsoluteNakshatraPada()
        {
            int n = (int)(this.ToNakshatra()).Value;
            int p = this.ToNakshatraPada();
            return ((n - 1) * 4) + p;
        }
        public double ToNakshatraPadaOffset()
        {
            int pnum = this.ToAbsoluteNakshatraPada();
            double cusp = ((double)(pnum - 1)) * (360.0 / (27.0 * 4.0));
            double ret = this.Value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= (360.0 / 27.0));
            return ret;
        }
        public double ToNakshatraPadaPercentage() 
        {
            double offset = ToNakshatraPadaOffset();
            double perc = offset / (360.0 / (27.0 * 4.0)) * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public override string ToString()
        {
            Longitude lon = this;
            string rasi = lon.ToZodiacHouse().Value.ToString();
            double offset = lon.ToZodiacHouseOffset();
            double minutes = Math.Floor(offset);
            offset = (offset - minutes) * 60.0;
            double seconds = Math.Floor(offset);
            offset = (offset - seconds) * 60.0;
            double subsecs = Math.Floor(offset);
            offset = (offset - subsecs) * 60.0;
            double subsubsecs = Math.Floor(offset);

            return String.Format("{0:00} {1} {2:00}:{3:00}:{4:00}", minutes, rasi, seconds, subsecs, subsubsecs);
        }
    }


}
