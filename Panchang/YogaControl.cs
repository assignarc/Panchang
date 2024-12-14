

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using org.transliteral.panchang;

namespace mhora
{
	public class YogaControl : mhora.MhoraControl
	{
		private System.Windows.Forms.ListView mList;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ColumnHeader Test1;
		private System.Windows.Forms.ContextMenu mContext;
		private System.Windows.Forms.MenuItem mReset;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		FindYogas fy = null;

		public YogaControl(Horoscope _h)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			h = _h;
			fy = new FindYogas(h, new Division(DivisionType.Rasi));
			this.mList.BackColor = GlobalOptions.Instance.ChakraBackgroundColor;
			this.AddViewsToContextMenu(this.mContext);
			h.Changed += new EvtChanged(OnRecalculate);

			this.evaluateYogas();
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// 

		private void OnRecalculate (Object o)
		{
			this.mList.Items.Clear();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mList = new System.Windows.Forms.ListView();
			this.Test1 = new System.Windows.Forms.ColumnHeader();
			this.mContext = new System.Windows.Forms.ContextMenu();
			this.mReset = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mList
			// 
			this.mList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					this.Test1});
			this.mList.ContextMenu = this.mContext;
			this.mList.Cursor = System.Windows.Forms.Cursors.Default;
			this.mList.FullRowSelect = true;
			this.mList.Location = new System.Drawing.Point(8, 8);
			this.mList.Name = "mList";
			this.mList.Size = new System.Drawing.Size(344, 200);
			this.mList.TabIndex = 0;
			this.mList.View = System.Windows.Forms.View.Details;
			this.mList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mList_MouseMove);
			this.mList.SelectedIndexChanged += new System.EventHandler(this.mList_SelectedIndexChanged);
			// 
			// Test1
			// 
			this.Test1.Width = 141;
			// 
			// mContext
			// 
			this.mContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mReset,
																					 this.menuItem1,
																					 this.menuItem2});
			// 
			// mReset
			// 
			this.mReset.Index = 0;
			this.mReset.Text = "&Reset";
			this.mReset.Click += new System.EventHandler(this.mReset_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// YogaControl
			// 
			this.Controls.Add(this.mList);
			this.Name = "YogaControl";
			this.Load += new System.EventHandler(this.YogaControl_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void YogaControl_Load(object sender, System.EventArgs e)
		{
		
		}

		private void mList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void resetColumns ()
		{
			this.mList.Columns.Clear();
			this.mList.Items.Clear();
			this.mList.Columns.Add ("True", 30, System.Windows.Forms.HorizontalAlignment.Left);
			this.mList.Columns.Add ("Cat", 70, System.Windows.Forms.HorizontalAlignment.Left);
			this.mList.Columns.Add ("Yoga", 70, System.Windows.Forms.HorizontalAlignment.Left);
			this.mList.Columns.Add ("Result", 500, System.Windows.Forms.HorizontalAlignment.Left);
			this.mList.Columns.Add ("Rule", 500, System.Windows.Forms.HorizontalAlignment.Left);
		}

		private void evaluateYoga (XmlYogaNode n)
		{
			bool bRet = fy.evaluateYoga(n);
			ListViewItem li = new ListViewItem();
			li.Text = bRet.ToString();
			li.SubItems.Add (n.yogaCat);
			li.SubItems.Add (n.yogaName);
			li.SubItems.Add (n.result);
			li.SubItems.Add (n.horaRule);
			this.mList.Items.Add (li);
		}

		private void evaluateYogas ()
		{
			try 
			{
				this.evaluateYogasHelper();
			}
			catch
			{
				MessageBox.Show("An error occured while reading file " + GlobalOptions.Instance.YogasFileName);
			}
		}
		private void evaluateYogasHelper ()
		{
			this.resetColumns();
			XmlYogaNode yn = null;
			string sLine = "";
			string sType = "";

			StreamReader objReader = new StreamReader(GlobalOptions.Instance.YogasFileName);

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
							this.evaluateYoga(yn);
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
			this.ColorAndFontRows(this.mList);
		}


		ToolTip tt = new ToolTip();
		private void mList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem di = this.mList.GetItemAt(e.X, e.Y);

			if (di == null) 
			{
				tt.Active = false;
				return;
			}

			tt.SetToolTip(this, di.SubItems[2].Text);
			tt.Active = true;
			tt.InitialDelay = 0;
		}

		private void mReset_Click(object sender, System.EventArgs e)
		{
			this.evaluateYogas();
		}

	}

}

