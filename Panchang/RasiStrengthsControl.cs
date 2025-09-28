

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{
    public class RasiStrengthsControl : Form
    {
        private ListView mList;
        private ComboBox cbStrength;
        private IContainer components = null;
        private UserOptions options = null;
        private ComboBox cbRasi1;
        private ComboBox cbRasi2;
        private ContextMenu cMenu;
        private MenuItem menuOptions;
        private Label lVarga;
        private Horoscope h = null;

        class UserOptions : ICloneable
        {
            Division m_dtype;

            [InVisible]
            public Division Division
            {
                get { return m_dtype; }
                set { m_dtype = value; }
            }

            [Visible("Varga")]
            public DivisionType UIDivision
            {
                get { return m_dtype.MultipleDivisions[0].Varga; }
                set { m_dtype = new Division(value); }
            }
            public UserOptions()
            {
                m_dtype = new Division(DivisionType.Rasi);
            }
            public object Clone()
            {
                UserOptions uo = new UserOptions
                {
                    Division = Division
                };
                return uo;
            }
            public object SetOptions(object _uo)
            {
                UserOptions uo = (UserOptions)_uo;
                Division = uo.Division;
                return Clone();
            }
        }

        public RasiStrengthsControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.

            InitializeComponent();

            if (false == PanchangAppOptions.Instance.RasiStrengthsFormSize.IsEmpty)
                Size = PanchangAppOptions.Instance.RasiStrengthsFormSize;

            h = _h;
            h.Changed += new EvtChanged(OnRecalculate);
            options = new UserOptions();
            InitializeComboBoxes();
        }

        public void OnRecalculate(object _h)
        {
            h = (Horoscope)_h;
            Compute();
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
            cbStrength = new ComboBox();
            cbRasi1 = new ComboBox();
            cbRasi2 = new ComboBox();
            mList = new ListView();
            cMenu = new ContextMenu();
            menuOptions = new MenuItem();
            lVarga = new Label();
            SuspendLayout();
            // 
            // cbStrength
            // 
            cbStrength.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            cbStrength.Location = new Point(8, 8);
            cbStrength.Name = "cbStrength";
            cbStrength.Size = new Size(128, 21);
            cbStrength.TabIndex = 0;
            cbStrength.Text = "cbStrength";
            cbStrength.SelectedIndexChanged += new EventHandler(cbStrength_SelectedIndexChanged);
            // 
            // cbRasi1
            // 
            cbRasi1.Location = new Point(8, 40);
            cbRasi1.Name = "cbRasi1";
            cbRasi1.Size = new Size(104, 21);
            cbRasi1.TabIndex = 1;
            cbRasi1.Text = "cbRasi1";
            cbRasi1.SelectedIndexChanged += new EventHandler(cbGraha1_SelectedIndexChanged);
            // 
            // cbRasi2
            // 
            cbRasi2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbRasi2.Location = new Point(144, 40);
            cbRasi2.Name = "cbRasi2";
            cbRasi2.Size = new Size(112, 21);
            cbRasi2.TabIndex = 2;
            cbRasi2.Text = "cbRasi2";
            cbRasi2.SelectedIndexChanged += new EventHandler(cbGraha2_SelectedIndexChanged);
            // 
            // mList
            // 
            mList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mList.FullRowSelect = true;
            mList.Location = new Point(16, 72);
            mList.Name = "mList";
            mList.Size = new Size(240, 176);
            mList.TabIndex = 3;
            mList.View = View.Details;
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // cMenu
            // 
            cMenu.MenuItems.AddRange(new MenuItem[] {
                                                                                  menuOptions});
            // 
            // menuOptions
            // 
            menuOptions.Index = 0;
            menuOptions.Text = "Options";
            menuOptions.Click += new EventHandler(menuOptions_Click);
            // 
            // lVarga
            // 
            lVarga.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lVarga.Location = new Point(144, 8);
            lVarga.Name = "lVarga";
            lVarga.Size = new Size(104, 23);
            lVarga.TabIndex = 4;
            lVarga.Text = "label1";
            lVarga.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RasiStrengthsControl
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(264, 262);
            ContextMenu = cMenu;
            Controls.Add(lVarga);
            Controls.Add(mList);
            Controls.Add(cbRasi2);
            Controls.Add(cbRasi1);
            Controls.Add(cbStrength);
            Name = "RasiStrengthsControl";
            Text = "Rasi Strengths Reckoner";
            Resize += new EventHandler(RasiStrengthsControl_Resize);
            Load += new EventHandler(GrahaStrengthsControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private ArrayList GetRules()
        {
            switch (cbStrength.SelectedIndex)
            {
                case 0:
                    return Strongest.RulesNarayanaDasaRasi(h);
                case 1:
                    return Strongest.RulesNaisargikaDasaRasi(h);
                case 2:
                    return Strongest.RulesMoolaDasaRasi(h);
                case 3:
                    return Strongest.RulesKarakaKendradiGrahaDasaRasi(h);
                case 4:
                    return Strongest.RulesNavamsaDasaRasi(h);
                case 5:
                    return Strongest.RulesJaiminiFirstRasi(h);
                case 6:
                    return Strongest.RulesJaiminiSecondRasi(h);
                default:
                    return Strongest.RulesNarayanaDasaRasi(h);
            }
        }
        private void InitializeComboBoxes()
        {
            for (int i = (int)ZodiacHouseName.Ari; i <= (int)ZodiacHouseName.Pis; i++)
            {
                string s = string.Format("{0}", ((ZodiacHouseName)i).ToString());
                cbRasi1.Items.Add(s);
                cbRasi2.Items.Add(s);
            }
            cbRasi1.SelectedIndex = 0;
            cbRasi2.SelectedIndex = 6;

            cbStrength.Items.Add("Narayana Dasa");
            cbStrength.Items.Add("Naisargika Dasa");
            cbStrength.Items.Add("Moola Dasa");
            cbStrength.Items.Add("Karaka Kendradi Graha Dasa");
            cbStrength.Items.Add("Navamsa Dasa");
            cbStrength.Items.Add("Jaimini's 1st Source of Strength");
            cbStrength.Items.Add("Jaimini's 2nd Source of Strength");
            cbStrength.SelectedIndex = 0;

            lVarga.Text = options.Division.ToString();

        }

        private void Compute()
        {
            mList.BeginUpdate();
            mList.Clear();

            mList.BackColor = Color.AliceBlue;


            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Winner", -1, HorizontalAlignment.Left);

            int winner = 0;
            if (cbRasi1.SelectedIndex < 0) cbRasi1.SelectedIndex = 0;
            if (cbRasi2.SelectedIndex < 0) cbRasi2.SelectedIndex = 0;

            ZodiacHouseName z1 = (ZodiacHouseName)cbRasi1.SelectedIndex + 1;
            ZodiacHouseName z2 = (ZodiacHouseName)cbRasi2.SelectedIndex + 1;

            ArrayList al = GetRules();
            for (int i = 0; i < al.Count; i++)
            {
                ArrayList rule = new ArrayList
                {
                    al[i]
                };
                Strongest fs = new Strongest(h, options.Division, rule);
                ZodiacHouseName zw = fs.StrongerRasi(z1, z2, false, ref winner);
                ListViewItem li = new ListViewItem
                {
                    Text = string.Format("{0}", EnumDescConverter.GetEnumDescription((Enum)al[i]))
                };

                if (winner == 0)
                    li.SubItems.Add(string.Format("{0}", zw));

                mList.Items.Add(li);
            }

            mList.Columns[0].Width = -1;
            mList.Columns[1].Width = -2;

            mList.EndUpdate();


        }


        private void GrahaStrengthsControl_Load(object sender, EventArgs e)
        {


        }

        private void cbStrength_SelectedIndexChanged(object sender, EventArgs e)
        {
            Compute();
        }

        private void cbGraha1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Compute();
        }

        private void cbGraha2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Compute();
        }

        public object SetOptions(object o)
        {
            object o2 = options.SetOptions(o);
            lVarga.Text = options.Division.ToString();
            return o2;
        }
        private void menuOptions_Click(object sender, EventArgs e)
        {
            new Options(options, new ApplyOptions(SetOptions)).ShowDialog();
        }

        private void RasiStrengthsControl_Resize(object sender, EventArgs e)
        {
            PanchangAppOptions.Instance.RasiStrengthsFormSize = Size;
        }
    }
}

