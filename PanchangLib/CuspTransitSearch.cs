using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class CuspTransitSearch
    {
        Horoscope h = null;

        public CuspTransitSearch(Horoscope _h)
        {
            h = _h;
        }

        private double DirectSpeed(Body.Name b)
        {
            switch (b)
            {
                case Body.Name.Sun: return 365.2425;
                case Body.Name.Moon: return 28.0;
                case Body.Name.Lagna: return 1.0;
            }
            return 0.0;
        }
        public double TransitSearchDirect(Body.Name SearchBody, Moment StartDate,
            bool Forward, Longitude TransitPoint, Longitude FoundLon, ref bool bForward)
        {
            bool bDiscard = true;

            Sweph.obtainLock(h);
            Transit t = new Transit(h, SearchBody);
            double ut_base = StartDate.toUniversalTime() - h.info.TimeZone.toDouble() / 24.0;
            Longitude lon_curr = t.GenericLongitude(ut_base, ref bDiscard);
            Sweph.releaseLock(h);

            double diff = 0;
            diff = TransitPoint.sub(lon_curr).value;

            if (false == Forward)
            {
                diff -= 360.0;
            }

            double ut_diff_approx = diff / 360.0 * this.DirectSpeed(SearchBody);
            Sweph.obtainLock(h);
            double found_ut = 0;

            if (SearchBody == Body.Name.Lagna)
                found_ut = t.LinearSearchBinary(ut_base + ut_diff_approx - 3.0 / 24.0, ut_base + ut_diff_approx + 3.0 / 24.0, TransitPoint, new ReturnLon(t.GenericLongitude));
            else
                found_ut = t.LinearSearch(ut_base + ut_diff_approx, TransitPoint, new ReturnLon(t.GenericLongitude));
            FoundLon.value = t.GenericLongitude(found_ut, ref bForward).value;
            bForward = true;
            Sweph.releaseLock(h);
            return found_ut;
        }



        public double TransitSearch(Body.Name SearchBody, Moment StartDate,
            bool Forward, Longitude TransitPoint,
            Longitude FoundLon, ref bool bForward)
        {
            if (SearchBody == Body.Name.Sun ||
                SearchBody == Body.Name.Moon)
            {
                return TransitSearchDirect(SearchBody, StartDate, Forward, TransitPoint,
                    FoundLon, ref bForward);
            }
            if (((int)SearchBody <= (int)Body.Name.Moon ||
                (int)SearchBody > (int)Body.Name.Saturn) &&
                SearchBody != Body.Name.Lagna)
                return StartDate.toUniversalTime();
            Sweph.obtainLock(h);

            Retrogression r = new Retrogression(h, SearchBody);

            double julday_ut = StartDate.toUniversalTime() - h.info.tz.toDouble() / 24.0;
            double found_ut = julday_ut;

            if (Forward)
                found_ut = r.GetTransitForward(julday_ut, TransitPoint);
            else
                found_ut = r.GetTransitBackward(julday_ut, TransitPoint);

            FoundLon.value = r.GetLon(found_ut, ref bForward).value;

            Sweph.releaseLock(h);
            return found_ut;
        }
    }

}
