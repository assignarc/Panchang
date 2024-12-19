using org.transliteral.panchang;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Division = org.transliteral.panchang.app.Division;
namespace org.transliteral.panchang.app
{
    public class GrahaStrengthsControl : Form
	{
        private ListView mList;
        private ComboBox cbStrength;
        private ComboBox cbGraha1;
        private ComboBox cbGraha2;
        private System.ComponentModel.IContainer components = null;
        private UserOptions options = null;
        private ContextMenu cMenu;
        private MenuItem menuOptions;
        private Label lVarga;
        private Label lColords;
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

            [@DisplayName("Varga")]
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

        public GrahaStrengthsControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
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
            cbGraha1 = new ComboBox();
            cbGraha2 = new ComboBox();
            mList = new ListView();
            cMenu = new ContextMenu();
            menuOptions = new MenuItem();
            lVarga = new Label();
            lColords = new Label();
            SuspendLayout();
            // 
            // cbStrength
            // 
            cbStrength.Anchor = AnchorStyles.Top | AnchorStyles.Left
                | AnchorStyles.Right;
            cbStrength.Location = new Point(8, 8);
            cbStrength.Name = "cbStrength";
            cbStrength.Size = new Size(120, 21);
            cbStrength.TabIndex = 0;
            cbStrength.Text = "cbStrength";
            cbStrength.SelectedIndexChanged += new EventHandler(cbStrength_SelectedIndexChanged);
            // 
            // cbGraha1
            // 
            cbGraha1.Location = new Point(8, 40);
            cbGraha1.Name = "cbGraha1";
            cbGraha1.Size = new Size(104, 21);
            cbGraha1.TabIndex = 1;
            cbGraha1.Text = "cbGraha1";
            cbGraha1.SelectedIndexChanged += new EventHandler(cbGraha1_SelectedIndexChanged);
            // 
            // cbGraha2
            // 
            cbGraha2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbGraha2.Location = new Point(152, 40);
            cbGraha2.Name = "cbGraha2";
            cbGraha2.Size = new Size(112, 21);
            cbGraha2.TabIndex = 2;
            cbGraha2.Text = "cbGraha2";
            cbGraha2.SelectedIndexChanged += new EventHandler(cbGraha2_SelectedIndexChanged);
            // 
            // mList
            // 
            mList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mList.FullRowSelect = true;
            mList.Location = new Point(16, 72);
            mList.Name = "mList";
            mList.Size = new Size(240, 208);
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
            lVarga.Text = "lVarga";
            lVarga.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lColords
            // 
            lColords.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                | AnchorStyles.Right;
            lColords.Location = new Point(16, 288);
            lColords.Name = "lColords";
            lColords.Size = new Size(240, 16);
            lColords.TabIndex = 5;
            lColords.Text = "lColords";
            lColords.Click += new EventHandler(lColords_Click);
            // 
            // GrahaStrengthsControl
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(264, 310);
            ContextMenu = cMenu;
            Controls.Add(lColords);
            Controls.Add(lVarga);
            Controls.Add(mList);
            Controls.Add(cbGraha2);
            Controls.Add(cbGraha1);
            Controls.Add(cbStrength);
            Name = "GrahaStrengthsControl";
            Text = "Graha Strengths Reckoner";
            Resize += new EventHandler(GrahaStrengthsControl_Resize);
            Load += new EventHandler(GrahaStrengthsControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        const int RCoLord = 0;
        const int RNaisargikaDasa = 1;
        const int RVimsottariDasa = 2;
        const int RKarakaKendradiGrahaDasa = 3;
        const int RCoLordKarakaKendradiGrahaDasa = 4;

        private ArrayList GetRules(ref bool bSimpleLord)
        {
            bSimpleLord = false;
            switch (cbStrength.SelectedIndex)
            {
                case 0:
                    bSimpleLord = true;
                    return FindStronger.RulesStrongerCoLord(h);
                case 1:
                    return FindStronger.RulesNaisargikaDasaGraha(h);
                case RVimsottariDasa:
                    return FindStronger.RulesVimsottariGraha(h);
                case RKarakaKendradiGrahaDasa:
                    return FindStronger.RulesKarakaKendradiGrahaDasaGraha(h);
                case RCoLordKarakaKendradiGrahaDasa:
                    return FindStronger.RulesKarakaKendradiGrahaDasaColord(h);
                default:
                    return FindStronger.RulesStrongerCoLord(h);
            }
        }
        private void InitializeComboBoxes()
        {
            for (int i = (int)Body.Name.Sun; i <= (int)Body.Name.Lagna; i++)
            {
                string s = Body.ToString((Body.Name)i);
                cbGraha1.Items.Add(s);
                cbGraha2.Items.Add(s);
            }
            cbGraha1.SelectedIndex = (int)Body.Name.Mars;
            cbGraha2.SelectedIndex = (int)Body.Name.Ketu;

            cbStrength.Items.Add("Co-Lord");
            cbStrength.Items.Add("Naisargika Graha Dasa");
            cbStrength.Items.Add("Vimsottari Dasa");
            cbStrength.Items.Add("Karaka Kendradi Graha Dasa");
            cbStrength.Items.Add("Karaka Kendradi Graha Dasa Co-Lord");
            cbStrength.SelectedIndex = 0;

            lVarga.Text = options.Division.ToString();
            populateColordLabel();

        }

        private void Compute()
        {
            mList.BeginUpdate();
            mList.Clear();

            mList.BackColor = Color.AliceBlue;


            mList.Columns.Add("Body", -1, HorizontalAlignment.Left);
            mList.Columns.Add("Winner", -1, HorizontalAlignment.Left);

            int winner = 0;
            Body.Name b1 = (Body.Name)cbGraha1.SelectedIndex;
            Body.Name b2 = (Body.Name)cbGraha2.SelectedIndex;

            bool bSimpleLord = false;
            ArrayList al = GetRules(ref bSimpleLord);
            for (int i = 0; i < al.Count; i++)
            {
                ArrayList rule = new ArrayList
                {
                    al[i]
                };
                FindStronger fs = new FindStronger(h, options.Division, rule);
                Body.Name bw = fs.StrongerGraha(b1, b2, bSimpleLord, ref winner);

                ListViewItem li = new ListViewItem
                {
                    Text = string.Format("{0}", EnumDescConverter.GetEnumDescription((Enum)al[i]))
                };

                if (winner == 0)
                    li.SubItems.Add(Body.ToString(bw));

                mList.Items.Add(li);
            }

            mList.Columns[0].Width = -1;
            mList.Columns[1].Width = -2;

            mList.EndUpdate();


        }


        private void GrahaStrengthsControl_Load(object sender, EventArgs e)
        {
            if (false == GlobalOptions.Instance.GrahaStrengthsFormSize.IsEmpty)
                Size = GlobalOptions.Instance.GrahaStrengthsFormSize;
        }

        private void cbStrength_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStrength.SelectedIndex == RVimsottariDasa)
            {
                options.Division = new Division(DivisionType.BhavaPada);
                cbGraha1.SelectedIndex = (int)Body.Name.Lagna;
                cbGraha1.SelectedIndex = (int)Body.Name.Moon;
            }
            lVarga.Text = options.Division.ToString();
            Compute();
        }

