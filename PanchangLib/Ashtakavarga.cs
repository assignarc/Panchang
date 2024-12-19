

using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace org.transliteral.panchang
{
	/// <summary>
	/// Summary description for Ashtakavarga.
	/// </summary>
	public class Ashtakavarga
	{
		
		Horoscope h;
		Division dtype;
		BodyName[] avBodies = null;

		public Ashtakavarga(Horoscope _h, Division _dtype)
		{
			h = _h;
			dtype = _dtype;
			avBodies = new BodyName[]
			{
				BodyName.Sun, 
				BodyName.Moon, 
				BodyName.Mars,
				BodyName.Mercury,
				BodyName.Jupiter, 
				BodyName.Venus,
				BodyName.Saturn, 
				BodyName.Lagna
			};
		}

		public void setKakshyaType (EKakshya k)
		{
			switch (k)
			{
			case EKakshya.EKStandard:
				avBodies = new BodyName[]
				{
					BodyName.Sun, 
					BodyName.Moon, 
					BodyName.Mars,
					BodyName.Mercury, 
					BodyName.Jupiter, 
					BodyName.Venus,
					BodyName.Saturn, 
					BodyName.Lagna
				};
				break;
			case EKakshya.EKRegular:
				avBodies = new BodyName[]
				{
					BodyName.Saturn, 
					BodyName.Jupiter, 
					BodyName.Mars, 
					BodyName.Sun,
					BodyName.Venus, 
					BodyName.Mercury, 
					BodyName.Moon, 
					BodyName.Lagna
				};
				break;

			}

		}

		public int[][] BindusSun()
		{
			int[][] bindus =
			{
				new int[] { 1, 2, 4, 7, 8, 9, 10, 11 },
				new int[] { 3, 6, 10, 11 },
				new int[] { 1, 2, 4, 7, 8, 9, 10, 11 },
				new int[] { 3, 5, 6, 9, 10, 11, 12 },
				new int[] { 5, 6, 9, 11 },
				new int[] { 6, 7, 12 },
				new int[] { 1, 2, 4, 7, 8, 9, 10, 11 },
				new int[] { 3, 4, 6, 10, 11, 12 }
			};
			return bindus;
		}
		public int[][] BindusMoon()
		{
			int[][] bindus =
			{
				new int[] { 3, 6, 7, 8, 10, 11 },
				new int[] { 1, 3, 6, 7, 9, 10, 11 },
				new int[] { 2, 3, 5, 6, 10, 11 },
				new int[] { 1, 3, 4, 5, 7, 8, 10, 11},
				new int[] { 1, 2, 4, 7, 8, 10, 11 },
				new int[] { 3, 4, 5, 7, 9, 10, 11 },
				new int[] { 3, 5, 6, 11 },
				new int[] { 3, 6, 10, 11 }
			};
			return bindus;
		}
				
		public int[][] BindusMars()
		{
			int[][] bindus =
			{
				new int[] { 3, 5, 6, 10, 11},
				new int[] { 3, 6, 11 },
				new int[] { 1, 2, 4, 7, 8, 10, 11 },
				new int[] { 3, 5, 6, 11 },
				new int[] { 6, 10, 11, 12 },
				new int[] { 6, 8, 11, 12 },
				new int[] { 1, 4, 7, 8, 9, 10, 11 },
				new int[] { 1, 3, 6, 10, 11},
			};
			return bindus;
		}

		public int[][] BindusMercury()
		{
			int[][] bindus =
			{
				new int[] { 5, 6, 9, 11, 12 },
				new int[] { 2, 4, 6, 8, 10, 11 },
				new int[] { 1, 2, 4, 7, 8, 9, 10, 11 },
				new int[] { 1, 3, 5, 6, 9, 10, 11, 12 },
				new int[] { 6, 8, 11, 12 },
				new int[] { 1, 2, 3, 4, 5, 8, 9, 11 },
				new int[] { 1, 2, 4, 7, 8, 9, 10, 11 },
				new int[] { 1, 2, 4, 6, 8, 10, 11 },
			};
			return bindus;
		}

		public int[][] BindusJupiter()
		{
			int[][] bindus =
			{
				new int[] { 1, 2, 3, 4, 7, 8, 9, 10, 11 },
				new int[] { 2, 5, 7, 9, 11 },
				new int[] { 1, 2, 4, 7, 8, 10, 11 },
				new int[] { 1, 2, 4, 5, 6, 9, 10, 11 },
				new int[] { 1, 2, 3, 4, 7, 8, 10, 11 },
				new int[] { 2, 5, 6, 9, 10, 11 },
				new int[] { 3, 5, 6, 12 },
				new int[] { 1, 2, 4, 5, 6, 7, 9, 10, 11},
			};
			return bindus;
		}

		public int[][] BindusVenus()
		{
			int[][] bindus =
			{
				new int[] { 8, 11, 12 },
				new int[] { 1, 2, 3, 4, 5, 8, 9, 11, 12 },
				new int[] { 3, 4, 6, 9, 11, 12 },
				new int[] { 3, 5, 6, 9, 11 },
				new int[] { 5, 8, 9, 10, 11 },
				new int[] { 1, 2, 3, 4, 5, 8, 9, 10, 11 },
				new int[] { 3, 4, 5, 8, 9, 10, 11 },
				new int[] { 1, 2, 3, 4, 5, 8, 9, 11 },
			};
			return bindus;
		}

		public int[][] BindusSaturn()
		{
			int[][] bindus =
			{
				new int[] { 1, 2, 4, 7, 8, 10, 11 },
				new int[] { 3, 6, 11 },
				new int[] { 3, 5, 6, 10, 11, 12 },
				new int[] { 6, 8, 9, 10, 11, 12 },
				new int[] { 5, 6, 11, 12 },
				new int[] { 6, 11, 12 },
				new int[] { 3, 5, 6, 11 },
				new int[] { 1, 3, 4, 6, 10, 11 },
			};
			return bindus;
		}

		public int[][] BindusLagna()
		{
			int[][] bindus =
			{
				new int[] { 3, 4, 6, 10, 11, 12 },
				new int[] { 3, 6, 10, 11, 12 },
				new int[] { 1, 3, 6, 10, 11 },
				new int[] { 1, 2, 4, 6, 8, 10, 11 },
				new int[] { 1, 2, 4, 5, 6, 7, 9, 10, 11 },
				new int[] { 1, 2, 3, 4, 5, 8, 9 },
				new int[] { 1, 3, 4, 6, 10, 11 },
				new int[] { 3, 6, 10, 11 },
			};
			return bindus;
		}

		public int BodyToInt (BodyName bodyName)
		{
			switch (bodyName)
			{
				case BodyName.Sun: return 0;
				case BodyName.Moon: return 1;
				case BodyName.Mars: return 2;
				case BodyName.Mercury: return 3;
				case BodyName.Jupiter: return 4;
				case BodyName.Venus: return 5;
				case BodyName.Saturn: return 6;
				case BodyName.Lagna: return 7;
				default: Trace.Assert(false, "Ashtakavarga:BodyToInt");
					return 0;
			}
		}
		public BodyName[] GetBodies ()
		{
			return this.avBodies;
		}
		public int[] GetPav (BodyName bodyName)
		{
			int[] ret = new int[12] {0,0,0,0,0,0,0,0,0,0,0,0};
			foreach (BodyName inner in GetBodies())
			{
				foreach (ZodiacHouseName zh in this.GetBindus(bodyName, inner))
				{
					ret[((int)zh)-1]++;
				}
			}
			return ret;
		}

        public int[] GetSavRao()
		{
			int[] sav = new int[12]{0,0,0,0,0,0,0,0,0,0,0,0};

			ZodiacHouse zl = h.GetPosition(BodyName.Lagna).ToDivisionPosition(this.dtype).ZodiacHouse;

			foreach (BodyName b in GetBodies())
			{

				int[] pav = this.GetPav(b);
				Debug.Assert(pav.Length == 12, "Internal error: Pav didn't have 12 entries");
				
				ZodiacHouse zb = h.GetPosition(b).ToDivisionPosition(this.dtype).ZodiacHouse;

				for (int i=0; i<12; i++)
				{
					ZodiacHouse zi = new ZodiacHouse((ZodiacHouseName)i+1);
					int rasi = zb.NumHousesBetween(zi);
					rasi = (int)zl.Add(rasi).Value;
					sav[rasi-1] += pav[i];
				}
			}
			return sav;
		}

		public int[] GetSav ()
		{
			int[] sav = new int[12]{0,0,0,0,0,0,0,0,0,0,0,0};
			foreach (BodyName b in GetBodies())
			{
				// Lagna's bindus are not included in SAV
				if (b == BodyName.Lagna)
					continue;

				int[] pav = this.GetPav(b);
				Debug.Assert(pav.Length == 12, "Internal error: Pav didn't have 12 entries");
				for (int i=0; i<12; i++)
				{
					sav[i] += pav[i];
				}
			}
			return sav;
		}

		public ZodiacHouseName[] GetBindus (BodyName bodyName1, BodyName bodyName2)
		{
			int[][][] allBindus = new int[8][][];
			allBindus [0] = BindusSun();
			allBindus [1] = BindusMoon();
			allBindus [2] = BindusMars();
			allBindus [3] = BindusMercury();
			allBindus [4] = BindusJupiter();
			allBindus [5] = BindusVenus();
			allBindus [6] = BindusSaturn();
			allBindus [7] = BindusLagna();

			ArrayList al = new ArrayList();

			ZodiacHouse zh = h.GetPosition(bodyName2).ToDivisionPosition(this.dtype).ZodiacHouse;
			foreach (int i in allBindus[BodyToInt(bodyName1)][BodyToInt(bodyName2)])
			{
				al.Add(zh.Add(i).Value);
			}
			return (ZodiacHouseName[])al.ToArray(typeof(ZodiacHouseName));

		}


	}
}
