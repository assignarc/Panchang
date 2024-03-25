

namespace org.transliteral.panchang
{
    public interface INakshatraYogaDasa: IDasa
	{
		Body.Name lordOfYoga (Longitude l);
		double lengthOfDasa (Body.Name plt);
	}
}
