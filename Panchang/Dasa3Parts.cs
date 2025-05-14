using System.Collections;
namespace org.transliteral.panchang.app
{
    /// <summary>
    /// Summary description for Dasa3Parts.
    /// </summary>
    public class Dasa3Parts : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        ToDate td = null;
        DasaEntry de = null;
        Horoscope h = null;
        private System.Windows.Forms.ListView mList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label txtDesc;


        public Dasa3Parts(Horoscope _h, DasaEntry _de, ToDate _td)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            td = _td;
            de = _de;
            h = _h;
            repopulate();
        }


        private void populateDescription()
        {
            Sweph.ObtainLock(h);
            Moment start = td.AddYears(de.startUT);
            Moment end = td.AddYears(de.startUT + de.DasaLength);
            Sweph.ReleaseLock(h);
            ZodiacHouse zh = new ZodiacHouse(de.ZHouse);
            if (de.ZHouse != 0)
                txtDesc.Text = string.Format("{0} - {1} to {2}", zh, start, end);
            else
                txtDesc.Text = string.Format("{0} - {1} to {2}", de.graha, start, end);
        }



        private void repopulate()
        {
            populateDescription();

            double partLength = de.DasaLength / 3.0;

            Sweph.ObtainLock(h);
            ArrayList alParts = new ArrayList();
            for (int i = 0; i < 4; i++)
            {
                Moment m = td.AddYears(de.startUT + partLength * i);
                alParts.Add(m);
            }
            Moment[] momentParts = (Moment[])alParts.ToArray(typeof(Moment));
            Sweph.ReleaseLock(h);

            for (int i = 1; i < momentParts.Length; i++)
            {
                string fmt = string.Format
                    ("Equal Part {0} - {1} to {2}",
                    i, momentParts[i - 1], momentParts[i]);
                mList.Items.Add(fmt);
            }

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
            txtDesc = new System.Windows.Forms.Label();
            mList = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            SuspendLayout();
            // 
            // txtDesc
            // 
            txtDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            txtDesc.Location = new System.Drawing.Point(8, 8);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new System.Drawing.Size(472, 23);
            txtDesc.TabIndex = 0;
            txtDesc.Text = "txtDesc";
            txtDesc.Click += new System.EventHandler(label1_Click);
            // 
            // mList
            // 
            mList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            mList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                    columnHeader1});
            mList.FullRowSelect = true;
            mList.Location = new System.Drawing.Point(8, 40);
            mList.Name = "mList";
            mList.Size = new System.Drawing.Size(472, 272);
            mList.TabIndex = 1;
            mList.View = System.Windows.Forms.View.Details;
            mList.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 1000;
            // 
            // Dasa3Parts
            // 
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(488, 318);
            Controls.Add(mList);
            Controls.Add(txtDesc);
            Name = "Dasa3Parts";
            Text = "Dasa 3 Parts Reckoner";
            Load += new System.EventHandler(Dasa3Parts_Load);
            ResumeLayout(false);

        }
        #endregion

        private void Dasa3Parts_Load(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
