using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class Retrogression
    {
        private Horoscope h;
        private Body.Name b;
        public Retrogression(Horoscope _h, Body.Name _b)
        {
            //Debug.Assert((int)_b >= (int)Body.Name.Moon &&
            //	(int)_b <= (int)Body.Name.Saturn, 
            //	string.Format("Retrogression::Retrogression. Invalid Body {0}", _b));
            h = _h;
            b = _b;
        }
        public void getRetroSolarCusps(ref Longitude start, ref Longitude end)
        {
            switch (b)
            {
                case Body.Name.Mars:
                    start.value = 211;
                    end.value = 232;
                    break;
                case Body.Name.Jupiter:
                    start.value = 240;
                    end.value = 248;
                    break;
                case Body.Name.Saturn:
                    start.value = 248;
                    end.value = 253;
                    break;
            }
        }
        public double gotoNextRetroSolarCusp(double ut)
        {
            return ut;
#if DND
			Longitude cusp_start = new Longitude(0);
			Longitude cusp_end = new Longitude(0);
			BodyPosition bp_sun = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(Body.Name.Sun), Body.Name.Sun, BodyType.Name.Other);
			BodyPosition bp_b = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other);
			Longitude diff = bp_b.longitude.sub(bp_sun.longitude);
			if (Transit.CircLonLessThan(cusp_start, diff) &&
				Transit.CircLonLessThan(diff, cusp_end))
				return ut;

			Longitude diffIncrease = diff.sub(cusp_start);
			double ret = ut + (diffIncrease.value * 360.0/365.2425);
			return ret;
