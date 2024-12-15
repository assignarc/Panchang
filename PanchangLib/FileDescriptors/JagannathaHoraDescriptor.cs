using System;
using System.IO;
using System.Text.RegularExpressions;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Class deals with reading the JagannathaHoraDescriptor/Jhd file specification
    /// used by Jagannatha Hora
    /// </summary>
    /// 


    public class JagannathaHoraDescriptor : IFileToHoraInfo
    {
        private string fname;
        public JagannathaHoraDescriptor(string fileName)
        {
            fname = fileName;
        }
        private static int readIntLine(StreamReader sr)
        {
            String s = sr.ReadLine();
            return int.Parse(s);
        }
        private static void writeHMSInfoLine(StreamWriter sw, HMSInfo hi)
        {
            string q;
            if (hi.direction == Direction.NorthSouth && hi.degree >= 0)
                q = "";
            else if (hi.direction == Direction.NorthSouth)
                q = "-";
            else if (hi.direction == Direction.EastWest && hi.degree >= 0)
                q = "-";
            else q = "";
            int thour = hi.degree >= 0 ? hi.degree : -hi.degree;
            string w = q + thour.ToString() + "." + numToString(hi.minute) + numToString(hi.second) + "00";
            sw.WriteLine(w);
        }
        private static HMSInfo readHmsLineInfo(StreamReader sr, bool negate, Direction dir)
        {
            int h = 0, m = 0, s = 0;
            readHmsLine(sr, ref h, ref m, ref s);
            if (negate) h *= -1;
            return new HMSInfo(h, m, s, dir);
        }
        private static void readHmsLine(StreamReader sr, ref int hour, ref int minute, ref int second)
        {
            String s = sr.ReadLine();
            Regex re = new Regex("[0-9]*$");
            Match m = re.Match(s);
            String s2 = m.Value;

            if (s[0] == '|') s = new string(s.ToCharArray(1, s.Length - 1));
            double dhour = double.Parse(s);
            dhour = dhour < 0 ? Math.Ceiling(dhour) : Math.Floor(dhour);
            hour = (int)dhour;
            minute = int.Parse(s2.Substring(0, 2));
            double _second = 0.0;
            if (s2.Length > 5)
                _second = (double.Parse(s2.Substring(2, 4)) / 10000.0) * 60.0;
            second = (int)_second;
        }
        private static Moment readMomentLine(StreamReader sr)
        {
            int month = readIntLine(sr);
            int day = readIntLine(sr);
            int year = readIntLine(sr);

            int hour = 0, minute = 0, second = 0;
            readHmsLine(sr, ref hour, ref minute, ref second);
            return new Moment(year, month, day, hour, minute, second);
        }
        private static string numToString(int _n)
        {
            int n = _n < 0 ? -_n : _n;
            string s;
            if (n < 10) s = "0" + n.ToString();
            else s = n.ToString();
            return s;
        }
        private static void writeMomentLine(StreamWriter sw, Moment m)
        {
            sw.WriteLine(m.Month);
            sw.WriteLine(m.Day);
            sw.WriteLine(m.Year);

            sw.WriteLine(m.Hour.ToString() + "." + numToString(m.Minute) + numToString(m.Second) + "00");
        }
        public HoraInfo toHoraInfo()
        {
            StreamReader sr = File.OpenText(fname);
            Moment m = readMomentLine(sr);
            HMSInfo tz = readHmsLineInfo(sr, true, Direction.EastWest);
            HMSInfo lon = readHmsLineInfo(sr, true, Direction.EastWest);
            HMSInfo lat = readHmsLineInfo(sr, false, Direction.NorthSouth);
            HoraInfo hi = new HoraInfo(m, lat, lon, tz);
            hi.FileType = EFileType.JagannathaHora;
            //hi.name = File.fname;
            return hi;
        }
        public void ToFile(HoraInfo h)
        {
            StreamWriter sw = new StreamWriter(fname, false);
            writeMomentLine(sw, h.tob);
            writeHMSInfoLine(sw, h.tz);
            writeHMSInfoLine(sw, h.lon);
            writeHMSInfoLine(sw, h.lat);
            sw.WriteLine("0.000000");
            sw.Flush();
            sw.Close();
        }
    }

}
