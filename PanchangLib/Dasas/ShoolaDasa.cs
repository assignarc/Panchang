

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
		public double paramAyus () 
		{
			return 108;
		}
		public void recalculateOptions ()
		{
			options.recalculate();
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, 
					zh_seed.value, zh_seed.add(7).value);

			double dasa_length_sum = 0.0;
			double dasa_length = 9.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = zh_seed.add (i);
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
			ArrayList al = new ArrayList(12);

			ZodiacHouse zh_first = new ZodiacHouse(pdi.zodiacHouse);
			ZodiacHouse zh_stronger = zh_first.add(1);
			zh_stronger.value = options.findStrongerRasi(options.SeventhStrengths,
				zh_first.value, zh_first.add(7).value);

			double dasa_start = pdi.startUT;

			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = zh_stronger.add (i);
				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_start, pdi.dasaLength / 12.0, pdi.level+1, pdi.shortDesc + " " + zh_dasa.value.ToString());
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
