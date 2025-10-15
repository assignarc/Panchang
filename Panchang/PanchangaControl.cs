using org.transliteral.panchang.data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
namespace org.transliteral.panchang.app
{
    public delegate void DelegateComputeFinished();
    public class PanchangaControl : BaseControl
    {
     
        private ListView mList;
        private Button bOpts;
        private IContainer components = null;
        private Button bCompute;
        private PanchangOptions opts = null;
        private ContextMenu contextMenu;
        private MenuItem menuItemPrintPanchanga;
        private MenuItem menuItemFilePrintPreview;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        public DelegateComputeFinished m_DelegateComputeFinished;
        bool bResultsInvalid = true;

        ArrayList locals = new ArrayList();
        GlobalMoments globals = new GlobalMoments();

        public PanchangaControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            horoscope = _h;
            horoscope.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            opts = new PanchangOptions()
            {
                NumberOfDays = 3
            };
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
            #region PROGRESS DIALOG - Disabled for now.
            //this.mutexProgress.WaitOne();
            //if (fProgress != null)
            //{
            //	this.mutexProgress.Close();
            //	return;
            //}
            //fProgress = new ProgressDialog(opts.NumDays);
            //fProgress.setProgress(opts.NumDays/2);



            //fProgress.ShowDialog();
            //this.mutexProgress.Close();
            #endregion

