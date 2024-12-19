using System;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    [TypeConverter(typeof(OrderedGrahasConverter))]
    public class OrderedGrahas : ICloneable
    {
        public ArrayList grahas;
        public OrderedGrahas()
        {
            this.grahas = new ArrayList();
        }
        public override string ToString()
        {
            string s = "";
            foreach (BodyName bn in this.grahas)
                s += Body.ToShortString(bn) + " ";
            return s;
        }
        public object Clone()
        {
            return new OrderedGrahas
            {
                grahas = (ArrayList)this.grahas.Clone()
            };
           
        }
    }



}
