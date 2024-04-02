

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
/*using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;*/

namespace org.transliteral.panchang
{


    public delegate void EvtChanged (Object h);
	public delegate void SetOptionsDelegate (Object sender);
	public delegate void Recalculate ();

	
	/// <summary>
	/// Simple functions that don't belong anywhere else
	/// </summary>
	public class Basics
	{
		/// <summary>
		/// Normalize a number between bounds
		/// </summary>
		/// <param name="lower">The lower bound of normalization</param>
		/// <param name="upper">The upper bound of normalization</param>
		/// <param name="x">The value to be normalized</param>
		/// <returns>The normalized value of x, where lower <= x <= upper </returns>
		public static int normalize_inc (int lower, int upper, int x) 
		{
			int size = upper - lower + 1;
			while (x > upper) x -= size;
			while (x < lower) x += size;
			Trace.Assert (x >= lower && x <= upper, "Basics.normalize failed");
			return x;
		}

		/// <summary>
		/// Normalize a number between bounds
		/// </summary>
		/// <param name="lower">The lower bound of normalization</param>
		/// <param name="upper">The upper bound of normalization</param>
		/// <param name="x">The value to be normalized</param>
		/// <returns>The normalized value of x, where lower = x <= upper </returns>
		public static double normalize_exc (double lower, double upper, double x) 
		{
			double size = upper - lower;
			while (x > upper) x -= size;
			while (x <= lower) x += size;
			Trace.Assert (x >= lower && x <= upper, "Basics.normalize failed");
			return x;
		}

		public static double normalize_exc_lower (double lower, double upper, double x) 
		{
			double size = upper - lower;
			while (x >= upper) x -= size;
			while (x < lower) x += size;
			Trace.Assert (x >= lower && x <= upper, "Basics.normalize failed");
			return x;
		}

		public static ZodiacHouse getMoolaTrikonaRasi (Body.Name b)
		{
			ZodiacHouseName z = ZodiacHouseName.Ari;
			switch (b)
			{
				case Body.Name.Sun: z = ZodiacHouseName.Leo; break;
				case Body.Name.Moon: z = ZodiacHouseName.Tau; break;
				case Body.Name.Mars: z = ZodiacHouseName.Ari; break;
				case Body.Name.Mercury: z = ZodiacHouseName.Vir; break;
				case Body.Name.Jupiter: z = ZodiacHouseName.Sag; break;
				case Body.Name.Venus: z = ZodiacHouseName.Lib; break;
				case Body.Name.Saturn: z = ZodiacHouseName.Aqu; break;
				case Body.Name.Rahu: z = ZodiacHouseName.Vir; break;
				case Body.Name.Ketu: z = ZodiacHouseName.Pis; break;
			}
			return new ZodiacHouse (z);
		}
		public static Weekday bodyToWeekday (Body.Name b)
		{
			switch (b)
			{
				case Body.Name.Sun: return Weekday.Sunday;
				case Body.Name.Moon: return Weekday.Monday;
				case Body.Name.Mars: return Weekday.Tuesday;
				case Body.Name.Mercury: return Weekday.Wednesday;
				case Body.Name.Jupiter: return Weekday.Thursday;
				case Body.Name.Venus: return Weekday.Friday;
				case Body.Name.Saturn: return Weekday.Saturday;
			}
			Debug.Assert(false, string.Format("bodyToWeekday({0})", b));
			throw new Exception();
		}
		public static Body.Name weekdayRuler (Weekday w)
		{
			switch (w)
			{
				case Weekday.Sunday: return Body.Name.Sun;
				case Weekday.Monday: return Body.Name.Moon;
				case Weekday.Tuesday: return Body.Name.Mars;
				case Weekday.Wednesday: return Body.Name.Mercury;
				case Weekday.Thursday: return Body.Name.Jupiter;
				case Weekday.Friday: return Body.Name.Venus;
				case Weekday.Saturday: return Body.Name.Saturn;
				default:
					Debug.Assert(false, "Basics::weekdayRuler");
					return Body.Name.Sun;
			}
		}

