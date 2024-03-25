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
        public double value
        {
            get { return m_lon; }
            set
            {
                Trace.Assert(value >= 0 && value <= 360);
                m_lon = value;
            }
        }
        public Longitude add(Longitude b)
        {
            return new Longitude(Basics.normalize_exc_lower(0, 360, value + b.value));
        }
        public Longitude add(double b)
        {
            return this.add(new Longitude(b));
        }
        public Longitude sub(Longitude b)
        {
            return new Longitude(Basics.normalize_exc_lower(0, 360, value - b.value));
        }
        public Longitude sub(double b)
        {
            return this.sub(new Longitude(b));
        }
        public double normalize()
        {
            return Basics.normalize_exc_lower(0, 360, this.value);
        }
        public bool isBetween(Longitude cusp_lower, Longitude cusp_higher)
        {
            double diff1 = this.sub(cusp_lower).value;
            double diff2 = this.sub(cusp_higher).value;

            bool bRet = (cusp_higher.sub(cusp_lower).value <= 180) && diff1 <= diff2;

            Console.WriteLine("Is it true that {0} < {1} < {2}? {3}", this, cusp_lower, cusp_higher, bRet);
            return bRet;
        }
        public SunMoonYoga toSunMoonYoga()
        {
            int smIndex = (int)(Math.Floor(this.value / (360.0 / 27.0)) + 1);
            SunMoonYoga smYoga = new SunMoonYoga((SunMoonYogaName)smIndex);
            return smYoga;
        }
        public double toSunMoonYogaBase()
        {
            int num = (int)(toSunMoonYoga().value);
            double cusp = ((double)(num - 1)) * (360.0 / 27.0);
            return cusp;
        }
        public double toSunMoonYogaOffset()
        {
            return (this.value - this.toSunMoonYogaBase());
        }
        public Tithi toTithi()
        {
            int tIndex = (int)(Math.Floor(this.value / (360.0 / 30.0)) + 1);
            Tithi t = new Tithi((TithiName)tIndex);
            return t;
        }
        public Karana toKarana()
        {
            int kIndex = (int)(Math.Floor(this.value / (360.0 / 60.0)) + 1);
            Karana k = new Karana((KaranaName)kIndex);
            return k;
        }
        public double toKaranaBase()
        {
            int num = (int)(toKarana().value);
            double cusp = ((double)(num - 1)) * (360.0 / 60.0);
            return cusp;
        }
        public double toKaranaOffset()
        {
            return (this.value - this.toKaranaBase());
        }
        public double toTithiBase()
        {
            int num = (int)(toTithi().value);
            double cusp = ((double)(num - 1)) * (360.0 / 30.0);
            return cusp;
        }
        public double toTithiOffset()
        {
            return (this.value - this.toTithiBase());
        }
        public Nakshatra toNakshatra()
        {
            int snum = (int)((System.Math.Floor(this.value / (360.0 / 27.0))) + 1.0);
            return new Nakshatra((NakshatraName)snum);
        }
        public double toNakshatraBase()
        {
            int num = (int)(toNakshatra().value);
            double cusp = ((double)(num - 1)) * (360.0 / 27.0);
            return cusp;
        }
        public Nakshatra28 toNakshatra28()
        {
            int snum = (int)((System.Math.Floor(this.value / (360.0 / 27.0))) + 1.0);

            Nakshatra28 ret = new Nakshatra28((Nakshatra28Name)snum);
            if (snum >= (int)Nakshatra28Name.Abhijit)
                ret = ret.add(2);
            if (this.value >= 270 + (6.0 + 40.0 / 60.0) &&
                this.value <= 270 + (10.0 + 53.0 / 60.0 + 20.0 / 3600.0))
                ret.value = Nakshatra28Name.Abhijit;

            return ret;
        }
        public ZodiacHouse toZodiacHouse()
        {
            int znum = (int)(System.Math.Floor(this.value / 30.0) + 1.0);
            return new ZodiacHouse((ZodiacHouseName)znum);
        }
        public double toZodiacHouseBase()
        {
            int znum = (int)(toZodiacHouse().value);
            double cusp = ((double)(znum - 1)) * 30.0;
            return cusp;
        }
        public double toZodiacHouseOffset()
        {
            int znum = (int)(toZodiacHouse().value);
            double cusp = ((double)(znum - 1)) * 30.0;
            double ret = this.value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= 30.0);
            return ret;
        }
        public double percentageOfZodiacHouse()
        {
            double offset = toZodiacHouseOffset();
            double perc = offset / 30.0 * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public double toNakshatraOffset()
        {
            int znum = (int)(toNakshatra().value);
            double cusp = ((double)(znum - 1)) * (360.0 / 27.0);
            double ret = this.value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= (360.0 / 27.0));
            return ret;
        }
        public double percentageOfNakshatra()
        {
            double offset = toNakshatraOffset();
            double perc = offset / (360.0 / 27.0) * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public int toNakshatraPada()
        {
            double offset = toNakshatraOffset();
            int val = (int)Math.Floor(offset / (360.0 / (27.0 * 4.0))) + 1;
            Trace.Assert(val >= 1 && val <= 4);
            return val;
        }
        public int toAbsoluteNakshatraPada()
        {
            int n = (int)(this.toNakshatra()).value;
            int p = this.toNakshatraPada();
            return ((n - 1) * 4) + p;
        }
        public double toNakshatraPadaOffset()
        {
            int pnum = this.toAbsoluteNakshatraPada();
            double cusp = ((double)(pnum - 1)) * (360.0 / (27.0 * 4.0));
            double ret = this.value - cusp;
            Trace.Assert(ret >= 0.0 && ret <= (360.0 / 27.0));
            return ret;
        }
        public double toNakshatraPadaPercentage()
        {
            double offset = toNakshatraPadaOffset();
            double perc = offset / (360.0 / (27.0 * 4.0)) * 100;
            Trace.Assert(perc >= 0 && perc <= 100);
            return perc;
        }
        public override string ToString()
        {
            Longitude lon = this;
            string rasi = lon.toZodiacHouse().value.ToString();
            double offset = lon.toZodiacHouseOffset();
            double minutes = Math.Floor(offset);
            offset = (offset - minutes) * 60.0;
            double seconds = Math.Floor(offset);
            offset = (offset - seconds) * 60.0;
            double subsecs = Math.Floor(offset);
            offset = (offset - subsecs) * 60.0;
            double subsubsecs = Math.Floor(offset);

            return
                String.Format("{0:00} {1} {2:00}:{3:00}:{4:00}",
                minutes, rasi, seconds, subsecs, subsubsecs
                );
        }
    }


}
