using System;

namespace org.transliteral.panchang
{
    public class VisibleAttribute : Attribute
    {
        public string Text;
        public VisibleAttribute(string _display)
        {
            this.Text = _display;
        }
    }
}
