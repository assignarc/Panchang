using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public class YogasParseException : Exception
    {
        public string status;
        public YogasParseException() : base()
        {
            status = null;
        }
        public YogasParseException(string message)
        {
            status = message;
        }
    }
}
