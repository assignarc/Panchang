using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{
    public class NavamsaControl : PanchangControl
    {
        private IContainer components = null;
        public Bitmap bmpBuffer = null;
        private Pen pn_black = null;
        private Pen pn_grey = null;
        private Pen pn_lgrey = null;
        private ContextMenu contextMenu;
        private MenuItem menuItem1;
        private Font f = null;
        private string[] nak_s = null;

        public NavamsaControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            h = _h;
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(onRedisplay);
            h.Changed += new EvtChanged(onRecalculate);
            pn_black = new Pen(Color.Black, (float)0.1);
            pn_grey = new Pen(Color.Gray, (float)0.1);
            pn_lgrey = new Pen(Color.LightGray, (float)0.1);
            nak_s = new string[27]
            {
                "Asw", "Bha", "Kri", "Roh", "Mri", "Ard", "Pun", "Pus", "Asl",
                "Mag", "PPl", "UPh", "Has", "Chi", "Swa", "Vis", "Anu", "Jye",
                "Moo", "PAs", "UAs", "Sra", "Dha", "Sha", "PBh", "UBh", "Rev"
            };
            AddViewsToContextMenu(contextMenu);
            onRedisplay(PanchangAppOptions.Instance);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            PanchangAppOptions.DisplayPrefsChanged -= new EvtChanged(onRedisplay);
            h.Changed -= new EvtChanged(onRecalculate);
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
            contextMenu = new ContextMenu();
            menuItem1 = new MenuItem();
            // 
            // contextMenu
            // 
            contextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                        menuItem1});
            // 
            // menuItem1
            // 
            menuItem1.Index = 0;
            menuItem1.Text = "-";
            // 
            // NavamsaControl
            // 
            ContextMenu = contextMenu;
            Name = "NavamsaControl";
            Size = new Size(376, 240);
            Resize += new EventHandler(NavamsaControl_Resize);
            Load += new EventHandler(NavamsaControl_Load);
            Paint += new PaintEventHandler(NavamsaControl_Paint);

        }
        #endregion

        public void onRedisplay(object o)
        {
            f = new Font(
                PanchangAppOptions.Instance.GeneralFont.FontFamily,
                PanchangAppOptions.Instance.GeneralFont.SizeInPoints - 5);
            DrawToBuffer(true);
            Invalidate();
        }
        public void onRecalculate(object o)
        {
            DrawToBuffer(true);
            Invalidate();
        }
        private void ResetChakra(Graphics g, double rot)
        {
            int size = Math.Min(bmpBuffer.Width, bmpBuffer.Height);
            float scale = (float)size / 300;
            g.ResetTransform();
            g.TranslateTransform(bmpBuffer.Width / 2, bmpBuffer.Height / 2);
            g.ScaleTransform(scale, scale);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.RotateTransform((float)(270.0 - rot));
        }

        private void DrawInnerChakra(Graphics g)
        {
            int size = Math.Min(bmpBuffer.Width, bmpBuffer.Height);
            float scale = (float)size / 300 / 3;
            g.ResetTransform();
            g.TranslateTransform(bmpBuffer.Width / 2, bmpBuffer.Height / 2);
            g.ScaleTransform(scale, scale);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            EastIndianChart dc = new EastIndianChart();
            g.TranslateTransform(-1 * dc.GetLength() / 2, -1 * dc.GetLength() / 2);
            dc.DrawOutline(g);

        }
        public bool PrintMode = false;

        public void DrawChakra(Graphics g)
        {
            //this.DrawInnerChakra(g);

            if (false == PrintMode)
                g.Clear(PanchangAppOptions.Instance.ChakraBackgroundColor);

            ResetChakra(g, 0.0);
            g.DrawEllipse(pn_grey, -40, -40, 80, 80);
            g.DrawEllipse(pn_grey, -125, -125, 250, 250);
            g.DrawEllipse(pn_grey, -105, -105, 210, 210);
            g.DrawEllipse(pn_grey, -115, -115, 230, 230);

            BodyName[] bodies = new BodyName[10]
            {
                BodyName.Lagna, BodyName.Sun, BodyName.Moon, BodyName.Mars, BodyName.Mercury,
                BodyName.Jupiter, BodyName.Venus, BodyName.Saturn, BodyName.Rahu, BodyName.Ketu
            };

            for (int i = 0; i < 12; i++)
            {
                ResetChakra(g, i * 30);
                g.DrawLine(pn_lgrey, 40, 0, 125, 0);
            }
            for (int i = 0; i < 12; i++)
            {
                ResetChakra(g, i * 30 + 15);
                ZodiacHouseName z = (ZodiacHouseName)(i + 1);
                SizeF sz = g.MeasureString(z.ToString(), f);
                g.DrawString(z.ToString(), f, Brushes.Gray, 40 - sz.Width, 0);
            }
            for (int i = 0; i < 27; i++)
            {
                ResetChakra(g, (i + 1) * (360.0 / 27.0));//+((360.0/27.0)/2.0));
                g.TranslateTransform(105, 0);
                g.RotateTransform((float)90.0);
                SizeF sz = g.MeasureString(nak_s[i], f);
                g.DrawString(nak_s[i], f, Brushes.Gray, (float)(360.0 / 27.0) - sz.Width / 2, sz.Height / 2);
            }
            for (int i = 0; i < 27 * 4; i++)
            {
                ResetChakra(g, i * (360.0 / (27.0 * 4.0)));
                Pen p = pn_lgrey;
                if (i % 12 == 0) p = pn_black;
                g.DrawLine(p, 115, 0, 125, 0);

                p = pn_lgrey;
                if (i % 4 == 0) p = pn_black;
                g.DrawLine(p, 105, 0, 115, 0);


            }

            double dist_sat = h.GetPosition(BodyName.Saturn).Distance;
            foreach (BodyName b in bodies)
            {
                Pen pn_b = new Pen(PanchangAppOptions.Instance.getBinduColor(b));
                Brush br_b = new SolidBrush(PanchangAppOptions.Instance.getBinduColor(b));
                BodyPosition bp = h.GetPosition(b);
                ResetChakra(g, bp.Longitude.Value);
                int chWidth = 2;
                g.DrawEllipse(pn_black, 110 - chWidth, 0, 1, 1);
                g.FillEllipse(br_b, 120 - chWidth, -chWidth, chWidth * 2, chWidth * 2);
                g.DrawEllipse(pn_grey, 120 - chWidth, -chWidth, chWidth * 2, chWidth * 2);
                SizeF sz = g.MeasureString(b.ToString(), f);
                g.DrawString(b.ToString(), f, Brushes.Black, 125, -sz.Height / 2);

                // current position with distance
                int dist = (int)(bp.Distance / dist_sat * (105 - 40 - chWidth * 2));
                g.FillEllipse(br_b, 40 + dist - chWidth, -chWidth, chWidth * 2, chWidth * 2);
                g.DrawEllipse(pn_grey, 40 + dist - chWidth, -chWidth, chWidth * 2, chWidth * 2);

                // speed
                double dspSize = bp.SpeedLongitude / 360.0 * 12000.0;
                if (bp.SpeedLongitude < 0) dspSize *= 2.0;
                int spSize = (int)dspSize;
                if (spSize > 40) spSize = 40;
                if (bp.SpeedLongitude > 0)
                    g.DrawLine(pn_lgrey, 40 + dist, -chWidth, 40 + dist, -spSize);
                else
                    g.DrawLine(pn_lgrey, 40 + dist, chWidth, 40 + dist, -spSize);
            }
        }

        private Image DrawToBuffer(bool bRecalc)
        {
            if (bmpBuffer != null && bmpBuffer.Size != Size)
            {
                bmpBuffer.Dispose();
                bmpBuffer = null;
            }

            if (Width == 0 || Height == 0)
                return bmpBuffer;

            if (bRecalc == false && Width == bmpBuffer.Width && Height == bmpBuffer.Height)
                return bmpBuffer;

            Graphics displayGraphics = CreateGraphics();
            bmpBuffer = new Bitmap(Width, Height, displayGraphics);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            DrawChakra(imageGraphics);
            displayGraphics.Dispose();
            return bmpBuffer;
        }

        public Bitmap DrawToBitmap(int size)
        {
            bmpBuffer = new Bitmap(size, size);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            DrawChakra(imageGraphics);
            return bmpBuffer;
        }


        private void NavamsaControl_Load(object sender, EventArgs e)
        {
        }

        private void NavamsaControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpBuffer, 0, 0);
        }

        private void NavamsaControl_Resize(object sender, EventArgs e)
        {
            DrawToBuffer(true);
            Invalidate();
        }
        protected override void copyToClipboard()
        {
            Clipboard.SetDataObject(bmpBuffer);
        }

    }
}

