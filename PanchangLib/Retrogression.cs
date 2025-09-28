using System;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Represents the retrogression of a celestial body in a horoscope.
    /// </summary>
    public class Retrogression
    {
        private Horoscope h;
        private BodyName b;
        public Retrogression(Horoscope _h, BodyName _b)
        {
            //Debug.Assert((int)_b >= (int)Body.Name.Moon &&
            //	(int)_b <= (int)Body.Name.Saturn, 
            //	string.Format("Retrogression::Retrogression. Invalid Body {0}", _b));
            h = _h;
            b = _b;
        }
        public void GetRetroSolarCusps(ref Longitude start, ref Longitude end)
        {
            switch (b)
            {
                case BodyName.Mars:
                    start.Value = 211;
                    end.Value = 232;
                    break;
                case BodyName.Jupiter:
                    start.Value = 240;
                    end.Value = 248;
                    break;
                case BodyName.Saturn:
                    start.Value = 248;
                    end.Value = 253;
                    break;
            }
        }
        public double GotoNextRetroSolarCusp(double ut)
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
                Logger.Info(String.Format("- {0} {1}", bp.Longitude.Value, lonToFind.Value));
                ut++;
                bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            }

            while (Transit.CircLonLessThan(lonToFind, bp.Longitude))
            {
                Logger.Info(String.Format("+ {0} {1}", bp.Longitude.Value, lonToFind.Value));
                ut--;
                bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            }

            return ut;

        }
        public double GetTransitBackward(double ut, Longitude lonToFind)
        {
            if (this.b == BodyName.Lagna)
                return GetLagnaTransitBackward(ut, lonToFind);

            bool becomesDirect = true;
            double ut_curr = ut;
            double ut_next = ut;

            while (true)
            {
                ut_curr = ut_next;

                double ut_start = ut_curr;
                if (ut_curr != ut) ut_start -= 5.0;
                ut_next = FindNextCuspBackward(ut_start, ref becomesDirect);

                BodyPosition bp_next = Basics.CalculateSingleBodyPosition(ut_curr, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                BodyPosition bp_curr = Basics.CalculateSingleBodyPosition(ut_next, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                Logger.Info(String.Format("{0}, {1}, {2}", becomesDirect, bp_curr.Longitude, bp_next.Longitude));

                if (false == becomesDirect &&
                    lonToFind.Subtract(bp_curr.Longitude).Value <= bp_next.Longitude.Subtract(bp_curr.Longitude).Value)
                {
                    Logger.Info(String.Format("+ Found {0} between {1} and {2}", lonToFind, bp_curr.Longitude, bp_next.Longitude));
                    break;
                }
                else if (true == becomesDirect &&
                    lonToFind.Subtract(bp_next.Longitude).Value <= bp_curr.Longitude.Subtract(bp_next.Longitude).Value)
                {
                    Logger.Info(String.Format("- Found {0} betweeen {1} and {2}", lonToFind, bp_next.Longitude, bp_curr.Longitude));
                    break;
                }
                else
                {
                    Logger.Info(String.Format("{3} Didn't find {0} betweeen {1} and {2}", lonToFind, bp_curr.Longitude, bp_next.Longitude, ut_curr));
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
                Sweph.SWE_ReverseJulianDay(ut_start, ref year, ref month, ref day, ref hour);
                Moment m = new Moment(year, month, day, hour);

                Logger.Info(String.Format("F {3} Lagna search for {0} between {1} and {2}", lonToFind, lon_start, lon_end, m));

                if (lonToFind.Subtract(lon_start).Value <= lon_end.Subtract(lon_start).Value)
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
                Sweph.SWE_ReverseJulianDay(ut_start, ref year, ref month, ref day, ref hour);
                Moment m = new Moment(year, month, day, hour);

                Logger.Info(String.Format("B {3} Lagna search for {0} between {1} and {2}",lonToFind, lon_start, lon_end, m));

                if (lonToFind.Subtract(lon_end).Value <= lon_start.Subtract(lon_end).Value)
                    break;
            }

            return this.BinaryLonSearch(ut_end, ut_start, lonToFind, true);
        }

        public double GetTransitForward(double ut, Longitude lonToFind)
        {
            if (this.b == BodyName.Lagna)
                return GetLagnaTransitForward(ut, lonToFind);


            bool becomesDirect = true;
            double ut_curr = ut;
            double ut_next = ut;

            while (true)
            {
                ut_curr = ut_next;

                double ut_start = ut_curr;
                if (ut_curr != ut) ut_start += this.GetStep();
                ut_next = FindNextCuspForward(ut_start, ref becomesDirect);

                BodyPosition bp_curr = Basics.CalculateSingleBodyPosition(ut_curr, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                BodyPosition bp_next = Basics.CalculateSingleBodyPosition(ut_next, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                Logger.Info(String.Format("{0}, {1}, {2}", becomesDirect, bp_curr.Longitude, bp_next.Longitude));

                if (false == becomesDirect &&
                    lonToFind.Subtract(bp_curr.Longitude).Value <= bp_next.Longitude.Subtract(bp_curr.Longitude).Value)
                {
                    Logger.Info(String.Format("+ Found {0} between {1} and {2}", lonToFind, bp_curr.Longitude, bp_next.Longitude));
                    break;
                }
                else if (true == becomesDirect &&
                    lonToFind.Subtract(bp_next.Longitude).Value <= bp_curr.Longitude.Subtract(bp_next.Longitude).Value)
                {
                    Logger.Info(String.Format("- Found {0} betweeen {1} and {2}", lonToFind, bp_next.Longitude, bp_curr.Longitude));
                    break;
                }
                else
                {
                    Logger.Info(String.Format("{3} Didn't find {0} betweeen {1} and {2}", lonToFind, bp_curr.Longitude, bp_next.Longitude, ut_curr));
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
            return bp.SpeedLongitude;
        }
        public Longitude GetLon(double ut, ref bool bForward)
        {
            if (this.b == BodyName.Lagna)
                return new Longitude(Sweph.SWE_Lagna(ut));

            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
            bForward = bp.SpeedLongitude >= 0;
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
                if (lon_to_find.Subtract(lon_start).Value <= lon_middle.Subtract(lon_start).Value)
                    return BinaryLonSearch(ut_start, ut_middle, lon_to_find, normal);
                else
                    return BinaryLonSearch(ut_middle, ut_end, lon_to_find, normal);
            }
            else
            {
                if (lon_to_find.Subtract(lon_end).Value <= lon_middle.Subtract(lon_end).Value)
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

            Logger.Info(String.Format("Speed BinarySearchNormal {0} UT: {2} Speed {1}", b, speed_middle, ut_middle));

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

        public double FindNextCuspBackward(double start_ut, ref bool becomesDirect)
        {
            double ut_step = 5.0;
            BodyPosition bp = Basics.CalculateSingleBodyPosition(start_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

            // Body is currently direct
            if (bp.SpeedLongitude >= 0)
            {
                start_ut = GotoNextRetroSolarCusp(start_ut);
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

                    Logger.Info(String.Format("D Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.SpeedLongitude, bp_h.SpeedLongitude));
                    // If first one is retro, we're exactly at the cusp
                    // If higher is still direct, contine
                    if (bp_l.SpeedLongitude < 0 && bp_h.SpeedLongitude > 0) break;
                    if (bp_l.SpeedLongitude > 0 && bp_h.SpeedLongitude < 0) break;
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

                    Logger.Info(String.Format("R Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bp_l.SpeedLongitude, bp_h.SpeedLongitude));
                    if (bp_l.SpeedLongitude > 0 && bp_h.SpeedLongitude <= 0) break;
                    if (bp_l.SpeedLongitude < 0 && bp_h.SpeedLongitude > 0) break;
                    //if (bp_l.speed_longitude > 0 && bp_h.speed_longitude > 0)
                    //	return findNextCuspBackward (lower_ut, ref becomesDirect);
                }
                // Within one day period
                return BinaryCuspSearch(higher_ut, lower_ut, false);
            }
        }

        public double FindNextCuspForward(double start_ut, ref bool becomesDirect)
        {
            double ut_step = 1.0;
            BodyPosition bp = Basics.CalculateSingleBodyPosition(start_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

            // Body is currently direct
            if (bp.SpeedLongitude >= 0)
            {
                start_ut = GotoNextRetroSolarCusp(start_ut);
                double lower_ut = start_ut;
                double higher_ut = start_ut;
                becomesDirect = false;
                while (true)
                {
                    lower_ut = higher_ut;
                    higher_ut = lower_ut + ut_step;

                    // Find speeds
                    BodyPosition bpLower = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bpHigher = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    Logger.Info(String.Format("D Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bpLower.SpeedLongitude, bpHigher.SpeedLongitude));
                    // If first one is retro, we're exactly at the cusp
                    // If higher is still direct, contine
                    if (bpLower.SpeedLongitude > 0 && bpHigher.SpeedLongitude < 0) break;
                    if (bpLower.SpeedLongitude < 0 && bpHigher.SpeedLongitude < 0)
                        return FindNextCuspForward(lower_ut, ref becomesDirect);
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
                    BodyPosition bpLower = Basics.CalculateSingleBodyPosition(lower_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);
                    BodyPosition bpHigher = Basics.CalculateSingleBodyPosition(higher_ut, Sweph.BodyNameToSweph(b), b, BodyType.Name.Other, this.h);

                    Logger.Info(String.Format("R Checking daily {0} UT: {1} {2} Speed {3} {4}", b, lower_ut, higher_ut, bpLower.SpeedLongitude, bpHigher.SpeedLongitude));
                    if (bpLower.SpeedLongitude < 0 && bpHigher.SpeedLongitude >= 0) break;
                    if (bpLower.SpeedLongitude > 0 && bpHigher.SpeedLongitude > 0)
                        return FindNextCuspForward(lower_ut, ref becomesDirect);
                }
                // Within one day period
                return BinaryCuspSearch(lower_ut, higher_ut, false);
            }
        }

    }

}
