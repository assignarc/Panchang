using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    /// <summary>
	/// A package related to a ZodiacHouse
	/// </summary>
	public class ZodiacHouse : ICloneable
    {
        private ZodiacHouseName m_zhouse;
        public ZodiacHouseName Value { get { return m_zhouse; } set { m_zhouse = value; } }
        public ZodiacHouse(ZodiacHouseName zhouse) { m_zhouse = zhouse; }
        
        public static ZodiacHouseName[] AllNames = new ZodiacHouseName[]
        {
            ZodiacHouseName.Ari, ZodiacHouseName.Tau, ZodiacHouseName.Gem, ZodiacHouseName.Can, ZodiacHouseName.Leo, ZodiacHouseName.Vir,
            ZodiacHouseName.Lib, ZodiacHouseName.Sco, ZodiacHouseName.Sag, ZodiacHouseName.Cap, ZodiacHouseName.Aqu, ZodiacHouseName.Pis
        };
        public object Clone()
        {
            return new ZodiacHouse(this.m_zhouse);
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public int Normalize()
        {
            return Basics.Normalize_inc(1, 12, (int)m_zhouse);
        }
        public ZodiacHouse Add(int i) 
        {
            int znum = Basics.Normalize_inc(1, 12, (int)(m_zhouse) + i - 1);
            return new ZodiacHouse((ZodiacHouseName)znum);
        }
        public ZodiacHouse AddReverse(int i)
        {
            int znum = Basics.Normalize_inc(1, 12, (int)(m_zhouse) - i + 1);
            return new ZodiacHouse((ZodiacHouseName)znum);
        }
        public int NumHousesBetweenReverse(ZodiacHouse zrel)
        {
            return Basics.Normalize_inc(1, 12, (14 - this.NumHousesBetween(zrel)));
        }
        public int NumHousesBetween(ZodiacHouse zrel)
        {
            int ret = Basics.Normalize_inc(1, 12, (int)zrel.Value - (int)m_zhouse + 1);
            Trace.Assert(ret >= 1 && ret <= 12, "ZodiacHouse.numHousesBetween failed");
            return ret;
        }
        public bool IsDaySign()
        {
            switch (this.Value)
            {
                case ZodiacHouseName.Ari:
                case ZodiacHouseName.Tau:
                case ZodiacHouseName.Gem:
                case ZodiacHouseName.Can:
                    return false;

                case ZodiacHouseName.Leo:
                case ZodiacHouseName.Vir:
                case ZodiacHouseName.Lib:
                case ZodiacHouseName.Sco:
                    return true;

                case ZodiacHouseName.Sag:
                case ZodiacHouseName.Cap:
                    return false;

                case ZodiacHouseName.Aqu:
                case ZodiacHouseName.Pis:
                    return true;

                default:
                    Trace.Assert(false, "isDaySign internal error");
                    return true;
            }
        }
        public bool IsOdd()
        {
            switch (this.Value)
            {
                case ZodiacHouseName.Ari:
                case ZodiacHouseName.Gem:
                case ZodiacHouseName.Leo:
                case ZodiacHouseName.Lib:
                case ZodiacHouseName.Sag:
                case ZodiacHouseName.Aqu:
                    return true;

                case ZodiacHouseName.Tau:
                case ZodiacHouseName.Can:
                case ZodiacHouseName.Vir:
                case ZodiacHouseName.Sco:
                case ZodiacHouseName.Cap:
                case ZodiacHouseName.Pis:
                    return false;

                default:
                    Trace.Assert(false, "isOdd internal error");
                    return true;
            }
        }
        public bool IsOddFooted()
        {
            switch (this.Value)
            {
                case ZodiacHouseName.Ari: return true;
                case ZodiacHouseName.Tau: return true;
                case ZodiacHouseName.Gem: return true;
                case ZodiacHouseName.Can: return false;
                case ZodiacHouseName.Leo: return false;
                case ZodiacHouseName.Vir: return false;
                case ZodiacHouseName.Lib: return true;
                case ZodiacHouseName.Sco: return true;
                case ZodiacHouseName.Sag: return true;
                case ZodiacHouseName.Cap: return false;
                case ZodiacHouseName.Aqu: return false;
                case ZodiacHouseName.Pis: return false;
            }
            Trace.Assert(false, "ZOdiacHouse::isOddFooted");
            return false;
        }
        public bool RasiDristi(ZodiacHouse b)
        {
            int ma = (int)Value % 3;
            int mb = (int)b.Value % 3;

            switch (ma)
            {
                case 1:
                    if (mb == 2 && this.Add(2).Value != b.Value) return true;
                    return false;
                case 2:
                    if (mb == 1 && this.AddReverse(2).Value != b.Value) return true;
                    return false;
                case 0:
                    if (mb == 0) return true;
                    return false;
            }

            Trace.Assert(false, "ZodiacHouse.RasiDristi");
            return false;
        }
        public RiseType RisesWith()
        {
            switch (this.Value)
            {
                case ZodiacHouseName.Ari:
                case ZodiacHouseName.Tau:
                case ZodiacHouseName.Can:
                case ZodiacHouseName.Sag:
                case ZodiacHouseName.Cap:
                    return RiseType.RisesWithFoot;
                case ZodiacHouseName.Gem:
                case ZodiacHouseName.Leo:
                case ZodiacHouseName.Vir:
                case ZodiacHouseName.Lib:
                case ZodiacHouseName.Sco:
                case ZodiacHouseName.Aqu:
                    return RiseType.RisesWithHead;
                default:
                    return RiseType.RisesWithBoth;
            }
        }
        public ZodiacHouse LordsOtherSign()
        {
            ZodiacHouseName ret = ZodiacHouseName.Ari;
            switch (this.Value)
            {
                case ZodiacHouseName.Ari: ret = ZodiacHouseName.Sco; break;
                case ZodiacHouseName.Tau: ret = ZodiacHouseName.Lib; break;
                case ZodiacHouseName.Gem: ret = ZodiacHouseName.Vir; break;
                case ZodiacHouseName.Can: ret = ZodiacHouseName.Can; break;
                case ZodiacHouseName.Leo: ret = ZodiacHouseName.Leo; break;
                case ZodiacHouseName.Vir: ret = ZodiacHouseName.Gem; break;
                case ZodiacHouseName.Lib: ret = ZodiacHouseName.Tau; break;
                case ZodiacHouseName.Sco: ret = ZodiacHouseName.Ari; break;
                case ZodiacHouseName.Sag: ret = ZodiacHouseName.Pis; break;
                case ZodiacHouseName.Cap: ret = ZodiacHouseName.Aqu; break;
                case ZodiacHouseName.Aqu: ret = ZodiacHouseName.Cap; break;
                case ZodiacHouseName.Pis: ret = ZodiacHouseName.Sag; break;
                default: Debug.Assert(false, "ZodiacHouse::KalachakraMirrorSign"); break;
            }
            return new ZodiacHouse(ret);
        }
        public ZodiacHouse AdarsaSign()
        {
            ZodiacHouseName ret = ZodiacHouseName.Ari;
            switch (this.Value)
            {
                case ZodiacHouseName.Ari: ret = ZodiacHouseName.Sco; break;
                case ZodiacHouseName.Tau: ret = ZodiacHouseName.Lib; break;
                case ZodiacHouseName.Gem: ret = ZodiacHouseName.Vir; break;
                case ZodiacHouseName.Can: ret = ZodiacHouseName.Aqu; break;
                case ZodiacHouseName.Leo: ret = ZodiacHouseName.Cap; break;
                case ZodiacHouseName.Vir: ret = ZodiacHouseName.Gem; break;
                case ZodiacHouseName.Lib: ret = ZodiacHouseName.Tau; break;
                case ZodiacHouseName.Sco: ret = ZodiacHouseName.Ari; break;
                case ZodiacHouseName.Sag: ret = ZodiacHouseName.Pis; break;
                case ZodiacHouseName.Cap: ret = ZodiacHouseName.Leo; break;
                case ZodiacHouseName.Aqu: ret = ZodiacHouseName.Can; break;
                case ZodiacHouseName.Pis: ret = ZodiacHouseName.Sag; break;
                default: Debug.Assert(false, "ZodiacHouse::AdarsaSign"); break;
            }
            return new ZodiacHouse(ret);
        }
        public ZodiacHouse AbhimukhaSign()
        {
            ZodiacHouseName ret = ZodiacHouseName.Ari;
            switch (this.Value)
            {
                case ZodiacHouseName.Ari: ret = ZodiacHouseName.Sco; break;
                case ZodiacHouseName.Tau: ret = ZodiacHouseName.Lib; break;
                case ZodiacHouseName.Gem: ret = ZodiacHouseName.Sag; break;
                case ZodiacHouseName.Can: ret = ZodiacHouseName.Aqu; break;
                case ZodiacHouseName.Leo: ret = ZodiacHouseName.Cap; break;
                case ZodiacHouseName.Vir: ret = ZodiacHouseName.Pis; break;
                case ZodiacHouseName.Lib: ret = ZodiacHouseName.Tau; break;
                case ZodiacHouseName.Sco: ret = ZodiacHouseName.Ari; break;
                case ZodiacHouseName.Sag: ret = ZodiacHouseName.Gem; break;
                case ZodiacHouseName.Cap: ret = ZodiacHouseName.Leo; break;
                case ZodiacHouseName.Aqu: ret = ZodiacHouseName.Can; break;
                case ZodiacHouseName.Pis: ret = ZodiacHouseName.Vir; break;
                default: Debug.Assert(false, "ZodiacHouse::AbhimukhaSign"); break;
            }
            return new ZodiacHouse(ret);
        }

        public static string ToShortString(ZodiacHouseName z)
        {
            switch (z)
            {
                case ZodiacHouseName.Ari: return "Ar";
                case ZodiacHouseName.Tau: return "Ta";
                case ZodiacHouseName.Gem: return "Ge";
                case ZodiacHouseName.Can: return "Cn";
                case ZodiacHouseName.Leo: return "Le";
                case ZodiacHouseName.Vir: return "Vi";
                case ZodiacHouseName.Lib: return "Li";
                case ZodiacHouseName.Sco: return "Sc";
                case ZodiacHouseName.Sag: return "Sg";
                case ZodiacHouseName.Cap: return "Cp";
                case ZodiacHouseName.Aqu: return "Aq";
                case ZodiacHouseName.Pis: return "Pi";
                default: return "";
            }
        }
    }

}
