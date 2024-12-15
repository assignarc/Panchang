namespace org.transliteral.panchang
{
    /// <summary>
	/// A list of nakshatras, and related helper functions
	/// </summary>
	public class Nakshatra28
    {
       
        private Nakshatra28Name m_nak;
        public Nakshatra28Name Value
        {
            get { return m_nak; }
            set { m_nak = value; }
        }
        public Nakshatra28(Nakshatra28Name nak)
        {
            m_nak = nak;
        }
        public int Normalize()
        {
            return Basics.Normalize_inc(1, 28, (int)this.Value);
        }
        public Nakshatra28 Add(int i)
        {
            int snum = Basics.Normalize_inc(1, 28, (int)this.Value + i - 1);
            return new Nakshatra28((Nakshatra28Name)snum);
        }
    }

}
