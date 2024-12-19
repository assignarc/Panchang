

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for ProgressDialog.
    /// </summary>
    public class ProgressDialog : Form
    {
        private ProgressBar mProgress;
        private Label txtOperation;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public ProgressDialog(int max)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            mProgress.Minimum = 0;
            mProgress.Maximum = max;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            mProgress = new ProgressBar();
            txtOperation = new Label();
            SuspendLayout();
            // 
            // mProgress
            // 
            mProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                | AnchorStyles.Right;
            mProgress.Location = new Point(8, 100);
            mProgress.Name = "mProgress";
            mProgress.Size = new Size(272, 23);
            mProgress.TabIndex = 0;
            // 
            // txtOperation
            // 
            txtOperation.Location = new Point(8, 8);
            txtOperation.Name = "txtOperation";
            txtOperation.Size = new Size(272, 80);
            txtOperation.TabIndex = 1;
            txtOperation.Text = "base";
            txtOperation.Click += new EventHandler(txtOperation_Click);
            // 
            // ProgressDialog
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(292, 134);
            Controls.Add(txtOperation);
            Controls.Add(mProgress);
            Name = "ProgressDialog";
            Text = "ProgressDialog";
            ResumeLayout(false);

        }
        #endregion

        public void setText(string s)
        {
            txtOperation.Text = s;
        }
        public void setProgress(int i)
        {
            mProgress.Value = i;
        }
        private void txtOperation_Click(object sender, EventArgs e)
        {

        }
    }
}
