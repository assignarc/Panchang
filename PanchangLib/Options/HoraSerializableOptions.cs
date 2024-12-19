using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace org.transliteral.panchang
{

    [Serializable]
    public class HoraSerializableOptions
    {
        protected void Constructor(System.Type ty, SerializationInfo info, StreamingContext context)
        {

            MemberInfo[] mi = FormatterServices.GetSerializableMembers(ty, context);
            for (int i = 0; i < mi.Length; i++)
            {
                FieldInfo fi = (FieldInfo)mi[i];
                Logger.Info(String.Format("User Preferences: Reading {0}", fi));
                try
                {
                    fi.SetValue(this, info.GetValue(fi.Name, fi.FieldType));
                }
                catch
                {
                    Logger.Info(String.Format("    Not found"));
                }
            }
        }

        protected void GetObjectData(
            System.Type ty, SerializationInfo info, StreamingContext context)
        {
            MemberInfo[] mi = FormatterServices.GetSerializableMembers(ty, context);
            for (int i = 0; i < mi.Length; i++)
            {
                Logger.Info(String.Format("User Preferences: Writing {0}", mi[i].Name));
                info.AddValue(mi[i].Name, ((FieldInfo)mi[i]).GetValue(this));
            }
        }
        static public string GetExeDir()
        {

            Process oLocal = Process.GetCurrentProcess();
            ProcessModule oMain = oLocal.MainModule;
            string fileName = Path.GetDirectoryName(oMain.FileName);
            if (fileName[fileName.Length - 1] == '\\')
                fileName.Remove(fileName.Length - 1, 1);
            //Debug.WriteLine( string.Format("Exe launched from {0}", fileName), "GlobalOptions");
            return fileName;
        }

        static public string GetOptsFilename()
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HoraOptions.xml";
            Logger.Info(string.Format("Options stored at {0}", fileName));
            return fileName;
        }
    }

}
