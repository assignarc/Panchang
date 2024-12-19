using System.Diagnostics;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for AboutSjc.
    /// </summary>
    public class AboutSjc : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label desc;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public AboutSjc()
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutSjc));
            desc = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            SuspendLayout();
            // 
            // desc
            // 
            desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            desc.Location = new System.Drawing.Point(128, 8);
            desc.Name = "desc";
            desc.Size = new System.Drawing.Size(272, 48);
            desc.TabIndex = 0;
            desc.Text = "The Jagannatha Jyotisha parampara was started at the hands of Sri Achyuta Dasa, a" +
                "nd is now led by Pandit Sanjay Rath. ";
            desc.Click += new System.EventHandler(label1_Click);
            // 
            // linkLabel1
            // 
            linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            linkLabel1.Location = new System.Drawing.Point(128, 56);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(144, 32);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "http://www.srijagannath.org";
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(112, 104);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // linkLabel2
            // 
            linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            linkLabel2.Location = new System.Drawing.Point(128, 72);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(160, 24);
            linkLabel2.TabIndex = 11;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "http://www.srath.com";
            linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
            // 
            // AboutSjc
            // 
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(416, 102);
            Controls.Add(linkLabel2);
            Controls.Add(pictureBox1);
            Controls.Add(linkLabel1);
            Controls.Add(desc);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AboutSjc";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "About Sri Jagannath Center";
            Load += new System.EventHandler(AboutSjc_Load);
            ResumeLayout(false);

        }
        #endregion

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void AboutSjc_Load(object sender, System.EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(linkLabel1.Text);
            }
            catch { }
        }

        private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(linkLabel2.Text);
            }
            catch { }
        }
    }
}
