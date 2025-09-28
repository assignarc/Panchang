

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class DrigDasa: Dasa, IDasa
	{
		private RasiDasaUserOptions options;
		private Horoscope h;
		public DrigDasa (Horoscope _h)
		{
			h = _h;
			options = new RasiDasaUserOptions(h, Strongest.RulesNarayanaDasaRasi(h));
		}
		public double ParamAyus () 
		{
			return 144;
		}
		public void RecalculateOptions ()
		{
			options.Recalculate();
		}
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

		public void DasaHelper (ZodiacHouse zh, ArrayList al)
		{
			int[] order_moveable = new int[] {5,8,11};
			int[] order_fixed = new int[] {3,6,9};
			int[] order_dual = new int[] {4,7,10};
			bool backward = false;
			if (!zh.IsOddFooted())
				backward = true;

			int[] order;
			switch ((int)zh.Value % 3)
			{
				case 1: order = order_moveable; break;
				case 2: order = order_fixed; break;
				default: order = order_dual; break;
			}
			al.Add (zh.Add (1));
			if (! backward) 
			{
				for (int i=0; i<3; i++)
					al.Add (zh.Add(order[i]));
			} 
			else 
			{
				for (int i=2; i>=0; i--)
					al.Add (zh.Add(order[i]));
			}
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al_order = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed().Add(9);

			for (int i=1; i<=4; i++) 
			{
				this.DasaHelper(zh_seed.Add(i), al_order);
			}

			ArrayList al = new ArrayList (12);

			double dasa_length_sum = 0.0;
			double dasa_length;
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh_dasa = (ZodiacHouse)al_order[i];
				DivisionPosition dp = h.CalculateDivisionPosition(h.GetPosition(this.GetLord(zh_dasa)), new Division(DivisionType.Rasi));
				dasa_length = NarayanaDasa.NarayanaDasaLength(zh_dasa, dp);
				DasaEntry di = new DasaEntry (zh_dasa.Value, dasa_length_sum, dasa_length, 1, zh_dasa.Value.ToString());
				al.Add (di);
				dasa_length_sum += dasa_length;

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
			return "Drig Dasa"
				+ " seeded from " + options.SeedRasi.ToString();
		}
        public new Object Options => this.options.Clone();
        public object SetOptions (Object a)
		{
			options.CopyFrom (a);
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
