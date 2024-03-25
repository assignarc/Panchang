using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    [TypeConverter(typeof(OrderedZodiacHousesConverter))]
    public class OrderedZodiacHouses : ICloneable
    {
        public ArrayList houses;
        public OrderedZodiacHouses()
        {
            this.houses = new ArrayList();
        }
        override public string ToString()
        {
            string s = "";
            ZodiacHouseName[] names = (ZodiacHouseName[])houses.ToArray(typeof(ZodiacHouseName));
            foreach (ZodiacHouseName zn in names)
                s += zn.ToString() + " ";
            return s;
        }
        public object Clone()
        {
            OrderedZodiacHouses oz = new OrderedZodiacHouses();
            oz.houses = (ArrayList)this.houses.Clone();
            return oz;
        }
    }

}
