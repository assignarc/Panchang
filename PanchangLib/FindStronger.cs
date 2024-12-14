using System;
using System.Collections;

namespace org.transliteral.panchang
{

    public class FindStronger
    {
        Horoscope h;
        Division dtype;
        ArrayList rules;
        bool bUseSimpleLords;


        public FindStronger(Horoscope _h, Division _dtype, ArrayList _rules, bool _UseSimpleLords)
        {
            h = _h;
            dtype = _dtype;
            rules = _rules;
            bUseSimpleLords = _UseSimpleLords;

        }
        public FindStronger(Horoscope _h, Division _dtype, ArrayList _rules)
        {
            h = _h;
            dtype = _dtype;
            rules = _rules;
            bUseSimpleLords = false;
        }

        static private StrengthOptions GetStrengthOptions(Horoscope h)
        {
            if (h.strength_options == null)
                return GlobalOptions.Instance.SOptions;
            else
                return h.strength_options;
        }
        static public ArrayList RulesNaisargikaDasaRasi(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).NaisargikaDasaRasi);
        }
        static public ArrayList RulesNarayanaDasaRasi(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).NarayanaDasaRasi);
        }
        static public ArrayList RulesKarakaKendradiGrahaDasaRasi(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).KarakaKendradiGrahaDasaRasi);
        }
        static public ArrayList RulesKarakaKendradiGrahaDasaGraha(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).KarakaKendradiGrahaDasaGraha);
        }
        static public ArrayList RulesKarakaKendradiGrahaDasaColord(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).KarakaKendradiGrahaDasaColord);
        }
        static public ArrayList RulesMoolaDasaRasi(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).MoolaDasaRasi);
        }
        static public ArrayList RulesNavamsaDasaRasi(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).NavamsaDasaRasi);
        }
        static public ArrayList RulesJaiminiFirstRasi(Horoscope h)
        {
            return  new ArrayList
            {
                ERasiStrength.AtmaKaraka,
                ERasiStrength.Conjunction,
                ERasiStrength.Exaltation,
                ERasiStrength.MoolaTrikona,
                ERasiStrength.OwnHouse,
                ERasiStrength.RasisNature,
                ERasiStrength.LordIsAtmaKaraka,
                ERasiStrength.LordsLongitude,
                ERasiStrength.LordInDifferentOddity
            };
        }
        static public ArrayList RulesJaiminiSecondRasi(Horoscope h)
        {
            return new ArrayList
            {
                ERasiStrength.AspectsRasi
            };
           
        }

        static public ArrayList RulesNaisargikaDasaGraha(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).NaisargikaDasaGraha);
        }
        static public ArrayList RulesVimsottariGraha(Horoscope h)
        {
            return new ArrayList
            {
                EGrahaStrength.KendraConjunction,
                EGrahaStrength.First
            };
           
        }

        static public ArrayList RulesStrongerCoLord(Horoscope h)
        {
            return new ArrayList(FindStronger.GetStrengthOptions(h).Colord);
        }

        public OrderedZodiacHouses[] ResultsZodiacKendras(ZodiacHouseName _zh)
        {
            OrderedZodiacHouses[] zRet = new OrderedZodiacHouses[3];
            ZodiacHouse zh = new ZodiacHouse(_zh);
            ZodiacHouseName[] zh1 = new ZodiacHouseName[4] { zh.add(1).value, zh.add(4).value, zh.add(7).value, zh.add(10).value };
            ZodiacHouseName[] zh2 = new ZodiacHouseName[4] { zh.add(2).value, zh.add(5).value, zh.add(8).value, zh.add(11).value };
            ZodiacHouseName[] zh3 = new ZodiacHouseName[4] { zh.add(3).value, zh.add(6).value, zh.add(9).value, zh.add(12).value };
            zRet[0] = this.GetOrderedHouses(zh1);
            zRet[1] = this.GetOrderedHouses(zh2);
            zRet[2] = this.GetOrderedHouses(zh3);
            return zRet;
        }
        public ZodiacHouseName[] ResultsKendraRasis(ZodiacHouseName _zh)
        {
            ZodiacHouseName[] zRet = new ZodiacHouseName[12];
            ZodiacHouse zh = new ZodiacHouse(_zh);
            ZodiacHouseName[] zh1 = new ZodiacHouseName[4] { zh.add(1).value, zh.add(4).value, zh.add(7).value, zh.add(10).value };
            ZodiacHouseName[] zh2 = new ZodiacHouseName[4] { zh.add(2).value, zh.add(5).value, zh.add(8).value, zh.add(11).value };
            ZodiacHouseName[] zh3 = new ZodiacHouseName[4] { zh.add(3).value, zh.add(6).value, zh.add(9).value, zh.add(12).value };
            GetOrderedRasis(zh1).CopyTo(zRet, 0);
            GetOrderedRasis(zh2).CopyTo(zRet, 4);
            GetOrderedRasis(zh3).CopyTo(zRet, 8);
            return zRet;
        }
        public ZodiacHouseName[] ResultsFirstSeventhRasis()
        {
            ZodiacHouseName[] zRet = new ZodiacHouseName[12];
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Ari, ZodiacHouseName.Lib }).CopyTo(zRet, 0);
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Tau, ZodiacHouseName.Sco }).CopyTo(zRet, 2);
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Gem, ZodiacHouseName.Sag }).CopyTo(zRet, 4);
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Can, ZodiacHouseName.Cap }).CopyTo(zRet, 6);
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Leo, ZodiacHouseName.Aqu }).CopyTo(zRet, 8);
            GetOrderedRasis(new ZodiacHouseName[] { ZodiacHouseName.Vir, ZodiacHouseName.Pis }).CopyTo(zRet, 10);
            return zRet;
        }


        public Body.Name[] GetOrderedGrahas()
        {
            Body.Name[] grahas = new Body.Name[]
            {
                Body.Name.Sun, Body.Name.Moon, Body.Name.Mars, Body.Name.Mercury,
                Body.Name.Jupiter, Body.Name.Venus, Body.Name.Saturn,
                Body.Name.Rahu, Body.Name.Ketu
            };
            return GetOrderedGrahas(grahas);
        }
        public Body.Name[] GetOrderedGrahas(Body.Name[] grahas)
        {
            if (grahas.Length <= 1)
                return grahas;

            for (int i = 0; i < grahas.Length - 1; i++)
            {
                for (int j = 0; j < grahas.Length - 1; j++)
                {
                    if (false == this.CmpGraha(grahas[j], grahas[j + 1], false))
                        (grahas[j + 1], grahas[j]) = (grahas[j], grahas[j + 1]);
                }
            }
            return grahas;
        }

        public ZodiacHouseName[] GetOrderedRasis()
        {
            ZodiacHouseName[] rasis = new ZodiacHouseName[]
            {
                ZodiacHouseName.Ari, ZodiacHouseName.Tau, ZodiacHouseName.Gem,
                ZodiacHouseName.Can, ZodiacHouseName.Leo, ZodiacHouseName.Vir,
                ZodiacHouseName.Lib, ZodiacHouseName.Sco, ZodiacHouseName.Sag,
                ZodiacHouseName.Cap, ZodiacHouseName.Aqu, ZodiacHouseName.Pis
            };
            return GetOrderedRasis(rasis);
        }
        public OrderedZodiacHouses GetOrderedHouses(ZodiacHouseName[] rasis)
        {
            ZodiacHouseName[] zh_ordered = GetOrderedRasis(rasis);
            OrderedZodiacHouses oz = new OrderedZodiacHouses();
            foreach (ZodiacHouseName zn in zh_ordered)
                oz.houses.Add(zn);
            return oz;
        }
        public ZodiacHouseName[] GetOrderedRasis(ZodiacHouseName[] rasis)
        {
            if (rasis.Length < 2) return rasis;

            int length = rasis.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - 1; j++)
                {
                    //System.Console.WriteLine ("Comparing {0} and {1}", i, j);
                    if (false == this.CmpRasi(rasis[j], rasis[j + 1], false))
                        (rasis[j + 1], rasis[j]) = (rasis[j], rasis[j + 1]);
                }
            }
            return rasis;
        }

        public ZodiacHouseName StrongerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord, ref int winner)
        {
            if (CmpRasi(za, zb, bSimpleLord, ref winner)) return za;
            return zb;
        }

        public ZodiacHouseName StrongerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            int winner = 0;
            return StrongerRasi(za, zb, bSimpleLord, ref winner);
        }
        public Body.Name StrongerGraha(Body.Name m, Body.Name n, bool bSimpleLord)
        {
            int winner = 0;
            return StrongerGraha(m, n, bSimpleLord, ref winner);
        }
        public Body.Name StrongerGraha(Body.Name m, Body.Name n, bool bSimpleLord, ref int winner)
        {
            if (CmpGraha(m, n, bSimpleLord, ref winner)) return m;
            return n;
        }
        public ZodiacHouseName WeakerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            if (CmpRasi(za, zb, bSimpleLord)) return zb;
            return za;
        }
        public Body.Name WeakerGraha(Body.Name m, Body.Name n, bool bSimpleLord)
        {
            if (CmpGraha(m, n, bSimpleLord)) return n;
            return m;
        }
        public bool CmpRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            int winner = 0;
            return CmpRasi(za, zb, bSimpleLord, ref winner);
        }

       

        public bool CmpRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord, ref int winner)
        {
            bool bRet = false;
            bool bFound = true;
            winner = 0;

            //System.Console.WriteLine("Rasi: {0} {1}", za.ToString(), zb.ToString());
            foreach (ERasiStrength s in rules)
            {
                //System.Console.WriteLine("Rasi::{0}", s);
                switch (s)
                {
                    case ERasiStrength.Conjunction:
                        try
                        {
                            bRet = new StrengthByConjunction(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.Exaltation:
                        try
                        {
                            bRet = new StrengthByExaltation(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.Longitude:
                        try
                        {
                            bRet = new StrengthByLongitude(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.AtmaKaraka:
                        try
                        {
                            bRet = new StrengthByAtmaKaraka(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.LordIsAtmaKaraka:
                        try
                        {
                            bRet = new StrengthByLordIsAtmaKaraka(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.RasisNature:
                        try
                        {
                            bRet = new StrengthByRasisNature(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.LordsNature:
                        try
                        {
                            bRet = new StrengthByLordsNature(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.AspectsRasi:
                        try
                        {
                            bRet = new StrengthByAspectsRasi(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.AspectsGraha:
                        try
                        {
                            bRet = new StrengthByAspectsGraha(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.LordInDifferentOddity:
                        try
                        {
                            bRet = new StrengthByLordInDifferentOddity(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.LordsLongitude:
                        try
                        {
                            bRet = new StrengthByLordsLongitude(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.NarayanaDasaLength:
                        try
                        {
                            bRet = new StrengthByNarayanaDasaLength(h, dtype, bSimpleLord).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.VimsottariDasaLength:
                        try
                        {
                            bRet = new StrengthByVimsottariDasaLength(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.MoolaTrikona:
                        try
                        {
                            bRet = new StrengthByMoolaTrikona(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.OwnHouse:
                        try
                        {
                            bRet = new StrengthByOwnHouse(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.KendraConjunction:
                        try
                        {
                            bRet = new StrengthByKendraConjunction(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.KarakaKendradiGrahaDasaLength:
                        try
                        {
                            bRet = new StrengthByKarakaKendradiGrahaDasaLength(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case ERasiStrength.First:
                        try
                        {
                            bRet = new StrengthByFirst(h, dtype).stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    default:
                        throw new Exception("Unknown Rasi Strength Rule");
                }
            }
            bFound = false;

        found:
            if (bFound == true) return bRet;
            return true;

        }

      

        public bool CmpGraha(Body.Name m, Body.Name n, bool bSimpleLord)
        {
            int winner = 0;
            return CmpGraha(m, n, bSimpleLord, ref winner);
        }
        public bool CmpGraha(Body.Name m, Body.Name n, bool bSimpleLord, ref int winner)
        {
            bool bRet = false;
            bool bFound = true;
            winner = 0;
            foreach (EGrahaStrength s in rules)
            {
                //Console.WriteLine("Trying {0}. Curr is {1}", s, winner);
                switch (s)
                {
                    case EGrahaStrength.Conjunction:
                        try
                        {
                            bRet = new StrengthByConjunction(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.Exaltation:
                        try
                        {
                            bRet = new StrengthByExaltation(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.Longitude:
                        try
                        {
                            bRet = new StrengthByLongitude(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.AtmaKaraka:
                        try
                        {
                            bRet = new StrengthByAtmaKaraka(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.RasisNature:
                        try
                        {
                            bRet = new StrengthByRasisNature(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.LordsNature:
                        try
                        {
                            bRet = new StrengthByLordsNature(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.AspectsRasi:
                        try
                        {
                            bRet = new StrengthByAspectsRasi(h, dtype, bSimpleLord).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.AspectsGraha:
                        try
                        {
                            bRet = new StrengthByAspectsGraha(h, dtype, bSimpleLord).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.NarayanaDasaLength:
                        try
                        {
                            bRet = new StrengthByNarayanaDasaLength(h, dtype, bSimpleLord).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.VimsottariDasaLength:
                        try
                        {
                            bRet = new StrengthByVimsottariDasaLength(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.MoolaTrikona:
                        try
                        {
                            bRet = new StrengthByMoolaTrikona(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.OwnHouse:
                        try
                        {
                            bRet = new StrengthByOwnHouse(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.NotInOwnHouse:
                        try
                        {
                            bRet = !(new StrengthByOwnHouse(h, dtype).stronger(m, n));
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.LordInOwnHouse:
                        try
                        {
                            bRet = new StrengthByLordInOwnHouse(h, dtype, bSimpleLord).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.LordInDifferentOddity:
                        try
                        {
                            bRet = new StrengthByLordInDifferentOddity(h, dtype, bSimpleLord).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.KendraConjunction:
                        try
                        {
                            bRet = new StrengthByKendraConjunction(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.KarakaKendradiGrahaDasaLength:
                        try
                        {
                            bRet = new StrengthByKarakaKendradiGrahaDasaLength(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case EGrahaStrength.First:
                        try
                        {
                            bRet = new StrengthByFirst(h, dtype).stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    default:
                        throw new Exception("Unknown Graha Strength Rule");
                }
            }
            bFound = false;

        found:
            if (bFound == true)
            {
                return bRet;
            }
            winner++;
            return true;
        }
    }

}