		// This matches the sweph definitions for easy conversion
		public enum Weekday : int
		{
			Monday=0, Tuesday=1, Wednesday=2, Thursday=3, Friday=4, Saturday=5, Sunday=6
		}

		public static string weekdayToShortString (Weekday w)
		{
			switch (w)
			{
				case Weekday.Sunday: return "Su";
				case Weekday.Monday: return "Mo";
				case Weekday.Tuesday: return "Tu";
				case Weekday.Wednesday: return "We";
				case Weekday.Thursday: return "Th";
				case Weekday.Friday: return "Fr";
				case Weekday.Saturday: return "Sa";
			}
			return "";
		}
	
		

		static public Nakshatra28Name NakLordOfMuhurta (Muhurta m)
		{
			switch (m)
			{
				case Muhurta.Rudra: return Nakshatra28Name.Aridra;
				case Muhurta.Ahi: return Nakshatra28Name.Aslesha;
				case Muhurta.Mitra: return Nakshatra28Name.Anuradha;
				case Muhurta.Pitri: return Nakshatra28Name.Makha;
				case Muhurta.Vasu: return Nakshatra28Name.Dhanishta;
				case Muhurta.Ambu: return Nakshatra28Name.PoorvaShada;
				case Muhurta.Visvadeva: return Nakshatra28Name.UttaraShada;
				case Muhurta.Abhijit: return Nakshatra28Name.Abhijit;
				case Muhurta.Vidhata: return Nakshatra28Name.Rohini;
				case Muhurta.Puruhuta: return Nakshatra28Name.Jyestha;
				case Muhurta.Indragni: return Nakshatra28Name.Vishaka;
				case Muhurta.Nirriti: return Nakshatra28Name.Moola;
				case Muhurta.Varuna: return Nakshatra28Name.Satabisha;
				case Muhurta.Aryaman: return Nakshatra28Name.UttaraPhalguni;
				case Muhurta.Bhaga: return Nakshatra28Name.PoorvaPhalguni;
				case Muhurta.Girisa: return Nakshatra28Name.Aridra;
				case Muhurta.Ajapada: return Nakshatra28Name.PoorvaBhadra;
				case Muhurta.Ahirbudhnya: return Nakshatra28Name.UttaraBhadra;
				case Muhurta.Pushan: return Nakshatra28Name.Revati;
				case Muhurta.Asvi: return Nakshatra28Name.Aswini;
				case Muhurta.Yama: return Nakshatra28Name.Bharani;
				case Muhurta.Agni: return Nakshatra28Name.Krittika;
				case Muhurta.Vidhaatri: return Nakshatra28Name.Rohini;
				case Muhurta.Chanda: return Nakshatra28Name.Mrigarirsa;
				case Muhurta.Aditi: return Nakshatra28Name.Punarvasu;
				case Muhurta.Jiiva: return Nakshatra28Name.Pushya;
				case Muhurta.Vishnu: return Nakshatra28Name.Sravana;
				case Muhurta.Arka: return Nakshatra28Name.Hasta;
				case Muhurta.Tvashtri: return Nakshatra28Name.Chittra;
				case Muhurta.Maruta: return Nakshatra28Name.Swati;
			}
			Debug.Assert (false, string.Format("Basics::NakLordOfMuhurta Unknown Muhurta {0}", m));
			return Nakshatra28Name.Aswini;
		}
		public static string variationNameOfDivision (Division d)
		{
			if (d.MultipleDivisions.Length > 1)
				return "Composite";

			switch (d.MultipleDivisions[0].Varga)
			{
				case DivisionType.Rasi: 
					return "Rasi";
				case DivisionType.Navamsa: 
					return "Navamsa";
				case DivisionType.HoraParasara:
					return "Parasara";
				case DivisionType.HoraJagannath:
					return "Jagannath";
				case DivisionType.HoraParivrittiDwaya:
					return "Parivritti";
				case DivisionType.HoraKashinath:
					return "Kashinath";
				case DivisionType.DrekkanaParasara:
					return "Parasara";
				case DivisionType.DrekkanaJagannath:
					return "Jagannath";
				case DivisionType.DrekkanaParivrittitraya:
					return "Parivritti";
				case DivisionType.DrekkanaSomnath:
					return "Somnath";
				case DivisionType.Chaturthamsa: 
					return "";
				case DivisionType.Panchamsa:
					return "";
				case DivisionType.Shashthamsa:
					return "";
				case DivisionType.Saptamsa:
					return "";
				case DivisionType.Ashtamsa:
					return "Rath";
				case DivisionType.AshtamsaRaman:
					return "Raman";
				case DivisionType.Dasamsa:
					return "";
				case DivisionType.Rudramsa:
					return "Rath";
				case DivisionType.RudramsaRaman:
					return "Raman";
				case DivisionType.Dwadasamsa:
					return "";
				case DivisionType.Shodasamsa:
					return "";
				case DivisionType.Vimsamsa:
					return "";
				case DivisionType.Chaturvimsamsa:
					return "";
				case DivisionType.Nakshatramsa:
					return "";
				case DivisionType.Trimsamsa:
					return "";
				case DivisionType.TrimsamsaParivritti:
					return "Parivritti";
				case DivisionType.TrimsamsaSimple:
					return "Simple";
				case DivisionType.Khavedamsa:
					return "";
				case DivisionType.Akshavedamsa:
					return "";
				case DivisionType.Shashtyamsa:
					return "";
				case DivisionType.Ashtottaramsa:
					return "";
				case DivisionType.Nadiamsa:
					return "Equal Size";
				case DivisionType.NadiamsaCKN:
					return "Chandra Kala";
				case DivisionType.NavamsaDwadasamsa:
					return "Composite";
				case DivisionType.DwadasamsaDwadasamsa:
					return "Composite";
				case DivisionType.BhavaPada:
					return "9 Padas";
				case DivisionType.BhavaEqual:
					return "Equal Houses";
				case DivisionType.BhavaAlcabitus:
					return "Alcabitus";
				case DivisionType.BhavaAxial:
					return "Axial";
				case DivisionType.BhavaCampanus:
					return "Campanus";
				case DivisionType.BhavaKoch:
					return "Koch";
				case DivisionType.BhavaPlacidus:
					return "Placidus";
				case DivisionType.BhavaRegiomontanus:
					return "Regiomontanus";
				case DivisionType.BhavaSripati:
					return "Sripati";
				case DivisionType.GenericParivritti:
					return "Parivritti";
				case DivisionType.GenericShashthamsa:
					return "Regular: Shashtamsa";
				case DivisionType.GenericSaptamsa:
					return "Regular: Saptamsa";
				case DivisionType.GenericDasamsa:
					return "Regular: Dasamsa";
				case DivisionType.GenericDwadasamsa:
					return "Regular: Dwadasamsa";
				case DivisionType.GenericChaturvimsamsa:
					return "Regular: Chaturvimsamsa";
				case DivisionType.GenericChaturthamsa:
					return "Kendra: Chaturtamsa";
				case DivisionType.GenericNakshatramsa:
					return "Kendra: Nakshatramsa";
				case DivisionType.GenericDrekkana:
					return "Trikona: Drekkana";
				case DivisionType.GenericShodasamsa:
					return "Trikona: Shodasamsa";
				case DivisionType.GenericVimsamsa:
					return "Trikona: Vimsamsa";
			}
			Debug.Assert(false, string.Format("Basics::numPartsInDivisionType. Unknown Division {0}", d.MultipleDivisions[0].Varga));
			return "";
		}

