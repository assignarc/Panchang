

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using org.transliteral.panchang;

namespace org.transliteral.panchang.app
{
    public class YogaControl : BaseControl
    {
        private ListView mList;
        private IContainer components = null;
        private ColumnHeader Test1;
        private ContextMenu mContext;
        private MenuItem mReset;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        FindYogas fy = null;

        public YogaControl(Horoscope _h)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            horoscope = _h;
            fy = new FindYogas(horoscope, new Division(DivisionType.Rasi));
            mList.BackColor = PanchangAppOptions.Instance.ChakraBackgroundColor;
            AddViewsToContextMenu(mContext);
            horoscope.Changed += new EvtChanged(OnRecalculate);

            evaluateYogas();
            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// 

        private void OnRecalculate(object o)
        {
            mList.Items.Clear();
        }

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
            mList = new ListView();
            Test1 = new ColumnHeader();
            mContext = new ContextMenu();
            mReset = new MenuItem();
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            SuspendLayout();
            // 
            // mList
            // 
            mList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mList.Columns.AddRange(new ColumnHeader[] {
                                                                                    Test1});
            mList.ContextMenu = mContext;
            mList.Cursor = Cursors.Default;
            mList.FullRowSelect = true;
            mList.Location = new Point(8, 8);
            mList.Name = "mList";
            mList.Size = new Size(344, 200);
            mList.TabIndex = 0;
            mList.View = View.Details;
            mList.MouseMove += new MouseEventHandler(mList_MouseMove);
            mList.SelectedIndexChanged += new EventHandler(mList_SelectedIndexChanged);
            // 
            // Test1
            // 
            Test1.Width = 141;
            // 
            // mContext
            // 
            mContext.MenuItems.AddRange(new MenuItem[] {
                                                                                     mReset,
                                                                                     menuItem1,
                                                                                     menuItem2});
            // 
            // mReset
            // 
            mReset.Index = 0;
            mReset.Text = "&Reset";
            mReset.Click += new EventHandler(mReset_Click);
            // 
            // menuItem1
            // 
            menuItem1.Index = 1;
            menuItem1.Text = "-";
            // 
            // menuItem2
            // 
            menuItem2.Index = 2;
            menuItem2.Text = "-";
            // 
            // YogaControl
            // 
            Controls.Add(mList);
            Name = "YogaControl";
            Load += new EventHandler(YogaControl_Load);
            ResumeLayout(false);

        }
        #endregion

        private void YogaControl_Load(object sender, EventArgs e)
        {

        }

        private void mList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void resetColumns()
        {
            mList.Columns.Clear();
            mList.Items.Clear();
            mList.Columns.Add("True", 30, HorizontalAlignment.Left);
            mList.Columns.Add("Cat", 70, HorizontalAlignment.Left);
            mList.Columns.Add("Yoga", 70, HorizontalAlignment.Left);
            mList.Columns.Add("Result", 500, HorizontalAlignment.Left);
            mList.Columns.Add("Rule", 500, HorizontalAlignment.Left);
        }

        private void evaluateYoga(XmlYogaNode n)
        {
            bool bRet = fy.EvaluateYoga(n);
            ListViewItem li = new ListViewItem
            {
                Text = bRet.ToString()
            };
            li.SubItems.Add(n.yogaCat);
            li.SubItems.Add(n.yogaName);
            li.SubItems.Add(n.result);
            li.SubItems.Add(n.horaRule);
            mList.Items.Add(li);
        }

        private void evaluateYogas()
        {
            try
            {
                evaluateYogasHelper();
            }
            catch
            {
                MessageBox.Show("An error occured while reading file " + PanchangAppOptions.Instance.YogasFileName);
            }
        }
        private void evaluateYogasHelper()
        {
            resetColumns();
            XmlYogaNode yn = null;
            string sLine = "";
            string sType = "";

            StreamReader objReader = new StreamReader(PanchangAppOptions.Instance.YogasFileName);

            while ((sLine = objReader.ReadLine()) != null)
            {
                if (sLine.Length > 0 && sLine[0] == '#')
                    continue;

                sType = "";


                Match m = Regex.Match(sLine, "^([^:]*)::(.*)$");
                if (m.Success)
                {
                    sType = m.Groups[1].Value;
                    sLine = m.Groups[2].Value;
                }

                switch (sType.ToLower())
                {
                    case "entry":
                        if (null != yn && yn.horaRule != null && yn.horaRule.Length > 0)
                            evaluateYoga(yn);
                        yn = new XmlYogaNode();
                        break;
                    case "rule":
                        yn.horaRule += sLine;
                        break;
                    case "sourceref":
                    case "ref":
                        yn.sourceRef += sLine;
                        break;
                    case "sourcetext":
                        yn.sourceText += sLine;
                        break;
                    case "sourceitxtext":
                        yn.sourceItxText += sLine;
                        break;
                    case "yogacat":
                        yn.yogaCat += sLine;
                        break;
                    case "yoganame":
                        yn.yogaName += sLine;
                        break;
                    default:
                    case "result":
                        if (null != yn)
                            yn.result += sLine;
                        break;
                }
            }

            objReader.Close();
            ColorAndFontRows(mList);
        }


        ToolTip tt = new ToolTip();
        private void mList_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem di = mList.GetItemAt(e.X, e.Y);

            if (di == null)
            {
                tt.Active = false;
                return;
            }

            tt.SetToolTip(this, di.SubItems[2].Text);
            tt.Active = true;
            tt.InitialDelay = 0;
        }

        private void mReset_Click(object sender, EventArgs e)
        {
            evaluateYogas();
        }

    }

}

