using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for JhoraMainTab.
    /// </summary>
    public class JhoraMainTab : PanchangControl
    {
        private TabControl mTab;
        private TabPage tabDasa;
        private TabPage tabBasics;
        private TabPage tabVargas;
        private ContextMenu contextMenu1;
        private TabPage tabTransits;

        bool bTabDasaLoaded = false;
        //bool bTabBasicsLoaded = false;
        bool bTabVargasLoaded = false;
        //bool bTabTajakaLoaded = false;
        bool bTabTithiPraveshLoaded = false;
        bool bTabTransitsLoaded = false;
        bool bTabPanchangaLoaded = false;

        private TabPage tabTithiPravesh;
        private TabPage tabPanchanga;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private void AddControlToTab(TabPage tab, PanchangControl mcontrol)
        {
            PanchangControlContainer container = new PanchangControlContainer(mcontrol)
            {
                Dock = DockStyle.Fill
            };
            tab.Controls.Add(container);
        }
        public JhoraMainTab(Horoscope _h)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            mTab.TabPages[0] = tabBasics;
            mTab.TabPages[1] = tabVargas;
            mTab.TabPages[2] = tabDasa;
            mTab.TabPages[3] = tabTransits;
            mTab.TabPages[4] = tabPanchanga;
            mTab.TabPages[5] = tabTithiPravesh;

            mTab.SelectedTab = tabBasics;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            h = _h;
            GlobalOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            OnRedisplay(GlobalOptions.Instance);

            AddControlToTab(tabBasics, new JhoraBasicsTab(h));
            //this.bTabBasicsLoaded = true;
        }

        public void OnRedisplay(object o)
        {
            Font = GlobalOptions.Instance.GeneralFont;
            /*
			this.tabBasics.Font = this.Font;
			this.tabDasa.Font = this.Font;
			this.tabPanchanga.Font = this.Font;
			this.tabTithiPravesh.Font = this.Font;
			this.tabTransits.Font = this.Font;
			this.tabVargas.Font = this.Font;
			*/
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mTab = new TabControl();
            tabBasics = new TabPage();
            tabTransits = new TabPage();
            tabDasa = new TabPage();
            tabVargas = new TabPage();
            tabPanchanga = new TabPage();
            tabTithiPravesh = new TabPage();
            contextMenu1 = new ContextMenu();
            mTab.SuspendLayout();
            SuspendLayout();
            // 
            // mTab
            // 
            mTab.Controls.Add(tabBasics);
            mTab.Controls.Add(tabDasa);
            mTab.Controls.Add(tabTransits);
            mTab.Controls.Add(tabVargas);
            mTab.Controls.Add(tabPanchanga);
            mTab.Controls.Add(tabTithiPravesh);
            mTab.Dock = DockStyle.Fill;
            mTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mTab.Location = new System.Drawing.Point(0, 0);
            mTab.Name = "mTab";
            mTab.Padding = new System.Drawing.Point(20, 4);
            mTab.SelectedIndex = 0;
            mTab.Size = new System.Drawing.Size(472, 256);
            mTab.TabIndex = 0;
            mTab.SelectedIndexChanged += new System.EventHandler(mTab_SelectedIndexChanged);
            // 
            // tabBasics
            // 
            tabBasics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            tabBasics.Location = new System.Drawing.Point(4, 25);
            tabBasics.Name = "tabBasics";
            tabBasics.Size = new System.Drawing.Size(464, 227);
            tabBasics.TabIndex = 1;
            tabBasics.Text = "Basics";
            tabBasics.Click += new System.EventHandler(tabBasics_Click);
            // 
            // tabTransits
            // 
            tabTransits.Location = new System.Drawing.Point(4, 25);
            tabTransits.Name = "tabTransits";
            tabTransits.Size = new System.Drawing.Size(464, 227);
            tabTransits.TabIndex = 4;
            tabTransits.Text = "Transits";
            tabTransits.Click += new System.EventHandler(tabTransits_Click);
            // 
            // tabDasa
            // 
            tabDasa.Location = new System.Drawing.Point(4, 25);
            tabDasa.Name = "tabDasa";
            tabDasa.Size = new System.Drawing.Size(464, 227);
            tabDasa.TabIndex = 0;
            tabDasa.Text = "Dasas";
            tabDasa.Click += new System.EventHandler(tabDasa_Click);
            // 
            // tabVargas
            // 
            tabVargas.Location = new System.Drawing.Point(4, 25);
            tabVargas.Name = "tabVargas";
            tabVargas.Size = new System.Drawing.Size(464, 227);
            tabVargas.TabIndex = 2;
            tabVargas.Text = "Varga";
            tabVargas.Click += new System.EventHandler(tabVargas_Click);
            // 
            // tabPanchanga
            // 
            tabPanchanga.Location = new System.Drawing.Point(4, 25);
            tabPanchanga.Name = "tabPanchanga";
            tabPanchanga.Size = new System.Drawing.Size(464, 227);
            tabPanchanga.TabIndex = 6;
            tabPanchanga.Text = "Panchanga";
            tabPanchanga.Click += new System.EventHandler(tabPanchanga_Click);
            // 
            // tabTithiPravesh
            // 
            tabTithiPravesh.Location = new System.Drawing.Point(4, 25);
            tabTithiPravesh.Name = "tabTithiPravesh";
            tabTithiPravesh.Size = new System.Drawing.Size(464, 227);
            tabTithiPravesh.TabIndex = 5;
            tabTithiPravesh.Text = "TithiPravesh";
            // 
            // JhoraMainTab
            // 
            Controls.Add(mTab);
            Name = "JhoraMainTab";
            Size = new System.Drawing.Size(472, 256);
            mTab.ResumeLayout(false);
            ResumeLayout(false);

        }
        #endregion


        private void tabBasics_Click(object sender, System.EventArgs e)
        {
        }

        private void tabTransits_Click(object sender, System.EventArgs e)
        {

        }

        private void tabVargas_Click(object sender, System.EventArgs e)
        {

        }

        private void mTab_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (mTab.SelectedTab == tabTransits && bTabTransitsLoaded == false)
            {
                AddControlToTab(tabTransits, new TransitSearch(h));
                bTabTransitsLoaded = true;
                return;
            }

            if (mTab.SelectedTab == tabDasa && bTabDasaLoaded == false)
            {
                PanchangControl mc = new PanchangControl();
                AddControlToTab(tabDasa, mc);

                //MhoraControlContainer mcc = new MhoraControlContainer(mc);
                mc.ControlHoroscope = h;
                switch (h.Info.type)
                {
                    case HoraType.TithiPravesh:
                        mc.ViewControl(PanchangControlContainer.BaseUserOptions.ViewType.DasaTithiPraveshAshtottariCompressedTithi);
                        break;
                    default:
                        mc.ViewControl(PanchangControlContainer.BaseUserOptions.ViewType.DasaVimsottari);
                        break;
                }
                bTabDasaLoaded = true;
                return;
            }

            if (mTab.SelectedTab == tabVargas && bTabVargasLoaded == false)
            {
                AddControlToTab(tabVargas, new DivisionalChart(h));
                bTabVargasLoaded = true;
            }

            if (mTab.SelectedTab == tabTransits && bTabTransitsLoaded == false)
            {
                AddControlToTab(tabTransits, new TransitSearch(h));
                bTabTransitsLoaded = true;
            }

            if (mTab.SelectedTab == tabTithiPravesh && bTabTithiPraveshLoaded == false)
            {
                DasaControl dc = new DasaControl(h, new TithiPraveshDasa(h))
                {
                    LinkToHoroscope = false
                };
                dc.DasaOptions.YearType = DateType.TithiPraveshYear;
                dc.Reset();
                AddControlToTab(tabTithiPravesh, dc);
                bTabTithiPraveshLoaded = true;
            }

            //if (this.mTab.SelectedTab == tabTajaka && this.bTabTajakaLoaded == false)
            //{
            //	this.AddControlToTab(tabTajaka, new DasaControl(h, new TajakaDasa(h)));
            //	this.bTabTajakaLoaded = true;		
            //}
            if (mTab.SelectedTab == tabPanchanga && bTabPanchangaLoaded == false)
            {
                AddControlToTab(tabPanchanga, new PanchangaControl(h));
                bTabPanchangaLoaded = true;
            }

        }

        private void tabPanchanga_Click(object sender, System.EventArgs e)
        {

        }

        private void tabDasa_Click(object sender, System.EventArgs e)
        {

        }
    }
}
