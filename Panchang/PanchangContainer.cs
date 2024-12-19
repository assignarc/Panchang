

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using IWshRuntimeLibrary;

using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{

    public class PanchangContainer : Form
    {
        public GlobalOptions gOpts;

        private int childCount;
        private MainMenu MdiMenu;
        private MenuItem menuItemFile;
        private MenuItem menuItemFileOpen;
        private MenuItem menuItemFileExit;
        private MenuItem menuItemWindow;
        private MenuItem menuItemWindowTile;
        private MenuItem menuItemWindowCascade;
        private MenuItem menuItemHelpAboutMhora;
        private MenuItem menuItemHelpAboutSjc;
        private MenuItem menuItemHelp;
        private StatusBar MdiStatusBar;
        private ToolBar MdiToolBar;
        private ImageList MdiImageList;
        private MenuItem menuItemFileNew;
        private MenuItem menuItemFileNewPrasna;
        private MenuItem mViewPreferences;
        private MenuItem menuItem4;
        private MenuItem mSavePreferences;
        private MenuItem menuItemAdvanced;
        private MenuItem menuItemFindCharts;
        private MenuItem mResetPreferences;
        private MenuItem menuItem1;
        private MenuItem menuItemNewView;
        private ToolBarButton toolbarButtonNew;
        private ToolBarButton toolbarButtonOpen;
        private ToolBarButton toolbarButtonSave;
        private ToolBarButton toolbarButtonHelp;
        private MenuItem mIncreaseFontSize;
        private MenuItem mDecreaseFontSize;
        private MenuItem mEditCalcPrefs;
        private MenuItem menuItem2;
        private MenuItem mEditStrengthPrefs;
        private MenuItem mResetStrengthPreferences;
        private ToolBarButton toolbarButtonPrint;
        private ToolBarButton toolbarButtonPreview;
        private ToolBarButton toolbarButtonDob;
        private ToolBarButton toolbarButtonDisp;
        private IContainer components;

        public PanchangContainer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            childCount = 0;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.Run(new PanchangContainer());
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PanchangContainer));
            MdiMenu = new MainMenu(components);
            menuItemFile = new MenuItem();
            menuItemFileNew = new MenuItem();
            menuItemFileNewPrasna = new MenuItem();
            menuItemFileOpen = new MenuItem();
            menuItemFileExit = new MenuItem();
            mViewPreferences = new MenuItem();
            menuItem4 = new MenuItem();
            mEditStrengthPrefs = new MenuItem();
            mEditCalcPrefs = new MenuItem();
            menuItem2 = new MenuItem();
            mResetPreferences = new MenuItem();
            mResetStrengthPreferences = new MenuItem();
            mSavePreferences = new MenuItem();
            mIncreaseFontSize = new MenuItem();
            mDecreaseFontSize = new MenuItem();
            menuItemWindow = new MenuItem();
            menuItemWindowTile = new MenuItem();
            menuItemWindowCascade = new MenuItem();
            menuItem1 = new MenuItem();
            menuItemNewView = new MenuItem();
            menuItemAdvanced = new MenuItem();
            menuItemFindCharts = new MenuItem();
            menuItemHelp = new MenuItem();
            menuItemHelpAboutMhora = new MenuItem();
            menuItemHelpAboutSjc = new MenuItem();
            MdiStatusBar = new StatusBar();
            MdiToolBar = new ToolBar();
            toolbarButtonNew = new ToolBarButton();
            toolbarButtonOpen = new ToolBarButton();
            toolbarButtonSave = new ToolBarButton();
            toolbarButtonPrint = new ToolBarButton();
            toolbarButtonPreview = new ToolBarButton();
            toolbarButtonDob = new ToolBarButton();
            toolbarButtonDisp = new ToolBarButton();
            toolbarButtonHelp = new ToolBarButton();
            MdiImageList = new ImageList(components);
            SuspendLayout();
            // 
            // MdiMenu
            // 
            MdiMenu.MenuItems.AddRange(new MenuItem[] {
            menuItemFile,
            mViewPreferences,
            menuItemWindow,
            menuItemAdvanced,
            menuItemHelp});
            // 
            // menuItemFile
            // 
            menuItemFile.Index = 0;
            menuItemFile.MenuItems.AddRange(new MenuItem[] {
            menuItemFileNew,
            menuItemFileNewPrasna,
            menuItemFileOpen,
            menuItemFileExit});
            menuItemFile.MergeType = MenuMerge.MergeItems;
            menuItemFile.Text = "&File";
            // 
            // menuItemFileNew
            // 
            menuItemFileNew.Index = 0;
            menuItemFileNew.Shortcut = Shortcut.CtrlN;
            menuItemFileNew.Text = "&New";
            menuItemFileNew.Click += new EventHandler(menuItemFileNew_Click);
            // 
            // menuItemFileNewPrasna
            // 
            menuItemFileNewPrasna.Index = 1;
            menuItemFileNewPrasna.Text = "New &Prasna";
            menuItemFileNewPrasna.Click += new EventHandler(menuItemFileNewPrasna_Click);
            // 
            // menuItemFileOpen
            // 
            menuItemFileOpen.Index = 2;
            menuItemFileOpen.Shortcut = Shortcut.CtrlO;
            menuItemFileOpen.Text = "&Open";
            menuItemFileOpen.Click += new EventHandler(menuItemFileOpen_Click);
            // 
            // menuItemFileExit
            // 
            menuItemFileExit.Index = 3;
            menuItemFileExit.MergeOrder = 2;
            menuItemFileExit.Shortcut = Shortcut.CtrlQ;
            menuItemFileExit.Text = "E&xit";
            menuItemFileExit.Click += new EventHandler(menuItemFileExit_Click);
            // 
            // mViewPreferences
            // 
            mViewPreferences.Index = 1;
            mViewPreferences.MenuItems.AddRange(new MenuItem[] {
            menuItem4,
            mEditStrengthPrefs,
            mEditCalcPrefs,
            menuItem2,
            mResetPreferences,
            mResetStrengthPreferences,
            mSavePreferences,
            mIncreaseFontSize,
            mDecreaseFontSize});
            mViewPreferences.MergeOrder = 1;
            mViewPreferences.MergeType = MenuMerge.MergeItems;
            mViewPreferences.Text = "&Options";
            // 
            // menuItem4
            // 
            menuItem4.Index = 0;
            menuItem4.MergeOrder = 1;
            menuItem4.Shortcut = Shortcut.CtrlP;
            menuItem4.Text = "Edit Global Display Options";
            menuItem4.Click += new EventHandler(menuItem4_Click);
            // 
            // mEditStrengthPrefs
            // 
            mEditStrengthPrefs.Index = 1;
            mEditStrengthPrefs.MergeOrder = 1;
            mEditStrengthPrefs.Text = "Edit Global Strength Options";
            mEditStrengthPrefs.Click += new EventHandler(mEditStrengthPrefs_Click);
            // 
            // mEditCalcPrefs
            // 
            mEditCalcPrefs.Index = 2;
            mEditCalcPrefs.MergeOrder = 1;
            mEditCalcPrefs.Shortcut = Shortcut.CtrlG;
            mEditCalcPrefs.Text = "Edit Global Calculation Options";
            mEditCalcPrefs.Click += new EventHandler(mEditCalcPrefs_Click);
            // 
            // menuItem2
            // 
            menuItem2.Index = 3;
            menuItem2.MergeOrder = 3;
            menuItem2.Text = "-";
            // 
            // mResetPreferences
            // 
            mResetPreferences.Index = 4;
            mResetPreferences.MergeOrder = 3;
            mResetPreferences.Text = "Reset All Options";
            mResetPreferences.Click += new EventHandler(mResetPreferences_Click);
            // 
            // mResetStrengthPreferences
            // 
            mResetStrengthPreferences.Index = 5;
            mResetStrengthPreferences.MergeOrder = 3;
            mResetStrengthPreferences.Text = "Reset Strength Options";
            mResetStrengthPreferences.Click += new EventHandler(mResetStrengthPreferences_Click);
            // 
            // mSavePreferences
            // 
            mSavePreferences.Index = 6;
            mSavePreferences.MergeOrder = 3;
            mSavePreferences.Text = "Save Options";
            mSavePreferences.Click += new EventHandler(mSavePreferences_Click);
            // 
            // mIncreaseFontSize
            // 
            mIncreaseFontSize.Index = 7;
            mIncreaseFontSize.MergeOrder = 3;
            mIncreaseFontSize.Text = "+ Font Size";
            mIncreaseFontSize.Click += new EventHandler(mIncreaseFontSize_Click);
            // 
            // mDecreaseFontSize
            // 
            mDecreaseFontSize.Index = 8;
            mDecreaseFontSize.MergeOrder = 3;
            mDecreaseFontSize.Text = "- Size Size";
            mDecreaseFontSize.Click += new EventHandler(mDecreaseFontSize_Click);
            // 
            // menuItemWindow
            // 
            menuItemWindow.Index = 2;
            menuItemWindow.MdiList = true;
            menuItemWindow.MenuItems.AddRange(new MenuItem[] {
            menuItemWindowTile,
            menuItemWindowCascade,
            menuItem1,
            menuItemNewView});
            menuItemWindow.MergeOrder = 2;
            menuItemWindow.Text = "&Window";
            // 
            // menuItemWindowTile
            // 
            menuItemWindowTile.Index = 0;
            menuItemWindowTile.Text = "&Tile";
            menuItemWindowTile.Click += new EventHandler(menuItemWindowTile_Click);
            // 
            // menuItemWindowCascade
            // 
            menuItemWindowCascade.Index = 1;
            menuItemWindowCascade.Text = "&Cascade";
            menuItemWindowCascade.Click += new EventHandler(menuItemWindowCascade_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 2;
            menuItem1.Text = "-";
            // 
            // menuItemNewView
            // 
            menuItemNewView.Index = 3;
            menuItemNewView.Text = "&New View";
            menuItemNewView.Click += new EventHandler(menuItemNewView_Click);
            // 
            // menuItemAdvanced
            // 
            menuItemAdvanced.Enabled = false;
            menuItemAdvanced.Index = 3;
            menuItemAdvanced.MenuItems.AddRange(new MenuItem[] {
            menuItemFindCharts});
            menuItemAdvanced.MergeOrder = 2;
            menuItemAdvanced.Text = "Advanced";
            // 
            // menuItemFindCharts
            // 
            menuItemFindCharts.Index = 0;
            menuItemFindCharts.Text = "Find Charts";
            menuItemFindCharts.Click += new EventHandler(menuItemFindCharts_Click);
            // 
            // menuItemHelp
            // 
            menuItemHelp.Index = 4;
            menuItemHelp.MenuItems.AddRange(new MenuItem[] {
            menuItemHelpAboutMhora,
            menuItemHelpAboutSjc});
            menuItemHelp.MergeOrder = 2;
            menuItemHelp.Shortcut = Shortcut.F1;
            menuItemHelp.Text = "&Help";
            // 
            // menuItemHelpAboutMhora
            // 
            menuItemHelpAboutMhora.Index = 0;
            menuItemHelpAboutMhora.Text = "&About Panchang";
            menuItemHelpAboutMhora.Click += new EventHandler(menuItemHelpAboutPanchang_Click);
            // 
            // menuItemHelpAboutSjc
            // 
            menuItemHelpAboutSjc.Index = 1;
            menuItemHelpAboutSjc.Text = "About &SJC";
            menuItemHelpAboutSjc.Click += new EventHandler(menuItemHelpAboutSjc_Click);
            // 
            // MdiStatusBar
            // 
            MdiStatusBar.Location = new Point(0, 267);
            MdiStatusBar.Name = "MdiStatusBar";
            MdiStatusBar.Size = new Size(456, 22);
            MdiStatusBar.TabIndex = 1;
            // 
            // MdiToolBar
            // 
            MdiToolBar.Buttons.AddRange(new ToolBarButton[] {
            toolbarButtonNew,
            toolbarButtonOpen,
            toolbarButtonSave,
            toolbarButtonPrint,
            toolbarButtonPreview,
            toolbarButtonDob,
            toolbarButtonDisp,
            toolbarButtonHelp});
            MdiToolBar.DropDownArrows = true;
            MdiToolBar.ImageList = MdiImageList;
            MdiToolBar.Location = new Point(0, 0);
            MdiToolBar.Name = "MdiToolBar";
            MdiToolBar.ShowToolTips = true;
            MdiToolBar.Size = new Size(456, 28);
            MdiToolBar.TabIndex = 2;
            MdiToolBar.ButtonClick += new ToolBarButtonClickEventHandler(MdiToolBar_ButtonClick);
            // 
            // toolbarButtonNew
            // 
            toolbarButtonNew.ImageIndex = 0;
            toolbarButtonNew.Name = "toolbarButtonNew";
            toolbarButtonNew.Tag = "New";
            toolbarButtonNew.ToolTipText = "New Chart";
            // 
            // toolbarButtonOpen
            // 
            toolbarButtonOpen.ImageIndex = 1;
            toolbarButtonOpen.Name = "toolbarButtonOpen";
            toolbarButtonOpen.Tag = "ButtonOpen";
            toolbarButtonOpen.ToolTipText = "Open Chart";
            // 
            // toolbarButtonSave
            // 
            toolbarButtonSave.ImageIndex = 2;
            toolbarButtonSave.Name = "toolbarButtonSave";
            toolbarButtonSave.Tag = "Save";
            toolbarButtonSave.ToolTipText = "Save Chart";
            // 
            // toolbarButtonPrint
            // 
            toolbarButtonPrint.ImageIndex = 4;
            toolbarButtonPrint.Name = "toolbarButtonPrint";
            toolbarButtonPrint.ToolTipText = "Print";
            // 
            // toolbarButtonPreview
            // 
            toolbarButtonPreview.ImageIndex = 5;
            toolbarButtonPreview.Name = "toolbarButtonPreview";
            toolbarButtonPreview.ToolTipText = "Print Preview";
            // 
            // toolbarButtonDob
            // 
            toolbarButtonDob.ImageIndex = 7;
            toolbarButtonDob.Name = "toolbarButtonDob";
            toolbarButtonDob.ToolTipText = "Birth Data, Events";
            // 
            // toolbarButtonDisp
            // 
            toolbarButtonDisp.ImageIndex = 8;
            toolbarButtonDisp.Name = "toolbarButtonDisp";
            toolbarButtonDisp.ToolTipText = "Display Preferences";
            // 
            // toolbarButtonHelp
            // 
            toolbarButtonHelp.ImageIndex = 6;
            toolbarButtonHelp.Name = "toolbarButtonHelp";
            toolbarButtonHelp.Tag = "ButtonHelp";
            toolbarButtonHelp.ToolTipText = "Help";
            // 
            // MdiImageList
            // 
            MdiImageList.ImageStream = (ImageListStreamer)resources.GetObject("MdiImageList.ImageStream");
            MdiImageList.TransparentColor = Color.Transparent;
            MdiImageList.Images.SetKeyName(0, "");
            MdiImageList.Images.SetKeyName(1, "");
            MdiImageList.Images.SetKeyName(2, "");
            MdiImageList.Images.SetKeyName(3, "");
            MdiImageList.Images.SetKeyName(4, "");
            MdiImageList.Images.SetKeyName(5, "");
            MdiImageList.Images.SetKeyName(6, "");
            MdiImageList.Images.SetKeyName(7, "");
            MdiImageList.Images.SetKeyName(8, "");
            // 
            // PanchangContainer
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(456, 289);
            Controls.Add(MdiToolBar);
            Controls.Add(MdiStatusBar);
            IsMdiContainer = true;
            Menu = MdiMenu;
            Name = "PanchangContainer";
            Text = "Panchang";
            WindowState = FormWindowState.Maximized;
            Closing += new CancelEventHandler(PanchangContainer_Closing);
            Load += new EventHandler(PanchangContainer_Load);
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion


        private void PanchangContainer_Load(object sender, EventArgs e)
        {
            gOpts = GlobalOptions.readFromFile();
            GlobalOptions.mainControl = this;
            if (GlobalOptions.Instance.ShowSplashScreen)
            {
                Genghis.Windows.Forms.SplashScreen ss = new Genghis.Windows.Forms.SplashScreen(typeof(Splash), Genghis.Windows.Forms.SplashScreenStyles.TopMost);
                System.Threading.Thread.Sleep(0);
                ss.Close(null, 1000);
            }

            openNewJhdFile();
        }

        private void menuItemNewView_Click(object sender, EventArgs e)
        {
            PanchangChild curr = (PanchangChild)ActiveMdiChild;
            if (null == curr) return;
            PanchangChild child2 = new PanchangChild(curr.getHoroscope())
            {
                Text = curr.Text,
                MdiParent = this,
                Name = curr.Name
            };
            child2.Show();
        }


        private void menuItemWindowTile_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuItemWindowCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void openNewJhdFile()
        {
            DateTime tNow = DateTime.Now;
            Moment mNow = new Moment(tNow.Year, tNow.Month, tNow.Day, tNow.Hour, tNow.Minute, tNow.Second);
            HoraInfo info = new HoraInfo(mNow,
                GlobalOptions.Instance.Latitude,
                GlobalOptions.Instance.Longitude,
                GlobalOptions.Instance.TimeZone);

            childCount++;
            Horoscope h = new Horoscope(info, (HoroscopeOptions)GlobalOptions.Instance.HOptions.Clone());
            //new HoroscopeOptions());
            PanchangChild child = new PanchangChild(h)
            {
                Text = childCount.ToString() + " - Prasna Chart",
                MdiParent = this
            };
            child.Name = child.Text;
            //info.name = child.Text;
            try
            {
                child.Show();
            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void openJhdFileNow()
        {
            DateTime tNow = DateTime.Now;
            Moment mNow = new Moment(tNow.Year, tNow.Month, tNow.Day, tNow.Hour, tNow.Minute, tNow.Second);

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JHD Files (*.jhd)|*.jhd"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;


            HoraInfo info = new JagannathaHoraDescriptor(ofd.FileName).toHoraInfo();
            info.tob = mNow;

            string[] _path_split = ofd.FileName.Split(new char[] { '/', '\\' });
            ArrayList path_split = new ArrayList(_path_split);

            childCount++;
            Horoscope h = new Horoscope(info, (HoroscopeOptions)GlobalOptions.Instance.HOptions.Clone());

            //Horoscope h = new Horoscope (info, new HoroscopeOptions());
            PanchangChild child = new PanchangChild(h)
            {
                Text = childCount.ToString() + " - Prasna Chart",
                MdiParent = this
            };
            child.Name = child.Text;
            child.Show();
        }


        private void openJhdFile()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Panchang Files (*.jhd; *.mhd)|*.jhd;*.mhd"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string[] sparts = ofd.FileName.ToLower().Split(new char[] { '.' });
            HoraInfo info = null;

            if (sparts[sparts.Length - 1] == "jhd")
                info = new JagannathaHoraDescriptor(ofd.FileName).toHoraInfo();
            else
                info = new HoroscopeDescriptor(ofd.FileName).toHoraInfo();

            string[] _path_split = ofd.FileName.Split(new char[] { '/', '\\' });
            ArrayList path_split = new ArrayList(_path_split);

            childCount++;
            Horoscope h = new Horoscope(info, new HoroscopeOptions());
            PanchangChild child = new PanchangChild(h)
            {
                Text = childCount.ToString() + " - " + path_split[path_split.Count - 1],
                MdiParent = this
            };
            child.Name = child.Text;
            child.mJhdFileName = ofd.FileName;

            child.Show();
        }

        public void AddChild(Horoscope h, string name)
        {
            childCount++;
            PanchangChild child = new PanchangChild(h);
            h.OnChanged();
            child.Text = childCount.ToString() + " - " + name;
            child.MdiParent = this;
            child.Name = child.Text;
            child.Show();
        }

        private void menuItemFileOpen_Click(object sender, EventArgs e)
        {
            openJhdFile();
        }

        private void menuItemHelpAboutPanchang_Click(object sender, EventArgs e)
        {
            Form dlg = new About();
            dlg.ShowDialog();
        }

        private void menuItemHelpAboutSjc_Click(object sender, EventArgs e)
        {
            Form dlg = new AboutSjc();
            dlg.ShowDialog();
        }

        private void MdiToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolbarButtonNew)
                openNewJhdFile();
            else if (e.Button == toolbarButtonOpen)
                openJhdFile();
            else if (e.Button == toolbarButtonSave)
            {
                PanchangChild curr = (PanchangChild)ActiveMdiChild;
                if (null == curr) return;
                curr.saveJhdFile();
            }
            else if (e.Button == toolbarButtonHelp)
            {
                Form dlg = new About();
                dlg.ShowDialog();
            }
            else if (e.Button == toolbarButtonPrint)
            {
                if (ActiveMdiChild is PanchangChild mc)
                    mc.menuPrint();
            }
            else if (e.Button == toolbarButtonPreview)
            {
                if (ActiveMdiChild is PanchangChild mc)
                    mc.menuPrintPreview();
            }
            else if (e.Button == toolbarButtonDob)
            {
                PanchangChild mc = ActiveMdiChild as PanchangChild;
                if (mc != null)
                    mc.menuShowDobOptions();
            }
            else if (e.Button == toolbarButtonDisp)
            {
                showMenuGlobalDisplayPrefs();
            }

        }

        private void OnClosing()
        {
            if (GlobalOptions.Instance.SavePrefsOnExit == true)
                GlobalOptions.Instance.saveToFile();
        }


        private void menuItemFileExit_Click(object sender, EventArgs e)
        {
            OnClosing();
            Close();
        }



        private void menuItemFileNew_Click(object sender, EventArgs e)
        {
            openNewJhdFile();

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuItemFileNewPrasna_Click(object sender, EventArgs e)
        {
            openJhdFileNow();
        }


        private void mSavePreferences_Click(object sender, EventArgs e)
        {
            GlobalOptions.Instance.saveToFile();
        }

        private object updateDisplayPreferences(object o)
        {
            GlobalOptions.NotifyDisplayChange();
            Sweph.swe_set_ephe_path(GlobalOptions.Instance.HOptions.EphemerisPath);
            return o;
        }

        private void showMenuGlobalDisplayPrefs()
        {
             Options f = new Options(GlobalOptions.Instance, new ApplyOptions(updateDisplayPreferences), true);
            f.ShowDialog();
        }
        private void menuItem4_Click(object sender, EventArgs e)
        {
            showMenuGlobalDisplayPrefs();
        }


        private bool checkJhd(string fileName)
        {
            HoraInfo info = new JagannathaHoraDescriptor(fileName).toHoraInfo();
            Horoscope h = new Horoscope(info, new HoroscopeOptions());
            if (h.GetPosition(Body.Name.Ketu).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse.Value ==
                h.GetPosition(Body.Name.Lagna).ToDivisionPosition(new Division(DivisionType.Rasi)).ZodiacHouse.Value)
                return true;

            return false;
        }

        private void findCharts(string pathFrom, string pathTo)
        {
            WshShell shell = new WshShellClass();
            DirectoryInfo di = new DirectoryInfo(pathFrom);

            foreach (FileInfo f in di.GetFiles("*.jhd"))
            {
                bool bMatch = false;
                try
                {
                    bMatch = checkJhd(f.FullName);
                }
                catch { }
                if (bMatch == true)
                {
                    string[] _path_split = f.FullName.Split(new char[] { '/', '\\' });
                    ArrayList path_split = new ArrayList(_path_split);
                    Link.Update(pathTo, f.FullName, (string)path_split[path_split.Count - 1], true);
                    //Console.WriteLine(f.FullName);
                }
            }

            foreach (DirectoryInfo d in di.GetDirectories())
                findCharts(d.FullName, pathTo);
        }

        private void menuItemFindCharts_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fFrom = new FolderBrowserDialog
            {
                Description = "Folder containing charts to search"
            };
            fFrom.ShowDialog();
            string pathFrom = fFrom.SelectedPath;

            FolderBrowserDialog fTo = new FolderBrowserDialog
            {
                Description = "Folder where shortcuts should be created"
            };
            fTo.ShowDialog();
            string pathTo = fTo.SelectedPath;

            findCharts(pathFrom, pathTo);

        }

        private void mResetPreferences_Click(object sender, EventArgs e)
        {
            GlobalOptions mh = new GlobalOptions();
            GlobalOptions.Instance = mh;
            GlobalOptions.NotifyDisplayChange();
            GlobalOptions.NotifyCalculationChange();
        }
        private void mResetStrengthPreferences_Click(object sender, EventArgs e)
        {
            GlobalOptions.Instance.SOptions = new StrengthOptions();
            GlobalOptions.NotifyCalculationChange();
        }
        private void menuItemSplash_Click(object sender, EventArgs e)
        {
            Genghis.Windows.Forms.SplashScreen ss = new Genghis.Windows.Forms.SplashScreen(typeof(Splash), Genghis.Windows.Forms.SplashScreenStyles.None);
            System.Threading.Thread.Sleep(0);
            ss.Close(null, 10000);

        }

        private void mIncreaseFontSize_Click(object sender, EventArgs e)
        {
            GlobalOptions.Instance.increaseFontSize();
            GlobalOptions.NotifyDisplayChange();
        }

        private void mDecreaseFontSize_Click(object sender, EventArgs e)
        {
            GlobalOptions.Instance.decreaseFontSize();
            GlobalOptions.NotifyDisplayChange();
        }

        public object updateCalcPreferences(object o)
        {
            Sweph.swe_set_ephe_path(GlobalOptions.Instance.HOptions.EphemerisPath);
            GlobalOptions.NotifyCalculationChange();
            return o;
        }
        private void mEditCalcPrefs_Click(object sender, EventArgs e)
        {
            Options f = new Options(GlobalOptions.Instance.HOptions, new ApplyOptions(updateCalcPreferences), true);
            f.ShowDialog();
        }
        private void mEditStrengthPrefs_Click(object sender, EventArgs e)
        {
            Options f = new Options(GlobalOptions.Instance.SOptions, new ApplyOptions(updateCalcPreferences), true);
            f.ShowDialog();
        }
        private void PanchangContainer_Closing(object sender, CancelEventArgs e)
        {
            OnClosing();
        }






    }
}
