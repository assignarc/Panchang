using System;
using System.Collections;
using System.ComponentModel;

namespace org.transliteral.panchang
{
    [Serializable]
    [TypeConverter(typeof(DivisionConverter))]
    public class Division : ICloneable
    {
        [Serializable]
        [TypeConverter(typeof(SingleDivisionConverter))]
        public class SingleDivision : ICloneable
        {
            private DivisionType mDtype;
            private int mNumParts;
            public DivisionType Varga
            {
                get { return mDtype; }
                set { mDtype = value; }
            }
            public int NumParts
            {
                get { return mNumParts; }
                set { mNumParts = value; }
            }
            public SingleDivision(DivisionType _dtype, int _numParts)
            {
                mDtype = _dtype;
                mNumParts = _numParts;
            }
            public SingleDivision(DivisionType _dtype)
            {
                mDtype = _dtype;
            }
            public SingleDivision()
            {
                mDtype = DivisionType.Rasi;
                mNumParts = 1;
            }
            public override string ToString()
            {
                return this.mDtype.ToString() + " " + mNumParts.ToString();
            }
            public object Clone()
            {
                return new Division.SingleDivision(this.Varga, this.NumParts);
            }
        }

        private SingleDivision[] mMultipleDivisions = null;

        [@DisplayName("Composite Division")]
        public SingleDivision[] MultipleDivisions
        {
            get { return this.mMultipleDivisions; }
            set { this.mMultipleDivisions = value; }
        }
        public Division(DivisionType _dtype)
        {
            this.mMultipleDivisions = new SingleDivision[] { new SingleDivision(_dtype) };
        }
        public Division(Division.SingleDivision single)
        {
            this.mMultipleDivisions = new SingleDivision[] { single };
        }
        public Division()
        {
            this.mMultipleDivisions = new SingleDivision[] { new SingleDivision(DivisionType.Rasi) };
        }
        public override string ToString()
        {
            return Basics.NumPartsInDivisionString(this);
        }
        public object Clone()
        {
            Division dRet = new Division();
            ArrayList al = new ArrayList();
            foreach (SingleDivision dSingle in this.MultipleDivisions)
            {
                al.Add(dSingle.Clone());
            }
            dRet.MultipleDivisions = (SingleDivision[])al.ToArray(typeof(Division.SingleDivision));
            return dRet;
        }
        public override bool Equals(object obj)
        {
            if (obj is Division)
                return (this == (Division)obj);
            else
                return base.Equals(obj);
        }

        public static bool operator !=(Division d1, Division d2)
        {
            return (!(d1 == d2));
        }
        public static bool operator ==(Division d1, Division d2)
        {
            if (d1 is null && d2 is null)
                return true;

            if (d1 is null || d2 is null)
                return false;

            if (d1.MultipleDivisions.Length != d2.MultipleDivisions.Length)
                return false;

            for (int i = 0; i < d1.MultipleDivisions.Length; i++)
            {
                if (d1.MultipleDivisions[i].Varga != d2.MultipleDivisions[i].Varga ||
                    d1.MultipleDivisions[i].NumParts != d2.MultipleDivisions[i].NumParts)
                    return false;
            }
            return true;
        }

       
    }


}
