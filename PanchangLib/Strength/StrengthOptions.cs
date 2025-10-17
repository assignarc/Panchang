using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    [Serializable]
    public class StrengthOptions : SerializableOptions, ISerializable, ICloneable
    {
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        GrahaStrength[] mColord = null;
        GrahaStrength[] mNaisargikaDasaGraha = null;
        GrahaStrength[] mKarakaKendradiGrahaDasaGraha = null;
        GrahaStrength[] mKarakaKendradiGrahaDasaColord = null;
        RasiStrength[] mNavamsaDasaRasi = null;
        RasiStrength[] mMoolaDasaRasi = null;
        RasiStrength[] mNarayanaDasaRasi = null;
        RasiStrength[] mNaisargikaDasaRasi = null;
        RasiStrength[] mKarakaKendradiGrahaDasaRasi = null;

        public object Clone()
        {
            return new StrengthOptions
            {
                Colord = (GrahaStrength[])this.Colord.Clone(),
                NaisargikaDasaGraha = (GrahaStrength[])this.NaisargikaDasaGraha.Clone(),
                NavamsaDasaRasi = (RasiStrength[])this.NavamsaDasaRasi.Clone(),
                MoolaDasaRasi = (RasiStrength[])this.MoolaDasaRasi.Clone(),
                NarayanaDasaRasi = (RasiStrength[])this.NarayanaDasaRasi.Clone(),
                NaisargikaDasaRasi = (RasiStrength[])this.NaisargikaDasaRasi.Clone()
            };
        }

        public object Copy(object o)
        {
            StrengthOptions so = (StrengthOptions)o;
            this.Colord = (GrahaStrength[])so.Colord.Clone();
            this.NaisargikaDasaGraha = (GrahaStrength[])so.NaisargikaDasaGraha.Clone();
            this.NavamsaDasaRasi = (RasiStrength[])so.NavamsaDasaRasi.Clone();
            this.MoolaDasaRasi = (RasiStrength[])so.MoolaDasaRasi.Clone();
            this.NarayanaDasaRasi = (RasiStrength[])so.NarayanaDasaRasi.Clone();
            this.NaisargikaDasaRasi = (RasiStrength[])so.NaisargikaDasaRasi.Clone();
            return this.Clone();
        }

        [Category("Co-Lord Strengths")]
        [Visible("Graha Strength")]
        public GrahaStrength[] Colord
        {
            get { return mColord; }
            set { mColord = value; }
        }

        [Category("Naisargika Dasa Strengths")]
        [Visible("Graha Strengths")]
        public GrahaStrength[] NaisargikaDasaGraha
        {
            get { return mNaisargikaDasaGraha; }
            set { mNaisargikaDasaGraha = value; }
        }

        [Category("Naisargika Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public RasiStrength[] NaisargikaDasaRasi
        {
            get { return mNaisargikaDasaRasi; }
            set { mNaisargikaDasaRasi = value; }
        }

        [Category("Navamsa Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public RasiStrength[] NavamsaDasaRasi
        {
            get { return mNavamsaDasaRasi; }
            set { mNavamsaDasaRasi = value; }
        }

        [Category("Moola Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public RasiStrength[] MoolaDasaRasi
        {
            get { return mMoolaDasaRasi; }
            set { mMoolaDasaRasi = value; }
        }

        [Category("Narayana Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public RasiStrength[] NarayanaDasaRasi
        {
            get { return mNarayanaDasaRasi; }
            set { mNarayanaDasaRasi = value; }
        }

        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("Rasi Strengths")]
        public RasiStrength[] KarakaKendradiGrahaDasaRasi
        {
            get { return mKarakaKendradiGrahaDasaRasi; }
            set { mKarakaKendradiGrahaDasaRasi = value; }
        }

        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("Graha Strengths")]
        public GrahaStrength[] KarakaKendradiGrahaDasaGraha
        {
            get { return mKarakaKendradiGrahaDasaGraha; }
            set { mKarakaKendradiGrahaDasaGraha = value; }
        }

        [InVisible]
        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("CoLord Strengths")]
        [TypeConverter(typeof(HoraArrayConverter))]
        public GrahaStrength[] KarakaKendradiGrahaDasaColord
        {
            get { return mKarakaKendradiGrahaDasaColord; }
            set { mKarakaKendradiGrahaDasaColord = value; }
        }

        public StrengthOptions()
        {
            Colord = new GrahaStrength[]
            {
                GrahaStrength.NotInOwnHouse,
                GrahaStrength.AspectsRasi,
                GrahaStrength.Exaltation,
                GrahaStrength.RasisNature,
                GrahaStrength.NarayanaDasaLength,
                GrahaStrength.Longitude
            };

            NaisargikaDasaGraha = new GrahaStrength[]
            {
                GrahaStrength.Exaltation,
                GrahaStrength.LordInOwnHouse,
                GrahaStrength.MoolaTrikona,
                GrahaStrength.Longitude
            };

            KarakaKendradiGrahaDasaGraha = new GrahaStrength[]
            {
                GrahaStrength.Exaltation,
                GrahaStrength.MoolaTrikona,
                GrahaStrength.OwnHouse,
                GrahaStrength.Longitude
            };

            KarakaKendradiGrahaDasaRasi = new RasiStrength[]
            {
                RasiStrength.Conjunction,
                RasiStrength.AspectsRasi,
                RasiStrength.Exaltation,
                RasiStrength.MoolaTrikona,
                RasiStrength.OwnHouse,
                RasiStrength.LordsNature,
                RasiStrength.AtmaKaraka,
                RasiStrength.Longitude,
                RasiStrength.LordInDifferentOddity,
                RasiStrength.KarakaKendradiGrahaDasaLength
            };
            KarakaKendradiGrahaDasaColord = new GrahaStrength[]
            {
                GrahaStrength.NotInOwnHouse,
                GrahaStrength.Conjunction,
                GrahaStrength.AspectsRasi,
                GrahaStrength.Exaltation,
                GrahaStrength.MoolaTrikona,
                GrahaStrength.OwnHouse,
                GrahaStrength.LordsNature,
                GrahaStrength.AtmaKaraka,
                GrahaStrength.Longitude,
                GrahaStrength.LordInDifferentOddity,
                GrahaStrength.KarakaKendradiGrahaDasaLength
            };

            NavamsaDasaRasi = new RasiStrength[]
            {
                RasiStrength.AspectsRasi,
                RasiStrength.Conjunction,
                RasiStrength.Exaltation,
                RasiStrength.LordInDifferentOddity,
                RasiStrength.RasisNature,
                RasiStrength.LordsLongitude
            };

            MoolaDasaRasi = new RasiStrength[]
            {
                RasiStrength.Conjunction,
                RasiStrength.Exaltation,
                RasiStrength.MoolaTrikona,
                RasiStrength.OwnHouse,
                RasiStrength.RasisNature,
                RasiStrength.LordsLongitude
            };

            NarayanaDasaRasi = new RasiStrength[]
            {
                RasiStrength.Conjunction,
                RasiStrength.AspectsRasi,
                RasiStrength.Exaltation,
                RasiStrength.LordInDifferentOddity,
                RasiStrength.RasisNature,
                RasiStrength.LordsLongitude
            };

            NaisargikaDasaRasi = new RasiStrength[]
            {
                RasiStrength.Conjunction,
                RasiStrength.AspectsRasi,
                RasiStrength.Exaltation,
                RasiStrength.RasisNature,
                RasiStrength.LordIsAtmaKaraka,
                RasiStrength.LordInDifferentOddity,
                RasiStrength.Longitude
            };
        }

        protected StrengthOptions(SerializationInfo info, StreamingContext context) : this() => this.Constructor(this.GetType(), info, context);
    }

}
