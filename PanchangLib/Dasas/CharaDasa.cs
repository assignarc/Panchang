

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
        public double ParamAyus() => 144;
        public void RecalculateOptions() => options.Recalculate();
        public DivisionPosition GetLordsPosition(ZodiacHouse zh)
		{
			BodyName b;
			if (zh.Value == ZodiacHouseName.Sco) b = options.ColordSco;
			else if (zh.Value == ZodiacHouseName.Aqu) b = options.ColordAqu;
			else b = Basics.SimpleLordOfZodiacHouse(zh.Value);

			return h.GetPosition(b).ToDivisionPosition(options.Division);
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();
			zh_seed.Value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.Value, zh_seed.Add(7).Value);

			bool bIsZodiacal = zh_seed.Add(9).IsOddFooted();

			double dasa_length_sum = 0.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsZodiacal)
					zh_dasa = zh_seed.Add (i);
				else
					zh_dasa = zh_seed.AddReverse (i);
				double dasa_length = NarayanaDasa.NarayanaDasaLength(zh_dasa, this.GetLordsPosition(zh_dasa));


				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_length_sum, dasa_length, 1, zh_dasa.Value.ToString());
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
        public String Description() => "Chara Dasa seeded from " + options.SeedRasi.ToString();
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
