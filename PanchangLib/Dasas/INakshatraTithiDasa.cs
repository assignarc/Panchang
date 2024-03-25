

namespace org.transliteral.panchang
{
    public interface INakshatraTithiDasa: IDasa
	{
		Body.Name lordOfTithi (Longitude l);
		double lengthOfDasa (Body.Name plt);      // Length of a maha dasa
	}
}
