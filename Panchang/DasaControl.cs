

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using org.transliteral.panchang;
using System.Linq;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for DasaControl.
    /// </summary>
    public class DasaControl : PanchangControl //System.Windows.Forms.UserControl
    {
        private ListView dasaItemList;
        private ColumnHeader Dasa;
        private ColumnHeader StartDate;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        private ContextMenu dasaContextMenu;
        private MenuItem mOptions;
        private MenuItem mPreviousCycle;
        private MenuItem mNextCycle;
        private IDasa id;
        private ToDate td;
        private int min_cycle, max_cycle;
        private Label dasaInfo;
        private MenuItem mReset;
        private MenuItem menuItem1;
        private MenuItem mDateOptions;
        private MenuItem menuItem2;
        private MenuItem mEntryChart;
        private MenuItem menuItem3;
        private MenuItem mSolarYears;
        private MenuItem mTithiYears;
        private MenuItem mFixedYears365;
        private MenuItem mFixedYears360;
        private MenuItem mCustomYears;
        private Button bPrevCycle;
        private Button bNextCycle;
        private Button bDasaOptions;
        private Button bDateOptions;
        private Button bRasiStrengths;
        private Button bGrahaStrengths;
        private MenuItem menuItem4;
        private MenuItem mNormalize;
        private MenuItem mEditDasas;
        private MenuItem menuItem5;
        private MenuItem mTribhagi80;
        private MenuItem mTriBhagi40;
        private MenuItem mResetParamAyus;
        private MenuItem mCompressSolar;
        private MenuItem mCompressLunar;
        private MenuItem mCompressTithiPraveshaTithi;
        private MenuItem mCompressTithiPraveshaSolar;
        private MenuItem mCompressedTithiPraveshaFixed;
        private MenuItem mEntryDate;
        private MenuItem mLocateEvent;
        private MenuItem mEntrySunriseChart;
        private MenuItem mShowEvents;
        private MenuItem m3Parts;
        private MenuItem mCompressedYogaPraveshaYoga;
        private MenuItem menuItem6;
        private MenuItem mCompressYoga;
        private MenuItem mEntryChartCompressed;
        private Dasa.Options mDasaOptions;

        public Dasa.Options DasaOptions
        {
            get { return mDasaOptions; }
        }

        public object DasaSpecificOptions
        {
            get { return id.Options; }
            set { id.SetOptions(value); }
        }

        private void SetDescriptionLabel()
        {
            dasaInfo.Text = id.Description();

            dasaInfo.Text += " (";

            if (mDasaOptions.Compression > 0)
                dasaInfo.Text += mDasaOptions.Compression.ToString();


            dasaInfo.Text = string.Format("{0} {1:0.00} {2}",
                dasaInfo.Text,
                mDasaOptions.YearLength,
                mDasaOptions.YearType
                );

            dasaInfo.Text += " )";

            return;

        }
        public void ResetDisplayOptions(object o)
        {
            dasaItemList.BackColor = PanchangAppOptions.Instance.DasaBackgroundColor;
            dasaItemList.Font = PanchangAppOptions.Instance.GeneralFont;
            foreach (ListViewItem li in dasaItemList.Items)
            {
                DasaItem di = (DasaItem)li;
                li.BackColor = PanchangAppOptions.Instance.DasaBackgroundColor;
                li.Font = PanchangAppOptions.Instance.GeneralFont;
                foreach (ListViewItem.ListViewSubItem si in li.SubItems)
                    si.BackColor = PanchangAppOptions.Instance.DasaBackgroundColor;
                di.EventDesc = "";
                if (li.SubItems.Count >= 2)
                {
                    li.SubItems[0].ForeColor = PanchangAppOptions.Instance.DasaPeriodColor;
                    li.SubItems[1].ForeColor = PanchangAppOptions.Instance.DasaDateColor;
                    li.SubItems[1].Font = PanchangAppOptions.Instance.FixedWidthFont;
                }
            }
            dasaItemList.HoverSelection = PanchangAppOptions.Instance.DasaHoverSelect;
            LocateChartEvents();
        }
        public void Reset()
        {
            id.RecalculateOptions();
            SetDescriptionLabel();
            dasaItemList.Items.Clear();
            SetDasaYearType();
            min_cycle = max_cycle = 0;
            double compress = mDasaOptions.Compression == 0.0 ? 0.0 : mDasaOptions.Compression / id.ParamAyus();

            Sweph.ObtainLock(h);
            ArrayList a = id.Dasa(0);
            foreach (DasaEntry de in a)
            {
                DasaItem di = new DasaItem(de);
                di.populateListViewItemMembers(td, id);
                dasaItemList.Items.Add(di);
            }
            Sweph.ReleaseLock(h);
            LocateChartEvents();
        }

        public void OnRecalculate(object o)
        {
            Reset();

        }
        public void OnDasaChanged(object o)
        {
            //Reset();
        }


        public DasaControl(Horoscope _h, IDasa _id) : base()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            h = _h;
            id = _id;
            mDasaOptions = new Dasa.Options();

            if (h.Info.defaultYearCompression != 0)
            {
                mDasaOptions.Compression = h.Info.defaultYearCompression;
                mDasaOptions.YearLength = h.Info.defaultYearLength;
                mDasaOptions.YearType = h.Info.defaultYearType;
            }


            SetDasaYearType();
            //td = new ToDate (h.baseUT, mDasaOptions.YearLength, 0.0, h);
            mShowEvents.Checked = PanchangAppOptions.Instance.DasaShowEvents;
            ResetDisplayOptions(PanchangAppOptions.Instance);

            Dasa d = (Dasa)id;
            d.RecalculateEvent += new Recalculate(recalculateEntries);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(ResetDisplayOptions);
            h.Changed += new EvtChanged(OnRecalculate);
            SetDescriptionLabel();
            d.Changed += new EvtChanged(OnDasaChanged);
            if (dasaItemList.Items.Count >= 1)
                dasaItemList.Items[0].Selected = true;

            VScroll = true;
            Reset();

            //this.LocateChartEvents();
        }


        public bool LinkToHoroscope
        {
            set
            {
                if (value == true)
                {
                    h.Changed += new EvtChanged(OnRecalculate);
                    ((Dasa)id).Changed += new EvtChanged(OnDasaChanged);
                }
                else
                {
                    h.Changed -= new EvtChanged(OnRecalculate);
                    ((Dasa)id).Changed += new EvtChanged(OnDasaChanged);
                }
            }
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
            dasaItemList = new ListView();
            Dasa = new ColumnHeader();
            StartDate = new ColumnHeader();
            dasaContextMenu = new ContextMenu();
            mEntryChart = new MenuItem();
            mEntrySunriseChart = new MenuItem();
            mEntryDate = new MenuItem();
            mLocateEvent = new MenuItem();
            mReset = new MenuItem();
            m3Parts = new MenuItem();
            mShowEvents = new MenuItem();
            mOptions = new MenuItem();
            mDateOptions = new MenuItem();
            mPreviousCycle = new MenuItem();
            mNextCycle = new MenuItem();
            menuItem3 = new MenuItem();
            mSolarYears = new MenuItem();
            mTithiYears = new MenuItem();
            mFixedYears360 = new MenuItem();
            mFixedYears365 = new MenuItem();
            mCustomYears = new MenuItem();
            menuItem5 = new MenuItem();
            mTribhagi80 = new MenuItem();
            mTriBhagi40 = new MenuItem();
            mResetParamAyus = new MenuItem();
            menuItem6 = new MenuItem();
            mCompressSolar = new MenuItem();
            mCompressLunar = new MenuItem();
            mCompressYoga = new MenuItem();
            mCompressTithiPraveshaTithi = new MenuItem();
            mCompressTithiPraveshaSolar = new MenuItem();
            mCompressedTithiPraveshaFixed = new MenuItem();
            mCompressedYogaPraveshaYoga = new MenuItem();
            menuItem4 = new MenuItem();
            mEditDasas = new MenuItem();
            mNormalize = new MenuItem();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            dasaInfo = new Label();
            bPrevCycle = new Button();
            bNextCycle = new Button();
            bDasaOptions = new Button();
            bDateOptions = new Button();
            bRasiStrengths = new Button();
            bGrahaStrengths = new Button();
            mEntryChartCompressed = new MenuItem();
            SuspendLayout();
            // 
            // dasaItemList
            // 
            dasaItemList.AllowColumnReorder = true;
            dasaItemList.AllowDrop = true;
            dasaItemList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            dasaItemList.BackColor = Color.Lavender;
            dasaItemList.Columns.AddRange(new ColumnHeader[] {
                                                                                           Dasa,
                                                                                           StartDate});
            dasaItemList.ContextMenu = dasaContextMenu;
            dasaItemList.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dasaItemList.ForeColor = Color.Black;
            dasaItemList.FullRowSelect = true;
            dasaItemList.HideSelection = false;
            dasaItemList.HoverSelection = true;
            dasaItemList.Location = new Point(8, 40);
            dasaItemList.MultiSelect = false;
            dasaItemList.Name = "dasaItemList";
            dasaItemList.Size = new Size(424, 264);
            dasaItemList.TabIndex = 0;
            dasaItemList.View = View.Details;
            dasaItemList.MouseDown += new MouseEventHandler(dasaItemList_MouseDown);
            dasaItemList.Click += new EventHandler(dasaItemList_Click);
            dasaItemList.MouseUp += new MouseEventHandler(dasaItemList_MouseUp);
            dasaItemList.DragDrop += new DragEventHandler(dasaItemList_DragDrop);
            dasaItemList.MouseEnter += new EventHandler(dasaItemList_MouseEnter);
            dasaItemList.DragEnter += new DragEventHandler(dasaItemList_DragEnter);
            dasaItemList.MouseMove += new MouseEventHandler(dasaItemList_MouseMove);
            dasaItemList.SelectedIndexChanged += new EventHandler(dasaItemList_SelectedIndexChanged);
            // 
            // Dasa
            // 
            Dasa.Text = "Dasa";
            Dasa.Width = 150;
            // 
            // StartDate
            // 
            StartDate.Text = "Dates";
            StartDate.Width = 500;
            // 
            // dasaContextMenu
            // 
            dasaContextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                            mEntryChart,
                                                                                            mEntryChartCompressed,
                                                                                            mEntrySunriseChart,
                                                                                            mEntryDate,
                                                                                            mLocateEvent,
                                                                                            mReset,
                                                                                            m3Parts,
                                                                                            mShowEvents,
                                                                                            mOptions,
                                                                                            mDateOptions,
                                                                                            mPreviousCycle,
                                                                                            mNextCycle,
                                                                                            menuItem3,
                                                                                            menuItem4,
                                                                                            menuItem1,
                                                                                            menuItem2});
            dasaContextMenu.Popup += new EventHandler(dasaContextMenu_Popup);
            // 
            // mEntryChart
            // 
            mEntryChart.Index = 0;
            mEntryChart.Text = "&Entry Chart";
            mEntryChart.Click += new EventHandler(mEntryChart_Click);
            // 
            // mEntrySunriseChart
            // 
            mEntrySunriseChart.Index = 2;
            mEntrySunriseChart.Text = "Entry &Sunrise Chart";
            mEntrySunriseChart.Click += new EventHandler(mEntrySunriseChart_Click);
            // 
            // mEntryDate
            // 
            mEntryDate.Index = 3;
            mEntryDate.Text = "Copy Entry Date";
            mEntryDate.Click += new EventHandler(mEntryDate_Click);
            // 
            // mLocateEvent
            // 
            mLocateEvent.Index = 4;
            mLocateEvent.Text = "Locate An Event";
            mLocateEvent.Click += new EventHandler(mLocateEvent_Click);
            // 
            // mReset
            // 
            mReset.Index = 5;
            mReset.Text = "&Reset";
            mReset.Click += new EventHandler(mReset_Click);
            // 
            // m3Parts
            // 
            m3Parts.Index = 6;
            m3Parts.Text = "3 Parts";
            m3Parts.Click += new EventHandler(m3Parts_Click);
            // 
            // mShowEvents
            // 
            mShowEvents.Checked = true;
            mShowEvents.Index = 7;
            mShowEvents.Text = "Show Events";
            mShowEvents.Click += new EventHandler(mShowEvents_Click);
            // 
            // mOptions
            // 
            mOptions.Index = 8;
            mOptions.Text = "Dasa &Options";
            mOptions.Visible = false;
            mOptions.Click += new EventHandler(mOptions_Click);
            // 
            // mDateOptions
            // 
            mDateOptions.Index = 9;
            mDateOptions.Text = "&Date Options";
            mDateOptions.Visible = false;
            mDateOptions.Click += new EventHandler(mDateOptions_Click);
            // 
            // mPreviousCycle
            // 
            mPreviousCycle.Index = 10;
            mPreviousCycle.Text = "&Previous Cycle";
            mPreviousCycle.Visible = false;
            mPreviousCycle.Click += new EventHandler(mPreviousCycle_Click);
            // 
            // mNextCycle
            // 
            mNextCycle.Index = 11;
            mNextCycle.Text = "&Next Cycle";
            mNextCycle.Visible = false;
            mNextCycle.Click += new EventHandler(mNextCycle_Click);
            // 
            // menuItem3
            // 
            menuItem3.Index = 12;
            menuItem3.MenuItems.AddRange(new MenuItem[] {
                                                                                      mSolarYears,
                                                                                      mTithiYears,
                                                                                      mFixedYears360,
                                                                                      mFixedYears365,
                                                                                      mCustomYears,
                                                                                      menuItem5,
                                                                                      mTribhagi80,
                                                                                      mTriBhagi40,
                                                                                      mResetParamAyus,
                                                                                      menuItem6,
                                                                                      mCompressSolar,
                                                                                      mCompressLunar,
                                                                                      mCompressYoga,
                                                                                      mCompressTithiPraveshaTithi,
                                                                                      mCompressTithiPraveshaSolar,
                                                                                      mCompressedTithiPraveshaFixed,
                                                                                      mCompressedYogaPraveshaYoga});
            menuItem3.Text = "Year Options";
            // 
            // mSolarYears
            // 
            mSolarYears.Index = 0;
            mSolarYears.Text = "&Solar Years (360 degrees)";
            mSolarYears.Click += new EventHandler(mSolarYears_Click);
            // 
            // mTithiYears
            // 
            mTithiYears.Index = 1;
            mTithiYears.Text = "&Tithi Years (360 tithis)";
            mTithiYears.Click += new EventHandler(mTithiYears_Click);
            // 
            // mFixedYears360
            // 
            mFixedYears360.Index = 2;
            mFixedYears360.Text = "Savana Years (360 days)";
            mFixedYears360.Click += new EventHandler(mFixedYears360_Click);
            // 
            // mFixedYears365
            // 
            mFixedYears365.Index = 3;
            mFixedYears365.Text = "~ Solar Year (365.2425 days)";
            mFixedYears365.Click += new EventHandler(mFixedYears365_Click);
            // 
            // mCustomYears
            // 
            mCustomYears.Index = 4;
            mCustomYears.Text = "&Custom Years";
            mCustomYears.Click += new EventHandler(mCustomYears_Click);
            // 
            // menuItem5
            // 
            menuItem5.Index = 5;
            menuItem5.Text = "-";
            // 
            // mTribhagi80
            // 
            mTribhagi80.Index = 6;
            mTribhagi80.Text = "Tribhagi ParamAyus (80 Years)";
            mTribhagi80.Click += new EventHandler(mTribhagi80_Click);
            // 
            // mTriBhagi40
            // 
            mTriBhagi40.Index = 7;
            mTriBhagi40.Text = "Tribhagi ParamAyus (40 Years)";
            mTriBhagi40.Click += new EventHandler(mTriBhagi40_Click);
            // 
            // mResetParamAyus
            // 
            mResetParamAyus.Index = 8;
            mResetParamAyus.Text = "Regular ParamAyus";
            mResetParamAyus.Click += new EventHandler(mResetParamAyus_Click);
            // 
            // menuItem6
            // 
            menuItem6.Index = 9;
            menuItem6.Text = "-";
            // 
            // mCompressSolar
            // 
            mCompressSolar.Index = 10;
            mCompressSolar.Text = "Compress to Solar Year";
            mCompressSolar.Click += new EventHandler(mCompressSolar_Click);
            // 
            // mCompressLunar
            // 
            mCompressLunar.Index = 11;
            mCompressLunar.Text = "Compress to Tithi Year";
            mCompressLunar.Click += new EventHandler(mCompressLunar_Click);
            // 
            // mCompressYoga
            // 
            mCompressYoga.Index = 12;
            mCompressYoga.Text = "Compress to Yoga Year";
            mCompressYoga.Click += new EventHandler(mCompressYoga_Click);
            // 
            // mCompressTithiPraveshaTithi
            // 
            mCompressTithiPraveshaTithi.Index = 13;
            mCompressTithiPraveshaTithi.Text = "Compress to Tithi Pravesha Year (Tithi)";
            mCompressTithiPraveshaTithi.Click += new EventHandler(mCompressTithiPraveshaTithi_Click);
            // 
            // mCompressTithiPraveshaSolar
            // 
            mCompressTithiPraveshaSolar.Index = 14;
            mCompressTithiPraveshaSolar.Text = "Compress to Tithi Pravesha Year (Solar)";
            mCompressTithiPraveshaSolar.Click += new EventHandler(mCompressTithiPraveshaSolar_Click);
            // 
            // mCompressedTithiPraveshaFixed
            // 
            mCompressedTithiPraveshaFixed.Index = 15;
            mCompressedTithiPraveshaFixed.Text = "Compress to Tithi Pravesha Year (Fixed)";
            mCompressedTithiPraveshaFixed.Click += new EventHandler(mCompressedTithiPraveshaFixed_Click);
            // 
            // mCompressedYogaPraveshaYoga
            // 
            mCompressedYogaPraveshaYoga.Index = 16;
            mCompressedYogaPraveshaYoga.Text = "Compress to Yoga Pravesha Year (Yoga)";
            mCompressedYogaPraveshaYoga.Click += new EventHandler(mCompressedYogaPraveshaYoga_Click);
            // 
            // menuItem4
            // 
            menuItem4.Index = 13;
            menuItem4.MenuItems.AddRange(new MenuItem[] {
                                                                                      mEditDasas,
                                                                                      mNormalize});
            menuItem4.Text = "Advanced";
            // 
            // mEditDasas
            // 
            mEditDasas.Index = 0;
            mEditDasas.Text = "Edit Dasas";
            mEditDasas.Click += new EventHandler(mEditDasas_Click);
            // 
            // mNormalize
            // 
            mNormalize.Index = 1;
            mNormalize.Text = "Normalize Dates";
            mNormalize.Click += new EventHandler(menuItem5_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 14;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 15;
            menuItem2.Text = "-";
            // 
            // dasaInfo
            // 
            dasaInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            dasaInfo.Location = new Point(184, 8);
            dasaInfo.Name = "dasaInfo";
            dasaInfo.Size = new Size(232, 23);
            dasaInfo.TabIndex = 1;
            dasaInfo.TextAlign = ContentAlignment.MiddleLeft;
            dasaInfo.Click += new EventHandler(dasaInfo_Click);
            // 
            // bPrevCycle
            // 
            bPrevCycle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bPrevCycle.Location = new Point(8, 8);
            bPrevCycle.Name = "bPrevCycle";
            bPrevCycle.Size = new Size(24, 23);
            bPrevCycle.TabIndex = 2;
            bPrevCycle.Text = "<";
            bPrevCycle.Click += new EventHandler(bPrevCycle_Click);
            // 
            // bNextCycle
            // 
            bNextCycle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bNextCycle.Location = new Point(32, 8);
            bNextCycle.Name = "bNextCycle";
            bNextCycle.Size = new Size(24, 23);
            bNextCycle.TabIndex = 3;
            bNextCycle.Text = ">";
            bNextCycle.Click += new EventHandler(bNextCycle_Click);
            // 
            // bDasaOptions
            // 
            bDasaOptions.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bDasaOptions.Location = new Point(64, 8);
            bDasaOptions.Name = "bDasaOptions";
            bDasaOptions.Size = new Size(40, 23);
            bDasaOptions.TabIndex = 4;
            bDasaOptions.Text = "Opts";
            bDasaOptions.Click += new EventHandler(bDasaOptions_Click);
            // 
            // bDateOptions
            // 
            bDateOptions.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bDateOptions.Location = new Point(104, 8);
            bDateOptions.Name = "bDateOptions";
            bDateOptions.Size = new Size(24, 23);
            bDateOptions.TabIndex = 5;
            bDateOptions.Text = "Yr";
            bDateOptions.Click += new EventHandler(bDateOptions_Click);
            // 
            // bRasiStrengths
            // 
            bRasiStrengths.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bRasiStrengths.Location = new Point(128, 8);
            bRasiStrengths.Name = "bRasiStrengths";
            bRasiStrengths.Size = new Size(24, 23);
            bRasiStrengths.TabIndex = 6;
            bRasiStrengths.Text = "R";
            bRasiStrengths.Click += new EventHandler(bRasiStrengths_Click);
            // 
            // bGrahaStrengths
            // 
            bGrahaStrengths.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bGrahaStrengths.Location = new Point(152, 8);
            bGrahaStrengths.Name = "bGrahaStrengths";
            bGrahaStrengths.Size = new Size(24, 23);
            bGrahaStrengths.TabIndex = 7;
            bGrahaStrengths.Text = "G";
            bGrahaStrengths.Click += new EventHandler(bGrahaStrengths_Click);
            // 
            // mEntryChartCompressed
            // 
            mEntryChartCompressed.Index = 1;
            mEntryChartCompressed.Text = "Entry Chart (&Compressed)";
            mEntryChartCompressed.Click += new EventHandler(mEntryChartCompressed_Click);
            // 
            // DasaControl
            // 
            AccessibleRole = AccessibleRole.None;
            Controls.Add(bGrahaStrengths);
            Controls.Add(bRasiStrengths);
            Controls.Add(bDateOptions);
            Controls.Add(bDasaOptions);
            Controls.Add(bNextCycle);
            Controls.Add(bPrevCycle);
            Controls.Add(dasaInfo);
            Controls.Add(dasaItemList);
            Name = "DasaControl";
            Size = new Size(440, 312);
            Load += new EventHandler(DasaControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void mEntryChart_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            Horoscope h2 = (Horoscope)h.Clone();
            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];

            Sweph.ObtainLock(h);
            Moment m = td.AddYears(di.entry.startUT);
            h2.Info.tob = m;
            Sweph.ReleaseLock(h);

            PanchangChild mchild = (PanchangChild)ParentForm;
            PanchangContainer mcont = (PanchangContainer)ParentForm.ParentForm;

            mcont.AddChild(h2, mchild.Name + ": Dasa Entry - (" + di.entry.shortDesc + ") " + id.Description());
        }


        private void mEntryChartCompressed_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            Horoscope h2 = (Horoscope)h.Clone();
            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];

            Sweph.ObtainLock(h);
            Moment m = td.AddYears(di.entry.startUT);
            Moment mEnd = td.AddYears(di.entry.startUT + di.entry.dasaLength);

            double ut_diff = mEnd.ToUniversalTime() - m.ToUniversalTime();
            h2.Info.tob = m;
            Sweph.ReleaseLock(h);


            h2.Info.defaultYearCompression = 1;
            h2.Info.defaultYearLength = ut_diff;
            h2.Info.defaultYearType = DateType.FixedYear;

            PanchangChild mchild = (PanchangChild)ParentForm;
            PanchangContainer mcont = (PanchangContainer)ParentForm.ParentForm;

            mcont.AddChild(h2, mchild.Name + ": Dasa Entry - (" + di.entry.shortDesc + ") " + id.Description());

        }



        private void mEntrySunriseChart_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            Horoscope h2 = (Horoscope)h.Clone();
            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];

            Sweph.ObtainLock(h);
            Moment m = td.AddYears(di.entry.startUT);
            Sweph.ReleaseLock(h);
            h2.Info.tob = m;

            h2.OnChanged();

            // if done once, get something usually 2+ minutes off. 
            // don't know why this is.
            double offsetSunrise = h2.HoursAfterSunrise() / 24.0;
            m = new Moment(h2.baseUT - offsetSunrise, h2);
            h2.Info.tob = m;
            h2.OnChanged();

            // so do it a second time, getting sunrise + 1 second.
            offsetSunrise = h2.HoursAfterSunrise() / 24.0;
            m = new Moment(h2.baseUT - offsetSunrise + 1.0 / (24.0 * 60.0 * 60.0), h2);
            h2.Info.tob = m;
            h2.OnChanged();

            PanchangChild mchild = (PanchangChild)ParentForm;
            PanchangContainer mcont = (PanchangContainer)ParentForm.ParentForm;

            mcont.AddChild(h2, mchild.Name + ": Dasa Entry Sunrise - (" + di.entry.shortDesc + ") " + id.Description());

        }



        private void mEntryDate_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];
            Sweph.ObtainLock(h);
            Moment m = td.AddYears(di.entry.startUT);
            Sweph.ReleaseLock(h);
            Clipboard.SetDataObject(m.ToString(), true);
        }

        private void SplitDasa()
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            SplitDasa((DasaItem)dasaItemList.SelectedItems[0]);
        }

        private void SplitDasa(DasaItem di)
        {

            //Trace.Assert(dasaItemList.SelectedItems.Count >= 1, "dasaItemList::doubleclick");
            int index = di.Index + 1;

            bool action_inserting = true;


            dasaItemList.BeginUpdate();
            while (index < dasaItemList.Items.Count)
            {
                DasaItem tdi = (DasaItem)dasaItemList.Items[index];
                if (tdi.entry.level > di.entry.level)
                {
                    action_inserting = false;
                    dasaItemList.Items.Remove(tdi);
                }
                else
                    break;
            }

            if (action_inserting == false)
            {
                dasaItemList.EndUpdate();
                return;
            }

            ArrayList a = id.AntarDasa(di.entry);
            double compress = mDasaOptions.Compression == 0.0 ? 0.0 : mDasaOptions.Compression / id.ParamAyus();

            Sweph.ObtainLock(h);
            foreach (DasaEntry de in a)
            {
                DasaItem pdi = new DasaItem(de);
                pdi.populateListViewItemMembers(td, id);
                dasaItemList.Items.Insert(index, pdi);
                index++;
            }
            Sweph.ReleaseLock(h);
            dasaItemList.EndUpdate();
            //this.dasaItemList.Items[index-1].Selected = true;
        }
        protected override void copyToClipboard()
        {
            int iMaxDescLength = 0;
            for (int i = 0; i < dasaItemList.Items.Count; i++)
                iMaxDescLength = Math.Max(dasaItemList.Items[i].Text.Length, iMaxDescLength);
            iMaxDescLength += 2;

            string s = dasaInfo.Text + "\r\n\r\n";
            for (int i = 0; i < dasaItemList.Items.Count; i++)
            {
                ListViewItem li = dasaItemList.Items[i];
                DasaItem di = (DasaItem)li;
                int levelSpace = di.entry.level * 2;
                s += li.Text.PadRight(iMaxDescLength + levelSpace, ' ');

                for (int j = 1; j < li.SubItems.Count; j++)
                {
                    s += "(" + li.SubItems[j].Text + ") ";
                }
                s += "\r\n";
            }
            Clipboard.SetDataObject(s);
        }
        private void recalculateEntries()
        {
            SetDescriptionLabel();
            dasaItemList.Items.Clear();
            ArrayList a = new ArrayList();
            for (int i = min_cycle; i <= max_cycle; i++)
            {
                ArrayList b = id.Dasa(i);
                a.AddRange(b);
            }
            Sweph.ObtainLock(h);
            foreach (DasaEntry de in a)
            {
                DasaItem di = new DasaItem(de);
                di.populateListViewItemMembers(td, id);
                dasaItemList.Items.Add(di);
            }
            Sweph.ReleaseLock(h);
            LocateChartEvents();
        }

        private void mOptions_Click(object sender, EventArgs e)
        {
            //object wrapper = new GlobalizedPropertiesWrapper(id.GetOptions());
            Options f = new Options(id.Options, new ApplyOptions(id.SetOptions));
            f.pGrid.ExpandAllGridItems();
            f.ShowDialog();
        }

        private void mPreviousCycle_Click(object sender, EventArgs e)
        {
            min_cycle--;
            ArrayList a = id.Dasa(min_cycle);
            int i = 0;
            Sweph.ObtainLock(h);
            foreach (DasaEntry de in a)
            {
                DasaItem di = new DasaItem(de);
                di.populateListViewItemMembers(td, id);
                dasaItemList.Items.Insert(i, di);
                i++;
            }
            Sweph.ReleaseLock(h);
        }

        private void mNextCycle_Click(object sender, EventArgs e)
        {
            max_cycle++;
            ArrayList a = id.Dasa(max_cycle);
            Sweph.ObtainLock(h);
            foreach (DasaEntry de in a)
            {
                DasaItem di = new DasaItem(de);
                di.populateListViewItemMembers(td, id);
                dasaItemList.Items.Add(di);
            }
            Sweph.ReleaseLock(h);
        }

        private void mReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void mVimsottari_Click(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(BaseUserOptionsViewType.DasaVimsottari);
        }

        private void mAshtottari_Click(object sender, EventArgs e)
        {
            ((PanchangControlContainer)Parent).h = h;
            ((PanchangControlContainer)Parent).SetView(BaseUserOptionsViewType.DasaAshtottari);
        }


        private void DasaControl_Load(object sender, EventArgs e)
        {
            AddViewsToContextMenu(dasaContextMenu);
        }

        private void SetDasaYearType()
        {
            double compress = mDasaOptions.Compression == 0.0 ? 0.0 : mDasaOptions.Compression / id.ParamAyus();
            if (mDasaOptions.YearType == DateType.FixedYear)// ||
                                                            //mDasaOptions.YearType == DateType.TithiYear)
                td = new ToDate(h.baseUT, mDasaOptions.YearLength, compress, h);
            else
                td = new ToDate(h.baseUT, mDasaOptions.YearType, mDasaOptions.YearLength, compress, h);

            td.SetOffset(mDasaOptions.OffsetDays + mDasaOptions.OffsetHours / 24.0 + mDasaOptions.OffsetMinutes / (24.0 * 60.0));
        }
        private object SetDasaOptions(object o)
        {
            Dasa.Options opts = (Dasa.Options)o;
            mDasaOptions.Copy(opts);
            SetDasaYearType();
            Reset();
            return mDasaOptions.Clone();

        }

        private void mDateOptions_Click(object sender, EventArgs e)
        {
            Form f = new Options(mDasaOptions.Clone(), new ApplyOptions(SetDasaOptions));
            f.ShowDialog();
        }

        private void dasaItemList_MouseMove(object sender, MouseEventArgs e)
        {
            DasaItem di = (DasaItem)dasaItemList.GetItemAt(e.X, e.Y);
            if (di == null)
                return;
            tooltip_event.SetToolTip(dasaItemList, di.EventDesc);
            tooltip_event.InitialDelay = 0;

            if (PanchangAppOptions.Instance.DasaMoveSelect)
                di.Selected = true;

            //Console.WriteLine ("MouseMove: {0} {1}", e.Y, li != null ? li.Index : -1);
            //if (li != null)
            //	li.Selected = true;
        }

        private void dasaItemList_MouseDown(object sender, MouseEventArgs e)
        {
            //this.dasaItemList_MouseMove(sender, e);
        }

        private void dasaItemList_MouseEnter(object sender, EventArgs e)
        {
            //Console.WriteLine ("Mouse Enter");
            //this.dasaItemList.Focus();
            //this.dasaItemList.Items[0].Selected = true;
        }

        private static ToolTip tooltip_event = new ToolTip();
        private void dasaItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.dasaItemList.SelectedItems.Count <= 0)
            //	return;

            //DasaItem di = (DasaItem)this.dasaItemList.SelectedItems[0];

            //tooltip_event.SetToolTip(this.dasaItemList, di.EventDesc);
            //tooltip_event.InitialDelay = 0;
        }


        private void dasaItemList_Click(object sender, EventArgs e)
        {
        }

        private void dasaItemList_MouseUp(object sender, MouseEventArgs e)
        {
            dasaItemList_MouseMove(sender, e);
            if (e.Button == MouseButtons.Left)
                SplitDasa();

            ListViewItem li = dasaItemList.GetItemAt(e.X, e.Y);
            //Console.WriteLine ("MouseMove Click: {0} {1}", e.Y, li != null ? li.Index : -1);
            if (li != null)
                li.Selected = true;

        }

        private void mFixedYears365_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.YearType == DateType.FixedYear &&
                mDasaOptions.YearLength == 365.2425)
                return;
            mDasaOptions.YearType = DateType.FixedYear;
            mDasaOptions.YearLength = 365.2425;
            SetDasaYearType();
            Reset();
        }

        private void mTithiYears_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.YearType == DateType.TithiYear &&
                mDasaOptions.YearLength == 360.0)
                return;
            mDasaOptions.YearType = DateType.TithiYear;
            mDasaOptions.YearLength = 360.0;
            SetDasaYearType();
            Reset();
        }

        private void mSolarYears_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.YearType == DateType.SolarYear &&
                mDasaOptions.YearLength == 360.0)
                return;
            mDasaOptions.YearType = DateType.SolarYear;
            mDasaOptions.YearLength = 360.0;
            SetDasaYearType();
            Reset();
        }

        private void mFixedYears360_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.YearType == DateType.FixedYear &&
                mDasaOptions.YearLength == 360.0)
                return;
            mDasaOptions.YearType = DateType.FixedYear;
            mDasaOptions.YearLength = 360.0;
            SetDasaYearType();
            Reset();
        }

        private void mTribhagi80_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 80)
                return;
            mDasaOptions.Compression = 80;
            SetDasaYearType();
            Reset();
        }

        private void mTriBhagi40_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 40)
                return;
            mDasaOptions.Compression = 40;
            SetDasaYearType();
            Reset();
        }

        private void mResetParamAyus_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 0)
                return;
            mDasaOptions.Compression = 0;
            SetDasaYearType();
            Reset();
        }
        private void mCompressSolar_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 1 &&
                mDasaOptions.YearType == DateType.SolarYear &&
                mDasaOptions.YearLength == 360)
                return;
            mDasaOptions.Compression = 1;
            mDasaOptions.YearLength = 360.0;
            mDasaOptions.YearType = DateType.SolarYear;
            SetDasaYearType();
            Reset();
        }

        private void mCompressLunar_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 1 &&
                mDasaOptions.YearType == DateType.TithiYear &&
                mDasaOptions.YearLength == 360)
                return;
            mDasaOptions.Compression = 1;
            mDasaOptions.YearLength = 360.0;
            mDasaOptions.YearType = DateType.TithiYear;
            SetDasaYearType();
            Reset();
        }

        private void mCompressYoga_Click(object sender, EventArgs e)
        {
            if (mDasaOptions.Compression == 1 &&
                mDasaOptions.YearType == DateType.YogaYear &&
                mDasaOptions.YearLength == 324)
                return;
            mDasaOptions.Compression = 1;
            mDasaOptions.YearLength = 324;
            mDasaOptions.YearType = DateType.YogaYear;
            SetDasaYearType();
            Reset();
        }

        private void mCompressTithiPraveshaTithi_Click(object sender, EventArgs e)
        {
            mDasaOptions.YearType = DateType.TithiYear;
            ToDate td_pravesh = new ToDate(h.baseUT, DateType.TithiPraveshYear, 360.0, 0, h);
            ToDate td_tithi = new ToDate(h.baseUT, DateType.TithiYear, 360.0, 0, h);
            Sweph.ObtainLock(h);
            if (td_tithi.AddYears(1).ToUniversalTime() + 15.0 < td_pravesh.AddYears(1).ToUniversalTime())
                mDasaOptions.YearLength = 390;
            else
                mDasaOptions.YearLength = 360;
            Sweph.ReleaseLock(h);
            mDasaOptions.Compression = 1;
            SetDasaYearType();
            Reset();
        }

        public void compressToYogaPraveshaYearYoga()
        {
            mDasaOptions.YearType = DateType.YogaYear;
            ToDate td_pravesh = new ToDate(h.baseUT, DateType.YogaPraveshYear, 360.0, 0, h);
            ToDate td_yoga = new ToDate(h.baseUT, DateType.YogaYear, 324.0, 0, h);
            Sweph.ObtainLock(h);
            double date_to_surpass = td_pravesh.AddYears(1).ToUniversalTime() - 5;
            double date_current = td_yoga.AddYears(0).ToUniversalTime();
            double months = 0;
            while (date_current < date_to_surpass)
            {
                Logger.Info($"{new Moment(date_current, h)} > {new Moment(date_to_surpass, h)}");

                months++;
                date_current = td_yoga.AddYears(months / 12.0).ToUniversalTime();
            }
            Sweph.ReleaseLock(h);
            mDasaOptions.Compression = 1;
            mDasaOptions.YearLength = (int)months * 27;
            SetDasaYearType();
            Reset();

        }
        private void mCompressedYogaPraveshaYoga_Click(object sender, EventArgs e)
        {
            compressToYogaPraveshaYearYoga();
        }

        private void mCompressTithiPraveshaSolar_Click(object sender, EventArgs e)
        {
            ToDate td_pravesh = new ToDate(h.baseUT, DateType.TithiPraveshYear, 360.0, 0, h);
            Sweph.ObtainLock(h);
            double ut_start = td_pravesh.AddYears(0).ToUniversalTime();
            double ut_end = td_pravesh.AddYears(1).ToUniversalTime();
            BodyPosition sp_start = Basics.CalculateSingleBodyPosition(
                ut_start, Sweph.BodyNameToSweph(BodyName.Sun), BodyName.Sun, BodyType.Name.Graha, h);
            BodyPosition sp_end = Basics.CalculateSingleBodyPosition(
                ut_end, Sweph.BodyNameToSweph(BodyName.Sun), BodyName.Sun, BodyType.Name.Graha, h);
            Longitude lDiff = sp_end.Longitude.Subtract(sp_start.Longitude);
            double diff = lDiff.Value;
            if (diff < 120.0) diff += 360.0;

            DasaOptions.YearType = DateType.SolarYear;
            DasaOptions.YearLength = diff;
            Sweph.ReleaseLock(h);
            DasaOptions.Compression = 1;
            Reset();
        }

        private void mCompressedTithiPraveshaFixed_Click(object sender, EventArgs e)
        {
            ToDate td_pravesh = new ToDate(h.baseUT, DateType.TithiPraveshYear, 360.0, 0, h);
            Sweph.ObtainLock(h);
            DasaOptions.YearType = DateType.FixedYear;
            DasaOptions.YearLength = td_pravesh.AddYears(1).ToUniversalTime() -
                td_pravesh.AddYears(0).ToUniversalTime();
            Sweph.ReleaseLock(h);
            Reset();
        }


        private void mCustomYears_Click(object sender, EventArgs e)
        {
            mDateOptions_Click(sender, e);
        }

        private void bPrevCycle_Click(object sender, EventArgs e)
        {
            mPreviousCycle_Click(sender, e);
        }

        private void bNextCycle_Click(object sender, EventArgs e)
        {
            mNextCycle_Click(sender, e);
        }

        private void bDasaOptions_Click(object sender, EventArgs e)
        {
            mOptions_Click(sender, e);
        }

        private void bDateOptions_Click(object sender, EventArgs e)
        {
            mDateOptions_Click(sender, e);
        }

        private void bRasiStrengths_Click(object sender, EventArgs e)
        {
            new RasiStrengthsControl(h).ShowDialog();
            //this.mRasiStrengths_Click(sender, e);		
        }

        public object SetDasasOptions(object a)
        {
            dasaItemList.Items.Clear();
            DasaEntry[] al = ((DasaEntriesWrapper)a).Entries;
            Sweph.ObtainLock(h);
            for (int i = 0; i < al.Length; i++)
            {
                DasaItem di = new DasaItem(al[i]);
                di.populateListViewItemMembers(td, id);
                dasaItemList.Items.Add(di);
            }
            Sweph.ReleaseLock(h);
            LocateChartEvents();
            return a;
        }

        class DasaEntriesWrapper
        {
            DasaEntry[] al;
            public DasaEntriesWrapper(DasaEntry[] _al)
            {
                al = _al;
            }
            public DasaEntry[] Entries
            {
                get { return al; }
                set { al = value; }
            }
        }
        private void bEditItems_Click(object sender, EventArgs e)
        {

        }

        private void dasaContextMenu_Popup(object sender, EventArgs e)
        {

        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            DasaEntry[] al = new DasaEntry[dasaItemList.Items.Count];
            DasaItem[] am = new DasaItem[dasaItemList.Items.Count];
            double start = 0.0;
            if (dasaItemList.Items.Count >= 1)
            {
                start = ((DasaItem)dasaItemList.Items[0]).entry.startUT;
            }
            for (int i = 0; i < dasaItemList.Items.Count; i++)
            {
                DasaItem di = (DasaItem)dasaItemList.Items[i];
                al[i] = di.entry;
                if (al[i].level == 1)
                {
                    al[i].startUT = start;
                    start += al[i].dasaLength;
                }
                am[i] = new DasaItem(al[i]);
            }
            dasaItemList.Items.Clear();
            Sweph.ObtainLock(h);
            for (int i = 0; i < am.Length; i++)
            {
                am[i].populateListViewItemMembers(td, id);
                dasaItemList.Items.Add(am[i]);
            }
            Sweph.ReleaseLock(h);
        }

        private void mDasaDown_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            Horoscope h2 = (Horoscope)h.Clone();
            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];

            Moment m = td.AddYears(di.entry.startUT);
            h2.Info.tob = m;

            PanchangChild mchild = (PanchangChild)ParentForm;
            PanchangContainer mcont = (PanchangContainer)ParentForm.ParentForm;

            mcont.AddChild(h2, mchild.Name + ": Dasa Entry Chart - (((" + di.entry.shortDesc + "))) " + id.Description());
        }

        private void mEditDasas_Click(object sender, EventArgs e)
        {
            DasaEntry[] al = new DasaEntry[dasaItemList.Items.Count];
            for (int i = 0; i < dasaItemList.Items.Count; i++)
            {
                DasaItem di = (DasaItem)dasaItemList.Items[i];
                al[i] = di.entry;
            }
            DasaEntriesWrapper dw = new DasaEntriesWrapper(al);
            Options f = new Options(dw, new ApplyOptions(SetDasasOptions), true);
            f.ShowDialog();
        }

        private void bGrahaStrengths_Click(object sender, EventArgs e)
        {
            GrahaStrengthsControl gc = new GrahaStrengthsControl(h);
            gc.ShowDialog();
        }


        class EventUserOptions : ICloneable
        {
            Moment mEventDate;
            int mLevels;
            public Moment EventDate
            {
                get { return mEventDate; }
                set { mEventDate = value; }
            }
            public int Depth
            {
                get { return mLevels; }
                set { mLevels = value; }
            }
            public EventUserOptions(Moment _m)
            {
                mEventDate = (Moment)_m.Clone();
                mLevels = 2;
            }
            public object Clone()
            {
                EventUserOptions euo = new EventUserOptions(mEventDate)
                {
                    mLevels = mLevels
                };
                return euo;
            }
        }


        private void ExpandEvent(Moment m, int levels, string eventDesc)
        {
            double ut_m = m.ToUniversalTime(h);
            for (int i = 0; i < dasaItemList.Items.Count; i++)
            {
                DasaItem di = (DasaItem)dasaItemList.Items[i];

                Sweph.ObtainLock(h);
                Moment m_start = td.AddYears(di.entry.startUT);
                Moment m_end = td.AddYears(di.entry.startUT + di.entry.dasaLength);
                Sweph.ReleaseLock(h);


                double ut_start = m_start.ToUniversalTime(h);
                double ut_end = m_end.ToUniversalTime(h);


                if (ut_m >= ut_start && ut_m < ut_end)
                {
                    Logger.Info($"Found: Looking for {m} between {m_start} and {m_end}");

                    if (levels > di.entry.level)
                    {
                        if (i == dasaItemList.Items.Count - 1)
                        {
                            dasaItemList.SelectedItems.Clear();
                            dasaItemList.Items[i].Selected = true;
                            SplitDasa((DasaItem)dasaItemList.Items[i]);
                        }
                        if (i < dasaItemList.Items.Count - 1)
                        {
                            DasaItem di_next = (DasaItem)dasaItemList.Items[i + 1];
                            if (di_next.entry.level == di.entry.level)
                            {
                                dasaItemList.SelectedItems.Clear();
                                dasaItemList.Items[i].Selected = true;
                                SplitDasa((DasaItem)dasaItemList.Items[i]);
                            }
                        }
                    }
                    else if (levels == di.entry.level)
                    {
                        foreach (ListViewItem.ListViewSubItem si in di.SubItems)
                            si.BackColor = PanchangAppOptions.Instance.DasaHighlightColor;

                        di.EventDesc += eventDesc;
                    }
                }
            }
        }

        public object LocateEvent(object _euo)
        {
            EventUserOptions euo = (EventUserOptions)_euo;
            mEventOptionsCache = euo;
            ExpandEvent(euo.EventDate, euo.Depth, "Event: " + euo.EventDate.ToString());
            return _euo;
        }

        public void LocateChartEvents()
        {
            if (mShowEvents.Checked == false)
                return;

            foreach (UserEvent ue in h.Info.Events.Cast<UserEvent>())
            {
                if (ue.WorkWithEvent == true)
                    ExpandEvent(ue.EventTime, PanchangAppOptions.Instance.DasaEventsLevel, ue.ToString());
            }
        }

        EventUserOptions mEventOptionsCache = null;
        private void mLocateEvent_Click(object sender, EventArgs e)
        {
            if (mEventOptionsCache == null)
            {
                DateTime dtNow = DateTime.Now;
                mEventOptionsCache = new EventUserOptions(
                    new Moment(dtNow.Year, dtNow.Month, dtNow.Day,
                    dtNow.Hour + dtNow.Minute / 60.0 + dtNow.Second / 3600.0));
            }

            new Options(mEventOptionsCache, new ApplyOptions(LocateEvent)).ShowDialog();
        }


        private void dasaItemList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void dasaItemList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivisionalChart)))
            {
                Division div = Division.CopyFromClipboard();
                if (null == div) return;
                id.DivisionChanged(div);
            }
        }

        private void dasaInfo_Click(object sender, EventArgs e)
        {

        }

        private void mShowEvents_Click(object sender, EventArgs e)
        {
            mShowEvents.Checked = !mShowEvents.Checked;
        }

        private void m3Parts_Click(object sender, EventArgs e)
        {
            if (dasaItemList.SelectedItems.Count == 0)
                return;

            DasaItem di = (DasaItem)dasaItemList.SelectedItems[0];
            DasaEntry de = di.entry;

            Dasa3Parts form = new Dasa3Parts(h, de, td);
            form.Show();
        }

    }



    /// <summary>
    /// Specifies a DasaItem which can be used by any of the Dasa Systems.
    /// Hence it includes _both_ a graha and zodiacHouse in order to be used
    /// by systems which Graha dasas and Rasi bhukti's and vice-versa. The logic
    /// should be checked carefully
    /// </summary>

    public class DasaItem : ListViewItem
    {
        public DasaEntry entry;
        public string EventDesc;
        public void populateListViewItemMembers(ToDate td, IDasa id)
        {
            UseItemStyleForSubItems = false;

            //this.Text = entry.shortDesc;
            Font = PanchangAppOptions.Instance.GeneralFont;
            ForeColor = PanchangAppOptions.Instance.DasaPeriodColor;
            Moment m = td.AddYears(entry.startUT);
            Moment m2 = td.AddYears(entry.startUT + entry.dasaLength);
            string sDateRange = m.ToString() + " - " + m2.ToString();
            for (int i = 1; i < entry.level; i++)
                sDateRange = " " + sDateRange;
            SubItems.Add(sDateRange);
            Text = entry.shortDesc + id.EntryDescription(entry, m, m2);
            SubItems[1].Font = PanchangAppOptions.Instance.FixedWidthFont;
            SubItems[1].ForeColor = PanchangAppOptions.Instance.DasaDateColor;
        }
        private void Construct(DasaEntry _entry)
        {
            entry = _entry;
        }
        public DasaItem(DasaEntry _entry)
        {
            entry = _entry;
        }
        public DasaItem(BodyName _graha, double _startUT, double _dasaLength, int _level, string _shortDesc)
        {
            Construct(new DasaEntry(_graha, _startUT, _dasaLength, _level, _shortDesc));
        }
        public DasaItem(ZodiacHouseName _zodiacHouse, double _startUT, double _dasaLength, int _level, string _shortDesc)
        {
            Construct(new DasaEntry(_zodiacHouse, _startUT, _dasaLength, _level, _shortDesc));
        }
    }


}
