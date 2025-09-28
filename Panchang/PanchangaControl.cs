using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace org.transliteral.panchang.app
{
    public delegate void DelegateComputeFinished();
    public class PanchangaControl : PanchangControl
    {

        public class UserOptions : ICloneable
        {
            int mNumDays;
            bool bCalculateLagnaCusps = false;
            bool bCalculateTithiCusps = true;
            bool bCalculateKaranaCusps = true;
            bool bCalculateNakshatraCusps = true;
            bool bCalculateHoraCusps = true;
            bool bCalculateSMYogaCusps = true;
            bool bCalculateKalaCusps = true;
            bool bShowSpecialKalas = true;
            bool bShowSunriset = true;
            bool bLargeHours = false;
            bool bShowUpdates = true;
            bool bOneEntryPerLine = false;
            public UserOptions()
            {
                NumDays = 3;
            }
            [Description("Number of days to compute information for")]
            public int NumDays
            {
                get { return mNumDays; }
                set { mNumDays = value; }
            }
            [Description("Include sunriset / sunset in the output?")]
            public bool ShowSunriset
            {
                get { return bShowSunriset; }
                set { bShowSunriset = value; }
            }
            [Description("Calculate and include Lagna cusp changes?")]
            public bool CalcLagnaCusps
            {
                get { return bCalculateLagnaCusps; }
                set { bCalculateLagnaCusps = value; }
            }
            [Description("Calculate and include Tithi cusp information?")]
            public bool CalcTithiCusps
            {
                get { return bCalculateTithiCusps; }
                set { bCalculateTithiCusps = value; }
            }
            [Description("Calculate and include Karana cusp information?")]
            public bool CalcKaranaCusps
            {
                get { return bCalculateKaranaCusps; }
                set { bCalculateKaranaCusps = value; }
            }
            [Description("Calculate and include Sun-Moon yoga cusp information?")]
            public bool CalcSMYogaCusps
            {
                get { return bCalculateSMYogaCusps; }
                set { bCalculateSMYogaCusps = value; }
            }
            [Description("Calculate and include Nakshatra cusp information?")]
            public bool CalcNakCusps
            {
                get { return bCalculateNakshatraCusps; }
                set { bCalculateNakshatraCusps = value; }
            }
            [Description("Calculate and include Hora cusp information?")]
            public bool CalcHoraCusps
            {
                get { return bCalculateHoraCusps; }
                set { bCalculateHoraCusps = value; }
            }
            [Description("Calculate and include special Kalas?")]
            public bool CalcSpecialKalas
            {
                get { return bShowSpecialKalas; }
                set { bShowSpecialKalas = value; }
            }
            [Description("Calculate and include Kala cusp information?")]
            public bool CalcKalaCusps
            {
                get { return bCalculateKalaCusps; }
                set { bCalculateKalaCusps = value; }
            }
            [Description("Display 02:00 after midnight as 26:00 or *02:00?")]
            public bool LargeHours
            {
                get { return bLargeHours; }
                set { bLargeHours = value; }
            }
            [Description("Display incremental updates?")]
            public bool ShowUpdates
            {
                get { return bShowUpdates; }
                set { bShowUpdates = value; }
            }
            [Description("Display only one entry / line?")]
            public bool OneEntryPerLine
            {
                get { return bOneEntryPerLine; }
                set { bOneEntryPerLine = value; }
            }
            public object Clone()
            {
                UserOptions uo = new UserOptions
                {
                    NumDays = NumDays,
                    CalcLagnaCusps = CalcLagnaCusps,
                    CalcNakCusps = CalcNakCusps,
                    CalcTithiCusps = CalcTithiCusps,
                    CalcKaranaCusps = CalcKaranaCusps,
                    CalcHoraCusps = CalcHoraCusps,
                    CalcKalaCusps = CalcKalaCusps,
                    CalcSpecialKalas = CalcSpecialKalas,
                    LargeHours = LargeHours,
                    ShowUpdates = ShowUpdates,
                    ShowSunriset = ShowSunriset,
                    OneEntryPerLine = OneEntryPerLine,
                    CalcSMYogaCusps = CalcSMYogaCusps
                };
                return uo;
            }
            public object CopyFrom(object _uo)
            {
                UserOptions uo = (UserOptions)_uo;
                NumDays = uo.NumDays;
                CalcLagnaCusps = uo.CalcLagnaCusps;
                CalcNakCusps = uo.CalcNakCusps;
                CalcTithiCusps = uo.CalcTithiCusps;
                CalcKaranaCusps = uo.CalcKaranaCusps;
                CalcHoraCusps = uo.CalcHoraCusps;
                CalcKalaCusps = uo.CalcKalaCusps;
                CalcSpecialKalas = uo.CalcSpecialKalas;
                LargeHours = uo.LargeHours;
                ShowUpdates = uo.ShowUpdates;
                ShowSunriset = uo.ShowSunriset;
                CalcSMYogaCusps = uo.CalcSMYogaCusps;
                OneEntryPerLine = uo.OneEntryPerLine;
                return Clone();
            }


        }



        private ListView mList;
        private Button bOpts;
        private IContainer components = null;
        private Button bCompute;
        private UserOptions opts = null;
        private ContextMenu contextMenu;
        private MenuItem menuItemPrintPanchanga;
        private MenuItem menuItemFilePrintPreview;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        public DelegateComputeFinished m_DelegateComputeFinished;


        public PanchangaControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            h = _h;
            h.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            opts = new UserOptions();
            AddViewsToContextMenu(contextMenu);
            mutexProgress = new Mutex(false);
            OnRedisplay(PanchangAppOptions.Instance.TableBackgroundColor);
            bCompute_Click(null, null);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
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
            bOpts = new Button();
            bCompute = new Button();
            contextMenu = new ContextMenu();
            menuItemPrintPanchanga = new MenuItem();
            menuItemFilePrintPreview = new MenuItem();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            SuspendLayout();
            // 
            // mList
            // 
            mList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mList.FullRowSelect = true;
            mList.Location = new Point(8, 40);
            mList.Name = "mList";
            mList.Size = new Size(512, 272);
            mList.TabIndex = 0;
            mList.View = View.Details;
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // bOpts
            // 
            bOpts.Location = new Point(16, 8);
            bOpts.Name = "bOpts";
            bOpts.TabIndex = 1;
            bOpts.Text = "Options";
            bOpts.Click += new EventHandler(bOpts_Click);
            // 
            // bCompute
            // 
            bCompute.Location = new Point(104, 8);
            bCompute.Name = "bCompute";
            bCompute.TabIndex = 2;
            bCompute.Text = "Compute";
            bCompute.Click += new EventHandler(bCompute_Click);
            // 
            // contextMenu
            // 
            contextMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                        menuItemPrintPanchanga,
                                                                                        menuItemFilePrintPreview,
                                                                                        menuItem1,
                                                                                        menuItem2});
            // 
            // menuItemPrintPanchanga
            // 
            menuItemPrintPanchanga.Index = 0;
            menuItemPrintPanchanga.Text = "Print Panchanga";
            menuItemPrintPanchanga.Click += new EventHandler(menuItemPrintPanchanga_Click);
            // 
            // menuItemFilePrintPreview
            // 
            menuItemFilePrintPreview.Index = 1;
            menuItemFilePrintPreview.Text = "Print Preview Panchanga";
            menuItemFilePrintPreview.Click += new EventHandler(menuItemFilePrintPreview_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 2;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 3;
            menuItem2.Text = "-";
            // 
            // PanchangaControl
            // 
            ContextMenu = contextMenu;
            Controls.Add(bCompute);
            Controls.Add(bOpts);
            Controls.Add(mList);
            Name = "PanchangaControl";
            Size = new Size(528, 320);
            Load += new EventHandler(PanchangaControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void PanchangaControl_Load(object sender, EventArgs e)
        {

        }

        bool bResultsInvalid = true;

        public void OnRedisplay(object o)
        {
            mList.ForeColor = PanchangAppOptions.Instance.TableForegroundColor;
            mList.BackColor = PanchangAppOptions.Instance.TableBackgroundColor;
            mList.Font = PanchangAppOptions.Instance.GeneralFont;
        }
        public void OnRecalculate(object _h)
        {
            if (bResultsInvalid == true)
                return;

            Horoscope h = (Horoscope)_h;

            ListViewItem li = new ListViewItem
            {
                Text = "Results may be out of date. Click the Compute Button to recalculate the panchanga"
            };
            mList.Items.Insert(0, li);
            mList.Items.Insert(1, "");
            bResultsInvalid = true;


        }
        public object SetOptions(object o)
        {
            return opts.CopyFrom(o);
        }

        private void bOpts_Click(object sender, EventArgs e)
        {
            //this.mutexProgress.WaitOne();
            //if (this.fProgress != null)
            //{
            //	MessageBox.Show("Cannot show options when calculation is in progress");
            //	this.mutexProgress.Close();
            //	return;
            //}
            new Options(opts, new ApplyOptions(SetOptions)).ShowDialog();
            //this.mutexProgress.Close();
        }

        private void bCompute_Click(object sender, EventArgs e)
        {

            m_DelegateComputeFinished = new DelegateComputeFinished(ComputeFinished);
            Thread t = new Thread(new ThreadStart(ComputeStart));
            t.Start();
        }

        //ProgressDialog fProgress = null;
        Mutex mutexProgress = null;

        private void ComputeStart()
        {
            //this.mutexProgress.WaitOne();
            //if (fProgress != null)
            //{
            //	this.mutexProgress.Close();
            //	return;
            //}
            //fProgress = new ProgressDialog(opts.NumDays);
            //fProgress.setProgress(opts.NumDays/2);
            Logger.Info("Starting threaded computation");
            //fProgress.ShowDialog();
            //this.mutexProgress.Close();
            bCompute.Enabled = false;
            bOpts.Enabled = false;
            ContextMenu = null;
            ComputeEntries();
            Invoke(m_DelegateComputeFinished);

        }
        private void ComputeFinished()
        {
            Logger.Info("Finished threaded execution");
            bResultsInvalid = false;
            bCompute.Enabled = true;
            bOpts.Enabled = true;
            ContextMenu = contextMenu;
            //this.m_DelegateComputeFinished -= new DelegateComputeFinished(this.ComputeFinished);
            //this.mutexProgress.WaitOne();
            //fProgress.Close();
            //fProgress = null;
            //this.mutexProgress.Close();
        }


        private void ComputeEntries()
        {
            mList.Clear();
            mList.Columns.Add("", -2, HorizontalAlignment.Left);

            if (false == opts.ShowUpdates)
                mList.BeginUpdate();

            double ut_start = Math.Floor(h.baseUT);
            double[] geopos = new double[]
            { h.Info.lon.toDouble(), h.Info.lat.toDouble(), h.Info.alt };

            globals = new GlobalMoments();
            locals = new ArrayList();

            for (int i = 0; i < opts.NumDays; i++)
            {
                ComputeEntry(ut_start, geopos);
                ut_start += 1;
                mList.Columns[0].Width = -2;
            }
            mList.Columns[0].Width = -2;

            if (false == opts.ShowUpdates)
                mList.EndUpdate();
        }

        private Moment utToMoment(double found_ut)
        {
            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += h.Info.tz.toDouble() / 24.0;
            Sweph.SWE_ReverseJulianDay(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            return m;
        }
        private string utToString(double ut)
        {
            int year = 0, month = 0, day = 0;
            double time = 0;

            ut += h.Info.tz.toDouble() / 24.0;
            Sweph.SWE_ReverseJulianDay(ut, ref year, ref month, ref day, ref time);
            return timeToString(time);
        }
        private string utTimeToString(double ut_event, double ut_sr, double sunrise)
        {
            Moment m = utToMoment(ut_event);
            HMSInfo hms = new HMSInfo(m.Time);

            if (ut_event >= ut_sr - sunrise / 24.0 + 1.0)
            {
                if (false == opts.LargeHours)
                    return string.Format("*{0:00}:{1:00}", hms.degree, hms.minute);
                else
                    return string.Format("{0:00}:{1:00}", hms.degree + 24, hms.minute);
            }
            return string.Format("{0:00}:{1:00}", hms.degree, hms.minute);
        }
        private string timeToString(double time)
        {
            HMSInfo hms = new HMSInfo(time);
            return string.Format("{0:00}:{1:00}",
                hms.degree, hms.minute, hms.second);
        }

        double sunrise = 0;
        double ut_sr = 0;

        int[] rahu_kalas = new int[] { 7, 1, 6, 4, 5, 3, 2 };
        int[] gulika_kalas = new int[] { 6, 5, 4, 3, 2, 1, 0 };
        int[] yama_kalas = new int[] { 4, 3, 2, 1, 0, 6, 5 };


        ArrayList locals = new ArrayList();
        GlobalMoments globals = new GlobalMoments();

        private void ComputeEntry(double ut, double[] geopos)
        {

            int year = 0, month = 0, day = 0;
            double sunset = 0, hour = 0;
            Sweph.Lock(h);
            h.PopulateSunrisetCacheHelper(ut - 0.5, ref sunrise, ref sunset, ref ut_sr);
            Sweph.Unlock(h);

            Sweph.SWE_ReverseJulianDay(ut_sr, ref year, ref month, ref day, ref hour);
            Moment moment_sr = new Moment(year, month, day, hour);
            Moment moment_ut = new Moment(ut, h);
            HoraInfo infoCurr = new HoraInfo(moment_ut, h.Info.lat, h.Info.lon, h.Info.tz);
            Horoscope hCurr = new Horoscope(infoCurr, h.Options);

            ListViewItem li = null;

            LocalMoments local = new LocalMoments
            {
                Sunrise = hCurr.Sunrise,
                Sunset = sunset,
                SunriseUT = ut_sr
            };
            Sweph.SWE_ReverseJulianDay(ut, ref year, ref month, ref day, ref hour);
            local.WeekDay = (Weekday)Sweph.SWE_DayOfWeek(ut);



            local.KalasUT = hCurr.GetKalaCuspsUt();
            if (opts.CalcSpecialKalas)
            {
                BodyName bStart = Basics.WeekdayRuler(hCurr.Weekday);
                if (hCurr.Options.KalaType == EHoraType.Lmt)
                    bStart = Basics.WeekdayRuler(hCurr.WeekdayLMT);

                local.RahuKalaIndex = rahu_kalas[(int)bStart];
                local.GulikaKalaIndex = gulika_kalas[(int)bStart];
                local.YamaKalaIndex = yama_kalas[(int)bStart];
            }

            if (opts.CalcLagnaCusps)
            {
                li = new ListViewItem();
                Sweph.Lock(h);
                BodyPosition bp_lagna_sr = Basics.CalculateSingleBodyPosition(ut_sr, Sweph.BodyNameToSweph(BodyName.Lagna), BodyName.Lagna, BodyType.Name.Lagna, h);
                DivisionPosition dp_lagna_sr = bp_lagna_sr.ToDivisionPosition(new Division(DivisionType.Rasi));
                local.LagnaZodiacHouse = dp_lagna_sr.ZodiacHouse.Value;

                Longitude bp_lagna_base = new Longitude(bp_lagna_sr.Longitude.ToZodiacHouseBase());
                double ut_transit = ut_sr;
                for (int i = 1; i <= 12; i++)
                {
                    Retrogression r = new Retrogression(h, BodyName.Lagna);
                    ut_transit = r.GetLagnaTransitForward(ut_transit, bp_lagna_base.Add(i * 30.0));

                    MomentInfo pmi = new MomentInfo(
                        ut_transit, (int)bp_lagna_sr.Longitude.ToZodiacHouse().Add(i + 1).Value);
                    local.LagnasUT.Add(pmi);
                }

                Sweph.Unlock(h);
            }

            if (opts.CalcTithiCusps)
            {
                Transit t = new Transit(h);
                Sweph.Lock(h);
                Tithi tithi_start = t.LongitudeOfTithi(ut_sr).ToTithi();
                Tithi tithi_end = t.LongitudeOfTithi(ut_sr + 1.0).ToTithi();

                Tithi tithi_curr = tithi_start.Add(1);
                local.TithiIndexStart = globals.TithisUT.Count - 1;
                local.TithiIndexEnd = globals.TithisUT.Count - 1;

                while (tithi_start.Value != tithi_end.Value &&
                    tithi_curr.Value != tithi_end.Value)
                {
                    tithi_curr = tithi_curr.Add(2);
                    double dLonToFind = ((double)(int)tithi_curr.Value - 1) * (360.0 / 30.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.TithisUT.Add(new MomentInfo(ut_found, (int)tithi_curr.Value));
                    local.TithiIndexEnd++;
                }
                Sweph.Unlock(h);
            }


            if (opts.CalcKaranaCusps)
            {
                Transit t = new Transit(h);
                Sweph.Lock(h);
                Karana karana_start = t.LongitudeOfTithi(ut_sr).ToKarana();
                Karana karana_end = t.LongitudeOfTithi(ut_sr + 1.0).ToKarana();

                Karana karana_curr = karana_start.add(1);
                local.KaranaIndexStart = globals.KaranasUT.Count - 1;
                local.KaranaIndexEnd = globals.KaranasUT.Count - 1;

                while (karana_start.value != karana_end.value &&
                    karana_curr.value != karana_end.value)
                {
                    karana_curr = karana_curr.add(2);
                    double dLonToFind = ((double)(int)karana_curr.value - 1) * (360.0 / 60.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfTithiDir));

                    globals.KaranasUT.Add(new MomentInfo(ut_found, (int)karana_curr.value));
                    local.KaranaIndexEnd++;
                }
                Sweph.Unlock(h);
            }

            if (opts.CalcSMYogaCusps)
            {
                Transit t = new Transit(h);
                Sweph.Lock(h);
                SunMoonYoga sm_start = t.LongitudeOfSunMoonYoga(ut_sr).ToSunMoonYoga();
                SunMoonYoga sm_end = t.LongitudeOfSunMoonYoga(ut_sr + 1.0).ToSunMoonYoga();

                SunMoonYoga sm_curr = sm_start.Add(1);
                local.SunMoonYogaIndexStart = globals.SunMoonYogasUT.Count - 1;
                local.SunMoonYogaIndexEnd = globals.SunMoonYogasUT.Count - 1;

                while (sm_start.Value != sm_end.Value &&
                    sm_curr.Value != sm_end.Value)
                {
                    sm_curr = sm_curr.Add(2);
                    double dLonToFind = ((double)(int)sm_curr.Value - 1) * (360.0 / 27);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.LongitudeOfSunMoonYogaDir));

                    globals.SunMoonYogasUT.Add(new MomentInfo(ut_found, (int)sm_curr.Value));
                    local.SunMoonYogaIndexEnd++;
                }

                Sweph.Unlock(h);
            }


            if (opts.CalcNakCusps)
            {
                bool bDiscard = true;
                Transit t = new Transit(h, BodyName.Moon);
                Sweph.Lock(h);
                Nakshatra nak_start = t.GenericLongitude(ut_sr, ref bDiscard).ToNakshatra();
                Nakshatra nak_end = t.GenericLongitude(ut_sr + 1.0, ref bDiscard).ToNakshatra();

                local.NakshatraIndexStart = globals.NakshatrasUT.Count - 1;
                local.NakshatraIndexEnd = globals.NakshatrasUT.Count - 1;

                Nakshatra nak_curr = nak_start.Add(1);

                while (nak_start.Value != nak_end.Value &&
                    nak_curr.Value != nak_end.Value)
                {
                    nak_curr = nak_curr.Add(2);
                    double dLonToFind = ((int)nak_curr.Value - 1) * (360.0 / 27.0);
                    double ut_found = t.LinearSearchBinary(ut_sr, ut_sr + 1.0, new Longitude(dLonToFind),
                        new ReturnLon(t.GenericLongitude));

                    globals.NakshatrasUT.Add(new MomentInfo(ut_found, (int)nak_curr.Value));
                    Logger.Info($"Found nakshatra {nak_curr.Value}");
                    local.NakshatraIndexEnd++;
                }
                Sweph.Unlock(h);
            }

            if (opts.CalcHoraCusps)
            {
                local.HorasUT = hCurr.GetHoraCuspsUt();
                hCurr.CalculateHora(ut_sr + 1.0 / 24.0, ref local.HoraBase);
            }

            if (opts.CalcKalaCusps)
            {
                hCurr.CalculateKala(ref local.KalaBase);
            }


            locals.Add(local);
            DisplayEntry(local);
        }

        private void DisplayEntry(LocalMoments local)
        {
            string s;
            int day = 0, month = 0, year = 0;
            double time = 0;

            Sweph.SWE_ReverseJulianDay(local.SunriseUT, ref year, ref month, ref day, ref time);
            Moment m = new Moment(year, month, day, time);
            mList.Items.Add(string.Format("{0}, {1}", local.WeekDay, m.ToDateString()));

            if (opts.ShowSunriset)
            {
                s = string.Format("Sunrise at {0}. Sunset at {1}",
                    timeToString(local.Sunrise),
                    timeToString(local.Sunset));
                mList.Items.Add(s);
            }

            if (opts.CalcSpecialKalas)
            {

                string s_rahu = string.Format("Rahu Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.RahuKalaIndex], h).ToTimeString(),
                    new Moment(local.KalasUT[local.RahuKalaIndex + 1], h).ToTimeString());
                string s_gulika = string.Format("Gulika Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.GulikaKalaIndex], h).ToTimeString(),
                    new Moment(local.KalasUT[local.GulikaKalaIndex + 1], h).ToTimeString());
                string s_yama = string.Format("Yama Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.YamaKalaIndex], h).ToTimeString(),
                    new Moment(local.KalasUT[local.YamaKalaIndex + 1], h).ToTimeString());

                if (opts.OneEntryPerLine)
                {
                    mList.Items.Add(s_rahu);
                    mList.Items.Add(s_gulika);
                    mList.Items.Add(s_yama);
                }
                else
                    mList.Items.Add(string.Format("{0}. {1}. {2}.", s_rahu, s_gulika, s_yama));
            }

            if (opts.CalcTithiCusps)
            {
                string s_tithi = "";

                if (local.TithiIndexStart == local.TithiIndexEnd &&
                    local.TithiIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.TithisUT[local.TithiIndexStart];
                    Tithi t = new Tithi((TithiName)pmi.Info);
                    mList.Items.Add(string.Format("{0} - full.", t.Value));
                }
                else
                {
                    for (int i = local.TithiIndexStart + 1; i <= local.TithiIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.TithisUT[i];
                        Tithi t = new Tithi((TithiName)pmi.Info).AddReverse(2);
                        s_tithi += string.Format("{0} until {1}",
                            t.Value,
                            utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));

                        if (opts.OneEntryPerLine)
                        {
                            mList.Items.Add(s_tithi);
                            s_tithi = "";
                        }
                        else
                            s_tithi += ". ";
                    }
                    if (false == opts.OneEntryPerLine)
                        mList.Items.Add(s_tithi);
                }
            }


            if (opts.CalcKaranaCusps)
            {
                string s_karana = "";

                if (local.KaranaIndexStart == local.KaranaIndexEnd &&
                    local.KaranaIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.KaranasUT[local.KaranaIndexStart];
                    Karana k = new Karana((KaranaName)pmi.Info);
                    mList.Items.Add(string.Format("{0} karana - full.", k.value));
                }
                else
                {
                    for (int i = local.KaranaIndexStart + 1; i <= local.KaranaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.KaranasUT[i];
                        Karana k = new Karana((KaranaName)pmi.Info).addReverse(2);
                        s_karana += string.Format("{0} karana until {1}",
                            k.value,
                            utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));

                        if (opts.OneEntryPerLine)
                        {
                            mList.Items.Add(s_karana);
                            s_karana = "";
                        }
                        else
                            s_karana += ". ";
                    }
                    if (false == opts.OneEntryPerLine)
                        mList.Items.Add(s_karana);
                }
            }



            if (opts.CalcSMYogaCusps)
            {
                string s_smyoga = "";

                if (local.SunMoonYogaIndexStart == local.SunMoonYogaIndexEnd &&
                    local.SunMoonYogaIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.SunMoonYogasUT[local.SunMoonYogaIndexStart];
                    SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info);
                    mList.Items.Add(string.Format("{0} yoga - full.", sm.Value));
                }
                else
                {
                    for (int i = local.SunMoonYogaIndexStart + 1; i <= local.SunMoonYogaIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.SunMoonYogasUT[i];
                        SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info).AddReverse(2);
                        s_smyoga += string.Format("{0} yoga until {1}",
                            sm.Value,
                            utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));

                        if (opts.OneEntryPerLine)
                        {
                            mList.Items.Add(s_smyoga);
                            s_smyoga = "";
                        }
                        else
                            s_smyoga += ". ";
                    }
                    if (false == opts.OneEntryPerLine)
                        mList.Items.Add(s_smyoga);
                }
            }



            if (opts.CalcNakCusps)
            {
                string s_nak = "";

                if (local.KaranaIndexStart == local.KaranaIndexEnd &&
                    local.NakshatraIndexStart >= 0)
                {
                    MomentInfo pmi = (MomentInfo)globals.NakshatrasUT[local.NakshatraIndexStart];
                    Nakshatra n = new Nakshatra((NakshatraName)pmi.Info);
                    mList.Items.Add(string.Format("{0} - full.", n.Value));
                }
                else
                {
                    for (int i = local.NakshatraIndexStart + 1; i <= local.NakshatraIndexEnd; i++)
                    {
                        if (i < 0)
                            continue;
                        MomentInfo pmi = (MomentInfo)globals.NakshatrasUT[i];
                        Nakshatra n = new Nakshatra((NakshatraName)pmi.Info).AddReverse(2);
                        s_nak += string.Format("{0} until {1}",
                            n.Value,
                            utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
                        if (opts.OneEntryPerLine)
                        {
                            mList.Items.Add(s_nak);
                            s_nak = "";
                        }
                        else
                            s_nak += ". ";
                    }
                    if (false == opts.OneEntryPerLine)
                        mList.Items.Add(s_nak);
                }
            }

            if (opts.CalcLagnaCusps)
            {
                string sLagna = "    ";
                ZodiacHouse zBase = new ZodiacHouse(local.LagnaZodiacHouse);
                for (int i = 0; i < 12; i++)
                {
                    MomentInfo pmi = (MomentInfo)local.LagnasUT[i];
                    ZodiacHouse zCurr = new ZodiacHouse((ZodiacHouseName)pmi.Info);
                    zCurr = zCurr.Add(12);
                    sLagna = string.Format("{0}{1} Lagna until {2}. ", sLagna, zCurr.Value,
                        utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sLagna);
                        sLagna = "";
                    }
                }
            }

            if (opts.CalcHoraCusps)
            {
                string sHora = "    ";
                for (int i = 0; i < 24; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 7, local.HoraBase + i);
                    BodyName bHora = h.HoraOrder[ib];
                    sHora = string.Format("{0}{1} hora until {2}. ", sHora, bHora,
                        utTimeToString(local.HorasUT[i + 1], local.SunriseUT, local.Sunrise));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sHora);
                        sHora = "";
                    }
                }
            }

            if (opts.CalcKalaCusps)
            {
                string sKala = "    ";
                for (int i = 0; i < 16; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 8, local.KalaBase + i);
                    BodyName bKala = h.KalaOrder[ib];
                    sKala = string.Format("{0}{1} kala until {2}. ", sKala, bKala,
                        utTimeToString(local.KalasUT[i + 1], local.SunriseUT, local.Sunrise));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sKala);
                        sKala = "";
                    }
                }
            }

            mList.Items.Add("");
        }

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected override void copyToClipboard()
        {
            string s = "";
            foreach (ListViewItem li in mList.Items)
            {
                s += li.Text + "\r\n";
            }
            Clipboard.SetDataObject(s, false);
        }

        private void menuCopyToClipboard_Click(object sender, EventArgs e)
        {
            copyToClipboard();
        }

        private void checkPrintReqs()
        {
            if (opts.CalcKalaCusps == false ||
                opts.CalcNakCusps == false ||
                opts.CalcTithiCusps == false ||
                opts.CalcSpecialKalas == false ||
                opts.CalcSMYogaCusps == false ||
                opts.CalcKaranaCusps == false)
            {
                MessageBox.Show("Not enough information calculated to show panchanga");
                throw new Exception();
            }
        }
        private void menuItemPrintPanchanga_Click(object sender, EventArgs e)
        {
            try
            {
                //this.checkPrintReqs();
                PanchangaPrintDocument mdoc = new PanchangaPrintDocument(opts, h, globals, locals);
                PrintDialog dlgPrint = new PrintDialog
                {
                    Document = mdoc
                };

                if (dlgPrint.ShowDialog() == DialogResult.OK)
                    mdoc.Print();
            }
            catch { }
        }

        private void menuItemFilePrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //this.checkPrintReqs();
                PanchangaPrintDocument mdoc = new PanchangaPrintDocument(opts, h, globals, locals);
                PrintPreviewDialog dlgPreview = new PrintPreviewDialog
                {
                    Document = mdoc
                };
                dlgPreview.ShowDialog();
            }
            catch { }
        }



    }

}

