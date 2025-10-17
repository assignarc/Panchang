using System;
using System.Collections;

namespace org.transliteral.panchang
{

    public class Strongest
    {
        Horoscope h;
        Division dtype;
        ArrayList rules;
        bool bUseSimpleLords;
        public Strongest(Horoscope _h, Division _dtype, ArrayList _rules, bool _UseSimpleLords)
        {
            h = _h;
            dtype = _dtype;
            rules = _rules;
            bUseSimpleLords = _UseSimpleLords;

        }
        public Strongest(Horoscope _h, Division _dtype, ArrayList _rules)
        {
            h = _h;
            dtype = _dtype;
            rules = _rules;
            bUseSimpleLords = false;
        }

        private static StrengthOptions GetStrengthOptions(Horoscope h)
        {
            if (h.StrengthOptions == null)
                return GlobalOptions.Instance.SOptions;
            else
                return h.StrengthOptions;
        }
        public static ArrayList RulesNaisargikaDasaRasi(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).NaisargikaDasaRasi);
        public static ArrayList RulesNarayanaDasaRasi(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).NarayanaDasaRasi);
        public static ArrayList RulesKarakaKendradiGrahaDasaRasi(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).KarakaKendradiGrahaDasaRasi);
        public static ArrayList RulesKarakaKendradiGrahaDasaGraha(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).KarakaKendradiGrahaDasaGraha);
        public static ArrayList RulesKarakaKendradiGrahaDasaColord(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).KarakaKendradiGrahaDasaColord);
        public static ArrayList RulesMoolaDasaRasi(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).MoolaDasaRasi);
        public static ArrayList RulesNavamsaDasaRasi(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).NavamsaDasaRasi);
        public static ArrayList RulesJaiminiFirstRasi(Horoscope h) => new ArrayList
            {
                RasiStrength.AtmaKaraka,
                RasiStrength.Conjunction,
                RasiStrength.Exaltation,
                RasiStrength.MoolaTrikona,
                RasiStrength.OwnHouse,
                RasiStrength.RasisNature,
                RasiStrength.LordIsAtmaKaraka,
                RasiStrength.LordsLongitude,
                RasiStrength.LordInDifferentOddity
            };
        public static ArrayList RulesJaiminiSecondRasi(Horoscope h) => new ArrayList
            {
                RasiStrength.AspectsRasi
            };

        public static ArrayList RulesNaisargikaDasaGraha(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).NaisargikaDasaGraha);
        public static ArrayList RulesVimsottariGraha(Horoscope h) => new ArrayList
            {
                GrahaStrength.KendraConjunction,
                GrahaStrength.First
            };

        public static ArrayList RulesStrongerCoLord(Horoscope h) => new ArrayList(Strongest.GetStrengthOptions(h).Colord);

        public OrderedZodiacHouses[] ResultsZodiacKendras(ZodiacHouseName _zh)
        {
            OrderedZodiacHouses[] zRet = new OrderedZodiacHouses[3];
            ZodiacHouse zh = new ZodiacHouse(_zh);
            ZodiacHouseName[] zh1 = new ZodiacHouseName[4] { zh.Add(1).Value, zh.Add(4).Value, zh.Add(7).Value, zh.Add(10).Value };
            ZodiacHouseName[] zh2 = new ZodiacHouseName[4] { zh.Add(2).Value, zh.Add(5).Value, zh.Add(8).Value, zh.Add(11).Value };
            ZodiacHouseName[] zh3 = new ZodiacHouseName[4] { zh.Add(3).Value, zh.Add(6).Value, zh.Add(9).Value, zh.Add(12).Value };
            zRet[0] = this.GetOrderedHouses(zh1);
            zRet[1] = this.GetOrderedHouses(zh2);
            zRet[2] = this.GetOrderedHouses(zh3);
            return zRet;
        }
        public ZodiacHouseName[] ResultsKendraRasis(ZodiacHouseName _zh)
        {
            ZodiacHouseName[] zRet = new ZodiacHouseName[12];
            ZodiacHouse zh = new ZodiacHouse(_zh);
            ZodiacHouseName[] zh1 = new ZodiacHouseName[4] { zh.Add(1).Value, zh.Add(4).Value, zh.Add(7).Value, zh.Add(10).Value };
            ZodiacHouseName[] zh2 = new ZodiacHouseName[4] { zh.Add(2).Value, zh.Add(5).Value, zh.Add(8).Value, zh.Add(11).Value };
            ZodiacHouseName[] zh3 = new ZodiacHouseName[4] { zh.Add(3).Value, zh.Add(6).Value, zh.Add(9).Value, zh.Add(12).Value };
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


        public BodyName[] GetOrderedGrahas() => GetOrderedGrahas(new BodyName[]
            {
                BodyName.Sun, BodyName.Moon, BodyName.Mars, BodyName.Mercury,
                BodyName.Jupiter, BodyName.Venus, BodyName.Saturn,
                BodyName.Rahu, BodyName.Ketu
            });
        public BodyName[] GetOrderedGrahas(BodyName[] grahas)
        {
            if (grahas.Length <= 1)
                return grahas;

            for (int i = 0; i < grahas.Length - 1; i++)
            {
                for (int j = 0; j < grahas.Length - 1; j++)
                {
                    if (false == this.CompareGraha(grahas[j], grahas[j + 1], false))
                        (grahas[j + 1], grahas[j]) = (grahas[j], grahas[j + 1]);
                }
            }
            return grahas;
        }

        public ZodiacHouseName[] GetOrderedRasis() => GetOrderedRasis(new ZodiacHouseName[]
            {
                ZodiacHouseName.Ari, ZodiacHouseName.Tau, ZodiacHouseName.Gem,
                ZodiacHouseName.Can, ZodiacHouseName.Leo, ZodiacHouseName.Vir,
                ZodiacHouseName.Lib, ZodiacHouseName.Sco, ZodiacHouseName.Sag,
                ZodiacHouseName.Cap, ZodiacHouseName.Aqu, ZodiacHouseName.Pis
            });
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
                    if (false == this.CompareRasi(rasis[j], rasis[j + 1], false))
                        (rasis[j + 1], rasis[j]) = (rasis[j], rasis[j + 1]);
                }
            }
            return rasis;
        }

        public ZodiacHouseName StrongerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord, ref int winner) => this.CompareRasi(za, zb, bSimpleLord, ref winner) ? za : zb;

        public ZodiacHouseName StrongerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            int winner = 0;
            return StrongerRasi(za, zb, bSimpleLord, ref winner);
        }
        public BodyName StrongerGraha(BodyName m, BodyName n, bool bSimpleLord)
        {
            int winner = 0;
            return StrongerGraha(m, n, bSimpleLord, ref winner);
        }
        public BodyName StrongerGraha(BodyName m, BodyName n, bool bSimpleLord, ref int winner)
        {
            if (CompareGraha(m, n, bSimpleLord, ref winner)) return m;
            return n;
        }
        public ZodiacHouseName WeakerRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            if (CompareRasi(za, zb, bSimpleLord)) return zb;
            return za;
        }
        public BodyName WeakerGraha(BodyName m, BodyName n, bool bSimpleLord)
        {
            if (CompareGraha(m, n, bSimpleLord)) return n;
            return m;
        }
        public bool CompareRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord)
        {
            int winner = 0;
            return CompareRasi(za, zb, bSimpleLord, ref winner);
        }
        public bool CompareRasi(ZodiacHouseName za, ZodiacHouseName zb, bool bSimpleLord, ref int winner)
        {
            bool bRet = false;
            bool bFound = true;
            winner = 0;

            Logger.Info(String.Format("Rasi: {0} {1}", za.ToString(), zb.ToString()));
            foreach (RasiStrength s in rules)
            {
                Logger.Info(String.Format("Rasi::{0}", s));
                switch (s)
                {
                    case RasiStrength.Conjunction:
                        try
                        {
                            bRet = new StrengthByConjunction(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.Exaltation:
                        try
                        {
                            bRet = new StrengthByExaltation(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.Longitude:
                        try
                        {
                            bRet = new StrengthByLongitude(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.AtmaKaraka:
                        try
                        {
                            bRet = new StrengthByAtmaKaraka(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.LordIsAtmaKaraka:
                        try
                        {
                            bRet = new StrengthByLordIsAtmaKaraka(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.RasisNature:
                        try
                        {
                            bRet = new StrengthByRasisNature(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.LordsNature:
                        try
                        {
                            bRet = new StrengthByLordsNature(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.AspectsRasi:
                        try
                        {
                            bRet = new StrengthByAspectsRasi(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.AspectsGraha:
                        try
                        {
                            bRet = new StrengthByAspectsGraha(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.LordInDifferentOddity:
                        try
                        {
                            bRet = new StrengthByLordInDifferentOddity(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.LordsLongitude:
                        try
                        {
                            bRet = new StrengthByLordsLongitude(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.NarayanaDasaLength:
                        try
                        {
                            bRet = new StrengthByNarayanaDasaLength(h, dtype, bSimpleLord).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.VimsottariDasaLength:
                        try
                        {
                            bRet = new StrengthByVimsottariDasaLength(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.MoolaTrikona:
                        try
                        {
                            bRet = new StrengthByMoolaTrikona(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.OwnHouse:
                        try
                        {
                            bRet = new StrengthByOwnHouse(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.KendraConjunction:
                        try
                        {
                            bRet = new StrengthByKendraConjunction(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.KarakaKendradiGrahaDasaLength:
                        try
                        {
                            bRet = new StrengthByKarakaKendradiGrahaDasaLength(h, dtype).Stronger(za, zb);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case RasiStrength.First:
                        try
                        {
                            bRet = new StrengthByFirst(h, dtype).Stronger(za, zb);
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
        public bool CompareGraha(BodyName m, BodyName n, bool bSimpleLord)
        {
            int winner = 0;
            return CompareGraha(m, n, bSimpleLord, ref winner);
        }
        public bool CompareGraha(BodyName m, BodyName n, bool bSimpleLord, ref int winner)
        {
            bool bRet = false;
            bool bFound = true;
            winner = 0;
            foreach (GrahaStrength s in rules)
            {
                Logger.Info(String.Format("Trying {0}. Curr is {1}", s, winner));
                switch (s)
                {
                    case GrahaStrength.Conjunction:
                        try
                        {
                            bRet = new StrengthByConjunction(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.Exaltation:
                        try
                        {
                            bRet = new StrengthByExaltation(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.Longitude:
                        try
                        {
                            bRet = new StrengthByLongitude(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.AtmaKaraka:
                        try
                        {
                            bRet = new StrengthByAtmaKaraka(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.RasisNature:
                        try
                        {
                            bRet = new StrengthByRasisNature(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.LordsNature:
                        try
                        {
                            bRet = new StrengthByLordsNature(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.AspectsRasi:
                        try
                        {
                            bRet = new StrengthByAspectsRasi(h, dtype, bSimpleLord).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.AspectsGraha:
                        try
                        {
                            bRet = new StrengthByAspectsGraha(h, dtype, bSimpleLord).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.NarayanaDasaLength:
                        try
                        {
                            bRet = new StrengthByNarayanaDasaLength(h, dtype, bSimpleLord).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.VimsottariDasaLength:
                        try
                        {
                            bRet = new StrengthByVimsottariDasaLength(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.MoolaTrikona:
                        try
                        {
                            bRet = new StrengthByMoolaTrikona(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.OwnHouse:
                        try
                        {
                            bRet = new StrengthByOwnHouse(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.NotInOwnHouse:
                        try
                        {
                            bRet = !(new StrengthByOwnHouse(h, dtype).Stronger(m, n));
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.LordInOwnHouse:
                        try
                        {
                            bRet = new StrengthByLordInOwnHouse(h, dtype, bSimpleLord).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.LordInDifferentOddity:
                        try
                        {
                            bRet = new StrengthByLordInDifferentOddity(h, dtype, bSimpleLord).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.KendraConjunction:
                        try
                        {
                            bRet = new StrengthByKendraConjunction(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.KarakaKendradiGrahaDasaLength:
                        try
                        {
                            bRet = new StrengthByKarakaKendradiGrahaDasaLength(h, dtype).Stronger(m, n);
                            goto found;
                        }
                        catch { winner++; }
                        break;
                    case GrahaStrength.First:
                        try
                        {
                            bRet = new StrengthByFirst(h, dtype).Stronger(m, n);
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
