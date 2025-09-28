

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{


    public class TransitSearch : PanchangControl
    {
        private ColumnHeader Moment;
        private IContainer components = null;
        private ColumnHeader Name1;
        private ContextMenu mContext;
        private MenuItem mOpenTransit;
        public PropertyGrid pgOptions;
        private ColumnHeader Event;
        private ColumnHeader Date;
        private ListView mlTransits;
        private Button bLocalSolarEclipse;
        private Button bSolarNewYear;
        private Button bProgressionLon;
        private Button bClearResults;
        private Button bNow;
        private Button bStartSearch;
        private Button bRetroCusp;
        private Button bProgression;
        private GroupBox groupBox1;
        private Button bTransitPrevVarga;
        private Button bVargaChange;
        private Button bTransitNextVarga;
        private Button bGlobSolEcl;
        private Panel panel1;
        private Button bGlobalLunarEclipse;
        private GroupBox groupBox2;
        private MenuItem mOpenTransitNext;
        private MenuItem mOpenTransitChartPrev;
        private TransitSearchOptions opts;

        public void Redisplay(object o)
        {
            mlTransits.Font = PanchangAppOptions.Instance.GeneralFont;
            mlTransits.BackColor = PanchangAppOptions.Instance.TableBackgroundColor;
            mlTransits.ForeColor = PanchangAppOptions.Instance.TableForegroundColor;
            pgOptions.Font = PanchangAppOptions.Instance.GeneralFont;
        }
        public void Reset()
        {
            updateOptions();
            mlTransits.Items.Clear();
            Redisplay(PanchangAppOptions.Instance);
        }

        public TransitSearch(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            h = _h;
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(Redisplay);
            opts = new TransitSearchOptions();
            updateOptions();
            AddViewsToContextMenu(mContext);

            ToolTip tt = null;

            tt = new ToolTip();
            tt.SetToolTip(bTransitPrevVarga, "Find when the graha goes to the previous rasi only");
            tt = new ToolTip();
            tt.SetToolTip(bTransitNextVarga, "Find when the graha goes to the next rasi only");
            tt = new ToolTip();
            tt.SetToolTip(bVargaChange, "Find when the graha changes rasis");

            // TODO: Add any initialization after the InitializeComponent call
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Name1 = new ColumnHeader();
            Moment = new ColumnHeader();
            mContext = new ContextMenu();
            mOpenTransit = new MenuItem();
            pgOptions = new PropertyGrid();
            mlTransits = new ListView();
            Event = new ColumnHeader();
            Date = new ColumnHeader();
            bLocalSolarEclipse = new Button();
            bSolarNewYear = new Button();
            bProgressionLon = new Button();
            bClearResults = new Button();
            bNow = new Button();
            bStartSearch = new Button();
            bRetroCusp = new Button();
            bProgression = new Button();
            groupBox1 = new GroupBox();
            bTransitPrevVarga = new Button();
            bVargaChange = new Button();
            bTransitNextVarga = new Button();
            bGlobSolEcl = new Button();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            bGlobalLunarEclipse = new Button();
            mOpenTransitNext = new MenuItem();
            mOpenTransitChartPrev = new MenuItem();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // Name1
            // 
            Name1.Text = "Type";
            Name1.Width = 269;
            // 
            // Moment
            // 
            Moment.Text = "Moment";
            Moment.Width = 188;
            // 
            // mContext
            // 
            mContext.MenuItems.AddRange(new MenuItem[] {
                                                                                     mOpenTransit,
                                                                                     mOpenTransitNext,
                                                                                     mOpenTransitChartPrev});
            mContext.Popup += new EventHandler(mContext_Popup);
            // 
            // mOpenTransit
            // 
            mOpenTransit.Index = 0;
            mOpenTransit.Text = "Open Transit Chart";
            mOpenTransit.Click += new EventHandler(mOpenTransit_Click);
            // 
            // pgOptions
            // 
            pgOptions.CommandsVisibleIfAvailable = true;
            pgOptions.HelpVisible = false;
            pgOptions.LargeButtons = false;
            pgOptions.LineColor = SystemColors.ScrollBar;
            pgOptions.Location = new Point(16, 8);
            pgOptions.Name = "pgOptions";
            pgOptions.PropertySort = PropertySort.Categorized;
            pgOptions.Size = new Size(296, 152);
            pgOptions.TabIndex = 5;
            pgOptions.Text = "Options";
            pgOptions.ToolbarVisible = false;
            pgOptions.ViewBackColor = SystemColors.Window;
            pgOptions.ViewForeColor = SystemColors.WindowText;
            pgOptions.Click += new EventHandler(pGrid_Click);
            // 
            // mlTransits
            // 
            mlTransits.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mlTransits.Columns.AddRange(new ColumnHeader[] {
                                                                                         Event,
                                                                                         Date});
            mlTransits.FullRowSelect = true;
            mlTransits.Location = new Point(16, 168);
            mlTransits.Name = "mlTransits";
            mlTransits.Size = new Size(648, 208);
            mlTransits.TabIndex = 9;
            mlTransits.View = View.Details;
            mlTransits.SelectedIndexChanged += new EventHandler(mlTransits_SelectedIndexChanged_1);
            // 
            // Event
            // 
            Event.Text = "Event";
            Event.Width = 387;
            // 
            // Date
            // 
            Date.Text = "Date";
            Date.Width = 173;
            // 
            // bLocalSolarEclipse
            // 
            bLocalSolarEclipse.Location = new Point(8, 40);
            bLocalSolarEclipse.Name = "bLocalSolarEclipse";
            bLocalSolarEclipse.Size = new Size(104, 23);
            bLocalSolarEclipse.TabIndex = 13;
            bLocalSolarEclipse.Text = "(L) Sol. Ecl.";
            bLocalSolarEclipse.Click += new EventHandler(bLocSolEclipse_Click);
            // 
            // bSolarNewYear
            // 
            bSolarNewYear.Location = new Point(152, 168);
            bSolarNewYear.Name = "bSolarNewYear";
            bSolarNewYear.Size = new Size(104, 23);
            bSolarNewYear.TabIndex = 9;
            bSolarNewYear.Text = "Solar Year";
            bSolarNewYear.Click += new EventHandler(bSolarNewYear_Click);
            // 
            // bProgressionLon
            // 
            bProgressionLon.Location = new Point(152, 136);
            bProgressionLon.Name = "bProgressionLon";
            bProgressionLon.Size = new Size(104, 23);
            bProgressionLon.TabIndex = 8;
            bProgressionLon.Text = "Progress Lons";
            bProgressionLon.Click += new EventHandler(bProgressionLon_Click);
            // 
            // bClearResults
            // 
            bClearResults.Location = new Point(16, 16);
            bClearResults.Name = "bClearResults";
            bClearResults.Size = new Size(104, 23);
            bClearResults.TabIndex = 3;
            bClearResults.Text = "Clear Results";
            bClearResults.Click += new EventHandler(bClearResults_Click);
            // 
            // bNow
            // 
            bNow.Location = new Point(16, 40);
            bNow.Name = "bNow";
            bNow.Size = new Size(104, 23);
            bNow.TabIndex = 7;
            bNow.Text = "Now";
            bNow.Click += new EventHandler(bNow_Click);
            // 
            // bStartSearch
            // 
            bStartSearch.Location = new Point(16, 72);
            bStartSearch.Name = "bStartSearch";
            bStartSearch.RightToLeft = RightToLeft.No;
            bStartSearch.Size = new Size(104, 23);
            bStartSearch.TabIndex = 2;
            bStartSearch.Text = "Find Transit";
            bStartSearch.Click += new EventHandler(bStartSearch_Click);
            // 
            // bRetroCusp
            // 
            bRetroCusp.Location = new Point(16, 96);
            bRetroCusp.Name = "bRetroCusp";
            bRetroCusp.RightToLeft = RightToLeft.No;
            bRetroCusp.Size = new Size(104, 23);
            bRetroCusp.TabIndex = 4;
            bRetroCusp.Text = "Find Retro";
            bRetroCusp.Click += new EventHandler(bRetroCusp_Click);
            // 
            // bProgression
            // 
            bProgression.Location = new Point(152, 112);
            bProgression.Name = "bProgression";
            bProgression.Size = new Size(104, 23);
            bProgression.TabIndex = 6;
            bProgression.Text = "Progress Time";
            bProgression.Click += new EventHandler(bProgression_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bTransitPrevVarga);
            groupBox1.Controls.Add(bVargaChange);
            groupBox1.Controls.Add(bTransitNextVarga);
            groupBox1.Location = new Point(144, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(120, 96);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Change Varga";
            // 
            // bTransitPrevVarga
            // 
            bTransitPrevVarga.Location = new Point(8, 16);
            bTransitPrevVarga.Name = "bTransitPrevVarga";
            bTransitPrevVarga.Size = new Size(104, 23);
            bTransitPrevVarga.TabIndex = 12;
            bTransitPrevVarga.Text = "<-- Prev";
            bTransitPrevVarga.TextAlign = ContentAlignment.MiddleLeft;
            bTransitPrevVarga.Click += new EventHandler(bTransitPrevVarga_Click);
            // 
            // bVargaChange
            // 
            bVargaChange.Location = new Point(8, 40);
            bVargaChange.Name = "bVargaChange";
            bVargaChange.Size = new Size(104, 23);
            bVargaChange.TabIndex = 13;
            bVargaChange.Text = "Change";
            bVargaChange.Click += new EventHandler(bVargaChange_Click);
            // 
            // bTransitNextVarga
            // 
            bTransitNextVarga.Location = new Point(8, 64);
            bTransitNextVarga.Name = "bTransitNextVarga";
            bTransitNextVarga.Size = new Size(104, 23);
            bTransitNextVarga.TabIndex = 10;
            bTransitNextVarga.Text = "Next  -->";
            bTransitNextVarga.TextAlign = ContentAlignment.MiddleRight;
            bTransitNextVarga.Click += new EventHandler(bTransitNextVarga_Click);
            // 
            // bGlobSolEcl
            // 
            bGlobSolEcl.Location = new Point(8, 64);
            bGlobSolEcl.Name = "bGlobSolEcl";
            bGlobSolEcl.Size = new Size(104, 23);
            bGlobSolEcl.TabIndex = 11;
            bGlobSolEcl.Text = "(G) Sol. Ecl.";
            bGlobSolEcl.Click += new EventHandler(bGlobSolEclipse_Click);
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(bSolarNewYear);
            panel1.Controls.Add(bProgressionLon);
            panel1.Controls.Add(bClearResults);
            panel1.Controls.Add(bNow);
            panel1.Controls.Add(bStartSearch);
            panel1.Controls.Add(bRetroCusp);
            panel1.Controls.Add(bProgression);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(320, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(304, 152);
            panel1.TabIndex = 8;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bLocalSolarEclipse);
            groupBox2.Controls.Add(bGlobSolEcl);
            groupBox2.Controls.Add(bGlobalLunarEclipse);
            groupBox2.Location = new Point(8, 128);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(120, 96);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Eclipses";
            // 
            // bGlobalLunarEclipse
            // 
            bGlobalLunarEclipse.Location = new Point(8, 16);
            bGlobalLunarEclipse.Name = "bGlobalLunarEclipse";
            bGlobalLunarEclipse.Size = new Size(104, 23);
            bGlobalLunarEclipse.TabIndex = 14;
            bGlobalLunarEclipse.Text = "Lunar Ecl.";
            bGlobalLunarEclipse.Click += new EventHandler(bGlobalLunarEclipse_Click);
            // 
            // mOpenTransitNext
            // 
            mOpenTransitNext.Index = 1;
            mOpenTransitNext.Text = "Open Transit Chart (Compress &Next)";
            mOpenTransitNext.Click += new EventHandler(mOpenTransitNext_Click);
            // 
            // mOpenTransitChartPrev
            // 
            mOpenTransitChartPrev.Index = 2;
            mOpenTransitChartPrev.Text = "Open Transit Chart (Compress &Prev)";
            mOpenTransitChartPrev.Click += new EventHandler(mOpenTransitChartPrev_Click);
            // 
            // TransitSearch
            // 
            ContextMenu = mContext;
            Controls.Add(pgOptions);
            Controls.Add(panel1);
            Controls.Add(mlTransits);
            Name = "TransitSearch";
            Size = new Size(672, 384);
            Load += new EventHandler(TransitSearch_Load);
            groupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);

        }
        #endregion

        private void TransitSearch_Load(object sender, EventArgs e)
        {
            Reset();

        }

        public object SetOptions(object o)
        {
            return opts.CopyFrom(o);
        }
        private void bOpts_Click(object sender, EventArgs e)
        {
            Options f = new Options(opts, new ApplyOptions(SetOptions));
            f.ShowDialog();
        }


        private Horoscope utToHoroscope(double found_ut, ref Moment m2)
        {
            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += h.Info.tz.toDouble() / 24.0;
            Sweph.SWE_ReverseJulianDay(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            HoraInfo inf = new HoraInfo(m,
                (HMSInfo)h.Info.lat.Clone(),
                (HMSInfo)h.Info.lon.Clone(),
                (HMSInfo)h.Info.tz.Clone());
            Horoscope hTransit = new Horoscope(inf,
                (HoroscopeOptions)h.Options.Clone());

            Sweph.SWE_ReverseJulianDay(found_ut + 5.0, ref year, ref month, ref day, ref hour);
            m2 = new Moment(year, month, day, hour);
            return hTransit;
        }

        private void ApplyLocal(Moment m)
        {
            if (opts.Apply)
            {
                h.Info.tob = m;
                h.OnChanged();
            }
        }
        private double DirectSpeed(BodyName b)
        {
            switch (b)
            {
                case BodyName.Sun: return 365.2425;
                case BodyName.Moon: return 28.0;
                case BodyName.Lagna: return 1.0;
            }
            return 0.0;
        }
        private void DirectProgression()
        {
            if (opts.SearchBody != BodyName.Sun &&
            opts.SearchBody != BodyName.Moon) // &&
                                               //opts.SearchBody != Body.Name.Lagna)
                return;

            double julday_ut = opts.StartDate.ToUniversalTime() - h.Info.tz.toDouble() / 24.0;
            //;.tob.time / 24.0;

            if (julday_ut <= h.baseUT)
            {
                MessageBox.Show("Error: Unable to progress in the future");
                return;
            }

            double totalProgression = GetProgressionDegree();
            double totalProgressionOrig = totalProgression;

            Sweph.Lock(h);
            Retrogression r = new Retrogression(h, opts.SearchBody);

            Longitude start_lon = r.GetLon(h.baseUT);
            //Console.WriteLine ("Real start lon is {0}", start_lon);
            double curr_julday = h.baseUT;
            Transit t = new Transit(h, opts.SearchBody);
            while (totalProgression >= 360.0)
            {
                curr_julday = t.LinearSearch(
                    curr_julday + DirectSpeed(opts.SearchBody),
                    start_lon, new ReturnLon(t.GenericLongitude));
                totalProgression -= 360.0;
            }

            curr_julday = t.LinearSearch(
                curr_julday + totalProgression / 360.0 * DirectSpeed(opts.SearchBody),
                start_lon.Add(totalProgression), new ReturnLon(t.GenericLongitude));


            //bool bDiscard = true;
            //Longitude got_lon = t.GenericLongitude(curr_julday, ref bDiscard);
            //Console.WriteLine ("Found Progressed Sun at {0}+{1}={2}={3}", 
            //	start_lon.value, new Longitude(totalProgressionOrig).value,
            //	got_lon.value, got_lon.sub(start_lon.add(totalProgressionOrig)).value
            //	);

            Sweph.Unlock(h);

            Moment m2 = new Moment(0, 0, 0, 0.0);
            Horoscope hTransit = utToHoroscope(curr_julday, ref m2);
            string fmt = hTransit.Info.DateOfBirth.ToString();
            ListViewItem li = new TransitItem(hTransit)
            {
                Text = string.Format("{0}'s Prog: {2}+{3:00.00} deg",
                opts.SearchBody, totalProgressionOrig,
                (int)Math.Floor(totalProgressionOrig / 360.0),
                new Longitude(totalProgressionOrig).Value)
            };
            li.SubItems.Add(fmt);
            ApplyLocal(hTransit.Info.tob);

            mlTransits.Items.Add(li);
            updateOptions();


        }
        private double GetProgressionDegree()
        {
            double julday_ut = opts.StartDate.ToUniversalTime() - h.Info.tz.toDouble() / 24.0;
            double ut_diff = julday_ut - h.baseUT;

            //Console.WriteLine ("Expected ut_diff is {0}", ut_diff);
            bool bDummy = true;
            Sweph.Lock(h);
            Transit t = new Transit(h);
            Longitude lon_start = t.LongitudeOfSun(h.baseUT, ref bDummy);
            Longitude lon_prog = t.LongitudeOfSun(julday_ut, ref bDummy);

            //Console.WriteLine ("Progression lons are {0} and {1}", lon_start, lon_prog);

            double dExpectedLon = ut_diff * 360.0 / 365.2425;
            Longitude lon_expected = lon_start.Add(dExpectedLon);
            Sweph.Unlock(h);


            if (Transit.CircLonLessThan(lon_expected, lon_prog))
                dExpectedLon += lon_prog.Subtract(lon_expected).Value;
            else
                dExpectedLon -= lon_expected.Subtract(lon_prog).Value;

            DivisionPosition dp = h.GetPosition(opts.SearchBody).ToDivisionPosition(opts.Division);

            //Console.WriteLine ("Sun progress {0} degrees in elapsed time", dExpectedLon);

            double ret = dExpectedLon / 360.0 * (30.0 / Basics.NumPartsInDivision(opts.Division));
            //(dp.cusp_higher - dp.cusp_lower);
            //Console.WriteLine ("Progressing by {0} degrees", ret);
            return ret;
        }
        private void bProgression_Click(object sender, EventArgs e)
        {
            if ((int)opts.SearchBody <= (int)BodyName.Moon ||
                (int)opts.SearchBody > (int)BodyName.Saturn)
            {
                DirectProgression();
                return;
            }

            DivisionPosition dp = h.GetPosition(opts.SearchBody).ToDivisionPosition(opts.Division);
            double yearlyProgression = (dp.CuspHigher - dp.CuspLower) / 30.0;
            double julday_ut = Sweph.SWE_JullianDay(
                opts.StartDate.Year,
                opts.StartDate.Month,
                opts.StartDate.Day,
                opts.StartDate.Hour + opts.StartDate.Minute / 60.0
                + opts.StartDate.Second / 3600.0);

            if (julday_ut <= h.baseUT)
            {
                MessageBox.Show("Error: Unable to progress in the future");
                return;
            }


            double totalProgression = GetProgressionDegree();
            double totalProgressionOrig = totalProgression;

            //Console.WriteLine ("Total Progression is {0}", totalProgression);
            bool becomesDirect = false;
            Sweph.Lock(h);
            Retrogression r = new Retrogression(h, opts.SearchBody);
            double curr_ut = h.baseUT;
            double next_ut = 0;
            double found_ut = h.baseUT;
            while (true)
            {
                next_ut = r.FindNextCuspForward(curr_ut, ref becomesDirect);
                Longitude curr_lon = r.GetLon(curr_ut);
                Longitude next_lon = r.GetLon(next_ut);


                if (false == becomesDirect && next_lon.Subtract(curr_lon).Value >= totalProgression)
                {
                    //Console.WriteLine ("1 Found {0} in {1}", totalProgression, next_lon.sub(curr_lon).value);
                    found_ut = r.GetTransitForward(curr_ut, curr_lon.Add(totalProgression));
                    break;
                }
                else if (true == becomesDirect && curr_lon.Subtract(next_lon).Value >= totalProgression)
                {
                    //Console.WriteLine ("2 Found {0} in {1}", totalProgression, curr_lon.sub(next_lon).value);
                    found_ut = r.GetTransitForward(curr_ut, curr_lon.Subtract(totalProgression));
                    break;
                }
                if (false == becomesDirect)
                {
                    //Console.WriteLine ("Progression: {0} degrees gone in direct motion", next_lon.sub(curr_lon).value);
                    totalProgression -= next_lon.Subtract(curr_lon).Value;
                }
                else
                {
                    //Console.WriteLine ("Progression: {0} degrees gone in retro motion", curr_lon.sub(next_lon).value);
                    totalProgression -= curr_lon.Subtract(next_lon).Value;
                }
                curr_ut = next_ut + 5.0;
            }
            Sweph.Unlock(h);

            Moment m2 = new Moment(0, 0, 0, 0.0);
            Horoscope hTransit = utToHoroscope(found_ut, ref m2);
            string fmt = hTransit.Info.DateOfBirth.ToString();
            ListViewItem li = new TransitItem(hTransit)
            {
                Text = string.Format("{0}'s Prog: {2}+{3:00.00} deg",
                opts.SearchBody, totalProgressionOrig,
                (int)Math.Floor(totalProgressionOrig / 360.0),
                new Longitude(totalProgressionOrig).Value)
            };
            li.SubItems.Add(fmt);
            mlTransits.Items.Add(li);
            updateOptions();
            ApplyLocal(hTransit.Info.tob);
        }

        private void bProgressionLon_Click(object sender, EventArgs e)
        {
            if (opts.Apply == false)
            {
                MessageBox.Show("This will modify the current chart. You must set Apply to 'true'");
                return;
            }
            h.OnChanged();
            double degToProgress = GetProgressionDegree();
            Longitude lonProgress = new Longitude(degToProgress);

            foreach (BodyPosition bp in h.PositionList)
            {
                bp.Longitude = bp.Longitude.Add(lonProgress);
            }
            h.OnlySignalChanged();
        }

        private void bRetroCusp_Click(object sender, EventArgs e)
        {
            if ((int)opts.SearchBody <= (int)BodyName.Moon ||
                (int)opts.SearchBody > (int)BodyName.Saturn)
                return;

            bool becomesDirect = false;
            Sweph.Lock(h);
            Retrogression r = new Retrogression(h, opts.SearchBody);
            double julday_ut = opts.StartDate.ToUniversalTime() - h.Info.tz.toDouble() / 24.0;
            double found_ut = julday_ut;
            if (opts.Forward)
            {
                found_ut = r.FindNextCuspForward(julday_ut, ref becomesDirect);
            }
            else
                found_ut = r.FindNextCuspBackward(julday_ut, ref becomesDirect);


            bool bForward = false;
            Longitude found_lon = r.GetLon(found_ut, ref bForward);
            Sweph.Unlock(h);

            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += h.Info.tz.toDouble() / 24.0;
            Sweph.SWE_ReverseJulianDay(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            HoraInfo inf = new HoraInfo(m,
                (HMSInfo)h.Info.lat.Clone(),
                (HMSInfo)h.Info.lon.Clone(),
                (HMSInfo)h.Info.tz.Clone());
            Horoscope hTransit = new Horoscope(inf,
                (HoroscopeOptions)h.Options.Clone());

            if (opts.Forward)
                Sweph.SWE_ReverseJulianDay(found_ut + 5.0, ref year, ref month, ref day, ref hour);
            else
                Sweph.SWE_ReverseJulianDay(found_ut - 5.0, ref year, ref month, ref day, ref hour);
            Moment m2 = new Moment(year, month, day, hour);
            opts.StartDate = m2;
            // add entry to our list
            string fmt = hTransit.Info.DateOfBirth.ToString();
            ListViewItem li = new TransitItem(hTransit)
            {
                Text = Body.ToString(opts.SearchBody)
            };
            if (becomesDirect)
                li.Text += " goes direct at " + found_lon.ToString();
            else
                li.Text += " goes retrograde at " + found_lon.ToString();
            li.SubItems.Add(fmt);
            mlTransits.Items.Add(li);
            updateOptions();
            ApplyLocal(hTransit.Info.tob);

        }

        private void bStartSearch_Click(object sender, EventArgs e)
        {
            StartSearch(true);
        }

        private void UpdateDateForNextSearch(double ut)
        {
            int year = 0, month = 0, day = 0;
            double hour = 0;

            double offset = 10.0 / (24.0 * 60.0 * 60.0);
            if (opts.Forward == true)
                ut += offset;
            else
                ut -= offset;


            Sweph.SWE_ReverseJulianDay(ut, ref year, ref month, ref day, ref hour);
            Moment m2 = new Moment(year, month, day, hour);
            opts.StartDate = m2;
            updateOptions();
        }





        private double StartSearch(bool bUpdateDate)
        {
            CuspTransitSearch cs = new CuspTransitSearch(h);
            Longitude found_lon = opts.TransitPoint.Add(0);
            bool bForward = true;
            double found_ut =
                cs.TransitSearch(opts.SearchBody, opts.StartDate, opts.Forward, opts.TransitPoint,
                found_lon, ref bForward);


            Moment m2 = new Moment(0, 0, 0, 0);
            Horoscope hTransit = utToHoroscope(found_ut, ref m2);
            UpdateDateForNextSearch(found_ut);

            // add entry to our list
            string fmt = hTransit.Info.DateOfBirth.ToString();
            ListViewItem li = new TransitItem(hTransit)
            {
                Text = Body.ToString(opts.SearchBody)
            };
            if (bForward == false)
                li.Text += " (R)";
            li.Text += " transits " + found_lon.ToString();

            li.SubItems.Add(fmt);
            mlTransits.Items.Add(li);
            updateOptions();
            ApplyLocal(hTransit.Info.tob);

            return found_ut;
        }


        private void mlTransits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mContext_Popup(object sender, EventArgs e)
        {

        }

        private void openTransitHelper(Horoscope hTransit)
        {
            hTransit.Info.type = HoraType.Transit;
            PanchangChild mcTransit = new PanchangChild(hTransit)
            {
                Name = "Transit Chart",
                Text = "Transit Chart",
                MdiParent = (PanchangContainer)PanchangAppOptions.mainControl
            };
            mcTransit.Show();
        }

        private void mOpenTransit_Click(object sender, EventArgs e)
        {
            if (mlTransits.SelectedItems.Count == 0)
                return;

            TransitItem ti = (TransitItem)mlTransits.SelectedItems[0];
            Horoscope hTransit = ti.GetHoroscope();
            openTransitHelper(hTransit);
        }



        private void mOpenTransitNext_Click(object sender, EventArgs e)
        {
            if (mlTransits.SelectedItems.Count == 0)
                return;

            TransitItem ti = (TransitItem)mlTransits.SelectedItems[0];
            Horoscope hTransit = ti.GetHoroscope();

            int nextEntry = mlTransits.SelectedItems[0].Index + 1;
            if (mlTransits.Items.Count >= nextEntry + 1)
            {
                TransitItem tiNext = (TransitItem)mlTransits.Items[nextEntry];
                Horoscope hTransitNext = tiNext.GetHoroscope();

                double ut_diff = hTransitNext.baseUT - hTransit.baseUT;
                if (ut_diff > 0)
                {
                    hTransit.Info.defaultYearCompression = 1;
                    hTransit.Info.defaultYearLength = ut_diff;
                    hTransit.Info.defaultYearType = DateType.FixedYear;
                }

            }

            openTransitHelper(hTransit);
        }

        private void mOpenTransitChartPrev_Click(object sender, EventArgs e)
        {
            if (mlTransits.SelectedItems.Count == 0)
                return;

            TransitItem ti = (TransitItem)mlTransits.SelectedItems[0];
            Horoscope hTransit = ti.GetHoroscope();
            hTransit.Info.type = HoraType.Transit;

            int prevEntry = mlTransits.SelectedItems[0].Index - 1;
            if (prevEntry >= 0)
            {
                TransitItem tiPrev = (TransitItem)mlTransits.Items[prevEntry];
                Horoscope hTransitPrev = tiPrev.GetHoroscope();

                double ut_diff = hTransit.baseUT - hTransitPrev.baseUT;
                if (ut_diff > 0)
                {
                    hTransit.Info.defaultYearCompression = 1;
                    hTransit.Info.defaultYearLength = ut_diff;
                    hTransit.Info.defaultYearType = DateType.FixedYear;
                }

            }

            openTransitHelper(hTransit);
        }


        private void cbGrahas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void bClearResults_Click(object sender, EventArgs e)
        {
            mlTransits.Items.Clear();
        }

        private void pGrid_Click(object sender, EventArgs e)
        {

        }


        private void bNow_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Moment m = new Moment(now.Year, now.Month, now.Day,
                now.Hour + now.Minute / 60.0 + now.Second / 3600.0);
            opts.StartDate = m;
            updateOptions();
        }

        private void bSolarNewYear_Click(object sender, EventArgs e)
        {
            opts.SearchBody = BodyName.Sun;
            opts.TransitPoint.Value = 0;
            updateOptions();
            bStartSearch_Click(sender, e);
        }

        private void bTransitPrevVarga_Click(object sender, EventArgs e)
        {
            Horoscope h2 = (Horoscope)h.Clone();
            h2.Info.tob = opts.StartDate;
            h2.OnChanged();
            DivisionPosition dp = h2.GetPosition(opts.SearchBody).ToDivisionPosition(opts.Division);
            opts.TransitPoint = new Longitude(dp.CuspLower);

            double found_ut = StartSearch(false) + h.Info.tz.toDouble() / 24.0;
            UpdateDateForNextSearch(found_ut);
            updateOptions();
        }

        private void updateOptions()
        {
            pgOptions.SelectedObject = new GlobalizedPropertiesWrapper(opts);
        }

        private void bTransitNextVarga_Click(object sender, EventArgs e)
        {
            // Update Search Parameters
            Horoscope h2 = (Horoscope)h.Clone();
            h2.Info.tob = opts.StartDate;
            h2.OnChanged();
            DivisionPosition dp = h2.GetPosition(opts.SearchBody).ToDivisionPosition(opts.Division);
            opts.TransitPoint = new Longitude(dp.CuspHigher);
            opts.TransitPoint = opts.TransitPoint.Add(1.0 / (60.0 * 60.0 * 60.0));

            double found_ut = StartSearch(false) + h.Info.tz.toDouble() / 24.0;
            UpdateDateForNextSearch(found_ut);
            updateOptions();
        }
        private void bVargaChange_Click(object sender, EventArgs e)
        {
            if (opts.SearchBody == BodyName.Sun ||
                opts.SearchBody == BodyName.Moon ||
                opts.SearchBody == BodyName.Lagna)
            {
                if (opts.Forward == true)
                    bTransitNextVarga_Click(sender, e);
                else
                    bTransitPrevVarga_Click(sender, e);
                return;
            }

            Horoscope h2 = (Horoscope)h.Clone();
            h2.Info.tob = opts.StartDate;
            h2.OnChanged();
            BodyPosition bp = h2.GetPosition(opts.SearchBody);
            DivisionPosition dp = bp.ToDivisionPosition(opts.Division);

            bool becomesDirect = false;
            bool bForward = false;
            Sweph.Lock(h);
            Retrogression r = new Retrogression(h, opts.SearchBody);
            double julday_ut = opts.StartDate.ToUniversalTime() - h.Info.tz.toDouble() / 24.0;
            double found_ut = julday_ut;
            bool bTransitForwardCusp = true;
            while (true)
            {
                if (opts.Forward)
                    found_ut = r.FindNextCuspForward(found_ut, ref becomesDirect);
                else
                    found_ut = r.FindNextCuspBackward(found_ut, ref becomesDirect);

                Longitude found_lon = r.GetLon(found_ut, ref bForward);


                if (new Longitude(dp.CuspHigher).IsBetween(bp.Longitude, found_lon))
                {
                    bTransitForwardCusp = true;
                    break;
                }

                if (new Longitude(dp.CuspLower).IsBetween(found_lon, bp.Longitude))
                {
                    bTransitForwardCusp = false;
                    break;
                }

                if (opts.Forward)
                    found_ut += 5.0;
                else
                    found_ut -= 5.0;
            }
            Sweph.Unlock(h);
            if (opts.Forward)
                found_ut += 5.0;
            else
                found_ut -= 5.0;
            UpdateDateForNextSearch(found_ut);

            if (bTransitForwardCusp)
            {
                opts.TransitPoint.Value = dp.CuspHigher;
                updateOptions();
                bStartSearch_Click(sender, e);
            }
            else
            {
                opts.TransitPoint.Value = dp.CuspLower;
                updateOptions();
                bStartSearch_Click(sender, e);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        protected override void copyToClipboard()
        {
            string s = "";
            foreach (ListViewItem li in mlTransits.Items)
            {
                foreach (ListViewItem.ListViewSubItem si in li.SubItems)
                {
                    s += si.Text + ". ";
                }
                s += "\r\n";
                Clipboard.SetDataObject(s, true);
            }
        }

        private void SolarEclipseHelper(double ut, string desc)
        {
            SolarEclipseHelper(ut, ut - 1, ut + 1, desc);
        }
        private void SolarEclipseHelper(double ut, double start, double end, string desc)
        {
            if (ut < start || ut > end)
                return;

            ListViewItem li = new ListViewItem(desc);
            Moment m = new Moment(0, 0, 0, 0);
            Horoscope hTransit = utToHoroscope(ut, ref m);
            li.SubItems.Add(hTransit.Info.tob.ToString());
            mlTransits.Items.Add(li);
        }
        private void bGlobSolEclipse_Click(object sender, EventArgs e)
        {
            double julday_ut = opts.StartDate.ToUniversalTime(h);
            double[] tret = new double[10];
            Sweph.Lock(h);
            Sweph.SWE_FindSolarEclipseGlobally(julday_ut, tret, opts.Forward);
            Sweph.Unlock(h);
            SolarEclipseHelper(tret[2], "Global Solar Eclipse Begins");
            SolarEclipseHelper(tret[3], "   Global Solar Eclipse Ends");
            SolarEclipseHelper(tret[4], tret[2], tret[3], "   Global Solar Eclipse Totality Begins");
            SolarEclipseHelper(tret[5], tret[2], tret[3], "   Global Solar Eclipse Totality Ends");
            SolarEclipseHelper(tret[0], "   Global Solar Eclipse Maximum");
            SolarEclipseHelper(tret[6], tret[2], tret[3], "   Global Solar Eclipse Centerline Begins");
            SolarEclipseHelper(tret[7], tret[2], tret[3], "   Global Solar Eclipse Centerline Begins");
            if (opts.Forward)
                opts.StartDate = new Moment(tret[3] + 1.0, h);
            else
                opts.StartDate = new Moment(tret[2] - 1.0, h);
            updateOptions();
        }
        private void bLocSolEclipse_Click(object sender, EventArgs e)
        {
            double julday_ut = opts.StartDate.ToUniversalTime(h);
            double[] tret = new double[10];
            double[] attr = new double[10];
            Sweph.Lock(h);
            Sweph.SWE_FindSolarEclipseLocally(h.Info, julday_ut, tret, attr, opts.Forward);
            Sweph.Unlock(h);
            SolarEclipseHelper(tret[0], "Local Solar Eclipse Maximum");
            SolarEclipseHelper(tret[1], tret[0] - 1, tret[0] + 1, "   Local Solar Eclipse 1st Contact");
            SolarEclipseHelper(tret[2], tret[0] - 1, tret[0] + 1, "   Local Solar Eclipse 2nd Contact");
            SolarEclipseHelper(tret[3], tret[0] - 1, tret[0] + 1, "   Local Solar Eclipse 3rd Contact");
            SolarEclipseHelper(tret[4], tret[0] - 1, tret[0] + 1, "   Local Solar Eclipse 4th Contact");
            if (opts.Forward)
                opts.StartDate = new Moment(tret[0] + 1.0, h);
            else
                opts.StartDate = new Moment(tret[0] - 1.0, h);
            updateOptions();
        }

        private void bGlobalLunarEclipse_Click(object sender, EventArgs e)
        {
            double julday_ut = opts.StartDate.ToUniversalTime(h);
            double[] tret = new double[10];
            Sweph.Lock(h);
            Sweph.SWE_FindLunarEclipse(julday_ut, tret, opts.Forward);
            Sweph.Unlock(h);
            SolarEclipseHelper(tret[0], "Lunar Eclipse Maximum");
            SolarEclipseHelper(tret[2], tret[0] - 1, tret[0] + 1, "   Lunar Eclipse Begins");
            SolarEclipseHelper(tret[3], tret[0] - 1, tret[0] + 1, "   Lunar Eclipse Ends");
            SolarEclipseHelper(tret[4], tret[0] - 1, tret[0] + 1, "   Lunar Total Eclipse Begins");
            SolarEclipseHelper(tret[5], tret[0] - 1, tret[0] + 1, "   Lunar Total Eclipse Ends");
            SolarEclipseHelper(tret[6], tret[0] - 1, tret[0] + 1, "   Lunar Penumbral Eclipse Begins");
            SolarEclipseHelper(tret[7], tret[0] - 1, tret[0] + 1, "   Lunar Penumbral Eclipse Ends");
            if (opts.Forward)
                opts.StartDate = new Moment(tret[0] + 1.0, h);
            else
                opts.StartDate = new Moment(tret[0] - 1.0, h);
            updateOptions();
        }

        private void mlTransits_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


    }



    public class TransitSearchOptions : ICloneable
    {
        private Moment mDateBase;
        private bool mForward;
        private BodyName mSearchGraha;
        private Longitude mTransitPoint;
        private Division mDivision;
        private bool mApplyNow;

        public TransitSearchOptions()
        {
            DateTime dt = DateTime.Now;
            mDateBase = new Moment(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            mSearchGraha = BodyName.Sun;
            mTransitPoint = new Longitude(0.0);
            mForward = true;
            Division = new Division(DivisionType.Rasi);
        }



        [InVisible]
        public Division Division
        {
            get { return mDivision; }
            set { mDivision = value; }
        }

        [Category("Transit Search")]
        [PropertyOrder(1), Visible("In Varga")]
        public DivisionType UIDivision
        {
            get { return mDivision.MultipleDivisions[0].Varga; }
            set { mDivision = new Division(value); }
        }

        public enum EForward
        {
            Before, After
        }

        [Category("Transit Search")]
        [PropertyOrder(2), Visible("Search")]
        public EForward UIForward
        {
            get
            {
                if (Forward) return EForward.After;
                else return EForward.Before;
            }
            set
            {
                if (value == EForward.After) Forward = true;
                else Forward = false;
            }
        }

        [InVisible]
        public bool Forward
        {
            get { return mForward; }
            set { mForward = value; }
        }

        [Category("Transit Search")]
        [PropertyOrder(3), Visible("Date")]
        public Moment StartDate
        {
            get { return mDateBase; }
            set { mDateBase = value; }
        }
        [Category("Transit Search")]
        [PropertyOrder(4), Visible("When Body")]
        public BodyName SearchBody
        {
            get { return mSearchGraha; }
            set { mSearchGraha = value; }
        }
        [Category("Transit Search")]
        [PropertyOrder(5), Visible("Transits")]
        public Longitude TransitPoint
        {
            get { return mTransitPoint; }
            set { mTransitPoint = value; }
        }
        [Category("Transit Search")]
        [PropertyOrder(6), Visible("Apply Locally")]
        public bool Apply
        {
            get { return mApplyNow; }
            set { mApplyNow = value; }
        }
        #region ICloneable Members

        public object Clone()
        {
            // TODO:  Add TransitSearchOptions.Clone implementation
            TransitSearchOptions ret = new TransitSearchOptions
            {
                mDateBase = (Moment)mDateBase.Clone(),
                mForward = mForward,
                mSearchGraha = mSearchGraha,
                mTransitPoint = mTransitPoint
            };
            return ret;
        }
        public object CopyFrom(object o)
        {
            TransitSearchOptions nopt = (TransitSearchOptions)o;
            mDateBase = (Moment)nopt.mDateBase.Clone();
            mForward = nopt.mForward;
            mSearchGraha = nopt.mSearchGraha;
            mTransitPoint = nopt.mTransitPoint;
            return Clone();
        }

        #endregion
    }


    public class TransitItem : ListViewItem
    {
        private Horoscope h;

        public Horoscope GetHoroscope()
        {
            return h;
        }
        public TransitItem(Horoscope _h)
        {
            h = _h;
        }
    }

}

