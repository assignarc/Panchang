

namespace org.transliteral.panchang
{
    /// <summary>
    /// A helper interface for all Vimsottari/Ashtottari Dasa like dasas
    /// </summary>
    public interface INakshatraDasa: IDasa
	{
        int NumberOfDasaItems();                 // Number of dasas for 1 cycle
        DasaEntry NextDasaLord(DasaEntry di);      // Order of Dasas
        double LengthOfDasa (Body.Name plt);      // Length of a maha dasa
		Body.Name LordOfNakshatra(Nakshatra n);   // Dasa lord of given nakshatra
	}
}
