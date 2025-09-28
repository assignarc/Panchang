using System;
using System.Collections;
using System.Diagnostics;

namespace org.transliteral.panchang
{
    /// <summary>
	/// Specifies a BodyPosition, i.e. the astronomical characteristics of a body like
	/// longitude, speed etc. It has no notion of its "rasi".
	/// The functions to convert this to a DivisionType (the various vargas) 
	/// are all implemented here
	/// </summary>
	public class BodyPosition : ICloneable
    {
        Longitude m_lon;
        private double m_splon, m_lat, m_splat, m_dist, m_spdist;
        public string otherString;
        public BodyName name;
        public BodyType.Name type;
        public Horoscope h;
        public Longitude Longitude { get { return m_lon; } set { m_lon = value; } }
        public double Latitude { get { return m_lat; } set { m_lat = value; } }
        public double Distance { get { return m_dist; } set { m_dist = value; } }
        public double SpeedLongitude { get { return m_splon; } set { m_splon = value; } }
        public double SpeedLatitude { get { return m_splat; } set { m_splat = value; } }
        public double SpeedDistance { get { return m_spdist; } set { m_spdist = value; } }
        public BodyPosition(Horoscope _h, BodyName aname, BodyType.Name atype, Longitude lon, double lat, double dist, double splon, double splat, double spdist)
        {
            this.Longitude = lon;
            m_lat = lat;
            m_dist = dist;
            m_splon = splon;
            m_splat = splat;
            m_spdist = spdist;
            name = aname;
            type = atype;
            h = _h;
            Logger.Info(String.Format("{0} {1} {2}", aname.ToString(), lon.Value, splon));
        }
        public object Clone()
        {
            return  new BodyPosition(h, name, type, m_lon.Add(0), m_lat,m_dist, m_splon, m_splat, m_spdist)
            {
                otherString = this.otherString
            };
           
        }

        public int PartOfZodiacHouse(int n)
        {
            Longitude l = this.Longitude;
            double offset = l.ToZodiacHouseOffset();
            int part = (int)(Math.Floor(offset / (30.0 / n))) + 1;
            Trace.Assert(part >= 1 && part <= n);
            return part;
        }
        private DivisionPosition PopulateRegularCusps(int n, DivisionPosition dp)
        {
            int part = PartOfZodiacHouse(n);
            double cusp_length = 30.0 / ((double)n);
            dp.CuspLower = ((double)(part - 1)) * cusp_length;
            dp.CuspLower += m_lon.ToZodiacHouseBase();
            dp.CuspHigher = dp.CuspLower + cusp_length;
            dp.Part = part;

            if (dp.Type == BodyType.Name.Graha || dp.Type == BodyType.Name.Lagna)
                Logger.Info(String.Format("D: {0} {1} {2} {3} {4} {5}", n, dp.Name, cusp_length,dp.CuspLower, m_lon.Value, dp.CuspHigher));
            return dp;
        }

        /// <summary>
        /// Many of the varga divisions (like navamsa) are regular divisions,
        /// and can be implemented by a single method. We do this when possible.
        /// </summary>
        /// <param name="n">The number of parts a house is divided into</param>
        /// <returns>The DivisionPosition the body falls into</returns>
        /// 
        private DivisionPosition ToRegularDivisionPosition(int n)
        {
            int zhouse = (int)(m_lon.ToZodiacHouse().Value);
            int num_parts = (((int)zhouse) - 1) * n + PartOfZodiacHouse(n);
            ZodiacHouse div_house = (new ZodiacHouse(ZodiacHouseName.Ari)).Add(num_parts);
            DivisionPosition dp = new DivisionPosition(name, type, div_house, 0, 0, 0);
            PopulateRegularCusps(n, dp);
            return dp;
        }

        private DivisionPosition ToRegularDivisionPositionFromCurrentHouseOddEven(int n=0)
        {
            int zhouse = (int)(m_lon.ToZodiacHouse().Value);
            int num_parts = PartOfZodiacHouse(n);
            ZodiacHouse div_house = m_lon.ToZodiacHouse().Add(num_parts);
            DivisionPosition dp = new DivisionPosition(name, type, div_house, 0, 0, 0);
            PopulateRegularCusps(n, dp);
            return dp;
        }


