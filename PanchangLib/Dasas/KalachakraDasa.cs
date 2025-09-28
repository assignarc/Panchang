
using System;
using System.Collections;

namespace org.transliteral.panchang
{
    public class KalachakraDasa: Dasa, IDasa
	{
		private Horoscope h;

		private ZodiacHouse[] mzhSavya = new ZodiacHouse[24];
		private ZodiacHouse[] mzhApasavya = new ZodiacHouse[24];

		
		public NakshatraGroupType NakshatraToGroup (Nakshatra n)
		{
			switch (n.Value)
			{
				case NakshatraName.Aswini:
				case NakshatraName.Krittika:
				case NakshatraName.Punarvasu:
				case NakshatraName.Aslesha:
				case NakshatraName.Hasta:
				case NakshatraName.Swati:
				case NakshatraName.Moola:
				case NakshatraName.UttaraShada:
				case NakshatraName.PoorvaBhadra:
					return NakshatraGroupType.Savya;
				case NakshatraName.Bharani:
				case NakshatraName.Pushya:
				case NakshatraName.Chittra:
				case NakshatraName.PoorvaShada:
				case NakshatraName.Revati:
					return NakshatraGroupType.SavyaMirrored;
				case NakshatraName.Rohini:
				case NakshatraName.Makha:
				case NakshatraName.Vishaka:
				case NakshatraName.Sravana:
					return NakshatraGroupType.Apasavya;
				default:
					return NakshatraGroupType.ApasavyaMirrored;
			}
			//switch ((int)n.Value % 6)
			//{
			//	case 1:	return NakshatraGroupType.Savya;
			//	case 2: return NakshatraGroupType.SavyaMirrored;
			//	case 3: return NakshatraGroupType.Savya;
			//	case 4: return NakshatraGroupType.Apasavya;
			//	case 5: return NakshatraGroupType.ApasavyaMirrored;
			//	default: return NakshatraGroupType.ApasavyaMirrored;
			//}
		}
        private void InitHelper(Longitude lon, ref ZodiacHouse[] mzhOrder, ref int offset)
		{
			NakshatraGroupType grp = this.NakshatraToGroup(lon.ToNakshatra());
			int pada = lon.ToNakshatraPada();

			switch (grp)
			{
				case NakshatraGroupType.Savya: 
				case NakshatraGroupType.SavyaMirrored:
					mzhOrder = mzhSavya; 
					break;
				default:
					mzhOrder = mzhApasavya;
					break;
			}

			switch (grp)
			{
				case NakshatraGroupType.Savya:
				case NakshatraGroupType.Apasavya:
					offset = 0;
					break;
				default:
					offset = 12;
					break;
			}
			offset = (int)Basics.NormalizeLower(0, 24, ((pada-1)*9)+offset);
		}
		public KalachakraDasa (Horoscope _h)
		{
			h = _h;

			ZodiacHouse zAri = new ZodiacHouse(ZodiacHouseName.Ari);
			ZodiacHouse zSag = new ZodiacHouse(ZodiacHouseName.Sag);
			for (int i=0; i<12; i++)
			{
				mzhSavya[i] = zAri.Add(i+1);
				mzhSavya[i+12] = mzhSavya[i].LordsOtherSign();
				mzhApasavya[i] = zSag.Add(i+1);
				mzhApasavya[i+12] = mzhApasavya[i].LordsOtherSign();
			}
		}
        public double ParamAyus() => 144;
        public double DasaLength (ZodiacHouse zh)
		{
			switch (zh.Value)
			{
				case ZodiacHouseName.Ari:
				case ZodiacHouseName.Sco:
					return 7;
				case ZodiacHouseName.Tau:
				case ZodiacHouseName.Lib:
					return 16;
				case ZodiacHouseName.Gem:
				case ZodiacHouseName.Vir:
					return 9;
				case ZodiacHouseName.Can:
					return 21;
				case ZodiacHouseName.Leo:
					return 5;
				case ZodiacHouseName.Sag:
				case ZodiacHouseName.Pis:
					return 10;
				case ZodiacHouseName.Cap:
				case ZodiacHouseName.Aqu:
					return 4;
				default:
					throw new Exception("KalachakraDasa::DasaLength");
			}
		}
		public ArrayList Dasa(int cycle)
		{
			Division dRasi = new Division(DivisionType.Rasi);
			Longitude mLon = h.GetPosition(BodyName.Moon).ExtrapolateLongitude(dRasi);

			int offset = 0;
			ZodiacHouse[] zhOrder = null;
			this.InitHelper(mLon, ref zhOrder, ref offset);

			ArrayList al = new ArrayList();

			double dasa_length_sum = 0;
			for (int i=0; i<9; i++)
			{
				ZodiacHouse zhCurr = zhOrder[(int)Basics.NormalizeLower(0,24,offset+i)];
				double dasa_length = this.DasaLength(zhCurr);
				DasaEntry de = new DasaEntry(zhCurr.Value, dasa_length_sum, dasa_length, 1, zhCurr.Value.ToString());
				al.Add(de);
				dasa_length_sum += dasa_length;
			}

			double offsetLength = mLon.ToNakshatraPadaPercentage() / 100.0 * dasa_length_sum;
	
			foreach (DasaEntry de in al)
			{
				de.startUT -= offsetLength;
			}

			return al;
		}
		public ArrayList AntarDasa (DasaEntry pdi) 
		{
			return new ArrayList();
		}
		public string Description ()
		{
			return "Kalachakra Dasa";
		}
        public new object Options => new object();
        public object SetOptions (object o)
		{
			return o;
		}
		public void RecalculateOptions ()
		{
		}
	}
}
