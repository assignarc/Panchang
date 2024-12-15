using System;

namespace org.transliteral.panchang
{
    public class YogasParseException : Exception
    {
        public string Status;
        public YogasParseException() : base()
        {
            Status = null;
        }
        public YogasParseException(string message)
        {
            Status = message;
        }
    }
}
