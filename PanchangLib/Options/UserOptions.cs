using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{
   
    public class UserOptions : ICloneable
    {
      
        private Division varga;
        private Division innerVarga;
        private EChartStyle mChartStyle;
        private EViewStyle mViewStyle;
        private bool mbShowInner;

        public UserOptions()
        {
            this.Varga = new Division(DivisionType.Rasi);
            mViewStyle = EViewStyle.Normal;
            mChartStyle = GlobalOptions.Instance.VargaStyle;
            varga = new Division(DivisionType.Rasi);
            innerVarga = new Division(DivisionType.Rasi);
            mbShowInner = false;
        }

        [Category("Options")]
        [Visible("Varga")]
        public Division Varga
        {
            get { return varga; }
            set { varga = value; }
        }


        [Visible("Dual Chart View")]
        public bool ShowInner
        {
            get { return mbShowInner; }
            set { this.mbShowInner = value; }
        }

        [Visible("View Type")]
        public EViewStyle ViewStyle
        {
            get { return this.mViewStyle; }
            set { this.mViewStyle = value; }
        }
        [Category("Options")]
        [Visible("Chart style")]
        public EChartStyle ChartStyle
        {
            get { return this.mChartStyle; }
            set { this.mChartStyle = value; }
        }

        public Object Copy(Object o)
        {
            UserOptions uo = (UserOptions)o;
            this.mChartStyle = uo.mChartStyle;
            this.mViewStyle = uo.mViewStyle;
            this.Varga = uo.Varga;
            this.ShowInner = uo.ShowInner;
            return uo;
        }
        public Object Clone()
        {
            UserOptions uo = new UserOptions();
            uo.Varga = Varga;
            uo.mChartStyle = this.mChartStyle;
            uo.mViewStyle = this.mViewStyle;
            uo.ShowInner = this.ShowInner;
            return uo;
        }
    }
}
