using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for KeyInfoControl.
    /// </summary>
    public class KeyInfoControl : BaseControl
    {
        private ListView mList;
        private ColumnHeader Key;
        private ColumnHeader Info;
        private ContextMenu mKeyInfoMenu;
        private MenuItem menuItem1;
        private MenuItem menuItem2;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public KeyInfoControl(Horoscope _h)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            horoscope = _h;
            horoscope.Changed += new EvtChanged(OnRecalculate);
            PanchangAppOptions.DisplayPrefsChanged += new EvtChanged(OnRedisplay);
            Repopulate();
            AddViewsToContextMenu(mKeyInfoMenu);

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
            mList = new ListView();
            Key = new ColumnHeader();
            Info = new ColumnHeader();
            mKeyInfoMenu = new ContextMenu();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            SuspendLayout();
            // 
            // mList
            // 
            mList.AllowColumnReorder = true;
            mList.BackColor = Color.Lavender;
            mList.Columns.AddRange(new ColumnHeader[] {
                                                                                    Key,
                                                                                    Info});
            mList.ContextMenu = mKeyInfoMenu;
            mList.Dock = DockStyle.Fill;
            mList.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mList.ForeColor = SystemColors.HotTrack;
            mList.FullRowSelect = true;
            mList.Location = new Point(0, 0);
            mList.Name = "mList";
            mList.Size = new Size(496, 240);
            mList.TabIndex = 0;
            mList.View = View.Details;
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // Key
            // 
            Key.Text = "Key";
            Key.Width = 136;
            // 
            // Info
            // 
            Info.Text = "Info";
            Info.Width = 350;
            // 
            // mKeyInfoMenu
            // 
            mKeyInfoMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                         menuItem1,
                                                                                         menuItem2});
            // 
            // menuItem1
            // 
            menuItem1.Index = 0;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 1;
            menuItem2.Text = "-";
            // 
            // KeyInfoControl
            // 
            Controls.Add(mList);
            Name = "KeyInfoControl";
            Size = new Size(496, 240);
            ResumeLayout(false);

        }
        #endregion

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected override void copyToClipboard()
        {
            int iMaxDescLength = 0;
            for (int i = 0; i < mList.Items.Count; i++)
                iMaxDescLength = Math.Max(mList.Items[i].Text.Length, iMaxDescLength);
            iMaxDescLength += 2;

            string s = "Key Info: " + "\r\n\r\n";

            for (int i = 0; i < mList.Items.Count; i++)
            {
                ListViewItem li = mList.Items[i];
                s += li.Text.PadRight(iMaxDescLength, ' ');
                s += "- ";
                for (int j = 1; j < li.SubItems.Count; j++)
                {
                    s += li.SubItems[j].Text;
                }
                s += "\r\n";
            }
            Clipboard.SetDataObject(s);
        }

        void Repopulate()
        {
            string[] weekdays = new string[]
            {
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
            };

            mList.Items.Clear();

            ListViewItem li;

            li = new ListViewItem("Date of Birth");
            li.SubItems.Add(horoscope.Info.tob.ToString());
            mList.Items.Add(li);

            li = new ListViewItem("Time Zone");
            li.SubItems.Add(horoscope.Info.tz.ToString());
            mList.Items.Add(li);

            li = new ListViewItem("Latitude");
            li.SubItems.Add(horoscope.Info.lat.ToString());
            mList.Items.Add(li);

            li = new ListViewItem("Longitude");
            li.SubItems.Add(horoscope.Info.lon.ToString());
            mList.Items.Add(li);

            li = new ListViewItem("Altitude");
            li.SubItems.Add(horoscope.Info.alt.ToString());
            mList.Items.Add(li);

            {
                HMSInfo hms_srise = new HMSInfo(horoscope.Sunrise);
                li = new ListViewItem("Sunrise");
                string fmt = string.Format("{0:00}:{1:00}:{2:00}",
                    hms_srise.degree, hms_srise.minute, hms_srise.second);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                HMSInfo hms_sset = new HMSInfo(horoscope.Sunset);
                li = new ListViewItem("Sunset");
                string fmt = string.Format("{0:00}:{1:00}:{2:00}",
                    hms_sset.degree, hms_sset.minute, hms_sset.second);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Weekday");
                string fmt = string.Format("{0}", horoscope.Weekday);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);

            }
            {
                Longitude ltithi = horoscope.GetPosition(BodyName.Moon).Longitude.Subtract(horoscope.GetPosition(BodyName.Sun).Longitude);
                double offset = 360.0 / 30.0 - ltithi.ToTithiOffset();
                Tithi ti = ltithi.ToTithi();
                BodyName tiLord = ti.GetLord();
                li = new ListViewItem("Tithi");
                string fmt = string.Format("{0} ({1}) {2:N}% left", ti.ToString(), tiLord, offset / 12.0 * 100);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                Longitude lmoon = horoscope.GetPosition(BodyName.Moon).Longitude;
                Nakshatra nmoon = lmoon.ToNakshatra();
                BodyName nmoonLord = VimsottariDasa.LordOfNakshatraS(nmoon);
                double offset = 360.0 / 27.0 - lmoon.ToNakshatraOffset();
                int pada = lmoon.ToNakshatraPada();
                string fmt = string.Format("{0} {1} ({2}) {3:N}% left",
                    nmoon.Value, pada, nmoonLord, offset / (360.0 / 27.0) * 100);
                li = new ListViewItem("Nakshatra");
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Karana");
                Longitude lkarana = horoscope.GetPosition(BodyName.Moon).Longitude.Subtract(horoscope.GetPosition(BodyName.Sun).Longitude);
                double koffset = 360.0 / 60.0 - lkarana.ToKaranaOffset();
                Karana k = lkarana.ToKarana();
                BodyName kLord = k.GetLord();
                string fmt = string.Format("{0} ({1}) {2:N}% left", k.value, kLord, koffset / 6.0 * 100);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Yoga");
                Longitude smLon = horoscope.GetPosition(BodyName.Sun).Longitude.Add(horoscope.GetPosition(BodyName.Moon).Longitude);
                double offset = 360.0 / 27.0 - smLon.ToSunMoonYogaOffset();
                SunMoonYoga smYoga = smLon.ToSunMoonYoga();
                BodyName smLord = smYoga.GetLord();
                string fmt = string.Format("{0} ({1}) {2:N}% left", smYoga, smLord, offset / (360.0 / 27.0) * 100);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Hora");
                BodyName b = horoscope.CalculateHora();
                string fmt = string.Format("{0}", b);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Kala");
                BodyName b = horoscope.CalculateKala();
                string fmt = string.Format("{0}", b);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("Muhurta");
                int mIndex = (int)(Math.Floor(horoscope.HoursAfterSunrise() / horoscope.LengthOfDay() * 30.0) + 1);
                Muhurta m = (Muhurta)mIndex;
                string fmt = string.Format("{0} ({1})", m, Basics.NakLordOfMuhurta(m));
                li.SubItems.Add(fmt);
                mList.Items.Add(li);

            }
            {
                double ghatisSr = horoscope.HoursAfterSunrise() * 2.5;
                double ghatisSs = horoscope.HoursAfterSunRiseSet() * 2.5;
                li = new ListViewItem("Ghatis");
                string fmt = string.Format("{0:0.0000} / {1:0.0000}", ghatisSr, ghatisSs);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                int vgOff = (int)Math.Ceiling(horoscope.HoursAfterSunRiseSet() * 150.0);
                vgOff = vgOff % 9;
                if (vgOff == 0) vgOff = 9;
                BodyName b = (BodyName)((int)BodyName.Sun + vgOff - 1);
                li = new ListViewItem("Vighatika Graha");
                string fmt = string.Format("{0}", b);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
            }
            {
                li = new ListViewItem("LMT Offset");
                double e = horoscope.LMT_Offset;
                double orig_e = e;
                e = e < 0 ? -e : e;
                e *= 24.0;
                int hour = (int)Math.Floor(e);
                e = (e - Math.Floor(e)) * 60.0;
                int min = (int)Math.Floor(e);
                e = (e - Math.Floor(e)) * 60.0;
                string prefix = "";
                if (orig_e < 0) prefix = "-";
                string fmt = string.Format("{0}{1:00}:{2:00}:{3:00.00}",
                    prefix, hour, min, e);
                string fmt2 = string.Format(" ({0:00.00} minutes)", horoscope.LMT_Offset * 24.0 * 60.0);
                li.SubItems.Add(fmt + fmt2);
                mList.Items.Add(li);
            }
            {
                Sweph.Lock(horoscope);
                li = new ListViewItem("Ayanamsa");
                double aya = Sweph.SWE_GetAyanamsaUniversalTime(horoscope.BaseUT);
                int aya_hour = (int)Math.Floor(aya);
                aya = (aya - Math.Floor(aya)) * 60.0;
                int aya_min = (int)Math.Floor(aya);
                aya = (aya - Math.Floor(aya)) * 60.0;
                string fmt = string.Format("{0:00}-{1:00}-{2:00.00}", aya_hour, aya_min, aya);
                li.SubItems.Add(fmt);
                mList.Items.Add(li);
                Sweph.Unlock(horoscope);
            }
            {
                li = new ListViewItem("Universal Time");
                li.SubItems.Add(horoscope.BaseUT.ToString());
                mList.Items.Add(li);
            }


            ColorAndFontRows(mList);
        }
        void OnRedisplay(object o)
        {
            ColorAndFontRows(mList);
        }

        void OnRecalculate(object o)
        {
            Repopulate();
        }

    }
}
