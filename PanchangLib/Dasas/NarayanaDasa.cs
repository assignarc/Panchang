

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class NarayanaDasa: Dasa, IDasa
	{
		private Horoscope h;
		public bool bSama;
		public RasiDasaUserOptions options;
        public new Object Options => this.options.Clone();
        public NarayanaDasa (Horoscope _h)
		{
			h = _h;
			this.bSama = false;
			options = new RasiDasaUserOptions(h, Strongest.RulesNarayanaDasaRasi(h));
		}
        public void RecalculateOptions() => options.Recalculate();
        public double ParamAyus() => 144;
        BodyName GetLord (ZodiacHouse zh)
		{
			switch (zh.Value)
			{
				case ZodiacHouseName.Aqu:
					return options.ColordAqu;
				case ZodiacHouseName.Sco:
					return options.ColordSco;
				default:
					return Basics.SimpleLordOfZodiacHouse(zh.Value);
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
			zh_seed.Value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.Value, zh_seed.Add(7).Value);

			if (zh_seed.Add(9).IsOddFooted() == true)
				backward = false;

			if (options.saturnExceptionApplies(zh_seed.Value))
			{
				order = order_moveable;
				backward = false;
			}
			else if (options.ketuExceptionApplies(zh_seed.Value))
			{
				backward = !backward;
			}

			double dasa_length_sum = 0.0;
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh_dasa;
				if (backward)
					zh_dasa = zh_seed.AddReverse(order[i]);
				else
					zh_dasa = zh_seed.Add (order[i]);

				BodyName dasa_lord = this.GetLord(zh_dasa);
				//gs.strongerForNarayanaDasa(zh_dasa);
				DivisionPosition dlord_dpos = h.CalculateDivisionPosition(h.GetPosition(dasa_lord), options.Division);
				double dasa_length = (double)this.DasaLength(zh_dasa, dlord_dpos);

				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_length_sum, dasa_length, 1, zh_dasa.Value.ToString());
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
			zh_stronger.Value = options.findStrongerRasi(options.SeventhStrengths, zh_stronger.Value, zh_stronger.Add(7).Value);

			BodyName b = this.GetLord(zh_stronger);
			DivisionPosition dp = h.CalculateDivisionPosition(h.GetPosition(b), options.Division);
			ZodiacHouse first = dp.ZodiacHouse;
			bool backward = false;
			if ((int)first.Value % 2 == 0)
				backward = true;

			double dasa_start = pdi.startUT;
			for (int i=1; i<=12; i++)
			{
				ZodiacHouse zh_dasa;
				if (!backward)
					zh_dasa = first.Add(i);
				else
					zh_dasa = first.AddReverse(i);
				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_start, pdi.dasaLength / 12.0, pdi.level+1, pdi.shortDesc + " " + zh_dasa.Value.ToString());
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
        
        public object SetOptions (Object a)
		{
			options.CopyFrom(a);
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
