using System.Drawing;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{

    public class SplitContainer : UserControl
    {
        private UserControl mControl2;
        private UserControl mControl1;
        private int nItems;
        public Splitter sp;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public enum DrawStyle : int
        {
            LeftRight, UpDown
        }
        private DrawStyle mDrawDock;
        public DrawStyle DrawDock
        {
            get { return mDrawDock; }
            set
            {
                mDrawDock = value;

                if (nItems < 1)
                {
                    mControl1.Dock = DockStyle.Fill;
                    return;
                }

                if (mDrawDock == DrawStyle.UpDown)
                {
                    mControl1.Dock = DockStyle.Top;
                    sp.Dock = DockStyle.Top;
                }
                else
                {
                    mControl1.Dock = DockStyle.Left;
                    sp.Dock = DockStyle.Left;
                }
                mControl2.Dock = DockStyle.Fill;
            }
        }

        public UserControl Control1
        {
            get { return mControl1; }
            set { mControl1 = value; }
        }
        public UserControl Control2
        {
            get { return mControl2; }
            set
            {
                mControl2 = value;
                DrawDock = DrawDock;
                //mControl1.Dock = DockStyle.Left;
                //mControl2.Dock = DockStyle.Fill;
                if (nItems == 1)
                {
                    nItems++;
                    Controls.Remove(mControl1);
                    /*if (this.DrawDock == DrawStyle.UpDown)
						sp.SplitPosition = this.Width / 2;
					else
						sp.SplitPosition = this.Height / 2;
					*/
                    Controls.AddRange(new Control[] { mControl2, sp, mControl1 });
                }
            }
        }
        public DockStyle SplitterDockStyle
        {
            get { return sp.Dock; }
            set { sp.Dock = value; }
        }

        public SplitContainer(UserControl _mControl)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            mControl1 = _mControl;
            mControl1.Dock = DockStyle.Fill;
            Controls.Add(mControl1);
            sp = new Splitter
            {
                BackColor = Color.LightGray,
                Dock = DockStyle.Left
            };
            DrawDock = DrawStyle.LeftRight;
            nItems = 1;

            Dock = DockStyle.Fill;
            sp.Height += 2;
            sp.Width += 2;

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
            // 
            // MhoraSplitContainer
            // 
            Name = "MhoraSplitContainer";
            Resize += new System.EventHandler(PanchangSplitContainer_Resize);
            Load += new System.EventHandler(PanchangSplitContainer_Load);

        }
        #endregion

        private void PanchangSplitContainer_Load(object sender, System.EventArgs e)
        {

        }

        private void PanchangSplitContainer_Resize(object sender, System.EventArgs e)
        {

        }
    }
}