            Logger.Info("Threaded Execution : Started");

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate {
                    bCompute.Enabled = false;
                    bOpts.Enabled = false;
                    ContextMenu = null;
                }));
            }
            else
            {
                bCompute.Enabled = false;
                bOpts.Enabled = false;
                ContextMenu = null;
            }
            ComputeEntries();
            Invoke(m_DelegateComputeFinished);

        }
        private void ComputeFinished()
        {
            Logger.Info("Threaded Execution : Finished");
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
            // Marshal all UI updates to the UI thread
            if (mList.InvokeRequired)
            {
                mList.Invoke(new MethodInvoker(ComputeEntries));
                return;
            }

            mList.Clear();
            mList.Columns.Add("", -2, HorizontalAlignment.Left);

            if (false == opts.ShowUpdates)
                mList.BeginUpdate();

            double ut_start = Math.Floor(horoscope.BaseUT);
            double[] geopos = new double[]
            { 
                horoscope.Info.lon.toDouble(), 
                horoscope.Info.lat.toDouble(), 
                horoscope.Info.alt 
            };

            globals = new GlobalMoments();
            locals = new ArrayList();

            HinduPanchang hinduPanchang = new HinduPanchang(horoscope, opts);
            hinduPanchang.Compute();
            foreach (var pDay in hinduPanchang.Calendar.Values)
            {
                this.DisplayEntrypDay(pDay);
                mList.Columns[0].Width = -2;
            }

            #region REFERENCE CODE - To ensure the new one is working exactly the same.
            //for (int i = 0; i < opts.NumberOfDays; i++)
            //{
            //    ComputeEntry(ut_start, geopos);

            //    ut_start += 1;
            //    mList.Columns[0].Width = -2;
            //}
            #endregion

            mList.Columns[0].Width = -2;

            if (false == opts.ShowUpdates)
                mList.EndUpdate();
        }
        private void DisplayEntrypDay(PanchangDay pDay)
        {
            foreach (var item in pDay.Texts)
            {
                if(!item.Key.StartsWith("#"))
                    mList.Items.Add(item.Value);
            }
            mList.Items.Add("");
        }


        #region REFERENCE CODE - To ensure the new one is working exactly the same.
        //OLD way to compute panchanga entries. Kept for reference.
        private void ComputeEntry(double ut, double[] geopos)
        {

            int year = 0, month = 0, day = 0;
            double sunset = 0, hour = 0,  sunrise = 0, ut_sr = 0;
            Sweph.Lock(horoscope);
            horoscope.PopulateSunrisetCacheHelper(ut - 0.5, ref sunrise, ref sunset, ref ut_sr);
            Sweph.Unlock(horoscope);

            Sweph.SWE_ReverseJulianDay(ut_sr, ref year, ref month, ref day, ref hour);
            Moment moment_sr = new Moment(year, month, day, hour);
            Moment moment_ut = new Moment(ut, horoscope);
            HoraInfo infoCurr = new HoraInfo(moment_ut, horoscope.Info.lat, horoscope.Info.lon, horoscope.Info.tz);
            Horoscope hCurr = new Horoscope(infoCurr, horoscope.Options);

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
            if (opts.CalculateSpecialKalas)
            {
                BodyName bStart = Basics.WeekdayRuler(hCurr.Weekday);
                if (hCurr.Options.KalaType == EHoraType.Lmt)
                    bStart = Basics.WeekdayRuler(hCurr.WeekdayLMT);

                local.RahuKalaIndex = opts.RahuKalas[(int)bStart];
                local.GulikaKalaIndex = opts.GulikaKalas[(int)bStart];
                local.YamaKalaIndex = opts.YamaKalas[(int)bStart];
            }

            if (opts.CalculateLagnaCusps)
            {
                li = new ListViewItem();
                Sweph.Lock(horoscope);
                BodyPosition bp_lagna_sr = Basics.CalculateSingleBodyPosition(ut_sr, Sweph.BodyNameToSweph(BodyName.Lagna), BodyName.Lagna, BodyType.Name.Lagna, horoscope);
                DivisionPosition dp_lagna_sr = bp_lagna_sr.ToDivisionPosition(new Division(DivisionType.Rasi));
                local.LagnaZodiacHouse = dp_lagna_sr.ZodiacHouse.Value;

                Longitude bp_lagna_base = new Longitude(bp_lagna_sr.Longitude.ToZodiacHouseBase());
                double ut_transit = ut_sr;
                for (int i = 1; i <= 12; i++)
                {
                    Retrogression r = new Retrogression(horoscope, BodyName.Lagna);
                    ut_transit = r.GetLagnaTransitForward(ut_transit, bp_lagna_base.Add(i * 30.0));

                    MomentInfo pmi = new MomentInfo(
                        ut_transit, (int)bp_lagna_sr.Longitude.ToZodiacHouse().Add(i + 1).Value);
                    local.LagnasUT.Add(pmi);
                }

                Sweph.Unlock(horoscope);
            }

            if (opts.CalculateTithiCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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
                Sweph.Unlock(horoscope);
            }

            if (opts.CalculateKaranaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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
                Sweph.Unlock(horoscope);
            }

            if (opts.CalculateSunMoonYogaCusps)
            {
                Transit t = new Transit(horoscope);
                Sweph.Lock(horoscope);
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

                Sweph.Unlock(horoscope);
            }

            if (opts.CalculateNakshatraCusps)
            {
                bool bDiscard = true;
                Transit t = new Transit(horoscope, BodyName.Moon);
                Sweph.Lock(horoscope);
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
                Sweph.Unlock(horoscope);
            }

            if (opts.CalculateHoraCusps)
            {
                local.HorasUT = hCurr.GetHoraCuspsUt();
                hCurr.CalculateHora(ut_sr + 1.0 / 24.0, ref local.HoraBase);
            }

            if (opts.CalculateKalaCusps)
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
                    TimeUtils.TimeToString(local.Sunrise),
                    TimeUtils.TimeToString(local.Sunset));
                mList.Items.Add(s);
            }

            if (opts.CalculateSpecialKalas)
            {

                string s_rahu = string.Format("Rahu Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.RahuKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.RahuKalaIndex + 1], horoscope).ToTimeString());
                string s_gulika = string.Format("Gulika Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.GulikaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.GulikaKalaIndex + 1], horoscope).ToTimeString());
                string s_yama = string.Format("Yama Kala from {0} to {1}",
                    new Moment(local.KalasUT[local.YamaKalaIndex], horoscope).ToTimeString(),
                    new Moment(local.KalasUT[local.YamaKalaIndex + 1], horoscope).ToTimeString());

                if (opts.OneEntryPerLine)
                {
                    mList.Items.Add(s_rahu);
                    mList.Items.Add(s_gulika);
                    mList.Items.Add(s_yama);
                }
                else
                    mList.Items.Add(string.Format("{0}. {1}. {2}.", s_rahu, s_gulika, s_yama));
            }

            if (opts.CalculateTithiCusps)
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
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz,opts.LargeHours));

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


            if (opts.CalculateKaranaCusps)
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
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise,horoscope.Info.tz,opts.LargeHours));

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



            if (opts.CalculateSunMoonYogaCusps)
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
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, opts.LargeHours));

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



            if (opts.CalculateNakshatraCusps)
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
                            TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, opts.LargeHours));
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

            if (opts.CalculateLagnaCusps)
            {
                string sLagna = "    ";
                ZodiacHouse zBase = new ZodiacHouse(local.LagnaZodiacHouse);
                for (int i = 0; i < 12; i++)
                {
                    MomentInfo pmi = (MomentInfo)local.LagnasUT[i];
                    ZodiacHouse zCurr = new ZodiacHouse((ZodiacHouseName)pmi.Info);
                    zCurr = zCurr.Add(12);
                    sLagna = string.Format("{0}{1} Lagna until {2}. ", sLagna, zCurr.Value,
                        TimeUtils.UtTimeToString(pmi.UT, local.SunriseUT, local.Sunrise, horoscope.Info.tz, opts.LargeHours));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sLagna);
                        sLagna = "";
                    }
                }
            }

            if (opts.CalculateHoraCusps)
            {
                string sHora = "    ";
                for (int i = 0; i < 24; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 7, local.HoraBase + i);
                    BodyName bHora = horoscope.HoraOrder[ib];
                    sHora = string.Format("{0}{1} hora until {2}. ", sHora, bHora,
                        TimeUtils.UtTimeToString(local.HorasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, opts.LargeHours));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sHora);
                        sHora = "";
                    }
                }
            }

            if (opts.CalculateKalaCusps)
            {
                string sKala = "    ";
                for (int i = 0; i < 16; i++)
                {
                    int ib = (int)Basics.NormalizeLower(0, 8, local.KalaBase + i);
                    BodyName bKala = horoscope.KalaOrder[ib];
                    sKala = string.Format("{0}{1} kala until {2}. ", sKala, bKala,
                        TimeUtils.UtTimeToString(local.KalasUT[i + 1], local.SunriseUT, local.Sunrise, horoscope.Info.tz, opts.LargeHours));
                    if (opts.OneEntryPerLine || i % 4 == 3)
                    {
                        mList.Items.Add(sKala);
                        sKala = "";
                    }
                }
            }

            mList.Items.Add("");
        }
        #endregion
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
            if (opts.CalculateKalaCusps == false ||
                opts.CalculateNakshatraCusps == false ||
                opts.CalculateTithiCusps == false ||
                opts.CalculateSpecialKalas == false ||
                opts.CalculateSunMoonYogaCusps == false ||
                opts.CalculateKaranaCusps == false)
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
                PanchangaPrintDocument mdoc = new PanchangaPrintDocument(opts, horoscope, globals, locals);
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
                PanchangaPrintDocument mdoc = new PanchangaPrintDocument(opts, horoscope, globals, locals);
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

