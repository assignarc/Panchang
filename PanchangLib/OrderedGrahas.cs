using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        override public string ToString()
        {
            string s = "";
            foreach (Body.Name bn in this.grahas)
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
