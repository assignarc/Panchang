

using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class NaisargikaGrahaDasa: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			Horoscope h;
			ArrayList std_div_pos;
			public Division dtype = new Division(DivisionType.Rasi);
			protected BodyName mLordAqu;
			protected BodyName mLordSco;
			OrderedGrahas[] mGrahasStrengths;
			OrderedZodiacHouses[] mZodiacStrengths;
			bool bExcludeNodes;
			bool bExcludeDasaLord;
			bool bExclude_3_10;
			bool bExclude_2_6_11_12;

			public UserOptions (Horoscope _h) 
			{
				h = _h;
				std_div_pos = h.CalculateDivisionPositions(dtype);
				Strongest fs = new Strongest(h, dtype, Strongest.RulesStrongerCoLord(h));
				mLordSco = fs.StrongerGraha(BodyName.Mars, BodyName.Ketu, true);
				mLordAqu = fs.StrongerGraha(BodyName.Saturn, BodyName.Rahu, true);
				this.bExcludeNodes = true;
				this.bExcludeDasaLord = true;
				this.bExclude_3_10 = false;
				this.bExclude_2_6_11_12 = false;
				this.CalculateRasiStrengths();
				this.CalculateGrahaStrengths();
			}
            public void Recalculate()
			{
				Strongest fs = new Strongest(h, dtype, Strongest.RulesStrongerCoLord(h));
				mLordSco = fs.StrongerGraha(BodyName.Mars, BodyName.Ketu, true);
				mLordAqu = fs.StrongerGraha(BodyName.Saturn, BodyName.Rahu, true);
				this.CalculateRasiStrengths();
				this.CalculateGrahaStrengths();
			}
			public void CompareAndRecalculate (UserOptions newOpts)
			{
				if (newOpts.mLordAqu != this.mLordAqu ||
					newOpts.mLordSco != this.mLordSco)
				{
					newOpts.CalculateRasiStrengths();
					newOpts.CalculateGrahaStrengths();
					return;
				}

				for (int i=0; i<3; i++)
				{
					if (newOpts.mZodiacStrengths[i].houses.Count != this.mZodiacStrengths[i].houses.Count)
					{
						newOpts.CalculateGrahaStrengths();
						return;
					}
					for (int j=0; j<newOpts.mZodiacStrengths[i].houses.Count; j++)
					{
						if ((ZodiacHouseName)newOpts.mZodiacStrengths[i].houses[j] !=
							(ZodiacHouseName)this.mZodiacStrengths[i].houses[j])
						{
							newOpts.CalculateGrahaStrengths();
							return;
						}
					}
					
				}
			}

			public void CalculateRasiStrengths()
			{
				Strongest fs = new Strongest(h, dtype, Strongest.RulesNaisargikaDasaRasi(h));
				this.mZodiacStrengths = fs.ResultsZodiacKendras(h.CalculateDivisionPosition(h.GetPosition(BodyName.Lagna), dtype).ZodiacHouse.Value);
			}
			public void CalculateGrahaStrengths()
			{
				StrengthByConjunction fs_temp = new StrengthByConjunction (h, dtype);
				Strongest fs = new Strongest(h, dtype, Strongest.RulesNaisargikaDasaGraha(h));
				this.mGrahasStrengths = new OrderedGrahas[3];
				for (int i=0; i<mZodiacStrengths.Length; i++)
				{
					this.mGrahasStrengths[i] = new OrderedGrahas();
					OrderedZodiacHouses oz = this.mZodiacStrengths[i];
					foreach (ZodiacHouseName zn in oz.houses)
					{
						ArrayList temp = fs_temp.FindGrahasInHouse(zn);
						BodyName[] temp_arr = new BodyName[temp.Count];
						for (int j=0; j< temp.Count; j++)
							temp_arr[j] = (BodyName)temp[j];
						BodyName[] sorted = fs.GetOrderedGrahas(temp_arr);
						foreach (BodyName bn in sorted)
							this.mGrahasStrengths[i].grahas.Add (bn);					}
				}

			}

			[Category("1: Colord")]
			[PropertyOrder(1), Visible("Colord")]
			[Description("Is Ketu or Mars the stronger lord of Scorpio?")]
			public BodyName Lord_Sco
			{
				get { return this.mLordSco; }
				set { this.mLordSco = value; }
			}

			[Category("1: Colord")]
			[PropertyOrder(2), Visible("Lord of Aquarius")]
			[Description("Is Rahu or Saturn the stronger lord of Aquarius?")]
			public BodyName Lord_Aqu
			{
				get { return mLordAqu; }
				set { this.mLordAqu = value; }
			}

			[Category("2: Strengths")]
			[PropertyOrder(2), Visible("Graha strength order")]
			public OrderedGrahas[] GrahaStrengths
			{
				get { return this.mGrahasStrengths; }
				set { this.mGrahasStrengths = value; }
			}

			[Category("2: Strengths")]
			[PropertyOrder(1), Visible("Rasi strength order")]
			public OrderedZodiacHouses[] RasiStrengths
			{
				get { return this.mZodiacStrengths; }
				set { this.mZodiacStrengths = value; }
			}

			[Category("4: Exclude Antardasas")]
			[Visible("Exclude Rahu / Ketu")]
			public bool ExcludeNodes
			{
				get { return this.bExcludeNodes; }
				set { this.bExcludeNodes = value; }
			}

			[Category("4: Exclude Antardasas")]
			[Visible("Exclude dasa lord")]
			public bool ExcludeDasaLord
			{
				get { return this.bExcludeDasaLord; }
				set { this.bExcludeDasaLord = value; }
			}

			[Category("4: Exclude Antardasas")]
			[Visible("Exclude grahas in 3rd & 10th")]
			public bool Exclude_3_10
			{
				get { return this.bExclude_3_10; }
				set { this.bExclude_3_10 = value; }
			}

			[Category("4: Exclude Antardasas")]
			[Visible("Exclude grahas in 2nd, 6th, 11th & 12th")]
			public bool Exclude_2_6_11_12
			{
				get { return this.bExclude_2_6_11_12; }
				set { this.bExclude_2_6_11_12 = value; }
			}

			public Object Clone () 
			{
				UserOptions uo = new UserOptions(h);
				uo.mLordSco = this.mLordSco;
				uo.mLordAqu = this.mLordAqu;
				uo.mZodiacStrengths = new OrderedZodiacHouses[3];
				for (int i=0; i<3; i++)
				{
					uo.mZodiacStrengths[i] = (OrderedZodiacHouses)this.mZodiacStrengths[i].Clone();
					uo.mGrahasStrengths[i] = (OrderedGrahas)this.mGrahasStrengths[i].Clone();
				}
				uo.bExcludeDasaLord = this.bExcludeDasaLord;
				uo.bExcludeNodes = this.bExcludeNodes;
				uo.bExclude_2_6_11_12 = this.bExclude_2_6_11_12;
				uo.bExclude_3_10 = this.bExclude_3_10;
				return uo;
			}
		}

		private Horoscope h;
		private UserOptions options;
		public NaisargikaGrahaDasa (Horoscope _h)
		{
			h = _h;
			options = new UserOptions(h);
		}
        public double ParamAyus() => 120.0;
        public void RecalculateOptions() => options.Recalculate();
        public double lengthOfDasa (BodyName plt)
		{
			switch (plt)
			{
				case BodyName.Sun: return 20;
				case BodyName.Moon: return 1;
				case BodyName.Mars: return 2;
				case BodyName.Mercury: return 9;
				case BodyName.Jupiter: return 18;
				case BodyName.Venus: return 20;
				case BodyName.Saturn: return 50;
				case BodyName.Lagna: return 0;
			}
			Trace.Assert (false, "NaisargikaGrahaDasa::lengthOfDasa");
			return 0;
		}
		public ArrayList Dasa(int cycle)
		{
			ArrayList al = new ArrayList (36);
			BodyName[] order = new BodyName[] 
				{
					BodyName.Moon, BodyName.Mars, BodyName.Mercury,
					BodyName.Venus, BodyName.Jupiter,	BodyName.Sun,
					BodyName.Saturn, BodyName.Lagna
				};

			double cycle_start = ParamAyus() * (double)cycle;
			double curr = 0.0;
			foreach (BodyName bn in order) 
			{
				double dasaLength = lengthOfDasa (bn);
				al.Add (new DasaEntry (bn, cycle_start + curr, dasaLength, 1, Body.ToShortString(bn)));
				curr += dasaLength;
			}
			
			return al;
		}
		private bool ExcludeGraha (DasaEntry pdi, BodyName graha)
		{
			if (options.ExcludeDasaLord == true && 
				(graha == pdi.graha))
				return true;

			if (options.ExcludeNodes == true &&
				(graha == BodyName.Rahu ||
				(graha == BodyName.Ketu)))
				return true;
		
			int diff = 0;
			if (options.Exclude_3_10 || options.Exclude_2_6_11_12)
			{
				ZodiacHouse zhDasa = h.GetPosition(pdi.graha).ToDivisionPosition(options.dtype).ZodiacHouse;
				ZodiacHouse zhAntar = h.GetPosition(graha).ToDivisionPosition(options.dtype).ZodiacHouse;
				diff = zhDasa.NumHousesBetween(zhAntar);
			}

			if (options.Exclude_3_10 == true &&	(diff == 3 || diff == 10))
				return true;

			if (options.Exclude_2_6_11_12 == true && 
				(diff == 2 || diff == 6 || diff == 11 || diff == 12))
				return true;

			return false;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			OrderedGrahas orderedAntar = new OrderedGrahas();
			ZodiacHouse lzh = h.GetPosition(pdi.graha).ToDivisionPosition(options.dtype).ZodiacHouse;
			int kendra_start = (int)Basics.NormalizeLower(0, 3,((int)lzh.Value % 3));
			for (int i=kendra_start; i<=2; i++)
			{
				foreach (BodyName b in this.options.GrahaStrengths[i].grahas)
					orderedAntar.grahas.Add(b);
			}
			for (int i=0; i<kendra_start; i++)
			{
				foreach (BodyName b in this.options.GrahaStrengths[i].grahas)
					orderedAntar.grahas.Add(b);
			}

			int size = orderedAntar.grahas.Count;
			double[] antarLengths = new double[size];
			double totalAntarLengths = 0.0;
			ArrayList ret = new ArrayList(size-1);


			for (int i=0; i<size; i++)
			{

				if (this.ExcludeGraha(pdi, (BodyName)orderedAntar.grahas[i]))
					continue;

				int diff = lzh.NumHousesBetween(h.GetPosition(
					(BodyName)orderedAntar.grahas[i]).ToDivisionPosition(options.dtype).ZodiacHouse);
				switch (diff)
				{
					case 7: 
						antarLengths[i] = 12.0; 
						totalAntarLengths += antarLengths[i];
						break;		
					case 1: 
						antarLengths[i] = 42.0; 
						totalAntarLengths += antarLengths[i];
						break;
					case 4:	case 8: 
						antarLengths[i] = 21.0;
						totalAntarLengths += antarLengths[i];
						break;
					case 5:	case 9: 
						antarLengths[i] = 28.0;
						totalAntarLengths += antarLengths[i];
						break;
					case 2: case 3: case 6: case 10: case 11: case 12:
						antarLengths[i] = 84.0;
						totalAntarLengths += antarLengths[i];
						break;
					default:
						Trace.Assert(false, "Naisargika Dasa Antardasa lengths Internal Error 1");
						break;
				}
			}
			double curr = pdi.startUT;
			for (int i=0; i<size; i++)
			{
				BodyName bn = (BodyName)orderedAntar.grahas[i];

				if (this.ExcludeGraha(pdi, bn))
					continue;

				double length = (antarLengths[i] / totalAntarLengths) * pdi.dasaLength;
				string desc = pdi.shortDesc + " " + Body.ToShortString(bn);
				ret.Add(new DasaEntry(bn, curr, length, pdi.level+1, desc));
				curr += length;
			}
			return ret;
		}
		public String Description ()
		{
			return "Naisargika Graha Dasa (SR)";
		}
        public new Object Options => this.options.Clone();
        public object SetOptions (Object a)
		{
			UserOptions newOpts = (UserOptions)a;
			this.options.CompareAndRecalculate(newOpts);
			this.options = newOpts;
			RecalculateEvent();
			return options.Clone();
		}

	}
}
