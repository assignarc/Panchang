

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mhora
{
	/// <summary>
	/// Summary description for ProgressDialog.
	/// </summary>
	public class ProgressDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar mProgress;
		private System.Windows.Forms.Label txtOperation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProgressDialog(int max)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.mProgress.Minimum = 0;
			this.mProgress.Maximum = max;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mProgress = new System.Windows.Forms.ProgressBar();
			this.txtOperation = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mProgress
			// 
			this.mProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mProgress.Location = new System.Drawing.Point(8, 100);
			this.mProgress.Name = "mProgress";
			this.mProgress.Size = new System.Drawing.Size(272, 23);
			this.mProgress.TabIndex = 0;
			// 
			// txtOperation
			// 
			this.txtOperation.Location = new System.Drawing.Point(8, 8);
			this.txtOperation.Name = "txtOperation";
			this.txtOperation.Size = new System.Drawing.Size(272, 80);
			this.txtOperation.TabIndex = 1;
			this.txtOperation.Text = "base";
			this.txtOperation.Click += new System.EventHandler(this.txtOperation_Click);
			// 
			// ProgressDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 134);
			this.Controls.Add(this.txtOperation);
			this.Controls.Add(this.mProgress);
			this.Name = "ProgressDialog";
			this.Text = "ProgressDialog";
			this.ResumeLayout(false);

		}
		#endregion

		public void setText (string s)
		{
			this.txtOperation.Text = s;
		}
		public void setProgress (int i)
		{
			this.mProgress.Value = i;
		}
		private void txtOperation_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
