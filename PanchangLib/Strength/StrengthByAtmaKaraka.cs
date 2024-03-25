

using System.Collections;
namespace org.transliteral.panchang
{
    // Stronger rasi contains AK
    // Stronger graha is AK
    public class StrengthByAtmaKaraka : BaseStrength, IStrengthRasi, IStrengthGraha
	{
		public StrengthByAtmaKaraka (Horoscope h, Division dtype)
			: base (h, dtype, true) {}

		public bool stronger (ZodiacHouseName za, ZodiacHouseName zb) 
		{
			ArrayList ala = findGrahasInHouse (za);
			ArrayList alb = findGrahasInHouse (zb);
			Body.Name ak = findAtmaKaraka();
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
		public bool stronger (Body.Name m, Body.Name n)
		{
			Body.Name ak = findAtmaKaraka();
			if (m == ak) return true;
			if (n == ak) return false;
			throw new EqualStrength();
		}
	}



}
	
