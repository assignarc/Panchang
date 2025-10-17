using org.transliteral.panchang;
using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{

    public class PanchangChild : Form
    {
        private MainMenu childMenu;
        private MenuItem menuItem1;
        private Horoscope h;
        public string mJhdFileName = null;

        private MainMenu mainMenu1;
        private MenuItem menuDobOptions;
        private MenuItem menuItemFile;
        private MenuItem menuItemFileSaveAs;
        private MenuItem menuItemFileClose;
        private System.ComponentModel.IContainer components;
        private MenuItem menuItem2;
        private MenuItem menuLayoutJhora;
        private MenuItem menuLayout2by2;
        private MenuItem menuLayoutTabbed;
        private MenuItem menuItem3;
        private MenuItem menuCalcOpts;
        private MenuItem menuLayout3by3;
        private MenuItem menuItem4;
        private MenuItem menuItemFilePrint;
        private MenuItem menuItemPrintPreview;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItemChartNotes;
        private MenuItem menuItemFileSave;
        private MenuItem menuStrengthOpts;
        private SplitContainer Contents;
        public PanchangChild(Horoscope _h)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            h = _h;
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
            components = new System.ComponentModel.Container();
            childMenu = new MainMenu(components);
            menuItemFile = new MenuItem();
            menuItemFileSave = new MenuItem();
            menuItemFileSaveAs = new MenuItem();
            menuItemFileClose = new MenuItem();
            menuItem5 = new MenuItem();
            menuItemPrintPreview = new MenuItem();
            menuItemFilePrint = new MenuItem();
            menuItem6 = new MenuItem();
            menuItemChartNotes = new MenuItem();
            menuItem1 = new MenuItem();
            menuDobOptions = new MenuItem();
            menuItem2 = new MenuItem();
            menuLayoutJhora = new MenuItem();
            menuLayoutTabbed = new MenuItem();
            menuLayout2by2 = new MenuItem();
            menuLayout3by3 = new MenuItem();
            menuItem3 = new MenuItem();
            menuItem4 = new MenuItem();
            menuStrengthOpts = new MenuItem();
            menuCalcOpts = new MenuItem();
            mainMenu1 = new MainMenu(components);
            SuspendLayout();
            // 
            // childMenu
            // 
            childMenu.MenuItems.AddRange(new MenuItem[] {
            menuItemFile,
            menuItem1});
            // 
            // menuItemFile
            // 
            menuItemFile.Index = 0;
            menuItemFile.MenuItems.AddRange(new MenuItem[] {
            menuItemFileSave,
            menuItemFileSaveAs,
            menuItemFileClose,
            menuItem5,
            menuItemPrintPreview,
            menuItemFilePrint,
            menuItem6,
            menuItemChartNotes});
            menuItemFile.MergeType = MenuMerge.MergeItems;
            menuItemFile.Text = "&File";
            // 
            // menuItemFileSave
            // 
            menuItemFileSave.Index = 0;
            menuItemFileSave.MergeOrder = 1;
            menuItemFileSave.Shortcut = Shortcut.CtrlS;
            menuItemFileSave.Text = "&Save";
            menuItemFileSave.Click += new EventHandler(menuItemFileSave_Click);
            // 
            // menuItemFileSaveAs
            // 
            menuItemFileSaveAs.Index = 1;
            menuItemFileSaveAs.MergeOrder = 1;
            menuItemFileSaveAs.Shortcut = Shortcut.CtrlA;
            menuItemFileSaveAs.Text = "Save &As";
            menuItemFileSaveAs.Click += new EventHandler(menuItemFileSaveAs_Click);
            // 
            // menuItemFileClose
            // 
            menuItemFileClose.Index = 2;
            menuItemFileClose.MergeOrder = 1;
            menuItemFileClose.Shortcut = Shortcut.CtrlW;
            menuItemFileClose.Text = "&Close";
            menuItemFileClose.Click += new EventHandler(menuItemFileClose_Click);
            // 
            // menuItem5
            // 
            menuItem5.Index = 3;
            menuItem5.MergeOrder = 1;
            menuItem5.Text = "-";
            // 
            // menuItemPrintPreview
            // 
            menuItemPrintPreview.Index = 4;
            menuItemPrintPreview.MergeOrder = 1;
            menuItemPrintPreview.Text = "Print Pre&view";
            menuItemPrintPreview.Click += new EventHandler(menuItemPrintPreview_Click);
            // 
            // menuItemFilePrint
            // 
            menuItemFilePrint.Index = 5;
            menuItemFilePrint.MergeOrder = 1;
            menuItemFilePrint.Shortcut = Shortcut.CtrlP;
            menuItemFilePrint.Text = "&Print";
            menuItemFilePrint.Click += new EventHandler(menuItemFilePrint_Click);
            // 
            // menuItem6
            // 
            menuItem6.Index = 6;
            menuItem6.MergeOrder = 1;
            menuItem6.Text = "-";
            // 
            // menuItemChartNotes
            // 
            menuItemChartNotes.Index = 7;
            menuItemChartNotes.MergeOrder = 1;
            menuItemChartNotes.Text = "Chart Notes";
            menuItemChartNotes.Click += new EventHandler(menuItemChartNotes_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 1;
            menuItem1.MenuItems.AddRange(new MenuItem[] {
            menuDobOptions,
            menuItem2,
            menuItem3,
            menuItem4});
            menuItem1.MergeOrder = 1;
            menuItem1.MergeType = MenuMerge.MergeItems;
            menuItem1.Text = "&Options";
            // 
            // menuDobOptions
            // 
            menuDobOptions.Index = 0;
            menuDobOptions.Shortcut = Shortcut.CtrlD;
            menuDobOptions.Text = "&Birth Data && Events";
            menuDobOptions.Click += new EventHandler(menuDobOptions_Click);
            // 
            // menuItem2
            // 
            menuItem2.Index = 1;
            menuItem2.MenuItems.AddRange(new MenuItem[] {
            menuLayoutJhora,
            menuLayoutTabbed,
            menuLayout2by2,
            menuLayout3by3});
            menuItem2.Text = "Layout";
            // 
            // menuLayoutJhora
            // 
            menuLayoutJhora.Index = 0;
            menuLayoutJhora.Text = "2 x &1";
            menuLayoutJhora.Click += new EventHandler(menuLayoutJhora_Click);
            // 
            // menuLayoutTabbed
            // 
            menuLayoutTabbed.Index = 1;
            menuLayoutTabbed.Text = "2 x 1 (&Tabbed)";
            menuLayoutTabbed.Click += new EventHandler(menuLayoutTabbed_Click);
            // 
            // menuLayout2by2
            // 
            menuLayout2by2.Index = 2;
            menuLayout2by2.Text = "&2 x 2";
            menuLayout2by2.Click += new EventHandler(menuLayout2by2_Click);
            // 
            // menuLayout3by3
            // 
            menuLayout3by3.Index = 3;
            menuLayout3by3.Text = "&3 x 3";
            menuLayout3by3.Click += new EventHandler(menuLayout3by3_Click);
            // 
            // menuItem3
            // 
            menuItem3.Index = 2;
            menuItem3.Text = "-";
            // 
            // menuItem4
            // 
            menuItem4.Index = 3;
            menuItem4.MenuItems.AddRange(new MenuItem[] {
            menuStrengthOpts,
            menuCalcOpts});
            menuItem4.MergeOrder = 2;
            menuItem4.Text = "Advanced Options";
            // 
            // menuStrengthOpts
            // 
            menuStrengthOpts.Index = 0;
            menuStrengthOpts.MergeOrder = 2;
            menuStrengthOpts.Text = "Edit Chart &Strength Options";
            menuStrengthOpts.Click += new EventHandler(menuStrengthOpts_Click);
            // 
            // menuCalcOpts
            // 
            menuCalcOpts.Index = 1;
            menuCalcOpts.MergeOrder = 2;
            menuCalcOpts.Shortcut = Shortcut.CtrlL;
            menuCalcOpts.Text = "Edit Chart &Calculation Options";
            menuCalcOpts.Click += new EventHandler(menuCalcOpts_Click);
            // 
            // PanchangChild
            // 
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(472, 329);
            Menu = childMenu;
            Name = "PanchangChild";
            Text = "PanchagChild";
            WindowState = FormWindowState.Maximized;
            Closing += new System.ComponentModel.CancelEventHandler(PanchangChild_Closing);
            Load += new EventHandler(PanchangChild_Load);
            ResumeLayout(false);

        }
        #endregion

        public Horoscope getHoroscope()
        {
            return h;
        }

        private void PanchangChild_Load(object sender, EventArgs e)
        {
            Contents = null;
            //this.menuLayoutJhora_Click(sender,e);
            //this.menuLayout2by2_Click(sender, e);
            menuLayoutTabbed_Click(sender, e);
            //this.menuLayoutJhora_Click (sender, e);
         
        }

        private void rtOutput_TextChanged(object sender, EventArgs e)
        {

        }


        public void menuShowDobOptions()
        {
            Options f = new Options(h.Info.Clone(), new ApplyOptions(h.UpdateHoraInfo));
            f.ShowDialog();
        }
        private void menuDobOptions_Click(object sender, EventArgs e)
        {
            menuShowDobOptions();
            //object wrapper = new GlobalizedPropertiesWrapper((HoraInfo)h.info.Clone());
        }

        public void saveJhdFile()
        {
            if (mJhdFileName == null || mJhdFileName.Length == 0)
                saveAsJhdFile();
            try
            {
                if (h.Info.FileType == FileType.JagannathaHora)
                    new JagannathaHoraDescriptor(mJhdFileName).ToFile(h.Info);
                else
                    new HoroscopeDescriptor(mJhdFileName).ToFile(h.Info);
            }
            catch (ArgumentNullException)
            {
            }
            catch
            {
                MessageBox.Show(this, "Error Saving File", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void saveAsJhdFile()
        {
            SaveFileDialog ofd = new SaveFileDialog
            {
                AddExtension = true,
                Filter = "Jagannatha Hora Files (*.jhd)|*.jhd|Panchang Files (*.phd)|*.phd",
                FilterIndex = 1
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            if (ofd.FileName.Length == 0)
                return;

            string[] sparts = ofd.FileName.ToLower().Split('.');
            try
            {
                if (sparts[sparts.Length - 1] == "jhd")
                {
                    h.Info.FileType = FileType.JagannathaHora;
                    new JagannathaHoraDescriptor(ofd.FileName).ToFile(h.Info);
                }
                else
                {
                    h.Info.FileType = FileType.PanchangHora;
                    new HoroscopeDescriptor(ofd.FileName).ToFile(h.Info);
                }

                mJhdFileName = ofd.FileName;
            }
            catch (ArgumentNullException)
            {
            }
            //catch 
            //{
            //	MessageBox.Show(this, "Error Saving File", "Error", 
            //		MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void menuItemFileSaveAs_Click(object sender, EventArgs e)
        {
            saveAsJhdFile();

        }

        private void menuItemFileSave_Click(object sender, EventArgs e)
        {
            saveJhdFile();
        }


        private void menuItemFileClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void menuLayoutJhora_Click(object sender, EventArgs e)
        {
            if (Contents != null)
                Controls.Remove(Contents);

            DasaControl dc = new DasaControl(h, new VimsottariDasa(h));
            PanchangControlContainer c_dc = new PanchangControlContainer(dc);

            DivisionalChart div_rasi = new DivisionalChart(h);
            PanchangControlContainer c_div_rasi = new PanchangControlContainer(div_rasi);

            DivisionalChart div_nav = new DivisionalChart(h);
            div_nav.options.Varga = new Division(DivisionType.Navamsa);
            div_nav.SetOptions(div_nav.options);
            PanchangControlContainer c_div_nav = new PanchangControlContainer(div_nav);


            SplitContainer sp_ud = new SplitContainer(c_div_rasi)
            {
                Control2 = c_div_nav,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_dc = new SplitContainer(c_dc);

            SplitContainer sp_lr = new SplitContainer(sp_ud)
            {
                Control2 = sp_dc
            };

            int sz = Screen.PrimaryScreen.WorkingArea.Height / 2 - 50;
            sp_lr.Control1.Width = sz;
            sp_ud.Control1.Height = sz;
            Controls.AddRange(new Control[] { sp_lr });
            Contents = sp_lr;

        }

        private void menuLayoutTabbed_Click(object sender, EventArgs e)
        {
            if (Contents != null)
                Controls.Remove(Contents);

            BaseControl mc = new JhoraMainTab(h);
            //DasaControl dc = new DasaControl(h, new VimsottariDasa(h));
            PanchangControlContainer c_dc = new PanchangControlContainer(mc);

            DivisionalChart div_rasi = new DivisionalChart(h);
            PanchangControlContainer c_div_rasi = new PanchangControlContainer(div_rasi);

            DivisionalChart div_nav = new DivisionalChart(h);
            div_nav.options.Varga = new Division(DivisionType.Navamsa);
            div_nav.SetOptions(div_nav.options);
            PanchangControlContainer c_div_nav = new PanchangControlContainer(div_nav);


            SplitContainer sp_ud = new SplitContainer(c_div_rasi)
            {
                Control2 = c_div_nav,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_dc = new SplitContainer(c_dc);

            SplitContainer sp_lr = new SplitContainer(sp_ud)
            {
                Control2 = sp_dc
            };

            int sz = Screen.PrimaryScreen.WorkingArea.Height / 2 - 50;
            sp_lr.Control1.Width = sz;
            sp_ud.Control1.Height = sz;
            Controls.AddRange(new Control[] { sp_lr });
            Contents = sp_lr;

        }

        private void menuLayout2by2_Click(object sender, EventArgs e)
        {
            if (Contents != null)
                Controls.Remove(Contents);

            DasaControl dc1 = new DasaControl(h, new VimsottariDasa(h));
            PanchangControlContainer c_dc1 = new PanchangControlContainer(dc1);

            BasicCalculationsControl dc2 = new BasicCalculationsControl(h);
            PanchangControlContainer c_dc2 = new PanchangControlContainer(dc2);

            DivisionalChart div_rasi = new DivisionalChart(h);
            PanchangControlContainer c_div_rasi = new PanchangControlContainer(div_rasi);

            DivisionalChart div_nav = new DivisionalChart(h);
            div_nav.options.Varga = new Division(DivisionType.Navamsa);
            div_nav.SetOptions(div_nav.options);
            PanchangControlContainer c_div_nav = new PanchangControlContainer(div_nav);


            SplitContainer sp_ud = new SplitContainer(c_div_rasi)
            {
                Control2 = c_div_nav,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_ud2 = new SplitContainer(c_dc1)
            {
                Control2 = c_dc2,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_dc = new SplitContainer(sp_ud2);

            SplitContainer sp_lr = new SplitContainer(sp_ud)
            {
                Control2 = sp_dc
            };

            int sz = Screen.PrimaryScreen.WorkingArea.Height / 2 - 50;
            sp_lr.Control1.Width = sz;
            sp_ud.Control1.Height = sz;
            sp_ud2.Control1.Height = sz;
            Controls.AddRange(new Control[] { sp_lr });
            Contents = sp_lr;

        }

        private void PanchangChild_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.Close();
            //this.Dispose();
        }

        public object SetCalcOptions(object o)
        {
            HoroscopeOptions ho = (HoroscopeOptions)o;
            h.Options.Copy(ho);
            h.OnChanged();
            return h.Options.Clone();
        }
        public object SetStrengthOptions(object o)
        {
            StrengthOptions so = (StrengthOptions)o;
            h.StrengthOptions.Copy(so);
            h.OnChanged();
            return h.StrengthOptions.Clone();
        }
        private void menuCalcOpts_Click(object sender, EventArgs e)
        {
            Options f = new Options(h.Options, new ApplyOptions(SetCalcOptions));
            f.ShowDialog();
        }


        private void menuStrengthOpts_Click(object sender, EventArgs e)
        {
            if (h.StrengthOptions == null)
                h.StrengthOptions = (StrengthOptions)PanchangAppOptions.Instance.SOptions.Clone();

            Options f = new Options(h.StrengthOptions, new ApplyOptions(SetStrengthOptions));
            f.ShowDialog();
        }

        private void menuLayout3by3_Click(object sender, EventArgs e)
        {
            if (Contents != null)
                Controls.Remove(Contents);

            DivisionalChart d1 = new DivisionalChart(h);
            PanchangControlContainer c_d1 = new PanchangControlContainer(d1);

            DivisionalChart d2 = new DivisionalChart(h);
            d2.options.Varga = new Division(DivisionType.DrekkanaParasara);
            d2.SetOptions(d2.options);
            PanchangControlContainer c_d2 = new PanchangControlContainer(d2);

            DivisionalChart d3 = new DivisionalChart(h);
            d3.options.Varga = new Division(DivisionType.Navamsa);
            d3.SetOptions(d3.options);
            PanchangControlContainer c_d3 = new PanchangControlContainer(d3);

            DivisionalChart d4 = new DivisionalChart(h);
            d4.options.Varga = new Division(DivisionType.Saptamsa);
            d4.SetOptions(d4.options);
            PanchangControlContainer c_d4 = new PanchangControlContainer(d4);

            DivisionalChart d5 = new DivisionalChart(h);
            d5.options.Varga = new Division(DivisionType.Dasamsa);
            d5.SetOptions(d5.options);
            PanchangControlContainer c_d5 = new PanchangControlContainer(d5);

            DivisionalChart d6 = new DivisionalChart(h);
            d6.options.Varga = new Division(DivisionType.Vimsamsa);
            d6.SetOptions(d6.options);
            PanchangControlContainer c_d6 = new PanchangControlContainer(d6);


            SplitContainer sp_ud1 = new SplitContainer(c_d1)
            {
                Control2 = c_d2,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_ud2 = new SplitContainer(c_d3)
            {
                Control2 = c_d4,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };

            SplitContainer sp_ud3 = new SplitContainer(c_d5)
            {
                Control2 = c_d6,
                DrawDock = SplitContainer.DrawStyle.UpDown
            };



            SplitContainer lr2 = new SplitContainer(sp_ud2)
            {
                Control2 = sp_ud3
            };


            SplitContainer lr1 = new SplitContainer(sp_ud1)
            {
                Control2 = lr2
            };


            int h_sz = Screen.PrimaryScreen.WorkingArea.Height / 2 - 30;
            int w_sz = Screen.PrimaryScreen.WorkingArea.Width / 3 - 30;
            int sz = Math.Min(h_sz, w_sz);
            lr1.Control1.Width = sz;
            lr2.Control1.Width = sz;
            sp_ud1.Control1.Height = sz;
            sp_ud2.Control1.Height = sz;
            sp_ud3.Control1.Height = sz;

            Controls.AddRange(new Control[] { lr1 });
            Contents = lr1;

        }

        public object OnCalcOptsChanged(object o)
        {
            h.Options.Copy((HoroscopeOptions)o);
            h.OnChanged();
            return h.Options.Clone();
        }

        private void menuEditCalcOpts_Click(object sender, EventArgs e)
        {
            new Options(h.Options, new ApplyOptions(OnCalcOptsChanged)).ShowDialog();
        }

        public void menuPrint()
        {
            PanchangPrintDocument mdoc = new PanchangPrintDocument(h);
            PrintDialog dlgPrint = new PrintDialog
            {
                Document = mdoc
            };

            if (dlgPrint.ShowDialog() == DialogResult.OK)
                mdoc.Print();
        }
        private void menuItemFilePrint_Click(object sender, EventArgs e)
        {
            menuPrint();
        }

        public void menuPrintPreview()
        {
            PanchangPrintDocument mdoc = new PanchangPrintDocument(h);
            PrintPreviewDialog dlgPreview = new PrintPreviewDialog
            {
                Document = mdoc
            };
            dlgPreview.ShowDialog();
        }
        private void menuItemPrintPreview_Click(object sender, EventArgs e)
        {
            menuPrintPreview();
        }

        private void menuItemChartNotes_Click(object sender, EventArgs e)
        {

            if (null == mJhdFileName || mJhdFileName.Length == 0)
            {
                MessageBox.Show("Please save the chart before editing notes");
                return;
            }

            System.IO.FileInfo fi = new System.IO.FileInfo(mJhdFileName);
            string ext = fi.Extension;

            string sfBase = new string(mJhdFileName.ToCharArray(), 0, mJhdFileName.Length - ext.Length);
            string sfExt = PanchangAppOptions.Instance.ChartNotesFileExtension;
            string sfName = sfBase;

            if (sfExt.Length > 0 && sfExt[0] == '.')
                sfName += sfExt;
            else
                sfName += "." + sfExt;

            try
            {
                if (false == System.IO.File.Exists(sfName))
                    System.IO.File.Create(sfName).Close();
                Process.Start(sfName);

            }
            catch
            {
                MessageBox.Show(string.Format("An error occurred. Unable to open file {0}", sfName));
            }

        }

        private void menuItemEvalYogas_Click(object sender, EventArgs e)
        {
            //this.evaluateYogas();
            //FindYogas.Test(h, new Division(DivisionType.Rasi));
        }
    }

}
