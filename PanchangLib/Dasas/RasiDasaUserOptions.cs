

using System;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    public class RasiDasaUserOptions : ICloneable
	{
		protected Horoscope h;
		protected Division mDtype;
		protected Body.Name mCoLordAqu;
		protected Body.Name mCoLordSco;
		protected OrderedZodiacHouses[] mSeventhStrengths;
		protected OrderedZodiacHouses mSaturnExceptions;
		protected OrderedZodiacHouses mKetuExceptions;
		protected ZodiacHouseName mSeed;
		protected int mSeedHouse;
		protected ArrayList mRules;


        public void Recalculate()
		{
			this.calculateCoLords();
			this.calculateExceptions();
			this.calculateSeed();
			this.calculateSeventhStrengths();
		}
		public RasiDasaUserOptions (Horoscope _h, ArrayList _rules)
		{
			h = _h;
			mRules = _rules;
			mSeventhStrengths = new OrderedZodiacHouses[6];
			mSaturnExceptions = new OrderedZodiacHouses();
			mKetuExceptions = new OrderedZodiacHouses();
			this.mDtype = new Division(DivisionType.Rasi);

			this.calculateCoLords();
			this.calculateExceptions();
			this.calculateSeed();
			this.calculateSeventhStrengths();
		}
		[InVisible]
		public Division Division
		{
			get { return this.mDtype; }
			set { this.mDtype = value; }
		}

		[@DisplayName("Division")]
		public DivisionType UIVarga
		{
			get { return this.mDtype.MultipleDivisions[0].Varga; }
			set { this.mDtype = new Division(value); }
		}

		[PropertyOrder(99), @DisplayName("Seed Rasi")]
		[Description("The rasi from which the dasa should be seeded.")]
		public ZodiacHouseName SeedRasi
		{
			get { return this.mSeed; }
			set { this.mSeed = value; }
		}
		[PropertyOrder(100), @DisplayName("Seed House")]
		[Description("House from which dasa should be seeded (reckoned from seed rasi)")]
		public int SeedHouse
		{
			get { return this.mSeedHouse; }
			set { this.mSeedHouse = value; }
		}

		[PropertyOrder(101), @DisplayName("Lord of Aquarius")]
		public Body.Name ColordAqu
		{
			get { return this.mCoLordAqu; }
			set { this.mCoLordAqu = value; }
		}
		[PropertyOrder(102), @DisplayName("Lord of Scorpio")]
		public Body.Name ColordSco
		{
			get { return this.mCoLordSco; }
			set { this.mCoLordSco = value; }
		}

		[PropertyOrder(103), @DisplayName("Rasi Strength Order")]
		public OrderedZodiacHouses[] SeventhStrengths
		{
			get { return this.mSeventhStrengths; }
			set { this.mSeventhStrengths = value; }
		}
		[PropertyOrder(104), @DisplayName("Rasis with Saturn Exception")]
		public OrderedZodiacHouses SaturnExceptions
		{
			get { return this.mSaturnExceptions; }
			set { this.mSaturnExceptions = value; }
		}
		[PropertyOrder(105), @DisplayName("Rasis with Ketu Exception")]
		public OrderedZodiacHouses KetuExceptions
		{
			get { return this.mKetuExceptions; }
			set { this.mKetuExceptions = value; }
		}
		virtual public object Clone ()
		{
			RasiDasaUserOptions uo = new RasiDasaUserOptions(h, mRules);
			uo.Division = (Division)this.Division.Clone();
			uo.ColordAqu = this.ColordAqu;
			uo.ColordSco = this.ColordSco;
			uo.mSeed = this.mSeed;
			uo.SeventhStrengths = this.SeventhStrengths;
			uo.KetuExceptions = this.KetuExceptions;
			uo.SaturnExceptions = this.SaturnExceptions;
			uo.SeedHouse = this.SeedHouse;
			return uo;
		}
		virtual public object CopyFrom (object _uo)
		{
			this.CopyFromNoClone(_uo);
			return this.Clone();
		}
		virtual public void CopyFromNoClone (object _uo)
		{
			RasiDasaUserOptions uo = (RasiDasaUserOptions)_uo;
			
			bool bDivisionChanged = false;
			bool bRecomputeChanged = false;

			if (this.Division != uo.Division)
				bDivisionChanged = true;
			if (this.ColordAqu != uo.ColordAqu ||
				this.ColordSco != uo.ColordSco)
				bRecomputeChanged = true;

			this.Division = (Division)uo.Division.Clone();
			this.ColordAqu = uo.ColordAqu;
			this.ColordSco = uo.ColordSco;
			this.mSeed = uo.mSeed;
			this.mSeedHouse = uo.mSeedHouse;
			for (int i=0; i<6; i++)
				this.SeventhStrengths[i] = (OrderedZodiacHouses)uo.SeventhStrengths[i].Clone();
			//this.SeventhStrengths = uo.SeventhStrengths.Clone();
			this.KetuExceptions = (OrderedZodiacHouses)uo.KetuExceptions.Clone();
			this.SaturnExceptions = (OrderedZodiacHouses)uo.SaturnExceptions.Clone();

			if (true == bDivisionChanged)
				this.calculateCoLords();

			if (true == bDivisionChanged || true == bRecomputeChanged)
			{
				this.calculateSeed();
				this.calculateSeventhStrengths();
				this.calculateExceptions();
			} 
		}
        public ZodiacHouse getSeed() => new ZodiacHouse(this.mSeed).Add(this.SeedHouse);
        public void calculateSeed ()
		{
			this.mSeed = h.GetPosition(Body.Name.Lagna).ToDivisionPosition(this.Division).ZodiacHouse.Value;
			this.mSeedHouse = 1;

		}
		public void calculateCoLords ()
		{
			FindStronger fs = new FindStronger(h, mDtype, FindStronger.RulesStrongerCoLord(h));
			this.mCoLordAqu = fs.StrongerGraha(Body.Name.Saturn, Body.Name.Rahu, true);
			this.mCoLordSco = fs.StrongerGraha(Body.Name.Mars, Body.Name.Ketu, true);
		}
		public void calculateExceptions ()
		{
			this.KetuExceptions.houses.Clear();
			this.SaturnExceptions.houses.Clear();

			ZodiacHouseName zhKetu = h.GetPosition(Body.Name.Ketu).ToDivisionPosition(this.Division).ZodiacHouse.Value;
			ZodiacHouseName zhSat = h.GetPosition(Body.Name.Saturn).ToDivisionPosition(this.Division).ZodiacHouse.Value;

			if (zhKetu != zhSat)
			{
				this.mKetuExceptions.houses.Add (zhKetu);
				this.mSaturnExceptions.houses.Add (zhSat);
			} 
			else
			{
				ArrayList rule = new ArrayList();
				rule.Add (EGrahaStrength.Longitude);
				FindStronger fs = new FindStronger(h, this.Division, rule);
				Body.Name b = fs.StrongerGraha(Body.Name.Saturn, Body.Name.Ketu, false);
				if (b == Body.Name.Ketu)
					this.mKetuExceptions.houses.Add (zhKetu);
				else
					this.mSaturnExceptions.houses.Add (zhSat);
			}

		}
		public ZodiacHouseName findStrongerRasi (OrderedZodiacHouses[] mList, ZodiacHouseName za, ZodiacHouseName zb)
		{
			for (int i=0; i<mList.Length; i++)
			{
				for (int j=0; j<mList[i].houses.Count; j++)
				{
					if ((ZodiacHouseName)mList[i].houses[j] == za) return za;
					if ((ZodiacHouseName)mList[i].houses[j] == zb) return zb;
				}
			}
			return za;
		}
		public bool ketuExceptionApplies (ZodiacHouseName zh)
		{
			for (int i=0; i<this.mKetuExceptions.houses.Count; i++)
			{
				if ((ZodiacHouseName)this.mKetuExceptions.houses[i] == zh)
					return true;
			}
			return false;
		}
		public bool saturnExceptionApplies (ZodiacHouseName zh)
		{
			for (int i=0; i<this.mSaturnExceptions.houses.Count; i++)
			{
				if ((ZodiacHouseName)this.mSaturnExceptions.houses[i] == zh)
					return true;
			}
			return false;
		}
		public void calculateSeventhStrengths ()
		{	
			FindStronger fs = new FindStronger(h, mDtype, this.mRules);
			ZodiacHouse zAri = new ZodiacHouse(ZodiacHouseName.Ari);
			for (int i=0; i<6; i++)
			{
				this.mSeventhStrengths[i] = new OrderedZodiacHouses();
				ZodiacHouse za = zAri.Add(i+1);
				ZodiacHouse zb = za.Add(7);
				if (fs.CompareRasi(za.Value, zb.Value, false))
				{
					this.mSeventhStrengths[i].houses.Add(za.Value);
					this.mSeventhStrengths[i].houses.Add(zb.Value);
				}
				else
				{
					this.mSeventhStrengths[i].houses.Add(zb.Value);
					this.mSeventhStrengths[i].houses.Add(za.Value);
				}
			}

		}
	}
}
