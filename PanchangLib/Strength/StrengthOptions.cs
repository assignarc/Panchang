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

        EGrahaStrength[] mColord = null;
        EGrahaStrength[] mNaisargikaDasaGraha = null;
        EGrahaStrength[] mKarakaKendradiGrahaDasaGraha = null;
        EGrahaStrength[] mKarakaKendradiGrahaDasaColord = null;
        ERasiStrength[] mNavamsaDasaRasi = null;
        ERasiStrength[] mMoolaDasaRasi = null;
        ERasiStrength[] mNarayanaDasaRasi = null;
        ERasiStrength[] mNaisargikaDasaRasi = null;
        ERasiStrength[] mKarakaKendradiGrahaDasaRasi = null;

        public object Clone()
        {
            StrengthOptions opts = new StrengthOptions();
            opts.Colord = (EGrahaStrength[])this.Colord.Clone();
            opts.NaisargikaDasaGraha = (EGrahaStrength[])this.NaisargikaDasaGraha.Clone();
            opts.NavamsaDasaRasi = (ERasiStrength[])this.NavamsaDasaRasi.Clone();
            opts.MoolaDasaRasi = (ERasiStrength[])this.MoolaDasaRasi.Clone();
            opts.NarayanaDasaRasi = (ERasiStrength[])this.NarayanaDasaRasi.Clone();
            opts.NaisargikaDasaRasi = (ERasiStrength[])this.NaisargikaDasaRasi.Clone();
            return opts;
        }

        public object Copy(object o)
        {
            StrengthOptions so = (StrengthOptions)o;
            this.Colord = (EGrahaStrength[])so.Colord.Clone();
            this.NaisargikaDasaGraha = (EGrahaStrength[])so.NaisargikaDasaGraha.Clone();
            this.NavamsaDasaRasi = (ERasiStrength[])so.NavamsaDasaRasi.Clone();
            this.MoolaDasaRasi = (ERasiStrength[])so.MoolaDasaRasi.Clone();
            this.NarayanaDasaRasi = (ERasiStrength[])so.NarayanaDasaRasi.Clone();
            this.NaisargikaDasaRasi = (ERasiStrength[])so.NaisargikaDasaRasi.Clone();
            return this.Clone();
        }

        [Category("Co-Lord Strengths")]
        [Visible("Graha Strength")]
        public EGrahaStrength[] Colord
        {
            get { return mColord; }
            set { mColord = value; }
        }

        [Category("Naisargika Dasa Strengths")]
        [Visible("Graha Strengths")]
        public EGrahaStrength[] NaisargikaDasaGraha
        {
            get { return mNaisargikaDasaGraha; }
            set { mNaisargikaDasaGraha = value; }
        }

        [Category("Naisargika Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public ERasiStrength[] NaisargikaDasaRasi
        {
            get { return mNaisargikaDasaRasi; }
            set { mNaisargikaDasaRasi = value; }
        }

        [Category("Navamsa Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public ERasiStrength[] NavamsaDasaRasi
        {
            get { return mNavamsaDasaRasi; }
            set { mNavamsaDasaRasi = value; }
        }

        [Category("Moola Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public ERasiStrength[] MoolaDasaRasi
        {
            get { return mMoolaDasaRasi; }
            set { mMoolaDasaRasi = value; }
        }

        [Category("Narayana Dasa Strengths")]
        [Visible("Rasi Strengths")]
        public ERasiStrength[] NarayanaDasaRasi
        {
            get { return mNarayanaDasaRasi; }
            set { mNarayanaDasaRasi = value; }
        }

        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("Rasi Strengths")]
        public ERasiStrength[] KarakaKendradiGrahaDasaRasi
        {
            get { return mKarakaKendradiGrahaDasaRasi; }
            set { mKarakaKendradiGrahaDasaRasi = value; }
        }

        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("Graha Strengths")]
        public EGrahaStrength[] KarakaKendradiGrahaDasaGraha
        {
            get { return mKarakaKendradiGrahaDasaGraha; }
            set { mKarakaKendradiGrahaDasaGraha = value; }
        }

        [InVisible]
        [Category("Karaka Kendradi Graha Dasa")]
        [Visible("CoLord Strengths")]
        [TypeConverter(typeof(HoraArrayConverter))]
        public EGrahaStrength[] KarakaKendradiGrahaDasaColord
        {
            get { return mKarakaKendradiGrahaDasaColord; }
            set { mKarakaKendradiGrahaDasaColord = value; }
        }

        public StrengthOptions()
        {
            Colord = new EGrahaStrength[]
            {
                EGrahaStrength.NotInOwnHouse,
                EGrahaStrength.AspectsRasi,
                EGrahaStrength.Exaltation,
                EGrahaStrength.RasisNature,
                EGrahaStrength.NarayanaDasaLength,
                EGrahaStrength.Longitude
            };

            NaisargikaDasaGraha = new EGrahaStrength[]
            {
                EGrahaStrength.Exaltation,
                EGrahaStrength.LordInOwnHouse,
                EGrahaStrength.MoolaTrikona,
                EGrahaStrength.Longitude
            };

            KarakaKendradiGrahaDasaGraha = new EGrahaStrength[]
            {
                EGrahaStrength.Exaltation,
                EGrahaStrength.MoolaTrikona,
                EGrahaStrength.OwnHouse,
                EGrahaStrength.Longitude
            };

            KarakaKendradiGrahaDasaRasi = new ERasiStrength[]
            {
                ERasiStrength.Conjunction,
                ERasiStrength.AspectsRasi,
                ERasiStrength.Exaltation,
                ERasiStrength.MoolaTrikona,
                ERasiStrength.OwnHouse,
                ERasiStrength.LordsNature,
                ERasiStrength.AtmaKaraka,
                ERasiStrength.Longitude,
                ERasiStrength.LordInDifferentOddity,
                ERasiStrength.KarakaKendradiGrahaDasaLength
            };
            KarakaKendradiGrahaDasaColord = new EGrahaStrength[]
            {
                EGrahaStrength.NotInOwnHouse,
                EGrahaStrength.Conjunction,
                EGrahaStrength.AspectsRasi,
                EGrahaStrength.Exaltation,
                EGrahaStrength.MoolaTrikona,
                EGrahaStrength.OwnHouse,
                EGrahaStrength.LordsNature,
                EGrahaStrength.AtmaKaraka,
                EGrahaStrength.Longitude,
                EGrahaStrength.LordInDifferentOddity,
                EGrahaStrength.KarakaKendradiGrahaDasaLength
            };

            NavamsaDasaRasi = new ERasiStrength[]
            {
                ERasiStrength.AspectsRasi,
                ERasiStrength.Conjunction,
                ERasiStrength.Exaltation,
                ERasiStrength.LordInDifferentOddity,
                ERasiStrength.RasisNature,
                ERasiStrength.LordsLongitude
            };

            MoolaDasaRasi = new ERasiStrength[]
            {
                ERasiStrength.Conjunction,
                ERasiStrength.Exaltation,
                ERasiStrength.MoolaTrikona,
                ERasiStrength.OwnHouse,
                ERasiStrength.RasisNature,
                ERasiStrength.LordsLongitude
            };

            NarayanaDasaRasi = new ERasiStrength[]
            {
                ERasiStrength.Conjunction,
                ERasiStrength.AspectsRasi,
                ERasiStrength.Exaltation,
                ERasiStrength.LordInDifferentOddity,
                ERasiStrength.RasisNature,
                ERasiStrength.LordsLongitude
            };

            NaisargikaDasaRasi = new ERasiStrength[]
            {
                ERasiStrength.Conjunction,
                ERasiStrength.AspectsRasi,
                ERasiStrength.Exaltation,
                ERasiStrength.RasisNature,
                ERasiStrength.LordIsAtmaKaraka,
                ERasiStrength.LordInDifferentOddity,
                ERasiStrength.Longitude
            };
        }

        protected StrengthOptions(SerializationInfo info, StreamingContext context) : this() => this.Constructor(this.GetType(), info, context);
    }

}
