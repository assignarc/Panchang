using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
    /// <summary>
	/// An interface which should be used by those whose properties
	/// should be updateable using the HoraOptions form. 
	/// </summary>
	public interface IUpdateable
    {
        Object GetOptions();
        Object SetOptions(Object a);
    }
}
