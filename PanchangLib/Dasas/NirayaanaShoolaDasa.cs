
using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NirayaanaShoolaDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		public NirayaanaShoolaDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNarayanaDasaRasi(h));
		}
        public double ParamAyus() => 96;
        public void RecalculateOptions() => options.Recalculate();
        public double getDasaLength (ZodiacHouse zh)
		{
			switch ((int)zh.Value % 3)
			{
				case 1: return 7.0;
				case 2: return 8.0;
				case 0: return 9.0;
				default: throw new Exception();
			}
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList();
			ZodiacHouse zh_seed = options.getSeed().Add(2);
			zh_seed.Value = options.findStrongerRasi(options.SeventhStrengths, 
				zh_seed.Value, zh_seed.Add(7).Value);

			bool bIsForward = zh_seed.IsOdd();

			double dasa_length_sum = 0.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsForward)
					zh_dasa = zh_seed.Add(i);
				else
					zh_dasa = zh_seed.AddReverse(i);

				double dasa_length = this.getDasaLength(zh_dasa);
				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_length_sum, dasa_length, 1, zh_dasa.Value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}

			double cycle_length = (double)cycle * this.ParamAyus();
			foreach (DasaEntry di in al)
			{
				di.startUT += cycle_length;
			}

			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			NarayanaDasa nd = new NarayanaDasa(h);
			nd.options = this.options;
			return nd.AntarDasa(pdi);
		}
		public String Description ()
		{
			return "Niryaana Shoola Dasa"
				+ " seeded from " + options.SeedRasi.ToString();
		}
        public Object Options => this.options.Clone();
        public object SetOptions (Object a)
		{
			RasiDasaUserOptions uo = (RasiDasaUserOptions)a;
			options.CopyFrom (uo);
			RecalculateEvent();
			return options.Clone();
		}
		public new void DivisionChanged (Division div)
		{
			RasiDasaUserOptions newOpts = (RasiDasaUserOptions)options.Clone();
			newOpts.Division = (Division)div.Clone();
			this.SetOptions(newOpts);
		}
	}
}
