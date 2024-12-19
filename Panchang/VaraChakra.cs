

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{
    public class VaraChakra : PanchangControl
    {
        private IContainer components = null;
        private ContextMenu contextMenu;
        private Bitmap bmpBuffer = null;
        private Pen pn_black = null;
        private Pen pn_grey = null;
        private Brush b_black = null;
        private Font f = null;

        public VaraChakra(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            h = _h;
            h.Changed += new EvtChanged(OnRecalculate);
            GlobalOptions.DisplayPrefsChanged += new EvtChanged(OnResize);
            pn_black = new Pen(Color.Black, (float)0.1);
            pn_grey = new Pen(Color.Gray, (float)0.1);
            b_black = new SolidBrush(Color.Black);
            AddViewsToContextMenu(contextMenu);
            OnResize(GlobalOptions.Instance);
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
            contextMenu = new ContextMenu();
            // 
            // VaraChakra
            // 
            ContextMenu = contextMenu;
            Name = "VaraChakra";
            Size = new Size(456, 240);
            Resize += new EventHandler(VaraChakra_Resize);
            Load += new EventHandler(VaraChakra_Load);
            Paint += new PaintEventHandler(VaraChakra_Paint);

        }
        #endregion

        private void VaraChakra_Load(object sender, EventArgs e)
        {

        }
        public void OnResize(object o)
        {
            f = new Font(
                GlobalOptions.Instance.GeneralFont.FontFamily,
                GlobalOptions.Instance.GeneralFont.SizeInPoints - 4
                );
            DrawToBuffer(true);
            Invalidate();
        }
        public void OnRecalculate(object o)
        {
            Invalidate();
        }
        private void ResetChakra(Graphics g, double rot)
        {
            int size = Math.Min(bmpBuffer.Width, bmpBuffer.Height);
            float scale = (float)size / 310;
            g.ResetTransform();
            g.TranslateTransform(bmpBuffer.Width / 2, bmpBuffer.Height / 2);
            g.ScaleTransform(scale, scale);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.RotateTransform((float)(270.0 + 360.0 / (9.0 * 2.0) - rot));
        }
        private void DrawChakra(Graphics g)
        {
            BodyName[] bodies = new BodyName[]
            {
                BodyName.Sun, BodyName.Moon, BodyName.Mars, BodyName.Mercury,
                BodyName.Jupiter, BodyName.Venus, BodyName.Saturn,
                BodyName.Rahu, BodyName.Ketu
            };

            g.Clear(GlobalOptions.Instance.ChakraBackgroundColor);

            ResetChakra(g, 0.0);
            g.DrawEllipse(pn_grey, -150, -150, 300, 300);
            g.DrawEllipse(pn_grey, -140, -140, 280, 280);

            for (int i = 0; i < 9; i++)
            {
                ResetChakra(g, i * (360.0 / 9.0));
                g.DrawLine(pn_grey, 0, 0, 150, 0);
            }

            for (int i = 0; i < 9; i++)
            {
                ResetChakra(g, i * (360.0 / 9.0) + 360.0 / (9.0 * 2.0));
                g.TranslateTransform(135, 0);
                g.RotateTransform((float)90.0);
                SizeF sz = g.MeasureString(Body.ToString(bodies[i]), f);
                g.DrawString(Body.ToString(bodies[i]), f, b_black, -sz.Width / 2, 0);
            }

            if (h.IsDayBirth())
            {

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

        private void VaraChakra_Resize(object sender, EventArgs e)
        {
            DrawToBuffer(true);
            Invalidate();
        }

        private void VaraChakra_Paint(object sender, PaintEventArgs e)
        {
            DrawChakra(e.Graphics);
        }
        protected override void copyToClipboard()
        {
            Clipboard.SetDataObject(bmpBuffer);
        }

    }
}