        private void cbGraha1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStrength.SelectedIndex == RCoLord)
            {
                switch (cbGraha1.SelectedIndex)
                {
                    case (int)Body.Name.Mars:
                        cbGraha2.SelectedIndex = (int)Body.Name.Ketu;
                        break;
                    case (int)Body.Name.Ketu:
                        cbGraha2.SelectedIndex = (int)Body.Name.Mars;
                        break;
                    case (int)Body.Name.Saturn:
                        cbGraha2.SelectedIndex = (int)Body.Name.Rahu;
                        break;
                    case (int)Body.Name.Rahu:
                        cbGraha2.SelectedIndex = (int)Body.Name.Saturn;
                        break;
                }
            }
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
            populateColordLabel();
            return o2;
        }
        private void menuOptions_Click(object sender, EventArgs e)
        {
            new Options(options, new ApplyOptions(SetOptions)).ShowDialog();
        }

        private void populateColordLabel()
        {
            Body.Name lAqu = h.LordOfZodiacHouse(new ZodiacHouse(ZodiacHouseName.Aqu), options.Division);
            Body.Name lSco = h.LordOfZodiacHouse(new ZodiacHouse(ZodiacHouseName.Sco), options.Division);
            lColords.Text = string.Format("{0} and {1} are the stronger co-lords", lSco, lAqu);
        }
        private void lColords_Click(object sender, EventArgs e)
        {

        }

        private void GrahaStrengthsControl_Resize(object sender, EventArgs e)
        {
            GlobalOptions.Instance.GrahaStrengthsFormSize = Size;
        }
    }
}

