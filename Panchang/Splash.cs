

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for GenghisSplash.
    /// </summary>
    public class Splash : Form
    {
        private Label label1;
        private Label label3;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public Splash()
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
            label1 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Firebrick;
            label1.Location = new Point(31, 99);
            label1.Name = "label1";
            label1.Size = new Size(219, 22);
            label1.TabIndex = 2;
            label1.Text = "Panchang";
            label1.Click += new EventHandler(label1_Click);
            // 
            // label3
            // 
            label3.BackColor = Color.Black;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(32, 408);
            label3.Name = "label3";
            label3.Size = new Size(256, 16);
            label3.TabIndex = 4;
            label3.Text = "v0.2 Alpha ";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += new EventHandler(label3_Click);
            // 
            // Splash
            // 
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = Color.SeaShell;
            ClientSize = new Size(288, 422);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "Splash";
            Text = "Panchang";
            TransparencyKey = Color.IndianRed;
            Load += new EventHandler(GenghisSplash_Load);
            ResumeLayout(false);

        }
        #endregion

        protected override void OnActivated(EventArgs e)
        {
            BringToFront();
        }

        private void GenghisSplash_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (this != null)
            //	this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
