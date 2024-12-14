

using System;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class MoolaDasa: Dasa, IDasa
	{
		public class UserOptions :ICloneable
		{
			Horoscope h;
			ArrayList std_div_pos;
			public Division dtype = new Division(DivisionType.Rasi);
			protected Body.Name mSeedBody;
			OrderedGrahas mGrahasStrengths;
			OrderedZodiacHouses[] mZodiacStrengths;

			public UserOptions (Horoscope _h) 
			{
				h = _h;
				std_div_pos = h.CalculateDivisionPositions(dtype);
				this.mSeedBody = Body.Name.Lagna;
				this.CalculateRasiStrengths();
				this.CalculateGrahaStrengths();
			}
			public void recalculate ()
			{
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

			public void CalculateRasiStrengths()
			{
				OrderedZodiacHouses[] zRet = new OrderedZodiacHouses[3];
				ZodiacHouse zh = h.getPosition(this.SeedBody).ToDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house;

				ZodiacHouseName[] zh_k = new ZodiacHouseName[4] { zh.add(1).value, zh.add(4).value, zh.add(7).value, zh.add(10).value };
				ZodiacHouseName[] zh_p = new ZodiacHouseName[4] { zh.add(2).value, zh.add(5).value, zh.add(8).value, zh.add(11).value };
				ZodiacHouseName[] zh_a = new ZodiacHouseName[4] { zh.add(3).value, zh.add(6).value, zh.add(9).value, zh.add(12).value };
				
				FindStronger fs = new FindStronger(h, dtype, FindStronger.RulesMoolaDasaRasi(h));
				zRet[0] = fs.GetOrderedHouses(zh_k);
				zRet[1] = fs.GetOrderedHouses(zh_p);
				zRet[2] = fs.GetOrderedHouses(zh_a);

				ZodiacHouseName zh_sat = h.getPosition(Body.Name.Saturn).ToDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house.value;
				ZodiacHouseName zh_ket = h.getPosition(Body.Name.Ketu).ToDivisionPosition(new Division(DivisionType.Rasi)).zodiac_house.value;

				bool bIsForward = zh.isOdd();
				if (zh_sat != zh_ket && zh_sat == zh.value)
					bIsForward = true;
				else if (zh_sat != zh_ket && zh_ket == zh.value)
					bIsForward = false;
				else if (zh_sat == zh_ket && zh_sat == zh.value)
				{
					ArrayList rule = new ArrayList();
					rule.Add (EGrahaStrength.Longitude);
					FindStronger fs2 = new FindStronger(h, new Division(DivisionType.Rasi), rule);
					bIsForward = fs2.CmpGraha(Body.Name.Saturn, Body.Name.Ketu, false);
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
				FindStronger fs = new FindStronger(h, dtype, FindStronger.RulesNaisargikaDasaGraha(h));
				this.mGrahasStrengths = new OrderedGrahas();
				foreach (OrderedZodiacHouses oz in this.mZodiacStrengths)
				{
					foreach (ZodiacHouseName zn in oz.houses)
					{
						ArrayList temp = fs_temp.findGrahasInHouse (zn);
						Body.Name[] temp_arr = new Body.Name[temp.Count];
						for (int i=0; i< temp.Count; i++)
							temp_arr[i] = (Body.Name)temp[i];
						Body.Name[] sorted = fs.GetOrderedGrahas(temp_arr);
						foreach (Body.Name bn in sorted)
							this.mGrahasStrengths.grahas.Add (bn);
					}
				}
			}

			[Category("Strengths1 Seed")]
			[PGDisplayName("Seed Body")]
			public Body.Name SeedBody
			{
				get { return this.mSeedBody; }
				set { this.mSeedBody = value; }
			}


			[Category("Strengths3 Grahas")]
			[PGDisplayName("Graha strength order")]
			public OrderedGrahas GrahaStrengths
			{
				get { return this.mGrahasStrengths; }
				set { this.mGrahasStrengths = value; }
			}

			[Category("Strengths2 Rasis")]
			[PGDisplayName("Rasi strength order")]
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
		public MoolaDasa (Horoscope _h)
		{
			h = _h;
			options = new UserOptions(h);
			vd = new VimsottariDasa(h);
		}
		public double paramAyus () 
		{
			return 120.0;
		}
		public void recalculateOptions ()
		{
			options.recalculate();
		}
		VimsottariDasa vd = null;
		public double lengthOfDasa (Body.Name plt)
		{
			double length = 0;

			// Count to moola trikona - 1.
			// Use Aqu / Sco as MT houses for Rahu / Ketu
			DivisionPosition dp_plt = h.getPosition(plt).ToDivisionPosition(new Division(DivisionType.Rasi));
			ZodiacHouse zh_plt = dp_plt.zodiac_house;
			ZodiacHouse zh_mt = Basics.GetMoolaTrikonaRasi(plt);
			if (plt == Body.Name.Rahu) zh_mt.value = ZodiacHouseName.Aqu;
			if (plt == Body.Name.Ketu) zh_mt.value = ZodiacHouseName.Sco;
			int diff = zh_plt.numHousesBetween(zh_mt);
			length = (double)(diff-1);

			// exaltation / debilitation correction
			if (dp_plt.isExaltedPhalita())
				length+=1.0;
			else if (dp_plt.isDebilitatedPhalita())
				length-=1.0;

			// subtract this length from the vimsottari lengths
			length = vd.lengthOfDasa(plt) - length;

			// Zero length = full vimsottari length.
			// If negative, make it positive
			if (length == 0) length = vd.lengthOfDasa(plt);
			else if (length < 0) length *= -1;

			return length;
		}
		public ArrayList Dasa(int cycle)
		{
			double cycle_start = paramAyus() * (double)cycle;
			double curr = 0.0;
			ArrayList al = new ArrayList (24);
			foreach (Body.Name b in options.GrahaStrengths.grahas)
			{
				double dasaLength = this.lengthOfDasa(b);
				al.Add (new DasaEntry (b, cycle_start + curr, dasaLength, 1, Body.ToShortString(b)));
				curr += dasaLength;
			}

			int numDasas = al.Count;
			for (int i=0; i< numDasas; i++)
			{
				DasaEntry de = (DasaEntry)al[i];
				double dasaLength = de.dasaLength-vd.lengthOfDasa(de.graha);
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
			foreach (Body.Name b in options.GrahaStrengths.grahas)
			{
				if (b != pdi.graha && bFound == false) continue;
				bFound = true;
				bOrder.Add (b);
			}
			foreach (Body.Name b in options.GrahaStrengths.grahas)
			{
				if (b == pdi.graha) break;
				bOrder.Add (b);
			}


			foreach (Body.Name b in bOrder)
			{
				double dasaLength = vd.lengthOfDasa(b) / vd.paramAyus() * pdi.dasaLength;
				al.Add (new DasaEntry(b, curr, dasaLength, pdi.level+1, pdi.shortDesc + " " + Body.ToShortString(b)));
				curr += dasaLength;
			}
			return al;
		}
		public String Description ()
		{
			return String.Format ("Moola Dasa seeded from {0}", options.SeedBody);
		}
		public Object GetOptions ()
		{
			return this.options.Clone();
		}
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
