namespace ScanOdds
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.sbobetibet1 = new ScanOdds.SBOBETIBET();
            this.SuspendLayout();
            // 
            // sbobetibet1
            // 
            this.sbobetibet1.Location = new System.Drawing.Point(12, 12);
            this.sbobetibet1.Name = "sbobetibet1";
            this.sbobetibet1.Size = new System.Drawing.Size(370, 277);
            this.sbobetibet1.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 294);
            this.Controls.Add(this.sbobetibet1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private SBOBETIBET sbobetibet1;
    }
}