		public static string numPartsInDivisionString (Division div)
		{
			string sRet = "D";
			foreach (Division.SingleDivision dSingle in div.MultipleDivisions)
			{
				sRet = string.Format("{0}-{1}", sRet, numPartsInDivision(dSingle));
			}
			return sRet;
		}
		public static int numPartsInDivision (Division div)
		{
			int parts = 1;
			foreach (Division.SingleDivision dSingle in div.MultipleDivisions)
			{
				parts *= numPartsInDivision(dSingle);
			}
			return parts;
		}
		public static int numPartsInDivision (Division.SingleDivision dSingle)
		{
			
			switch (dSingle.Varga)
			{
				case DivisionType.Rasi: 
					return 1;
				case DivisionType.Navamsa: 
					return 9;
				case DivisionType.HoraParasara:
				case DivisionType.HoraJagannath:
				case DivisionType.HoraParivrittiDwaya:
				case DivisionType.HoraKashinath:
					return 2;
				case DivisionType.DrekkanaParasara:
				case DivisionType.DrekkanaJagannath:
				case DivisionType.DrekkanaParivrittitraya:
				case DivisionType.DrekkanaSomnath:
					return 3;
				case DivisionType.Chaturthamsa: 
					return 4;
				case DivisionType.Panchamsa:
					return 5;
				case DivisionType.Shashthamsa:
					return 6;
				case DivisionType.Saptamsa:
					return 7;
				case DivisionType.Ashtamsa:
				case DivisionType.AshtamsaRaman:
					return 8;
				case DivisionType.Dasamsa:
					return 10;
				case DivisionType.Rudramsa:
				case DivisionType.RudramsaRaman:
					return 11;
				case DivisionType.Dwadasamsa:
					return 12;
				case DivisionType.Shodasamsa:
					return 16;
				case DivisionType.Vimsamsa:
					return 20;
				case DivisionType.Chaturvimsamsa:
					return 24;
				case DivisionType.Nakshatramsa:
					return 27;
				case DivisionType.Trimsamsa:
				case DivisionType.TrimsamsaParivritti:
				case DivisionType.TrimsamsaSimple:
					return 30;
				case DivisionType.Khavedamsa:
					return 40;
				case DivisionType.Akshavedamsa:
					return 45;
				case DivisionType.Shashtyamsa:
					return 60;
				case DivisionType.Ashtottaramsa:
					return 108;
				case DivisionType.Nadiamsa:
				case DivisionType.NadiamsaCKN:
					return 150;
				case DivisionType.NavamsaDwadasamsa:
					return 108;
				case DivisionType.DwadasamsaDwadasamsa:
					return 144;
				case DivisionType.BhavaPada:
				case DivisionType.BhavaEqual:
				case DivisionType.BhavaAlcabitus:
				case DivisionType.BhavaAxial:
				case DivisionType.BhavaCampanus:
				case DivisionType.BhavaKoch:
				case DivisionType.BhavaPlacidus:
				case DivisionType.BhavaRegiomontanus:
				case DivisionType.BhavaSripati:
					return 1;
				default:
					return dSingle.NumParts;
			}
		}

