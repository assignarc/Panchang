using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    /// <summary>
	/// A list of nakshatras, and related helper functions
	/// </summary>
	public class Nakshatra28
    {
       
        private Nakshatra28Name m_nak;
        public Nakshatra28Name value
        {
            get { return m_nak; }
            set { m_nak = value; }
        }
        public Nakshatra28(Nakshatra28Name nak)
        {
            m_nak = nak;
        }
        public int normalize()
        {
            return Basics.Normalize_inc(1, 28, (int)this.value);
        }
        public Nakshatra28 add(int i)
        {
            int snum = Basics.Normalize_inc(1, 28, (int)this.value + i - 1);
            return new Nakshatra28((Nakshatra28Name)snum);
        }
    }

}