#endif
        }
        public double FindClosestTransit(double ut, Longitude lonToFind)
        {
            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            while (Transit.CircLonLessThan(bp.Longitude, lonToFind))
            {
                //Console.WriteLine("- {0} {1}", bp.longitude.value, lonToFind.value);
                ut++;
                bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            }

            while (Transit.CircLonLessThan(lonToFind, bp.Longitude))
            {
                //Console.WriteLine("+ {0} {1}", bp.longitude.value, lonToFind.value);
                ut--;
                bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            }

            return ut;

        }
        public double GetTransitBackward(double ut, Longitude lonToFind)
        {
            if (this.b == Body.Name.Lagna)
                return GetLagnaTransitBackward(ut, lonToFind);

            bool becomesDirect = true;
            double ut_curr = ut;
            double ut_next = ut;

            while (true)
            {
                ut_curr = ut_next;

                double ut_start = ut_curr;
                if (ut_curr != ut) ut_start -= 5.0;
                ut_next = findNextCuspBackward(ut_start, ref becomesDirect);

                BodyPosition bp_next = Basics.CalculateSingleBodyPosition(ut_curr, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                BodyPosition bp_curr = Basics.CalculateSingleBodyPosition(ut_next, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                //Console.WriteLine ("{0}, {1}, {2}", becomesDirect, bp_curr.longitude, bp_next.longitude);

                if (false == becomesDirect &&
                    lonToFind.sub(bp_curr.Longitude).value <= bp_next.Longitude.sub(bp_curr.Longitude).value)
                {
                    //Console.WriteLine ("+ Found {0} between {1} and {2}", lonToFind, bp_curr.longitude, bp_next.longitude);
                    break;
                }
                else if (true == becomesDirect &&
                    lonToFind.sub(bp_next.Longitude).value <= bp_curr.Longitude.sub(bp_next.Longitude).value)
                {
                    //Console.WriteLine ("- Found {0} betweeen {1} and {2}", lonToFind, bp_next.longitude, bp_curr.longitude);
                    break;
                }
                else
                {
                    //Console.WriteLine ("{3} Didn't find {0} betweeen {1} and {2}", lonToFind, bp_curr.longitude, bp_next.longitude, ut_curr);
                }
            }

            if (false == becomesDirect)
                return this.BinaryLonSearch(ut_next, ut_curr, lonToFind, true);
            else
                return this.BinaryLonSearch(ut_next, ut_curr, lonToFind, false);
        }

        public double GetStep()
        {

            return 5.0;
        }
        public double GetLagnaTransitForward(double ut, Longitude lonToFind)
        {
            double ut_start = ut;
            double ut_end = ut;

            while (true)
            {
                ut_start = ut_end;
                ut_end = ut_start + (1.0 / 24.0);

                Longitude lon_start = this.GetLon(ut_start);
                Longitude lon_end = this.GetLon(ut_end);

                int day = 0, month = 0, year = 0;
                double hour = 0;
                Sweph.swe_revjul(ut_start, ref year, ref month, ref day, ref hour);
                Moment m = new Moment(year, month, day, hour);

                //Console.WriteLine ("F {3} Lagna search for {0} between {1} and {2}",
                //lonToFind, lon_start, lon_end, m);

                if (lonToFind.sub(lon_start).value <= lon_end.sub(lon_start).value)
                    break;
            }

            return this.BinaryLonSearch(ut_start, ut_end, lonToFind, true);
        }
        public double GetLagnaTransitBackward(double ut, Longitude lonToFind)
        {
            double ut_start = ut;
            double ut_end = ut;

            while (true)
            {
                ut_start = ut_end;
                ut_end = ut_start - (1.0 / 24.0);

                Longitude lon_start = this.GetLon(ut_start);
                Longitude lon_end = this.GetLon(ut_end);

                int day = 0, month = 0, year = 0;
                double hour = 0;
                Sweph.swe_revjul(ut_start, ref year, ref month, ref day, ref hour);
                Moment m = new Moment(year, month, day, hour);

                //Console.WriteLine ("B {3} Lagna search for {0} between {1} and {2}",
                //lonToFind, lon_start, lon_end, m);

                if (lonToFind.sub(lon_end).value <= lon_start.sub(lon_end).value)
                    break;
            }

            return this.BinaryLonSearch(ut_end, ut_start, lonToFind, true);
        }

        public double GetTransitForward(double ut, Longitude lonToFind)
        {
            if (this.b == Body.Name.Lagna)
                return GetLagnaTransitForward(ut, lonToFind);


            bool becomesDirect = true;
            double ut_curr = ut;
            double ut_next = ut;

            while (true)
            {
                ut_curr = ut_next;

                double ut_start = ut_curr;
                if (ut_curr != ut) ut_start += this.GetStep();
                ut_next = findNextCuspForward(ut_start, ref becomesDirect);

                BodyPosition bp_curr = Basics.CalculateSingleBodyPosition(ut_curr, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                BodyPosition bp_next = Basics.CalculateSingleBodyPosition(ut_next, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                //Console.WriteLine ("{0}, {1}, {2}", becomesDirect, bp_curr.longitude, bp_next.longitude);

                if (false == becomesDirect &&
                    lonToFind.sub(bp_curr.Longitude).value <= bp_next.Longitude.sub(bp_curr.Longitude).value)
                {
                    //Console.WriteLine ("+ Found {0} between {1} and {2}", lonToFind, bp_curr.longitude, bp_next.longitude);
                    break;
                }
                else if (true == becomesDirect &&
                    lonToFind.sub(bp_next.Longitude).value <= bp_curr.Longitude.sub(bp_next.Longitude).value)
                {
                    //Console.WriteLine ("- Found {0} betweeen {1} and {2}", lonToFind, bp_next.longitude, bp_curr.longitude);
                    break;
                }
                else
                {
                    //Console.WriteLine ("{3} Didn't find {0} betweeen {1} and {2}", lonToFind, bp_curr.longitude, bp_next.longitude, ut_curr);
                }
            }

            if (false == becomesDirect)
                return this.BinaryLonSearch(ut_curr, ut_next, lonToFind, true);
            else
                return this.BinaryLonSearch(ut_curr, ut_next, lonToFind, false);
        }
        public double GetSpeed(double ut)
        {
            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            return bp.Speed_longitude;
        }
        public Longitude GetLon(double ut, ref bool bForward)
        {
            if (this.b == Body.Name.Lagna)
                return new Longitude(Sweph.swe_lagna(ut));

            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            bForward = bp.Speed_longitude >= 0;
            return bp.Longitude;
        }
        public Longitude GetLon(double ut)
        {
            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            return bp.Longitude;
        }
        public double BinaryLonSearch(double ut_start, double ut_end, Longitude lon_to_find, bool normal)
        {
            if (Math.Abs(ut_end - ut_start) < (1.0 / (24.0 * 60.0 * 60.0 * 60.0 * 60.0)))
            {
                if (Transit.CircLonLessThan(GetLon(ut_start), lon_to_find))
                {
                    if (normal) return ut_end;
                    else return ut_start;
                }
                else
                {
                    if (normal) return ut_start;
                    else return ut_end;
                }
            }

            double ut_middle = (ut_start + ut_end) / 2.0;

            Longitude lon_start = GetLon(ut_start);
            Longitude lon_middle = GetLon(ut_middle);
            Longitude lon_end = GetLon(ut_end);

            if (normal)
            {
                if (lon_to_find.sub(lon_start).value <= lon_middle.sub(lon_start).value)
                    return BinaryLonSearch(ut_start, ut_middle, lon_to_find, normal);
                else
                    return BinaryLonSearch(ut_middle, ut_end, lon_to_find, normal);
            }
            else
            {
                if (lon_to_find.sub(lon_end).value <= lon_middle.sub(lon_end).value)
                    return BinaryLonSearch(ut_middle, ut_end, lon_to_find, normal);
                else
                    return BinaryLonSearch(ut_start, ut_middle, lon_to_find, normal);
            }

        }
        public double BinaryCuspSearch(double ut_start, double ut_end, bool normal)
        {
            if (Math.Abs(ut_end - ut_start) < (1.0 / (24.0 * 60.0 * 60.0 * 60.0)))
                return ut_start;

            double ut_middle = (ut_start + ut_end) / 2.0;
            double speed_start = GetSpeed(ut_start);
            double speed_middle = GetSpeed(ut_middle);
            double speed_end = GetSpeed(ut_end);

            //Console.WriteLine ("Speed BinarySearchNormal {0} UT: {2} Speed {1}", b, speed_middle, ut_middle);

            if (speed_start > 0 && speed_end < 0)
            {
                if (speed_middle > 0)
                    return BinaryCuspSearch(ut_middle, ut_end, normal);
                else
                    return BinaryCuspSearch(ut_start, ut_middle, normal);
            }

            if (speed_start < 0 && speed_end > 0)
            {
                if (speed_middle < 0)
                    return BinaryCuspSearch(ut_middle, ut_end, normal);
                else
                    return BinaryCuspSearch(ut_start, ut_middle, normal);
            }

            if (speed_start == 0)
                return ut_start;

            return ut_end;

        }

        public double findNextCuspBackward(double start_ut, ref bool becomesDirect)
        {
            double ut_step = 5.0;
            BodyPosition bp = Basics.CalculateSingleBodyPosition(start_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

            // Body is currently direct
            if (bp.Speed_longitude >= 0)
            {
                start_ut = gotoNextRetroSolarCusp(start_ut);
                double lower_ut = start_ut;
                double higher_ut = start_ut;
                becomesDirect = false;
                while (true)
                {
                    lower_ut = higher_ut;
                    higher_ut = lower_ut - ut_step;

                    // Find speeds
                    BodyPosition bp_l = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bp_h = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    //Console.WriteLine ("DChecking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.speed_longitude, bp_h.speed_longitude);
                    // If first one is retro, we're exactly at the cusp
                    // If higher is still direct, contine
                    if (bp_l.Speed_longitude < 0 && bp_h.Speed_longitude > 0) break;
                    if (bp_l.Speed_longitude > 0 && bp_h.Speed_longitude < 0) break;
                    //if (bp_l.speed_longitude < 0 && bp_h.speed_longitude < 0) 
                    //	return findNextCuspBackward (lower_ut, ref becomesDirect);
                }

                // Within one day period
                return BinaryCuspSearch(higher_ut, lower_ut, true);
            }

            // Body is current retrograde
            else
            {
                double lower_ut = start_ut;
                double higher_ut = start_ut;
                becomesDirect = true;
                while (true)
                {
                    lower_ut = higher_ut;
                    higher_ut = lower_ut - ut_step;
                    // Find speeds
                    BodyPosition bp_l = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bp_h = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    //Console.WriteLine ("R Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.speed_longitude, bp_h.speed_longitude);
                    if (bp_l.Speed_longitude > 0 && bp_h.Speed_longitude <= 0) break;
                    if (bp_l.Speed_longitude < 0 && bp_h.Speed_longitude > 0) break;
                    //if (bp_l.speed_longitude > 0 && bp_h.speed_longitude > 0)
                    //	return findNextCuspBackward (lower_ut, ref becomesDirect);
                }
                // Within one day period
                return BinaryCuspSearch(higher_ut, lower_ut, false);
            }
        }

        public double findNextCuspForward(double start_ut, ref bool becomesDirect)
        {
            double ut_step = 1.0;
            BodyPosition bp = Basics.CalculateSingleBodyPosition(start_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

            // Body is currently direct
            if (bp.Speed_longitude >= 0)
            {
                start_ut = gotoNextRetroSolarCusp(start_ut);
                double lower_ut = start_ut;
                double higher_ut = start_ut;
                becomesDirect = false;
                while (true)
                {
                    lower_ut = higher_ut;
                    higher_ut = lower_ut + ut_step;

                    // Find speeds
                    BodyPosition bp_l = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bp_h = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    //Console.WriteLine ("DChecking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.speed_longitude, bp_h.speed_longitude);
                    // If first one is retro, we're exactly at the cusp
                    // If higher is still direct, contine
                    if (bp_l.Speed_longitude > 0 && bp_h.Speed_longitude < 0) break;
                    if (bp_l.Speed_longitude < 0 && bp_h.Speed_longitude < 0)
                        return findNextCuspForward(lower_ut, ref becomesDirect);
                }

                // Within one day period
                return BinaryCuspSearch(lower_ut, higher_ut, true);
            }

            // Body is current retrograde
            else
            {
                double lower_ut = start_ut;
                double higher_ut = start_ut;
                becomesDirect = true;
                while (true)
                {
                    lower_ut = higher_ut;
                    higher_ut = lower_ut + ut_step;
                    // Find speeds
                    BodyPosition bp_l = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bp_h = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    //Console.WriteLine ("R Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.speed_longitude, bp_h.speed_longitude);
                    if (bp_l.Speed_longitude < 0 && bp_h.Speed_longitude >= 0) break;
                    if (bp_l.Speed_longitude > 0 && bp_h.Speed_longitude > 0)
                        return findNextCuspForward(lower_ut, ref becomesDirect);
                }
                // Within one day period
                return BinaryCuspSearch(lower_ut, higher_ut, false);
            }
        }

    }

}