		public static Division[] Shadvargas ()
		{
			return new Division[]
			{
				new Division(DivisionType.Rasi), 
				new Division(DivisionType.HoraParasara), 
				new Division(DivisionType.DrekkanaParasara),
				new Division(DivisionType.Navamsa), 
				new Division(DivisionType.Dwadasamsa), 
				new Division(DivisionType.Trimsamsa)
			};
		}

		public static Division[] Saptavargas ()
		{
			return new Division[]
			{
				new Division(DivisionType.Rasi), 
				new Division(DivisionType.HoraParasara), 
				new Division(DivisionType.DrekkanaParasara),
				new Division(DivisionType.Saptamsa),
				new Division(DivisionType.Navamsa), 
				new Division(DivisionType.Dwadasamsa), 
				new Division(DivisionType.Trimsamsa)
			};
		}

		public static Division[] Dasavargas ()
		{
			return new Division[]
			{
				new Division(DivisionType.Rasi), 
				new Division(DivisionType.HoraParasara), 
				new Division(DivisionType.DrekkanaParasara),
				new Division(DivisionType.Saptamsa), 
				new Division(DivisionType.Navamsa), 
				new Division(DivisionType.Dasamsa),
				new Division(DivisionType.Dwadasamsa), 
				new Division(DivisionType.Shodasamsa), 
				new Division(DivisionType.Trimsamsa),
				new Division(DivisionType.Shashtyamsa)
			};
		}

