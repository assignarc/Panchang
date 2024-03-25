

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NarayanaDasa: Dasa, IDasa
	{
		private Horoscope h;
		public bool bSama;
		public RasiDasaUserOptions options;
		public NarayanaDasa (Horoscope _h)
		{
			h = _h;
			this.bSama = false;
			options = new RasiDasaUserOptions(h, FindStronger.RulesNarayanaDasaRasi(h));
		}
		public void recalculateOptions()
		{
			options.recalculate();
		}
		public double paramAyus () 
		{
			return 144;
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

		public int DasaLength (ZodiacHouse zh, DivisionPosition dp) 
		{
			if (this.bSama)
				return 12;

			return NarayanaDasa.NarayanaDasaLength(zh, dp);
		}
		public ArrayList Dasa(int cycle)
		{
			int[] order_moveable = new int[] { 1,2,3,4,5,6,7,8,9,10,11,12 };
			int[] order_fixed = new int[] { 1,6,11,4,9,2,7,12,5,10,3,8 };
			int[] order_dual = new int[] { 1,5,9,10,2,6,7,11,3,4,8,12 };

			ArrayList al = new ArrayList (24);
			bool backward = true;

			int[] order;
			switch ((int)options.SeedRasi % 3)
			{
				case 1: order = order_moveable; break;
				case 2: order = order_fixed; break;
				default: order = order_dual; break;
			}
			ZodiacHouse zh_seed = options.getSeed();
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.value, zh_seed.add(7).value);

			if (zh_seed.add(9).isOddFooted() == true)
				backward = false;

			if (options.saturnExceptionApplies(zh_seed.value))
			{
				order = order_moveable;
				backward = false;
			}
			else if (options.ketuExceptionApplies(zh_seed.value))
			{
				backward = !backward;
			}

			double dasa_length_sum = 0.0;
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh_dasa;
				if (backward)
					zh_dasa = zh_seed.addReverse(order[i]);
				else
					zh_dasa = zh_seed.add (order[i]);

				Body.Name dasa_lord = this.GetLord(zh_dasa);
				//gs.strongerForNarayanaDasa(zh_dasa);
				DivisionPosition dlord_dpos = h.CalculateDivisionPosition(h.getPosition(dasa_lord), options.Division);
				double dasa_length = (double)this.DasaLength(zh_dasa, dlord_dpos);

				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_length_sum, dasa_length, 1, zh_dasa.value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;
			}

			if (bSama == false)
			{
				for (int i=0; i<12; i++)
				{
					DasaEntry di = (DasaEntry)al[i];
					DasaEntry dn = new DasaEntry(di.zodiacHouse, dasa_length_sum, 12.0-di.dasaLength, 1, di.zodiacHouse.ToString());
					dasa_length_sum += dn.dasaLength;
					al.Add (dn);
				}
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
			zh_stronger.value = options.findStrongerRasi(options.SeventhStrengths, zh_stronger.value, zh_stronger.add(7).value);

			Body.Name b = this.GetLord(zh_stronger);
			DivisionPosition dp = h.CalculateDivisionPosition(h.getPosition(b), options.Division);
			ZodiacHouse first = dp.zodiac_house;
			bool backward = false;
			if ((int)first.value % 2 == 0)
				backward = true;

			double dasa_start = pdi.startUT;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa;
				if (!backward)
					zh_dasa = first.add(i);
				else
					zh_dasa = first.addReverse(i);
				DasaEntry di = new DasaEntry (zh_dasa.value, dasa_start, pdi.dasaLength / 12.0, pdi.level+1, pdi.shortDesc + " " + zh_dasa.value.ToString());
				al.Add (di);
				dasa_start += pdi.dasaLength / 12.0;
			}
			return al;
		}
		public String Description ()
		{
			return "Narayana Dasa for "
				+ options.Division.ToString() 
				+ " seeded from " + options.SeedRasi.ToString();
		}
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
		public object SetOptions (Object a)
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
