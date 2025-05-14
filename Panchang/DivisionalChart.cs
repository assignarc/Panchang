using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{


    /// <summary>
    /// Summary description for DivisionalChart.
    /// </summary>
    [Serializable()]
    public class DivisionalChart : PanchangControl //System.Windows.Forms.UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        public ContextMenu contextMenu;
        private MenuItem mOptions;
        private IDrawChart dc;
        private MenuItem menuItem1;
        private MenuItem mRasi;
        private MenuItem mNavamsa;
        private MenuItem menuItem5;
        private MenuItem mDrekkanaParasara;
        private MenuItem mChaturamsa;
        private MenuItem mPanchamsa;
        private MenuItem mShashtamsa;
        private MenuItem mSaptamsa;
        private MenuItem mDasamsa;
        private MenuItem mDwadasamsa;
        private MenuItem mShodasamsa;
        private MenuItem mVimsamsa;
        private MenuItem mChaturvimsamsa;
        private MenuItem mTrimsamsa;
        private MenuItem mNakshatramsa;
        private MenuItem mKhavedamsa;
        private MenuItem menuItem18;
        private MenuItem mDrekkanaJagannath;
        private MenuItem mDrekkanaSomnath;
        private MenuItem mDrekkanaParivrittitraya;
        private MenuItem menuItem2;

        public UserOptions options;
    
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem mHoraKashinath;
        private MenuItem mHoraParivritti;
        private MenuItem mHoraParasara;
        private MenuItem mHoraJagannath;
        private MenuItem mTrimsamsaParivritti;
        private MenuItem mLagnaChange;
        public HoroscopeOptions calculation_options;
        private ArrayList div_pos;
        private MenuItem mExtrapolate;
        private MenuItem menuItem6;
        private MenuItem mShashtyamsa;
        private MenuItem mAkshavedamsa;
        private MenuItem mAshtottaramsa;
        private MenuItem mRudramsaRath;
        private MenuItem mRudramsaRaman;
        private MenuItem mNadiamsa;
        private MenuItem mAshtamsa;
        private MenuItem mAshtamsaRaman;
        private MenuItem mTrimsamsaSimple;
        private MenuItem menuItem7;
        private MenuItem mBhava;
        private MenuItem mBhavaEqual;
        private MenuItem mBhavaSripati;
        private MenuItem mBhavaKoch;
        private MenuItem mBhavaPlacidus;
        private MenuItem menuBhavaAlcabitus;
        private MenuItem menuBhavaRegiomontanus;
        private MenuItem menuBhavaCampanus;
        private MenuItem menuBhavaAxial;
        private MenuItem menuItem9;
        private MenuItem mNadiamsaCKN;
        private ArrayList arudha_pos;
        private ArrayList varnada_pos;
        private MenuItem menuItem8;
        private MenuItem mViewNormal;
        private MenuItem mViewDualGrahaArudha;
        private MenuItem mNavamsaDwadasamsa;
        private MenuItem mDwadasamsaDwadasamsa;
        private MenuItem menuItem10;
        private MenuItem mViewCharaKarakas;
        private MenuItem mViewCharaKarakas7;
        private MenuItem mViewVarnada;
        private ArrayList graha_arudha_pos;
        private int[] sav_bindus;
        private MenuItem menuItem11;
        private MenuItem mRegularParivritti;
        private MenuItem mRegularFromHouse;
        private MenuItem mRegularTrikona;
        private MenuItem mRegularKendraChaturthamsa;
        private MenuItem mRegularSaptamsaBased;
        private MenuItem mRegularDasamsaBased;
        private MenuItem mRegularShashthamsaBased;
        private MenuItem mRegularShodasamsaBased;
        private MenuItem mRegularVimsamsaBased;
        private MenuItem mRegularNakshatramsaBased;
        private MenuItem menuItem12;

        public bool PrintMode = false;

        DivisionalChart innerControl = null;


        public void AddInnerControl()
        {
            innerControl = new DivisionalChart(h, true);
            Controls.Add(innerControl);
        }

        public void Constructor(Horoscope _h)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            h = _h;
            options = new UserOptions();
            calculation_options = h.Options;
            h.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            OnRecalculate(h);
            SetChartStyle(options.ChartStyle);
            //dc = new SouthIndianChart();
            //dc = new EastIndianChart();
        }

        bool bInnerMode;
        public DivisionalChart(Horoscope _h) : base()
        {
            Constructor(_h);
            bInnerMode = false;
        }

        public DivisionalChart(Horoscope _h, bool bInner) : base()
        {
            Constructor(_h);
            bInnerMode = true;
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
            contextMenu = new ContextMenu();
            mLagnaChange = new MenuItem();
            mOptions = new MenuItem();
            menuItem8 = new MenuItem();
            mViewNormal = new MenuItem();
            mViewDualGrahaArudha = new MenuItem();
            mViewVarnada = new MenuItem();
            mViewCharaKarakas = new MenuItem();
            mViewCharaKarakas7 = new MenuItem();
            menuItem1 = new MenuItem();
            mRasi = new MenuItem();
            mNavamsa = new MenuItem();
            menuItem7 = new MenuItem();
            mBhava = new MenuItem();
            mBhavaEqual = new MenuItem();
            mBhavaSripati = new MenuItem();
            menuItem9 = new MenuItem();
            menuBhavaAlcabitus = new MenuItem();
            menuBhavaAxial = new MenuItem();
            menuBhavaCampanus = new MenuItem();
            mBhavaKoch = new MenuItem();
            mBhavaPlacidus = new MenuItem();
            menuBhavaRegiomontanus = new MenuItem();
            menuItem5 = new MenuItem();
            mHoraParasara = new MenuItem();
            mDrekkanaParasara = new MenuItem();
            mChaturamsa = new MenuItem();
            mPanchamsa = new MenuItem();
            mShashtamsa = new MenuItem();
            mSaptamsa = new MenuItem();
            mAshtamsa = new MenuItem();
            mDasamsa = new MenuItem();
            mDwadasamsa = new MenuItem();
            mShodasamsa = new MenuItem();
            mVimsamsa = new MenuItem();
            mChaturvimsamsa = new MenuItem();
            menuItem18 = new MenuItem();
            mHoraParivritti = new MenuItem();
            mHoraJagannath = new MenuItem();
            mHoraKashinath = new MenuItem();
            mDrekkanaParivrittitraya = new MenuItem();
            mDrekkanaJagannath = new MenuItem();
            mDrekkanaSomnath = new MenuItem();
            mAshtamsaRaman = new MenuItem();
            mRudramsaRath = new MenuItem();
            mRudramsaRaman = new MenuItem();
            mTrimsamsaParivritti = new MenuItem();
            mTrimsamsaSimple = new MenuItem();
            menuItem6 = new MenuItem();
            mNakshatramsa = new MenuItem();
            mTrimsamsa = new MenuItem();
            mKhavedamsa = new MenuItem();
            mAkshavedamsa = new MenuItem();
            mShashtyamsa = new MenuItem();
            mAshtottaramsa = new MenuItem();
            mNadiamsa = new MenuItem();
            mNadiamsaCKN = new MenuItem();
            menuItem10 = new MenuItem();
            mNavamsaDwadasamsa = new MenuItem();
            mDwadasamsaDwadasamsa = new MenuItem();
            mExtrapolate = new MenuItem();
            menuItem11 = new MenuItem();
            mRegularParivritti = new MenuItem();
            mRegularFromHouse = new MenuItem();
            mRegularSaptamsaBased = new MenuItem();
            mRegularDasamsaBased = new MenuItem();
            mRegularShashthamsaBased = new MenuItem();
            menuItem12 = new MenuItem();
            mRegularTrikona = new MenuItem();
            mRegularShodasamsaBased = new MenuItem();
            mRegularVimsamsaBased = new MenuItem();
            mRegularKendraChaturthamsa = new MenuItem();
            mRegularNakshatramsaBased = new MenuItem();
            menuItem3 = new MenuItem();
            menuItem4 = new MenuItem();
            menuItem2 = new MenuItem();
            // 
            // contextMenu
            // 
            contextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                        mLagnaChange,
                                                                                        mOptions,
                                                                                        menuItem8,
                                                                                        menuItem1,
                                                                                        mRasi,
                                                                                        mNavamsa,
                                                                                        menuItem7,
                                                                                        menuItem5,
                                                                                        mHoraParasara,
                                                                                        mDrekkanaParasara,
                                                                                        mChaturamsa,
                                                                                        mPanchamsa,
                                                                                        mShashtamsa,
                                                                                        mSaptamsa,
                                                                                        mAshtamsa,
                                                                                        mDasamsa,
                                                                                        mDwadasamsa,
                                                                                        mShodasamsa,
                                                                                        mVimsamsa,
                                                                                        mChaturvimsamsa,
                                                                                        menuItem18,
                                                                                        menuItem6,
                                                                                        menuItem11,
                                                                                        menuItem3,
                                                                                        menuItem4});
            // 
            // mLagnaChange
            // 
            mLagnaChange.Index = 0;
            mLagnaChange.Text = "Lagna Change";
            mLagnaChange.Click += new EventHandler(mLagnaChange_Click);
            // 
            // mOptions
            // 
            mOptions.Index = 1;
            mOptions.Text = "&Options";
            mOptions.Click += new EventHandler(mOptions_Click);
            // 
            // menuItem8
            // 
            menuItem8.Index = 2;
            menuItem8.MenuItems.AddRange(new MenuItem[] {
                                                                                      mViewNormal,
                                                                                      mViewDualGrahaArudha,
                                                                                      mViewVarnada,
                                                                                      mViewCharaKarakas,
                                                                                      mViewCharaKarakas7});
            menuItem8.Text = "View";
            // 
            // mViewNormal
            // 
            mViewNormal.Index = 0;
            mViewNormal.Text = "Normal";
            mViewNormal.Click += new EventHandler(mViewNormal_Click);
            // 
            // mViewDualGrahaArudha
            // 
            mViewDualGrahaArudha.Index = 1;
            mViewDualGrahaArudha.Text = "Graha Arudha";
            mViewDualGrahaArudha.Click += new EventHandler(mViewDualGrahaArudha_Click);
            // 
            // mViewVarnada
            // 
            mViewVarnada.Index = 2;
            mViewVarnada.Text = "Varnada";
            mViewVarnada.Click += new EventHandler(mViewVarnada_Click);
            // 
            // mViewCharaKarakas
            // 
            mViewCharaKarakas.Index = 3;
            mViewCharaKarakas.Text = "Chara Karakas (8)";
            mViewCharaKarakas.Click += new EventHandler(mViewCharaKarakas_Click);
            // 
            // mViewCharaKarakas7
            // 
            mViewCharaKarakas7.Index = 4;
            mViewCharaKarakas7.Text = "Chara Karakas (7)";
            mViewCharaKarakas7.Click += new EventHandler(mViewCharaKarakas7_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 3;
            menuItem1.Text = "-";
            // 
            // mRasi
            // 
            mRasi.Index = 4;
            mRasi.Text = "Rasi";
            mRasi.Click += new EventHandler(mRasi_Click);
            // 
            // mNavamsa
            // 
            mNavamsa.Index = 5;
            mNavamsa.Text = "Navamsa";
            mNavamsa.Click += new EventHandler(mNavamsa_Click);
            // 
            // menuItem7
            // 
            menuItem7.Index = 6;
            menuItem7.MenuItems.AddRange(new MenuItem[] {
                                                                                      mBhava,
                                                                                      mBhavaEqual,
                                                                                      mBhavaSripati,
                                                                                      menuItem9,
                                                                                      menuBhavaAlcabitus,
                                                                                      menuBhavaAxial,
                                                                                      menuBhavaCampanus,
                                                                                      mBhavaKoch,
                                                                                      mBhavaPlacidus,
                                                                                      menuBhavaRegiomontanus});
            menuItem7.Text = "Bhavas";
            // 
            // mBhava
            // 
            mBhava.Index = 0;
            mBhava.Text = "Equal houses (9 padas)";
            mBhava.Click += new EventHandler(mBhava_Click);
            // 
            // mBhavaEqual
            // 
            mBhavaEqual.Index = 1;
            mBhavaEqual.Text = "Equal houses (30 degrees)";
            mBhavaEqual.Click += new EventHandler(mBhavaEqual_Click);
            // 
            // mBhavaSripati
            // 
            mBhavaSripati.Index = 2;
            mBhavaSripati.Text = "Sripati (Poryphory)";
            mBhavaSripati.Click += new EventHandler(mBhavaSripati_Click);
            // 
            // menuItem9
            // 
            menuItem9.Index = 3;
            menuItem9.Text = "-";
            // 
            // menuBhavaAlcabitus
            // 
            menuBhavaAlcabitus.Index = 4;
            menuBhavaAlcabitus.Text = "Alcabitus";
            menuBhavaAlcabitus.Click += new EventHandler(menuBhavaAlcabitus_Click);
            // 
            // menuBhavaAxial
            // 
            menuBhavaAxial.Index = 5;
            menuBhavaAxial.Text = "Axial";
            menuBhavaAxial.Click += new EventHandler(menuBhavaAxial_Click);
            // 
            // menuBhavaCampanus
            // 
            menuBhavaCampanus.Index = 6;
            menuBhavaCampanus.Text = "Campanus";
            menuBhavaCampanus.Click += new EventHandler(menuBhavaCampanus_Click);
            // 
            // mBhavaKoch
            // 
            mBhavaKoch.Index = 7;
            mBhavaKoch.Text = "Koch";
            mBhavaKoch.Click += new EventHandler(mBhavaKoch_Click);
            // 
            // mBhavaPlacidus
            // 
            mBhavaPlacidus.Index = 8;
            mBhavaPlacidus.Text = "Placidus";
            mBhavaPlacidus.Click += new EventHandler(mBhavaPlacidus_Click);
            // 
            // menuBhavaRegiomontanus
            // 
            menuBhavaRegiomontanus.Index = 9;
            menuBhavaRegiomontanus.Text = "Regiomontanus";
            menuBhavaRegiomontanus.Click += new EventHandler(menuBhavaRegiomontanus_Click);
            // 
            // menuItem5
            // 
            menuItem5.Index = 7;
            menuItem5.Text = "-";
            // 
            // mHoraParasara
            // 
            mHoraParasara.Index = 8;
            mHoraParasara.Text = "D-2: Hora";
            mHoraParasara.Click += new EventHandler(mHoraParasara_Click);
            // 
            // mDrekkanaParasara
            // 
            mDrekkanaParasara.Index = 9;
            mDrekkanaParasara.Text = "D-3: Drekkana";
            mDrekkanaParasara.Click += new EventHandler(mDrekkanaParasara_Click);
            // 
            // mChaturamsa
            // 
            mChaturamsa.Index = 10;
            mChaturamsa.Text = "D-4: Chaturamsa";
            mChaturamsa.Click += new EventHandler(mChaturamsa_Click);
            // 
            // mPanchamsa
            // 
            mPanchamsa.Index = 11;
            mPanchamsa.Text = "D-5: Panchamsa";
            mPanchamsa.Click += new EventHandler(mPanchamsa_Click);
            // 
            // mShashtamsa
            // 
            mShashtamsa.Index = 12;
            mShashtamsa.Text = "D-6: Sashtamsa";
            mShashtamsa.Click += new EventHandler(mShashtamsa_Click);
            // 
            // mSaptamsa
            // 
            mSaptamsa.Index = 13;
            mSaptamsa.Text = "D-7: Saptamsa";
            mSaptamsa.Click += new EventHandler(mSaptamsa_Click);
            // 
            // mAshtamsa
            // 
            mAshtamsa.Index = 14;
            mAshtamsa.Text = "D-8: Ashtamsa";
            mAshtamsa.Click += new EventHandler(mAshtamsa_Click);
            // 
            // mDasamsa
            // 
            mDasamsa.Index = 15;
            mDasamsa.Text = "D-10: Dasamsa";
            mDasamsa.Click += new EventHandler(mDasamsa_Click);
            // 
            // mDwadasamsa
            // 
            mDwadasamsa.Index = 16;
            mDwadasamsa.Text = "D-12: Dwadasamsa";
            mDwadasamsa.Click += new EventHandler(mDwadasamsa_Click);
            // 
            // mShodasamsa
            // 
            mShodasamsa.Index = 17;
            mShodasamsa.Text = "D-16: Shodasamsa";
            mShodasamsa.Click += new EventHandler(mShodasamsa_Click);
            // 
            // mVimsamsa
            // 
            mVimsamsa.Index = 18;
            mVimsamsa.Text = "D-20: Vimsamsa";
            mVimsamsa.Click += new EventHandler(mVimsamsa_Click);
            // 
            // mChaturvimsamsa
            // 
            mChaturvimsamsa.Index = 19;
            mChaturvimsamsa.Text = "D-24: Chaturvimsamsa";
            mChaturvimsamsa.Click += new EventHandler(mChaturvimsamsa_Click);
            // 
            // menuItem18
            // 
            menuItem18.Index = 20;
            menuItem18.MenuItems.AddRange(new MenuItem[] {
                                                                                       mHoraParivritti,
                                                                                       mHoraJagannath,
                                                                                       mHoraKashinath,
                                                                                       mDrekkanaParivrittitraya,
                                                                                       mDrekkanaJagannath,
                                                                                       mDrekkanaSomnath,
                                                                                       mAshtamsaRaman,
                                                                                       mRudramsaRath,
                                                                                       mRudramsaRaman,
                                                                                       mTrimsamsaParivritti,
                                                                                       mTrimsamsaSimple});
            menuItem18.Text = "Other Vargas";
            // 
            // mHoraParivritti
            // 
            mHoraParivritti.Index = 0;
            mHoraParivritti.Text = "D-2: Parivritti Dvaya Hora";
            mHoraParivritti.Click += new EventHandler(mHoraParivritti_Click);
            // 
            // mHoraJagannath
            // 
            mHoraJagannath.Enabled = false;
            mHoraJagannath.Index = 1;
            mHoraJagannath.Text = "D-2: Jagannath Hora";
            mHoraJagannath.Click += new EventHandler(mHoraJagannath_Click);
            // 
            // mHoraKashinath
            // 
            mHoraKashinath.Index = 2;
            mHoraKashinath.Text = "D-2: Kashinath Hora";
            mHoraKashinath.Click += new EventHandler(mHoraKashinath_Click);
            // 
            // mDrekkanaParivrittitraya
            // 
            mDrekkanaParivrittitraya.Index = 3;
            mDrekkanaParivrittitraya.Text = "D-3: Parivritti Traya Drekkana";
            mDrekkanaParivrittitraya.Click += new EventHandler(mDrekkanaParivrittitraya_Click);
            // 
            // mDrekkanaJagannath
            // 
            mDrekkanaJagannath.Index = 4;
            mDrekkanaJagannath.Text = "D-3: Jagannath Drekkana";
            mDrekkanaJagannath.Click += new EventHandler(mDrekkanaJagannath_Click);
            // 
            // mDrekkanaSomnath
            // 
            mDrekkanaSomnath.Index = 5;
            mDrekkanaSomnath.Text = "D-3: Somnath Drekkana";
            mDrekkanaSomnath.Click += new EventHandler(mDrekkanaSomnath_Click);
            // 
            // mAshtamsaRaman
            // 
            mAshtamsaRaman.Index = 6;
            mAshtamsaRaman.Text = "D-8: Raman Ashtamsa";
            mAshtamsaRaman.Click += new EventHandler(mAshtamsaRaman_Click);
            // 
            // mRudramsaRath
            // 
            mRudramsaRath.Index = 7;
            mRudramsaRath.Text = "D-11: Rath Rudramsa";
            mRudramsaRath.Click += new EventHandler(mRudramsaRath_Click);
            // 
            // mRudramsaRaman
            // 
            mRudramsaRaman.Index = 8;
            mRudramsaRaman.Text = "D-11: Raman Rudramsa";
            mRudramsaRaman.Click += new EventHandler(mRudramsaRaman_Click);
            // 
            // mTrimsamsaParivritti
            // 
            mTrimsamsaParivritti.Index = 9;
            mTrimsamsaParivritti.Text = "D-30: Parivritti Trimsa Trimsamasa";
            mTrimsamsaParivritti.Click += new EventHandler(mTrimsamsaParivritti_Click);
            // 
            // mTrimsamsaSimple
            // 
            mTrimsamsaSimple.Index = 10;
            mTrimsamsaSimple.Text = "D-30: Zodiacal Trimsamsa";
            mTrimsamsaSimple.Click += new EventHandler(mTrimsamsaSimple_Click);
            // 
            // menuItem6
            // 
            menuItem6.Index = 21;
            menuItem6.MenuItems.AddRange(new MenuItem[] {
                                                                                      mNakshatramsa,
                                                                                      mTrimsamsa,
                                                                                      mKhavedamsa,
                                                                                      mAkshavedamsa,
                                                                                      mShashtyamsa,
                                                                                      mAshtottaramsa,
                                                                                      mNadiamsa,
                                                                                      mNadiamsaCKN,
                                                                                      menuItem10,
                                                                                      mNavamsaDwadasamsa,
                                                                                      mDwadasamsaDwadasamsa,
                                                                                      mExtrapolate});
            menuItem6.Text = "Higher Vargas";
            // 
            // mNakshatramsa
            // 
            mNakshatramsa.Index = 0;
            mNakshatramsa.Text = "D-27: Nakshatramsa";
            mNakshatramsa.Click += new EventHandler(mNakshatramsa_Click);
            // 
            // mTrimsamsa
            // 
            mTrimsamsa.Index = 1;
            mTrimsamsa.Text = "D-30: Trimsamsa";
            mTrimsamsa.Click += new EventHandler(mTrimsamsa_Click);
            // 
            // mKhavedamsa
            // 
            mKhavedamsa.Index = 2;
            mKhavedamsa.Text = "D-40: Khavedamsa";
            mKhavedamsa.Click += new EventHandler(mKhavedamsa_Click);
            // 
            // mAkshavedamsa
            // 
            mAkshavedamsa.Index = 3;
            mAkshavedamsa.Text = "D-45: Akshavedamsa";
            mAkshavedamsa.Click += new EventHandler(mAkshavedamsa_Click_1);
            // 
            // mShashtyamsa
            // 
            mShashtyamsa.Index = 4;
            mShashtyamsa.Text = "D-60: Shashtyamsa";
            mShashtyamsa.Click += new EventHandler(mShashtyamsa_Click_1);
            // 
            // mAshtottaramsa
            // 
            mAshtottaramsa.Index = 5;
            mAshtottaramsa.Text = "D-108: Parivritti Ashtottaramsa";
            mAshtottaramsa.Click += new EventHandler(menuItem7_Click);
            // 
            // mNadiamsa
            // 
            mNadiamsa.Index = 6;
            mNadiamsa.Text = "D-150: Nadiamsa";
            mNadiamsa.Click += new EventHandler(mNadiamsa_Click);
            // 
            // mNadiamsaCKN
            // 
            mNadiamsaCKN.Index = 7;
            mNadiamsaCKN.Text = "D-150: Nadiamsa (Variable)";
            mNadiamsaCKN.Click += new EventHandler(mNadiamsaCKN_Click);
            // 
            // menuItem10
            // 
            menuItem10.Index = 8;
            menuItem10.Text = "-";
            // 
            // mNavamsaDwadasamsa
            // 
            mNavamsaDwadasamsa.Index = 9;
            mNavamsaDwadasamsa.Text = "D-108: Navamsa Dwadasamsa";
            mNavamsaDwadasamsa.Click += new EventHandler(mNavamsaDwadasamsa_Click);
            // 
            // mDwadasamsaDwadasamsa
            // 
            mDwadasamsaDwadasamsa.Index = 10;
            mDwadasamsaDwadasamsa.Text = "D-144: Dwadasamsa Dwadasamsa";
            mDwadasamsaDwadasamsa.Click += new EventHandler(mDwadasamsaDwadasamsa_Click);
            // 
            // mExtrapolate
            // 
            mExtrapolate.Index = 11;
            mExtrapolate.Text = "Extrapolate Horoscope";
            mExtrapolate.Click += new EventHandler(mExtrapolate_Click);
            // 
            // menuItem11
            // 
            menuItem11.Index = 22;
            menuItem11.MenuItems.AddRange(new MenuItem[] {
                                                                                       mRegularParivritti,
                                                                                       mRegularFromHouse,
                                                                                       mRegularSaptamsaBased,
                                                                                       mRegularDasamsaBased,
                                                                                       mRegularShashthamsaBased,
                                                                                       menuItem12,
                                                                                       mRegularTrikona,
                                                                                       mRegularShodasamsaBased,
                                                                                       mRegularVimsamsaBased,
                                                                                       mRegularKendraChaturthamsa,
                                                                                       mRegularNakshatramsaBased});
            menuItem11.Text = "Custom Varga Variations";
            // 
            // mRegularParivritti
            // 
            mRegularParivritti.Index = 0;
            mRegularParivritti.Text = "Regular: Parivritti";
            mRegularParivritti.Click += new EventHandler(mRegularParivritti_Click);
            // 
            // mRegularFromHouse
            // 
            mRegularFromHouse.Index = 1;
            mRegularFromHouse.Text = "Regular: 1: (d-12,d-60 based)";
            mRegularFromHouse.Click += new EventHandler(mRegularFromHouse_Click);
            // 
            // mRegularSaptamsaBased
            // 
            mRegularSaptamsaBased.Index = 2;
            mRegularSaptamsaBased.Text = "Regular: 1,7:  (d-7 based)";
            mRegularSaptamsaBased.Click += new EventHandler(mRegularSaptamsaBased_Click);
            // 
            // mRegularDasamsaBased
            // 
            mRegularDasamsaBased.Index = 3;
            mRegularDasamsaBased.Text = "Regular: 1,9: (d-10 based)";
            mRegularDasamsaBased.Click += new EventHandler(mRegularDasamsaBased_Click);
            // 
            // mRegularShashthamsaBased
            // 
            mRegularShashthamsaBased.Index = 4;
            mRegularShashthamsaBased.Text = "Regular: Ari,Lib:  (d-6, d-40 based)";
            mRegularShashthamsaBased.Click += new EventHandler(mRegularShashthamsaBased_Click);
            // 
            // menuItem12
            // 
            menuItem12.Index = 5;
            menuItem12.Text = "Regular: Leo,Can: (d-24 based)";
            menuItem12.Click += new EventHandler(menuItem12_Click);
            // 
            // mRegularTrikona
            // 
            mRegularTrikona.Index = 6;
            mRegularTrikona.Text = "Trikona: 1,5,9: (d-3 based)";
            mRegularTrikona.Click += new EventHandler(mRegularTrikona_Click);
            // 
            // mRegularShodasamsaBased
            // 
            mRegularShodasamsaBased.Index = 7;
            mRegularShodasamsaBased.Text = "Trikona: Ari,Leo,Sag: (d-16, d-45 based)";
            mRegularShodasamsaBased.Click += new EventHandler(mRegularShodasamsaBased_Click);
            // 
            // mRegularVimsamsaBased
            // 
            mRegularVimsamsaBased.Index = 8;
            mRegularVimsamsaBased.Text = "Trikona: Ari,Sag,Leo: (d-8, d-20 based)";
            mRegularVimsamsaBased.Click += new EventHandler(mRegularVimsamsaBased_Click);
            // 
            // mRegularKendraChaturthamsa
            // 
            mRegularKendraChaturthamsa.Index = 9;
            mRegularKendraChaturthamsa.Text = "Kendra: 1,4,7,10: (d-4 based)";
            mRegularKendraChaturthamsa.Click += new EventHandler(mRegularKendraChaturthamsa_Click);
            // 
            // mRegularNakshatramsaBased
            // 
            mRegularNakshatramsaBased.Index = 10;
            mRegularNakshatramsaBased.Text = "Kendra: Ari,Can,Lib,Cap: (d-27 based)";
            mRegularNakshatramsaBased.Click += new EventHandler(mRegularNakshatramsaBased_Click);
            // 
            // menuItem3
            // 
            menuItem3.Index = 23;
            menuItem3.Text = "-";
            // 
            // menuItem4
            // 
            menuItem4.Index = 24;
            menuItem4.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = -1;
            menuItem2.Text = "Change view 2";
            // 
            // DivisionalChart
            // 
            AllowDrop = true;
            ContextMenu = contextMenu;
            Font = new Font("Times New Roman", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "DivisionalChart";
            Size = new Size(504, 312);
            DragEnter += new DragEventHandler(DivisionalChart_DragEnter);
            Resize += new EventHandler(DivisionalChart_Resize);
            Load += new EventHandler(DivisionalChart_Load);
            DragLeave += new EventHandler(DivisionalChart_DragLeave);
            Paint += new PaintEventHandler(DivisionalChart_Paint);
            DragDrop += new DragEventHandler(DivisionalChart_DragDrop);
            MouseLeave += new EventHandler(DivisionalChart_MouseLeave);
            MouseDown += new MouseEventHandler(DivisionalChart_MouseDown);

        }
        #endregion

        private void DivisionalChart_Load(object sender, EventArgs e)
        {
            AddViewsToContextMenu(contextMenu);
        }

        Point GetOffset(ZodiacHouse zh, int item)
        {
            return dc.GetSmallItemOffset(zh, item);
        }
        private void AddItem(Graphics g, ZodiacHouse zh, int item, DivisionPosition dp, bool large)
        {
            string s = null;

            if (dp.Type == BodyType.Name.GrahaArudha ||
                dp.Type == BodyType.Name.Varnada)
                s = dp.otherString;
            else
                s = Body.ToShortString(dp.Name);

            AddItem(g, zh, item, dp, large, s);
        }

        private readonly Pen pn_black = new Pen(Color.Black, (float)0.01);
        Brush b = new SolidBrush(Color.Black);

        Font fBase = new Font(
            PanchangAppOptions.Instance.VargaFont.FontFamily,
            PanchangAppOptions.Instance.VargaFont.SizeInPoints);

        private void AddItem(Graphics g, ZodiacHouse zh, int item, DivisionPosition dp, bool large, string s)
        {
            Point p;
            Font f;

            if (large)
            {
                p = dc.GetItemOffset(zh, item);
                f = fBase;
                if (dp.Type == BodyType.Name.Graha)
                {
                    BodyPosition bp = h.GetPosition(dp.Name);
                    if (bp.SpeedLongitude < 0.0 && bp.name != BodyName.Rahu && bp.name != BodyName.Ketu)
                        f = new Font(fBase.Name, fBase.Size, FontStyle.Underline);
                }
                else if (dp.Name == BodyName.Lagna)
                {
                    f = new Font(fBase.Name, fBase.Size, FontStyle.Bold);
                }

            }
            else
            {
                FontStyle fs = FontStyle.Regular;
                if (dp.Type == BodyType.Name.BhavaArudhaSecondary)
                    fs = FontStyle.Italic;

                p = dc.GetSmallItemOffset(zh, item);
                f = new Font(fBase.Name, fBase.SizeInPoints - 1, fs);
            }

            if (dp.Type == BodyType.Name.GrahaArudha)
            {
                f = new Font(fBase.Name, fBase.SizeInPoints - 1);
            }

            switch (dp.Type)
            {
                case BodyType.Name.Graha:
                case BodyType.Name.GrahaArudha:
                    b = new SolidBrush(PanchangAppOptions.Instance.VargaGrahaColor);
                    break;
                case BodyType.Name.SpecialLagna:
                    b = new SolidBrush(PanchangAppOptions.Instance.VargaSpecialLagnaColor);
                    break;
                case BodyType.Name.BhavaArudha:
                case BodyType.Name.Varnada:
                case BodyType.Name.BhavaArudhaSecondary:
                    b = new SolidBrush(PanchangAppOptions.Instance.VargaSecondaryColor);
                    break;
                case BodyType.Name.Lagna:
                    b = new SolidBrush(PanchangAppOptions.Instance.VargaLagnaColor);
                    break;
            }

            Font f2 = null;
            if (bInnerMode == true)
                f2 = new Font(f.FontFamily, f.SizeInPoints + 1, f.Style);
            else
                f2 = f;
            //SizeF sf = g.MeasureString (s, f, this.Width);
            //g.FillRectangle(r, p.X, p.Y, sf.Width, sf.Height);
            g.DrawString(s, f2, b, p.X, p.Y);

            if (options.Varga.MultipleDivisions.Length == 1 &&
                options.Varga.MultipleDivisions[0].Varga == DivisionType.Rasi &&
                PrintMode == false &&
                (dp.Type == BodyType.Name.Graha ||
                  dp.Type == BodyType.Name.Lagna))
            {
                Point pLon = dc.GetDegreeOffset(h.GetPosition(dp.Name).Longitude);
                Pen pn = new Pen(PanchangAppOptions.Instance.getBinduColor(dp.Name), (float)0.01);
                Brush br = new SolidBrush(PanchangAppOptions.Instance.getBinduColor(dp.Name));
                g.FillEllipse(br, pLon.X - 1, pLon.Y - 1, 4, 4);
                //g.DrawEllipse(pn, pLon.X-1, pLon.Y-1, 2, 2);
                g.DrawEllipse(new Pen(Color.Gray), pLon.X - 1, pLon.Y - 1, 4, 4);
            }

        }



        private void PaintObjects(Graphics g)
        {
            switch (options.ViewStyle)
            {
                case EViewStyle.Panchanga:
                case EViewStyle.Normal:
                case EViewStyle.Varnada:
                    PaintNormalView(g);
                    break;
                case EViewStyle.DualGrahaArudha:
                    PaintDualGrahaArudhasView(g);
                    break;
                case EViewStyle.CharaKarakas7:
                case EViewStyle.CharaKarakas8:
                    PaintCharaKarakas(g);
                    break;
            }
        }

        private readonly string[] karakas_s = new string[] { "AK", "AmK", "BK", "MK", "PiK", "PuK", "JK", "DK" };
        private readonly string[] karakas_s7 = new string[] { "AK", "AmK", "BK", "MK", "PiK", "JK", "DK"    };


        private void PaintCharaKarakas(Graphics g)
        {
            int[] nItems = new int[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ArrayList al = new ArrayList();

            // number of karakas to display
            int max = 0;
            if (options.ViewStyle == EViewStyle.CharaKarakas7)
                max = (int)BodyName.Saturn;
            else
                max = (int)BodyName.Rahu;

            // determine karakas
            for (int i = (int)BodyName.Sun; i <= max; i++)
            {
                BodyName b = (BodyName)i;
                BodyPosition bp = h.GetPosition(b);
                BodyKarakaComparer bkc = new BodyKarakaComparer(bp);
                al.Add(bkc);
            }
            al.Sort();

            int[] kindex = new int[max + 1];
            for (int i = 0; i <= max; i++)
            {
                BodyPosition bp = ((BodyKarakaComparer)al[i]).GetPosition;
                kindex[(int)bp.name] = i;
            }


            // display bodies
            for (int i = 0; i <= max; i++)
            {
                BodyName b = (BodyName)i;
                DivisionPosition dp = (DivisionPosition)div_pos[i];
                ZodiacHouse zh = dp.ZodiacHouse;
                int j = (int)zh.Value;
                nItems[j]++;
                if (options.ViewStyle == EViewStyle.CharaKarakas7)
                    AddItem(g, zh, nItems[j], dp, true, karakas_s7[kindex[i]]);
                else
                    AddItem(g, zh, nItems[j], dp, true, karakas_s[kindex[i]]);

            }

            DivisionPosition dp2 = (DivisionPosition)div_pos[(int)BodyName.Lagna];
            ZodiacHouse zh2 = dp2.ZodiacHouse;
            int j2 = (int)zh2.Value;
            nItems[j2]++;
            AddItem(g, zh2, nItems[j2], dp2, true);
        }
        private void PaintDualGrahaArudhasView(Graphics g)
        {
            int[] nItems = new int[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            DivisionPosition dpo;
            int i;
            dpo = h.GetPosition(BodyName.Lagna).ToDivisionPosition(options.Varga);
            i = (int)dpo.ZodiacHouse.Value;
            nItems[i]++;
            AddItem(g, dpo.ZodiacHouse, nItems[i], dpo, true);


            foreach (DivisionPosition dp in graha_arudha_pos)
            {
                i = (int)dp.ZodiacHouse.Value;
                nItems[i]++;
                AddItem(g, dp.ZodiacHouse, nItems[i], dp, true);
            }
        }

        private void PaintSAV(Graphics g)
        {
            if (true == PrintMode)
                return;

            if (false == PanchangAppOptions.Instance.VargaShowSAVVarga &&
                false == PanchangAppOptions.Instance.VargaShowSAVRasi)
                return;

            ZodiacHouse zh = new ZodiacHouse(ZodiacHouseName.Ari);
            Brush b = new SolidBrush(PanchangAppOptions.Instance.VargaSAVColor);
            Font f = PanchangAppOptions.Instance.GeneralFont;
            for (int i = 1; i <= 12; i++)
            {
                ZodiacHouse zhi = zh.Add(i);
                Point p = dc.GetSingleItemOffset(zhi);
                g.DrawString(sav_bindus[i - 1].ToString(), f, b, p.X, p.Y);
            }
        }
        private void PaintNormalView(Graphics g)
        {
            int[] nItems = new int[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            BodyName[] bItems = new BodyName[10]
             {
                 BodyName.Lagna, BodyName.Sun, BodyName.Moon,
                 BodyName.Mars, BodyName.Mercury, BodyName.Jupiter,
                 BodyName.Venus, BodyName.Saturn, BodyName.Rahu,
                 BodyName.Ketu
             };

#if DDD
			foreach (ZodiacHouse.Name _zh in ZodiacHouse.AllNames)
			{
				ZodiacHouse zh = new ZodiacHouse(_zh);
				for (int i=1; i<9; i++)
				{
					DivisionPosition dp = new DivisionPosition(Body.Name.Jupiter,
						BodyType.Name.Graha, zh, 0.0, 0.0);
					AddItem (g, zh, i, dp, true);
				}
				for (int i=1; i<=6; i++)
				{
					DivisionPosition dp = new DivisionPosition(Body.Name.A11, 
											BodyType.Name.BhavaArudha, zh, 0.0, 0.0);
					AddItem (g, zh, i, dp, false);
				}

			}
#endif

            foreach (DivisionPosition dp in div_pos)
            {
                if (options.ViewStyle == EViewStyle.Panchanga &&
                    dp.Type != BodyType.Name.Graha)
                    continue;

                if (dp.Type != BodyType.Name.Graha && dp.Type != BodyType.Name.Lagna)
                    continue;
                ZodiacHouse zh = dp.ZodiacHouse;
                int i = (int)zh.Value;
                nItems[i]++;
                AddItem(g, zh, nItems[i], dp, true);
            }


            if (options.ViewStyle == EViewStyle.Panchanga)
                return;

            foreach (DivisionPosition dp in div_pos)
            {
                if (dp.Type != BodyType.Name.SpecialLagna)
                    continue;
                ZodiacHouse zh = dp.ZodiacHouse;
                int i = (int)zh.Value;
                nItems[i]++;
                AddItem(g, zh, nItems[i], dp, true);
            }
            for (int k = 1; k < 13; k++)
                nItems[k] = 0;

            ArrayList secondary_pos = null;

            if (options.ViewStyle == EViewStyle.Normal)
                secondary_pos = arudha_pos;
            else
                secondary_pos = varnada_pos;

            foreach (DivisionPosition dp in secondary_pos)
            {
                ZodiacHouse zh = dp.ZodiacHouse;
                int i = (int)zh.Value;
                nItems[i]++;
                AddItem(g, zh, nItems[i], dp, false);
            }

        }

        private void DivisionalChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawChart(g);
        }

        public void DrawChart(Graphics g)
        {
            DrawChart(g, Width, Height);
        }
        public void DrawChart(Graphics g, int width, int height)
        {
            DrawChart(g, width, height, false);
        }
        public void DrawChart(Graphics g, int width, int height, bool bDrawInner)
        {
            Font f = PanchangAppOptions.Instance.VargaFont;
            //this.BackColor = System.Drawing.Color.White;
            if (width == 0 || height == 0)
                return;


            int xw = dc.GetLength();
            int yw = dc.GetLength();

            int off = 5;

            if (bInnerMode)
                off = 0;

            int m_wh = Math.Min(height, width) - 2 * off - off;
            float scale_x = ((float)width - 2 * off) / xw;
            float scale_y = ((float)height - 2 * off) / yw;
            float scale = m_wh / (float)xw;

            if (false == PrintMode && false == bDrawInner)
                g.Clear(BackColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TranslateTransform(off, off);
            if (PanchangAppOptions.Instance.VargaChartIsSquare)
            {
                scale_x = scale;
                scale_y = scale;
            }

            g.ScaleTransform(scale_x, scale_y);

            if (false == PrintMode)
            {
                Brush bg = new SolidBrush(PanchangAppOptions.Instance.VargaBackgroundColor);
                g.FillRectangle(bg, 0, 0, xw, yw);
            }
            dc.DrawOutline(g);
            if (innerControl != null)
            {
                Point ptInner = dc.GetInnerSquareOffset();
                int length = dc.GetLength() - ptInner.X * 2;
                innerControl.Left = (int)(ptInner.X * scale_x) + off;
                innerControl.Top = (int)(ptInner.X * scale_y) + off;
                innerControl.Width = (int)(length * scale_x);
                innerControl.Height = (int)(length * scale_y);
                innerControl.Anchor = AnchorStyles.Left;
            }

            PaintSAV(g);

            PaintObjects(g);

            string s_dtype = Basics.NumPartsInDivisionString(options.Varga);
            //string.Format("D-{0}", Basics.numPartsInDivision(options.Varga));
            SizeF hint = g.MeasureString(s_dtype, f);
            g.DrawString(s_dtype, f, Brushes.Black, xw * 2 / 4 - hint.Width / 2, yw * 2 / 4 - hint.Height / 2);

            s_dtype = Basics.VariationNameOfDivision(options.Varga);
            hint = g.MeasureString(s_dtype, f);
            g.DrawString(s_dtype, f, Brushes.Black, xw * 2 / 4 - hint.Width / 2, yw * 2 / 4 - f.Height - hint.Height / 2);

            if (options.ChartStyle == EChartStyle.SouthIndian &&
                true == PanchangAppOptions.Instance.VargaShowDob &&
                false == PrintMode && false == bDrawInner)
            {
                string tob = h.Info.tob.ToString();
                hint = g.MeasureString(tob, f);
                g.DrawString(tob, f, Brushes.Black, xw * 2 / 4 - hint.Width / 2, (float)(yw * 2 / 4 - hint.Height / 2 + f.Height * 1.5));

                string latlon = h.Info.lat.ToString() + " " + h.Info.lon.ToString();
                hint = g.MeasureString(latlon, f);
                g.DrawString(latlon, f, Brushes.Black, xw * 2 / 4 - hint.Width / 2, (float)(yw * 2 / 4 - hint.Height / 2 + f.Height * 2.5));

                hint = g.MeasureString(h.Info.name, f);
                g.DrawString(h.Info.name, f, Brushes.Black, xw * 2 / 4 - hint.Width / 2, (float)(yw * 2 / 4 - hint.Height / 2 - f.Height * 1.5));
            }


            /*
			ZodiacHouse zh = new ZodiacHouse(ZodiacHouse.Name.Sco);
			for (int i=1; i<9; i++)
				AddItem(g, zh, i, new D, true);

			for (int i=1; i<=12; i++) 
			{
				ZodiacHouse zh = new ZodiacHouse((ZodiacHouse.Name)i);
				AddItem (g, zh, 9, zh.value.ToString());
			}
			*/
            Update();

        }

        private void DivisionalChart_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void SetChartStyle(EChartStyle cs)
        {
            switch (cs)
            {
                case EChartStyle.EastIndian:
                    dc = new EastIndianChart();
                    return;
                case EChartStyle.SouthIndian:
                default:
                    dc = new SouthIndianChart();
                    return;
            }
        }
        public object SetOptions(object o)
        {

            UserOptions uo = (UserOptions)o;
            if (uo.ChartStyle != options.ChartStyle)
                SetChartStyle(uo.ChartStyle);
            options = uo;
            OnRecalculate(h);

            return options.Clone();
        }
        private void mOptions_Click(object sender, EventArgs e)
        {
            Form f = new Options(options.Clone(), new ApplyOptions(SetOptions));
            f.ShowDialog();
        }

        private void CalculateBindus()
        {
            if (PanchangAppOptions.Instance.VargaShowSAVVarga)
                sav_bindus = new Ashtakavarga(h, options.Varga).GetSav();
            else if (PanchangAppOptions.Instance.VargaShowSAVRasi)
                sav_bindus = new Ashtakavarga(h, new Division(DivisionType.Rasi)).GetSav();
        }
        private void OnRedisplay(object o)
        {
            SetChartStyle(PanchangAppOptions.Instance.VargaStyle);
            options.ChartStyle = PanchangAppOptions.Instance.VargaStyle;
            fBase = new Font(
                PanchangAppOptions.Instance.VargaFont.FontFamily,
                PanchangAppOptions.Instance.VargaFont.SizeInPoints);
            CalculateBindus();
            Invalidate();
        }

        private void OnRecalculate(object o)
        {
            div_pos = h.CalculateDivisionPositions(options.Varga);
            arudha_pos = h.CalculateArudhaDivisionPositions(options.Varga);
            varnada_pos = h.CalculateVarnadaDivisionPositions(options.Varga);
            graha_arudha_pos = h.CalculateGrahaArudhaDivisionPositions(options.Varga);
            CalculateBindus();
            Invalidate();
        }

        private void DivisionalChart_DragDrop(object sender, DragEventArgs e)
        {

            if (options.ShowInner == true &&
                e.Data.GetDataPresent(typeof(DivisionalChart)))
            {
                Division div = Division.CopyFromClipboard();
                if (null == div) return;
                if (innerControl == null)
                    AddInnerControl();
                innerControl.options.Varga = div;
                innerControl.OnRecalculate(innerControl.h);
                Invalidate();
            }
        }

        private void DivisionalChart_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void mRasi_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Rasi);
            OnRecalculate(h);
            Invalidate();
        }

        private void mNavamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Navamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mBhava_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaPada);
            OnRecalculate(h);
            Invalidate();
        }
        private void mBhavaEqual_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaEqual);
            OnRecalculate(h);
            Invalidate();
        }
        private void mBhavaSripati_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaSripati);
            OnRecalculate(h);
            Invalidate();
        }
        private void mBhavaKoch_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaKoch);
            OnRecalculate(h);
            Invalidate();
        }
        private void mBhavaPlacidus_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaPlacidus);
            OnRecalculate(h);
            Invalidate();
        }
        private void menuBhavaAlcabitus_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaAlcabitus);
            OnRecalculate(h);
            Invalidate();
        }
        private void menuBhavaCampanus_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaCampanus);
            OnRecalculate(h);
            Invalidate();
        }
        private void menuBhavaRegiomontanus_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaRegiomontanus);
            OnRecalculate(h);
            Invalidate();
        }
        private void menuBhavaAxial_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.BhavaAxial);
            OnRecalculate(h);
            Invalidate();
        }
        private void mDrekkanaParasara_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.DrekkanaParasara);
            OnRecalculate(h);
            Invalidate();
        }

        private void mChaturamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Chaturthamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mPanchamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Panchamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mShashtamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Shashthamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mSaptamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Saptamsa);
            OnRecalculate(h);
            Invalidate();
        }
        private void mAshtamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Ashtamsa);
            OnRecalculate(h);
            Invalidate();
        }
        private void mAshtamsaRaman_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.AshtamsaRaman);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDasamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Dasamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDwadasamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Dwadasamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mShodasamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Shodasamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mVimsamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Vimsamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mChaturvimsamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Chaturvimsamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mNakshatramsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Nakshatramsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mTrimsamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Trimsamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mKhavedamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Khavedamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDrekkanaJagannath_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.DrekkanaJagannath);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDrekkanaSomnath_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.DrekkanaSomnath);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDrekkanaParivrittitraya_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.DrekkanaParivrittitraya);
            OnRecalculate(h);
            Invalidate();
        }

        private void mHoraKashinath_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.HoraKashinath);
            OnRecalculate(h);
            Invalidate();
        }

        private void mHoraParivritti_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.HoraParivrittiDwaya);
            OnRecalculate(h);
            Invalidate();
        }

        private void mHoraParasara_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.HoraParasara);
            OnRecalculate(h);
            Invalidate();
        }

        private void mHoraJagannath_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.HoraJagannath);
            OnRecalculate(h);
            Invalidate();
        }

        private void mTrimsamsaParivritti_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.TrimsamsaParivritti);
            OnRecalculate(h);
            Invalidate();
        }
        private void mTrimsamsaSimple_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.TrimsamsaSimple);
            OnRecalculate(h);
            Invalidate();
        }


        private void mLagnaChange_Click(object sender, EventArgs e)
        {
            VargaRectificationForm vf = new VargaRectificationForm(h, this, (Division)options.Varga);
            vf.Show();
        }

        private void mExtrapolate_Click(object sender, EventArgs e)
        {

            foreach (BodyPosition bp in h.PositionList)
            {
                DivisionPosition dp = bp.ToDivisionPosition(options.Varga);
                Longitude lLower = new Longitude(dp.CuspLower);
                Longitude lOffset = bp.Longitude.Subtract(lLower);
                Longitude lRange = new Longitude(dp.CuspHigher).Subtract(lLower);
                Trace.Assert(lOffset.Value <= lRange.Value, "Extrapolation internal error: Slice smaller than range. Weird.");

                double newOffset = lOffset.Value / lRange.Value * 30.0;
                double newBase = ((int)dp.ZodiacHouse.Value - 1) * 30.0;
                bp.Longitude = new Longitude(newOffset + newBase);
            }

            h.OnlySignalChanged();

        }
        private void mAkshavedamsa_Click_1(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Akshavedamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mShashtyamsa_Click_1(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Shashtyamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRudramsaRath_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Rudramsa);
            OnRecalculate(h);
            Invalidate();
        }
        private void mRudramsaRaman_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.RudramsaRaman);
            OnRecalculate(h);
            Invalidate();
        }
        private void mNadiamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Nadiamsa);
            OnRecalculate(h);
            Invalidate();
        }
        private void mNadiamsaCKN_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.NadiamsaCKN);
            OnRecalculate(h);
            Invalidate();
        }
        private void mNavamsaDwadasamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.NavamsaDwadasamsa);
            OnRecalculate(h);
            Invalidate();
        }

        private void mDwadasamsaDwadasamsa_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.DwadasamsaDwadasamsa);
            OnRecalculate(h);
            Invalidate();
        }
        private void menuItem7_Click(object sender, EventArgs e)
        {
            options.Varga = new Division(DivisionType.Ashtottaramsa);
            OnRecalculate(h);
            Invalidate();

        }
        private void mRegularParivritti_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericParivritti,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);

            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularFromHouse_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericDwadasamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);

            OnRecalculate(h);
            Invalidate();
        }


        private void mRegularTrikona_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericDrekkana,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }
        protected override void copyToClipboard()
        {
            Graphics displayGraphics = CreateGraphics();
            int size = Math.Min(Width, Height);
            Bitmap bmpBuffer = new Bitmap(size, size, displayGraphics);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            DrawChart(imageGraphics);
            displayGraphics.Dispose();
            Clipboard.SetDataObject(bmpBuffer, true);
            imageGraphics.Dispose();
        }

        private void DivisionalChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Copy);
                Clipboard.SetDataObject("");
            }
            else
            {
                contextMenu.Show(this, new Point(e.X, e.Y)); ;
            }

        }

        private void mViewNormal_Click(object sender, EventArgs e)
        {
            options.ViewStyle = EViewStyle.Normal;
            Invalidate();
        }

        private void mViewDualGrahaArudha_Click(object sender, EventArgs e)
        {
            options.ViewStyle = EViewStyle.DualGrahaArudha;
            Invalidate();
        }

        private void mViewCharaKarakas_Click(object sender, EventArgs e)
        {
            options.ViewStyle = EViewStyle.CharaKarakas8;
            Invalidate();
        }

        private void mViewCharaKarakas7_Click(object sender, EventArgs e)
        {
            options.ViewStyle = EViewStyle.CharaKarakas7;
            Invalidate();
        }

        private void mViewVarnada_Click(object sender, EventArgs e)
        {
            options.ViewStyle = EViewStyle.Varnada;
            Invalidate();
        }

        private void mRegularKendraChaturthamsa_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericChaturthamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularSaptamsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericSaptamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularDasamsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericDasamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularShashthamsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericShashthamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularShodasamsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericShodasamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularVimsamsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericVimsamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void mRegularNakshatramsaBased_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericNakshatramsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            panchang.Division.SingleDivision single = new panchang.Division.SingleDivision(DivisionType.GenericChaturvimsamsa,
                Basics.NumPartsInDivision(options.Varga));
            options.Varga = new Division(single);
            OnRecalculate(h);
            Invalidate();
        }

        private void DivisionalChart_MouseLeave(object sender, EventArgs e)
        {
            //if (e
            //Division.CopyToClipboard(this.options.Varga);
            //this.DoDragDrop(this, DragDropEffects.Copy);

        }

        private void DivisionalChart_DragLeave(object sender, EventArgs e)
        {
            Division.CopyToClipboard((Division)options.Varga);
        }


    }


    public interface IDrawChart
    {
        Point GetDegreeOffset(Longitude l);
        Point GetSingleItemOffset(ZodiacHouse zh);
        Point GetItemOffset(ZodiacHouse zh, int n);
        Point GetSmallItemOffset(ZodiacHouse zh, int n);
        Point GetInnerSquareOffset();
        void DrawOutline(Graphics g);
        int GetLength();
    }


    public class EastIndianChart : IDrawChart
    {
        const int xw = 200;
        const int yw = 200;
        Pen pn_black;

        public EastIndianChart()
        {
            pn_black = new Pen(Color.Black, (float)0.1);
        }

        public int GetLength()
        {
            return xw;
        }
        public void DrawOutline(Graphics g)
        {
            g.DrawLine(pn_black, xw / 3, 0, xw / 3, yw);
            g.DrawLine(pn_black, xw * 2 / 3, 0, xw * 2 / 3, yw);
            g.DrawLine(pn_black, 0, yw / 3, xw, yw / 3);
            g.DrawLine(pn_black, 0, yw * 2 / 3, xw, yw * 2 / 3);
            g.DrawLine(pn_black, xw / 3, yw / 3, 0, 0);
            g.DrawLine(pn_black, xw * 2 / 3, yw / 3, xw, 0);
            g.DrawLine(pn_black, xw / 3, yw * 2 / 3, 0, yw);
            g.DrawLine(pn_black, xw * 2 / 3, yw * 2 / 3, xw, yw);
        }
        public Point GetInnerSquareOffset()
        {
            return new Point(xw / 3, yw / 3);
        }

        public Point GetZhouseOffset(ZodiacHouse zh)
        {
            int iOff = xw / 3;
            switch (zh.Value)
            {
                case ZodiacHouseName.Ari: return new Point(iOff * 2, 0);
                case ZodiacHouseName.Tau: return new Point(iOff, 0);
                case ZodiacHouseName.Gem: return new Point(0, 0);
                case ZodiacHouseName.Can: return new Point(0, iOff);
                case ZodiacHouseName.Leo: return new Point(0, iOff * 2);
                case ZodiacHouseName.Vir: return new Point(0, iOff * 3);
                case ZodiacHouseName.Lib: return new Point(iOff, iOff * 3);
                case ZodiacHouseName.Sco: return new Point(iOff * 2, iOff * 3);
                case ZodiacHouseName.Sag: return new Point(iOff * 3, iOff * 3);
                case ZodiacHouseName.Cap: return new Point(iOff * 3, iOff * 2);
                case ZodiacHouseName.Aqu: return new Point(iOff * 3, iOff);
                case ZodiacHouseName.Pis: return new Point(iOff * 3, 0);
            }
            return new Point(0, 0);
        }
        public Point GetDegreeOffset(Longitude l)
        {
            ZodiacHouseName zh = l.ToZodiacHouse().Value;
            double dOffset = l.ToZodiacHouseOffset();
            int iOff = (int)(dOffset / 30.0 * (xw / 3));
            Point pBase = GetZhouseOffset(l.ToZodiacHouse());
            switch (zh)
            {
                case ZodiacHouseName.Pis:
                case ZodiacHouseName.Ari:
                case ZodiacHouseName.Tau:
                    pBase.X -= iOff; break;
                case ZodiacHouseName.Gem:
                case ZodiacHouseName.Can:
                case ZodiacHouseName.Leo:
                    pBase.Y += iOff; break;
                case ZodiacHouseName.Vir:
                case ZodiacHouseName.Lib:
                case ZodiacHouseName.Sco:
                    pBase.X += iOff; break;
                case ZodiacHouseName.Sag:
                case ZodiacHouseName.Cap:
                case ZodiacHouseName.Aqu:
                    pBase.Y -= iOff; break;
            }
            return pBase;
        }

        public Point GetGemOffset(int n)
        {
            int wi = xw / 3 / 4;
            int yi = yw / 3 / 6;
            switch (n)
            {
                case 4: return new Point(0, yi * 4);
                case 3: return new Point(wi * 1, yi * 4);
                case 8: return new Point(wi * 2, yi * 4);
                case 1: return new Point(0, yi * 3);
                case 5: return new Point(wi * 1, yi * 3);
                case 2: return new Point(0, yi * 2);
                case 6: return new Point(wi * 1 - 4, yi * 2);
                case 7: return new Point(0, yi * 1);
            }
            return GetGemOffset(1);
        }
        public Point GetSmallGemOffset(int n)
        {
            int wi = xw / 3 / 5;
            int yi = xw / 3 / 6;
            switch (n)
            {
                case 4: return new Point(0, yi * 5);
                case 1: return new Point(wi * 1, yi * 5);
                case 3: return new Point(wi * 2, yi * 5);
                case 2: return new Point(wi * 3, yi * 5);
                case 5: return new Point(wi * 4, yi * 5);

            }
            return new Point(100, 100);
        }
        public Point GetSingleGemOffset()
        {
            return new Point(xw / 3 / 4, xw / 3 * 2 / 3);
        }
        public Point GetSingleItemOffset(ZodiacHouse zh)
        {
            switch (zh.Value)
            {
                case ZodiacHouseName.Ari: return new Point(90, 0);
                case ZodiacHouseName.Can: return new Point(5, 90);
                case ZodiacHouseName.Lib: return new Point(90, 185);
                case ZodiacHouseName.Cap: return new Point(180, 90);
                default:
                    Point pret = GetSingleGemOffset();
                    return FromGemOffset(zh, pret);
            }
        }
        public Point GetItemOffset(ZodiacHouse zh, int n)
        {
            Point pret = GetGemOffset(n);
            return FromGemOffset(zh, pret);
        }
        public Point FromGemOffset(ZodiacHouse zh, Point pret)
        {
            int wi = xw / 3 / 4;
            int yi = yw / 3 / 6;
            switch (zh.Value)
            {
                case ZodiacHouseName.Gem:
                    return pret;
                case ZodiacHouseName.Aqu:
                    pret.X = xw - pret.X - wi;
                    return pret;
                case ZodiacHouseName.Leo:
                    pret.Y = yw - pret.Y - yi;
                    return pret;
                case ZodiacHouseName.Sag:
                    pret.X = xw - pret.X - wi;
                    pret.Y = yw - pret.Y - yi;
                    return pret;
                case ZodiacHouseName.Pis:
                    pret.X += xw * 2 / 3;
                    pret.Y = yw / 3 - pret.Y - yi;
                    return pret;
                case ZodiacHouseName.Tau:
                    pret.X = xw / 3 - pret.X - wi;
                    pret.Y = yw / 3 - pret.Y - yi;
                    return pret;
                case ZodiacHouseName.Vir:
                    pret.X = xw / 3 - pret.X - wi;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Sco:
                    pret.X += xw * 2 / 3;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Ari:
                    pret.X += xw / 3;
                    return pret;
                case ZodiacHouseName.Can:
                    pret.Y += yw / 3;
                    return pret;
                case ZodiacHouseName.Lib:
                    pret.X += xw / 3;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Cap:
                    pret.X += xw * 2 / 3;
                    pret.Y += yw / 3;
                    return pret;

            }
            return new Point(100, 100);
        }
        public Point GetSmallItemOffset(ZodiacHouse zh, int n)
        {
            int wi = xw / 3 / 5;
            //int yi = (xw/3)/6;
            Point pret;
            switch (zh.Value)
            {
                case ZodiacHouseName.Gem:
                    return GetSmallGemOffset(n);
                case ZodiacHouseName.Tau:
                    pret = GetSmallGemOffset(n);
                    pret.Y = 0;
                    pret.X = xw / 3 - pret.X - wi;
                    return pret;
                case ZodiacHouseName.Pis:
                    pret = GetSmallGemOffset(n);
                    pret.Y = 0;
                    pret.X += xw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Aqu:
                    pret = GetSmallGemOffset(n);
                    pret.X = xw / 3 - pret.X + xw * 2 / 3 - wi;
                    return pret;
                case ZodiacHouseName.Vir:
                    pret = GetSmallGemOffset(n);
                    pret.X = xw / 3 - pret.X - wi;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Sco:
                    pret = GetSmallGemOffset(n);
                    pret.X += xw * 2 / 3;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Sag:
                    pret = GetSmallGemOffset(n);
                    pret.Y = yw * 2 / 3;
                    pret.X = xw / 3 - pret.X + xw * 2 / 3 - wi;
                    return pret;
                case ZodiacHouseName.Leo:
                    pret = GetSmallGemOffset(n);
                    pret.Y = yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Ari:
                    pret = GetSmallGemOffset(n);
                    pret.X += xw / 3;
                    return pret;
                case ZodiacHouseName.Can:
                    pret = GetSmallGemOffset(n);
                    pret.Y += yw / 3;
                    return pret;
                case ZodiacHouseName.Lib:
                    pret = GetSmallGemOffset(n);
                    pret.X += xw / 3;
                    pret.Y += yw * 2 / 3;
                    return pret;
                case ZodiacHouseName.Cap:
                    pret = GetSmallGemOffset(n);
                    pret.X += xw * 2 / 3;
                    pret.Y += yw / 3;
                    return pret;
            }
            return new Point(100, 100);
        }
    }


    public class SouthIndianChart : IDrawChart
    {
        const int xw = 200;
        const int yw = 200;
        const int xo = 0;
        const int yo = 0;

        const int hxs = 200;
        const int hys = 125;
        const int hsys = 75;
        Pen pn_black;

        public SouthIndianChart()
        {
            pn_black = new Pen(Color.Black, (float)0.1);
        }

        public Point GetInnerSquareOffset()
        {
            return new Point(xw / 4, yw / 4);
        }
        public int GetLength()
        {
            return xw;
        }

        public Point GetDegreeOffset(Longitude l)
        {
            ZodiacHouseName zh = l.ToZodiacHouse().Value;
            double dOffset = l.ToZodiacHouseOffset();
            int iOff = (int)(dOffset / 30.0 * (xw / 4));
            Point pBase = GetZhouseOffset(l.ToZodiacHouse());
            switch (zh)
            {
                case ZodiacHouseName.Ari:
                case ZodiacHouseName.Tau:
                case ZodiacHouseName.Gem:
                    pBase.X += iOff; break;
                case ZodiacHouseName.Can:
                case ZodiacHouseName.Leo:
                case ZodiacHouseName.Vir:
                    pBase.X += xw / 4;
                    pBase.Y += iOff;
                    break;
                case ZodiacHouseName.Lib:
                case ZodiacHouseName.Sco:
                case ZodiacHouseName.Sag:
                    pBase.X += xw / 4 - iOff;
                    pBase.Y += xw / 4;
                    break;
                case ZodiacHouseName.Cap:
                case ZodiacHouseName.Aqu:
                case ZodiacHouseName.Pis:
                    pBase.Y += xw / 4 - iOff; break;
            }
            return pBase;
        }


        public void DrawOutline(Graphics g)
        {
            g.DrawLine(pn_black, xo, yo + 0, xo + 0, yo + yw);
            g.DrawLine(pn_black, xo, yo + 0, xo + xw, yo + 0);
            g.DrawLine(pn_black, xo + xw, yo + yw, xo + 0, yo + yw);
            g.DrawLine(pn_black, xo + xw, yo + yw, xo + xw, yo + 0);
            g.DrawLine(pn_black, xo, yo + yw / 4, xo + xw, yo + yw / 4);
            g.DrawLine(pn_black, xo, yo + yw * 3 / 4, xo + xw, yo + yw * 3 / 4);
            g.DrawLine(pn_black, xo + xw / 4, yo, xo + xw / 4, yo + yw);
            g.DrawLine(pn_black, xo + xw * 3 / 4, yo, xo + xw * 3 / 4, yo + yw);
            g.DrawLine(pn_black, xo + xw / 2, yo, xo + xw / 2, yo + yw / 4);
            g.DrawLine(pn_black, xo + xw / 2, yo + yw * 3 / 4, xo + xw / 2, yo + yw);
            g.DrawLine(pn_black, xo, yo + yw / 2, xo + xw / 4, yo + yw / 2);
            g.DrawLine(pn_black, xo + xw * 3 / 4, yo + yw / 2, xo + xw, yo + yw / 2);
        }
        public Point GetSingleItemOffset(ZodiacHouse zh)
        {
            Point p = GetZhouseOffset(zh);
            return new Point(p.X + 15, p.Y + 15);
        }
        public Point GetItemOffset(ZodiacHouse zh, int n)
        {
            Point p = GetZhouseOffset(zh);
            Point q = GetZhouseItemOffset(n);
            return new Point(p.X + q.X, p.Y + q.Y);
        }
        public Point GetSmallItemOffset(ZodiacHouse zh, int n)
        {
            Point p = GetZhouseOffset(zh);
            Point q = GetSmallZhouseItemOffset(n);
            return new Point(p.X + q.X, p.Y + q.Y);
        }
        private Point GetSmallZhouseItemOffset(int n)
        {
            if (n >= 7)
            {
                Debug.WriteLine("South Indian Chart (s) is too small for data");
                return new Point(0, 0);
            }
            int[] item_map = new int[7] { 0, 6, 2, 3, 4, 2, 1 };
            n = item_map[n - 1];

            int xiw = hxs / 4;
            int yiw = hsys / 6;

            int row = (int)Math.Floor(n / (double)3);
            int col = n - row * 3;

            return new Point(xiw * row / 3, hys / 4 + yiw * col / 3);

        }
        private Point GetZhouseItemOffset(int n)
        {
            if (n >= 10)
            {
                Debug.WriteLine("South Indian Chart is too small for data");
                return GetSmallZhouseItemOffset(n - 10 + 1);
            }
            int[] item_map = new int[10] { 0, 5, 7, 9, 3, 1, 2, 4, 6, 8 };
            n = item_map[n] - 1;

            int xiw = hxs / 4;
            int yiw = hys / 4;

            int col = (int)Math.Floor(n / (double)3);
            int row = n - col * 3;

            return new Point(xiw * row / 3, yiw * col / 3);
        }
        private Point GetZhouseOffset(ZodiacHouse zh)
        {
            switch ((int)zh.Value)
            {
                case 1: return new Point(xo + xw / 4, yo + 0);
                case 2: return new Point(xo + xw * 2 / 4, yo + 0);
                case 3: return new Point(xo + xw * 3 / 4, yo + 0);
                case 4: return new Point(xo + xw * 3 / 4, yo + yw / 4);
                case 5: return new Point(xo + xw * 3 / 4, yo + yw * 2 / 4);
                case 6: return new Point(xo + xw * 3 / 4, yo + yw * 3 / 4);
                case 7: return new Point(xo + xw * 2 / 4, yo + yw * 3 / 4);
                case 8: return new Point(xo + xw / 4, yo + yw * 3 / 4);
                case 9: return new Point(xo + 0, yo + yw * 3 / 4);
                case 10: return new Point(xo + 0, yo + yw * 2 / 4);
                case 11: return new Point(xo + 0, yo + yw * 1 / 4);
                case 12: return new Point(xo + 0, yo);
            }
            return new Point(0, 0);
        }

    }

}
