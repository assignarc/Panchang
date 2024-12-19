using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BaseDivision = org.transliteral.panchang.Division;
namespace org.transliteral.panchang.app
{
    [Serializable]
    [TypeConverter(typeof(DivisionConverter))]
    public class Division : BaseDivision, ICloneable
    {
        public Division(DivisionType _dtype) : base(_dtype) { }
        public Division(SingleDivision single) : base(single) { }
        public Division() : base() { }
        public static void CopyToClipboard(Division div)
        {
            MemoryStream mStr = new MemoryStream();
            BinaryWriter bStr = new BinaryWriter(mStr);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mStr, div);
            Clipboard.SetDataObject(mStr, false);
        }
        public static Division CopyFromClipboard()
        {
            try
            {
                MemoryStream mStr = (MemoryStream)Clipboard.GetDataObject().GetData(typeof(MemoryStream));
                BinaryReader bStr = new BinaryReader(mStr);
                BinaryFormatter formatter = new BinaryFormatter();
                Division div = (Division)formatter.Deserialize(bStr.BaseStream);
                return div;
            }
            catch
            {
                return null;
            }
        }
    }


}
