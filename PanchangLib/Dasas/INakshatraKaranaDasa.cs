

namespace org.transliteral.panchang
{
    public interface INakshatraKaranaDasa : IDasa
	{
		Body.Name lordOfKarana (Longitude l);
		double lengthOfDasa (Body.Name plt);
	}
}
