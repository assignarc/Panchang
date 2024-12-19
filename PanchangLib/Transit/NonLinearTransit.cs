using System;

namespace org.transliteral.panchang
{

    public class NonLinearTransit
    {
        private Horoscope h;
        BodyName b;

        public NonLinearTransit(Horoscope _h, BodyName _b)
        {
            h = _h;
            b = _b;
        }

        public int BodyNameToSweph(BodyName b)
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
                default:
                    throw new Exception();
            }
        }
        public Longitude GetLongitude(double ut, ref bool bForwardDir)
        {
            int swephBody = BodyNameToSweph(b);
            BodyPosition bp = Basics.CalculateSingleBodyPosition(ut, swephBody, b, BodyType.Name.Other, h);
            if (bp.SpeedLongitude >= 0) bForwardDir = true;
            else bForwardDir = false;
            return bp.Longitude;
        }

        public double BinarySearchNormal(double ut_start, double ut_end, Longitude lon_to_find)
        {
            bool bDiscard = true;

            if (Math.Abs(ut_end - ut_start) < (1.0 / (24.0 * 60.0 * 60.0 * 60.0)))
            {
                Logger.Info(String.Format("BinarySearchNormal: Found {0} at {1}", lon_to_find, ut_start));
                if (Transit.CircLonLessThan(GetLongitude(ut_start, ref bDiscard), lon_to_find))
                    return ut_end;
                else
                    return ut_start;
            }

            double ut_middle = (ut_start + ut_end) / 2.0;

            Longitude lon = GetLongitude(ut_middle, ref bDiscard);
            Logger.Info(String.Format("BinarySearchNormal {0} Find:{1} {2} curr:{3}", b, lon_to_find.Value, ut_middle, lon.Value));
            if (Transit.CircLonLessThan(lon, lon_to_find))
                return BinarySearchNormal(ut_middle, ut_end, lon_to_find);
            else
                return BinarySearchNormal(ut_start, ut_middle, lon_to_find);
        }

        public double BinarySearchRetro(double ut_start, double ut_end, Longitude lon_to_find)
        {
            if (Math.Abs(ut_end - ut_start) < (1.0 / (24.0 * 60.0 * 60.0 * 60.0)))
            {
                Logger.Info(String.Format("BinarySearchRetro: Found {0} at {1}", lon_to_find, ut_start));
                return ut_start;
            }

            double ut_middle = (ut_start + ut_end) / 2.0;
            bool bDiscard = true;
            Longitude lon = GetLongitude(ut_middle, ref bDiscard);
            Logger.Info(String.Format("BinarySearchRetro {0} Find:{1} {2} curr:{3}", b, lon_to_find.Value, ut_middle, lon.Value));
            if (Transit.CircLonLessThan(lon, lon_to_find))
                return BinarySearchRetro(ut_start, ut_middle, lon_to_find);
            else
                return BinarySearchRetro(ut_middle, ut_end, lon_to_find);

        }

        public double Forward(double ut, Longitude lonToFind)
        {
            while (true)
            {
                bool bForwardStart = true, bForwardEnd = true;
                Longitude lStart = GetLongitude(ut, ref bForwardStart);
                Longitude lEnd = GetLongitude(ut + 1.0, ref bForwardEnd);
                if (bForwardStart == true && bForwardEnd == true)
                {
                    if (Transit.CircLonLessThan(lStart, lonToFind) &&
                        Transit.CircLonLessThan(lonToFind, lEnd))
                    {
                        Logger.Info(String.Format("2: (N) +1.0. {0} Curr:{1} Start:{2} End:{3}", b, lonToFind.Value, lStart.Value, lEnd.Value));
                        return this.BinarySearchNormal(ut, ut + 1.0, lonToFind);
                    }
                    else
                    {
                        Logger.Info(String.Format("1: (N) +1.0. {0} Find:{1} Start:{2} End:{3}", b, lonToFind.Value, lStart.Value, lEnd.Value));
                        ut += 10.0;
                    }
                }
                else if (bForwardStart == false && bForwardEnd == false)
                {
                    if (Transit.CircLonLessThan(lEnd, lonToFind) &&
                        Transit.CircLonLessThan(lonToFind, lStart))
                    {
                        Logger.Info(String.Format("2: (R) +1.0. {0} Curr:{1} Start:{2} End:{3}", b, lonToFind.Value, lStart.Value, lEnd.Value));
                        return this.BinarySearchRetro(ut, ut + 1.0, lonToFind);
                    }
                    else
                    {
                        Logger.Info(String.Format("1: (R) +1.0. {0} Find:{1} Start:{2} End:{3}", b, lonToFind.Value, lStart.Value, lEnd.Value));
                        ut += 10.0;
                    }
                }
                else
                {
                    Logger.Info(String.Format("Retrograde Cusp date at {0}. Skipping for now.", ut));
                    ut += 10.0;
                }
            }
        }
    }

}
