

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using org.transliteral.panchang;

namespace org.transliteral.panchang.app
{
    public class ChooseHoroscopeControl : Form
    {
        private ListBox lBox;
        private Button bOK;
        private IContainer components = null;

        public ChooseHoroscopeControl()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
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
            lBox = new ListBox();
            bOK = new Button();
            SuspendLayout();
            // 
            // lBox
            // 
            lBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            lBox.Location = new Point(8, 32);
            lBox.Name = "lBox";
            lBox.Size = new Size(344, 160);
            lBox.TabIndex = 0;
            // 
            // bOK
            // 
            bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                | AnchorStyles.Right;
            bOK.Location = new Point(136, 208);
            bOK.Name = "bOK";
            bOK.Size = new Size(75, 24);
            bOK.TabIndex = 1;
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);
            // 
            // ChooseHoroscopeControl
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(352, 238);
            Controls.Add(bOK);
            Controls.Add(lBox);
            Name = "ChooseHoroscopeControl";
            Text = "Please Choose An Open Horoscope";
            Load += new EventHandler(ChooseHoroscopeControl_Load);
            ResumeLayout(false);

        }
        #endregion

        public string GetHoroscopeName()
        {
            if (lBox.SelectedIndex < 0) return null;

            PanchangContainer mc = (PanchangContainer)PanchangAppOptions.mainControl;
            foreach (Form c in mc.MdiChildren)
            {
                if (c is PanchangChild)
                {
                    if (((PanchangChild)c).Name == (string)lBox.Items[lBox.SelectedIndex])
                    {
                        return ((PanchangChild)c).Name;
                    }
                }
            }
            return null;
        }
        public Horoscope GetHorsocope()
        {
            if (lBox.SelectedIndex < 0) return null;

            PanchangContainer mc = (PanchangContainer)PanchangAppOptions.mainControl;
            foreach (Form c in mc.MdiChildren)
            {
                if (c is PanchangChild)
                {
                    if (((PanchangChild)c).Name == (string)lBox.Items[lBox.SelectedIndex])
                    {
                        PanchangChild ch = (PanchangChild)c;
                        return ch.getHoroscope();
                    }
                }
            }
            return null;
        }

        private void ChooseHoroscopeControl_Load(object sender, EventArgs e)
        {
            PanchangContainer mc = (PanchangContainer)PanchangAppOptions.mainControl;
            foreach (Form c in mc.MdiChildren)
            {
                if (c is PanchangChild)
                {
                    lBox.Items.Add(((PanchangChild)c).Name);
                }
            }
            if (lBox.Items.Count > 0)
                lBox.SelectedIndex = 0;
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}

