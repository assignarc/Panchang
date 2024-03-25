using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    
      /// <summary>
     /// Errors found withitn the unmanaged Swiss Ephemeris Library
     /// </summary>
    public class SwephException : Exception
    {
        public string status;
        public SwephException() : base()
        {
            status = null;
        }
        public SwephException(string message)
        {
            status = message;
        }
    }
}
