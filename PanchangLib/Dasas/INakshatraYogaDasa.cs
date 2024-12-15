

namespace org.transliteral.panchang
{
    public interface INakshatraYogaDasa: IDasa
	{
        Body.Name LordOfYoga(Longitude l);
		double LengthOfDasa(Body.Name plt);
	}
}
