

using System.Collections;
namespace org.transliteral.panchang
{
    // Stronger rasi contains AK
    // Stronger graha is AK
    public class StrengthByAtmaKaraka : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAtmaKaraka (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

        public bool Stronger(ZodiacHouseName za, ZodiacHouseName zb)
        {
			ArrayList ala = FindGrahasInHouse (za);
			ArrayList alb = FindGrahasInHouse (zb);
			Body.Name ak = FindAtmaKaraka();
			foreach (Body.Name ba in ala) 
			{
				if (ba == ak) return true;
			}
			foreach (Body.Name bb in alb)
			{
				if (bb == ak) return false;
			}
			throw new EqualStrength();
		}
        public bool Stronger(Body.Name m, Body.Name n)
		{
			Body.Name ak = FindAtmaKaraka();
			if (m == ak) return true;
			if (n == ak) return false;
			throw new EqualStrength();
		}
	}



}
	
