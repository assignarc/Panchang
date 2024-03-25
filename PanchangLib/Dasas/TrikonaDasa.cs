

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class TrikonaDasa: Dasa, IDasa
	{
		class UserOptions : RasiDasaUserOptions
		{
			protected OrderedZodiacHouses mTrikonaStrengths;
			public OrderedZodiacHouses TrikonaStrengths
			{
				get { return this.mTrikonaStrengths; }
				set { this.mTrikonaStrengths = value; }
			}
			public UserOptions (Horoscope _h, ArrayList _rules) :
				base (_h, _rules)
			{
				this.calculateTrikonaStrengths();
			}
			private void calculateTrikonaStrengths ()
			{
				ZodiacHouse zh = this.getSeed();
				ZodiacHouseName[] zh_t = new ZodiacHouseName[3] { zh.add(1).value, zh.add(5).value, zh.add(9).value };
				FindStronger fs = new FindStronger(h, this.Division, this.mRules);
				mTrikonaStrengths = fs.getOrderedHouses(zh_t);
			}
			public override object Clone()
			{
				UserOptions uo = new UserOptions(h, this.mRules);
				this.CopyFromNoClone(this);
				uo.mTrikonaStrengths = (OrderedZodiacHouses)this.mTrikonaStrengths.Clone();
				return uo;
			}
			override public object CopyFrom (object _uo)
			{
				UserOptions uo = (UserOptions)_uo;
				if (this.Division != uo.Division ||
					this.ColordAqu != uo.ColordAqu ||
					this.ColordSco != uo.ColordSco) 
				{
					this.calculateTrikonaStrengths();
					this.calculateSeed();
					this.calculateExceptions();
					this.calculateSeventhStrengths();
					this.calculateCoLords();
				}
				base.CopyFromNoClone(_uo);
				return this.Clone();
			}
			new public void recalculate ()
			{
				this.calculateTrikonaStrengths();
				this.calculateSeed();
				this.calculateExceptions();
				this.calculateSeventhStrengths();
				this.calculateCoLords();
			}
		}
		private Horoscope h;
		private UserOptions options;
		public TrikonaDasa (Horoscope _h)
		{
			h = _h;
			options = new UserOptions(h, FindStronger.RulesNavamsaDasaRasi(h));
		}
		public void recalculateOptions ()
		{
			options.recalculate();
		}
		public double paramAyus () 
		{
			return 144;
		}

		public DivisionPosition getLordsPosition (ZodiacHouse zh)
		{
			Body.Name b;
			if (zh.value == ZodiacHouseName.Sco) b = options.ColordSco;
			else if (zh.value == ZodiacHouseName.Aqu) b = options.ColordAqu;
			else b = Basics.SimpleLordOfZodiacHouse(zh.value);

			return h.getPosition(b).toDivisionPosition(options.Division);
		}
		int[] order = {1,5,9,2,6,10,3,7,11,4,8,12};
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (12);
			ZodiacHouse zh_seed = options.getSeed();
			if (options.TrikonaStrengths.houses.Count >= 1)
				zh_seed.value = (ZodiacHouseName)options.TrikonaStrengths.houses[0];
			zh_seed.value = options.findStrongerRasi(options.SeventhStrengths, zh_seed.value, zh_seed.add(7).value);

			bool bIsZodiacal = zh_seed.isOdd();

			double dasa_length_sum = 0.0;
			for (int i=0; i<12; i++)
			{
				ZodiacHouse zh_dasa = null;
				if (bIsZodiacal)
					zh_dasa = zh_seed.add (order[i]);
				else
					zh_dasa = zh_seed.addReverse (order[i]);
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
			return "Trikona Dasa seeded from " + options.SeedRasi.ToString();
		}
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
		public object SetOptions (Object a)
		{
			UserOptions uo = (UserOptions)a;
			options.CopyFrom (uo);
			RecalculateEvent();
			return options.Clone();
		}
		new public void DivisionChanged (Division div)
		{
			UserOptions newOpts = (UserOptions)options.Clone();
			newOpts.Division = (Division)div.Clone();
			this.SetOptions(newOpts);
		}
	}
}
