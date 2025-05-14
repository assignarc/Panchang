using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for BasicCalculationsControl.
    /// </summary>
    public class BasicCalculationsControl : PanchangControl
    {
        private ListView mList;
        private ColumnHeader Body;
        private ColumnHeader Longitude;
        private ColumnHeader Nakshatra;
        private ColumnHeader Pada;
        private ContextMenu calculationsContextMenu;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        private MenuItem menuBasicGrahas;
        private MenuItem menuSpecialTithis;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuSpecialTaras;


        private MenuItem menuChangeVarga;
        private UserOptions options;
        private MenuItem menuBhavaCusps;
        private MenuItem menuOtherLongitudes;
        private MenuItem menuMrityuLongitudes;
        private MenuItem menuAstroInfo;
        private MenuItem menuNakshatraAspects;
        private MenuItem menuCharaKarakas;
        private MenuItem menuSahamaLongitudes;
        private MenuItem menuAvasthas;
        private MenuItem menuCharaKarakas7;
        private MenuItem menu64Navamsa;
        private MenuItem menuCopyLon;
        private MenuItem menuNonLonBodies;

        private readonly int[] latta_aspects = new int[] { 12, 22, 3, 7, 6, 5, 8, 9 };
        private readonly int[][] tara_aspects = new int[][]
        {
            new int[] { 14, 15 },
            new int[] { 14, 15 },
            new int[] { 1, 3, 7, 8, 15 },
            new int[] { 1, 15 },
            new int[] { 10, 15, 19 },
            new int[] { 1, 15 },
            new int[] { 3, 5, 15, 19 },
            new int[] { }
        };
        private readonly string[] karakas = new string[] { "Atma", "Amatya", "Bhratri", "Matri", "Pitri", "Putra", "Jnaati", "Dara" };
        private readonly string[] karakas7 = new string[] { "Atma", "Amatya", "Bhratri", "Matri", "Pitri", "Jnaati", "Dara" };
        private readonly string[] karakas_s = new string[] { "AK", "AmK", "BK", "MK", "PiK", "PuK", "JK", "DK" };
        private readonly string[] karakas_s7 = new string[] { "AK", "AmK", "BK", "MK", "PiK", "JK", "DK" };

      
        class UserOptions : ICloneable
        {
            private Division dtype;
            private ENakshatraLord mNakLord;

            [InVisible]
            public Division DivisionType
            {
                get { return dtype; }
                set { dtype = value; }
            }

            [Visible("Division Type")]
            public DivisionType UIDivisionType
            {
                get { return dtype.MultipleDivisions[0].Varga; }
                set { dtype = new Division(value); }
            }

            public ENakshatraLord NakshatraLord
            {
                get { return mNakLord; }
                set { mNakLord = value; }
            }

            public object Clone()
            {
                UserOptions uo = new UserOptions
                {
                    dtype = dtype,
                    mNakLord = mNakLord
                };
                return uo;
            }
            public object Copy(object o)
            {
                UserOptions uo = (UserOptions)o;
                dtype = uo.dtype;
                mNakLord = uo.mNakLord;
                return Clone();
            }
        }

        ViewType vt;
        public BasicCalculationsControl(Horoscope _h)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            h = _h;
            vt = ViewType.ViewBasicGrahas;
            menuBasicGrahas.Checked = true;
            h.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            options = new UserOptions
            {
                DivisionType = new Division(DivisionType.Rasi)
            };

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
            mList = new ListView();
            Body = new ColumnHeader();
            Longitude = new ColumnHeader();
            Nakshatra = new ColumnHeader();
            Pada = new ColumnHeader();
            calculationsContextMenu = new ContextMenu();
            menuChangeVarga = new MenuItem();
            menuCopyLon = new MenuItem();
            menuBasicGrahas = new MenuItem();
            menuOtherLongitudes = new MenuItem();
            menuMrityuLongitudes = new MenuItem();
            menuSahamaLongitudes = new MenuItem();
            menuNonLonBodies = new MenuItem();
            menuCharaKarakas = new MenuItem();
            menuCharaKarakas7 = new MenuItem();
            menu64Navamsa = new MenuItem();
            menuAstroInfo = new MenuItem();
            menuSpecialTithis = new MenuItem();
            menuSpecialTaras = new MenuItem();
            menuNakshatraAspects = new MenuItem();
            menuBhavaCusps = new MenuItem();
            menuAvasthas = new MenuItem();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            SuspendLayout();
            // 
            // mList
            // 
            mList.AllowDrop = true;
            mList.Columns.AddRange(new ColumnHeader[] {
                                                                                    Body,
                                                                                    Longitude,
                                                                                    Nakshatra,
                                                                                    Pada});
            mList.ContextMenu = calculationsContextMenu;
            mList.Dock = DockStyle.Fill;
            mList.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mList.ForeColor = SystemColors.HotTrack;
            mList.FullRowSelect = true;
            mList.Location = new Point(0, 0);
            mList.Name = "mList";
            mList.Size = new Size(496, 176);
            mList.TabIndex = 0;
            mList.View = View.Details;
            mList.MouseHover += new EventHandler(mList_MouseHover);
            mList.DragDrop += new DragEventHandler(mList_DragDrop);
            mList.DragEnter += new DragEventHandler(mList_DragEnter);
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // Body
            // 
            Body.Text = "Body";
            Body.Width = 100;
            // 
            // Longitude
            // 
            Longitude.Text = "Longitude";
            Longitude.Width = 120;
            // 
            // Nakshatra
            // 
            Nakshatra.Text = "Nakshatra";
            Nakshatra.Width = 120;
            // 
            // Pada
            // 
            Pada.Text = "Pada";
            Pada.Width = 50;
            // 
            // calculationsContextMenu
            // 
            calculationsContextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                                    menuChangeVarga,
                                                                                                    menuCopyLon,
                                                                                                    menuBasicGrahas,
                                                                                                    menuOtherLongitudes,
                                                                                                    menuMrityuLongitudes,
                                                                                                    menuSahamaLongitudes,
                                                                                                    menuNonLonBodies,
                                                                                                    menuCharaKarakas,
                                                                                                    menuCharaKarakas7,
                                                                                                    menu64Navamsa,
                                                                                                    menuAstroInfo,
                                                                                                    menuSpecialTithis,
                                                                                                    menuSpecialTaras,
                                                                                                    menuNakshatraAspects,
                                                                                                    menuBhavaCusps,
                                                                                                    menuAvasthas,
                                                                                                    menuItem1,
                                                                                                    menuItem2});
            calculationsContextMenu.Popup += new EventHandler(calculationsContextMenu_Popup);
            // 
            // menuChangeVarga
            // 
            menuChangeVarga.Index = 0;
            menuChangeVarga.Text = "Options";
            menuChangeVarga.Click += new EventHandler(menuChangeVarga_Click);
            // 
            // menuCopyLon
            // 
            menuCopyLon.Index = 1;
            menuCopyLon.Text = "Copy Longitude";
            menuCopyLon.Click += new EventHandler(menuCopyLon_Click);
            // 
            // menuBasicGrahas
            // 
            menuBasicGrahas.Index = 2;
            menuBasicGrahas.Text = "Basic Longitudes";
            menuBasicGrahas.Click += new EventHandler(menuBasicGrahas_Click);
            // 
            // menuOtherLongitudes
            // 
            menuOtherLongitudes.Index = 3;
            menuOtherLongitudes.Text = "Other Longitudes";
            menuOtherLongitudes.Click += new EventHandler(menuOtherLongitudes_Click);
            // 
            // menuMrityuLongitudes
            // 
            menuMrityuLongitudes.Index = 4;
            menuMrityuLongitudes.Text = "Mrityu Longitudes";
            menuMrityuLongitudes.Click += new EventHandler(menuMrityuLongitudes_Click);
            // 
            // menuSahamaLongitudes
            // 
            menuSahamaLongitudes.Index = 5;
            menuSahamaLongitudes.Text = "Sahama Longitudes";
            menuSahamaLongitudes.Click += new EventHandler(menuSahamaLongitudes_Click);
            // 
            // menuNonLonBodies
            // 
            menuNonLonBodies.Index = 6;
            menuNonLonBodies.Text = "Non-Longitude Bodies";
            menuNonLonBodies.Click += new EventHandler(menuNonLonBodies_Click);
            // 
            // menuCharaKarakas
            // 
            menuCharaKarakas.Index = 7;
            menuCharaKarakas.Text = "Chara Karakas (8)";
            menuCharaKarakas.Click += new EventHandler(menuCharaKarakas_Click);
            // 
            // menuCharaKarakas7
            // 
            menuCharaKarakas7.Index = 8;
            menuCharaKarakas7.Text = "Chara Karakas (7)";
            menuCharaKarakas7.Click += new EventHandler(menuCharaKarakas7_Click);
            // 
            // menu64Navamsa
            // 
            menu64Navamsa.Index = 9;
            menu64Navamsa.Text = "64th Navamsa";
            menu64Navamsa.Click += new EventHandler(menu64Navamsa_Click);
            // 
            // menuAstroInfo
            // 
            menuAstroInfo.Index = 10;
            menuAstroInfo.Text = "Astronomical Info";
            menuAstroInfo.Click += new EventHandler(menuAstroInfo_Click);
            // 
            // menuSpecialTithis
            // 
            menuSpecialTithis.Index = 11;
            menuSpecialTithis.Text = "Special Tithis";
            menuSpecialTithis.Click += new EventHandler(menuSpecialTithis_Click);
            // 
            // menuSpecialTaras
            // 
            menuSpecialTaras.Index = 12;
            menuSpecialTaras.Text = "Special Nakshatras";
            menuSpecialTaras.Click += new EventHandler(menuSpecialTaras_Click);
            // 
            // menuNakshatraAspects
            // 
            menuNakshatraAspects.Index = 13;
            menuNakshatraAspects.Text = "Nakshatra Aspects";
            menuNakshatraAspects.Click += new EventHandler(menuNakshatraAspects_Click);
            // 
            // menuBhavaCusps
            // 
            menuBhavaCusps.Index = 14;
            menuBhavaCusps.Text = "Bhava Cusps";
            menuBhavaCusps.Click += new EventHandler(menuBhavaCusps_Click);
            // 
            // menuAvasthas
            // 
            menuAvasthas.Index = 15;
            menuAvasthas.Text = "Avasthas";
            menuAvasthas.Click += new EventHandler(menuAvasthas_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 16;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 17;
            menuItem2.Text = "-";
            // 
            // BasicCalculationsControl
            // 
            ContextMenu = calculationsContextMenu;
            Controls.Add(mList);
            Name = "BasicCalculationsControl";
            Size = new Size(496, 176);
            Load += new EventHandler(BasicCalculationsControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void BasicCalculationsControl_Load(object sender, EventArgs e)
        {
            AddViewsToContextMenu(calculationsContextMenu);
            Repopulate();

        }

        private void ResizeColumns()
        {
            for (int i = 0; i < mList.Columns.Count; i++)
            {
                mList.Columns[i].Width = -1;
            }
            mList.Columns[mList.Columns.Count - 1].Width = -2;
        }
        string GetTithiName(double val, ref double tithi, ref double perc)
        {
            double offset = val;
            while (offset >= 12.0) offset -= 12.0;
            int t = (int)Math.Floor(val / 12.0) + 1;
            tithi = t;
            perc = 100 - offset / 12.0 * 100;
            string[] tithis = new string[]
            {
                "Prathama", "Dvitiya", "Tritiya", "Chaturthi", "Panchami", "Shashti",
                "Saptami", "Ashtami", "Navami", "Dashami", "Ekadasi", "Dvadashi",
                "Trayodashi", "Chaturdashi"
            };
            if (t == 15) return "Pournima";
            if (t == 30) return "Amavasya";
            string str;
            if (t > 15)
            {
                str = "Krishna ";
                t -= 15;
            }
            else
            {
                str = "Shukla ";
            }
            return str + " " + tithis[t - 1];

        }

        private void RepopulateSpecialTaras()
        {
            int[] specialIndices = new int[]
            {
                1, 10, 18, 16,
                4, 7, 12, 13,
                19, 22, 25
            };
            string[] specialNames = new string[]
            {
                "Janma", "Karma", "Saamudaayika", "Sanghaatika",
                "Jaati", "Naidhana", "Desa", "Abhisheka",
                "Aadhaana", "Vainaasika", "Maanasa"
            };

            mList.Columns.Clear();
            mList.Items.Clear();
            mList.Columns.Add("Name", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Nakshatra (27)", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Nakshatra (28)", -1, HorizontalAlignment.Left);

            Nakshatra nmoon = h.GetPosition(panchang.BodyName.Moon).Longitude.ToNakshatra();
            Nakshatra28 nmoon28 = h.GetPosition(panchang.BodyName.Moon).Longitude.ToNakshatra28();
            for (int i = 0; i < specialIndices.Length; i++)
            {
                Nakshatra sn = nmoon.Add(specialIndices[i]);
                Nakshatra28 sn28 = nmoon28.Add(specialIndices[i]);

                ListViewItem li = new ListViewItem
                {
                    Text = string.Format("{0:00}  {1} Tara", specialIndices[i], specialNames[i])
                };
                li.SubItems.Add(sn.Value.ToString());
                li.SubItems.Add(sn28.Value.ToString());
                mList.Items.Add(li);
            }
            ColorAndFontRows(mList);
            ResizeColumns();

        }

       
        private void RepopulateCharaKarakas()
        {
            mList.Clear();
            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Karaka", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Offset", -2, HorizontalAlignment.Left);

            ArrayList al = new ArrayList();
            int max = 0;
            if (vt == ViewType.ViewCharaKarakas)
                max = (int)panchang.BodyName.Rahu;
            else
                max = (int)panchang.BodyName.Saturn;


            for (int i = (int)panchang.BodyName.Sun; i <= max; i++)
            {
                BodyName b = (BodyName)i;
                BodyPosition bp = h.GetPosition(b);
                BodyKarakaComparer bkc = new BodyKarakaComparer(bp);
                al.Add(bkc);
            }
            al.Sort();

            for (int i = 0; i < al.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                BodyKarakaComparer bk = (BodyKarakaComparer)al[i];
                li.Text = panchang.Body.ToString(bk.GetPosition.name);
                if (vt == ViewType.ViewCharaKarakas)
                    li.SubItems.Add(karakas[i]);
                else
                    li.SubItems.Add(karakas7[i]);
                li.SubItems.Add(string.Format("{0:0.00}", bk.GetOffset()));
                mList.Items.Add(li);
            }


            ColorAndFontRows(mList);
            ResizeColumns();
        }

        readonly string[] avasthas = new string[]
        {
            "Saisava (child - quarter)",
            "Kumaara (adolescent - half)",
            "Yuva (youth - full)",
            "Vriddha (old - some)",
            "Mrita (dead - none)"
        };


        private void Repopulate64NavamsaHelper(BodyName b, string name, BodyPosition bp, Division div)
        {
            DivisionPosition dp = bp.ToDivisionPosition(div);
            ListViewItem li = new ListViewItem
            {
                Text = b.ToString()
            };
            li.SubItems.Add(name);
            li.SubItems.Add(dp.ZodiacHouse.Value.ToString());
            li.SubItems.Add(panchang.Body.ToString(h.LordOfZodiacHouse(dp.ZodiacHouse, div)));
            mList.Items.Add(li);
        }
        private void RepopulateNonLonBodies()
        {
            mList.Clear();
            mList.Columns.Add("Non Longitudinal Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Zodiac House", -2, HorizontalAlignment.Left);

            ArrayList al = h.CalculateArudhaDivisionPositions(options.DivisionType);
            al.AddRange(h.CalculateVarnadaDivisionPositions(options.DivisionType));

            foreach (DivisionPosition dp in al)
            {
                string desc = "";
                if (dp.Name == panchang.BodyName.Other)
                    desc = dp.otherString;
                else
                    desc = dp.Name.ToString();

                ListViewItem li = new ListViewItem(desc);
                li.SubItems.Add(dp.ZodiacHouse.Value.ToString());
                mList.Items.Add(li);

            }

            ColorAndFontRows(mList);
            ResizeColumns();

        }
        private void Repopulate64Navamsa()
        {
            mList.Clear();
            mList.Columns.Add("Reference", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Count", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Rasi", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Lord", -2, HorizontalAlignment.Left);

            BodyName[] bodyReferences = new BodyName[]
            {panchang.BodyName.Lagna,panchang.BodyName.Moon,panchang.BodyName.Sun };

            foreach (BodyName b in bodyReferences)
            {
                BodyPosition bp = (BodyPosition)h.GetPosition(b).Clone();
                Longitude bpLon = bp.Longitude.Add(0);

                bp.Longitude = bpLon.Add(30.0 / 9.0 * (64 - 1));
                Repopulate64NavamsaHelper(b, "64th Navamsa", bp, new Division(DivisionType.Navamsa));

                bp.Longitude = bpLon.Add(30.0 / 3.0 * (22 - 1));
                Repopulate64NavamsaHelper(b, "22nd Drekkana", bp, new Division(DivisionType.DrekkanaParasara));
                Repopulate64NavamsaHelper(b, "22nd Drekkana (Parivritti)", bp, new Division(DivisionType.DrekkanaParivrittitraya));
                Repopulate64NavamsaHelper(b, "22nd Drekkana (Somnath)", bp, new Division(DivisionType.DrekkanaSomnath));
                Repopulate64NavamsaHelper(b, "22nd Drekkana (Jagannath)", bp, new Division(DivisionType.DrekkanaJagannath));
            }

            ColorAndFontRows(mList);
            ResizeColumns();
        }

        private void RepopulateAvasthas()
        {
            mList.Clear();
            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Age", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Alertness", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Mood", -2, HorizontalAlignment.Left);

            for (int i = (int)panchang.BodyName.Sun; i <= (int)panchang.BodyName.Ketu; i++)
            {
                BodyName b = (BodyName)i;
                ListViewItem li = new ListViewItem
                {
                    Text = panchang.Body.ToString(b)
                };
                DivisionPosition dp = h.GetPosition(b).ToDivisionPosition(new Division(DivisionType.Panchamsa));
                int avastha_index = -1;
                switch ((int)dp.ZodiacHouse.Value % 2)
                {
                    case 1: avastha_index = dp.Part; break;
                    case 0: avastha_index = 6 - dp.Part; break;
                }
                li.SubItems.Add(avasthas[avastha_index - 1]);
                mList.Items.Add(li);
            }

            ColorAndFontRows(mList);
            ResizeColumns();
        }
        private void RepopulateNakshatraAspects()
        {

            mList.Clear();
            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Latta", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Aspected", -2, HorizontalAlignment.Left);

            for (int i = (int)panchang.BodyName.Sun; i <= (int)panchang.BodyName.Rahu; i++)
            {
                BodyName b = (BodyName)i;
                bool dirForward = true;
                if (i % 2 == 1) dirForward = false;

                BodyPosition bp = h.GetPosition(b);
                Nakshatra n = bp.Longitude.ToNakshatra();
                Nakshatra l = null;

                if (dirForward)
                    l = n.Add(latta_aspects[i]);
                else
                    l = n.AddReverse(latta_aspects[i]);

                string nak_fmt = "";
                foreach (int j in tara_aspects[i])
                {
                    Nakshatra na = n.Add(j);
                    if (nak_fmt.Length > 0)
                        nak_fmt = string.Format("{0}, {1}-{2}", nak_fmt, j, na.Value);
                    else
                        nak_fmt = string.Format("{0}-{1}", j, na.Value);
                }

                ListViewItem li = new ListViewItem(panchang.Body.ToString(b));
                string fmt = string.Format("{0:00}-{1}", latta_aspects[i], l.Value);
                li.SubItems.Add(fmt);
                li.SubItems.Add(nak_fmt);
                mList.Items.Add(li);
            }

            ColorAndFontRows(mList);
            ResizeColumns();

        }
        private void RepopulateAstronomicalInfo()
        {
            mList.Clear();
            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Lon (deg)", -1, HorizontalAlignment.Left);
            mList.Columns.Add("/ day", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Lat (deb)", -1, HorizontalAlignment.Left);
            mList.Columns.Add("/ day", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Distance (AU)", -1, HorizontalAlignment.Left);
            mList.Columns.Add("/ day", -1, HorizontalAlignment.Left);

            for (int i = (int)panchang.BodyName.Sun; i <= (int)panchang.BodyName.Saturn; i++)
            {
                BodyName b = (BodyName)i;
                BodyPosition bp = h.GetPosition(b);
                ListViewItem li = new ListViewItem(panchang.Body.ToString(b));
                li.SubItems.Add(bp.Longitude.Value.ToString());
                li.SubItems.Add(bp.SpeedLongitude.ToString());
                li.SubItems.Add(bp.Latitude.ToString());
                li.SubItems.Add(bp.SpeedLatitude.ToString());
                li.SubItems.Add(bp.Distance.ToString());
                li.SubItems.Add(bp.SpeedDistance.ToString());
                mList.Items.Add(li);
            }
            ColorAndFontRows(mList);
            ResizeColumns();
        }
        private void RepopulateSpecialTithis()
        {
            string[] specialNames = new string[]
            {
                "", "Janma", "Dhana", "Bhratri", "Matri", "Putra", "Shatru",
                "Kalatra", "Mrityu", "Bhagya", "Karma", "Laabha", "Vyaya"
            };

            mList.Columns.Clear();
            mList.Items.Clear();
            mList.Columns.Add("Name", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Tithi", -1, HorizontalAlignment.Left);
            mList.Columns.Add("% Left", -1, HorizontalAlignment.Left);

            Longitude spos = h.GetPosition(panchang.BodyName.Sun).Longitude;
            Longitude mpos = h.GetPosition(panchang.BodyName.Moon).Longitude;
            double baseTithi = mpos.Subtract(spos).Value;
            for (int i = 1; i <= 12; i++)
            {
                double spTithiVal = new Longitude(baseTithi * i).Value;
                double tithi = 0;
                double perc = 0;
                ListViewItem li = new ListViewItem();
                string s1 = string.Format("{0:00}  {1} Tithi", i, specialNames[i]);
                li.Text = s1;
                string s2 = GetTithiName(spTithiVal, ref tithi, ref perc);
                li.SubItems.Add(s2);
                string s3 = string.Format("{0:###.##}%", perc);
                li.SubItems.Add(s3);
                mList.Items.Add(li);
            }

            ColorAndFontRows(mList);
            ResizeColumns();

        }
        private string getNakLord(Longitude l)
        {
            INakshatraDasa id = null;
            switch (options.NakshatraLord)
            {
                default:
                case ENakshatraLord.Vimsottari:
                    id = new VimsottariDasa(h);
                    break;
                case ENakshatraLord.Ashtottari:
                    id = new AshtottariDasa(h);
                    break;
                case ENakshatraLord.Yogini:
                    id = new YoginiDasa(h);
                    break;
                case ENakshatraLord.Shodashottari:
                    id = new ShodashottariDasa(h);
                    break;
                case ENakshatraLord.Dwadashottari:
                    id = new DwadashottariDasa(h);
                    break;
                case ENakshatraLord.Panchottari:
                    id = new PanchottariDasa(h);
                    break;
                case ENakshatraLord.Shatabdika:
                    id = new ShatabdikaDasa(h);
                    break;
                case ENakshatraLord.ChaturashitiSama:
                    id = new ChaturashitiSamaDasa(h);
                    break;
                case ENakshatraLord.DwisaptatiSama:
                    id = new DwisaptatiSamaDasa(h);
                    break;
                case ENakshatraLord.ShatTrimshaSama:
                    id = new ShatTrimshaSamaDasa(h);
                    break;
            }
            BodyName b = id.LordOfNakshatra(l.ToNakshatra());
            return b.ToString();
        }
        private void RepopulateHouseCusps()
        {
            mList.Clear();
            mList.Columns.Add("System", -1, HorizontalAlignment.Left);
            mList.Columns.Add("House", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Start", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Stop", -1, HorizontalAlignment.Left);

            for (int i = 0; i < 12; i++)
            {
                ListViewItem li = new ListViewItem
                {
                    Text = string.Format("{0}", (char)h.SwephHouseSystem)
                };
                li.SubItems.Add(h.SwephHouseCusps[i].Value.ToString());
                li.SubItems.Add(h.SwephHouseCusps[i + 1].Value.ToString());
                mList.Items.Add(li);
            }
            ColorAndFontRows(mList);
            ResizeColumns();
        }
        private string longitudeToString(Longitude lon)
        {
            string rasi = lon.ToZodiacHouse().Value.ToString();
            double offset = lon.ToZodiacHouseOffset();
            double minutes = Math.Floor(offset);
            offset = (offset - minutes) * 60.0;
            double seconds = Math.Floor(offset);
            offset = (offset - seconds) * 60.0;
            double subsecs = Math.Floor(offset);
            offset = (offset - subsecs) * 60.0;
            double subsubsecs = Math.Floor(offset);

            return
                string.Format("{0:00} {1} {2:00}:{3:00}:{4:00}",
                minutes, rasi, seconds, subsecs, subsubsecs
                );
        }
        private void RepopulateBhavaCusps()
        {
            mList.Clear();
            mList.Columns.Add("Cusp Start", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Cusp End", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Rasi", -2, HorizontalAlignment.Left);

            Longitude lpos = h.GetPosition(panchang.BodyName.Lagna).Longitude.Add(0);
            BodyPosition bp = new BodyPosition(h, panchang.BodyName.Lagna, BodyType.Name.Lagna, lpos, 0, 0, 0, 0, 0);
            for (int i = 0; i < 12; i++)
            {
                DivisionPosition dp = bp.ToDivisionPosition(options.DivisionType);
                ListViewItem li = new ListViewItem
                {
                    Text = longitudeToString(new Longitude(dp.CuspLower))
                };
                li.SubItems.Add(longitudeToString(new Longitude(dp.CuspHigher)));
                li.SubItems.Add(dp.ZodiacHouse.Value.ToString());
                bp.Longitude = new Longitude(dp.CuspHigher + 1);
                mList.Items.Add(li);
            }
            ColorAndFontRows(mList);
            ResizeColumns();

        }

        private static readonly string[] mRulersHora = new string[]
            {
                "Devaas - Sun", "Pitris - Moon"
            };
        private static readonly string[] mRulersDrekkana = new string[]
            {
                "Naarada", "Agastya", "Durvaasa"
            };
        private static readonly string[] mRulersChaturthamsa = new string[]
            {
                "Sanaka", "Sanandana", "Sanat Kumaara", "Sanaatana"
            };
        private static readonly string[] mRulersSaptamsa = new string[]
            {
                "Kshaara", "Ksheera", "Dadhi", "Ghrita", "Ikshurasa", "Madya", "Shuddha Jala"
            };
        private static readonly string[] mRulersNavamsa = new string[]
            {
                "Deva", "Nara", "Rakshasa"
            };
        private static readonly string[] mRulersDasamsa = new string[]
            {
                "Indra", "Agni", "Yama", "Rakshasa", "Varuna", "Vayu", "Kubera", "Ishana", "Brahma", "Ananta"
            };
        private static string[] mRulersDwadasamsa = new string[]
            {
                "Ganesha", "Ashwini Kumars", "Yama", "Sarpa"
            };
        private static readonly string[] mRulersShodasamsa = new string[]
            {
                "Brahma", "Vishnu", "Shiva", "Surya"
            };
        private static readonly string[] mRulersVimsamsa = new string[]
            {
                "Kali", "Gauri", "Jaya", "Lakshmi", "Vijaya", "Vimala", "Sati", "Tara",
                "Jwalamukhi", "Shaveta", "Lalita", "Bagla", "Pratyangira", "Shachi", "Raudri",
                "Bhavani", "Varda", "Jaya", "Tripura", "Sumukhi",
                "Daya", "Medha", "China Shirsha", "Pishachini", "Dhoomavati", "Matangi",
                "Bala", "Bhadra", "Aruna", "Anala", "Pingala", "Chuccuka", "Ghora", "Varahi",
                "Vaishnavi", "Sita", "Bhuvaneshi", "Bhairavi", "Mangla", "Aparajita"
            };
        private static readonly string[] mRulersChaturvimsamsa = new string[]
            {
                "Skanda", "Parashudhara", "Anala", "Vishvakarma", "Bhaga", "Mitra", "Maya",
                "Antaka", "Vrishdhwaja", "Govinda", "Madana", "Bhima"
            };
        private static readonly string[] mRulersNakshatramsa = new string[]
            {
                "Ashwini Kumara", "Yama", "Agni", "Brahma", "Chandra Isa", "Aditi", "Jiva", "Abhi",
                "Pitara", "Bhaga", "Aryama", "Surya", "Tvashta", "Maruta", "Shakragni", "Mitra", "Indra",
                "Rakshasa", "Varuna", "Vishvadeva", "Brahma", "Govinda", "Vasu", "Varuna", "Ajapata", "Ahirbudhnya", "Pusha"
            };
        private static readonly string[] mRulersTrimsamsa = new string[]
            {
                "Agni", "Vayu", "Indra", "Kubera", "Varuna"
            };
        private static readonly string[] mRulersKhavedamsa = new string[]
            {
                "Vishnu", "Chandra", "Marichi", "Twashta", "Brahma", "Shiva", "Surya", "Yama", "Yakshesha",
                "Ghandharva", "Kala", "Varuna"
            };
        private static readonly string[] mRulersAkshavedamsa = new string[]
            {
                "Brahma", "Shiva", "Vishnu"
            };
        private static readonly string[] mRulersShashtyamsa = new string[]
            {
                "Ghora", "Rakshasa", "Deva", "Kubera", "Yaksha", "Kinnara",
                "Bharashta", "Kulaghna", "Garala", "Vahni", "Maya", "Purishaka",
                "Apampathi", "Marut", "Kaala", "Sarpa", "Amrita", "Indu",
                "Mridu", "Komala", "Heramba", "Brahma", "Vishnu", "Maheshwara",
                "Deva", "Ardra", "Kalinasa", "Kshitishwara", "Kamalakara", "Gulika",
                "Mrityu", "Kala", "Davagni", "Ghora", "Yama", "Kantaka",
                "Sudha", "Amrita", "Poornachandra", "Vishadagdha", "Kulanasa", "Vamsa Khaya",
                "Utpata", "Kala", "Saumya", "Komala", "Sheetala", "Karala damshtra",
                "Chandramukhi", "Praveena", "Kala Pavaka", "Dandayudha", "Nirmala", "Saumya",
                "Kroora", "AtiSheetala", "Amrita", "Payodhi", "Bhramana", "Chandrarekha",
            };
        private static readonly string[] mRulersNadiamsaRajan = new string[]
        {
            "Vasudha", "Vaishnavi", "Brahmi", "Kalakoota", "Sankari",
            "Sadaakari", "Samaa", "Saumya", "Suraa", "Maayaa",
            "Manoharaa", "Maadhavi", "Manjuswana", "Ghoraa", "Kumbhini",
            "Kutilaa", "Prabhaa", "Paraa", "Payaswini", "Maala",
            "Jagathi", "Jarjharaa", "Dhruva", "TO BE CONTINUED"
        };
        private static readonly string[] mRulersNadiamsaCKN = new string[]
        {
            "Vasudha", "Vaishnavi", "Brahmi", "Kalakoota", "Sankari",
            "Sudhakarasama", "Saumya", "Suraa", "Maaya", "Manoharaa",
            "Maadhavi", "Manjuswana", "Ghoraa", "Kumbhini", "Kutilaa",
            "Prabhaa", "Paraa", "Payaswini", "Malaa", "Jagathi",
            "Jarjhari", "Dhruvaa", "Musalaa", "Mudgala", "Pasaa",
            "Chambaka", "Daamini", "Mahi", "Kalushaa", "Kamalaa",
            "Kanthaa", "Kaalaa", "Karikaraa", "Kshamaa", "Durdharaa",
            "Durbhagaa", "Viswa", "Visirnaa", "Vihwala", "Anilaa",
            "Bhima", "Sukhaprada", "Snigdha", "Sodaraa", "Surasundari",
            "Amritaprasini", "Karalaa", "KamadrukkaraVeerini", "Gahwaraa", "Kundini",
            "Kanthaa", "Vishakhya", "Vishanaasini", "Nirmada", "Seethala",
            "Nimnaa", "Preeta", "Priyavivardhani", "Manaadha", "Durbhaga",
            "Chitraa", "Vichitra", "Chirajivini", "Boopa", "Gadaaharaa",
            "Naalaa", "Gaalavee", "Nirmalaa", "Nadi", "Sudha",
            "Mritamsuga", "Kaali", "Kaalika", "Kalushankura", "Krailokyamohanakari",
            "Mahaamaayaa", "Suseethala", "Sukhadaa", "Suprabhaa", "Sobhaa",
            "Sobhana", "Sivadaa", "Siva", "Balaa", "Jwalaa",
            "Gadaa", "Gaadaa", "Sootana", "Sumanoharaa", "Somavalli",
            "Somalatha", "Mangala", "Mudrika", "Sudha", "Melaa",
            "Apavargaa", "Pasyathaa", "Navaneetha", "Nisachari", "Nivrithi",
            "Nirgathaa", "Saaraa", "Samagaa", "Samadaa", "Samaa",
            "Visvambharaa", "Kumari", "Kokila", "Kunjarakrithi", "Indra",
            "Swaahaa", "Swadha", "Vahni", "Preethaa", "Yakshi",
            "Achalaprabha", "Saarini", "Madhuraa", "Maitri", "Harini",
            "Haarini", "Maruthaa", "DHananjaya", "Dhanakari", "Dhanada",
            "Kaccapa", "Ambuja", "Isaani", "Soolini", "Raudri",
            "Sivaasivakari", "Kalaa", "Kundaa", "Mukundaa", "Bharata",
            "Kadali", "Smaraa", "Basitha", "Kodala", "Kokilamsa",
            "Kaamini", "Kalasodbhava", "Viraprasoo", "Sangaraa", "Sathayagna",
            "Sataavari", "Sragvi", "Paatalini", "Naagapankaja", "Parameswari"
        };
        private string AmsaRuler(BodyPosition bp, DivisionPosition dp)
        {

            if (dp.RulerIndex == 0) return "";
            int ri = dp.RulerIndex - 1;

            if (options.DivisionType.MultipleDivisions.Length == 1)
                switch (options.DivisionType.MultipleDivisions[0].Varga)
                {
                    case DivisionType.HoraParasara: return mRulersHora[ri];
                    case DivisionType.DrekkanaParasara: return mRulersDrekkana[ri];
                    case DivisionType.Chaturthamsa: return mRulersChaturthamsa[ri];
                    case DivisionType.Saptamsa: return mRulersSaptamsa[ri];
                    case DivisionType.Navamsa: return mRulersNavamsa[ri];
                    case DivisionType.Dasamsa: return mRulersDasamsa[ri];
                    case DivisionType.Dwadasamsa: return mRulersDwadasamsa[ri];
                    case DivisionType.Shodasamsa: return mRulersShodasamsa[ri];
                    case DivisionType.Vimsamsa: return mRulersVimsamsa[ri];
                    case DivisionType.Chaturvimsamsa: return mRulersChaturvimsamsa[ri];
                    case DivisionType.Nakshatramsa: return mRulersNakshatramsa[ri];
                    case DivisionType.Trimsamsa: return mRulersTrimsamsa[ri];
                    case DivisionType.Khavedamsa: return mRulersKhavedamsa[ri];
                    case DivisionType.Akshavedamsa: return mRulersAkshavedamsa[ri];
                    case DivisionType.Shashtyamsa: return mRulersShashtyamsa[ri];
                    case DivisionType.Nadiamsa: return mRulersNadiamsaCKN[ri];
                    case DivisionType.NadiamsaCKN: return mRulersNadiamsaCKN[ri];
                }
            return "";
        }

        private string GetBodyString(BodyPosition bp)
        {
            string dir = bp.SpeedLongitude >= 0.0 ? "" : " (R)";

            if (bp.name == panchang.BodyName.Other ||
                bp.name == panchang.BodyName.MrityuPoint)
                return bp.otherString + dir;

            return bp.name.ToString();
        }
        private bool CheckBodyForCurrentView(BodyPosition bp)
        {
            switch (vt)
            {
                case ViewType.ViewMrityuLongitudes:
                    if (bp.name == panchang.BodyName.MrityuPoint) return true;
                    return false;
                case ViewType.ViewOtherLongitudes:
                    if (bp.name == panchang.BodyName.Other &&
                        bp.type != BodyType.Name.Sahama) return true;
                    return false;
                case ViewType.ViewSahamaLongitudes:
                    if (bp.type == BodyType.Name.Sahama) return true;
                    return false;
                case ViewType.ViewBasicGrahas:
                    if (bp.name == panchang.BodyName.MrityuPoint ||
                        bp.name == panchang.BodyName.Other)
                        return false;
                    return true;
            }
            return true;
        }
        private void RepopulateBasicGrahas()
        {
            mList.Columns.Clear();
            mList.Items.Clear();

            ArrayList al = new ArrayList();
            for (int i = (int)panchang.BodyName.Sun; i <= (int)panchang.BodyName.Rahu; i++)
            {
                BodyName b = (BodyName)i;
                BodyPosition bp = h.GetPosition(b);
                BodyKarakaComparer bkc = new BodyKarakaComparer(bp);
                al.Add(bkc);
            }
            al.Sort();
            int[] karaka_indices = new int[9];
            for (int i = 0; i < al.Count; i++)
            {
                BodyKarakaComparer bk = (BodyKarakaComparer)al[i];
                karaka_indices[(int)bk.GetPosition.name] = i;
            }


            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Longitude", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Nakshatra", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Pada", -1, HorizontalAlignment.Left);
            mList.Columns.Add("NakLord", -1, HorizontalAlignment.Left);
            mList.Columns.Add(options.DivisionType.ToString(), 100, HorizontalAlignment.Left);
            mList.Columns.Add("Part", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Ruler", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Cusp Start", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Cusp End", -2, HorizontalAlignment.Left);


            foreach (BodyPosition bp in h.PositionList)
            {
                if (false == CheckBodyForCurrentView(bp))
                    continue;

                ListViewItem li = new ListViewItem
                {
                    Text = GetBodyString(bp)
                };

                if ((int)bp.name >= (int)panchang.BodyName.Sun && (int)bp.name <= (int)panchang.BodyName.Rahu)
                    li.Text = string.Format("{0}   {1}",
                        li.Text, karakas_s[karaka_indices[(int)bp.name]]);

                li.SubItems.Add(longitudeToString(bp.Longitude));
                li.SubItems.Add(bp.Longitude.ToNakshatra().Value.ToString());
                li.SubItems.Add(bp.Longitude.ToNakshatraPada().ToString());
                li.SubItems.Add(getNakLord(bp.Longitude));

                DivisionPosition dp = bp.ToDivisionPosition(options.DivisionType);
                li.SubItems.Add(dp.ZodiacHouse.Value.ToString());
                li.SubItems.Add(dp.Part.ToString());
                li.SubItems.Add(AmsaRuler(bp, dp));
                li.SubItems.Add(longitudeToString(new Longitude(dp.CuspLower)));
                li.SubItems.Add(longitudeToString(new Longitude(dp.CuspHigher)));

                mList.Items.Add(li);

            }
            ColorAndFontRows(mList);
            ResizeColumns();
        }

        private void Repopulate()
        {
            mList.BeginUpdate();
            switch (vt)
            {
                case ViewType.ViewBasicGrahas: RepopulateBasicGrahas(); break;
                case ViewType.ViewOtherLongitudes: RepopulateBasicGrahas(); break;
                case ViewType.ViewMrityuLongitudes: RepopulateBasicGrahas(); break;
                case ViewType.ViewSahamaLongitudes: RepopulateBasicGrahas(); break;
                case ViewType.ViewSpecialTithis: RepopulateSpecialTithis(); break;
                case ViewType.ViewSpecialTaras: RepopulateSpecialTaras(); break;
                case ViewType.ViewBhavaCusps: RepopulateBhavaCusps(); break;
                case ViewType.ViewAstronomicalInfo: RepopulateAstronomicalInfo(); break;
                case ViewType.ViewNakshatraAspects: RepopulateNakshatraAspects(); break;
                case ViewType.ViewCharaKarakas: RepopulateCharaKarakas(); break;
                case ViewType.ViewCharaKarakas7: RepopulateCharaKarakas(); break;
                case ViewType.ViewAvasthas: RepopulateAvasthas(); break;
                case ViewType.View64Navamsa: Repopulate64Navamsa(); break;
                case ViewType.ViewNonLonBodies: RepopulateNonLonBodies(); break;
            }
            mList.EndUpdate();
        }
        void OnRecalculate(object o)
        {
            Repopulate();
        }

        void OnRedisplay(object o)
        {
            ColorAndFontRows(mList);
        }

        protected override void copyToClipboard()
        {
        }

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //mChangeView_Click (sender, e);
        }

        private void mList_MouseHover(object sender, EventArgs e)
        {

        }

        private void ResetMenuItems()
        {
            foreach (MenuItem mi in ContextMenu.MenuItems)
                mi.Checked = false;
            menuChangeVarga.Enabled = false;
            menuCopyLon.Enabled = false;
        }
        private void menuBasicGrahas_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuCopyLon.Enabled = true;
            menuBasicGrahas.Checked = true;
            vt = ViewType.ViewBasicGrahas;
            Repopulate();
        }

        private void menuSpecialTithis_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuSpecialTithis.Checked = true;
            vt = ViewType.ViewSpecialTithis;
            Repopulate();
        }

        private void menuSpecialTaras_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuSpecialTaras.Checked = true;
            vt = ViewType.ViewSpecialTaras;
            Repopulate();
        }


        public object SetOptions(object o)
        {
            options.Copy(o);
            Repopulate();
            return options.Clone();
        }
        private void menuChangeVarga_Click(object sender, EventArgs e)
        {
            Options opts = new Options(options.Clone(), new ApplyOptions(SetOptions));
            opts.ShowDialog();
        }

        private void menuBhavaCusps_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuBhavaCusps.Checked = true;
            menuChangeVarga.Enabled = true;

            vt = ViewType.ViewBhavaCusps;
            Repopulate();
        }

        private void menuOtherLongitudes_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuOtherLongitudes.Checked = true;
            menuCopyLon.Enabled = true;
            vt = ViewType.ViewOtherLongitudes;
            Repopulate();
        }

        private void menuMrityuLongitudes_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuCopyLon.Enabled = true;
            menuMrityuLongitudes.Checked = true;
            vt = ViewType.ViewMrityuLongitudes;
            Repopulate();
        }

        private void menuAstroInfo_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuAstroInfo.Checked = true;
            vt = ViewType.ViewAstronomicalInfo;
            Repopulate();
        }

        private void menuNakshatraAspects_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuNakshatraAspects.Checked = true;
            vt = ViewType.ViewNakshatraAspects;
            Repopulate();
        }

        private void menuCharaKarakas_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuCharaKarakas.Checked = true;
            vt = ViewType.ViewCharaKarakas;
            Repopulate();
        }

        private void menuSahamaLongitudes_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuCopyLon.Enabled = true;
            menuSahamaLongitudes.Checked = true;
            vt = ViewType.ViewSahamaLongitudes;
            Repopulate();
        }

        private void menuAvasthas_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuAvasthas.Checked = true;
            vt = ViewType.ViewAvasthas;
            Repopulate();
        }

        private void calculationsContextMenu_Popup(object sender, EventArgs e)
        {

        }

        private void menuCharaKarakas7_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuCharaKarakas7.Checked = true;
            vt = ViewType.ViewCharaKarakas7;
            Repopulate();
        }

        private void menu64Navamsa_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menu64Navamsa.Checked = true;
            vt = ViewType.View64Navamsa;
            Repopulate();
        }

        private void menuCopyLon_Click(object sender, EventArgs e)
        {
            if (mList.SelectedItems.Count <= 0) return;
            ListViewItem li = mList.SelectedItems[0];
            Clipboard.SetDataObject(li.SubItems[1].Text, false);
        }

        private void menuNonLonBodies_Click(object sender, EventArgs e)
        {
            ResetMenuItems();
            menuChangeVarga.Enabled = true;
            menuNonLonBodies.Checked = true;
            vt = ViewType.ViewNonLonBodies;
            Repopulate();
        }

        private void mList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void mList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
            {
                Division div = Division.CopyFromClipboard();
                if (null == div) return;
                options.DivisionType = div;
                OnRecalculate(options);
            }
        }

    }
}