		public static Division[] Shodasavargas ()
		{
			return new Division[]
			{
				new Division(DivisionType.Rasi), 
				new Division(DivisionType.HoraParasara), 
				new Division(DivisionType.DrekkanaParasara),
				new Division(DivisionType.Chaturthamsa), 
				new Division(DivisionType.Saptamsa), 
				new Division(DivisionType.Navamsa), 
				new Division(DivisionType.Dasamsa), 
				new Division(DivisionType.Dwadasamsa), 
				new Division(DivisionType.Shodasamsa), 
				new Division(DivisionType.Vimsamsa), 
				new Division(DivisionType.Chaturvimsamsa), 
				new Division(DivisionType.Nakshatramsa),
				new Division(DivisionType.Trimsamsa), 
				new Division(DivisionType.Khavedamsa), 
				new Division(DivisionType.Akshavedamsa),
				new Division(DivisionType.Shashtyamsa)
			};
		}

		/// <summary>
		/// Specify the Lord of a ZodiacHouse. The owernership of the nodes is not considered
		/// </summary>
		/// <param name="zh">The House whose lord should be returned</param>
		/// <returns>The lord of zh</returns>
		public static Body.Name SimpleLordOfZodiacHouse (ZodiacHouseName zh) 
		{
			switch (zh) 
			{
				case ZodiacHouseName.Ari: return Body.Name.Mars;
				case ZodiacHouseName.Tau: return Body.Name.Venus;
				case ZodiacHouseName.Gem: return Body.Name.Mercury;
				case ZodiacHouseName.Can: return Body.Name.Moon;
				case ZodiacHouseName.Leo: return Body.Name.Sun; 
				case ZodiacHouseName.Vir: return Body.Name.Mercury;
				case ZodiacHouseName.Lib: return Body.Name.Venus;
				case ZodiacHouseName.Sco: return Body.Name.Mars;
				case ZodiacHouseName.Sag: return Body.Name.Jupiter;
				case ZodiacHouseName.Cap: return Body.Name.Saturn;
				case ZodiacHouseName.Aqu: return Body.Name.Saturn;
				case ZodiacHouseName.Pis: return Body.Name.Jupiter;
			}
			
			Trace.Assert (false, 
			string.Format ("Basics.SimpleLordOfZodiacHouse for {0} failed", (int)zh));
			return Body.Name.Other;
		}


		public static Longitude CalculateBodyLongitude (double ut, int ipl)
		{
			double[] xx = new Double[6]{0,0,0,0,0,0};
			try
			{
				Sweph.swe_calc_ut(ut, ipl, 0, xx);
				return new Longitude(xx[0]);
			}
			catch (SwephException exc)
			{
				System.Console.WriteLine ( "Sweph: {0}\n", exc.Message);
				throw new System.Exception("");
			}
		}

		/// <summary>
		/// Calculated a BodyPosition for a given time and place using the swiss ephemeris
		/// </summary>
		/// <param name="ut">The time for which the calculations should be performed</param>
		/// <param name="ipl">The Swiss Ephemeris body Name</param>
		/// <param name="body">The local application body name</param>
		/// <param name="type">The local application body type</param>
		/// <returns>A BodyPosition class</returns>
		/// 
		public static BodyPosition CalculateSingleBodyPosition (double ut, int ipl, Body.Name body, BodyType.Name type, Horoscope h) 
		{
			if (body == Body.Name.Lagna)
			{
				BodyPosition b = new BodyPosition(h, body, type, new Longitude(Sweph.swe_lagna(ut)), 0, 0, 0, 0, 0);
				return b;
			}
			double[] xx = new Double[6] {0,0,0,0,0,0};
			try 
			{
				Sweph.swe_calc_ut (ut, ipl, 0, xx);

				BodyPosition b = new BodyPosition (h, body, type, new Longitude(xx[0]), xx[1], xx[2],
					xx[3], xx[4], xx[5]);
				return b;
			} 
			catch (SwephException exc) 
			{
				System.Console.WriteLine ( "Sweph: {0}\n", exc.Message	);
				throw new System.Exception("");
			}
		}


