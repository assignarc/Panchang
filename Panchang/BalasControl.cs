using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{
    public class BalasControl : BaseControl
    {
        private ListView mList;
        private ContextMenu contextMenu;
        private IContainer components = null;

        public BalasControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            horoscope = _h;
            horoscope.Changed += new EvtChanged(onRecalculate);
            mList.BackColor = Color.AliceBlue;
            Repopulate();
            AddViewsToContextMenu(contextMenu);
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
            mList = new ListView();
            contextMenu = new ContextMenu();
            SuspendLayout();
            // 
            // mList
            // 
            mList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mList.ContextMenu = contextMenu;
            mList.FullRowSelect = true;
            mList.Location = new Point(8, 8);
            mList.Name = "mList";
            mList.Size = new Size(520, 272);
            mList.TabIndex = 0;
            mList.View = View.Details;
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // BalasControl
            // 
            Controls.Add(mList);
            Name = "BalasControl";
            Size = new Size(536, 288);
            ResumeLayout(false);

        }
        #endregion

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void onRecalculate(object h)
        {
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

        public string fmtBala(double rupas)
        {
            string fmt = string.Format("{0:0.00}", rupas);
            return fmt;
        }
        public void Repopulate()
        {
            BodyName[] grahas = new BodyName[]
            {
                BodyName.Sun, BodyName.Moon, BodyName.Mars, BodyName.Mercury,
                BodyName.Jupiter, BodyName.Venus, BodyName.Saturn
            };
            mList.Clear();
            ShadBalas sb = new ShadBalas(horoscope);

            mList.Columns.Add("Bala", 120, HorizontalAlignment.Left);
            foreach (BodyName b in grahas)
                mList.Columns.Add(b.ToString(), 70, HorizontalAlignment.Left);
            {
                ListViewItem li = new ListViewItem("Sthana");
                foreach (BodyName b in grahas)
                    li.SubItems.Add("-");
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Uccha");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.UcchaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Oja-Yugma");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.OjaYugmaRasyAmsaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Kendra");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.KendraBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Drekkana");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.DrekkanaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Dik");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.DigBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Kaala");
                foreach (BodyName b in grahas)
                    li.SubItems.Add("-");
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Nathonnatha");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.NathonnathaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Paksha");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.PakshaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Tribhaaga");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.TribhaagaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Abda");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.abdaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Masa");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.masaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Vara");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.varaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("-> Hora");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.horaBala(b)));
                mList.Items.Add(li);
            }
            {
                ListViewItem li = new ListViewItem("Naisargika");
                foreach (BodyName b in grahas)
                    li.SubItems.Add(fmtBala(sb.naisargikaBala(b)));
                mList.Items.Add(li);
            }


            ColorAndFontRows(mList);
        }
    }
}

