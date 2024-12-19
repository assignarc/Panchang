

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
			BodyName ak = FindAtmaKaraka();
			foreach (BodyName ba in ala) 
			{
				if (ba == ak) return true;
			}
			foreach (BodyName bb in alb)
			{
				if (bb == ak) return false;
			}
			throw new EqualStrength();
		}
        public bool Stronger(BodyName m, BodyName n)
		{
			BodyName ak = FindAtmaKaraka();
			if (m == ak) return true;
			if (n == ak) return false;
			throw new EqualStrength();
		}
	}



}
	
