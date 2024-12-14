using org.transliteral.panchang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HinduPanchang hinduPanchang = new HinduPanchang();
            hinduPanchang.Compute();
        }
    }
}
