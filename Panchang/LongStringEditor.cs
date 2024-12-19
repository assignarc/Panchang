

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for LongStringEditor.
    /// </summary>
    public class LongStringEditor : Form
    {
        private TextBox mTextBox;
        private Button bOK;
        private Button bCancel;
        private Button bReset;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private string mTextOrig;
        public LongStringEditor(string _text)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            mTextOrig = _text;
            EditorText = mTextOrig;

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
            mTextBox = new TextBox();
            bOK = new Button();
            bCancel = new Button();
            bReset = new Button();
            SuspendLayout();
            // 
            // mTextBox
            // 
            mTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            mTextBox.Location = new Point(8, 8);
            mTextBox.Multiline = true;
            mTextBox.Name = "mTextBox";
            mTextBox.ScrollBars = ScrollBars.Both;
            mTextBox.Size = new Size(272, 224);
            mTextBox.TabIndex = 0;
            mTextBox.Text = "";
            mTextBox.TextChanged += new EventHandler(mTextBox_TextChanged);
            // 
            // bOK
            // 
            bOK.Location = new Point(16, 240);
            bOK.Name = "bOK";
            bOK.TabIndex = 1;
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);
            // 
            // bCancel
            // 
            bCancel.Location = new Point(104, 240);
            bCancel.Name = "bCancel";
            bCancel.TabIndex = 2;
            bCancel.Text = "Cancel";
            bCancel.Click += new EventHandler(bCancel_Click);
            // 
            // bReset
            // 
            bReset.Location = new Point(192, 240);
            bReset.Name = "bReset";
            bReset.TabIndex = 3;
            bReset.Text = "Reset";
            bReset.Click += new EventHandler(bReset_Click);
            // 
            // LongStringEditor
            // 
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(292, 266);
            Controls.Add(bReset);
            Controls.Add(bCancel);
            Controls.Add(bOK);
            Controls.Add(mTextBox);
            Name = "LongStringEditor";
            Load += new EventHandler(tData_Load);
            ResumeLayout(false);

        }
        #endregion


        public string EditorText
        {
            get { return mTextBox.Text; }
            set { mTextBox.Text = value; }
        }

        public string TitleText
        {
            set { Text = value; }
        }
        private void tData_Load(object sender, EventArgs e)
        {

        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            EditorText = mTextOrig;
            Close();
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            EditorText = mTextOrig;
        }

        private void mTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
