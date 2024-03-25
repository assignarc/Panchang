

namespace org.transliteral.panchang
{
    /// <summary>
    /// A helper interface for all Vimsottari/Ashtottari Dasa like dasas
    /// </summary>
    public interface INakshatraDasa: IDasa
	{
		int numberOfDasaItems ();                 // Number of dasas for 1 cycle
		DasaEntry nextDasaLord (DasaEntry di);      // Order of Dasas
		double lengthOfDasa (Body.Name plt);      // Length of a maha dasa
		Body.Name lordOfNakshatra(Nakshatra n);   // Dasa lord of given nakshatra
	}
}
