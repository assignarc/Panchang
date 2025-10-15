

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{

    public class PanchangControlContainer : UserControl
    {
        private BaseControl mControl;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        public BaseUserOptions options;
        public Horoscope h;
        public class BaseUserOptions : ICloneable
        {
            private BaseUserOptionsViewType _view;
            public object Clone()
            {
                BaseUserOptions uo = new BaseUserOptions
                {
                    _view = _view
                };
                return uo;
            }
            public BaseUserOptionsViewType View
            {
                get { return _view; }
                set
                {
                    _view = value;
                }
            }
            public BaseUserOptions()
            {
                _view = BaseUserOptionsViewType.DivisionalChart;
            }
        }

        public void SetView(BaseUserOptionsViewType view)
        {
            BaseControl mc = null; ;

            switch (view)
            {
                case BaseUserOptionsViewType.DivisionalChart:
                    mc = new DivisionalChart(h);
                    break;
                case BaseUserOptionsViewType.Ashtakavarga:
                    mc = new AshtakavargaControl(h);
                    break;
                case BaseUserOptionsViewType.ChakraSarvatobhadra81:
                    mc = new Sarvatobhadra81Control(h);
                    break;
                case BaseUserOptionsViewType.NavamsaCircle:
                    mc = new NavamsaControl(h);
                    break;
                case BaseUserOptionsViewType.VaraChakra:
                    mc = new VaraChakra(h);
                    break;
                case BaseUserOptionsViewType.Panchanga:
                    mc = new PanchangaControl(h);
                    break;
                case BaseUserOptionsViewType.KutaMatching:
                    {
                        Horoscope h2 = h;
                        foreach (Form f in ((PanchangContainer)PanchangAppOptions.mainControl).MdiChildren)
                        {
                            if (f is PanchangChild mch)
                            {
                                if (h == h2 && mch.getHoroscope() != h2)
                                {
                                    h2 = mch.getHoroscope();
                                    break;
                                }
                            }
                        }
                        mc = new KutaMatchingControl(h, h);
                    }
                    break;
                case BaseUserOptionsViewType.DasaVimsottari:
                    mc = new DasaControl(h, new VimsottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaYogini:
                    mc = new DasaControl(h, new YoginiDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaShodashottari:
                    mc = new DasaControl(h, new ShodashottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaAshtottari:
                    mc = new DasaControl(h, new AshtottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaTithiAshtottari:
                    mc = new DasaControl(h, new TithiAshtottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaKaranaChaturashitiSama:
                    mc = new DasaControl(h, new KaranaChaturashitiSamaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaYogaVimsottari:
                    mc = new DasaControl(h, new YogaVimsottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaLagnaKendradiRasi:
                    mc = new DasaControl(h, new LagnaKendradiRasiDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaKarakaKendradiGraha:
                    mc = new DasaControl(h, new KarakaKendradiGrahaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaKalachakra:
                    mc = new DasaControl(h, new KalachakraDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaMoola:
                    mc = new DasaControl(h, new MoolaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaNavamsa:
                    mc = new DasaControl(h, new NavamsaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaMandooka:
                    mc = new DasaControl(h, new MandookaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaChara:
                    mc = new DasaControl(h, new CharaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaTrikona:
                    mc = new DasaControl(h, new TrikonaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaSu:
                    mc = new DasaControl(h, new SuDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaSudarshanaChakra:
                    mc = new DasaControl(h, new SudarshanaChakraDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaMudda:
                    {
                        DasaControl dc = new DasaControl(h, new VimsottariDasa(h));
                        dc.DasaOptions.YearType = DateType.SolarYear;
                        dc.DasaOptions.YearLength = 360;
                        dc.DasaOptions.Compression = 1;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaSudarshanaChakraCompressed:
                    {
                        DasaControl dc = new DasaControl(h, new SudarshanaChakraDasa(h));
                        dc.DasaOptions.YearType = DateType.SolarYear;
                        dc.DasaOptions.YearLength = 360;
                        dc.DasaOptions.Compression = 1;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaYogaPraveshVimsottariCompressedYoga:
                    {
                        DasaControl dc = new DasaControl(h, new YogaVimsottariDasa(h));
                        dc.compressToYogaPraveshaYearYoga();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaTithiPraveshAshtottariCompressedTithi:
                    {
                        DasaControl dc = new DasaControl(h, new TithiAshtottariDasa(h));
                        dc.DasaOptions.YearType = DateType.TithiYear;
                        ToDate td_pravesh = new ToDate(h.BaseUT, DateType.TithiPraveshYear, 360.0, 0, h);
                        ToDate td_tithi = new ToDate(h.BaseUT, DateType.TithiYear, 360.0, 0, h);
                        Sweph.Lock(h);
                        if (td_tithi.AddYears(1).ToUniversalTime() + 15.0 < td_pravesh.AddYears(1).ToUniversalTime())
                            dc.DasaOptions.YearLength = 390;
                        Sweph.Unlock(h);
                        dc.DasaOptions.Compression = 1;

                        TithiAshtottariDasa.UserOptions tuo = (TithiAshtottariDasa.UserOptions)dc.DasaSpecificOptions;
                        tuo.UseTithiRemainder = true;
                        dc.DasaSpecificOptions = tuo;

                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaTithiPraveshAshtottariCompressedFixed:
                    {
                        DasaControl dc = new DasaControl(h, new TithiAshtottariDasa(h));
                        ToDate td_pravesh = new ToDate(h.BaseUT, DateType.TithiPraveshYear, 360.0, 0, h);
                        Sweph.Lock(h);
                        dc.DasaOptions.YearType = DateType.FixedYear;
                        dc.DasaOptions.YearLength = td_pravesh.AddYears(1).ToUniversalTime() -
                            td_pravesh.AddYears(0).ToUniversalTime();
                        Sweph.Unlock(h);

                        TithiAshtottariDasa.UserOptions tuo = (TithiAshtottariDasa.UserOptions)dc.DasaSpecificOptions;
                        tuo.UseTithiRemainder = true;
                        dc.DasaSpecificOptions = tuo;
                        dc.DasaOptions.Compression = 1;

                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaTithiPraveshAshtottariCompressedSolar:
                    {
                        DasaControl dc = new DasaControl(h, new TithiAshtottariDasa(h));
                        ToDate td_pravesh = new ToDate(h.BaseUT, DateType.TithiPraveshYear, 360.0, 0, h);
                        Sweph.Lock(h);
                        double ut_start = td_pravesh.AddYears(0).ToUniversalTime();
                        double ut_end = td_pravesh.AddYears(1).ToUniversalTime();
                        BodyPosition sp_start = Basics.CalculateSingleBodyPosition(
                            ut_start, Sweph.BodyNameToSweph(BodyName.Sun), BodyName.Sun, BodyType.Name.Graha, h);
                        BodyPosition sp_end = Basics.CalculateSingleBodyPosition(
                            ut_end, Sweph.BodyNameToSweph(BodyName.Sun), BodyName.Sun, BodyType.Name.Graha, h);
                        Longitude lDiff = sp_end.Longitude.Subtract(sp_start.Longitude);
                        double diff = lDiff.Value;
                        if (diff < 120.0) diff += 360.0;

                        dc.DasaOptions.YearType = DateType.SolarYear;
                        dc.DasaOptions.YearLength = diff;
                        Sweph.Unlock(h);

                        TithiAshtottariDasa.UserOptions tuo = (TithiAshtottariDasa.UserOptions)dc.DasaSpecificOptions;
                        tuo.UseTithiRemainder = true;
                        dc.DasaSpecificOptions = tuo;


                        //dc.DasaOptions.YearType = DateType.FixedYear;
                        //dc.DasaOptions.YearLength = td_pravesh.AddYears(1).toUniversalTime() - 
                        //	td_pravesh.AddYears(0).toUniversalTime();
                        dc.DasaOptions.Compression = 1;

                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaDwadashottari:
                    mc = new DasaControl(h, new DwadashottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaPanchottari:
                    mc = new DasaControl(h, new PanchottariDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaShatabdika:
                    mc = new DasaControl(h, new ShatabdikaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaChaturashitiSama:
                    mc = new DasaControl(h, new ChaturashitiSamaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaDwisaptatiSama:
                    mc = new DasaControl(h, new DwisaptatiSamaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaShatTrimshaSama:
                    mc = new DasaControl(h, new ShatTrimshaSamaDasa(h));
                    break;
                case BaseUserOptionsViewType.BasicCalculations:
                    mc = new BasicCalculationsControl(h);
                    break;
                case BaseUserOptionsViewType.KeyInfo:
                    mc = new KeyInfoControl(h);
                    break;
                case BaseUserOptionsViewType.Balas:
                    mc = new BalasControl(h);
                    break;
                case BaseUserOptionsViewType.TransitSearch:
                    mc = new TransitSearch(h);
                    break;
                case BaseUserOptionsViewType.NaisargikaRasiDasa:
                    mc = new DasaControl(h, new NaisargikaRasiDasa(h));
                    break;
                case BaseUserOptionsViewType.NaisargikaGrahaDasa:
                    mc = new DasaControl(h, new NaisargikaGrahaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaNarayana:
                    mc = new DasaControl(h, new NarayanaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaNarayanaSama:
                    mc = new DasaControl(h, new NarayanaSamaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaShoola:
                    mc = new DasaControl(h, new ShoolaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaNiryaanaShoola:
                    mc = new DasaControl(h, new NirayaanaShoolaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaDrig:
                    mc = new DasaControl(h, new DrigDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaTajaka:
                    mc = new DasaControl(h, new TajakaDasa(h));
                    break;
                case BaseUserOptionsViewType.DasaTithiPravesh:
                    {
                        DasaControl dc = new DasaControl(h, new TithiPraveshDasa(h));
                        dc.DasaOptions.YearType = DateType.TithiPraveshYear;
                        dc.LinkToHoroscope = false;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaYogaPravesh:
                    {
                        DasaControl dc = new DasaControl(h, new YogaPraveshDasa(h));
                        dc.DasaOptions.YearType = DateType.YogaPraveshYear;
                        dc.LinkToHoroscope = false;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaNakshatraPravesh:
                    {
                        DasaControl dc = new DasaControl(h, new NakshatraPraveshDasa(h));
                        dc.DasaOptions.YearType = DateType.NakshatraPraveshYear;
                        dc.LinkToHoroscope = false;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaKaranaPravesh:
                    {
                        DasaControl dc = new DasaControl(h, new KaranaPraveshDasa(h));
                        dc.DasaOptions.YearType = DateType.KaranaPraveshYear;
                        dc.LinkToHoroscope = false;
                        dc.Reset();
                        mc = dc;
                    }
                    break;
                case BaseUserOptionsViewType.DasaTattwa:
                    mc = new DasaControl(h, new TattwaDasa(h));
                    break;
                default:
                    Debug.Assert(false, "Unknown View Internal error");
                    break;
            }
            mc.Dock = DockStyle.Fill;
            Control?.Dispose();
            Control = mc;

            return;

        }
        public void SetBaseOptions(object o)
        {
            BaseUserOptions uo = (BaseUserOptions)o;
            options.View = uo.View;
            SetView(uo.View);

        }

        public BaseControl Control
        {
            get { return mControl; }
            set
            {
                if (mControl != null)
                    Controls.Remove(mControl);
                mControl = value;
                mControl.Dock = DockStyle.Fill;
                Controls.Add(mControl);
                mControl.Parent = this;
            }
        }

        public PanchangControlContainer(BaseControl _mControl)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            Control = _mControl;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            Name = "PanchangControlContainer";
            Load += new EventHandler(PanchangControlContainer_Load);

        }
        #endregion

        private void mControl_Load(object sender, EventArgs e)
        {

        }

        private void PanchangControlContainer_Load(object sender, EventArgs e)
        {
            options = new BaseUserOptions();

        }
    }
}