        private DivisionPosition ToBhavaDivisionPositionRasi(Longitude[] cusps)
        {
            Debug.Assert(cusps.Length == 13);
            cusps[12] = cusps[0];
            for (int i = 0; i < 12; i++)
            {
                if (m_lon.Subtract(cusps[i]).Value <= cusps[i + 1].Subtract(cusps[i]).Value)
                    return new DivisionPosition(name, type, new ZodiacHouse((ZodiacHouseName)i + 1),
                        cusps[i].Value, cusps[i + 1].Value, 1);
            }
            throw new Exception();
        }
        private DivisionPosition ToBhavaDivisionPositionHouse(Longitude[] cusps)
        {
            Debug.Assert(cusps.Length == 13);

            ZodiacHouse zlagna = h.GetPosition(BodyName.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;
            for (int i = 0; i < 12; i++)
            {
                if (m_lon.Subtract(cusps[i]).Value < cusps[i + 1].Subtract(cusps[i]).Value)
                {
                    Logger.Info(String.Format("Found {4} - {0} in cusp {3} between {1} and {2}", 
                            this.m_lon.Value,cusps[i].Value, cusps[i+1].Value, i+1, this.name.ToString()));

                    return new DivisionPosition(name, type,
                        zlagna.Add(i + 1), cusps[i].Value, cusps[i + 1].Value, 1);
                }
            }
            return new DivisionPosition(name, type,
                zlagna.Add(1), cusps[0].Value, cusps[1].Value, 1);
        }
        private DivisionPosition ToDivisionPositionBhavaEqual()
        {
            double offset = h.GetPosition(BodyName.Lagna).Longitude.ToZodiacHouseOffset();
            Longitude[] cusps = new Longitude[13];
            for (int i = 0; i < 12; i++)
                cusps[i] = new Longitude(i * 30.0 + offset - 15.0);
            return this.ToBhavaDivisionPositionRasi(cusps);
        }
        private DivisionPosition ToDivisionPositionBhavaPada()
        {
            Longitude[] cusps = new Longitude[13];
            double offset = h.GetPosition(BodyName.Lagna).Longitude.ToZodiacHouseOffset();
            int padasOffset = (int)Math.Floor(offset / (360.0 / 108.0));
            double startOffset = (double)padasOffset * (360.0 / 108.0);

            for (int i = 0; i < 12; i++)
                cusps[i] = new Longitude(i * 30.0 + startOffset - 15.0);
            return this.ToBhavaDivisionPositionRasi(cusps);
        }
        private DivisionPosition ToDivisionPositionBhavaHelper(int hsys)
        {
            Longitude[] cusps = new Longitude[13];
            double[] dCusps = new double[13];
            double[] ascmc = new double[10];

            if (hsys != h.SwephHouseSystem)
            {
                h.SwephHouseSystem = hsys;
                h.PopulateHouseCusps();
            }
            return this.ToBhavaDivisionPositionHouse(h.SwephHouseCusps);
        }
        private bool HoraSunDayNight()
        {
            int sign = (int)m_lon.ToZodiacHouse().Value;
            int part = PartOfZodiacHouse(2);
            if (m_lon.ToZodiacHouse().IsDaySign())
            {
                if (part == 1) return true;
                return false;
            }
            else
            {
                if (part == 1) return false;
                return true;
            }
        }
        private bool HoraSunOddEven()
        {
            int sign = (int)m_lon.ToZodiacHouse().Value;
            int part = PartOfZodiacHouse(2);
            int mod = sign % 2;
            switch (mod)
            {
                case 1:
                    if (part == 1) return true;
                    return false;
                default:
                    if (part == 1) return false;
                    return true;
            }
        }
        private DivisionPosition ToDivisionPositionHoraKashinath()
        {
            int[] daySigns = new int[13] { 0, 8, 7, 6, 5, 5, 6, 7, 8, 12, 11, 11, 12 };
            int[] nightSigns = new int[13] { 0, 1, 2, 3, 4, 4, 3, 2, 1, 9, 10, 10, 9 };

            ZodiacHouse zh;
            int sign = (int)m_lon.ToZodiacHouse().Value;
            if (this.HoraSunOddEven()) zh = new ZodiacHouse((ZodiacHouseName)daySigns[sign]);
            else zh = new ZodiacHouse((ZodiacHouseName)nightSigns[sign]);
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);
            this.PopulateRegularCusps(2, dp);
            return dp;
        }
        private DivisionPosition ToDivisionPositionHoraJagannath()
        {
            ZodiacHouse zh = m_lon.ToZodiacHouse();

            Logger.Info(String.Format("{2} in {3}: OddEven is {0}, DayNight is {1}",
                this.HoraSunOddEven(), this.HoraSunDayNight(), this.name, zh.Value));

            if (this.HoraSunDayNight() && false == this.HoraSunOddEven())
                zh = zh.Add(7);
            else if (false == this.HoraSunDayNight() && true == this.HoraSunOddEven())
                zh = zh.Add(7);

            Logger.Info(String.Format("{0} ends in {1}", this.name, zh.Value));

            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);
            this.PopulateRegularCusps(2, dp);
            return dp;
        }
        private DivisionPosition ToDivisionPositionHoraParasara()
        {
            ZodiacHouse zh;
            int ruler_index = 0;
            if (this.HoraSunOddEven())
            {
                zh = new ZodiacHouse(ZodiacHouseName.Leo);
                ruler_index = 1;
            }
            else
            {
                zh = new ZodiacHouse(ZodiacHouseName.Can);
                ruler_index = 2;
            }
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0)
            {
                RulerIndex = ruler_index
            };
            return this.PopulateRegularCusps(2, dp);
        }
        private DivisionPosition ToDivisionPositionDrekanna(int n)
        {
            int[] offset = new int[4] { 9, 1, 5, 9 };
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.Add(offset[part % 3]);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            PopulateRegularCusps(n, dp);
            if (n == 3)
            {
                int ruler_index = (int)dp.ZodiacHouse.Value % 3;
                if (ruler_index == 0) ruler_index = 3;
                dp.RulerIndex = ruler_index;
            }
            return dp;
        }
        private DivisionPosition ToDivisionPositionDrekannaJagannath()
        {
            ZodiacHouse zh = m_lon.ToZodiacHouse();
            ZodiacHouse zhm;
            ZodiacHouse dhouse;
            int mod = ((int)(m_lon.ToZodiacHouse().Value)) % 3;
            // Find moveable sign in trines
            switch (mod)
            {
                case 1: zhm = zh.Add(1); break;
                case 2: zhm = zh.Add(9); break;
                default: zhm = zh.Add(5); break;
            }

            // From moveable sign, 3 parts belong to the trines
            int part = PartOfZodiacHouse(3);
            switch (part)
            {
                case 1: dhouse = zhm.Add(1); break;
                case 2: dhouse = zhm.Add(5); break;
                default: dhouse = zhm.Add(9); break;
            }

            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(3, dp);
        }
        private DivisionPosition ToDivisionPositionDrekkanaSomnath()
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 2;
            int part = PartOfZodiacHouse(3);
            ZodiacHouse zh = m_lon.ToZodiacHouse();
            int p = (int)zh.Value;

            if (mod == 0) p--;
            p = (p - 1) / 2;
            int num_done = p * 3;

            ZodiacHouse zh1 = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zh2;
            if (mod == 1) zh2 = zh1.Add(num_done + part);
            else zh2 = zh1.AddReverse(num_done + part + 1);
            DivisionPosition dp = new DivisionPosition(name, type, zh2, 0, 0, 0);
            return this.PopulateRegularCusps(3, dp);
        }
        private DivisionPosition ToDivisionPositionChaturthamsa(int n)
        {
            int[] offset = new int[5] { 10, 1, 4, 7, 10 };
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.Add(offset[part % 4]);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 4)
                dp.RulerIndex = part;
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionShashthamsa(int n)
        {
            int mod = ((int)(m_lon.ToZodiacHouse().Value)) % 2;
            ZodiacHouseName dhousen = (mod % 2 == 1) ? ZodiacHouseName.Ari : ZodiacHouseName.Lib;
            ZodiacHouse dhouse = (new ZodiacHouse(dhousen)).Add(PartOfZodiacHouse(n));
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionSaptamsa(int n)
        {
            int part = PartOfZodiacHouse(n);
            ZodiacHouse zh = m_lon.ToZodiacHouse();
            if (false == zh.IsOdd())
                zh = zh.Add(7);
            zh = zh.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);

            if (n == 7)
            {
                if (this.Longitude.ToZodiacHouse().IsOdd())
                    dp.RulerIndex = part;
                else
                    dp.RulerIndex = 8 - part;
            }
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionNavamsa()
        {
            int part = PartOfZodiacHouse(9);
            DivisionPosition dp = this.ToRegularDivisionPosition(9);
            switch ((int)this.Longitude.ToZodiacHouse().Value % 3)
            {
                case 1: dp.RulerIndex = part; break;
                case 2: dp.RulerIndex = part + 1; break;
                case 0: dp.RulerIndex = part + 2; break;
            }
            while (dp.RulerIndex > 3) dp.RulerIndex -= 3;
            return dp;
        }
        private DivisionPosition ToDivisionPositionAshtamsaRaman()
        {
            ZodiacHouse zstart = null;
            switch ((int)m_lon.ToZodiacHouse().Value % 3)
            {
                case 1: zstart = new ZodiacHouse(ZodiacHouseName.Ari); break;
                case 2: zstart = new ZodiacHouse(ZodiacHouseName.Leo); break;
                case 0:
                default:
                    zstart = new ZodiacHouse(ZodiacHouseName.Sag); break;
            }
            ZodiacHouse dhouse = zstart.Add(PartOfZodiacHouse(8));
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(8, dp);
        }
        private DivisionPosition ToDivisionPositionPanchamsa()
        {
            ZodiacHouseName[] offset_odd = new ZodiacHouseName[5]
                        {
                            ZodiacHouseName.Ari, ZodiacHouseName.Aqu, ZodiacHouseName.Sag,
                            ZodiacHouseName.Gem, ZodiacHouseName.Lib};
            ZodiacHouseName[] offset_even = new ZodiacHouseName[5]
                        {
                            ZodiacHouseName.Tau, ZodiacHouseName.Vir, ZodiacHouseName.Pis,
                            ZodiacHouseName.Cap, ZodiacHouseName.Sco};
            int part = PartOfZodiacHouse(5);
            int mod = ((int)(m_lon.ToZodiacHouse().Value)) % 2;
            ZodiacHouseName dhouse = (mod % 2 == 1) ? offset_odd[part - 1] : offset_even[part - 1];
            DivisionPosition dp = new DivisionPosition(name, type, new ZodiacHouse(dhouse), 0, 0, 0);
            return this.PopulateRegularCusps(5, dp);
        }
        private DivisionPosition ToDivisionPositionRudramsa()
        {
            ZodiacHouse zari = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int diff = zari.NumHousesBetween(zhouse);
            ZodiacHouse zstart = zari.AddReverse(diff);
            int part = PartOfZodiacHouse(11);
            ZodiacHouse zend = zstart.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, zend, 0, 0, 0);
            return this.PopulateRegularCusps(11, dp);
        }
        private DivisionPosition ToDivisionPositionRudramsaRaman()
        {
            ZodiacHouse zhstart = m_lon.ToZodiacHouse().Add(12);
            int part = PartOfZodiacHouse(11);
            ZodiacHouse zend = zhstart.AddReverse(part);
            DivisionPosition dp = new DivisionPosition(name, type, zend, 0, 0, 0);
            return this.PopulateRegularCusps(11, dp);
        }
        private DivisionPosition ToDivisionPositionDasamsa(int n)
        {
            int[] offset = new int[2] { 9, 1 };
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            ZodiacHouse dhouse = zhouse.Add(offset[((int)zhouse.Value) % 2]);
            int part = PartOfZodiacHouse(n);
            dhouse = dhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 10)
            {
                if (this.Longitude.ToZodiacHouse().IsOdd())
                    dp.RulerIndex = part;
                else
                    dp.RulerIndex = 11 - part;
            }
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionDwadasamsa(int n)
        {
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 12)
                dp.RulerIndex = Basics.NormalizeInclusive(1, 4, part);
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionShodasamsa()
        {
            int part = PartOfZodiacHouse(16);
            DivisionPosition dp = this.ToRegularDivisionPosition(16);
            int ruler = part;
            if (this.Longitude.ToZodiacHouse().IsOdd())
                ruler = part;
            else
                ruler = 17 - part;
            dp.RulerIndex = Basics.NormalizeInclusive(1, 4, ruler);
            return dp;
        }
        private DivisionPosition ToDivisionPositionVimsamsa(int n)
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 3;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Sag; break;
                default: dhousename = ZodiacHouseName.Leo; break;
            }
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionVimsamsa()
        {
            int part = PartOfZodiacHouse(20);
            DivisionPosition dp = this.ToRegularDivisionPosition(20);
            if (this.Longitude.ToZodiacHouse().IsOdd())
                dp.RulerIndex = part;
            else
                dp.RulerIndex = 20 + part;
            return dp;
        }
        private DivisionPosition ToDivisionPositionChaturvimsamsa(int n)
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 2;
            ZodiacHouseName dhousename = (mod % 2 == 1) ? ZodiacHouseName.Leo : ZodiacHouseName.Can;
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 24)
            {
                if (this.Longitude.ToZodiacHouse().IsOdd())
                    dp.RulerIndex = part;
                else
                    dp.RulerIndex = 25 - part;
                dp.RulerIndex = Basics.NormalizeInclusive(1, 12, dp.RulerIndex);
            }
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionNakshatramsa(int n)
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 4;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Can; break;
                case 3: dhousename = ZodiacHouseName.Lib; break;
                default: dhousename = ZodiacHouseName.Cap; break;
            }
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionNakshatramsa()
        {
            DivisionPosition dp = this.ToRegularDivisionPosition(27);
            dp.RulerIndex = this.PartOfZodiacHouse(27);
            return dp;
        }
        private DivisionPosition ToDivisionPositionTrimsamsaSimple()
        {
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(30);
            ZodiacHouse dhouse = zhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.PopulateRegularCusps(30, dp);
        }
        private DivisionPosition ToDivisionPositionTrimsamsa()
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 2;
            double off = m_lon.ToZodiacHouseOffset();
            ZodiacHouse dhouse;
            double cusp_lower = 0;
            double cusp_higher = 0;
            int ruler_index = 0;
            int part = 0;
            if (mod == 1)
            {
                if (off <= 5)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Ari);
                    cusp_lower = 0.0; cusp_higher = 5.0;
                    ruler_index = 1;
                    part = 1;
                }
                else if (off <= 10)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Aqu);
                    cusp_lower = 5.01; cusp_higher = 10.0;
                    ruler_index = 2;
                    part = 2;
                }
                else if (off <= 18)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Sag);
                    cusp_lower = 10.01; cusp_higher = 18.0;
                    ruler_index = 3;
                    part = 3;
                }
                else if (off <= 25)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Gem);
                    cusp_lower = 18.01; cusp_higher = 25.0;
                    ruler_index = 4;
                    part = 4;
                }
                else
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Lib);
                    cusp_lower = 25.01; cusp_higher = 30.0;
                    ruler_index = 5;
                    part = 5;
                }
            }
            else
            {
                if (off <= 5)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Tau);
                    cusp_lower = 0.0; cusp_higher = 5.0;
                    ruler_index = 5;
                    part = 1;
                }
                else if (off <= 12)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Vir);
                    cusp_lower = 5.01; cusp_higher = 12.0;
                    ruler_index = 4;
                    part = 2;
                }
                else if (off <= 20)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Pis);
                    cusp_lower = 12.01; cusp_higher = 20.0;
                    ruler_index = 3;
                    part = 3;
                }
                else if (off <= 25)
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Cap);
                    cusp_lower = 20.01; cusp_higher = 25.0;
                    ruler_index = 2;
                    part = 4;
                }
                else
                {
                    dhouse = new ZodiacHouse(ZodiacHouseName.Sco);
                    cusp_lower = 25.01; cusp_higher = 30.0;
                    ruler_index = 1;
                    part = 5;
                }
            }
            cusp_lower += m_lon.ToZodiacHouseBase();
            cusp_higher += m_lon.ToZodiacHouseBase();

            DivisionPosition dp = new DivisionPosition(name, type, dhouse, cusp_lower, cusp_higher, 0)
            {
                RulerIndex = ruler_index,
                Part = part
            };
            return dp;
        }
        private DivisionPosition ToDivisionPositionKhavedamsa()
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 2;
            ZodiacHouseName dhousename = (mod % 2 == 1) ? ZodiacHouseName.Ari : ZodiacHouseName.Lib;
            int part = PartOfZodiacHouse(40);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0)
            {
                RulerIndex = Basics.NormalizeInclusive(1, 12, part)
            };
            return this.PopulateRegularCusps(40, dp);
        }
        private DivisionPosition ToDivisionPositionAkshavedamsa(int n)
        {
            int mod = ((int)(m_lon.ToZodiacHouse()).Value) % 3;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Leo; break;
                default: dhousename = ZodiacHouseName.Sag; break;
            }
            int part = PartOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 45)
            {
                switch ((int)this.Longitude.ToZodiacHouse().Value % 3)
                {
                    case 1: dp.RulerIndex = part; break;
                    case 2: dp.RulerIndex = part + 1; break;
                    case 0: dp.RulerIndex = part + 2; break;
                }
                dp.RulerIndex = Basics.NormalizeInclusive(1, 3, dp.RulerIndex);
            }
            return this.PopulateRegularCusps(n, dp);
        }
        private DivisionPosition ToDivisionPositionShashtyamsa()
        {
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(60);
            ZodiacHouse dhouse = zhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (this.Longitude.ToZodiacHouse().IsOdd())
                dp.RulerIndex = part;
            else
                dp.RulerIndex = 61 - part;
            return this.PopulateRegularCusps(60, dp);
        }
        private DivisionPosition ToDivisionPositionNadiamsa()
        {
#if DND
			ZodiacHouse zhouse = m_lon.toZodiacHouse();
			int part = partOfZodiacHouse(150);
			ZodiacHouse dhouse = null;
			switch ((int)zhouse.value % 3)
			{
				case 1:	dhouse = zhouse.add(part); break;
				case 2:	dhouse = zhouse.addReverse(part); break;
				default:
				case 0:
					dhouse = zhouse.add(part-75); break;
			}
			DivisionPosition dp = new DivisionPosition (name, type, dhouse, 0, 0, 0);
#endif
            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            int part = PartOfZodiacHouse(150);
            ZodiacHouse dhouse = zhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            switch ((int)this.Longitude.ToZodiacHouse().Value % 3)
            {
                case 1: dp.RulerIndex = part; break;
                case 2: dp.RulerIndex = 151 - part; break;
                case 0: dp.RulerIndex = Basics.NormalizeInclusive(1, 150, 75 + part); break;
            }
            return this.PopulateRegularCusps(150, dp);
        }
        static bool mbNadiamsaCKNCalculated = false;
        static double[] mNadiamsaCusps = null;
        private void CalculateNadiamsaCusps()
        {
            if (true == BodyPosition.mbNadiamsaCKNCalculated)
                return;

            int[] bases = new int[] { 1, 2, 3, 4, 7, 9, 10, 12, 16, 20, 24, 27, 30, 40, 45, 60 };
            ArrayList alUnsorted = new ArrayList(150);
            foreach (int iVarga in bases)
            {
                for (int i = 0; i < iVarga; i++)
                    alUnsorted.Add((double)i / (double)iVarga * (double)30.0);
            }
            alUnsorted.Add((double)30.0);
            alUnsorted.Sort();

            ArrayList alSorted = new ArrayList(150)
            {
                (double)0.0
            };

            for (int i = 0; i < alUnsorted.Count; i++)
            {
                if ((double)alUnsorted[i] != (double)alSorted[alSorted.Count - 1])
                    alSorted.Add(alUnsorted[i]);
            }

            Debug.Assert(alSorted.Count == 151,
                String.Format("Found {0} Nadis. Expected 151.", alSorted.Count));

            mNadiamsaCusps = (double[])alSorted.ToArray(typeof(double));
            BodyPosition.mbNadiamsaCKNCalculated = true;
        }
        private DivisionPosition ToDivisionPositionNadiamsaCKN()
        {
            this.CalculateNadiamsaCusps();
            int part = PartOfZodiacHouse(150) - 10;
            if (part < 0) part = 0;

            for (; part < 149; part++)
            {
                Longitude lonLow = new Longitude(BodyPosition.mNadiamsaCusps[part]);
                Longitude lonHigh = new Longitude(BodyPosition.mNadiamsaCusps[part + 1]);
                Longitude offset = new Longitude(this.Longitude.ToZodiacHouseOffset());

                if (offset.Subtract(lonLow).Value <= lonHigh.Subtract(lonLow).Value)
                    break;
            }
            part++;

#if DND
			ZodiacHouse zhouse = m_lon.toZodiacHouse();
			ZodiacHouse dhouse = null;
			switch ((int)zhouse.value % 3)
			{
				case 1:	dhouse = zhouse.add(part); break;
				case 2:	dhouse = zhouse.addReverse(part); break;
				default:
				case 0:
					dhouse = zhouse.add(part-75); break;
			}
			DivisionPosition dp = new DivisionPosition (name, type, dhouse, 0, 0, 0);
#endif

            ZodiacHouse zhouse = m_lon.ToZodiacHouse();
            ZodiacHouse dhouse = zhouse.Add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);


            switch ((int)this.Longitude.ToZodiacHouse().Value % 3)
            {
                case 1: dp.RulerIndex = part; break;
                case 2: dp.RulerIndex = 151 - part; break;
                case 0: dp.RulerIndex = Basics.NormalizeInclusive(1, 150, 75 + part); break;
            }
            dp.CuspLower = this.Longitude.ToZodiacHouseBase() + BodyPosition.mNadiamsaCusps[part - 1];
            dp.CuspHigher = this.Longitude.ToZodiacHouseBase() + BodyPosition.mNadiamsaCusps[part];
            dp.Part = part;
            return dp;
        }
        private DivisionPosition ToDivisionPositionNavamsaDwadasamsa()
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            bp.Longitude = bp.ExtrapolateLongitude(new Division(DivisionType.Navamsa));
            DivisionPosition dp = bp.ToDivisionPositionDwadasamsa(12);
            this.PopulateRegularCusps(108, dp);
            return dp;
        }
        private DivisionPosition ToDivisionPositionDwadasamsaDwadasamsa()
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            bp.Longitude = bp.ExtrapolateLongitude(new Division(DivisionType.Dwadasamsa));
            DivisionPosition dp = bp.ToDivisionPositionDwadasamsa(12);
            this.PopulateRegularCusps(144, dp);
            return dp;
        }
        /// <summary>
        /// Calculated any known Varga positions. Simply calls the appropriate
        /// helper function
        /// </summary>
        /// <param name="dtype">The requested DivisionType</param>
        /// <returns>A division Position</returns>


        public DivisionPosition ToDivisionPosition(Division d)
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            DivisionPosition dp = null;
            for (int i = 0; i < d.MultipleDivisions.Length; i++)
            {
                dp = bp.ToDivisionPosition(d.MultipleDivisions[i]);
                bp.Longitude = bp.ExtrapolateLongitude(d.MultipleDivisions[i]);
            }
            return dp;
        }
        public DivisionPosition ToDivisionPosition(Division.SingleDivision d)
        {
            if (d.NumParts < 1)
                d.NumParts = 1;

            switch (d.Varga)
            {
                case DivisionType.Rasi:
                    return ToRegularDivisionPosition(1);
                case DivisionType.BhavaPada:
                    return ToDivisionPositionBhavaPada();
                case DivisionType.BhavaEqual:
                    return ToDivisionPositionBhavaEqual();
                case DivisionType.BhavaSripati:
                    return this.ToDivisionPositionBhavaHelper('O');
                case DivisionType.BhavaKoch:
                    return this.ToDivisionPositionBhavaHelper('K');
                case DivisionType.BhavaPlacidus:
                    return this.ToDivisionPositionBhavaHelper('P');
                case DivisionType.BhavaCampanus:
                    return this.ToDivisionPositionBhavaHelper('C');
                case DivisionType.BhavaRegiomontanus:
                    return this.ToDivisionPositionBhavaHelper('R');
                case DivisionType.BhavaAlcabitus:
                    return this.ToDivisionPositionBhavaHelper('B');
                case DivisionType.BhavaAxial:
                    return this.ToDivisionPositionBhavaHelper('X');
                case DivisionType.HoraParivrittiDwaya:
                    return ToRegularDivisionPosition(2);
                case DivisionType.HoraKashinath:
                    return ToDivisionPositionHoraKashinath();
                case DivisionType.HoraParasara:
                    return ToDivisionPositionHoraParasara();
                case DivisionType.HoraJagannath:
                    return ToDivisionPositionHoraJagannath();
                case DivisionType.DrekkanaParasara:
                    return ToDivisionPositionDrekanna(3);
                case DivisionType.DrekkanaJagannath:
                    return ToDivisionPositionDrekannaJagannath();
                case DivisionType.DrekkanaParivrittitraya:
                    return ToRegularDivisionPosition(3);
                case DivisionType.DrekkanaSomnath:
                    return ToDivisionPositionDrekkanaSomnath();
                case DivisionType.Chaturthamsa:
                    return ToDivisionPositionChaturthamsa(4);
                case DivisionType.Panchamsa:
                    return ToDivisionPositionPanchamsa();
                case DivisionType.Shashthamsa:
                    return ToDivisionPositionShashthamsa(6);
                case DivisionType.Saptamsa:
                    return ToDivisionPositionSaptamsa(7);
                case DivisionType.Ashtamsa:
                    return ToRegularDivisionPosition(8);
                case DivisionType.AshtamsaRaman:
                    return ToDivisionPositionAshtamsaRaman();
                case DivisionType.Navamsa:
                    return ToDivisionPositionNavamsa();
                case DivisionType.Dasamsa:
                    return ToDivisionPositionDasamsa(10);
                case DivisionType.Rudramsa:
                    return ToDivisionPositionRudramsa();
                case DivisionType.RudramsaRaman:
                    return ToDivisionPositionRudramsaRaman();
                case DivisionType.Dwadasamsa:
                    return ToDivisionPositionDwadasamsa(12);
                case DivisionType.Shodasamsa:
                    return ToDivisionPositionShodasamsa();
                case DivisionType.Vimsamsa:
                    return ToDivisionPositionVimsamsa();
                case DivisionType.Chaturvimsamsa:
                    return ToDivisionPositionChaturvimsamsa(24);
                case DivisionType.Nakshatramsa:
                    return ToDivisionPositionNakshatramsa();
                case DivisionType.Trimsamsa:
                    return ToDivisionPositionTrimsamsa();
                case DivisionType.TrimsamsaParivritti:
                    return ToRegularDivisionPosition(30);
                case DivisionType.TrimsamsaSimple:
                    return ToDivisionPositionTrimsamsaSimple();
                case DivisionType.Khavedamsa:
                    return ToDivisionPositionKhavedamsa();
                case DivisionType.Akshavedamsa:
                    return ToDivisionPositionAkshavedamsa(45);
                case DivisionType.Shashtyamsa:
                    return ToDivisionPositionShashtyamsa();
                case DivisionType.Ashtottaramsa:
                    return ToRegularDivisionPosition(108);
                case DivisionType.Nadiamsa:
                    return ToDivisionPositionNadiamsa();
                case DivisionType.NadiamsaCKN:
                    return ToDivisionPositionNadiamsaCKN();
                case DivisionType.NavamsaDwadasamsa:
                    return ToDivisionPositionNavamsaDwadasamsa();
                case DivisionType.DwadasamsaDwadasamsa:
                    return ToDivisionPositionDwadasamsaDwadasamsa();
                case DivisionType.GenericParivritti:
                    return ToRegularDivisionPosition(d.NumParts);
                case DivisionType.GenericShashthamsa:
                    return ToDivisionPositionShashthamsa(d.NumParts);
                case DivisionType.GenericSaptamsa:
                    return ToDivisionPositionSaptamsa(d.NumParts);
                case DivisionType.GenericDasamsa:
                    return ToDivisionPositionDasamsa(d.NumParts);
                case DivisionType.GenericDwadasamsa:
                    return ToDivisionPositionDwadasamsa(d.NumParts);
                case DivisionType.GenericChaturvimsamsa:
                    return ToDivisionPositionChaturvimsamsa(d.NumParts);
                case DivisionType.GenericChaturthamsa:
                    return ToDivisionPositionChaturthamsa(d.NumParts);
                case DivisionType.GenericNakshatramsa:
                    return ToDivisionPositionNakshatramsa(d.NumParts);
                case DivisionType.GenericDrekkana:
                    return ToDivisionPositionDrekanna(d.NumParts);
                case DivisionType.GenericShodasamsa:
                    return ToDivisionPositionAkshavedamsa(d.NumParts);
                case DivisionType.GenericVimsamsa:
                    return ToDivisionPositionVimsamsa(d.NumParts);
            }
            Trace.Assert(false, "DivisionPosition Error");
            return new DivisionPosition(name, type, new ZodiacHouse(ZodiacHouseName.Ari), 0, 0, 0);
        }
        public Longitude ExtrapolateLongitude(Division d)
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            foreach (Division.SingleDivision dSingle in d.MultipleDivisions)
            {
                bp.Longitude = this.ExtrapolateLongitude(dSingle);
            }
            return bp.Longitude;
        }
        public Longitude ExtrapolateLongitude(Division.SingleDivision d)
        {
            DivisionPosition dp = this.ToDivisionPosition(d);
            Longitude lOffset = this.Longitude.Subtract(dp.CuspLower);
            Longitude lRange = new Longitude(dp.CuspHigher).Subtract(dp.CuspLower);
            Trace.Assert(lOffset.Value <= lRange.Value, "Extrapolation internal error: Slice smaller than range. Weird.");

            double newOffset = (lOffset.Value / lRange.Value) * 30.0;
            double newBase = ((double)((int)dp.ZodiacHouse.Value - 1)) * 30.0;
            return (new Longitude(newOffset + newBase));
        }
    }

}
