using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{
    public class KutaMatchingControl : PanchangControl
    {
        private TextBox tbHorMale;
        private TextBox tbHorFemale;
        private IContainer components = null;

        private ListView lView;
        private Button bMaleChange;
        private Button bFemaleChange;
        private ContextMenu mContext;
        private ColumnHeader Male;
        protected ColumnHeader Female;
        private ColumnHeader Score;
        private ColumnHeader Kuta;
        private Label label1;
        private Label label2;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        Horoscope h2 = null;

        private void populateTextBoxes()
        {
            tbHorMale.Text = "Current Chart";
            tbHorFemale.Text = "Current Chart";
            foreach (Form f in ((PanchangContainer)GlobalOptions.mainControl).MdiChildren)
                if (f is PanchangChild)
                {
                    PanchangChild mch = (PanchangChild)f;
                    if (mch.getHoroscope() == h)
                        tbHorMale.Text = mch.Text;
                    if (mch.getHoroscope() == h2)
                        tbHorFemale.Text = mch.Text;
                }
        }

        public KutaMatchingControl(Horoscope _h, Horoscope _h2)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            h = _h;
            h2 = _h2;
            h.Changed += new EvtChanged(OnRecalculate);
            h2.Changed += new EvtChanged(OnRecalculate);
            AddViewsToContextMenu(mContext);
            populateTextBoxes();
            OnRecalculate(h);
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbHorMale = new TextBox();
            tbHorFemale = new TextBox();
            lView = new ListView();
            Kuta = new ColumnHeader();
            Male = new ColumnHeader();
            Female = new ColumnHeader();
            Score = new ColumnHeader();
            bMaleChange = new Button();
            bFemaleChange = new Button();
            mContext = new ContextMenu();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // tbHorMale
            // 
            tbHorMale.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            tbHorMale.Location = new Point(72, 8);
            tbHorMale.Name = "tbHorMale";
            tbHorMale.ReadOnly = true;
            tbHorMale.Size = new Size(320, 20);
            tbHorMale.TabIndex = 0;
            tbHorMale.Text = "";
            // 
            // tbHorFemale
            // 
            tbHorFemale.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            tbHorFemale.Location = new Point(72, 40);
            tbHorFemale.Name = "tbHorFemale";
            tbHorFemale.ReadOnly = true;
            tbHorFemale.Size = new Size(320, 20);
            tbHorFemale.TabIndex = 1;
            tbHorFemale.Text = "";
            // 
            // lView
            // 
            lView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            lView.Columns.AddRange(new ColumnHeader[] {
                                                                                    Kuta,
                                                                                    Male,
                                                                                    Female,
                                                                                    Score});
            lView.FullRowSelect = true;
            lView.Location = new Point(8, 72);
            lView.Name = "lView";
            lView.Size = new Size(544, 264);
            lView.TabIndex = 2;
            lView.View = View.Details;
            lView.SelectedIndexChanged += new EventHandler(lView_SelectedIndexChanged);
            // 
            // Kuta
            // 
            Kuta.Text = "Kuta";
            Kuta.Width = 163;
            // 
            // Male
            // 
            Male.Text = "Male";
            Male.Width = 126;
            // 
            // Female
            // 
            Female.Text = "Female";
            Female.Width = 119;
            // 
            // Score
            // 
            Score.Text = "Score";
            Score.Width = 107;
            // 
            // bMaleChange
            // 
            bMaleChange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMaleChange.Location = new Point(464, 8);
            bMaleChange.Name = "bMaleChange";
            bMaleChange.Size = new Size(64, 24);
            bMaleChange.TabIndex = 3;
            bMaleChange.Text = "Change";
            bMaleChange.Click += new EventHandler(bMaleChange_Click);
            // 
            // bFemaleChange
            // 
            bFemaleChange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bFemaleChange.Location = new Point(464, 40);
            bFemaleChange.Name = "bFemaleChange";
            bFemaleChange.Size = new Size(64, 23);
            bFemaleChange.TabIndex = 4;
            bFemaleChange.Text = "Change";
            bFemaleChange.Click += new EventHandler(bFemaleChange_Click);
            // 
            // mContext
            // 
            mContext.MenuItems.AddRange(new MenuItem[] {
                                                                                     menuItem1,
                                                                                     menuItem2});
            // 
            // menuItem1
            // 
            menuItem1.Index = 0;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 1;
            menuItem2.Text = "-";
            // 
            // label1
            // 
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(48, 23);
            label1.TabIndex = 5;
            label1.Text = "Male:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(8, 40);
            label2.Name = "label2";
            label2.Size = new Size(64, 23);
            label2.TabIndex = 6;
            label2.Text = "Female:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // KutaMatchingControl
            // 
            ContextMenu = mContext;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bFemaleChange);
            Controls.Add(bMaleChange);
            Controls.Add(lView);
            Controls.Add(tbHorFemale);
            Controls.Add(tbHorMale);
            Name = "KutaMatchingControl";
            Size = new Size(560, 344);
            Load += new EventHandler(KutaMatchingControl_Load);
            ResumeLayout(false);

        }
        #endregion

        public string getGhatakaString(bool gh)
        {
            if (gh)
                return "Ghataka";
            else
                return "-";

        }
        public void OnRecalculate(object o)
        {
            Division dtype = new Division(DivisionType.Rasi);

            BodyPosition l1 = h.GetPosition(BodyName.Lagna);
            BodyPosition l2 = h2.GetPosition(BodyName.Lagna);
            BodyPosition m1 = h.GetPosition(BodyName.Moon);
            BodyPosition m2 = h2.GetPosition(BodyName.Moon);
            ZodiacHouse z1 = m1.ToDivisionPosition(dtype).ZodiacHouse;
            ZodiacHouse z2 = m2.ToDivisionPosition(dtype).ZodiacHouse;
            Nakshatra n1 = m1.Longitude.ToNakshatra();
            Nakshatra n2 = m2.Longitude.ToNakshatra();

            lView.Items.Clear();

            {
                ListViewItem li = new ListViewItem("Nakshatra Yoni");
                li.SubItems.Add(KutaNakshatraYoni.getType(n1).ToString()
                    + " (" + KutaNakshatraYoni.GetSex(n1).ToString() + ")");
                li.SubItems.Add(KutaNakshatraYoni.getType(n2).ToString()
                    + " (" + KutaNakshatraYoni.GetSex(n2).ToString() + ")");
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Rasi Yoni");
                li.SubItems.Add(KutaRasiYoni.getType(z1).ToString());
                li.SubItems.Add(KutaRasiYoni.getType(z2).ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Varna");
                li.SubItems.Add(KutaVarna.GetType(n1).ToString());
                li.SubItems.Add(KutaVarna.GetType(n2).ToString());
                li.SubItems.Add(KutaVarna.GetScore(n1, n2).ToString() + "/" + KutaVarna.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Gana (Chandra)");
                li.SubItems.Add(KutaGana.GetType(n1).ToString());
                li.SubItems.Add(KutaGana.GetType(n2).ToString());
                li.SubItems.Add(KutaGana.GetScore(n1, n2).ToString() + "/" + KutaGana.GetMaxScore().ToString());

                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Gana (Lagna)");
                li.SubItems.Add(KutaGana.GetType(l1.Longitude.ToNakshatra()).ToString());
                li.SubItems.Add(KutaGana.GetType(l2.Longitude.ToNakshatra()).ToString());
                li.SubItems.Add(KutaGana.GetScore(l1.Longitude.ToNakshatra(), l2.Longitude.ToNakshatra()).ToString()
                    + "/" + KutaGana.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Vedha");
                li.SubItems.Add(KutaVedha.GetType(n1).ToString());
                li.SubItems.Add(KutaVedha.GetType(n2).ToString());
                li.SubItems.Add(KutaVedha.GetScore(n1, n2).ToString() + "/" + KutaVedha.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Rajju");
                li.SubItems.Add(KutaRajju.GetType(n1).ToString());
                li.SubItems.Add(KutaRajju.GetType(n2).ToString());
                li.SubItems.Add(KutaRajju.GetScore(n1, n2).ToString() + "/" + KutaRajju.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Nadi");
                li.SubItems.Add(KutaNadi.GetType(n1).ToString());
                li.SubItems.Add(KutaNadi.GetType(n2).ToString());
                li.SubItems.Add(KutaNadi.GetScore(n1, n2).ToString() + "/" + KutaNadi.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Gotra (TD:Abhi)");
                li.SubItems.Add(KutaGotra.GetType(n1).ToString());
                li.SubItems.Add(KutaGotra.GetType(n2).ToString());
                li.SubItems.Add(KutaGotra.GetScore(n1, n2).ToString() + "/" + KutaGotra.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Vihanga");
                li.SubItems.Add(KutaVihanga.GetType(n1).ToString());
                li.SubItems.Add(KutaVihanga.GetType(n2).ToString());
                li.SubItems.Add(KutaVihanga.GetDominator(n1, n2).ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Bhuta (Nakshatra)");
                li.SubItems.Add(KutaBhutaNakshatra.GetType(n1).ToString());
                li.SubItems.Add(KutaBhutaNakshatra.GetType(n2).ToString());
                li.SubItems.Add(KutaBhutaNakshatra.GetScore(n1, n2).ToString() + "/" + KutaBhutaNakshatra.GetMaxScore().ToString());
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka (Moon)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                ZodiacHouse ch = h2.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                bool isGhataka = GhatakaMoon.CheckGhataka(ja, ch);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(ch.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka (Tithi)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                Longitude ltithi = h2.GetPosition(BodyName.Moon).Longitude.Subtract(h2.GetPosition(BodyName.Sun).Longitude);
                Tithi t = ltithi.ToTithi();
                bool isGhataka = GhatakaTithi.CheckTithi(ja, t);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(t.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka (Day)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                Weekday wd = h2.Weekday;
                bool isGhataka = GhatakaDay.CheckDay(ja, wd);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(wd.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka (Star)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                Nakshatra na = h2.GetPosition(BodyName.Moon).Longitude.ToNakshatra();
                bool isGhataka = GhatakaStar.CheckStar(ja, na);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(na.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka Lagna(S)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                ZodiacHouse sa = h2.GetPosition(BodyName.Lagna).ToDivisionPosition(dtype).ZodiacHouse;
                bool isGhataka = GhatakaLagnaSame.CheckLagna(ja, sa);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(sa.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Ghataka Lagna(O)");
                ZodiacHouse ja = h.GetPosition(BodyName.Moon).ToDivisionPosition(dtype).ZodiacHouse;
                ZodiacHouse op = h2.GetPosition(BodyName.Lagna).ToDivisionPosition(dtype).ZodiacHouse;
                bool isGhataka = GhatakaLagnaOpp.CheckLagna(ja, op);
                li.SubItems.Add(ja.ToString());
                li.SubItems.Add(op.ToString());
                li.SubItems.Add(getGhatakaString(isGhataka));
                lView.Items.Add(li);
            }
            ColorAndFontRows(lView);
        }

        private void KutaMatchingControl_Load(object sender, EventArgs e)
        {

        }

        private void bMaleChange_Click(object sender, EventArgs e)
        {
            ChooseHoroscopeControl f = new ChooseHoroscopeControl();
            f.ShowDialog();
            if (f.GetHorsocope() != null)
            {
                h.Changed -= new EvtChanged(OnRecalculate);
                h = f.GetHorsocope();
                tbHorMale.Text = f.GetHoroscopeName();
                h.Changed += new EvtChanged(OnRecalculate);
                OnRecalculate(h);
            }
            f.Dispose();
        }

        private void lView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bFemaleChange_Click(object sender, EventArgs e)
        {
            ChooseHoroscopeControl f = new ChooseHoroscopeControl();
            f.ShowDialog();
            if (f.GetHorsocope() != null)
            {
                h2.Changed -= new EvtChanged(OnRecalculate);
                h2 = f.GetHorsocope();
                tbHorFemale.Text = f.GetHoroscopeName();
                h2.Changed += new EvtChanged(OnRecalculate);
                OnRecalculate(h2);
            }
            f.Dispose();
        }
    }
}

