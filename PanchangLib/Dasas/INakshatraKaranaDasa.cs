

namespace org.transliteral.panchang
{
    public interface INakshatraKaranaDasa : IDasa
	{
        Body.Name LordOfKarana(Longitude l);
		double LengthOfDasa(Body.Name plt);
	}
}
