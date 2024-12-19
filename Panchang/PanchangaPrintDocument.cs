

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for PanchangaPrintDocument.
    /// </summary>
    public class PanchangaPrintDocument : PrintDocument
    {
        private PanchangaGlobalMoments globals = null;
        private ArrayList locals = null;
        private Horoscope h = null;
        private PanchangaControl.UserOptions opts = null;

        public bool bPrintPanchanga = true;
        public bool bPrintLagna = false;

        public PanchangaPrintDocument(
            PanchangaControl.UserOptions _opts,
            Horoscope _h, PanchangaGlobalMoments _globals, ArrayList _locals)
        {
            h = _h;
            opts = _opts;
            globals = _globals;
            locals = _locals;

            if (locals.Count > 0 &&
                ((PanchangaLocalMoments)locals[0]).LagnasUT.Count > 1)
                bPrintLagna = true;
        }

        Font f = GlobalOptions.Instance.GeneralFont;
        Font f_u = new Font(GlobalOptions.Instance.GeneralFont.FontFamily,
            GlobalOptions.Instance.GeneralFont.SizeInPoints, FontStyle.Underline);
        int local_index = 0;

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);

            divisional_chart_size = 250;
            time_width = 43;

            day_width = 65;
            wday_width = 25;
            sunrise_width = time_width;
            sunset_width = time_width;
            tithi_name_width = 75;
            tithi_time_width = time_width;
            karana_name_width = 85;
            karana_time_width = time_width;
            nak_name_width = 70;
            nak_time_width = time_width;
            sm_name_width = 80;
            sm_time_width = time_width;
            rahu_kala_width = time_width * 2 + 10;

            day_offset = 0;
            wday_offset = day_width;
            sunrise_offset = wday_offset + wday_width;
            sunset_offset = sunrise_offset + sunrise_width;

            nak_name_offset = sunset_offset + sunset_width;
            nak_time_offset = nak_name_offset + nak_name_width;
            tithi_name_offset = nak_time_offset + nak_time_width;
            tithi_time_offset = tithi_name_offset + tithi_name_width;
            karana_name_1_offset = tithi_time_offset + tithi_time_width;
            karana_time_1_offset = karana_name_1_offset + karana_name_width;
            karana_name_2_offset = karana_time_1_offset + karana_time_width;
            karana_time_2_offset = karana_name_2_offset + karana_name_width;
            sm_name_offset = karana_time_2_offset + karana_time_width;
            sm_time_offset = sm_name_offset + sm_name_width;
            rahu_kala_offset = sm_time_offset + sm_time_width;
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
        }
        private void checkForMorePages(PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            if (bPrintLagna == false &&
                bPrintPanchanga == false)
                e.HasMorePages = false;
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            if (bPrintPanchanga)
            {
                PrintFirstPage(e);
            }
            else if (bPrintLagna)
            {
                PrintLagna(e);
            }

            checkForMorePages(e);
        }

        int margin_offset = 30;
        int header_offset = 30;
        Brush b = Brushes.Black;
        Pen p = Pens.Black;

        int time_width;
        int day_offset;
        int day_width;
        int wday_offset;
        int wday_width;
        int sunrise_offset;
        int sunrise_width;
        int sunset_offset;
        int sunset_width;
        int tithi_name_offset;
        int tithi_name_width;
        int tithi_time_offset;
        int tithi_time_width;
        int karana_name_1_offset;
        int karana_name_width;
        int karana_time_1_offset;
        int karana_time_width;
        int karana_name_2_offset;
        int karana_time_2_offset;
        int nak_name_offset;
        int nak_name_width;
        int nak_time_offset;
        int nak_time_width;
        int sm_name_offset;
        int sm_name_width;
        int sm_time_offset;
        int sm_time_width;
        int rahu_kala_offset;
        int rahu_kala_width;
        int divisional_chart_size;

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
        private string utTimeToString(double ut_event, double ut_sr, double sunrise)
        {
            Moment m = utToMoment(ut_event);
            HMSInfo hms = new HMSInfo(m.Time);

            if (ut_event >= ut_sr - sunrise / 24.0 + 1.0)
            {
                if (false == opts.LargeHours)
                    return string.Format("*{0}:{1:00}", hms.degree, hms.minute);
                else
                    return string.Format("{0:00}:{1:00}", hms.degree + 24, hms.minute);
            }
            return string.Format("{0:00}:{1:00}", hms.degree, hms.minute);
        }
        private void PrintLagna(PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            Graphics g = e.Graphics;
            g.ResetTransform();
            g.TranslateTransform(100, header_offset);

            for (int j = 1; j <= 12; j++)
            {
                ZodiacHouse zh = new ZodiacHouse((ZodiacHouseName)j);
                g.DrawString(zh.Value.ToString(), f, b,
                    day_offset + 100 + (int)zh.Value * time_width, 0);
            }
            g.TranslateTransform(0, f.Height);

            int i = local_index;
            while (i < locals.Count)
            {
                PanchangaLocalMoments local = (PanchangaLocalMoments)locals[i];
                Moment m_sunrise = new Moment(local.SunriseUT, h);
                g.DrawString(m_sunrise.ToString(), f, b, day_offset, 0);

                for (int j = 0; j < local.LagnasUT.Count; j++)
                {
                    PanchangaMomentInfo pmi = (PanchangaMomentInfo)local.LagnasUT[j];
                    //Moment m_lagna = new Moment(pmi.ut, h);
                    ZodiacHouse zh = new ZodiacHouse((ZodiacHouseName)pmi.Info);
                    zh = zh.Add(12);
                    Font _f = f;

                    if (local.LagnaZodiacHouse == zh.Value)
                        _f = f_u;

                    g.DrawString(
                        utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise),
                        _f, b,
                        day_offset + 100 + (int)zh.Value * time_width, 0);
                }

                local_index = ++i;
                g.TranslateTransform(0, f.Height);
                if (g.Transform.OffsetY > e.PageBounds.Height - header_offset)
                    return;

            }

            bPrintLagna = false;
            e.HasMorePages = false;

        }
        private void PrintTitle(Graphics g, int left, int right, string s)
        {
            SizeF sz = g.MeasureString(s, f);
            g.DrawString(s, f, b, left + (right - left - sz.Width) / 2, 0);
        }
        private void PrintFirstPage(PrintPageEventArgs e)
        {

            e.HasMorePages = true;
            Graphics g = e.Graphics;
            g.ResetTransform();
            g.TranslateTransform(margin_offset, header_offset);

            PrintTitle(g, 0, wday_offset + wday_width, "Date/Day");
            PrintTitle(g, sunrise_offset, sunset_offset + sunset_width, "Sunrise/set");
            PrintTitle(g, nak_name_offset, nak_time_offset + nak_time_width, "Nakshatra");
            PrintTitle(g, tithi_name_offset, tithi_time_offset + tithi_time_width, "Tithi");
            PrintTitle(g, karana_name_1_offset, karana_time_2_offset + karana_time_width, "Karana");
            PrintTitle(g, sm_name_offset, sm_time_offset + sm_time_width, "SM-Yoga");

            g.TranslateTransform(0, (float)(f.Height * 1.5));

            int iStart = local_index;
            int i = local_index;
            while (i < locals.Count)
            {
                int numLines = 1;
                PanchangaLocalMoments local = (PanchangaLocalMoments)locals[i];
                Moment m_sunrise = new Moment(local.SunriseUT, h);
                Moment m_sunset = new Moment(0, 0, 0, local.Sunset);

                g.DrawString(m_sunrise.ToShortDateString(), f, b, day_offset, 0);
                g.DrawString(Basics.WeekdayToShortString(local.WeekDay), f, b, wday_offset, 0);

                if (opts.ShowSunriset)
                {
                    g.DrawString(m_sunrise.ToTimeString(), f, b, sunrise_offset, 0);
                    g.DrawString(m_sunset.ToTimeString(), f, b, sunset_offset, 0);
                }

                int numTithis = local.TithiIndexEnd - local.TithiIndexStart;
                int numNaks = local.TithiIndexEnd - local.TithiIndexStart;
                int numSMYogas = local.TithiIndexEnd - local.TithiIndexStart;
                int numKaranas = local.TithiIndexEnd - local.TithiIndexStart;

                if (opts.CalcTithiCusps)
                {
                    numLines = Math.Max(numLines, numTithis);
                    for (int j = 0; j < numTithis; j++)
                    {
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.TithisUT[local.TithiIndexStart + 1 + j];
                        Tithi t = new Tithi((TithiName)pmi.Info);
                        Moment mTithi = new Moment(pmi.UT, h);
                        g.DrawString(t.ToUnqualifiedString(), f, b, tithi_name_offset, j * f.Height);
                        g.DrawString(utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise),
                            f, b, tithi_time_offset, j * f.Height);
                    }
                }

                if (opts.CalcKaranaCusps)
                {
                    numLines = Math.Max(numLines, (int)Math.Ceiling(numKaranas / 2.0));
                    for (int j = 0; j < numKaranas; j++)
                    {
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.KaranasUT[local.KaranaIndexStart + 1 + j];
                        Karana k = new Karana((KaranaName)pmi.Info);
                        Moment mKarana = new Moment(pmi.UT, h);
                        int jRow = (int)Math.Floor((decimal)j / 2);
                        int name_offset = karana_name_1_offset;
                        int time_offset = karana_time_1_offset;
                        if (j % 2 == 1)
                        {
                            name_offset = karana_name_2_offset;
                            time_offset = karana_time_2_offset;
                        }

                        g.DrawString(k.value.ToString(), f, b, name_offset, jRow * f.Height);
                        g.DrawString(utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise),
                            f, b, time_offset, jRow * f.Height);
                    }
                }

                if (opts.CalcNakCusps)
                {
                    numLines = Math.Max(numLines, numNaks);
                    for (int j = 0; j < numNaks; j++)
                    {
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.NakshatrasUT[local.NakshatraIndexStart + 1 + j];
                        Nakshatra n = new Nakshatra((NakshatraName)pmi.Info);
                        Moment mNak = new Moment(pmi.UT, h);
                        g.DrawString(n.ToString(), f, b, nak_name_offset, j * f.Height);
                        g.DrawString(utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise),
                            f, b, nak_time_offset, j * f.Height);
                    }
                }

                if (opts.CalcSMYogaCusps)
                {
                    numLines = Math.Max(numLines, numSMYogas);
                    for (int j = 0; j < numSMYogas; j++)
                    {
                        PanchangaMomentInfo pmi = (PanchangaMomentInfo)globals.SunMoonYogasUT[local.SunMoonYogaIndexStart + 1 + j];
                        SunMoonYoga sm = new SunMoonYoga((SunMoonYogaName)pmi.Info);
                        Moment mSMYoga = new Moment(pmi.UT, h);
                        g.DrawString(sm.Value.ToString(), f, b, sm_name_offset, j * f.Height);
                        g.DrawString(utTimeToString(pmi.UT, local.SunriseUT, local.Sunrise),
                            f, b, sm_time_offset, j * f.Height);
                    }
                }


