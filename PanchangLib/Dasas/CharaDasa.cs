

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class CharaDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		public CharaDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNavamsaDasaRasi(h));
		}
		public double paramAyus () 
		{
			return 144;
		}
		public void recalculateOptions ()
		{
			options.recalculate();
		}
		public DivisionPosition getLordsPosition (ZodiacHouse zh)
		{
			Body.Name b;
			if (zh.value == ZodiacHouseName.Sco) b = options.ColordSco;
			else if (zh.value == ZodiacHouseName.Aqu) b = options.ColordAqu;
			else b = Basics.SimpleLordOfZodiacHouse(zh.value);

			return h.getPosition(b).toDivisionPosition(options.Division);
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.value, zh_seed.add(7).value);

			bool bIsZodiacal = zh_seed.add(9).isOddFooted();

			double dasa_length_sum = 0.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsZodiacal)
					zh_dasa = zh_seed.add (i);
				else
					zh_dasa = zh_seed.addReverse (i);
				double dasa_length = NarayanaDasa.NarayanaDasaLength(zh_dasa, this.getLordsPosition(zh_dasa));


				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_length_sum, dasa_length, 1, zh_dasa.value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}

			for (int i=0; i<12; i++)
			{
				DasaEntry df = (DasaEntry)al[i];
				double dasa_length = 12.0 - df.dasaLength;
				DasaEntry di = new DasaEntry (df.zodiacHouse, dasa_length_sum, dasa_length, 1, df.shortDesc);
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
			return "Chara Dasa seeded from " + options.SeedRasi.ToString();
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
