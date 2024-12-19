using org.transliteral.panchang;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{


    public class PanchangControl : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;


        protected Horoscope h;
        protected Splitter sp;

        public Horoscope ControlHoroscope
        {
            get { return h; }
            set { h = value; }
        }
       
        public PanchangControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
           
            AutoScroll = true;
            Name = "PanchangControl";
            Size = new Size(360, 216);
            Load += new EventHandler(PanchangControl_Load);

        }
        #endregion


        public void ViewControl(PanchangControlContainer.BaseUserOptions.ViewType vt)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(vt);
        }
        protected void ViewVimsottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaVimsottari);
        }
        protected void ViewYogaVimsottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaYogaVimsottari);
        }
        protected void ViewKaranaChaturashitiSamaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaKaranaChaturashitiSama);
        }
        protected void ViewAshtottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaAshtottari);
        }
        protected void ViewTithiPraveshAshtottariDasaTithi(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiPraveshAshtottariCompressedTithi);
        }
        protected void ViewTithiPraveshAshtottariDasaSolar(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiPraveshAshtottariCompressedSolar);
        }
        protected void ViewTithiPraveshAshtottariDasaFixed(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiPraveshAshtottariCompressedFixed);
        }
        protected void ViewTithiAshtottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiAshtottari);
        }
        protected void ViewYogaPraveshVimsottariDasaYoga(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaYogaPraveshVimsottariCompressedYoga);
        }
        protected void ViewShodashottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaShodashottari);
        }

        protected void ViewDwadashottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaDwadashottari);
        }

        protected void ViewPanchottariDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaPanchottari);
        }

        protected void ViewShatabdikaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaShatabdika);
        }

        protected void ViewChaturashitiSamaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaChaturashitiSama);
        }

        protected void ViewDwisaptatiSamaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaDwisaptatiSama);
        }

        protected void ViewShatTrimshaSamaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaShatTrimshaSama);
        }
        protected void ViewYoginiDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaYogini);
        }
        protected void ViewKalachakraDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaKalachakra);
        }
        protected void ViewNaisargikaGrahaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.NaisargikaGrahaDasa);
        }
        protected void ViewKarakaKendradiGrahaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaKarakaKendradiGraha);
        }
        protected void ViewMoolaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaMoola);
        }
        protected void ViewNaisargikaRasiDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.NaisargikaRasiDasa);
        }
        protected void ViewNarayanaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaNarayana);
        }
        protected void ViewNarayanaSamaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaNarayanaSama);
        }
        protected void ViewShoolaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaShoola);
        }
        protected void ViewNiryaanaShoolaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaNiryaanaShoola);
        }
        protected void ViewSuDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaSu);
        }
        protected void ViewNavamsaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaNavamsa);
        }
        protected void ViewMandookaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaMandooka);
        }
        protected void ViewCharaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaChara);
        }
        protected void ViewTrikonaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTrikona);
        }
        protected void ViewDrigDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaDrig);
        }
        protected void ViewSudarshanaChakraDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaSudarshanaChakra);
        }
        protected void ViewLagnaKendradiRasiDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaLagnaKendradiRasi);
        }
        protected void ViewSudarshanaChakraDasaCompressed(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaSudarshanaChakraCompressed);
        }
        protected void ViewMuddaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaMudda);
        }
        protected void ViewTajakaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTajaka);
        }
        protected void ViewTattwaDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTattwa);
        }
        protected void ControlCopyToClipboard(object sender, EventArgs e)
        {
            copyToClipboard();
        }
        protected virtual void copyToClipboard()
        {
        }
        protected void ViewTithiPraveshDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiPravesh);
        }
        protected void ViewYogaPraveshDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaYogaPravesh);
        }
        protected void ViewNakshatraPraveshDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaNakshatraPravesh);
        }
        protected void ViewKaranaPraveshDasa(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaKaranaPravesh);
        }
        protected void ViewKeyInfo(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.KeyInfo);
        }
        protected void ViewBasicCalculations(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.BasicCalculations);
        }
        protected void ViewDivisionalChart(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DivisionalChart);
        }
        protected void ViewBalas(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.Balas);
        }
        protected void ViewAshtakavarga(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.Ashtakavarga);
        }
        protected void ViewKutaMatching(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.KutaMatching);
        }
        protected void ViewNavamsaCircle(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.NavamsaCircle);
        }
        protected void ViewVaraChakra(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.VaraChakra);
        }
        protected void ViewSarvatobhadraChakra(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.ChakraSarvatobhadra81);
        }
        protected void ViewTransitsSearch(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.TransitSearch);
        }
        protected void ViewPanchanga(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.Panchanga);
        }
        protected void SplitViewHorizontal(object sender, EventArgs e)
        {
            PanchangControlContainer c_this = (PanchangControlContainer)Parent;
            SplitContainer c_grand = (SplitContainer)c_this.Parent;

            DivisionalChart dc1 = new DivisionalChart(h);
            PanchangControlContainer c_dc1 = new PanchangControlContainer(dc1);

            DivisionalChart dc2 = new DivisionalChart(h);
            PanchangControlContainer c_cd2 = new PanchangControlContainer(dc2);

            SplitContainer ns = new SplitContainer(c_dc1)
            {
                Control1 = c_cd2,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };
            c_grand.Control2 = ns;
            return;

           
        }
        protected void AddViewsToContextMenu(ContextMenu cmenu)
        {

            MenuItem mBasicsMenu = new MenuItem("Basic Info");
            mBasicsMenu.MenuItems.Add("Key Info", new EventHandler(ViewKeyInfo));
            mBasicsMenu.MenuItems.Add("Calculations", new EventHandler(ViewBasicCalculations));
            mBasicsMenu.MenuItems.Add("Divisional Chart", new EventHandler(ViewDivisionalChart));
            mBasicsMenu.MenuItems.Add("Balas", new EventHandler(ViewBalas));
            mBasicsMenu.MenuItems.Add("Ashtakavarga", new EventHandler(ViewAshtakavarga));

            MenuItem mChakrasMenu = new MenuItem("Chakras");
            mChakrasMenu.MenuItems.Add("Navamsa Chakra", new EventHandler(ViewNavamsaCircle));
            mChakrasMenu.MenuItems.Add("Vara Chakra", new EventHandler(ViewVaraChakra));
            mChakrasMenu.MenuItems.Add("Sarvatobhadra Chakra", new EventHandler(ViewSarvatobhadraChakra));

            MenuItem mNakDasaMenu = new MenuItem("Nakshatra Dasa");
            mNakDasaMenu.MenuItems.Add("Vimsottari Dasa", new EventHandler(ViewVimsottariDasa));
            mNakDasaMenu.MenuItems.Add("Ashottari Dasa", new EventHandler(ViewAshtottariDasa));
            mNakDasaMenu.MenuItems.Add("-");
            mNakDasaMenu.MenuItems.Add("Panchottari Dasa", new EventHandler(ViewPanchottariDasa));
            mNakDasaMenu.MenuItems.Add("Dwadashottari Dasa", new EventHandler(ViewDwadashottariDasa));
            mNakDasaMenu.MenuItems.Add("Shodashottari Dasa", new EventHandler(ViewShodashottariDasa));
            mNakDasaMenu.MenuItems.Add("Chaturashiti Sama Dasa", new EventHandler(ViewChaturashitiSamaDasa));
            mNakDasaMenu.MenuItems.Add("Dwisaptati Sama Dasa", new EventHandler(ViewDwisaptatiSamaDasa));
            mNakDasaMenu.MenuItems.Add("ShatTrimsha Sama Dasa", new EventHandler(ViewShatTrimshaSamaDasa));
            mNakDasaMenu.MenuItems.Add("Shatabdika Dasa", new EventHandler(ViewShatabdikaDasa));
            mNakDasaMenu.MenuItems.Add("Kalachakra Dasa", new EventHandler(ViewKalachakraDasa));
            mNakDasaMenu.MenuItems.Add("Yogini Dasa", new EventHandler(ViewYoginiDasa));
            mNakDasaMenu.MenuItems.Add("-");
            mNakDasaMenu.MenuItems.Add("Tithi Ashtottari Dasa", new EventHandler(ViewTithiAshtottariDasa));
            mNakDasaMenu.MenuItems.Add("Yoga Vimsottari Dasa", new EventHandler(ViewYogaVimsottariDasa));
            mNakDasaMenu.MenuItems.Add("Karana Chaturashiti Sama Dasa", new EventHandler(ViewKaranaChaturashitiSamaDasa));

            MenuItem mGrahaDasaMenu = new MenuItem("Graha Dasa");
            mGrahaDasaMenu.MenuItems.Add("Naisargika Dasa", new EventHandler(ViewNaisargikaGrahaDasa));
            mGrahaDasaMenu.MenuItems.Add("Moola Dasa", new EventHandler(ViewMoolaDasa));
            mGrahaDasaMenu.MenuItems.Add("Karaka Kendradi Dasa", new EventHandler(ViewKarakaKendradiGrahaDasa));

            MenuItem mRasiDasaMenu = new MenuItem("Rasi Dasa");
            mRasiDasaMenu.MenuItems.Add("Naisargika Dasa", new EventHandler(ViewNaisargikaRasiDasa));
            mRasiDasaMenu.MenuItems.Add("Narayana Dasa", new EventHandler(ViewNarayanaDasa));
            mRasiDasaMenu.MenuItems.Add("Narayana Sama Dasa", new EventHandler(ViewNarayanaSamaDasa));
            mRasiDasaMenu.MenuItems.Add("Shoola Dasa", new EventHandler(ViewShoolaDasa));
            mRasiDasaMenu.MenuItems.Add("Niryaana Shoola Dasa", new EventHandler(ViewNiryaanaShoolaDasa));
            mRasiDasaMenu.MenuItems.Add("Drig Dasa", new EventHandler(ViewDrigDasa));
            mRasiDasaMenu.MenuItems.Add("Su Dasa", new EventHandler(ViewSuDasa));
            mRasiDasaMenu.MenuItems.Add("Sudarshana Chakra Dasa", new EventHandler(ViewSudarshanaChakraDasa));
            mRasiDasaMenu.MenuItems.Add("Lagna Kendradi Rasi Dasa", new EventHandler(ViewLagnaKendradiRasiDasa));
            mRasiDasaMenu.MenuItems.Add("Navamsa Ayur Dasa", new EventHandler(ViewNavamsaDasa));
            mRasiDasaMenu.MenuItems.Add("Mandooka Dasa", new EventHandler(ViewMandookaDasa));
            mRasiDasaMenu.MenuItems.Add("Chara Dasa", new EventHandler(ViewCharaDasa));
            mRasiDasaMenu.MenuItems.Add("Trikona Dasa", new EventHandler(ViewTrikonaDasa));

            MenuItem mRelatedChartMenu = new MenuItem("Yearly Charts");
            mRelatedChartMenu.MenuItems.Add("Tajaka Chart", new EventHandler(ViewTajakaDasa));
            mRelatedChartMenu.MenuItems.Add("Sudarshana Chakra Dasa (Solar Year)", new EventHandler(ViewSudarshanaChakraDasaCompressed));
            mRelatedChartMenu.MenuItems.Add("Mudda Dasa (Solar Year)", new EventHandler(ViewMuddaDasa));
            mRelatedChartMenu.MenuItems.Add("-");
            mRelatedChartMenu.MenuItems.Add("Tithi Pravesh Chart", new EventHandler(ViewTithiPraveshDasa));
            mRelatedChartMenu.MenuItems.Add("Tithi Pravesh Ashtottari Dasa (Tithi Year)", new EventHandler(ViewTithiPraveshAshtottariDasaTithi));
            mRelatedChartMenu.MenuItems.Add("Tithi Pravesh Ashtottari Dasa (Solar Year)", new EventHandler(ViewTithiPraveshAshtottariDasaSolar));
            mRelatedChartMenu.MenuItems.Add("Tithi Pravesh Ashtottari Dasa (Fixed Year)", new EventHandler(ViewTithiPraveshAshtottariDasaFixed));
            mRelatedChartMenu.MenuItems.Add("-");
            mRelatedChartMenu.MenuItems.Add("Yoga Pravesh Chart", new EventHandler(ViewYogaPraveshDasa));
            mRelatedChartMenu.MenuItems.Add("Yoga Pravesh Vimsottari Dasa (Yoga Year)", new EventHandler(ViewYogaPraveshVimsottariDasaYoga));
            mRelatedChartMenu.MenuItems.Add("-");
            mRelatedChartMenu.MenuItems.Add("Nakshatra Pravesh Chart", new EventHandler(ViewNakshatraPraveshDasa));
            mRelatedChartMenu.MenuItems.Add("-");
            mRelatedChartMenu.MenuItems.Add("Karana Pravesh Chart", new EventHandler(ViewKaranaPraveshDasa));
            mRelatedChartMenu.MenuItems.Add("-");
            mRelatedChartMenu.MenuItems.Add("Tattwa Dasa", new EventHandler(ViewTattwaDasa));


            MenuItem mOtherMenu = new MenuItem("Other");
            mOtherMenu.MenuItems.Add("Kuta Matching", new EventHandler(ViewKutaMatching));
            mOtherMenu.MenuItems.Add("Transit Search", new EventHandler(ViewTransitsSearch));
            mOtherMenu.MenuItems.Add("Panchanga", new EventHandler(ViewPanchanga));
            //MenuItem mSplitMenu = new MenuItem ("Split View");
            //mSplitMenu.MenuItems.Add ("Split Horizontal", new EventHandler(SplitViewHorizontal));


            cmenu.MenuItems.Add(mBasicsMenu);
            cmenu.MenuItems.Add(mChakrasMenu);
            cmenu.MenuItems.Add(mNakDasaMenu);
            cmenu.MenuItems.Add(mGrahaDasaMenu);
            cmenu.MenuItems.Add(mRasiDasaMenu);
            cmenu.MenuItems.Add(mOtherMenu);
            cmenu.MenuItems.Add(mRelatedChartMenu);

            cmenu.MenuItems.Add("Copy To clipboard", new EventHandler(ControlCopyToClipboard));
            //cmenu.MenuItems.Add (mSplitMenu);

        }
        protected void PanchangControl_Load(object sender, EventArgs e)
        {
        }

        protected virtual void FontRows(ListView mList)
        {
            mList.ForeColor = GlobalOptions.Instance.TableForegroundColor;
            Font f = GlobalOptions.Instance.GeneralFont;
            //new Font ("Courier New", 10);
            mList.Font = f;
            foreach (ListViewItem li in mList.Items)
            {
                li.Font = f;
            }
        }
        protected virtual void ColorAndFontRows(ListView mList)
        {
            ColorRows(mList);
            FontRows(mList);
        }
        protected virtual void ColorRows(ListView mList)
        {
            Color[] cList = new Color[2];
            cList[1] = GlobalOptions.Instance.TableOddRowColor;
            cList[0] = GlobalOptions.Instance.TableEvenRowColor;
            //cList[1] = Color.WhiteSmoke;

            for (int i = 0; i < mList.Items.Count; i++)
            {
                if (i % 2 == 1) mList.Items[i].BackColor = cList[0];
                else mList.Items[i].BackColor = cList[1];
            }
            mList.BackColor = GlobalOptions.Instance.TableBackgroundColor;
        }

       

        private void mViewDasaVimsottari_Click(object sender, EventArgs e)
        {

        }

        private void mDasa_Click(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DasaVimsottari);
        }

        private void mDivisionalChart_Click(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).SetView(PanchangControlContainer.BaseUserOptions.ViewType.DivisionalChart);
        }
    }
}
