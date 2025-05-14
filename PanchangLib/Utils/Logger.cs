namespace org.transliteral.panchang
{
    public class Logger
    {
        public enum Destination : int
        {
            CONSOLE = 0
        }
        public enum Level : int
        {
            INFO = 0,
            TRACE = 2,
            DEBUG = 4,
            ERROR = 8,
            FATAL = 16
        }
        private static readonly Destination _destination = Destination.CONSOLE;
        private static readonly Level _destinationLevel = Level.INFO;

        public static void Log(string message, Level _level = Level.INFO)
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
        public static void Debug(string message) => Log(message, Level.DEBUG);
        public static void Info(string message) => Log(message, Level.INFO);
        public static void Trace(string message) => Log(message, Level.TRACE);
        public static void Error(string message) => Log(message, Level.ERROR);
        public static void Fatal(string message) => Log(message, Level.FATAL);

    }
}
