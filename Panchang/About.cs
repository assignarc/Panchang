namespace org.transliteral.panchang.app
{

    public class About : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkMail;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public About()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

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
                components?.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            label1 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            linkMail = new System.Windows.Forms.LinkLabel();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(144, 88);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(152, 23);
            label1.TabIndex = 0;
            // 
            // label6
            // 
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label6.ForeColor = System.Drawing.Color.Firebrick;
            label6.Location = new System.Drawing.Point(144, 32);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(100, 23);
            label6.TabIndex = 5;
            label6.Text = "Panchang";
            label6.Click += new System.EventHandler(label6_Click);
            // 
            // label7
            // 
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            label7.Location = new System.Drawing.Point(144, 48);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(100, 23);
            label7.TabIndex = 6;
            label7.Text = "Version 0.2 Alpha";
            label7.Click += new System.EventHandler(label7_Click);
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(144, 160);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(224, 16);
            label9.TabIndex = 8;
            label9.Text = "Portions (c) 2002-04 The Genghis Group";
            label9.Click += new System.EventHandler(label9_Click);
            // 
            // linkLabel1
            // 
            linkLabel1.Location = new System.Drawing.Point(144, 120);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(144, 23);
            linkLabel1.TabIndex = 10;
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked_1);
            // 
            // linkMail
            // 
            linkMail.Location = new System.Drawing.Point(144, 104);
            linkMail.Name = "linkMail";
            linkMail.Size = new System.Drawing.Size(100, 16);
            linkMail.TabIndex = 11;
            linkMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkMail_LinkClicked);
            // 
            // linkLabel2
            // 
            linkLabel2.Location = new System.Drawing.Point(144, 176);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(160, 16);
            linkLabel2.TabIndex = 12;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "http://www.genghisgroup.com";
            linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(8, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(128, 208);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // About
            // 
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(352, 246);
            Controls.Add(linkLabel2);
            Controls.Add(linkMail);
            Controls.Add(linkLabel1);
            Controls.Add(pictureBox1);
            Controls.Add(label9);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "About";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "About Panchang";
            Load += new System.EventHandler(About_Load);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }
        #endregion

        private void linkLabel1_LinkClicked_1(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabel1.Text);
            }
            catch { }
        }

        private void linkMail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:" + linkMail.Text + "?subject=Panchang");
            }
            catch { }
        }

        private void label9_Click(object sender, System.EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabel2.Text);
            }
            catch { }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void About_Load(object sender, System.EventArgs e)
        {
        }

        private void label7_Click(object sender, System.EventArgs e)
        {

        }

        private void label6_Click(object sender, System.EventArgs e)
        {

        }
    }
}
