using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for JhoraBasicsTab.
    /// </summary>
    public class JhoraBasicsTab : PanchangControl
    {
        private TabControl tabControl1;
        private TabPage tabKeyInfo;
        private TabPage tabCalculations;
        private TabPage tabAshtakavarga;
        private TabPage tabNavamsaChakra;
        private TabPage tabYogas;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private void AddControlToTab(TabPage tab, PanchangControl mcontrol)
        {
            PanchangControlContainer container = new PanchangControlContainer(mcontrol);
            container.Dock = DockStyle.Fill;
            tab.Controls.Add(container);
        }

        //bool bTabKeyInfoLoaded = false;
        bool bTabCalculationsLoaded = false;
        bool bTabAshtakavargaLoaded = false;
        bool bTabNavamsaChakraLoaded = false;
        bool bTabYogasLoaded = false;
        public JhoraBasicsTab(Horoscope _h)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            h = _h;
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            OnRedisplay(PanchangAppOptions.Instance);
            AddControlToTab(tabKeyInfo, new KeyInfoControl(h));
            //this.AddControlToTab (tabTest, new BalasControl(h));
            //this.AddControlToTab (tabTest, new Sarvatobhadra81Control(h));
            //this.AddControlToTab (tabTest, new KutaMatchingControl(h, h));
            //this.AddControlToTab (tabTest, new VaraChakra(h));

            tabControl1.TabPages[0] = tabKeyInfo;
            tabControl1.TabPages[1] = tabCalculations;
            tabControl1.TabPages[2] = tabNavamsaChakra;
            tabControl1.TabPages[3] = tabAshtakavarga;
            tabControl1.TabPages[4] = tabYogas;

            tabControl1.SelectedTab = tabKeyInfo;
            //this.tabControl1.SelectedTab = tabTest;
        }

        public void OnRedisplay(object o)
        {
            Font = PanchangAppOptions.Instance.GeneralFont;
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabKeyInfo = new TabPage();
            tabNavamsaChakra = new TabPage();
            tabCalculations = new TabPage();
            tabAshtakavarga = new TabPage();
            tabYogas = new TabPage();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Bottom;
            tabControl1.Controls.Add(tabKeyInfo);
            tabControl1.Controls.Add(tabCalculations);
            tabControl1.Controls.Add(tabYogas);
            tabControl1.Controls.Add(tabNavamsaChakra);
            tabControl1.Controls.Add(tabAshtakavarga);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(15, 3);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(292, 266);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            // 
            // tabKeyInfo
            // 
            tabKeyInfo.Location = new Point(4, 4);
            tabKeyInfo.Name = "tabKeyInfo";
            tabKeyInfo.Size = new Size(284, 238);
            tabKeyInfo.TabIndex = 0;
            tabKeyInfo.Text = "Key Info";
            tabKeyInfo.Click += new EventHandler(tabKeyInfo_Click);
            // 
            // tabNavamsaChakra
            // 
            tabNavamsaChakra.Location = new Point(4, 4);
            tabNavamsaChakra.Name = "tabNavamsaChakra";
            tabNavamsaChakra.Size = new Size(284, 238);
            tabNavamsaChakra.TabIndex = 3;
            tabNavamsaChakra.Text = "Chakra";
            tabNavamsaChakra.Click += new EventHandler(tabTest_Click);
            // 
            // tabCalculations
            // 
            tabCalculations.Location = new Point(4, 4);
            tabCalculations.Name = "tabCalculations";
            tabCalculations.Size = new Size(284, 238);
            tabCalculations.TabIndex = 1;
            tabCalculations.Text = "Calculations";
            // 
            // tabAshtakavarga
            // 
            tabAshtakavarga.Location = new Point(4, 4);
            tabAshtakavarga.Name = "tabAshtakavarga";
            tabAshtakavarga.Size = new Size(284, 238);
            tabAshtakavarga.TabIndex = 2;
            tabAshtakavarga.Text = "Ashtakavarga";
            // 
            // tabYogas
            // 
            tabYogas.Location = new Point(4, 4);
            tabYogas.Name = "tabYogas";
            tabYogas.Size = new Size(284, 238);
            tabYogas.TabIndex = 4;
            tabYogas.Text = "Yogas";
            // 
            // JhoraBasicsTab
            // 
            Controls.Add(tabControl1);
            Name = "JhoraBasicsTab";
            Size = new Size(292, 266);
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);

        }
        #endregion

        private void tabKeyInfo_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tp = tabControl1.SelectedTab;
            if (tp == tabCalculations && bTabCalculationsLoaded == false)
            {
                AddControlToTab(tabCalculations, new BasicCalculationsControl(h));
                bTabCalculationsLoaded = true;
            }

            if (tp == tabAshtakavarga && bTabAshtakavargaLoaded == false)
            {
                AddControlToTab(tabAshtakavarga, new AshtakavargaControl(h));
                bTabAshtakavargaLoaded = true;
            }

            if (tp == tabNavamsaChakra && bTabNavamsaChakraLoaded == false)
            {
                AddControlToTab(tabNavamsaChakra, new NavamsaControl(h));
                bTabNavamsaChakraLoaded = true;
            }
            if (tp == tabYogas && bTabYogasLoaded == false)
            {
                //this.AddControlToTab(tabYogas, new YogaControl(h));
                bTabYogasLoaded = true;
            }

        }

        private void tabTest_Click(object sender, EventArgs e)
        {

        }
    }
}
