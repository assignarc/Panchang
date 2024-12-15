

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class LagnaKendradiRasiDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		private Division m_dtype = new Division(DivisionType.Rasi);

		public LagnaKendradiRasiDasa (Horoscope _h)
		{
			FindStronger fs_rasi = new FindStronger (h, m_dtype, FindStronger.RulesNarayanaDasaRasi(h));
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNarayanaDasaRasi(h));
		}
        public void RecalculateOptions() => options.Recalculate();
        private bool IsZodiacal()
		{
			ZodiacHouse zh_start = options.getSeed();
			zh_start.Value = options.findStrongerRasi(options.SeventhStrengths, zh_start.Value, zh_start.Add(7).Value);

			bool forward = zh_start.IsOdd();
			if (options.saturnExceptionApplies(zh_start.Value))
				return forward;
			if (options.ketuExceptionApplies(zh_start.Value))
				forward = !forward;
			return forward;
		}

        public double ParamAyus() => 144;

        public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (24);
			int[] order = {1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12};
			double dasa_length_sum = 0;

			ZodiacHouse zh_start = options.getSeed();
			zh_start.Value = options.findStrongerRasi(options.SeventhStrengths, zh_start.Value, zh_start.Add(7).Value);

			bool bIsZodiacal = this.IsZodiacal();
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh = zh_start.Add(1);
				if (bIsZodiacal) zh = zh.Add(order[i]);
				else zh = zh.AddReverse(order[i]);
				Body.Name lord = h.LordOfZodiacHouse(zh, m_dtype);
				DivisionPosition dp_lord = h.GetPosition(lord).ToDivisionPosition(m_dtype);
				double dasa_length = NarayanaDasa.NarayanaDasaLength(zh, dp_lord);
				DasaEntry de = new DasaEntry(zh.Value, dasa_length_sum, dasa_length, 1, zh.Value.ToString());
				al.Add (de);
				dasa_length_sum += dasa_length;
			}
			for (int i=0; i<12; i++)
			{
				DasaEntry de_first = (DasaEntry)al[i];
				double dasa_length = 12.0 - de_first.dasaLength;
				DasaEntry de = new DasaEntry(de_first.zodiacHouse, dasa_length_sum, dasa_length, 1, de_first.shortDesc);
				dasa_length_sum += dasa_length;
				al.Add(de);
			}
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			NarayanaDasa nd = new NarayanaDasa(h);
			nd.options = this.options;
			return nd.AntarDasa(pdi);
		}
		public string Description ()
		{
			return "Lagna Kendradi Rasi Dasa seeded from"
				+ " seeded from " + options.SeedRasi.ToString();
		}
        public object Options => this.options.Clone();
        public object SetOptions(object a)
		{
			options.CopyFrom(a);
			RecalculateEvent();
			return options.Clone();
		}
		new public void DivisionChanged (Division div)
		{
			RasiDasaUserOptions newOpts = (RasiDasaUserOptions)options.Clone();
			newOpts.Division = (Division)div.Clone();
			this.SetOptions(newOpts);
		}
	}
}
