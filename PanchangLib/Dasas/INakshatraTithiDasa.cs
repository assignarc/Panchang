

namespace org.transliteral.panchang
{
    public interface INakshatraTithiDasa: IDasa
	{
        BodyName LordOfTithi(Longitude l);
		double LengthOfDasa(BodyName plt);      // Length of a maha dasa
	}
}
