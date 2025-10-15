using System.Collections;

namespace org.transliteral.panchang
{
    public class LocalMoments 
	{
		public double Sunrise;
		public double Sunset;
		public Weekday WeekDay;
		public double SunriseUT;
		public double[] KalasUT;
		public double[] HorasUT;
		public ArrayList LagnasUT = new ArrayList();
		public ZodiacHouseName LagnaZodiacHouse;
		public int RahuKalaIndex;
		public int GulikaKalaIndex;
		public int YamaKalaIndex;
		public int KalaBase;
		public int HoraBase;
		public int NakshatraIndexStart;
		public int NakshatraIndexEnd;
		public int TithiIndexStart;
		public int TithiIndexEnd;
		public int KaranaIndexStart;
		public int KaranaIndexEnd;
		public int SunMoonYogaIndexStart;
		public int SunMoonYogaIndexEnd;
		public HoraInfo HoraInfo;
	}

}