		/// <summary>
		/// Given a HoraInfo object (all required user inputs), calculate a list of
		/// all bodypositions we will ever require
		/// </summary>
		/// <param name="h">The HoraInfo object</param>
		/// <returns></returns>
		public static ArrayList CalculateBodyPositions (Horoscope h, double sunrise) 
		{
			HoraInfo hi = h.info;
			HoroscopeOptions o = h.options;

			StringBuilder serr = new StringBuilder (256);
			string ephe_path = MhoraGlobalOptions.Instance.HOptions.EphemerisPath;

			// The order of the array must reflect the order define in Basics.GrahaIndex
			ArrayList std_grahas = new ArrayList (20);
			
			Sweph.swe_set_ephe_path (ephe_path);
			double julday_ut = Sweph.swe_julday (hi.tob.year, hi.tob.month, hi.tob.day,
				hi.tob.time - hi.tz.toDouble());
			//	h.tob.hour + (((double)h.tob.minute) / 60.0) + (((double)h.tob.second) / 3600.0));
			//	(h.tob.time / 24.0) + (h.tz.toDouble()/24.0));
				//(h.tob.hour/24.0) + (((double)h.tob.minute) / 60.0) + (((double)h.tob.second) / 3600.0));
			//julday_ut = julday_ut - (h.tz.toDouble() / 24.0);
			
			int swephRahuBody = Sweph.SE_MEAN_NODE;
			if (o.nodeType == ENodeType.True)
				swephRahuBody = Sweph.SE_TRUE_NODE;

			int addFlags = 0;
			if (o.grahaPositionType == EGrahaPositionType.True)
				addFlags = Sweph.SEFLG_TRUEPOS;

			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_SUN, Body.Name.Sun, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_MOON, Body.Name.Moon, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_MARS, Body.Name.Mars, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_MERCURY, Body.Name.Mercury, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_JUPITER, Body.Name.Jupiter, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_VENUS, Body.Name.Venus, BodyType.Name.Graha, h));
			std_grahas.Add (CalculateSingleBodyPosition (julday_ut, Sweph.SE_SATURN, Body.Name.Saturn, BodyType.Name.Graha, h));
			BodyPosition rahu = CalculateSingleBodyPosition (julday_ut, swephRahuBody, Body.Name.Rahu, BodyType.Name.Graha, h);

			BodyPosition ketu = CalculateSingleBodyPosition (julday_ut, swephRahuBody, Body.Name.Ketu, BodyType.Name.Graha, h);
			ketu.longitude = rahu.longitude.add (new Longitude (180.0));
			std_grahas.Add (rahu);
			std_grahas.Add (ketu);

			double asc = Sweph.swe_lagna(julday_ut);
			std_grahas.Add (new BodyPosition (h, Body.Name.Lagna, BodyType.Name.Lagna, new Longitude (asc), 0, 0, 0, 0, 0));

			double ista_ghati = normalize_exc( 0.0, 24.0, hi.tob.time - sunrise) * 2.5;
			Longitude gl_lon = ((BodyPosition)std_grahas[0]).longitude.add(new Longitude(ista_ghati * 30.0));
			Longitude hl_lon = ((BodyPosition)std_grahas[0]).longitude.add(new Longitude(ista_ghati * 30.0/ 2.5));
			Longitude bl_lon = ((BodyPosition)std_grahas[0]).longitude.add(new Longitude(ista_ghati * 30.0/ 5.0));

			double vl = ista_ghati * 5.0;
			while (ista_ghati > 12.0) ista_ghati -= 12.0;
			Longitude vl_lon = ((BodyPosition)std_grahas[0]).longitude.add(new Longitude(vl * 30.0));

			std_grahas.Add (new BodyPosition (h, Body.Name.BhavaLagna, BodyType.Name.SpecialLagna, bl_lon,0,0,0,0,0));
			std_grahas.Add (new BodyPosition (h, Body.Name.HoraLagna, BodyType.Name.SpecialLagna, hl_lon,0,0,0,0,0));
			std_grahas.Add (new BodyPosition (h, Body.Name.GhatiLagna, BodyType.Name.SpecialLagna, gl_lon, 0,0,0,0,0));
			std_grahas.Add (new BodyPosition (h, Body.Name.VighatiLagna, BodyType.Name.SpecialLagna, vl_lon,0,0,0,0,0));			


			return std_grahas;
		}
	}

}
