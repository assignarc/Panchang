using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    public class Logger
    {
        public enum Destination : int
        {
            CONSOLE =0
        }
        public enum Level : int
        {
            INFO = 1,
            TRACE = 2,
            DEBUG = 4,
            ERROR = 8,
            FATAL = 16,
        }
        private static Destination _destination = Destination.CONSOLE;
        private static Level _destinationLevel = Level.DEBUG;

        public static void Log(Level _level, string message)
        {
            if (_level < _destinationLevel)
                return;
            switch (_destination)
            {
                case Destination.CONSOLE:
                   System.Diagnostics.Debug.WriteLine(message); break;
                default:
                   System.Diagnostics.Debug.WriteLine(message); break;
            }
        }
        public static void Debug(string message) => Log(Level.DEBUG, message);
        public static void Info(string message) => Log(Level.INFO, message);
        public static void Trace(string message) => Log(Level.TRACE, message);
        public static void Error(string message) => Log(Level.ERROR, message);
        public static void Fatal(string message) => Log(Level.FATAL, message);

    }
}
