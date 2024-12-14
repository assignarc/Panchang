using System.Collections;

namespace org.transliteral.panchang
{
    public class PanchangaLocalMoments 
	{
		public double sunrise;
		public double sunset;
		public Basics.Weekday wday;
		public double sunrise_ut;
		public double[] kalas_ut;
		public double[] horas_ut;
		public ArrayList lagnas_ut = new ArrayList();
		public ZodiacHouseName lagna_zh;
		public int rahu_kala_index;
		public int gulika_kala_index;
		public int yama_kala_index;
		public int kala_base;
		public int hora_base;
		public int nakshatra_index_start;
		public int nakshatra_index_end;
		public int tithi_index_start;
		public int tithi_index_end;
		public int karana_index_start;
		public int karana_index_end;
		public int smyoga_index_start;
		public int smyoga_index_end;
	}

}

