

using System.Diagnostics;

namespace org.transliteral.panchang
{
    public class KutaVarna
	{
		public enum EType
		{
			IBrahmana, 
			IKshatriya, 
			IVaishya, 
			ISudra, 
			IAnuloma, 
			IPratiloma
		};
        public static int GetMaxScore()
		{
			return 2;
		}
		public static int GetScore (Nakshatra m, Nakshatra f)
		{
			EType em = GetType(m);
			EType ef = GetType(f);
			if (em == ef) return 2;
			if (em == EType.IBrahmana && 
				(ef == EType.IKshatriya || ef == EType.IVaishya || ef == EType.ISudra)) 
				return 1;
			if (em == EType.IKshatriya &&
				(ef == EType.IVaishya || ef == EType.ISudra))
				return 1;
			if (em == EType.IVaishya && ef == EType.ISudra) return 1;
			if (em == EType.IAnuloma && ef != EType.IPratiloma) return 1;
			if (ef == EType.IAnuloma && em != EType.IAnuloma) return 1;
			return 0;
		}
		public static EType GetType (Nakshatra n)
		{
			switch (((int)n.Value)%6)
			{
				case 1: return EType.IBrahmana;
				case 2: return EType.IKshatriya;
				case 3: return EType.IVaishya;
				case 4: return EType.ISudra;
				case 5: return EType.IAnuloma;
				case 0: return EType.IPratiloma;
				case 6: return EType.IPratiloma;
			}
			Debug.Assert(false, "KutaVarna::getType");
			return EType.IAnuloma;
		}
	}


}
