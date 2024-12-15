using System;

namespace org.transliteral.panchang
{
    /// <summary>
	/// An interface which should be used by those whose properties
	/// should be updateable using the HoraOptions form. 
	/// </summary>
	public interface IUpdateable
    {
        object Options { get; }

        object SetOptions(object a);
    }
}
