using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    public class AshtakavargaControl : PanchangControl
    {

        private readonly System.ComponentModel.IContainer components = null;
        private ContextMenu contextMenu;
        private MenuItem menuSav;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuPavSun;

        private MenuItem menuPavMoon;
        private MenuItem menuPavMars;
        private MenuItem menuPavMercury;
        private MenuItem menuPavJupiter;
        private MenuItem menuPavVenus;
        private MenuItem menuPavSaturn;
        private MenuItem menuPavLagna;

        Ashtakavarga av = null;
        BodyName[] outerBodies = null;
        readonly BodyName[] innerBodies = null;
        private MenuItem menuOptions;
        Bitmap bmpBuffer = null;
        private MenuItem menuJhoraSav;
        readonly AshtakavargaOptions userOptions = null;
        EDisplayStyle mDisplayStyle = EDisplayStyle.Chancha;
        Font fBig = null;
        Font fBigBold = null;
        readonly Brush b_black = null;
        Brush b_red = null;
        public AshtakavargaControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            userOptions = new AshtakavargaOptions();
            h = _h;
            h.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(onRedisplay);
            av = new Ashtakavarga(h, userOptions.VargaType);
            outerBodies = new BodyName[]
            {
                BodyName.Sun, BodyName.Moon, BodyName.Mars,
                BodyName.Mercury, BodyName.Jupiter, BodyName.Venus,
                BodyName.Saturn, BodyName.Lagna
            };

            b_black = new SolidBrush(Color.Black);

            innerBodies = (BodyName[])outerBodies.Clone();
            resetContextMenuChecks(menuSav);
            onRedisplay(PanchangAppOptions.Instance);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// 

        private void onRedisplay(object o)
        {
            userOptions.ChartStyle = PanchangAppOptions.Instance.VargaStyle;
            fBig = new Font(PanchangAppOptions.Instance.GeneralFont.FontFamily,
                PanchangAppOptions.Instance.GeneralFont.SizeInPoints + 3);
            fBigBold = new Font(PanchangAppOptions.Instance.GeneralFont.FontFamily,
                PanchangAppOptions.Instance.GeneralFont.SizeInPoints + 3,
                FontStyle.Bold | FontStyle.Underline |
                FontStyle.Italic);
            b_red = new SolidBrush(PanchangAppOptions.Instance.VargaGrahaColor);
            DrawToBuffer();
            Invalidate();
        }
        private void OnRecalculate(object _h)
        {
            av = new Ashtakavarga(h, userOptions.VargaType);
            DrawToBuffer();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
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
            contextMenu = new ContextMenu();
            menuOptions = new MenuItem();
            menuJhoraSav = new MenuItem();
            menuSav = new MenuItem();
            menuPavSun = new MenuItem();
            menuPavMoon = new MenuItem();
            menuPavMars = new MenuItem();
            menuPavMercury = new MenuItem();
            menuPavJupiter = new MenuItem();
            menuPavVenus = new MenuItem();
            menuPavSaturn = new MenuItem();
            menuPavLagna = new MenuItem();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            // 
            // contextMenu
            // 
            contextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                        menuOptions,
                                                                                        menuJhoraSav,
                                                                                        menuSav,
                                                                                        menuPavSun,
                                                                                        menuPavMoon,
                                                                                        menuPavMars,
                                                                                        menuPavMercury,
                                                                                        menuPavJupiter,
                                                                                        menuPavVenus,
                                                                                        menuPavSaturn,
                                                                                        menuPavLagna,
                                                                                        menuItem1,
                                                                                        menuItem2});
            // 
            // menuOptions
            // 
            menuOptions.Index = 0;
            menuOptions.Text = "Options";
            menuOptions.Click += new EventHandler(menuOptions_Click);
            // 
            // menuJhoraSav
            // 
            menuJhoraSav.Index = 1;
            menuJhoraSav.Text = "SAV, PAV";
            menuJhoraSav.Click += new EventHandler(menuJhoraSav_Click);
            // 
            // menuSav
            // 
            menuSav.Index = 2;
            menuSav.Text = "SAV, PAV, BAV";
            menuSav.Click += new EventHandler(menuSav_Click);
            // 
            // menuPavSun
            // 
            menuPavSun.Index = 3;
            menuPavSun.Text = "PAV - Sun";
            menuPavSun.Click += new EventHandler(menuPavSun_Click);
            // 
            // menuPavMoon
            // 
            menuPavMoon.Index = 4;
            menuPavMoon.Text = "PAV - Moon";
            menuPavMoon.Click += new EventHandler(menuPavMoon_Click);
            // 
            // menuPavMars
            // 
            menuPavMars.Index = 5;
            menuPavMars.Text = "PAV - Mars";
            menuPavMars.Click += new EventHandler(menuPavMars_Click);
            // 
            // menuPavMercury
            // 
            menuPavMercury.Index = 6;
            menuPavMercury.Text = "PAV - Mercury";
            menuPavMercury.Click += new EventHandler(menuPavMercury_Click);
            // 
            // menuPavJupiter
            // 
            menuPavJupiter.Index = 7;
            menuPavJupiter.Text = "PAV - Jupiter";
            menuPavJupiter.Click += new EventHandler(menuPavJupiter_Click);
            // 
            // menuPavVenus
            // 
            menuPavVenus.Index = 8;
            menuPavVenus.Text = "PAV - Venus";
            menuPavVenus.Click += new EventHandler(menuPavVenus_Click);
            // 
            // menuPavSaturn
            // 
            menuPavSaturn.Index = 9;
            menuPavSaturn.Text = "PAV - Saturn";
            menuPavSaturn.Click += new EventHandler(menuPavSaturn_Click);
            // 
            // menuPavLagna
            // 
            menuPavLagna.Index = 10;
            menuPavLagna.Text = "PAV - Lagna";
            menuPavLagna.Click += new EventHandler(menuPavLagna_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 11;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 12;
            menuItem2.Text = "-";
            // 
            // AshtakavargaControl
            // 
            AllowDrop = true;
            ContextMenu = contextMenu;
            Name = "AshtakavargaControl";
            Size = new Size(208, 128);
            DragEnter += new DragEventHandler(AshtakavargaControl_DragEnter);
            Resize += new EventHandler(AshtakavargaControl_Resize);
            Load += new EventHandler(AshtakavargaControl_Load);
            Paint += new PaintEventHandler(AshtakavargaControl_Paint);
            DragDrop += new DragEventHandler(AshtakavargaControl_DragDrop);

        }
        #endregion

        private void AshtakavargaControl_Load(object sender, EventArgs e)
        {
            AddViewsToContextMenu(contextMenu);
        }


        private void DrawJhoraChakra(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (false == PrintMode)
                g.Clear(BackColor);
            int offset = 5;
            int size = Math.Min(bmpBuffer.Width, bmpBuffer.Height) / 3 - 10;
            IDrawChart dc = null;
            switch (userOptions.ChartStyle)
            {
                default:
                case EChartStyle.EastIndian:
                    dc = new EastIndianChart();
                    break;
                case EChartStyle.SouthIndian:
                    dc = new SouthIndianChart();
                    break;
            }

            BodyName[] bin_body = new BodyName[] {
                                                BodyName.Lagna,
                                                BodyName.Lagna, BodyName.Sun, BodyName.Moon,
                                                BodyName.Mars, BodyName.Mercury, BodyName.Jupiter,
                                                BodyName.Venus, BodyName.Saturn };
            int[][] bins = new int[9][];

            if (userOptions.SavType == ESavType.Normal)
                bins[0] = av.GetSav();
            else
                bins[0] = av.GetSavRao();

            bins[1] = av.GetPav(BodyName.Lagna);
            bins[2] = av.GetPav(BodyName.Sun);
            bins[3] = av.GetPav(BodyName.Moon);
            bins[4] = av.GetPav(BodyName.Mars);
            bins[5] = av.GetPav(BodyName.Mercury);
            bins[6] = av.GetPav(BodyName.Jupiter);
            bins[7] = av.GetPav(BodyName.Venus);
            bins[8] = av.GetPav(BodyName.Saturn);

            string[] strs = new string[9];
            strs[0] = "SAV";
            strs[1] = Body.ToString(BodyName.Lagna);
            strs[2] = Body.ToString(BodyName.Sun);
            strs[3] = Body.ToString(BodyName.Moon);
            strs[4] = Body.ToString(BodyName.Mars);
            strs[5] = Body.ToString(BodyName.Mercury);
            strs[6] = Body.ToString(BodyName.Jupiter);
            strs[7] = Body.ToString(BodyName.Venus);
            strs[8] = Body.ToString(BodyName.Saturn);

            Brush b_background = new SolidBrush(PanchangAppOptions.Instance.ChakraBackgroundColor);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    g.ResetTransform();
                    g.TranslateTransform(i * size + (i + 1) * offset, j * size + (j + 1) * offset);
                    float scale = size / (float)dc.GetLength();
                    g.ScaleTransform(scale, scale);
                    if (false == PrintMode)
                        g.FillRectangle(b_background, 0, 0, dc.GetLength(), dc.GetLength());
                    dc.DrawOutline(g);
                    int off = j * 3 + i;
                    int[] bin = bins[off];
                    Debug.Assert(bin.Length == 12, "PAV/SAV: unknown size");
                    for (int z = 0; z < 12; z++)
                    {
                        Font f = fBig;
                        int zh = (int)h.GetPosition(bin_body[off]).ToDivisionPosition(userOptions.VargaType).ZodiacHouse.Value;
                        if (z == zh - 1)
                            f = fBigBold;
                        Point p = dc.GetSingleItemOffset(new ZodiacHouse((ZodiacHouseName)z + 1));
                        g.DrawString(bin[z].ToString(), f, b_black, p);
                    }
                    SizeF sz = g.MeasureString(strs[off], fBig);
                    g.DrawString(strs[off], fBig, b_red, 100 - sz.Width / 2, 100 - sz.Height / 2);

                    if (off == 0 && userOptions.SavType == ESavType.Rao)
                    {
                        sz = g.MeasureString("Rao", fBig);
                        g.DrawString("Rao", fBig, b_red, 100 - sz.Width / 2, 120 - sz.Height / 2);
                    }
                }
            }
        }

        private Image DrawToBuffer()
        {
            bmpBuffer?.Dispose();
            if (Width == 0 || Height == 0)
                return bmpBuffer;
            Graphics displayGraphics = CreateGraphics();
            bmpBuffer = new Bitmap(Width, Height, displayGraphics);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);

            switch (mDisplayStyle)
            {
                case EDisplayStyle.Chancha:
                    DrawChanchaChakra(imageGraphics);
                    break;
                case EDisplayStyle.NavaSav:
                    DrawJhoraChakra(imageGraphics);
                    break;
            }

            displayGraphics.Dispose();
            return bmpBuffer;
        }

        public Bitmap DrawChanchaToImage(int size)
        {
            bmpBuffer = new Bitmap(size, size);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            DrawChanchaChakra(imageGraphics);
            return bmpBuffer;
        }
        public Bitmap DrawNavaChakrasToImage(int size)
        {
            bmpBuffer = new Bitmap(size, size);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            DrawJhoraChakra(imageGraphics);
            return bmpBuffer;
        }

        private void AshtakavargaControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpBuffer, 0, 0);
        }

        private bool ChanchaReset(Graphics g, int ray)
        {
            int outerSize = Math.Min(bmpBuffer.Width, bmpBuffer.Height);
            float scaleOuter = (float)outerSize / 200;

            g.ResetTransform();
            g.TranslateTransform(bmpBuffer.Width / 2, bmpBuffer.Height / 2);
            g.ScaleTransform(scaleOuter, scaleOuter);

            int numEntries = 8 * 12;
            float rotDegree = (float)360.0 / numEntries;
            float rotTotal = 0;

            switch (userOptions.ChartStyle)
            {
                case EChartStyle.SouthIndian:
                    rotTotal = rotDegree * ray - 120;
                    g.RotateTransform(rotTotal);
                    break;
                case EChartStyle.EastIndian:
                    rotTotal = -1 * rotDegree * ray - 75 - rotDegree;
                    g.RotateTransform(rotTotal);
                    break;
            }

            while (rotTotal < 0) rotTotal += 360;
            while (rotTotal > 360) rotTotal -= 360;

            switch (userOptions.ChartStyle)
            {
                case EChartStyle.SouthIndian:
                    if (0 <= rotTotal && rotTotal < 90 ||
                        270 <= rotTotal && rotTotal < 360)
                        return true;
                    return false;
                case EChartStyle.EastIndian:
                    if (0 <= rotTotal && rotTotal < 105 ||
                        285 <= rotTotal && rotTotal < 360)
                        return true;
                    return false;
            }

            return true;
        }

        private void DrawChanchaInner(Graphics g)
        {

            IDrawChart dc = null;
            switch (userOptions.ChartStyle)
            {
                default:
                case EChartStyle.EastIndian:
                    dc = new EastIndianChart();
                    break;
                case EChartStyle.SouthIndian:
                    dc = new SouthIndianChart();
                    break;
            }
            int innerSize = (int)(Math.Min(bmpBuffer.Width, bmpBuffer.Height) / 3.15);
            float scaleInner = innerSize / (float)dc.GetLength();

            g.ResetTransform();
            g.TranslateTransform(bmpBuffer.Width / 2, bmpBuffer.Height / 2);
            g.TranslateTransform(-1 * innerSize / 2, -1 * innerSize / 2);
            g.ScaleTransform(scaleInner, scaleInner);

            dc.DrawOutline(g);

            int[] inner_bindus;

            if (outerBodies.Length > 1)
            {
                if (userOptions.SavType == ESavType.Rao)
                    inner_bindus = av.GetSavRao();
                else
                    inner_bindus = av.GetSav();
            }
            else
                inner_bindus = av.GetPav(outerBodies[0]);

            for (int i = 0; i < 12; i++)
            {
                ZodiacHouse zh = new ZodiacHouse((ZodiacHouseName)i + 1);
                Point p = dc.GetSingleItemOffset(zh);
                g.DrawString(inner_bindus[i].ToString(), fBig, b_black, p);
            }

            string av_desc = "SAV";
            if (outerBodies.Length == 1) av_desc = "PAV";
            SizeF sz = g.MeasureString(av_desc, fBig);
            g.DrawString(av_desc, fBig, b_black, 100 - sz.Width / 2, 80 - sz.Height / 2);

            if (outerBodies.Length == 1)
            {
                string desc = Body.ToString(outerBodies[0]);
                sz = g.MeasureString(desc, fBig);
                g.DrawString(desc, fBig, b_black, 100 - sz.Width / 2, 120 - sz.Height / 2);
            }

            if (userOptions.SavType == ESavType.Rao)
            {
                string desc = "Rao";
                sz = g.MeasureString(desc, fBig);
                g.DrawString(desc, fBig, b_black, 100 - sz.Width / 2, 120 - sz.Height / 2);
            }

            {
                string desc = Basics.NumPartsInDivisionString(userOptions.VargaType); ;
                sz = g.MeasureString(desc, fBig);
                g.DrawString(desc, fBig, b_black, 100 - sz.Width / 2, 100 - sz.Height / 2);
            }

        }

        public bool PrintMode = false;
        private void DrawChanchaChakra(Graphics g)
        {
            string[] sBindus = new string[] { "Su", "Mo", "Ma", "Me", "Ju", "Ve", "Sa", "As" };
            Pen pn_black = new Pen(Color.Black, (float)0.01);
            Pen pn_grey = new Pen(Color.LightGray, (float)0.01);
            Pen pn_dgrey = new Pen(Color.Gray, (float)0.01);
            Brush b_black = new SolidBrush(Color.Black);
            Brush b_red = new SolidBrush(Color.Red);
            Font f = new Font(PanchangAppOptions.Instance.FixedWidthFont.FontFamily,
                PanchangAppOptions.Instance.FixedWidthFont.SizeInPoints - 6);


            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (PrintMode == false)
                g.Clear(PanchangAppOptions.Instance.ChakraBackgroundColor);

            DrawChanchaInner(g);


            // inner and outer bounding circles
            ChanchaReset(g, 0);
            for (int i = 1; i <= 8; i++)
            {
                int w = 45 + i * 4;
                g.DrawEllipse(pn_grey, -w, -w, w * 2, w * 2);
            }
            g.DrawEllipse(pn_black, -45, -45, 90, 90);
            g.DrawEllipse(pn_black, -85, -85, 85 * 2, 85 * 2);
            g.DrawEllipse(pn_black, -98, -98, 98 * 2, 98 * 2);


            // draw per-spoke stuff: spoke, bindus
            int numEntries = 8 * 12;
            float rotDegree = (float)360.0 / numEntries;
            for (int i = 0; i < numEntries; i++)
            {
                bool bDir = ChanchaReset(g, i);
                Pen p = pn_grey;
                if (i % 8 == 7 && userOptions.ChartStyle == EChartStyle.EastIndian) p = pn_black;
                if (i % 8 == 0 && userOptions.ChartStyle == EChartStyle.SouthIndian) p = pn_black;
                g.DrawLine(p, 45, 0, 98, 0);

                Brush b = b_black;
                //if (this.outerBodies.Length == 1 &&	av.BodyToInt(this.outerBodies[0]) == i%8)
                //	b = b_red;
                if (outerBodies.Length > 1)
                {
                    if (bDir == true)
                        g.DrawString(sBindus[i % 8], f, b, 49 + 9 * 4, 0);
                    else
                    {
                        g.ScaleTransform((float)-1.0, (float)-1.0);
                        g.DrawString(sBindus[i % 8], f, b, -1 * (49 + 11 * 4), -6);
                    }
                }
                //g.DrawString(i.ToString(), f, b, 49+9*4, 0);
            }

            // write the pav values at the top of the circle
            foreach (BodyName bOuter in outerBodies)
            {
                int[] pav = av.GetPav(bOuter);
                for (int i = 0; i < 12; i++)
                {
                    int iRing = i * 8 + av.BodyToInt(bOuter);
                    bool bDir = ChanchaReset(g, iRing);
                    SizeF sz = g.MeasureString(pav[i].ToString(), f);
                    if (true == bDir)
                        g.DrawString(pav[i].ToString(), f, b_black, 49 + 7 * 4, 0);
                    else
                    {
                        g.ScaleTransform((float)-1.0, (float)-1.0);
                        g.DrawString(pav[i].ToString(), f, b_black,
                            new RectangleF(-1 * (49 + 7 * 4 + sz.Width), -1 * (sz.Height - 1), sz.Width, sz.Height));
                    }

                }
            }

            // draw the bindus
            foreach (BodyName bOuter in outerBodies)
            {
                foreach (BodyName bInner in innerBodies)
                {
                    int iOuter = av.BodyToInt(bOuter);
                    int iInner = av.BodyToInt(bInner);
                    ZodiacHouseName[] zhBins = av.GetBindus(bOuter, bInner);
                    Brush br = new SolidBrush(PanchangAppOptions.Instance.GetBinduColor(bInner));

                    foreach (ZodiacHouseName zh in zhBins)
                    {
                        int iRing = ((int)zh - 1) * 8 + iInner;
                        bool bDir = ChanchaReset(g, iRing);
                        g.FillEllipse(br, 50 + (iOuter - 1) * 4, 1, 2, 2);
                        g.DrawEllipse(pn_dgrey, 50 + (iOuter - 1) * 4, 1, 2, 2);

                        if (outerBodies.Length == 1)
                        {
                            if (true == bDir)
                                g.DrawString(sBindus[iRing % 8], f, b_black, 49 + 9 * 4, 0);
                            else
                            {
                                g.ScaleTransform((float)-1.0, (float)-1.0);
                                g.DrawString(sBindus[iRing % 8], f, b_black, -1 * (49 + 11 * 4), -6);
                            }
                        }
                    }
                }
            }


        }

        private void menuSav_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[]
            {
                BodyName.Sun, BodyName.Moon, BodyName.Mars,
                BodyName.Mercury, BodyName.Jupiter, BodyName.Venus,
                BodyName.Saturn, BodyName.Lagna
            };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuSav);
        }

        private void menuPavSun_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Sun };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavSun);
        }

        private void menuPavMoon_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Moon };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavMoon);
        }

        private void menuPavJupiter_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Jupiter };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavJupiter);
        }

        private void menuPavMars_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Mars };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavMars);
        }

        private void menuPavMercury_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Mercury };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavMercury);
        }

        private void menuPavVenus_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Venus };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavVenus);
        }

        private void menuPavSaturn_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Saturn };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavSaturn);
        }

        private void menuPavLagna_Click(object sender, EventArgs e)
        {
            outerBodies = new BodyName[] { BodyName.Lagna };
            mDisplayStyle = EDisplayStyle.Chancha;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuPavLagna);
        }

        private void AshtakavargaControl_Resize(object sender, EventArgs e)
        {
            DrawToBuffer();
            Invalidate();
        }
        protected override void copyToClipboard()
        {
            Clipboard.SetDataObject(bmpBuffer);
        }

        private object SetOptions(object o)
        {
            AshtakavargaOptions ao = (AshtakavargaOptions)o;
            if (ao.VargaType != userOptions.VargaType)
                av = new Ashtakavarga(h, ao.VargaType);
            userOptions.SetOptions(ao);
            DrawToBuffer();
            Invalidate();
            return userOptions.Clone();
        }
        private void menuOptions_Click(object sender, EventArgs e)
        {
            Options f = new Options(userOptions.Clone(), new ApplyOptions(SetOptions));
            f.ShowDialog();
        }

        private void menuJhoraSav_Click(object sender, EventArgs e)
        {
            mDisplayStyle = EDisplayStyle.NavaSav;
            DrawToBuffer();
            Invalidate();
            resetContextMenuChecks(menuJhoraSav);
        }

        private void resetContextMenuChecks(MenuItem mi)
        {
            menuJhoraSav.Checked = false;
            menuSav.Checked = false;
            menuPavLagna.Checked = false;
            menuPavSun.Checked = false;
            menuPavMoon.Checked = false;
            menuPavMars.Checked = false;
            menuPavMercury.Checked = false;
            menuPavJupiter.Checked = false;
            menuPavVenus.Checked = false;
            menuPavSaturn.Checked = false;
            mi.Checked = true;
        }

        private void AshtakavargaControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void AshtakavargaControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
            {
                Division div = Division.CopyFromClipboard();
                if (null == div) return;
                userOptions.VargaType = div;
                SetOptions(userOptions);
                OnRecalculate(h);
            }
        }
    }
}

