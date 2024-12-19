

using System;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class KarakaKendradiGrahaDasa: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			Horoscope h;
			ArrayList std_div_pos;
			public Division dtype = new Division(DivisionType.Rasi);
			protected BodyName mSeedBody;
			OrderedGrahas mGrahasStrengths;
			OrderedZodiacHouses[] mZodiacStrengths;

			public UserOptions (Horoscope _h) 
			{
				h = _h;
				std_div_pos = h.CalculateDivisionPositions(dtype);
				this.Recalculate();
			}
            public void Recalculate()
			{
				this.CalculateSeedBody();
				this.CalculateRasiStrengths();
				this.CalculateGrahaStrengths();
			}
			public void CompareAndRecalculate (UserOptions newOpts)
			{
				if (newOpts.SeedBody != this.SeedBody)
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

			public void CalculateSeedBody ()
			{
				ArrayList al_k = new ArrayList();
				for (int i=(int)BodyName.Sun; i<=(int)BodyName.Rahu; i++)
				{
					BodyName b = (BodyName)i;
					BodyPosition bp = h.GetPosition(b);
					BodyKarakaComparer bkc = new BodyKarakaComparer(bp);
					al_k.Add (bkc);
				}
				al_k.Sort();

				BodyPosition bp_ak = ((BodyKarakaComparer)al_k[0]).GetPosition;
				this.SeedBody = bp_ak.name;
			}
			public void CalculateRasiStrengths()
			{
				OrderedZodiacHouses[] zRet = new OrderedZodiacHouses[3];
				ZodiacHouse zh = h.GetPosition(this.SeedBody).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse;

				ZodiacHouseName[] zh_k = new ZodiacHouseName[4] { zh.Add(1).Value, zh.Add(4).Value, zh.Add(7).Value, zh.Add(10).Value };
				ZodiacHouseName[] zh_p = new ZodiacHouseName[4] { zh.Add(2).Value, zh.Add(5).Value, zh.Add(8).Value, zh.Add(11).Value };
				ZodiacHouseName[] zh_a = new ZodiacHouseName[4] { zh.Add(3).Value, zh.Add(6).Value, zh.Add(9).Value, zh.Add(12).Value };
				
				FindStronger fs = new FindStronger(h, dtype, FindStronger.RulesKarakaKendradiGrahaDasaRasi(h));
				zRet[0] = fs.GetOrderedHouses(zh_k);
				zRet[1] = fs.GetOrderedHouses(zh_p);
				zRet[2] = fs.GetOrderedHouses(zh_a);

				ZodiacHouseName zh_sat = h.GetPosition(BodyName.Saturn).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse.Value;
				ZodiacHouseName zh_ket = h.GetPosition(BodyName.Ketu).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse.Value;

				bool bIsForward = zh.IsOdd();
				if (zh_sat != zh_ket && zh_sat == zh.Value)
					bIsForward = true;
				else if (zh_sat != zh_ket && zh_ket == zh.Value)
					bIsForward = false;
				else if (zh_sat == zh_ket && zh_sat == zh.Value)
				{
					ArrayList rule = new ArrayList();
					rule.Add (EGrahaStrength.Longitude);
					FindStronger fs2 = new FindStronger(h, new Division(DivisionType.Rasi), rule);
					bIsForward = fs2.CompareGraha(BodyName.Saturn, BodyName.Ketu, false);
				}


				this.mZodiacStrengths = new OrderedZodiacHouses[3];
				this.mZodiacStrengths[0] = zRet[0];
				if (bIsForward)
				{
					this.mZodiacStrengths[1] = zRet[1];
					this.mZodiacStrengths[2] = zRet[2];
				} 
				else
				{
					this.mZodiacStrengths[1] = zRet[2];
					this.mZodiacStrengths[2] = zRet[1];
				}
			}
			public void CalculateGrahaStrengths()
			{
				StrengthByConjunction fs_temp = new StrengthByConjunction (h, dtype);
				FindStronger fs = new FindStronger(h, dtype, FindStronger.RulesKarakaKendradiGrahaDasaGraha(h));
				this.mGrahasStrengths = new OrderedGrahas();
				foreach (OrderedZodiacHouses oz in this.mZodiacStrengths)
				{
					foreach (ZodiacHouseName zn in oz.houses)
					{
						ArrayList temp = fs_temp.FindGrahasInHouse (zn);
						BodyName[] temp_arr = new BodyName[temp.Count];
						for (int i=0; i< temp.Count; i++)
							temp_arr[i] = (BodyName)temp[i];
						BodyName[] sorted = fs.GetOrderedGrahas(temp_arr);
						foreach (BodyName bn in sorted)
							this.mGrahasStrengths.grahas.Add (bn);
					}
				}
			}

			[Category("Strengths1 Seed")]
			[Visible("Seed Body")]
			public BodyName SeedBody
			{
				get { return this.mSeedBody; }
				set { this.mSeedBody = value; }
			}


			[Category("Strengths3 Grahas")]
			[Visible("Graha strength order")]
			public OrderedGrahas GrahaStrengths
			{
				get { return this.mGrahasStrengths; }
				set { this.mGrahasStrengths = value; }
			}

			[Category("Strengths2 Rasis")]
			[Visible("Rasi strength order")]
			public OrderedZodiacHouses[] RasiStrengths
			{
				get { return this.mZodiacStrengths; }
				set { this.mZodiacStrengths = value; }
			}


			public Object Clone () 
			{
				UserOptions uo = new UserOptions(h);
				uo.mGrahasStrengths = (OrderedGrahas)this.mGrahasStrengths.Clone();
				uo.mZodiacStrengths = new OrderedZodiacHouses[3];
				uo.mSeedBody = this.mSeedBody;
				for (int i=0; i<3; i++)
					uo.mZodiacStrengths[i] = (OrderedZodiacHouses)this.mZodiacStrengths[i].Clone();
					
				return uo;
			}
		}

		private Horoscope h;
		private UserOptions options;
		public KarakaKendradiGrahaDasa (Horoscope _h)
		{
			h = _h;
			options = new UserOptions(h);
			vd = new VimsottariDasa(h);
		}
		public double ParamAyus () 
		{
			return 120.0;
		}
		public void RecalculateOptions ()
		{
			options.Recalculate();
		}
		VimsottariDasa vd = null;
		public double lengthOfDasa (BodyName plt)
		{
			DivisionPosition dp_plt = h.GetPosition(plt).ToDivisionPosition(new Division(DivisionType.Rasi));
			return KarakaKendradiGrahaDasa.LengthOfDasa(h, options.dtype, plt, dp_plt);
		}
		public static double LengthOfDasa (Horoscope h, Division dtype, BodyName plt, DivisionPosition dp_plt)
		{
			double length = 0;

			// Count to moola trikona - 1.
			// Use Aqu / Sco as MT houses for Rahu / Ketu
			//DivisionPosition dp_plt = h.getPosition(plt).toDivisionPosition(new Division(DivisionType.Rasi));
			ZodiacHouse zh_plt = dp_plt.ZodiacHouse;
			ZodiacHouse zh_mt = Basics.GetMoolaTrikonaRasi(plt);
			
			if (plt == BodyName.Rahu) zh_mt.Value = ZodiacHouseName.Aqu;
			if (plt == BodyName.Ketu) zh_mt.Value = ZodiacHouseName.Sco;

			int diff = zh_plt.NumHousesBetween(zh_mt);
			length = (double)(diff-1);

			// exaltation / debilitation correction
			if (dp_plt.IsExaltedPhalita())
				length+=1.0;
			else if (dp_plt.IsDebilitatedPhalita())
				length-=1.0;

			if (plt == h.LordOfZodiacHouse(zh_plt, dtype))
				length = 12.0;

			// subtract this length from the vimsottari lengths
			length = VimsottariDasa.LengthOfDasaS(plt) - length;

			// Zero length = full vimsottari length.
			// If negative, make it positive
			if (length == 0) length = VimsottariDasa.LengthOfDasaS(plt);
			else if (length < 0) length *= -1;

			return length;
		}
		public ArrayList Dasa(int cycle)
		{
			double cycle_start = ParamAyus() * (double)cycle;
			double curr = 0.0;
			ArrayList al = new ArrayList (24);
			foreach (BodyName b in options.GrahaStrengths.grahas)
			{
				double dasaLength = this.lengthOfDasa(b);
				al.Add (new DasaEntry (b, cycle_start + curr, dasaLength, 1, Body.ToShortString(b)));
				curr += dasaLength;
			}

			int numDasas = al.Count;
			for (int i=0; i< numDasas; i++)
			{
				DasaEntry de = (DasaEntry)al[i];
				double dasaLength = de.dasaLength-vd.LengthOfDasa(de.graha);
				if (dasaLength < 0) dasaLength *= -1;
				al.Add (new DasaEntry (de.graha, cycle_start + curr, dasaLength, 1, Body.ToShortString(de.graha)));
				curr += dasaLength;

			}
			
			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			ArrayList al = new ArrayList();
			double curr = pdi.startUT;

			ArrayList bOrder = new ArrayList();
			bool bFound = false;
			foreach (BodyName b in options.GrahaStrengths.grahas)
			{
				if (b != pdi.graha && bFound == false) continue;
				bFound = true;
				bOrder.Add (b);
			}
			foreach (BodyName b in options.GrahaStrengths.grahas)
			{
				if (b == pdi.graha) break;
				bOrder.Add (b);
			}


			double dasaLength = pdi.dasaLength / 9.0;
			foreach (BodyName b in bOrder)
			{
				al.Add (new DasaEntry(b, curr, dasaLength, pdi.level+1, pdi.shortDesc + " " + Body.ToShortString(b)));
				curr += dasaLength;
			}
			return al;
		}
		public String Description ()
		{
			return String.Format ("Karaka Kendradi Graha Dasa seeded from {0}", options.SeedBody);
		}
        public Object Options => this.options.Clone();
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
