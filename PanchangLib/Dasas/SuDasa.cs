

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class SuDasa: Dasa, IDasa
	{
		private Horoscope h;
		private RasiDasaUserOptions options;
		new public void DivisionChanged (Division div)
		{
			RasiDasaUserOptions newOpts = (RasiDasaUserOptions)options.Clone();
			newOpts.Division = (Division)div.Clone();
			this.SetOptions(newOpts);
		}
		public SuDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNarayanaDasaRasi(h));
		}
		public double paramAyus () 
		{
			return 144;
		}
		public void recalculateOptions()
		{
			options.recalculate();
		}
		Body.Name GetLord (ZodiacHouse zh)
		{
			switch (zh.value)
			{
				case ZodiacHouseName.Aqu:
					return options.ColordAqu;
				case ZodiacHouseName.Sco:
					return options.ColordSco;
				default:
					return Basics.SimpleLordOfZodiacHouse(zh.value);
			}
		}
		int[] order = new int[] { 0,1,4,7,10,2,5,8,11,3,6,9,12 };
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList();
			BodyPosition bp_sl = h.getPosition(Body.Name.SreeLagna);
			ZodiacHouse zh_seed = bp_sl.ToDivisionPosition(options.Division).zodiac_house;
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.value, zh_seed.add(7).value);

			bool bIsForward = zh_seed.isOdd();

			double dasa_length_sum = 0.0;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsForward)
					zh_dasa = zh_seed.add(order[i]);
				else
					zh_dasa = zh_seed.addReverse(order[i]);

				Body.Name bl = this.GetLord(zh_dasa);
				DivisionPosition dp = h.getPosition(bl).ToDivisionPosition(options.Division);
				double dasa_length = NarayanaDasa.NarayanaDasaLength(zh_dasa, dp);
				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_length_sum, dasa_length, 1, zh_dasa.value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}

			for (int i=0; i<12; i++)
			{
				DasaEntry di_first = ((DasaEntry)al[i]);
				double dasa_length = 12.0 - di_first.dasaLength;
				DasaEntry di = new DasaEntry (di_first.zodiacHouse, dasa_length_sum, dasa_length, 1, di_first.zodiacHouse.ToString());
				al.Add(di);
				dasa_length_sum += dasa_length;
			}

			double cycle_length = (double)cycle * this.paramAyus();
			double offset_length = (bp_sl.Longitude.toZodiacHouseOffset() / 30.0) *
				((DasaEntry)al[0]).dasaLength;

			//Console.WriteLine ("Completed {0}, going back {1} of {2} years", bp_sl.longitude.toZodiacHouseOffset() / 30.0,
			//	offset_length, ((DasaEntry)al[0]).dasaLength);

			cycle_length -= offset_length;

			foreach (DasaEntry di in al)
			{
				di.startUT += cycle_length;
			}

			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			ArrayList al = new ArrayList();
			ZodiacHouse zh_seed = new ZodiacHouse(pdi.zodiacHouse);

			double dasa_length = pdi.dasaLength / 12.0;
			double dasa_length_sum = pdi.startUT;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa = null;
				zh_dasa = zh_seed.addReverse(order[i]);

				DasaEntry di = new DasaEntry(zh_dasa.value, dasa_length_sum, dasa_length, pdi.level+1, 
					pdi.shortDesc + " " + zh_dasa.value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}
			return al;
		}
		public String Description ()
		{
			return "Sudasa";
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
	}
}
