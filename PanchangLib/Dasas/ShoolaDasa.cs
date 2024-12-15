

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class ShoolaDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		public ShoolaDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNarayanaDasaRasi(h));
		}
		public double ParamAyus () 
		{
			return 108;
		}
		public void RecalculateOptions ()
		{
			options.Recalculate();
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();
			zh_seed.Value = options.findStrongerRasi(options.SeventhStrengths, 
					zh_seed.Value, zh_seed.Add(7).Value);

			double dasa_length_sum = 0.0;
			double dasa_length = 9.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = zh_seed.Add (i);
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
			ArrayList al = new ArrayList(12);

			ZodiacHouse zh_first = new ZodiacHouse(pdi.zodiacHouse);
			ZodiacHouse zh_stronger = zh_first.Add(1);
			zh_stronger.Value = options.findStrongerRasi(options.SeventhStrengths,
				zh_first.Value, zh_first.Add(7).Value);

			double dasa_start = pdi.startUT;

			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = zh_stronger.Add (i);
				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_start, pdi.dasaLength / 12.0, pdi.level+1, pdi.shortDesc + " " + zh_dasa.Value.ToString());
				al.Add (di);
				dasa_start += pdi.dasaLength / 12.0;
			}

			return al;
		}
		public String Description ()
		{
			return "Shoola Dasa"
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
		new public void DivisionChanged (Division div)
		{
			RasiDasaUserOptions newOpts = (RasiDasaUserOptions)options.Clone();
			newOpts.Division = (Division)div.Clone();
			this.SetOptions(newOpts);
		}
	}
}
