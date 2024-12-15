using System;

namespace org.transliteral.panchang
{

    public class AshtakavargaOptions : ICloneable
    {
        private Division mDtype;
        private EChartStyle mChartStyle;
        private ESavType mSavType;

        public AshtakavargaOptions()
        {
            this.mDtype = new Division(DivisionType.Rasi);
            this.mChartStyle = (EChartStyle)GlobalOptions.Instance.VargaStyle;
        }

        [InVisible]
        public Division VargaType
        {
            get { return this.mDtype; }
            set { this.mDtype = value; }
        }

        [DisplayName("Varga Type")]
        public DivisionType UIVargaType
        {
            get { return this.mDtype.MultipleDivisions[0].Varga; }
            set { this.mDtype = new Division(value); }
        }

        [DisplayName("SAV Type")]
        public ESavType SavType
        {
            get { return this.mSavType; }
            set { this.mSavType = value; }
        }

        public EChartStyle ChartStyle
        {
            get { return this.mChartStyle; }
            set { this.mChartStyle = value; }
        }
        public object Clone()
        {
            AshtakavargaOptions ao = new AshtakavargaOptions();
            ao.mDtype = this.mDtype;
            ao.mChartStyle = this.mChartStyle;
            ao.mSavType = this.mSavType;
            return ao;
        }
        public void SetOptions(AshtakavargaOptions ao)
        {
            this.mDtype = ao.mDtype;
            this.mChartStyle = ao.mChartStyle;
            this.mSavType = ao.mSavType;
        }
    }

}
