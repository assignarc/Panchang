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
       
       

        [TypeConverter(typeof(EnumDescConverter))]
        public enum EViewStyle
        {
            [Description("Regular")] Normal,
            [Description("Dual Graha Arudhas")] DualGrahaArudha,
            [Description("8 Chara Karakas (regular)")] CharaKarakas8,
            [Description("7 Chara Karakas (mundane)")] CharaKarakas7,
            [Description("Varnada Lagna")] Varnada,
            [Description("Panchanga Print View")] Panchanga
        };

        private Division varga;
        private Division innerVarga;
        private EChartStyle mChartStyle;
        private EViewStyle mViewStyle;
        private bool mbShowInner;

        public UserOptions()
        {
            this.Varga = new Division(DivisionType.Rasi);
            mViewStyle = EViewStyle.Normal;
            mChartStyle = MhoraGlobalOptions.Instance.VargaStyle;
            varga = new Division(DivisionType.Rasi);
            innerVarga = new Division(DivisionType.Rasi);
            mbShowInner = false;
        }

        [Category("Options")]
        [PGDisplayName("Varga")]
        public Division Varga
        {
            get { return varga; }
            set { varga = value; }
        }


        [PGDisplayName("Dual Chart View")]
        public bool ShowInner
        {
            get { return mbShowInner; }
            set { this.mbShowInner = value; }
        }

        [PGDisplayName("View Type")]
        public EViewStyle ViewStyle
        {
            get { return this.mViewStyle; }
            set { this.mViewStyle = value; }
        }
        [Category("Options")]
        [PGDisplayName("Chart style")]
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
