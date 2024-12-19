

namespace org.transliteral.panchang
{
    public interface INakshatraKaranaDasa : IDasa
	{
        BodyName LordOfKarana(Longitude l);
		double LengthOfDasa(BodyName plt);
	}
}
