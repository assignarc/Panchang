using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    /// <summary>
    /// Class deals with reading the Jhd file specification
    /// used by Jagannatha Hora
    /// </summary>
    /// 

    public class Mhd : IFileToHoraInfo
    {
        private string fname;
        public Mhd(string fileName)
        {
            fname = fileName;
        }
        public HoraInfo toHoraInfo()
        {
            try
            {
                HoraInfo hi = new HoraInfo();
                FileStream sOut;
                sOut = new FileStream(fname, FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                hi = (HoraInfo)formatter.Deserialize(sOut);
                sOut.Close();
                return hi;
            }
            catch
            {
                LogMessage("Unable to read file");
                return new HoraInfo();
            }
        }

        public void ToFile(HoraInfo hi)
        {
            FileStream sOut = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(sOut, hi);
            sOut.Close();
        }

        private void LogMessage(string message)
        {
            Console.WriteLine(message); 
        }


    }

}
