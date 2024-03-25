using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    [Serializable]
    public class MhoraSerializableOptions
    {
        protected void Constructor(System.Type ty, SerializationInfo info, StreamingContext context)
        {

            MemberInfo[] mi = FormatterServices.GetSerializableMembers(ty, context);
            for (int i = 0; i < mi.Length; i++)
            {
                FieldInfo fi = (FieldInfo)mi[i];
                //Console.WriteLine ("User Preferences: Reading {0}", fi);
                try
                {
                    fi.SetValue(this, info.GetValue(fi.Name, fi.FieldType));
                }
                catch
                {
                    //Console.WriteLine ("    Not found");
                }
            }
        }

        protected void GetObjectData(
            System.Type ty, SerializationInfo info, StreamingContext context)
        {
            MemberInfo[] mi = FormatterServices.GetSerializableMembers(ty, context);
            for (int i = 0; i < mi.Length; i++)
            {
                //Console.WriteLine ("User Preferences: Writing {0}", mi[i].Name);
                info.AddValue(mi[i].Name, ((FieldInfo)mi[i]).GetValue(this));
            }
        }
        static public string getExeDir()
        {

            Process oLocal = Process.GetCurrentProcess();
            ProcessModule oMain = oLocal.MainModule;
            string fileName = Path.GetDirectoryName(oMain.FileName);
            if (fileName[fileName.Length - 1] == '\\')
                fileName.Remove(fileName.Length - 1, 1);
            //Debug.WriteLine( string.Format("Exe launched from {0}", fileName), "GlobalOptions");
            return fileName;
        }

        static public string getOptsFilename()
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MhoraOptions.xml";
            //Debug.WriteLine( string.Format("Options stored at {0}", fileName), "GlobalOptions");
            return fileName;
        }
    }

}
