
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
		public double paramAyus () 
		{
			return 96;
		}
		public void recalculateOptions()
		{
			options.recalculate();
		}
		public double getDasaLength (ZodiacHouse zh)
		{
			switch ((int)zh.value % 3)
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
			ZodiacHouse zh_seed = options.getSeed().add(2);
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, 
				zh_seed.value, zh_seed.add(7).value);

			bool bIsForward = zh_seed.isOdd();

			double dasa_length_sum = 0.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsForward)
					zh_dasa = zh_seed.add(i);
				else
					zh_dasa = zh_seed.addReverse(i);

				double dasa_length = this.getDasaLength(zh_dasa);
				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_length_sum, dasa_length, 1, zh_dasa.value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}

			double cycle_length = (double)cycle * this.paramAyus();
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
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
		public object SetOptions (Object a)
		{
			RasiDasaUserOptions uo = (RasiDasaUserOptions)a;
			options.CopyFrom (uo);
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
