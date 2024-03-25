

using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using org.transliteral.panchang;
using BaseFindYogas = org.transliteral.panchang.FindYogas;

namespace mhora
{
	
	public class FindYogas : BaseFindYogas
	{	
		public FindYogas(Horoscope _h, Division __dtype) : base(_h,__dtype)
		{
			
		}

		public override void LogMessage(string mesage)
		{
			MessageBox.Show(mesage);
        }
    }


}
