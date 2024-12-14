

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NavamsaDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		public NavamsaDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNavamsaDasaRasi(h));
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
			ZodiacHouse zh_seed = h.getPosition(Body.Name.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house;

			if (! zh_seed.isOdd())
				zh_seed = zh_seed.AdarsaSign();

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
			return "Navamsa Dasa";
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
