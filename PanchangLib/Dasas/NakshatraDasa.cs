

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Base class to be implemented by Vimsottari/Ashtottari like dasas
    /// </summary>
    abstract public class NakshatraDasa: Dasa
	{
		abstract public Object GetOptions ();
		abstract public Object SetOptions (Object a);
		protected INakshatraDasa common;
		protected INakshatraTithiDasa tithiCommon;
		protected INakshatraYogaDasa yogaCommon;
		protected INakshatraKaranaDasa karanaCommon;

		/// <summary>
		/// Returns the antardasas
		/// </summary>
		/// <param name="pdi">The current dasa item whose antardasas should be calculated</param>
		/// <returns></returns>
		protected ArrayList _AntarDasa (DasaEntry pdi)
		{
			int numItems = common.numberOfDasaItems();
			ArrayList ditems = new ArrayList (numItems);
			DasaEntry curr = new DasaEntry (pdi.graha, pdi.startUT, 0, pdi.level + 1, "");
			for (int i=0; i<numItems; i++) 
			{
				double dlength = (common.lengthOfDasa (curr.graha) / common.paramAyus()) * pdi.dasaLength;
				string desc = pdi.shortDesc + " " + Body.ToShortString (curr.graha);
				DasaEntry di = new DasaEntry (curr.graha, curr.startUT, dlength, curr.level, desc);
				ditems.Add (di);
				curr = common.nextDasaLord (di);
				curr.startUT = di.startUT + dlength;
			}
			return ditems;
		}

		/// <summary>
		/// Given a Lontigude, a Nakshatra Offset and Cycle number, calculate the maha dasa
		/// </summary>
		/// <param name="lon">The seed longitude (eg. Moon for Utpanna)</param>
		/// <param name="offset">The seed start (eg. 5 for Utpanna)</param>
		/// <param name="cycle">The cycle number. eg which 120 year cycle? 0 for "current"</param>
		/// <returns></returns>
		protected ArrayList _Dasa (Longitude lon, int offset, int cycle) 
		{
			ArrayList ditems = new ArrayList (common.numberOfDasaItems());
			Nakshatra n = (lon.toNakshatra()).add (offset);
			Body.Name g = common.lordOfNakshatra (n);
			double perc_traversed = lon.percentageOfNakshatra();
			double start = (cycle * common.paramAyus()) - (perc_traversed / 100.0 * common.lengthOfDasa(g));
			//System.Console.WriteLine ("{0} {1} {2}", common.lengthOfDasa(g), perc_traversed, start);

			// Calculate a "seed" dasaItem, to make use of the AntarDasa function
			DasaEntry di = new DasaEntry (g, start, common.paramAyus(), 0, "");
			return _AntarDasa (di);
		}

		protected ArrayList _TithiDasa (Longitude lon, int offset, int cycle)
		{
			//ArrayList ditems = new ArrayList(tithiCommon.numberOfDasaItems());
			lon = lon.add(new Longitude((cycle*(offset-1))*12.0));
			Body.Name g = tithiCommon.lordOfTithi(lon);

			double tithiOffset = lon.value;
			while (tithiOffset >= 12.0) tithiOffset -= 12.0;
			double perc_traversed = tithiOffset / 12.0;
			double start = (cycle * tithiCommon.paramAyus()) - (perc_traversed * tithiCommon.lengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, tithiCommon.paramAyus(), 0, "");
			return _AntarDasa (di);
		}

		protected ArrayList _YogaDasa (Longitude lon, int offset, int cycle)
		{
			lon = lon.add(new Longitude((cycle*(offset-1))*(360.0/27.0)));
			Body.Name g = yogaCommon.lordOfYoga(lon);

			double yogaOffset = lon.toSunMoonYogaOffset();
			double perc_traversed = yogaOffset / (360.0/27.0);
			double start = (cycle * common.paramAyus()) - (perc_traversed * common.lengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, common.paramAyus(), 0, "");
			return _AntarDasa (di);
		}

		protected ArrayList _KaranaDasa (Longitude lon, int offset, int cycle)
		{
			lon = lon.add(new Longitude((cycle*(offset-1))*(360.0/60.0)));
			Body.Name g = karanaCommon.lordOfKarana(lon);

			double karanaOffset = lon.toKaranaOffset();
			double perc_traversed = karanaOffset / (360.0/60.0);
			double start = (cycle * common.paramAyus()) - (perc_traversed * common.lengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, common.paramAyus(), 0, "");
			return _AntarDasa (di);
		}
		
		public void recalculateOptions ()
		{
		}
	}
}
