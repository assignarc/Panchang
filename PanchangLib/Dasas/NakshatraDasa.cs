

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Base class to be implemented by Vimsottari/Ashtottari like dasas
    /// </summary>
    abstract public class NakshatraDasa: Dasa
	{
        abstract public Object Options { get; }

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
        protected ArrayList AntarDasa(DasaEntry pdi)
		{
			int numItems = common.NumberOfDasaItems();
			ArrayList ditems = new ArrayList (numItems);
			DasaEntry curr = new DasaEntry (pdi.graha, pdi.startUT, 0, pdi.level + 1, "");
			for (int i=0; i<numItems; i++) 
			{
				double dlength = (common.LengthOfDasa(curr.graha) / common.ParamAyus()) * pdi.dasaLength;
				string desc = pdi.shortDesc + " " + Body.ToShortString (curr.graha);
				DasaEntry di = new DasaEntry (curr.graha, curr.startUT, dlength, curr.level, desc);
				ditems.Add (di);
				curr = common.NextDasaLord (di);
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
        protected ArrayList Dasa(Longitude lon, int offset, int cycle)
        {
            ArrayList ditems = new ArrayList(common.NumberOfDasaItems());
			Nakshatra n = (lon.ToNakshatra()).Add (offset);
			Body.Name g = common.LordOfNakshatra(n);
			double perc_traversed = lon.PercentageOfNakshatra();
			double start = (cycle * common.ParamAyus()) - (perc_traversed / 100.0 * common.LengthOfDasa(g));
            Logger.Info(String.Format("{0} {1} {2}", common.LengthOfDasa(g), perc_traversed, start));

			// Calculate a "seed" dasaItem, to make use of the AntarDasa function
			DasaEntry di = new DasaEntry (g, start, common.ParamAyus(), 0, "");
			return AntarDasa (di);
		}

        protected ArrayList TithiDasa(Longitude lon, int offset, int cycle)
		{
			//ArrayList ditems = new ArrayList(tithiCommon.numberOfDasaItems());
			lon = lon.Add(new Longitude((cycle*(offset-1))*12.0));
			Body.Name g = tithiCommon.LordOfTithi(lon);

			double tithiOffset = lon.Value;
			while (tithiOffset >= 12.0) tithiOffset -= 12.0;
			double perc_traversed = tithiOffset / 12.0;
			double start = (cycle * tithiCommon.ParamAyus()) - (perc_traversed * tithiCommon.LengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, tithiCommon.ParamAyus(), 0, "");
			return AntarDasa (di);
		}

        protected ArrayList YogaDasa(Longitude lon, int offset, int cycle)
		{
			lon = lon.Add(new Longitude((cycle*(offset-1))*(360.0/27.0)));
			Body.Name g = yogaCommon.LordOfYoga(lon);

			double yogaOffset = lon.ToSunMoonYogaOffset();
			double perc_traversed = yogaOffset / (360.0/27.0);
			double start = (cycle * common.ParamAyus()) - (perc_traversed * common.LengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, common.ParamAyus(), 0, "");
			return AntarDasa (di);
		}

        protected ArrayList KaranaDasa(Longitude lon, int offset, int cycle)
		{
			lon = lon.Add(new Longitude((cycle*(offset-1))*(360.0/60.0)));
			Body.Name g = karanaCommon.LordOfKarana(lon);

			double karanaOffset = lon.ToKaranaOffset();
			double perc_traversed = karanaOffset / (360.0/60.0);
			double start = (cycle * common.ParamAyus()) - (perc_traversed * common.LengthOfDasa(g));
			DasaEntry di = new DasaEntry(g, start, common.ParamAyus(), 0, "");
			return AntarDasa (di);
		}

        public void RecalculateOptions()
		{
		}
	}
}
