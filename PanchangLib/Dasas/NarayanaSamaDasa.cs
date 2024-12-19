

using System;

namespace org.transliteral.panchang
{
    public class NarayanaSamaDasa: NarayanaDasa, IDasa
	{
		public NarayanaSamaDasa (Horoscope _h) :base (_h)
		{
			this.bSama = true;
		}
		public new String Description ()
		{
			return "Narayana Sama Dasa for "
				+ options.Division.ToString() 
				+ " seeded from " + options.SeedRasi.ToString();
		}
	}
}
