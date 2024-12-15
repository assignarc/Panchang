

namespace org.transliteral.panchang
{
    public interface INakshatraTithiDasa: IDasa
	{
        Body.Name LordOfTithi(Longitude l);
		double LengthOfDasa(Body.Name plt);      // Length of a maha dasa
	}
}
