

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
		Body.Name[] avBodies = null;

		public Ashtakavarga(Horoscope _h, Division _dtype)
		{
			h = _h;
			dtype = _dtype;
			avBodies = new Body.Name[]
			{
				Body.Name.Sun, 
				Body.Name.Moon, 
				Body.Name.Mars,
				Body.Name.Mercury,
				Body.Name.Jupiter, 
				Body.Name.Venus,
				Body.Name.Saturn, 
				Body.Name.Lagna
			};
		}

		public void setKakshyaType (EKakshya k)
		{
			switch (k)
			{
			case EKakshya.EKStandard:
				avBodies = new Body.Name[]
				{
					Body.Name.Sun, 
					Body.Name.Moon, 
					Body.Name.Mars,
					Body.Name.Mercury, 
					Body.Name.Jupiter, 
					Body.Name.Venus,
					Body.Name.Saturn, 
					Body.Name.Lagna
				};
				break;
			case EKakshya.EKRegular:
				avBodies = new Body.Name[]
				{
					Body.Name.Saturn, 
					Body.Name.Jupiter, 
					Body.Name.Mars, 
					Body.Name.Sun,
					Body.Name.Venus, 
					Body.Name.Mercury, 
					Body.Name.Moon, 
					Body.Name.Lagna
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

		public int BodyToInt (Body.Name bodyName)
		{
			switch (bodyName)
			{
				case Body.Name.Sun: return 0;
				case Body.Name.Moon: return 1;
				case Body.Name.Mars: return 2;
				case Body.Name.Mercury: return 3;
				case Body.Name.Jupiter: return 4;
				case Body.Name.Venus: return 5;
				case Body.Name.Saturn: return 6;
				case Body.Name.Lagna: return 7;
				default: Trace.Assert(false, "Ashtakavarga:BodyToInt");
					return 0;
			}
		}
		public Body.Name[] GetBodies ()
		{
			return this.avBodies;
		}
		public int[] GetPav (Body.Name bodyName)
		{
			int[] ret = new int[12] {0,0,0,0,0,0,0,0,0,0,0,0};
			foreach (Body.Name inner in GetBodies())
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

			ZodiacHouse zl = h.GetPosition(Body.Name.Lagna).ToDivisionPosition(this.dtype).ZodiacHouse;

			foreach (Body.Name b in GetBodies())
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
			foreach (Body.Name b in GetBodies())
			{
				// Lagna's bindus are not included in SAV
				if (b == Body.Name.Lagna)
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

		public ZodiacHouseName[] GetBindus (Body.Name bodyName1, Body.Name bodyName2)
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