#if DND

				string s_rahu_kala = string.Format("{0} - {1}", 
					this.utTimeToString(local.kalas_ut[local.rahu_kala_index], local.sunrise_ut, local.sunrise),
					this.utTimeToString(local.kalas_ut[local.rahu_kala_index+1], local.sunrise_ut, local.sunrise));
				g.DrawString(s_rahu_kala, f, b, rahu_kala_offset, 0);
#endif

                g.TranslateTransform(0, f.Height * numLines);

                local_index = ++i;

                if (g.Transform.OffsetY > e.PageBounds.Height - header_offset - divisional_chart_size)
                    goto first_done;

            }

            bPrintPanchanga = false;
            local_index = 0;

        first_done:
            float offsetY = g.Transform.OffsetY;
            float offsetX = margin_offset + sm_time_offset + sm_time_width;

            Moment mCurr = new Moment(((PanchangaLocalMoments)locals[iStart]).SunriseUT, h);
            HoraInfo hiCurr = new HoraInfo(mCurr, h.Info.lat, h.Info.lon, h.Info.tz);
            Horoscope hCurr = new Horoscope(hiCurr, h.Options);
            DivisionalChart dc = new DivisionalChart(hCurr);
            dc.PrintMode = true;
            dc.options.ViewStyle = EViewStyle.Panchanga;
            dc.SetOptions(dc.options);
            dc.DrawChart(g, divisional_chart_size, divisional_chart_size);

            g.ResetTransform();
            // horizontal top & bottom
            g.DrawLine(p, margin_offset - 5, header_offset - 5, margin_offset + sm_time_offset + sm_time_width + 5, header_offset - 5);
            g.DrawLine(p, margin_offset - 5, header_offset - 5 + f.Height * (float)1.5, margin_offset + sm_time_offset + sm_time_width + 5, header_offset - 5 + f.Height * (float)1.5);
            g.DrawLine(p, margin_offset - 5, offsetY + 5, offsetX + 5, offsetY + 5);
            // vertical left and right
            g.DrawLine(p, margin_offset - 5, header_offset - 5, margin_offset - 5, offsetY + 5);
            g.DrawLine(p, offsetX + 5, header_offset - 5, offsetX + 5, offsetY + 5);

            g.DrawLine(p,
                margin_offset + sunset_offset + sunset_width - 2, header_offset - 5,
                margin_offset + sunset_offset + sunset_width - 2, offsetY + 5);

            g.DrawLine(p,
                margin_offset + tithi_time_offset + tithi_time_width - 2, header_offset - 5,
                margin_offset + tithi_time_offset + tithi_time_width - 2, offsetY + 5);

            g.DrawLine(p,
                margin_offset + nak_time_offset + nak_time_width - 2, header_offset - 5,
                margin_offset + nak_time_offset + nak_time_width - 2, offsetY + 5);

            g.DrawLine(p,
                margin_offset + karana_time_2_offset + karana_time_width - 2, header_offset - 5,
                margin_offset + karana_time_2_offset + karana_time_width - 2, offsetY + 5);





        }



    }
}
