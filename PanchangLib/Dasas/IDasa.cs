

using System;
using System.Collections;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Interface implemented by all IDasa functions. At the moment the method of
    /// implementation for any level below AntarDasa is assumed to be the same.
    /// </summary>
    public interface IDasa: IUpdateable
	{
		double paramAyus();
		ArrayList Dasa(int cycle);
		ArrayList AntarDasa (DasaEntry pdi);
		string EntryDescription (DasaEntry de, Moment start, Moment end);
		String Description ();
		void DivisionChanged (Division d);
		void recalculateOptions ();
	}
}
