

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for VargaRectificationForm.
    /// </summary>
    public class VargaRectificationForm : Form
    {
        public class UserOptions : ICloneable
        {
            Moment mStart;
            Moment mEnd;

            Division[] dtypes = new Division[]
            {
                new Division(DivisionType.Rasi),
                new Division(DivisionType.DrekkanaParasara),
                new Division(DivisionType.Navamsa),
                new Division(DivisionType.Saptamsa),
                new Division(DivisionType.Dasamsa),
                new Division(DivisionType.Dwadasamsa),
                new Division(DivisionType.Shodasamsa)
            };

            public Division[] Divisions
            {
                get { return dtypes; }
                set { dtypes = value; }
            }
            public Moment StartTime
            {
                get { return mStart; }
                set { mStart = value; }
            }
            public Moment EndTime
            {
                get { return mEnd; }
                set { mEnd = value; }
            }
            public UserOptions(Moment _start, Moment _end, Division dtype)
            {
                mStart = _start;
                mEnd = _end;

                if (dtype.MultipleDivisions.Length == 1 &&
                    dtype.MultipleDivisions[0].Varga != DivisionType.Rasi &&
                    dtype.MultipleDivisions[0].Varga != DivisionType.Navamsa)
                {
                    dtypes = new Division[]
                    {
                        new Division(DivisionType.Rasi),
                        new Division(DivisionType.Navamsa),
                        dtype
                    };
                }
                else
                {
                    dtypes = new Division[]
                    {
                        new Division(DivisionType.Rasi),
                        new Division(DivisionType.Saptamsa),
                        new Division(DivisionType.Navamsa)
                    };
                }

            }
            public UserOptions(Moment _start, Moment _end)
            {
                mStart = _start;
                mEnd = _end;
            }
            public object Clone()
            {
                UserOptions uo = new UserOptions((Moment)mStart.Clone(), (Moment)mEnd.Clone());
                uo.Divisions = (Division[])Divisions.Clone();
                return uo;
            }
            public object CopyFrom(object _uo)
            {
                UserOptions uo = (UserOptions)_uo;
                StartTime = uo.StartTime;
                EndTime = uo.EndTime;
                Divisions = (Division[])uo.Divisions.Clone();
                return Clone();
            }

        }


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        Horoscope h = null;
        DivisionalChart dc = null;
        UserOptions opts = null;
        CuspTransitSearch cs = null;
        Division dtypeRasi = new Division(DivisionType.Rasi);
        BodyName mBody = BodyName.Lagna;
        double ut_lower = 0;
        private ContextMenu mContext;
        private MenuItem menuOptions;
        double ut_higher = 0;
        private MenuItem menuReset;
        private MenuItem menuCopyToClipboard;
        private MenuItem menuCenter;
        private MenuItem menuHalve;
        private MenuItem menuDouble;
        private MenuItem menuItem1;
        private MenuItem menuShadvargas;
        private MenuItem menuSaptavargas;
        private MenuItem menuDasavargas;
        private MenuItem menuShodasavargas;
        private MenuItem menuNadiamsavargas;
        private MenuItem menuDisplaySeconds;
        Moment mOriginal = null;

        public VargaRectificationForm(Horoscope _h, DivisionalChart _dc, Division _dtype)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            if (false == PanchangAppOptions.Instance.VargaRectificationFormSize.IsEmpty)
                Size = PanchangAppOptions.Instance.VargaRectificationFormSize;

            h = _h;
            dc = _dc;
            mOriginal = (Moment)h.Info.tob.Clone();
            cs = new CuspTransitSearch(h);
            PopulateOptionsInit(_dtype);
            //this.PopulateOptions();
            PopulateCache();
            HScroll = true;
            VScroll = true;
            Invalidate();
        }

        private Moment utToMoment(double found_ut)
        {
            // turn into horoscope
            int year = 0, month = 0, day = 0;
            double hour = 0;
            found_ut += h.Info.tz.toDouble() / 24.0;
            Sweph.swe_revjul(found_ut, ref year, ref month, ref day, ref hour);
            Moment m = new Moment(year, month, day, hour);
            return m;
        }
        private double momentToUT(Moment m)
        {
            double local_ut = Sweph.swe_julday(m.Year, m.Month, m.Day, m.Time);
            return local_ut - h.Info.tz.toDouble() / 24.0;
        }
        private void PopulateOptions()
        {
            ut_lower = momentToUT(opts.StartTime);
            ut_higher = momentToUT(opts.EndTime);
        }
        private void PopulateOptionsInit(Division dtype)
        {
            DivisionPosition dp = h.GetPosition(mBody).ToDivisionPosition(dtypeRasi);
            Longitude foundLon = new Longitude(0);
            bool bForward = true;
            ut_lower = cs.TransitSearch(mBody, h.Info.tob, false, new Longitude(dp.CuspLower), foundLon, ref bForward);
            ut_higher = cs.TransitSearch(mBody, h.Info.tob, true, new Longitude(dp.CuspHigher), foundLon, ref bForward);


            double ut_span = (ut_higher - ut_lower) / Basics.NumPartsInDivision(dtype) * 5.0;
            double ut_curr = h.baseUT;
            ut_lower = ut_curr - ut_span / 2.0;
            ut_higher = ut_curr + ut_span / 2.0;

            //double ut_extra = (ut_higher-ut_lower)*(1.0/3.0);
            //ut_lower -= ut_extra;
            //ut_higher += ut_extra;


            //ut_lower = h.baseUT - 1.0/24.0;
            //ut_higher = h.baseUT + 1.0/24.0;
            opts = new UserOptions(utToMoment(ut_lower), utToMoment(ut_higher), dtype);
        }
        private double[][] momentCusps = null;
        private ZodiacHouseName[][] zhCusps = null;
        private void PopulateCache()
        {
            momentCusps = new double[opts.Divisions.Length][];
            zhCusps = new ZodiacHouseName[opts.Divisions.Length][];
            for (int i = 0; i < opts.Divisions.Length; i++)
            {
                Division dtype = opts.Divisions[i];
                ArrayList al = new ArrayList();
                ArrayList zal = new ArrayList();
                //Console.WriteLine ("Calculating cusps for {0} between {1} and {2}", 
                //	dtype, this.utToMoment(ut_lower), this.utToMoment(ut_higher));
                double ut_curr = ut_lower - 1.0 / (24.0 * 60.0);

                Sweph.ObtainLock(h);
                BodyPosition bp = Basics.CalculateSingleBodyPosition(ut_curr, Sweph.BodyNameToSweph(mBody), mBody,
                    BodyType.Name.Graha, h);
                Sweph.ReleaseLock(h);
                //BodyPosition bp = (BodyPosition)h.getPosition(mBody).Clone();
                //DivisionPosition dp = bp.toDivisionPosition(this.dtypeRasi);

                DivisionPosition dp = bp.ToDivisionPosition(dtype);

                //Console.WriteLine ("Longitude at {0} is {1} as is in varga rasi {2}",
                //	this.utToMoment(ut_curr), bp.longitude, dp.zodiac_house.value);

                //bp.longitude = new Longitude(dp.cusp_lower - 0.2);
                //dp = bp.toDivisionPosition(dtype);

                while (true)
                {
                    Moment m = utToMoment(ut_curr);
                    Longitude foundLon = new Longitude(0);
                    bool bForward = true;

                    //Console.WriteLine ("    Starting search at {0}", this.utToMoment(ut_curr));

                    ut_curr = cs.TransitSearch(mBody, utToMoment(ut_curr), true,
                        new Longitude(dp.CuspHigher), foundLon, ref bForward);

                    bp.Longitude = new Longitude(dp.CuspHigher + 0.1);
                    dp = bp.ToDivisionPosition(dtype);

                    if (ut_curr >= ut_lower && ut_curr <= ut_higher + 1.0 / (24.0 * 60.0 * 60.0) * 5.0)
                    {
                        //	Console.WriteLine ("{0}: {1} at {2}",
                        //		dtype, foundLon, this.utToMoment(ut_curr));
                        al.Add(ut_curr);
                        zal.Add(dp.ZodiacHouse.Value);
                    }
                    else if (ut_curr > ut_higher)
                    {
                        //	Console.WriteLine ("- {0}: {1} at {2}",
                        //		dtype, foundLon, this.utToMoment(ut_curr));						
                        break;
                    }
                    ut_curr += 1.0 / (24.0 * 60.0 * 60.0) * 5.0;
                }
                momentCusps[i] = (double[])al.ToArray(typeof(double));
                zhCusps[i] = (ZodiacHouseName[])zal.ToArray(typeof(ZodiacHouseName));
            }


            //for (int i=0; i<opts.Divisions.Length; i++)
            //{
            //	for (int j=0; j<momentCusps[i].Length; j++)
            //	{
            //		Console.WriteLine ("Cusp for {0} at {1}", opts.Divisions[i], momentCusps[i][j]);
            //	}
            //}

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mContext = new ContextMenu();
            menuOptions = new MenuItem();
            menuReset = new MenuItem();
            menuCenter = new MenuItem();
            menuHalve = new MenuItem();
            menuDouble = new MenuItem();
            menuDisplaySeconds = new MenuItem();
            menuItem1 = new MenuItem();
            menuShadvargas = new MenuItem();
            menuSaptavargas = new MenuItem();
            menuDasavargas = new MenuItem();
            menuShodasavargas = new MenuItem();
            menuNadiamsavargas = new MenuItem();
            menuCopyToClipboard = new MenuItem();
            // 
            // mContext
            // 
            mContext.MenuItems.AddRange(new MenuItem[] {
                                                                                     menuOptions,
                                                                                     menuReset,
                                                                                     menuCenter,
                                                                                     menuHalve,
                                                                                     menuDouble,
                                                                                     menuDisplaySeconds,
                                                                                     menuItem1,
                                                                                     menuCopyToClipboard});
            // 
            // menuOptions
            // 
            menuOptions.Index = 0;
            menuOptions.Text = "Options";
            menuOptions.Click += new EventHandler(menuOptions_Click);
            // 
            // menuReset
            // 
            menuReset.Index = 1;
            menuReset.Text = "Reset Original Time";
            menuReset.Click += new EventHandler(menuReset_Click);
            // 
            // menuCenter
            // 
            menuCenter.Index = 2;
            menuCenter.Text = "Center to Current Time";
            menuCenter.Click += new EventHandler(menuCenter_Click);
            // 
            // menuHalve
            // 
            menuHalve.Index = 3;
            menuHalve.Text = "Zoom In";
            menuHalve.Click += new EventHandler(menuHalve_Click);
            // 
            // menuDouble
            // 
            menuDouble.Index = 4;
            menuDouble.Text = "Zoom Out";
            menuDouble.Click += new EventHandler(menuDouble_Click);
            // 
            // menuDisplaySeconds
            // 
            menuDisplaySeconds.Index = 5;
            menuDisplaySeconds.Text = "Display Seconds";
            menuDisplaySeconds.Click += new EventHandler(menuDisplaySeconds_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 6;
            menuItem1.MenuItems.AddRange(new MenuItem[] {
                                                                                      menuShadvargas,
                                                                                      menuSaptavargas,
                                                                                      menuDasavargas,
                                                                                      menuShodasavargas,
                                                                                      menuNadiamsavargas});
            menuItem1.Text = "Show Vargas";
            // 
            // menuShadvargas
            // 
            menuShadvargas.Index = 0;
            menuShadvargas.Text = "Shadvargas";
            menuShadvargas.Click += new EventHandler(menuShadvargas_Click);
            // 
            // menuSaptavargas
            // 
            menuSaptavargas.Index = 1;
            menuSaptavargas.Text = "Saptavargas";
            menuSaptavargas.Click += new EventHandler(menuSaptavargas_Click);
            // 
            // menuDasavargas
            // 
            menuDasavargas.Index = 2;
            menuDasavargas.Text = "Dasavargas";
            menuDasavargas.Click += new EventHandler(menuDasavargas_Click);
            // 
            // menuShodasavargas
            // 
            menuShodasavargas.Index = 3;
            menuShodasavargas.Text = "Shodasavargas";
            menuShodasavargas.Click += new EventHandler(menuShodasavargas_Click);
            // 
            // menuNadiamsavargas
            // 
            menuNadiamsavargas.Index = 4;
            menuNadiamsavargas.Text = "Nadiamsa vargas";
            menuNadiamsavargas.Click += new EventHandler(menuNadiamsavargas_Click);
            // 
            // menuCopyToClipboard
            // 
            menuCopyToClipboard.Index = 7;
            menuCopyToClipboard.Text = "Copy To Clipboard";
            menuCopyToClipboard.Click += new EventHandler(menuCopyToClipboard_Click);
            // 
            // VargaRectificationForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScaleBaseSize = new Size(5, 13);
            AutoScroll = true;
            ClientSize = new Size(512, 142);
            ContextMenu = mContext;
            Name = "VargaRectificationForm";
            Text = "Lagna Based Rectification Helper";
            Resize += new EventHandler(VargaRectificationForm_Resize);
            Click += new EventHandler(VargaRectificationForm_Click);
            Load += new EventHandler(VargaRectificationForm_Load);
            DoubleClick += new EventHandler(VargaRectificationForm_DoubleClick);
            Paint += new PaintEventHandler(VargaRectificationForm_Paint);

        }
        #endregion

        private void VargaRectificationForm_Load(object sender, EventArgs e)
        {

        }

        int vname_width = 50;
        int unit_height = 30;
        int half_tick_height = 3;

        private void Draw(Graphics g)
        {
            Font f_time = PanchangAppOptions.Instance.GeneralFont;
            Pen p_black = new Pen(Brushes.Black);
            Pen p_lgray = new Pen(Brushes.LightGray);
            Pen p_orange = new Pen(Brushes.DarkOrange);
            Pen p_red = new Pen(Brushes.DarkRed);

            //int bar_width = this.Width - vname_width*2;
            int bar_width = zoomWidth - vname_width * 2;
            float x_offset = 0;
            string s;
            SizeF sz;

            g.Clear(Color.AliceBlue);

            x_offset = (float)((momentToUT(mOriginal) - ut_lower) / (ut_higher - ut_lower) * bar_width) + vname_width;
            g.DrawLine(p_lgray, x_offset, unit_height / 2, x_offset, opts.Divisions.Length * unit_height + unit_height / 2);


            x_offset = (float)((h.baseUT - ut_lower) / (ut_higher - ut_lower) * bar_width) + vname_width;
            float y_max = opts.Divisions.Length * unit_height + unit_height / 2;
            g.DrawLine(p_red, x_offset, unit_height / 2, x_offset, y_max);
            Moment mNow = utToMoment(h.baseUT);
            s = mNow.ToTimeString(menuDisplaySeconds.Checked);
            sz = g.MeasureString(s, f_time);
            g.DrawString(s, f_time, Brushes.DarkRed, x_offset - sz.Width / 2, y_max);



            for (int iVarga = 0; iVarga < opts.Divisions.Length; iVarga++)
            {
                int varga_y = (iVarga + 1) * unit_height;
                g.DrawLine(p_black, vname_width, varga_y, vname_width + bar_width, varga_y);
                s = string.Format("D-{0}", Basics.NumPartsInDivision(opts.Divisions[iVarga]));
                sz = g.MeasureString(s, f_time);
                g.DrawString(s, f_time, Brushes.Gray, 4, varga_y - sz.Height / 2);


                float old_x_offset = 0;
                for (int j = 0; j < momentCusps[iVarga].Length; j++)
                {
                    double ut_curr = momentCusps[iVarga][j];
                    double perc = (ut_curr - ut_lower) / (ut_higher - ut_lower) * 100.0;
                    //Console.WriteLine ("Varga {0}, perc {1}", opts.Divisions[iVarga], perc);
                    x_offset = (float)((ut_curr - ut_lower) / (ut_higher - ut_lower) * bar_width) + vname_width;

                    //(float)((ut_curr-ut_lower)/(ut_higher/ut_lower)*bar_width);
                    Moment m = utToMoment(ut_curr);
                    s = string.Format("{0} {1}",
                        m.ToTimeString(menuDisplaySeconds.Checked),
                        ZodiacHouse.ToShortString(zhCusps[iVarga][j]));
                    sz = g.MeasureString(s, f_time);
                    if (old_x_offset + sz.Width < x_offset)
                    {
                        g.DrawLine(p_black, x_offset, varga_y - half_tick_height, x_offset, varga_y + half_tick_height);
                        g.DrawString(s, f_time, Brushes.Gray,
                            x_offset - sz.Width / 2, varga_y - sz.Height - half_tick_height);
                        //							x_offset-(sz.Width/2), varga_y-sz.Height-half_tick_height);
                        old_x_offset = x_offset;

                        //						s = zhCusps[iVarga][j].ToString();
                        //						sz = g.MeasureString(s, f_time);
                        //						g.DrawString(s, f_time, Brushes.Black,
                        //							x_offset, varga_y - sz.Height);

                    }
                    else
                    {
                        g.DrawLine(p_orange, x_offset, varga_y - half_tick_height, x_offset, varga_y + half_tick_height);
                    }
                }
            }




            //this.Height);
        }

        Bitmap bmpBuffer = null;
        int zoomWidth = 0;
        int zoomHeight = 0;
        private void VargaRectificationForm_Paint(object sender, PaintEventArgs e)
        {
            zoomHeight = (opts.Divisions.Length + 1) * unit_height + unit_height / 2;

            if (bmpBuffer != null &&
                bmpBuffer.Width == zoomWidth &&
                bmpBuffer.Height == zoomHeight)
            {
                e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                e.Graphics.DrawImage(bmpBuffer, 0, 0);
                return;
            }

            zoomWidth = Width;
            Graphics displayGraphics = CreateGraphics();
            bmpBuffer = new Bitmap(zoomWidth, zoomHeight, displayGraphics);
            Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            Draw(imageGraphics);
            displayGraphics.Dispose();
            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
            e.Graphics.DrawImage(bmpBuffer, 0, 0);

            AutoScrollMinSize = new Size(zoomWidth, zoomHeight);
        }

        private void VargaRectificationForm_Resize(object sender, EventArgs e)
        {
            PanchangAppOptions.Instance.VargaRectificationFormSize = Size;
            bmpBuffer = null;
            Invalidate();
        }

        public object SetOptions(object _uo)
        {
            //UserOptions uo = (UserOptions)_uo;
            //opts.StartTime = uo.StartTime;
            //opts.EndTime = uo.EndTime;

            object ret = opts.CopyFrom(_uo);
            PopulateOptions();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
            return ret;
        }
        private void menuOptions_Click(object sender, EventArgs e)
        {
            new Options(opts, new ApplyOptions(SetOptions)).ShowDialog();
        }

        private void VargaRectificationForm_DoubleClick(object sender, EventArgs e)
        {
            Point pt = PointToClient(MousePosition);

            double click_width = pt.X - (double)vname_width;
            //double bar_width = (double)(1000 - vname_width*2);
            double bar_width = Width - vname_width * 2;
            double perc = click_width / bar_width;

            if (perc < 0 && perc > 100)
                return;

            double ut_new = ut_lower + (ut_higher - ut_lower) * perc;
            Moment mNew = utToMoment(ut_new);
            h.Info.tob = mNew;
            h.OnChanged();
            bmpBuffer = null;
            Invalidate();

            //Console.WriteLine ("Click at {0}. Width at {1}. Percentage is {2}", 
            //	click_width, bar_width, perc);


        }

        private void menuReset_Click(object sender, EventArgs e)
        {
            h.Info.tob = (Moment)mOriginal.Clone();
            h.OnChanged();
            bmpBuffer = null;
            Invalidate();
        }

        private void VargaRectificationForm_Click(object sender, EventArgs e)
        {
            //this.VargaRectificationForm_DoubleClick(sender,e);
        }

        private void menuCopyToClipboard_Click(object sender, EventArgs e)
        {
            //Graphics displayGraphics = this.CreateGraphics();
            //Bitmap bmpBuffer = new Bitmap(this.Width, this.Height, displayGraphics);
            //Graphics imageGraphics = Graphics.FromImage(bmpBuffer);
            //this.Draw(imageGraphics);
            //displayGraphics.Dispose();

            Clipboard.SetDataObject(bmpBuffer, true);
        }

        private void UpdateOptsFromUT()
        {
            opts.StartTime = utToMoment(ut_lower);
            opts.EndTime = utToMoment(ut_higher);
        }
        private void menuCenter_Click(object sender, EventArgs e)
        {
            double ut_half = (ut_higher - ut_lower) / 2.0;
            double ut_curr = momentToUT(h.Info.tob);
            ut_lower = ut_curr - ut_half;
            ut_higher = ut_curr + ut_half;
            UpdateOptsFromUT();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuHalve_Click(object sender, EventArgs e)
        {
            double ut_curr = momentToUT(h.Info.tob);
            double ut_quarter = (ut_higher - ut_lower) / 4.0;
            ut_lower = ut_curr - ut_quarter;
            ut_higher = ut_curr + ut_quarter;
            UpdateOptsFromUT();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuDouble_Click(object sender, EventArgs e)
        {
            double ut_curr = momentToUT(h.Info.tob);
            double ut_half = ut_higher - ut_lower;
            ut_lower = ut_curr - ut_half;
            ut_higher = ut_curr + ut_half;
            UpdateOptsFromUT();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuShadvargas_Click(object sender, EventArgs e)
        {
            //TODO: Recheck the typecasting
            opts.Divisions = (Division[])Basics.Shadvargas();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuSaptavargas_Click(object sender, EventArgs e)
        {
            //TODO: Recheck the typecasting
            opts.Divisions = (Division[])Basics.Saptavargas();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuDasavargas_Click(object sender, EventArgs e)
        {
            //TODO: Recheck the typecasting
            opts.Divisions = (Division[])Basics.Dasavargas();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuShodasavargas_Click(object sender, EventArgs e)
        {
            //TODO: Recheck the typecasting
            opts.Divisions = (Division[])Basics.Shodasavargas();
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuNadiamsavargas_Click(object sender, EventArgs e)
        {
            Division[] divs_shod = (Division[])Basics.Shodasavargas();
            Division[] divs = new Division[divs_shod.Length + 1];
            divs_shod.CopyTo(divs, 0);
            divs[divs_shod.Length] = new Division(DivisionType.NadiamsaCKN);
            opts.Divisions = divs;
            PopulateCache();
            bmpBuffer = null;
            Invalidate();
        }

        private void menuDisplaySeconds_Click(object sender, EventArgs e)
        {
            menuDisplaySeconds.Checked = !menuDisplaySeconds.Checked;
            bmpBuffer = null;
            Invalidate();
        }
    }
}
