using org.transliteral.panchang;
using System.Windows.Forms;
using BaseFindYogas = org.transliteral.panchang.FindYogas;

namespace org.transliteral.panchang.app
{

    public class FindYogas : BaseFindYogas
    {
        public FindYogas(Horoscope _h, Division __dtype) : base(_h, __dtype)
        {

        }

        public void LogMessage(string mesage)
        {
            MessageBox.Show(mesage);
        }
    }


}
