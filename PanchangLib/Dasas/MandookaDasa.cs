
using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class MandookaDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		public MandookaDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNavamsaDasaRasi(h));
		}
		public double paramAyus () 
		{
			return 96;
		}
		public void recalculateOptions ()
		{
			options.recalculate();
		}
		public int DasaLength (ZodiacHouse zh)
		{
			switch ((int)zh.value % 3)
			{
				case 1: return 7;
				case 2: return 8;
				default: return 9;
			}
		}
		public ArrayList Dasa(int cycle)
		{
			int[] sequence = new int[] { 1,3,5,7,9,11,2,4,6,8,10,12 };
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();

			if (zh_seed.isOdd())
				zh_seed = zh_seed.add(3);
			else
				zh_seed = zh_seed.addReverse(3);

			bool bDirZodiacal = true;
			if (! zh_seed.isOdd())
			{
				//zh_seed = zh_seed.AdarsaSign();
				bDirZodiacal = false;
			}

			double dasa_length_sum = 0.0;
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bDirZodiacal)
					zh_dasa = zh_seed.add(sequence[i]);
				else
					zh_dasa = zh_seed.addReverse(sequence[i]);
				double dasa_length = this.DasaLength(zh_dasa);
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
			if (! zh_stronger.isOdd())
				zh_stronger = zh_stronger.AdarsaSign();

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
			return "Mandooka Dasa (seeded from) " + Basics.NumPartsInDivisionString (options.Division);
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
