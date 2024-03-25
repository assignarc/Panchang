using System.IO;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace org.transliteral.panchang
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOrderAttribute : Attribute
    {
        //
        // Simple attribute to allow the order of a property to be specified
        //
        private int _order;
        public PropertyOrderAttribute(int order)
        {
            _order = order;
        }

        public int Order
        {
            get
            {
                return _order;
            }
        }
    }

}
