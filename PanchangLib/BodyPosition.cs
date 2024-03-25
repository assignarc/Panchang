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
        public Body.Name name;
        public BodyType.Name type;
        public Horoscope h;
        public Longitude longitude { get { return m_lon; } set { m_lon = value; } }
        public double latitude { get { return m_lat; } set { m_lat = value; } }
        public double distance { get { return m_dist; } set { m_dist = value; } }
        public double speed_longitude { get { return m_splon; } set { m_splon = value; } }
        public double speed_latitude { get { return m_splat; } set { m_splat = value; } }
        public double speed_distance { get { return m_spdist; } set { m_spdist = value; } }
        public BodyPosition(Horoscope _h, Body.Name aname, BodyType.Name atype, Longitude lon, double lat, double dist, double splon, double splat, double spdist)
        {
            this.longitude = lon;
            m_lat = lat;
            m_dist = dist;
            m_splon = splon;
            m_splat = splat;
            m_spdist = spdist;
            name = aname;
            type = atype;
            h = _h;
            //Console.WriteLine ("{0} {1} {2}", aname.ToString(), lon.value, splon);
        }
        public object Clone()
        {
            BodyPosition bp = new BodyPosition(h, name, type, m_lon.add(0), m_lat,
                m_dist, m_splon, m_splat, m_spdist);
            bp.otherString = this.otherString;
            return bp;
        }

        public int partOfZodiacHouse(int n)
        {
            Longitude l = this.longitude;
            double offset = l.toZodiacHouseOffset();
            int part = (int)(Math.Floor(offset / (30.0 / n))) + 1;
            Trace.Assert(part >= 1 && part <= n);
            return part;
        }
        private DivisionPosition populateRegularCusps(int n, DivisionPosition dp)
        {
            int part = partOfZodiacHouse(n);
            double cusp_length = 30.0 / ((double)n);
            dp.cusp_lower = ((double)(part - 1)) * cusp_length;
            dp.cusp_lower += m_lon.toZodiacHouseBase();
            dp.cusp_higher = dp.cusp_lower + cusp_length;
            dp.part = part;

            //if (dp.type == BodyType.Name.Graha || dp.type == BodyType.Name.Lagna)
            //Console.WriteLine ("D: {0} {1} {2} {3} {4} {5}", 
            //	n, dp.name, cusp_length,
            //	dp.cusp_lower, m_lon.value, dp.cusp_higher);


            return dp;
        }

        /// <summary>
        /// Many of the varga divisions (like navamsa) are regular divisions,
        /// and can be implemented by a single method. We do this when possible.
        /// </summary>
        /// <param name="n">The number of parts a house is divided into</param>
        /// <returns>The DivisionPosition the body falls into</returns>
        /// 
        private DivisionPosition toRegularDivisionPosition(int n)
        {
            int zhouse = (int)(m_lon.toZodiacHouse().value);
            int num_parts = (((int)zhouse) - 1) * n + partOfZodiacHouse(n);
            ZodiacHouse div_house = (new ZodiacHouse(ZodiacHouseName.Ari)).add(num_parts);
            DivisionPosition dp = new DivisionPosition(name, type, div_house, 0, 0, 0);
            populateRegularCusps(n, dp);
            return dp;
        }

        private DivisionPosition toRegularDivisionPositionFromCurrentHouseOddEven(int n)
        {
            int zhouse = (int)(m_lon.toZodiacHouse().value);
            int num_parts = partOfZodiacHouse(n);
            ZodiacHouse div_house = m_lon.toZodiacHouse().add(num_parts);
            DivisionPosition dp = new DivisionPosition(name, type, div_house, 0, 0, 0);
            populateRegularCusps(n, dp);
            return dp;
        }


        private DivisionPosition toBhavaDivisionPositionRasi(Longitude[] cusps)
        {
            Debug.Assert(cusps.Length == 13);
            cusps[12] = cusps[0];
            for (int i = 0; i < 12; i++)
            {
                if (m_lon.sub(cusps[i]).value <= cusps[i + 1].sub(cusps[i]).value)
                    return new DivisionPosition(name, type, new ZodiacHouse((ZodiacHouseName)i + 1),
                        cusps[i].value, cusps[i + 1].value, 1);
            }
            throw new Exception();
        }
        private DivisionPosition toBhavaDivisionPositionHouse(Longitude[] cusps)
        {
            Debug.Assert(cusps.Length == 13);

            ZodiacHouse zlagna = h.getPosition(Body.Name.Lagna).toDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house;
            for (int i = 0; i < 12; i++)
            {
                if (m_lon.sub(cusps[i]).value < cusps[i + 1].sub(cusps[i]).value)
                {
                    //Console.WriteLine ("Found {4} - {0} in cusp {3} between {1} and {2}", this.m_lon.value,
                    //	cusps[i].value, cusps[i+1].value, i+1, this.name.ToString());

                    return new DivisionPosition(name, type,
                        zlagna.add(i + 1), cusps[i].value, cusps[i + 1].value, 1);
                }
            }
            return new DivisionPosition(name, type,
                zlagna.add(1), cusps[0].value, cusps[1].value, 1);
        }
        private DivisionPosition toDivisionPositionBhavaEqual()
        {
            double offset = h.getPosition(Body.Name.Lagna).longitude.toZodiacHouseOffset();
            Longitude[] cusps = new Longitude[13];
            for (int i = 0; i < 12; i++)
                cusps[i] = new Longitude(i * 30.0 + offset - 15.0);
            return this.toBhavaDivisionPositionRasi(cusps);
        }
        private DivisionPosition toDivisionPositionBhavaPada()
        {
            Longitude[] cusps = new Longitude[13];
            double offset = h.getPosition(Body.Name.Lagna).longitude.toZodiacHouseOffset();
            int padasOffset = (int)Math.Floor(offset / (360.0 / 108.0));
            double startOffset = (double)padasOffset * (360.0 / 108.0);

            for (int i = 0; i < 12; i++)
                cusps[i] = new Longitude(i * 30.0 + startOffset - 15.0);
            return this.toBhavaDivisionPositionRasi(cusps);
        }
        private DivisionPosition toDivisionPositionBhavaHelper(int hsys)
        {
            Longitude[] cusps = new Longitude[13];
            double[] dCusps = new double[13];
            double[] ascmc = new double[10];

            if (hsys != h.swephHouseSystem)
            {
                h.swephHouseSystem = hsys;
                h.populateHouseCusps();
            }
            return this.toBhavaDivisionPositionHouse(h.swephHouseCusps);
        }
        private bool HoraSunDayNight()
        {
            int sign = (int)m_lon.toZodiacHouse().value;
            int part = partOfZodiacHouse(2);
            if (m_lon.toZodiacHouse().isDaySign())
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
            int sign = (int)m_lon.toZodiacHouse().value;
            int part = partOfZodiacHouse(2);
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
        private DivisionPosition toDivisionPositionHoraKashinath()
        {
            int[] daySigns = new int[13] { 0, 8, 7, 6, 5, 5, 6, 7, 8, 12, 11, 11, 12 };
            int[] nightSigns = new int[13] { 0, 1, 2, 3, 4, 4, 3, 2, 1, 9, 10, 10, 9 };

            ZodiacHouse zh;
            int sign = (int)m_lon.toZodiacHouse().value;
            if (this.HoraSunOddEven()) zh = new ZodiacHouse((ZodiacHouseName)daySigns[sign]);
            else zh = new ZodiacHouse((ZodiacHouseName)nightSigns[sign]);
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);
            this.populateRegularCusps(2, dp);
            return dp;
        }
        private DivisionPosition toDivisionPositionHoraJagannath()
        {
            ZodiacHouse zh = m_lon.toZodiacHouse();

            Console.WriteLine("{2} in {3}: OddEven is {0}, DayNight is {1}",
                this.HoraSunOddEven(), this.HoraSunDayNight(), this.name, zh.value);

            if (this.HoraSunDayNight() && false == this.HoraSunOddEven())
                zh = zh.add(7);
            else if (false == this.HoraSunDayNight() && true == this.HoraSunOddEven())
                zh = zh.add(7);

            Console.WriteLine("{0} ends in {1}", this.name, zh.value);

            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);
            this.populateRegularCusps(2, dp);
            return dp;
        }
        private DivisionPosition toDivisionPositionHoraParasara()
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
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);
            dp.ruler_index = ruler_index;
            return this.populateRegularCusps(2, dp);
        }
        private DivisionPosition toDivisionPositionDrekanna(int n)
        {
            int[] offset = new int[4] { 9, 1, 5, 9 };
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.add(offset[part % 3]);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            populateRegularCusps(n, dp);
            if (n == 3)
            {
                int ruler_index = (int)dp.zodiac_house.value % 3;
                if (ruler_index == 0) ruler_index = 3;
                dp.ruler_index = ruler_index;
            }
            return dp;
        }
        private DivisionPosition toDivisionPositionDrekannaJagannath()
        {
            ZodiacHouse zh = m_lon.toZodiacHouse();
            ZodiacHouse zhm;
            ZodiacHouse dhouse;
            int mod = ((int)(m_lon.toZodiacHouse().value)) % 3;
            // Find moveable sign in trines
            switch (mod)
            {
                case 1: zhm = zh.add(1); break;
                case 2: zhm = zh.add(9); break;
                default: zhm = zh.add(5); break;
            }

            // From moveable sign, 3 parts belong to the trines
            int part = partOfZodiacHouse(3);
            switch (part)
            {
                case 1: dhouse = zhm.add(1); break;
                case 2: dhouse = zhm.add(5); break;
                default: dhouse = zhm.add(9); break;
            }

            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(3, dp);
        }
        private DivisionPosition toDivisionPositionDrekkanaSomnath()
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 2;
            int part = partOfZodiacHouse(3);
            ZodiacHouse zh = m_lon.toZodiacHouse();
            int p = (int)zh.value;

            if (mod == 0) p--;
            p = (p - 1) / 2;
            int num_done = p * 3;

            ZodiacHouse zh1 = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zh2;
            if (mod == 1) zh2 = zh1.add(num_done + part);
            else zh2 = zh1.addReverse(num_done + part + 1);
            DivisionPosition dp = new DivisionPosition(name, type, zh2, 0, 0, 0);
            return this.populateRegularCusps(3, dp);
        }
        private DivisionPosition toDivisionPositionChaturthamsa(int n)
        {
            int[] offset = new int[5] { 10, 1, 4, 7, 10 };
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.add(offset[part % 4]);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 4)
                dp.ruler_index = part;
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionShashthamsa(int n)
        {
            int mod = ((int)(m_lon.toZodiacHouse().value)) % 2;
            ZodiacHouseName dhousen = (mod % 2 == 1) ? ZodiacHouseName.Ari : ZodiacHouseName.Lib;
            ZodiacHouse dhouse = (new ZodiacHouse(dhousen)).add(partOfZodiacHouse(n));
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionSaptamsa(int n)
        {
            int part = partOfZodiacHouse(n);
            ZodiacHouse zh = m_lon.toZodiacHouse();
            if (false == zh.isOdd())
                zh = zh.add(7);
            zh = zh.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, zh, 0, 0, 0);

            if (n == 7)
            {
                if (this.longitude.toZodiacHouse().isOdd())
                    dp.ruler_index = part;
                else
                    dp.ruler_index = 8 - part;
            }
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionNavamsa()
        {
            int part = partOfZodiacHouse(9);
            DivisionPosition dp = this.toRegularDivisionPosition(9);
            switch ((int)this.longitude.toZodiacHouse().value % 3)
            {
                case 1: dp.ruler_index = part; break;
                case 2: dp.ruler_index = part + 1; break;
                case 0: dp.ruler_index = part + 2; break;
            }
            while (dp.ruler_index > 3) dp.ruler_index -= 3;
            return dp;
        }
        private DivisionPosition toDivisionPositionAshtamsaRaman()
        {
            ZodiacHouse zstart = null;
            switch ((int)m_lon.toZodiacHouse().value % 3)
            {
                case 1: zstart = new ZodiacHouse(ZodiacHouseName.Ari); break;
                case 2: zstart = new ZodiacHouse(ZodiacHouseName.Leo); break;
                case 0:
                default:
                    zstart = new ZodiacHouse(ZodiacHouseName.Sag); break;
            }
            ZodiacHouse dhouse = zstart.add(partOfZodiacHouse(8));
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(8, dp);
        }
        private DivisionPosition toDivisionPositionPanchamsa()
        {
            ZodiacHouseName[] offset_odd = new ZodiacHouseName[5]
                        {
                            ZodiacHouseName.Ari, ZodiacHouseName.Aqu, ZodiacHouseName.Sag,
                            ZodiacHouseName.Gem, ZodiacHouseName.Lib};
            ZodiacHouseName[] offset_even = new ZodiacHouseName[5]
                        {
                            ZodiacHouseName.Tau, ZodiacHouseName.Vir, ZodiacHouseName.Pis,
                            ZodiacHouseName.Cap, ZodiacHouseName.Sco};
            int part = partOfZodiacHouse(5);
            int mod = ((int)(m_lon.toZodiacHouse().value)) % 2;
            ZodiacHouseName dhouse = (mod % 2 == 1) ? offset_odd[part - 1] : offset_even[part - 1];
            DivisionPosition dp = new DivisionPosition(name, type, new ZodiacHouse(dhouse), 0, 0, 0);
            return this.populateRegularCusps(5, dp);
        }
        private DivisionPosition toDivisionPositionRudramsa()
        {
            ZodiacHouse zari = new ZodiacHouse(ZodiacHouseName.Ari);
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int diff = zari.numHousesBetween(zhouse);
            ZodiacHouse zstart = zari.addReverse(diff);
            int part = partOfZodiacHouse(11);
            ZodiacHouse zend = zstart.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, zend, 0, 0, 0);
            return this.populateRegularCusps(11, dp);
        }
        private DivisionPosition toDivisionPositionRudramsaRaman()
        {
            ZodiacHouse zhstart = m_lon.toZodiacHouse().add(12);
            int part = partOfZodiacHouse(11);
            ZodiacHouse zend = zhstart.addReverse(part);
            DivisionPosition dp = new DivisionPosition(name, type, zend, 0, 0, 0);
            return this.populateRegularCusps(11, dp);
        }
        private DivisionPosition toDivisionPositionDasamsa(int n)
        {
            int[] offset = new int[2] { 9, 1 };
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            ZodiacHouse dhouse = zhouse.add(offset[((int)zhouse.value) % 2]);
            int part = partOfZodiacHouse(n);
            dhouse = dhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 10)
            {
                if (this.longitude.toZodiacHouse().isOdd())
                    dp.ruler_index = part;
                else
                    dp.ruler_index = 11 - part;
            }
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionDwadasamsa(int n)
        {
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = zhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 12)
                dp.ruler_index = Basics.normalize_inc(1, 4, part);
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionShodasamsa()
        {
            int part = partOfZodiacHouse(16);
            DivisionPosition dp = this.toRegularDivisionPosition(16);
            int ruler = part;
            if (this.longitude.toZodiacHouse().isOdd())
                ruler = part;
            else
                ruler = 17 - part;
            dp.ruler_index = Basics.normalize_inc(1, 4, ruler);
            return dp;
        }
        private DivisionPosition toDivisionPositionVimsamsa(int n)
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 3;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Sag; break;
                default: dhousename = ZodiacHouseName.Leo; break;
            }
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionVimsamsa()
        {
            int part = partOfZodiacHouse(20);
            DivisionPosition dp = this.toRegularDivisionPosition(20);
            if (this.longitude.toZodiacHouse().isOdd())
                dp.ruler_index = part;
            else
                dp.ruler_index = 20 + part;
            return dp;
        }
        private DivisionPosition toDivisionPositionChaturvimsamsa(int n)
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 2;
            ZodiacHouseName dhousename = (mod % 2 == 1) ? ZodiacHouseName.Leo : ZodiacHouseName.Can;
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 24)
            {
                if (this.longitude.toZodiacHouse().isOdd())
                    dp.ruler_index = part;
                else
                    dp.ruler_index = 25 - part;
                dp.ruler_index = Basics.normalize_inc(1, 12, dp.ruler_index);
            }
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionNakshatramsa(int n)
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 4;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Can; break;
                case 3: dhousename = ZodiacHouseName.Lib; break;
                default: dhousename = ZodiacHouseName.Cap; break;
            }
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionNakshatramsa()
        {
            DivisionPosition dp = this.toRegularDivisionPosition(27);
            dp.ruler_index = this.partOfZodiacHouse(27);
            return dp;
        }
        private DivisionPosition toDivisionPositionTrimsamsaSimple()
        {
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(30);
            ZodiacHouse dhouse = zhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            return this.populateRegularCusps(30, dp);
        }
        private DivisionPosition toDivisionPositionTrimsamsa()
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 2;
            double off = m_lon.toZodiacHouseOffset();
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
            cusp_lower += m_lon.toZodiacHouseBase();
            cusp_higher += m_lon.toZodiacHouseBase();

            DivisionPosition dp = new DivisionPosition(name, type, dhouse, cusp_lower, cusp_higher, 0);
            dp.ruler_index = ruler_index;
            dp.part = part;
            return dp;
        }
        private DivisionPosition toDivisionPositionKhavedamsa()
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 2;
            ZodiacHouseName dhousename = (mod % 2 == 1) ? ZodiacHouseName.Ari : ZodiacHouseName.Lib;
            int part = partOfZodiacHouse(40);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            dp.ruler_index = Basics.normalize_inc(1, 12, part);
            return this.populateRegularCusps(40, dp);
        }
        private DivisionPosition toDivisionPositionAkshavedamsa(int n)
        {
            int mod = ((int)(m_lon.toZodiacHouse()).value) % 3;
            ZodiacHouseName dhousename;
            switch (mod)
            {
                case 1: dhousename = ZodiacHouseName.Ari; break;
                case 2: dhousename = ZodiacHouseName.Leo; break;
                default: dhousename = ZodiacHouseName.Sag; break;
            }
            int part = partOfZodiacHouse(n);
            ZodiacHouse dhouse = (new ZodiacHouse(dhousename)).add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (n == 45)
            {
                switch ((int)this.longitude.toZodiacHouse().value % 3)
                {
                    case 1: dp.ruler_index = part; break;
                    case 2: dp.ruler_index = part + 1; break;
                    case 0: dp.ruler_index = part + 2; break;
                }
                dp.ruler_index = Basics.normalize_inc(1, 3, dp.ruler_index);
            }
            return this.populateRegularCusps(n, dp);
        }
        private DivisionPosition toDivisionPositionShashtyamsa()
        {
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(60);
            ZodiacHouse dhouse = zhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            if (this.longitude.toZodiacHouse().isOdd())
                dp.ruler_index = part;
            else
                dp.ruler_index = 61 - part;
            return this.populateRegularCusps(60, dp);
        }
        private DivisionPosition toDivisionPositionNadiamsa()
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
            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            int part = partOfZodiacHouse(150);
            ZodiacHouse dhouse = zhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);
            switch ((int)this.longitude.toZodiacHouse().value % 3)
            {
                case 1: dp.ruler_index = part; break;
                case 2: dp.ruler_index = 151 - part; break;
                case 0: dp.ruler_index = Basics.normalize_inc(1, 150, 75 + part); break;
            }
            return this.populateRegularCusps(150, dp);
        }
        static bool mbNadiamsaCKNCalculated = false;
        static double[] mNadiamsaCusps = null;
        private void calculateNadiamsaCusps()
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
            ArrayList alSorted = new ArrayList(150);

            alSorted.Add((double)0.0);
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
        private DivisionPosition toDivisionPositionNadiamsaCKN()
        {
            this.calculateNadiamsaCusps();
            int part = partOfZodiacHouse(150) - 10;
            if (part < 0) part = 0;

            for (; part < 149; part++)
            {
                Longitude lonLow = new Longitude(BodyPosition.mNadiamsaCusps[part]);
                Longitude lonHigh = new Longitude(BodyPosition.mNadiamsaCusps[part + 1]);
                Longitude offset = new Longitude(this.longitude.toZodiacHouseOffset());

                if (offset.sub(lonLow).value <= lonHigh.sub(lonLow).value)
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

            ZodiacHouse zhouse = m_lon.toZodiacHouse();
            ZodiacHouse dhouse = zhouse.add(part);
            DivisionPosition dp = new DivisionPosition(name, type, dhouse, 0, 0, 0);


            switch ((int)this.longitude.toZodiacHouse().value % 3)
            {
                case 1: dp.ruler_index = part; break;
                case 2: dp.ruler_index = 151 - part; break;
                case 0: dp.ruler_index = Basics.normalize_inc(1, 150, 75 + part); break;
            }
            dp.cusp_lower = this.longitude.toZodiacHouseBase() + BodyPosition.mNadiamsaCusps[part - 1];
            dp.cusp_higher = this.longitude.toZodiacHouseBase() + BodyPosition.mNadiamsaCusps[part];
            dp.part = part;
            return dp;
        }
        private DivisionPosition toDivisionPositionNavamsaDwadasamsa()
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            bp.longitude = bp.extrapolateLongitude(new Division(DivisionType.Navamsa));
            DivisionPosition dp = bp.toDivisionPositionDwadasamsa(12);
            this.populateRegularCusps(108, dp);
            return dp;
        }
        private DivisionPosition toDivisionPositionDwadasamsaDwadasamsa()
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            bp.longitude = bp.extrapolateLongitude(new Division(DivisionType.Dwadasamsa));
            DivisionPosition dp = bp.toDivisionPositionDwadasamsa(12);
            this.populateRegularCusps(144, dp);
            return dp;
        }
        /// <summary>
        /// Calculated any known Varga positions. Simply calls the appropriate
        /// helper function
        /// </summary>
        /// <param name="dtype">The requested DivisionType</param>
        /// <returns>A division Position</returns>


        public DivisionPosition toDivisionPosition(Division d)
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            DivisionPosition dp = null;
            for (int i = 0; i < d.MultipleDivisions.Length; i++)
            {
                dp = bp.toDivisionPosition(d.MultipleDivisions[i]);
                bp.longitude = bp.extrapolateLongitude(d.MultipleDivisions[i]);
            }
            return dp;
        }
        public DivisionPosition toDivisionPosition(Division.SingleDivision d)
        {
            if (d.NumParts < 1)
                d.NumParts = 1;

            switch (d.Varga)
            {
                case DivisionType.Rasi:
                    return toRegularDivisionPosition(1);
                case DivisionType.BhavaPada:
                    return toDivisionPositionBhavaPada();
                case DivisionType.BhavaEqual:
                    return toDivisionPositionBhavaEqual();
                case DivisionType.BhavaSripati:
                    return this.toDivisionPositionBhavaHelper('O');
                case DivisionType.BhavaKoch:
                    return this.toDivisionPositionBhavaHelper('K');
                case DivisionType.BhavaPlacidus:
                    return this.toDivisionPositionBhavaHelper('P');
                case DivisionType.BhavaCampanus:
                    return this.toDivisionPositionBhavaHelper('C');
                case DivisionType.BhavaRegiomontanus:
                    return this.toDivisionPositionBhavaHelper('R');
                case DivisionType.BhavaAlcabitus:
                    return this.toDivisionPositionBhavaHelper('B');
                case DivisionType.BhavaAxial:
                    return this.toDivisionPositionBhavaHelper('X');
                case DivisionType.HoraParivrittiDwaya:
                    return toRegularDivisionPosition(2);
                case DivisionType.HoraKashinath:
                    return toDivisionPositionHoraKashinath();
                case DivisionType.HoraParasara:
                    return toDivisionPositionHoraParasara();
                case DivisionType.HoraJagannath:
                    return toDivisionPositionHoraJagannath();
                case DivisionType.DrekkanaParasara:
                    return toDivisionPositionDrekanna(3);
                case DivisionType.DrekkanaJagannath:
                    return toDivisionPositionDrekannaJagannath();
                case DivisionType.DrekkanaParivrittitraya:
                    return toRegularDivisionPosition(3);
                case DivisionType.DrekkanaSomnath:
                    return toDivisionPositionDrekkanaSomnath();
                case DivisionType.Chaturthamsa:
                    return toDivisionPositionChaturthamsa(4);
                case DivisionType.Panchamsa:
                    return toDivisionPositionPanchamsa();
                case DivisionType.Shashthamsa:
                    return toDivisionPositionShashthamsa(6);
                case DivisionType.Saptamsa:
                    return toDivisionPositionSaptamsa(7);
                case DivisionType.Ashtamsa:
                    return toRegularDivisionPosition(8);
                case DivisionType.AshtamsaRaman:
                    return toDivisionPositionAshtamsaRaman();
                case DivisionType.Navamsa:
                    return toDivisionPositionNavamsa();
                case DivisionType.Dasamsa:
                    return toDivisionPositionDasamsa(10);
                case DivisionType.Rudramsa:
                    return toDivisionPositionRudramsa();
                case DivisionType.RudramsaRaman:
                    return toDivisionPositionRudramsaRaman();
                case DivisionType.Dwadasamsa:
                    return toDivisionPositionDwadasamsa(12);
                case DivisionType.Shodasamsa:
                    return toDivisionPositionShodasamsa();
                case DivisionType.Vimsamsa:
                    return toDivisionPositionVimsamsa();
                case DivisionType.Chaturvimsamsa:
                    return toDivisionPositionChaturvimsamsa(24);
                case DivisionType.Nakshatramsa:
                    return toDivisionPositionNakshatramsa();
                case DivisionType.Trimsamsa:
                    return toDivisionPositionTrimsamsa();
                case DivisionType.TrimsamsaParivritti:
                    return toRegularDivisionPosition(30);
                case DivisionType.TrimsamsaSimple:
                    return toDivisionPositionTrimsamsaSimple();
                case DivisionType.Khavedamsa:
                    return toDivisionPositionKhavedamsa();
                case DivisionType.Akshavedamsa:
                    return toDivisionPositionAkshavedamsa(45);
                case DivisionType.Shashtyamsa:
                    return toDivisionPositionShashtyamsa();
                case DivisionType.Ashtottaramsa:
                    return toRegularDivisionPosition(108);
                case DivisionType.Nadiamsa:
                    return toDivisionPositionNadiamsa();
                case DivisionType.NadiamsaCKN:
                    return toDivisionPositionNadiamsaCKN();
                case DivisionType.NavamsaDwadasamsa:
                    return toDivisionPositionNavamsaDwadasamsa();
                case DivisionType.DwadasamsaDwadasamsa:
                    return toDivisionPositionDwadasamsaDwadasamsa();
                case DivisionType.GenericParivritti:
                    return toRegularDivisionPosition(d.NumParts);
                case DivisionType.GenericShashthamsa:
                    return toDivisionPositionShashthamsa(d.NumParts);
                case DivisionType.GenericSaptamsa:
                    return toDivisionPositionSaptamsa(d.NumParts);
                case DivisionType.GenericDasamsa:
                    return toDivisionPositionDasamsa(d.NumParts);
                case DivisionType.GenericDwadasamsa:
                    return toDivisionPositionDwadasamsa(d.NumParts);
                case DivisionType.GenericChaturvimsamsa:
                    return toDivisionPositionChaturvimsamsa(d.NumParts);
                case DivisionType.GenericChaturthamsa:
                    return toDivisionPositionChaturthamsa(d.NumParts);
                case DivisionType.GenericNakshatramsa:
                    return toDivisionPositionNakshatramsa(d.NumParts);
                case DivisionType.GenericDrekkana:
                    return toDivisionPositionDrekanna(d.NumParts);
                case DivisionType.GenericShodasamsa:
                    return toDivisionPositionAkshavedamsa(d.NumParts);
                case DivisionType.GenericVimsamsa:
                    return toDivisionPositionVimsamsa(d.NumParts);
            }
            Trace.Assert(false, "DivisionPosition Error");
            return new DivisionPosition(name, type, new ZodiacHouse(ZodiacHouseName.Ari), 0, 0, 0);
        }
        public Longitude extrapolateLongitude(Division d)
        {
            BodyPosition bp = (BodyPosition)this.Clone();
            foreach (Division.SingleDivision dSingle in d.MultipleDivisions)
            {
                bp.longitude = this.extrapolateLongitude(dSingle);
            }
            return bp.longitude;
        }
        public Longitude extrapolateLongitude(Division.SingleDivision d)
        {
            DivisionPosition dp = this.toDivisionPosition(d);
            Longitude lOffset = this.longitude.sub(dp.cusp_lower);
            Longitude lRange = new Longitude(dp.cusp_higher).sub(dp.cusp_lower);
            Trace.Assert(lOffset.value <= lRange.value, "Extrapolation internal error: Slice smaller than range. Weird.");

            double newOffset = (lOffset.value / lRange.value) * 30.0;
            double newBase = ((double)((int)dp.zodiac_house.value - 1)) * 30.0;
            return (new Longitude(newOffset + newBase));
        }
    }

}
