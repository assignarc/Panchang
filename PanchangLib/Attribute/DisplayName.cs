using System;

namespace org.transliteral.panchang
{
    public class DisplayName : Attribute
    {
        public string Text;
        public DisplayName(string _display)
        {
            this.Text = _display;
        }
    }
}
